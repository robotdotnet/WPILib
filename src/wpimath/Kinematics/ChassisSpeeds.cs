using System.Numerics;
using Google.Protobuf.Reflection;
using UnitsNet;
using WPIMath.Geometry;
using WPIMath.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath;

public class ChassisSpeeds :    IAdditionOperators<ChassisSpeeds, ChassisSpeeds, ChassisSpeeds>,
                                ISubtractionOperators<ChassisSpeeds, ChassisSpeeds, ChassisSpeeds>,
                                IMultiplyOperators<ChassisSpeeds, double, ChassisSpeeds>,
                                IDivisionOperators<ChassisSpeeds, double, ChassisSpeeds>,
                                IUnaryNegationOperators<ChassisSpeeds, ChassisSpeeds>
{
    /// <summary>
    /// Chassis velocity in the X-axis (Forward is positive).
    /// </summary>
    public Speed Vx { get; } = Speed.FromMetersPerSecond(0);

    /// <summary>
    /// Chassis velocity in the Y-axis (Left is positive).
    /// </summary>
    public Speed Vy { get; } = Speed.FromMetersPerSecond(0);

    /// <summary>
    /// Chassis Angular velocity (Z-axis or theta, Counter-Clockwise is positive).
    /// </summary>
    public RotationalSpeed Omega { get; } = RotationalSpeed.FromRadiansPerSecond(0);

    public static IProtobuf<ChassisSpeeds, ProtobufChassisSpeeds> Proto { get; } = new ChassisSpeedsProto();
    public static IStruct<ChassisSpeeds> Struct { get; } = new ChassisSpeedsStruct();

    /// <summary>
    /// Constructs a new ChassisSpeeds object with zero velocity on all axes.
    /// </summary>
    public ChassisSpeeds() {}

    /// <summary>
    /// Constructs a new ChassisSpeeds object with the given velocities.
    /// </summary>
    /// <param name="Vx">Chassis velocity in the X-axis (Forward is positive).</param>
    /// <param name="Vy">Chassis velocity in the Y-axis (Left is positive).</param>
    /// <param name="Omega">Chassis Angular velocity (Z-axis or theta, Counter-Clockwise is positive).</param>
    public ChassisSpeeds(Speed Vx, Speed Vy, RotationalSpeed Omega) 
    {
        this.Vx = Vx;
        this.Vy = Vy;
        this.Omega = Omega;
    }

    public static ChassisSpeeds Discretize(Speed Vx, Speed Vy, RotationalSpeed Omega, TimeSpan Dt)
    {
        Pose2d desiredDeltaPose = new Pose2d(Vx * Dt, Vy * Dt, new Rotation2d(Omega * Dt));
        var twist = new Pose2d().Log(desiredDeltaPose);
        return new(twist.Dx / Dt, twist.Dy / Dt, twist.Dtheta / Dt);
    }

    public static ChassisSpeeds Discretize(ChassisSpeeds ContinuousSpeeds, TimeSpan Dt) =>
        Discretize(ContinuousSpeeds.Vx, ContinuousSpeeds.Vy, ContinuousSpeeds.Omega, Dt);

    public static ChassisSpeeds FromFieldRelativeSpeeds(Speed Vx, Speed Vy, RotationalSpeed Omega, Rotation2d RobotAngle)
    {
        var oneSecond = TimeSpan.FromSeconds(1);
        // clockwise rotation into robot frame, so negate the angle
        var chassisFrameVelocity = new Translation2d(Vx * oneSecond, Vy * oneSecond).RotateBy(-RobotAngle);
        return new(chassisFrameVelocity.X / oneSecond, chassisFrameVelocity.Y / oneSecond, Omega);
    }

    public static ChassisSpeeds FromFieldRelativeSpeeds(ChassisSpeeds FieldRelativeSpeeds, Rotation2d RobotAngle) =>
        FromFieldRelativeSpeeds(FieldRelativeSpeeds.Vx, FieldRelativeSpeeds.Vy, FieldRelativeSpeeds.Omega, RobotAngle);

    public static ChassisSpeeds FromRobotRelativeSpeeds(Speed Vx, Speed Vy, RotationalSpeed Omega, Rotation2d RobotAngle)
    {
        var oneSecond = TimeSpan.FromSeconds(1);
        // counter-clockwise rotation out of robot frame
        var chassisFrameVelocity = new Translation2d(Vx * oneSecond, Vy * oneSecond).RotateBy(RobotAngle);
        return new(chassisFrameVelocity.X / oneSecond, chassisFrameVelocity.Y / oneSecond, Omega);
    }

    public static ChassisSpeeds FromRobotRelativeSpeeds(ChassisSpeeds RobotRelativeSpeeds, Rotation2d RobotAngle) =>
        FromRobotRelativeSpeeds(RobotRelativeSpeeds.Vx, RobotRelativeSpeeds.Vy, RobotRelativeSpeeds.Omega, RobotAngle);

    public static ChassisSpeeds operator +(ChassisSpeeds left, ChassisSpeeds right) => 
        new(left.Vx + right.Vx, left.Vy + right.Vy, left.Omega + right.Omega);
    
    public static ChassisSpeeds operator -(ChassisSpeeds left, ChassisSpeeds right) => 
        new(left.Vx - right.Vx, left.Vy - right.Vy, left.Omega - right.Omega);

    public static ChassisSpeeds operator -(ChassisSpeeds value) =>
        new(-value.Vx, -value.Vy, -value.Omega);

    public static ChassisSpeeds operator *(ChassisSpeeds speed, double scalar) =>
        new(speed.Vx * scalar, speed.Vy * scalar, speed.Omega * scalar);

    public static ChassisSpeeds operator /(ChassisSpeeds speeds, double scalar) =>
        new(speeds.Vx / scalar, speeds.Vy / scalar, speeds.Omega / scalar);

    public override string ToString() => 
        $"ChassisSpeeds(Vx: {Vx:F2} {Vx.Unit}, Vy: {Vy:F2} {Vy.Unit}, Omega: {Omega:F2} {Omega.Unit})";
}

