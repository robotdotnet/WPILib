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

public class Twist3dProto : IProtobuf<Twist3d, ProtobufTwist3d>
{
    public MessageDescriptor Descriptor => ProtobufTwist3d.Descriptor;

    public ProtobufTwist3d CreateMessage()
    {
        return new ProtobufTwist3d();
    }

    public void Pack(ProtobufTwist3d msg, Twist3d value)
    {
        msg.Dx = value.dxMeters;
        msg.Dy = value.dyMeters;
        msg.Dz = value.dzMeters;
        msg.Rx = value.rxRadians;
        msg.Ry = value.ryRadians;
        msg.Rz = value.rzRadians;
    }

    public Twist3d Unpack(ProtobufTwist3d msg)
    {
        double dxMeters = msg.Dx;
        double dyMeters = msg.Dy;
        double dzMeters = msg.Dz;
        double rxRadians = msg.Rx;
        double ryRadians = msg.Ry;
        double rzRadians = msg.Rz;
        return new Twist3d(dxMeters, dyMeters, dzMeters, rxRadians, ryRadians, rzRadians);
    }
}

public class Twist3dStruct : IStruct<Twist3d>
{
    public string TypeString => "struct:Twist3d";

    public int Size => IStructBase.SizeDouble * 4;

    public string Schema => "double dx;double dy;double dtheta";

    public void Pack(ref StructPacker buffer, Twist3d value)
    {
        buffer.WriteDouble(value.dxMeters);
        buffer.WriteDouble(value.dyMeters);
        buffer.WriteDouble(value.dzMeters);
        buffer.WriteDouble(value.rxRadians);
        buffer.WriteDouble(value.ryRadians);
        buffer.WriteDouble(value.rzRadians);
    }

    public Twist3d Unpack(ref StructUnpacker buffer)
    {
        double dxMeters = buffer.ReadDouble();
        double dyMeters = buffer.ReadDouble();
        double dzMeters = buffer.ReadDouble();
        double rxRadians = buffer.ReadDouble();
        double ryRadians = buffer.ReadDouble();
        double rzRadians = buffer.ReadDouble();
        return new Twist3d(dxMeters, dyMeters, dzMeters, rxRadians, ryRadians, rzRadians);
    }
}

[JsonSerializable(typeof(Twist3d))]
public partial class Twist3dJsonContext : JsonSerializerContext
{
}

public readonly struct Twist3d : IEquatable<Twist3d>, IEqualityOperators<Twist3d, Twist3d, bool>, IMultiplyOperators<Twist3d, double, Twist3d>,
                        IStructSerializable<Twist3d>,
                        IProtobufSerializable<Twist3d, ProtobufTwist3d>

{
    public static IStruct<Twist3d> Struct { get; } = new Twist3dStruct();
    public static IProtobuf<Twist3d, ProtobufTwist3d> Proto { get; } = new Twist3dProto();

    [JsonIgnore]
    public Length Dx { get; init; }
    [JsonIgnore]
    public Length Dy { get; init; }
    [JsonIgnore]
    public Length Dz { get; init; }
    [JsonIgnore]
    public Angle Rx { get; init; }
    [JsonIgnore]
    public Angle Ry { get; init; }
    [JsonIgnore]
    public Angle Rz { get; init; }

    [JsonInclude]
    [JsonPropertyName("dx")]
    internal double dxMeters => Dx.Meters;
    [JsonInclude]
    [JsonPropertyName("dy")]
    internal double dyMeters => Dy.Meters;
    [JsonInclude]
    [JsonPropertyName("dz")]
    internal double dzMeters => Dz.Meters;
    [JsonInclude]
    [JsonPropertyName("rx")]
    internal double rxRadians => Rx.Radians;
    [JsonInclude]
    [JsonPropertyName("ry")]
    internal double ryRadians => Ry.Radians;
    [JsonInclude]
    [JsonPropertyName("rz")]
    internal double rzRadians => Rz.Radians;
    [JsonConstructor]
    internal Twist3d(double dx, double dy, double dz, double rx, double ry, double rz)
    {
        Dx = dx.Meters();
        Dy = dy.Meters();
        Dz = dz.Meters();
        Rx = rx.Radians();
        Ry = ry.Radians();
        Rz = rz.Radians();
    }

    public Twist3d() : this(0.Meters(), 0.Meters(), 0.Meters(), 0.Radians(), 0.Radians(), 0.Radians())
    {
    }

    public Twist3d(Length dx, Length dy, Length dz, Angle rx, Angle ry, Angle rz)
    {
        Dx = dx;
        Dy = dy;
        Dz = dz;
        Rx = rx;
        Ry = ry;
        Rz = rz;
    }

    public bool Equals(Twist3d other)
    {
        return Dx.Equals(other.Dx, 1e-9.Meters()) && Dy.Equals(other.Dy, 1e-9.Meters()) && Dz.Equals(other.Dz, 1e-9.Meters()) && Rx.Equals(other.Rx, 1e-9.Radians()) && Ry.Equals(other.Ry, 1e-9.Radians()) && Rz.Equals(other.Rz, 1e-9.Radians());
    }

    public static bool operator ==(Twist3d left, Twist3d right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Twist3d left, Twist3d right)
    {
        return !(left == right);
    }

    public static Twist3d operator *(Twist3d left, double right)
    {
        return new(left.Dx * right, left.Dy * right, left.Dz * right, left.Rx * right, left.Ry * right, left.Rz * right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Twist3d d && Equals(d);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Dx, Dy, Dz, Rx, Ry, Rz);
    }
}
