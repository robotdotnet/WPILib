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

public class Pose2dProto : IProtobuf<Pose2d, ProtobufPose2d>
{
    public MessageDescriptor Descriptor => ProtobufPose2d.Descriptor;

    public ProtobufPose2d CreateMessage() => new();

    public void Pack(ProtobufPose2d msg, Pose2d value)
    {
        Translation2d.Proto.Pack(msg.Translation, value.Translation);
        Rotation2d.Proto.Pack(msg.Rotation, value.Rotation);
    }

    public Pose2d Unpack(ProtobufPose2d msg)
    {
        Translation2d translation = Translation2d.Proto.Unpack(msg.Translation);
        Rotation2d rotation = Rotation2d.Proto.Unpack(msg.Rotation);
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

public readonly struct Pose2d : IStructSerializable<Pose2d>, IProtobufSerializable<Pose2d, ProtobufPose2d>,
                                    IMultiplyOperators<Pose2d, double, Pose2d>,
                                    IAdditionOperators<Pose2d, Transform2d, Pose2d>,
                                    ISubtractionOperators<Pose2d, Pose2d, Transform2d>,
                                    IDivisionOperators<Pose2d, double, Pose2d>,
    IEqualityOperators<Pose2d, Pose2d, bool>,
        IInterpolatable<Pose2d>,
                                    IEquatable<Pose2d>
{
    public static IStruct<Pose2d> Struct { get; } = new Pose2dStruct();
    public static IProtobuf<Pose2d, ProtobufPose2d> Proto { get; } = new Pose2dProto();

    [JsonInclude]
    [JsonPropertyName("translation")]
    public Translation2d Translation { get; init; }
    [JsonInclude]
    [JsonPropertyName("rotation")]
    public Rotation2d Rotation { get; init; }

    [JsonIgnore]
    public Length X => Translation.X;
    [JsonIgnore]
    public Length Y => Translation.Y;

    public Pose2d() : this(new(), new())
    {

    }

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

    public Pose2d TransformBy(Transform2d other)
    {
        return new Pose2d(Translation + other.Translation.RotateBy(Rotation), other.Rotation + Rotation);
    }

    public Pose2d RotateBy(Rotation2d other)
    {
        return new Pose2d(Translation.RotateBy(other), Rotation.RotateBy(other));
    }

    public Pose2d RelativeTo(Pose2d other)
    {
        var transform = new Transform2d(other, this);
        return new Pose2d(transform.Translation, transform.Rotation);
    }

    public Pose2d Exp(Twist2d twist)
    {
        double dx = twist.Dx.Meters;
        double dy = twist.Dy.Meters;
        double dtheta = twist.Dtheta.Radians;

        double sinTheta = Math.Sin(dtheta);
        double cosTheta = Math.Cos(dtheta);

        double s;
        double c;
        if (Math.Abs(dtheta) < 1E-9)
        {
            s = 1.0 - 1.0 / 6.0 * dtheta * dtheta;
            c = 0.5 * dtheta;
        }
        else
        {
            s = sinTheta / dtheta;
            c = (1 - cosTheta) / dtheta;
        }
        var transform =
            new Transform2d(
                new Translation2d(dx * s - dy * c, dx * c + dy * s),
                new Rotation2d(cosTheta, sinTheta));

        return this + transform;
    }

    public Twist2d Log(Pose2d end)
    {
        var transform = end.RelativeTo(this);
        var dtheta = transform.Rotation.Angle.Radians;
        var halfDtheta = dtheta / 2.0;

        var cosMinusOne = transform.Rotation.Cos - 1;

        double halfThetaByTanOfHalfDtheta;
        if (Math.Abs(cosMinusOne) < 1E-9)
        {
            halfThetaByTanOfHalfDtheta = 1.0 - 1.0 / 12.0 * dtheta * dtheta;
        }
        else
        {
            halfThetaByTanOfHalfDtheta = -(halfDtheta * transform.Rotation.Sin) / cosMinusOne;
        }

        Translation2d translationPart =
            transform
                .Translation
                .RotateBy(new Rotation2d(halfThetaByTanOfHalfDtheta, -halfDtheta))
                 * double.Hypot(halfThetaByTanOfHalfDtheta, halfDtheta);

        return new Twist2d(translationPart.X, translationPart.Y, dtheta.Radians());
    }

    public Pose2d Nearest(ReadOnlySpan<Pose2d> poses)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Pose2d left, Pose2d right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Pose2d left, Pose2d right)
    {
        return !(left == right);
    }

    public static Pose2d operator *(Pose2d left, double right)
    {
        return new Pose2d(left.Translation * right, left.Rotation * right);
    }

    public static Pose2d operator /(Pose2d left, double right)
    {
        return left * (1.0 / right);
    }

    public static Pose2d operator +(Pose2d left, Transform2d right)
    {
        return left.TransformBy(right);
    }

    public static Transform2d operator -(Pose2d left, Pose2d right)
    {
        var pose = left.RelativeTo(right);
        return new(pose.Translation, pose.Rotation);
    }

    public Pose2d Interpolate(Pose2d endValue, double t)
    {
        if (t < 0)
        {
            return this;
        }
        else if (t >= 1)
        {
            return endValue;
        }
        else
        {
            var twist = Log(endValue);
            var scaledTwist = new Twist2d(twist.Dx * t, twist.Dy * t, twist.Dtheta * t);
            return Exp(scaledTwist);
        }
    }

    public bool Equals(Pose2d other)
    {
        return Translation == other.Translation && Rotation == other.Rotation;
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
