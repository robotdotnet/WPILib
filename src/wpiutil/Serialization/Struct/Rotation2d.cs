using System;
using System.Numerics;
using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using UnitsNet;
using UnitsNet.NumberExtensions.NumberToAngle;
using Wpi.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath.Geometry;

public class Rotation2dProto : IProtobuf<Rotation2d, ProtobufRotation2d>
{
    public MessageDescriptor Descriptor => ProtobufRotation2d.Descriptor;

    public ProtobufRotation2d CreateMessage()
    {
        return new ProtobufRotation2d();
    }

    public void Pack(ProtobufRotation2d msg, Rotation2d value)
    {
        msg.Value = value.Angle.Radians;
    }

    public Rotation2d Unpack(ProtobufRotation2d msg)
    {
        return new(msg.Value.Radians());
    }
}

public class Rotation2dStruct : IStruct<Rotation2d>
{
    public string TypeString => "struct:Rotation2d";

    public int Size => IStructBase.SizeDouble;

    public string Schema => "double value";

    public void Pack(ref StructPacker buffer, Rotation2d value)
    {
        buffer.WriteDouble(value.Angle.Radians);
    }

    public Rotation2d Unpack(ref StructUnpacker buffer)
    {
        return new Rotation2d(buffer.ReadDouble().Radians());
    }
}

public readonly struct Rotation2d : IStructSerializable<Rotation2d>,
                                    IProtobufSerializable<Rotation2d>,
                                    IAdditionOperators<Rotation2d, Rotation2d, Rotation2d>,
                                    ISubtractionOperators<Rotation2d, Rotation2d, Rotation2d>,
                                    IUnaryNegationOperators<Rotation2d, Rotation2d>,
                                    IMultiplyOperators<Rotation2d, double, Rotation2d>,
                                    IDivisionOperators<Rotation2d, double, Rotation2d>,
                                    IEqualityOperators<Rotation2d, Rotation2d, bool>,
                                    IEquatable<Rotation2d>
{
    public static IStruct<Rotation2d> Struct { get; } = new Rotation2dStruct();
    public static IProtobuf<Rotation2d> Proto { get; } = new Rotation2dProto();

    public Rotation2d()
    {

    }

    public Rotation2d(Angle angle)
    {
        Angle = angle;
        m_cos = Math.Cos(angle.Radians);
        m_sin = Math.Sin(angle.Radians);
    }

    public Rotation2d(double x, double y)
    {
        double magnitude = double.Hypot(x, y);
        if (magnitude > 1e-6)
        {
            m_sin = y / magnitude;
            m_cos = x / magnitude;
        }
        else
        {
            m_sin = 0.0;
            m_cos = 1.0;
        }
        Angle = Math.Atan2(m_sin, m_cos).Radians();
    }

    [JsonConstructor]
#pragma warning disable IDE0051 // Remove unused private members
    private Rotation2d(double radians) : this(radians.Radians())
    {

    }

    [JsonInclude]
    [JsonPropertyName("radians")]
    private readonly double Radians => Angle.Radians;
#pragma warning restore IDE0051 // Remove unused private members

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public Angle Angle { get; } = 0.Radians();

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public double Cos => m_cos;
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public double Sin => m_sin;
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public double Tan => Sin / Cos;

    private readonly double m_cos = 1;
    private readonly double m_sin = 0;

    public readonly Rotation2d RotateBy(Rotation2d other)
    {
        return new(Cos * other.Cos - Sin * other.Sin, Cos * other.Sin + Sin * other.Cos);
    }

    public static implicit operator Rotation2d(Angle angle)
    {
        return new(angle);
    }

    public static Rotation2d operator +(Rotation2d left, Rotation2d right)
    {
        return left.RotateBy(right);
    }

    public static Rotation2d operator -(Rotation2d left, Rotation2d right)
    {
        return left + -right;
    }

    public static Rotation2d operator -(Rotation2d value)
    {
        return new(-value.Angle);
    }

    public static Rotation2d operator *(Rotation2d left, double right)
    {
        return new(left.Angle * right);
    }

    public static Rotation2d operator /(Rotation2d left, double right)
    {
        return new(left.Angle / right);
    }

    public static bool operator ==(Rotation2d left, Rotation2d right)
    {
        return double.Hypot(left.Cos - right.Cos, left.Sin - right.Sin) < 1E-9;
    }

    public static bool operator !=(Rotation2d left, Rotation2d right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Rotation2d rot && Equals(rot);
    }

    public override int GetHashCode()
    {
        return Angle.GetHashCode();
    }

    public bool Equals(Rotation2d other)
    {
        return this == other;
    }
}
