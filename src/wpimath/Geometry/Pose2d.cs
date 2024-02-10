using System;
using System.ComponentModel;
using System.Numerics;
using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using UnitsNet;
using WPIMath.Interpolation;
using WPIMath.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath.Geometry;

public class Pose2dProto : IProtobuf<Pose2d, ProtobufPose2d>
{
    public MessageDescriptor Descriptor => ProtobufPose2d.Descriptor;

    public ProtobufPose2d CreateMessage() => new();

    public void Pack(ProtobufPose2d msg, Pose2d value)
    {
        Translation2d.Proto.Pack(msg, value.Translation);
        Rotation2d.Proto.Pack(msg, value.Rotation);
    }

    public Pose2d Unpack(ProtobufPose2d msg)
    {
        Translation2d translation = Translation2d.Proto.Unpack(msg);
        Rotation2d rotation = Rotation2d.Proto.Unpack(msg);
        return new(translation, rotation);
    }
}

public class Pose2dStruct : IStruct<Pose2d>
{
    public string TypeString => "struct:Pose2d";

    public int Size => Translation2d.Struct.Size + Rotation2d.Struct.Size;
    public string Schema => "Translation2d translation;Rotation2d rotation";

    public IStructBase[] Nested { get; } = [Translation2d.Struct, Rotation2d.Struct];

    public void Pack(ref StructPacker buffer, Pose2d value)
    {
        Translation2d.Struct.Pack(ref buffer, value.Translation);
        Rotation2d.Struct.Pack(ref buffer, value.Rotation);
    }

    public Pose2d Unpack(ref StructUnpacker buffer)
    {
        Translation2d translation = Translation2d.Struct.Unpack(ref buffer);
        Rotation2d rotation = Rotation2d.Struct.Unpack(ref buffer);
        return new(translation, rotation);
    }
}

[JsonSerializable(typeof(Pose2d))]
public partial class Pose2dJsonContext : JsonSerializerContext
{
}

public readonly struct Pose2d : IStructSerializable<Pose2d>, IProtobufSerializable<Pose2d>,
                                    IMultiplyOperators<Pose2d, double, Pose2d>,
                                    IDivisionOperators<Pose2d, double, Pose2d>,
    IEqualityOperators<Pose2d, Pose2d, bool>,
        IInterpolatable<Pose2d>,
                                    IEquatable<Pose2d>
{
    public static IStruct<Pose2d> Struct { get; } = new Pose2dStruct();
    public static IProtobuf<Pose2d, ProtobufPose2d> Proto { get; } = new Pose2dProto();
    static IProtobuf<Pose2d> IProtobufSerializable<Pose2d>.Proto => Proto;

    [JsonInclude]
    [JsonPropertyName("translation")]
    public Translation2d Translation { get; }
    [JsonInclude]
    [JsonPropertyName("rotation")]
    public Rotation2d Rotation { get; }

    [JsonIgnore]
    public Length X => Translation.X;
    [JsonIgnore]
    public Length Y => Translation.Y;


    [JsonConstructor]
    public Pose2d(Translation2d translation, Rotation2d rotation)
    {
        Translation = translation;
        Rotation = rotation;
    }

    public Pose2d(Length x, Length y, Rotation2d rotation)
    {
        Translation = new(x, y);
        Rotation = rotation;
    }

    public Pose2d RotateBy(Rotation2d other)
    {
        throw new NotImplementedException();
    }

    public Pose2d RelativeTo(Pose2d other)
    {
        throw new NotImplementedException();
    }

    public Pose2d Nearest(ReadOnlySpan<Pose2d> poses)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Pose2d left, Pose2d right)
    {
        throw new NotImplementedException();
    }

    public static bool operator !=(Pose2d left, Pose2d right)
    {
        throw new NotImplementedException();
    }

    public static Pose2d operator *(Pose2d left, double right)
    {
        throw new NotImplementedException();
    }

    public static Pose2d operator /(Pose2d left, double right)
    {
        throw new NotImplementedException();
    }

    public Pose2d Interpolate(Pose2d endValue, double t)
    {
        throw new NotImplementedException();
    }

    public bool Equals(Pose2d other)
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return obj is Pose2d d && Equals(d);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Translation, Rotation);
    }
}
