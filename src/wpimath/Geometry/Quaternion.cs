using System.Numerics;
using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using WPIMath.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath;

public class QuaternionProto : IProtobuf<Quaternion, ProtobufQuaternion>
{
    public MessageDescriptor Descriptor => ProtobufQuaternion.Descriptor;

    public ProtobufQuaternion CreateMessage()
    {
        return new ProtobufQuaternion();
    }

    public void Pack(ProtobufQuaternion msg, Quaternion value)
    {
        msg.W = value.W;
        msg.X = value.X;
        msg.Y = value.Y;
        msg.Z = value.Z;
    }

    public Quaternion Unpack(ProtobufQuaternion msg)
    {
        double w = msg.W;
        double x = msg.X;
        double y = msg.Y;
        double z = msg.Z;
        return new Quaternion(w, x, y, z);
    }
}

public class QuaternionStruct : IStruct<Quaternion>
{
    public string TypeString => "struct:Quaternion";

    public int Size => IStructBase.SizeDouble * 4;

    public string Schema => "double w;double x;double y;double z";

    public void Pack(ref StructPacker buffer, Quaternion value)
    {
        buffer.WriteDouble(value.W);
        buffer.WriteDouble(value.X);
        buffer.WriteDouble(value.Y);
        buffer.WriteDouble(value.Z);
    }

    public Quaternion Unpack(ref StructUnpacker buffer)
    {
        double w = buffer.ReadDouble();
        double x = buffer.ReadDouble();
        double y = buffer.ReadDouble();
        double z = buffer.ReadDouble();
        return new Quaternion(w, x, y, z);
    }
}

[JsonSerializable(typeof(Quaternion))]
public partial class QuaternionJsonContext : JsonSerializerContext
{
}

public readonly struct Quaternion : IEquatable<Quaternion>, IEqualityOperators<Quaternion, Quaternion, bool>,
                        IAdditionOperators<Quaternion, Quaternion, Quaternion>,
                        ISubtractionOperators<Quaternion, Quaternion, Quaternion>,
                        IMultiplyOperators<Quaternion, double, Quaternion>,
                        IMultiplyOperators<Quaternion, Quaternion, Quaternion>,
                        IDivisionOperators<Quaternion, double, Quaternion>,
                        IStructSerializable<Quaternion>,
                        IProtobufSerializable<Quaternion, ProtobufQuaternion>