public class ChassisSpeedsProto : IProtobuf<ChassisSpeeds, ProtobufChassisSpeeds>
{
    public MessageDescriptor Descriptor => ProtobufChassisSpeeds.Descriptor;

    public ProtobufChassisSpeeds CreateMessage() => new();

    public void Pack(ProtobufChassisSpeeds msg, ChassisSpeeds value) 
    {
        msg.Vx = value.Vx.MetersPerSecond;
        msg.Vy = value.Vy.MetersPerSecond;
        msg.Omega = value.Omega.RadiansPerSecond;
    }

    public ChassisSpeeds Unpack(ProtobufChassisSpeeds msg)
    {
        return new(Speed.FromMetersPerSecond(msg.Vx), Speed.FromMetersPerSecond(msg.Vy), RotationalSpeed.FromRadiansPerSecond(msg.Omega));
    }
}

public class ChassisSpeedsStruct : IStruct<ChassisSpeeds>
{
    public string TypeString => "struct:ChassisSpeeds";

    public int Size => sizeof(double) * 3;

    public string Schema => "double vx;double vy;double omega";

    public void Pack(ref StructPacker buffer, ChassisSpeeds value)
    {
        buffer.WriteDouble(value.Vx.MetersPerSecond);
        buffer.WriteDouble(value.Vy.MetersPerSecond);
        buffer.WriteDouble(value.Omega.RadiansPerSecond);
    }

    public ChassisSpeeds Unpack(ref StructUnpacker buffer)
    {
        var vx = Speed.FromMetersPerSecond(buffer.ReadDouble());
        var vy = Speed.FromMetersPerSecond(buffer.ReadDouble());
        var omega = RotationalSpeed.FromRadiansPerSecond(buffer.ReadDouble());
        return new(vx, vy, omega);
    }
}
