using System.Numerics;
using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using UnitsNet;
using WPIMath.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath.Geometry;

public class Transform2dProto : IProtobuf<Transform2d, ProtobufTransform2d>
{
    public MessageDescriptor Descriptor => ProtobufTransform2d.Descriptor;

    public ProtobufTransform2d CreateMessage()
    {
        return new ProtobufTransform2d();
    }

    public void Pack(ProtobufTransform2d msg, Transform2d value)
    {
        Translation2d.Proto.Pack(msg.Translation, value.Translation);
        Rotation2d.Proto.Pack(msg.Rotation, value.Rotation);
    }

    public Transform2d Unpack(ProtobufTransform2d msg)
    {
        var translation = Translation2d.Proto.Unpack(msg.Translation);
        var rotation = Rotation2d.Proto.Unpack(msg.Rotation);
        return new Transform2d(translation, rotation);
    }
}

public class Transform2dStruct : IStruct<Transform2d>
{
    public string TypeString => "struct:Transform2d";

    public int Size => Translation2d.Struct.Size + Rotation2d.Struct.Size;

    public IStructBase[] Nested { get; } = [Translation2d.Struct, Rotation2d.Struct];

    public string Schema => "Translation2d translation;Rotation2d rotation";

    public void Pack(ref StructPacker buffer, Transform2d value)
    {
        Translation2d.Struct.Pack(ref buffer, value.Translation);
        Rotation2d.Struct.Pack(ref buffer, value.Rotation);
    }

    public Transform2d Unpack(ref StructUnpacker buffer)
    {
        var translation = Translation2d.Struct.Unpack(ref buffer);
        var rotation = Rotation2d.Struct.Unpack(ref buffer);
        return new Transform2d(translation, rotation);
    }
}

[JsonSerializable(typeof(Transform2d))]
public partial class Transform2dJsonContext : JsonSerializerContext
{
}

public readonly struct Transform2d : IAdditionOperators<Transform2d, Transform2d, Transform2d>,
                                    IMultiplyOperators<Transform2d, double, Transform2d>,
                                    IDivisionOperators<Transform2d, double, Transform2d>,
                                    IEqualityOperators<Transform2d, Transform2d, bool>,
                                    IAdditiveIdentity<Transform2d, Transform2d>,
                                    IEquatable<Transform2d>,
                                    IStructSerializable<Transform2d>,
                                    IProtobufSerializable<Transform2d, ProtobufTransform2d>
{
    public static IStruct<Transform2d> Struct { get; } = new Transform2dStruct();
    public static IProtobuf<Transform2d, ProtobufTransform2d> Proto { get; } = new Transform2dProto();

    [JsonInclude]
    [JsonPropertyName("translation")]
    public Translation2d Translation { get; init; }
    [JsonInclude]
    [JsonPropertyName("rotation")]
    public Rotation2d Rotation { get; init; }

    public static Transform2d AdditiveIdentity => new();

    public Transform2d() : this(new Translation2d(), new Rotation2d())
    {

    }

    [JsonConstructor]
    public Transform2d(Translation2d translation, Rotation2d rotation)
    {
        Translation = translation;
        Rotation = rotation;
    }

    public Transform2d(Pose2d initial, Pose2d last)
    {
        // We are rotating the difference between the translations
        // using a clockwise rotation matrix. This transforms the global
        // delta into a local delta (relative to the initial pose).
        Translation = (last.Translation - initial.Translation).RotateBy(-initial.Rotation);

        Rotation = last.Rotation - initial.Rotation;
    }

    public Transform2d(Length x, Length y, Rotation2d rotation)
    {
        Translation = new Translation2d(x, y);
        Rotation = rotation;
    }

    [JsonIgnore]
    public Length X => Translation.X;
    [JsonIgnore]
    public Length Y => Translation.Y;

    public Transform2d Inverse()
    {
        // We are rotating the difference between the translations
        // using a clockwise rotation matrix. This transforms the global
        // delta into a local delta (relative to the initial pose).
        var negRotation = Rotation;
        return new((-Translation).RotateBy(negRotation), negRotation);
    }

    public static Transform2d operator +(Transform2d left, Transform2d right)
    {
        return new(new Pose2d(), new Pose2d().TransformBy(left).TransformBy(right));
    }

    public static Transform2d operator *(Transform2d left, double right)
    {
        return new(left.Translation * right, left.Rotation * right);
    }

    public static Transform2d operator /(Transform2d left, double right)
    {
        return left * (1.0 / right);
    }

    public static bool operator ==(Transform2d left, Transform2d right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Transform2d left, Transform2d right)
    {
        return !(left == right);
    }

    public bool Equals(Transform2d other)
    {
        return Translation == other.Translation && Rotation == other.Rotation;
    }

    public override bool Equals(object? obj)
    {
        return obj is Transform2d d && Equals(d);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Translation, Rotation);
    }
}
