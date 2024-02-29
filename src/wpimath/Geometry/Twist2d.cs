using System.Numerics;
using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using UnitsNet;
using UnitsNet.NumberExtensions.NumberToAngle;
using UnitsNet.NumberExtensions.NumberToLength;
using WPIMath.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath.Geometry;

public class Twist2dProto : IProtobuf<Twist2d, ProtobufTwist2d>
{
    public MessageDescriptor Descriptor => ProtobufTwist2d.Descriptor;

    public ProtobufTwist2d CreateMessage()
    {
        return new ProtobufTwist2d();
    }

    public void Pack(ProtobufTwist2d msg, Twist2d value)
    {
        msg.Dx = value.dxMeters;
        msg.Dy = value.dyMeters;
        msg.Dtheta = value.dthetaRadians;
    }

    public Twist2d Unpack(ProtobufTwist2d msg)
    {
        double dxMeters = msg.Dx;
        double dyMeters = msg.Dy;
        double dthetaRadians = msg.Dtheta;
        return new Twist2d(dxMeters, dyMeters, dthetaRadians);
    }
}

public class Twist2dStruct : IStruct<Twist2d>
{
    public string TypeString => "struct:Twist2d";

    public int Size => IStructBase.SizeDouble * 4;

    public string Schema => "double dx;double dy;double dtheta";

    public void Pack(ref StructPacker buffer, Twist2d value)
    {
        buffer.WriteDouble(value.dxMeters);
        buffer.WriteDouble(value.dyMeters);
        buffer.WriteDouble(value.dthetaRadians);
    }

    public Twist2d Unpack(ref StructUnpacker buffer)
    {
        double dxMeters = buffer.ReadDouble();
        double dyMeters = buffer.ReadDouble();
        double dthetaRadians = buffer.ReadDouble();
        return new Twist2d(dxMeters, dyMeters, dthetaRadians);
    }
}

[JsonSerializable(typeof(Twist2d))]
public partial class Twist2dJsonContext : JsonSerializerContext
{
}

public readonly struct Twist2d : IEquatable<Twist2d>, IEqualityOperators<Twist2d, Twist2d, bool>, IMultiplyOperators<Twist2d, double, Twist2d>,
                        IStructSerializable<Twist2d>,
                        IProtobufSerializable<Twist2d, ProtobufTwist2d>

{
    public static IStruct<Twist2d> Struct { get; } = new Twist2dStruct();
    public static IProtobuf<Twist2d, ProtobufTwist2d> Proto { get; } = new Twist2dProto();

    [JsonIgnore]
    public Length Dx { get; init; }
    [JsonIgnore]
    public Length Dy { get; init; }
    [JsonIgnore]
    public Angle Dtheta { get; init; }

    [JsonInclude]
    [JsonPropertyName("dx")]
    internal double dxMeters => Dx.Meters;
    [JsonInclude]
    [JsonPropertyName("dy")]
    internal double dyMeters => Dy.Meters;
    [JsonInclude]
    [JsonPropertyName("dtheta")]
    internal double dthetaRadians => Dtheta.Radians;
    [JsonConstructor]
    internal Twist2d(double dx, double dy, double dtheta)
    {
        Dx = dx.Meters();
        Dy = dy.Meters();
        Dtheta = dtheta.Radians();
    }

    public Twist2d() : this(0.Meters(), 0.Meters(), 0.Radians())
    {
    }

    public Twist2d(Length dx, Length dy, Angle dtheta)
    {
        Dx = dx;
        Dy = dy;
        Dtheta = dtheta;
    }

    public bool Equals(Twist2d other)
    {
        return Dx.Equals(other.Dx, 1e-9.Meters()) && Dy.Equals(other.Dy, 1e-9.Meters()) && Dtheta.Equals(other.Dtheta, 1e-9.Radians());
    }

    public static bool operator ==(Twist2d left, Twist2d right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Twist2d left, Twist2d right)
    {
        return !(left == right);
    }

    public static Twist2d operator *(Twist2d left, double right)
    {
        return new(left.Dx * right, left.Dy * right, left.Dtheta * right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Twist2d d && Equals(d);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Dx, Dy, Dtheta);
    }
}
