using System.Numerics;
using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using UnitsNet;
using UnitsNet.NumberExtensions.NumberToAngle;
using WPIMath.Interpolation;
using WPIMath.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath.Geometry;

public class Rotation3dProto : IProtobuf<Rotation3d, ProtobufRotation3d>
{
    public MessageDescriptor Descriptor => ProtobufRotation3d.Descriptor;

    public ProtobufRotation3d CreateMessage()
    {
        return new ProtobufRotation3d();
    }

    public void Pack(ProtobufRotation3d msg, Rotation3d value)
    {
        Quaternion.Proto.Pack(msg.Q, value.Quaternion);
    }

    public Rotation3d Unpack(ProtobufRotation3d msg)
    {
        Quaternion q = Quaternion.Proto.Unpack(msg.Q);
        return new(q);
    }
}

public class Rotation3dStruct : IStruct<Rotation3d>
{
    public string TypeString => "struct:Rotation3d";

    public int Size => IStructBase.SizeDouble;

    public string Schema => "Quaternion q";

    public IStructBase[] Nested { get; } = [Quaternion.Struct];

    public void Pack(ref StructPacker buffer, Rotation3d value)
    {
        Quaternion.Struct.Pack(ref buffer, value.Quaternion);
    }

    public Rotation3d Unpack(ref StructUnpacker buffer)
    {
        Quaternion q = Quaternion.Struct.Unpack(ref buffer);
        return new Rotation3d(q);
    }
}

[JsonSerializable(typeof(Rotation3d))]
public partial class Rotation3dJsonContext : JsonSerializerContext
{
}

public readonly struct Rotation3d : IStructSerializable<Rotation3d>,
                                    IProtobufSerializable<Rotation3d, ProtobufRotation3d>,
                                    IAdditionOperators<Rotation3d, Rotation3d, Rotation3d>,
                                    ISubtractionOperators<Rotation3d, Rotation3d, Rotation3d>,
                                    IUnaryNegationOperators<Rotation3d, Rotation3d>,
                                    IMultiplyOperators<Rotation3d, double, Rotation3d>,
                                    IDivisionOperators<Rotation3d, double, Rotation3d>,
                                    IEqualityOperators<Rotation3d, Rotation3d, bool>,
                                    IAdditiveIdentity<Rotation3d, Rotation3d>,
                                    IInterpolatable<Rotation3d>,
                                    IEquatable<Rotation3d>
{
    public static IStruct<Rotation3d> Struct { get; } = new Rotation3dStruct();
    public static IProtobuf<Rotation3d, ProtobufRotation3d> Proto { get; } = new Rotation3dProto();

    [JsonInclude]
    [JsonPropertyName("quaternion")]
    public Quaternion Quaternion { get; init; }

    public Rotation3d() : this(new())
    {

    }

    [JsonConstructor]
    public Rotation3d(Quaternion quaternion)
    {
        Quaternion = quaternion;
    }

    public Rotation3d(Angle roll, Angle pitch, Angle yaw)
    {
        // https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles#Euler_angles_to_quaternion_conversion
        double rollRadians = roll.Radians;
        double pitchRadians = pitch.Radians;
        double yawRadians = yaw.Radians;

        double cr = Math.Cos(rollRadians * 0.5);
        double sr = Math.Sin(rollRadians * 0.5);

        double cp = Math.Cos(pitchRadians * 0.5);
        double sp = Math.Sin(pitchRadians * 0.5);

        double cy = Math.Cos(yawRadians * 0.5);
        double sy = Math.Sin(yawRadians * 0.5);

        Quaternion =
            new Quaternion(
                cr * cp * cy + sr * sp * sy,
                sr * cp * cy - cr * sp * sy,
                cr * sp * cy + sr * cp * sy,
                cr * cp * sy - sr * sp * cy);
    }

    [JsonIgnore]
    public double X
    {
        get
        {
            var w = Quaternion.W;
            var x = Quaternion.X;
            var y = Quaternion.Y;
            var z = Quaternion.Z;

            // wpimath/algorithms.md
            var cxcy = 1.0 - 2.0 * (x * x + y * y);
            var sxcy = 2.0 * (w * x + y * z);
            var cy_sq = cxcy * cxcy + sxcy * sxcy;
            if (cy_sq > 1e-20)
            {
                return Math.Atan2(sxcy, cxcy);
            }
            else
            {
                return 0.0;
            }
        }
    }

    [JsonIgnore]
    public double Y
    {
        get
        {
            var w = Quaternion.W;
            var x = Quaternion.X;
            var y = Quaternion.Y;
            var z = Quaternion.Z;

            // https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles#Quaternion_to_Euler_angles_(in_3-2-1_sequence)_conversion
            double ratio = 2.0 * (w * y - z * x);
            if (Math.Abs(ratio) >= 1.0)
            {
                return Math.CopySign(Math.PI / 2.0, ratio);
            }
            else
            {
                return Math.Asin(ratio);
            }
        }
    }

    [JsonIgnore]
    public double Z
    {
        get
        {
            var w = Quaternion.W;
            var x = Quaternion.X;
            var y = Quaternion.Y;
            var z = Quaternion.Z;

            // wpimath/algorithms.md
            var cycz = 1.0 - 2.0 * (y * y + z * z);
            var cysz = 2.0 * (w * z + x * y);
            var cy_sq = cycz * cycz + cysz * cysz;
            if (cy_sq > 1e-20)
            {
                return Math.Atan2(cysz, cycz);
            }
            else
            {
                return Math.Atan2(2.0 * w * z, w * w - z * z);
            }
        }
    }

    [JsonIgnore]
    public Angle Angle
    {
        get
        {
            var w = Quaternion.W;
            var x = Quaternion.X;
            var y = Quaternion.Y;
            var z = Quaternion.Z;

            double norm =
                Math.Sqrt(x * x + y * y + z * z);
            return (2.0 * Math.Atan2(norm, w)).Radians();
        }
    }

    public Rotation2d ToRotation2d()
    {
        return new Rotation2d(Z);
    }

    public static Rotation3d AdditiveIdentity => new();

    public readonly Rotation3d RotateBy(Rotation3d other)
    {
        return new(other.Quaternion * Quaternion);
    }

    public static Rotation3d operator +(Rotation3d left, Rotation3d right)
    {
        return left.RotateBy(right);
    }

    public static Rotation3d operator -(Rotation3d left, Rotation3d right)
    {
        return left + -right;
    }

    public static Rotation3d operator -(Rotation3d value)
    {
        return new(value.Quaternion.Inverse());
    }

    public static Rotation3d operator *(Rotation3d left, double right)
    {
        throw new NotImplementedException();
    }

    public static Rotation3d operator /(Rotation3d left, double right)
    {
        return left * (1.0 / right);
    }

    public static bool operator ==(Rotation3d left, Rotation3d right)
    {
        return Math.Abs(Math.Abs(left.Quaternion.Dot(right.Quaternion)) - left.Quaternion.Norm() * right.Quaternion.Norm()) < 1e-9;
    }

    public static bool operator !=(Rotation3d left, Rotation3d right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Rotation3d rot && Equals(rot);
    }

    public override int GetHashCode()
    {
        return Quaternion.GetHashCode();
    }

    public bool Equals(Rotation3d other)
    {
        return this == other;
    }

    public Rotation3d Interpolate(Rotation3d endValue, double t)
    {
        return MathExtras.Lerp(this, endValue, t);
    }

}
