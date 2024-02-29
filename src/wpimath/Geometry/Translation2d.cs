using System.Numerics;
using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using UnitsNet;
using UnitsNet.NumberExtensions.NumberToLength;
using WPIMath.Interpolation;
using WPIMath.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath.Geometry;

public class Translation2dProto : IProtobuf<Translation2d, ProtobufTranslation2d>
{
    public MessageDescriptor Descriptor => ProtobufTranslation2d.Descriptor;

    public ProtobufTranslation2d CreateMessage()
    {
        return new ProtobufTranslation2d();
    }

    public void Pack(ProtobufTranslation2d msg, Translation2d value)
    {
        msg.X = value.X.Meters;
        msg.Y = value.Y.Meters;
    }

    public Translation2d Unpack(ProtobufTranslation2d msg)
    {
        return new(msg.X, msg.Y);
    }
}

public class Translation2dStruct : IStruct<Translation2d>
{
    public string TypeString => "struct:Translation2d";

    public int Size => IStructBase.SizeDouble * 2;

    public string Schema => "double x;double y";

    public void Pack(ref StructPacker buffer, Translation2d value)
    {
        buffer.WriteDouble(value.X.Meters);
        buffer.WriteDouble(value.Y.Meters);
    }

    public Translation2d Unpack(ref StructUnpacker buffer)
    {
        double x = buffer.ReadDouble();
        double y = buffer.ReadDouble();
        return new(x, y);
    }
}

[JsonSerializable(typeof(Translation2d))]
public partial class Translation2dJsonContext : JsonSerializerContext
{
}

public readonly struct Translation2d : IStructSerializable<Translation2d>, IProtobufSerializable<Translation2d, ProtobufTranslation2d>,
                                        IAdditionOperators<Translation2d, Translation2d, Translation2d>,
                                    ISubtractionOperators<Translation2d, Translation2d, Translation2d>,
                                    IUnaryNegationOperators<Translation2d, Translation2d>,
                                    IMultiplyOperators<Translation2d, double, Translation2d>,
                                    IDivisionOperators<Translation2d, double, Translation2d>,
                                    IEqualityOperators<Translation2d, Translation2d, bool>,
                                    IAdditiveIdentity<Translation2d, Translation2d>,
                                    IInterpolatable<Translation2d>,
                                    IEquatable<Translation2d>
{
    public static IStruct<Translation2d> Struct { get; } = new Translation2dStruct();
    public static IProtobuf<Translation2d, ProtobufTranslation2d> Proto { get; } = new Translation2dProto();

    [JsonIgnore]
    public Length X { get; }
    [JsonIgnore]
    public Length Y { get; }

    [JsonConstructor]
    internal Translation2d(double x, double y)
    {
        X = x.Meters();
        Y = y.Meters();
    }
    [JsonInclude]
    [JsonPropertyName("x")]
    internal double JsonX => X.Meters;
    [JsonInclude]
    [JsonPropertyName("y")]
    internal double JsonY => Y.Meters;

    public Translation2d() : this(0.Meters(), 0.Meters())
    {
    }

    public Translation2d(Length x, Length y)
    {
        X = x;
        Y = y;
    }

    public Translation2d(Length distance, Rotation2d angle)
    {
        X = distance * angle.Cos;
        Y = distance * angle.Sin;
    }

    [JsonIgnore]
    public Length Norm => double.Hypot(X.Meters, Y.Meters).Meters();

    [JsonIgnore]
    public Rotation2d Angle => new(X.Meters, Y.Meters);

    public static Translation2d AdditiveIdentity => new();

    public Length GetDistance(Translation2d other)
    {
        return double.Hypot((other.X - X).Meters, (other.Y - Y).Meters).Meters();
    }

    public Translation2d RotateBy(Rotation2d other)
    {
        return new(X * other.Cos - Y * other.Sin, X * other.Sin + Y * other.Cos);
    }

    public Translation2d Nearest(ReadOnlySpan<Translation2d> translations)
    {
        throw new NotImplementedException();
    }

    public Translation2d Interpolate(Translation2d endValue, double t)
    {
        return new Translation2d(MathExtras.Lerp(X, endValue.X, t), MathExtras.Lerp(Y, endValue.Y, t));
    }

    public bool Equals(Translation2d other)
    {
        return X.Equals(other.X, 1e-9.Meters()) && Y.Equals(other.Y, 1e-9.Meters());
    }

    public static Translation2d operator +(Translation2d left, Translation2d right)
    {
        return new(left.X + right.X, left.Y + right.Y);
    }

    public static Translation2d operator -(Translation2d left, Translation2d right)
    {
        return new Translation2d(left.X - right.X, left.Y - right.Y);
    }

    public static Translation2d operator -(Translation2d value)
    {
        return new Translation2d(-value.X, -value.Y);
    }

    public static Translation2d operator *(Translation2d left, double right)
    {
        return new(left.X * right, left.Y * right);
    }

    public static Translation2d operator /(Translation2d left, double right)
    {
        return new(left.X / right, left.Y / right);
    }

    public static bool operator ==(Translation2d left, Translation2d right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Translation2d left, Translation2d right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Translation2d d && Equals(d);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