{
    public static IStruct<Quaternion> Struct { get; } = new QuaternionStruct();
    public static IProtobuf<Quaternion, ProtobufQuaternion> Proto { get; } = new QuaternionProto();

    [JsonInclude]
    public double W { get; init; }
    [JsonInclude]
    public double X { get; init; }
    [JsonInclude]
    public double Y { get; init; }
    [JsonInclude]
    public double Z { get; init; }

    public Quaternion()
    {
        W = 1.0;
        X = 0.0;
        Y = 0.0;
        Z = 0.0;
    }

    [JsonConstructor]
    public Quaternion(double w, double x, double y, double z)
    {
        W = w;
        X = x;
        Y = y;
        Z = z;
    }

    public bool Equals(Quaternion other)
    {
        var thisNorm = Norm();
        var otherNorm = other.Norm();
        return Math.Abs(Dot(other) - thisNorm * otherNorm) < 1e-9 && Math.Abs(thisNorm - otherNorm) < 1e-9;
    }

    public static bool operator ==(Quaternion left, Quaternion right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Quaternion left, Quaternion right)
    {
        return !(left == right);
    }

    public static Quaternion operator +(Quaternion left, Quaternion right)
    {
        return new(left.W + right.W, left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static Quaternion operator -(Quaternion left, Quaternion right)
    {
        return new(left.W - right.W, left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public static Quaternion operator /(Quaternion left, double right)
    {
        return new(left.W / right, left.X / right, left.Y / right, left.Z / right);
    }

    public static Quaternion operator *(Quaternion left, double right)
    {
        return new(left.W * right, left.X * right, left.Y * right, left.Z * right);
    }

    public static Quaternion operator *(Quaternion left, Quaternion right)
    {
        // https://en.wikipedia.org/wiki/Quaternion#Scalar_and_vector_parts
        var r1 = left.W;
        var r2 = right.W;

        // v₁ ⋅ v₂
        double dot = left.X * right.X + left.Y * right.Y + left.Z * right.Z;

        // v₁ x v₂
        double cross_x = left.Y * right.Z - right.Y * left.Z;
        double cross_y = right.X * left.Z - left.X * right.Z;
        double cross_z = left.X * right.Y - right.X * left.Y;

        return new(
            // r = r₁r₂ − v₁ ⋅ v₂
            r1 * r2 - dot,
            // v = r₁v₂ + r₂v₁ + v₁ x v₂
            r1 * right.X + r2 * left.X + cross_x,
            r1 * right.Y + r2 * left.Y + cross_y,
            r1 * right.Z + r2 * left.Z + cross_z);
    }

    public Quaternion Conjugate()
    {
        return new(W, -X, -Y, -Z);
    }

    public double Dot(Quaternion other)
    {
        return W * other.W + X * other.X + Y * other.Y + Z * other.Z;
    }

    public Quaternion Inverse()
    {
        var norm = Norm();
        return Conjugate() / (norm * norm);
    }

    public double Norm()
    {
        return Math.Sqrt(Dot(this));
    }

    public Quaternion Normalize()
    {
        double norm = Norm();
        if (norm == 0.0)
        {
            return new();
        }
        else
        {
            return this / norm;
        }
    }

    public Quaternion Pow(double t)
    {
        // q^t = e^(ln(q^t)) = e^(t * ln(q))
        return (this.Log() * t).Exp();
    }

    public Quaternion Exp(Quaternion adjustment)
    {
        return adjustment.Exp() * this;
    }

    public Quaternion Exp()
    {
        var scalar = Math.Exp(W);

        var axialMagnitude = Math.Sqrt(X * X + Y * Y + Z * Z);
        var cosine = Math.Cos(axialMagnitude);

        double axialScalar;

        if (axialMagnitude < 1e-9)
        {
            // Taylor series of sin(θ) / θ near θ = 0: 1 − θ²/6 + θ⁴/120 + O(n⁶)
            var axialMagnitudeSq = axialMagnitude * axialMagnitude;
            var axialMagnitudeSqSq = axialMagnitudeSq * axialMagnitudeSq;
            axialScalar = 1.0 - axialMagnitudeSq / 6.0 + axialMagnitudeSqSq / 120.0;
        }
        else
        {
            axialScalar = Math.Sin(axialMagnitude) / axialMagnitude;
        }

        return new(
            cosine * scalar,
            X * axialScalar * scalar,
            Y * axialScalar * scalar,
            Z * axialScalar * scalar);
    }

    public Quaternion Log(Quaternion end)
    {
        return (end * Inverse()).Log();
    }

    public Quaternion Log()
    {
        var norm = Norm();
        var scalar = Math.Log(norm);

        var vNorm = Math.Sqrt(X * X + Y * Y + Z * Z);

        var sNorm = W / norm;

        if (Math.Abs(sNorm + 1) < 1e-9)
        {
            return new Quaternion(scalar, -Math.PI, 0, 0);
        }

        double vScalar;

        if (vNorm < 1e-9)
        {
            // Taylor series expansion of atan2(y / x) / y around y = 0 => 1/x - y²/3*x³ + O(y⁴)
            vScalar = 1.0 / W - 1.0 / 3.0 * vNorm * vNorm / (W * W * W);
        }
        else
        {
            vScalar = Math.Atan2(vNorm, W) / vNorm;
        }

        return new Quaternion(scalar, vScalar * X, vScalar * Y, vScalar * Z);
    }

    public override bool Equals(object? obj)
    {
        return obj is Quaternion quaternion && Equals(quaternion);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(W, X, Y, Z);
    }
}
