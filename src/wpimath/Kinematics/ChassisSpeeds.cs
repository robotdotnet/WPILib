using System.Numerics;
using Google.Protobuf.Reflection;
using UnitsNet;
using UnitsNet.NumberExtensions.NumberToRotationalSpeed;
using UnitsNet.NumberExtensions.NumberToSpeed;
using WPIMath.Geometry;
using WPIMath.Proto;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath;

public readonly struct ChassisSpeeds : IAdditionOperators<ChassisSpeeds, ChassisSpeeds, ChassisSpeeds>,
                                ISubtractionOperators<ChassisSpeeds, ChassisSpeeds, ChassisSpeeds>,
                                IMultiplyOperators<ChassisSpeeds, double, ChassisSpeeds>,
                                IDivisionOperators<ChassisSpeeds, double, ChassisSpeeds>,
                                IUnaryNegationOperators<ChassisSpeeds, ChassisSpeeds>,
                                IEquatable<ChassisSpeeds>
{
    /// <summary>
    /// Chassis velocity in the X-axis (Forward is positive).
    /// </summary>
    public Speed Vx { get; }

    /// <summary>
    /// Chassis velocity in the Y-axis (Left is positive).
    /// </summary>
    public Speed Vy { get; }

    /// <summary>
    /// Chassis Angular velocity (Z-axis or theta, Counter-Clockwise is positive).
    /// </summary>
    public RotationalSpeed Omega { get; }

    public static IProtobuf<ChassisSpeeds, ProtobufChassisSpeeds> Proto { get; } = new ChassisSpeedsProto();
    public static IStruct<ChassisSpeeds> Struct { get; } = new ChassisSpeedsStruct();

    /// <summary>
    /// Constructs a new ChassisSpeeds object with zero velocity on all axes.
    /// </summary>
    public ChassisSpeeds() { }

    /// <summary>
    /// Constructs a new ChassisSpeeds object with the given velocities.
    /// </summary>
    /// <param name="vx">Chassis velocity in the X-axis (Forward is positive).</param>
    /// <param name="vy">Chassis velocity in the Y-axis (Left is positive).</param>
    /// <param name="omega">Chassis Angular velocity (Z-axis or theta, Counter-Clockwise is positive).</param>
    public ChassisSpeeds(Speed vx, Speed vy, RotationalSpeed omega)
    {
        Vx = vx;
        Vy = vy;
        Omega = omega;
    }

    /// <summary>
    /// Constructs a new ChassisSpeeds object with the given velocities.
    /// </summary>
    /// <param name="vx">Chassis velocity in the X-axis (Forward is positive) in meters per second.</param>
    /// <param name="vy">Chassis velocity in the Y-axis (Left is positive) in meters per second.</param>
    /// <param name="omega">Chassis Angular velocity (Z-axis or theta, Counter-Clockwise is positive) in radians per second.</param>
    public ChassisSpeeds(double vx, double vy, double omega)
    {
        Vx = vx.MetersPerSecond();
        Vy = vy.MetersPerSecond();
        Omega = omega.RadiansPerSecond();
    }

    /// <summary>
    /// Discretizes a continuous-time chassis speed.
    /// 
    /// <para>
    /// This function converts a continuous-time chassis speed into a discrete-time one such that
    /// when the discrete-time chassis speed is applied for one timestep, the robot moves as if the
    /// velocity components are independent (i.e., the robot moves v_x * dt along the x-axis, v_y * dt
    /// along the y-axis, and omega * dt around the z-axis).
    /// </para>
    /// <para>
    /// This is useful for compensating for translational skew when translating and rotating a
    /// swerve drivetrain.
    /// </para>
    /// </summary>
    /// <param name="vx">Forward velocity.</param>
    /// <param name="vy">Sideways velocity (left is positive).</param>
    /// <param name="omega">Angular velocity (couter-clockwise is positive).</param>
    /// <param name="dt">Timestep the speed should be applied for (this should be the same as the robot loop duration for most users).</param>
    /// <returns>Discretized ChassisSpeeds.</returns>
    public static ChassisSpeeds Discretize(Speed vx, Speed vy, RotationalSpeed omega, TimeSpan dt)
    {
        Pose2d desiredDeltaPose = new Pose2d(vx * dt, vy * dt, new Rotation2d(omega * dt));
        var twist = new Pose2d().Log(desiredDeltaPose);
        return new(twist.Dx / dt, twist.Dy / dt, twist.Dtheta / dt);
    }

    /// <summary>
    /// Discretizes a  continuous-time chassis speed.
    /// 
    /// <para>
    /// This function converts a continuous-time chassis speed into a discrete-time one such that
    /// when the discrete-time chassis speed is applied for one timestep, the robot moves as if the
    /// velocity components are independent (i.e., the robot moves v_x * dt along the x-axis, v_y * dt
    /// along the y-axis, and omega * dt around the z-axis).
    /// </para>
    /// <para>
    /// This is useful for compensating for translational skew when translating and rotating a
    /// swerve drivetrain.
    /// </para>
    /// </summary>
    /// <param name="continuousSpeeds">ChassisSpeeds to discretize.</param>
    /// <param name="dt">Timestep the speed should be applied for (this should be the same as the robot loop duration for most users).</param>
    /// <returns>Discretized ChassisSpeeds.</returns>
    public static ChassisSpeeds Discretize(ChassisSpeeds continuousSpeeds, TimeSpan dt) =>
        Discretize(continuousSpeeds.Vx, continuousSpeeds.Vy, continuousSpeeds.Omega, dt);

    /// <summary>
    /// Converts a user provided field-relative set of speeds into a robot-relative ChassisSpeeds object.
    /// </summary>
    /// <param name="vx">The component of speed in the x direction relative to the field. Positive x is away 
    /// from your alliance wall.</param>
    /// <param name="vy">The component of speed in the y direction relative to the field. Positive y is to 
    /// your left when standing behind the alliance wall.</param>
    /// <param name="omega">The angular velocity of the robot.</param>
    /// <param name="robotAngle">The angle of the robot as measured by a gyroscope. The robot's angle is 
    /// considered to be zero when it is facing directly away from your alliance station wall.
    /// Remember that this should be CCW positive.</param>
    /// <returns>ChassisSpeeds object representing the speeds in the robot's frame of reference.</returns>
    public static ChassisSpeeds FromFieldRelativeSpeeds(Speed vx, Speed vy, RotationalSpeed omega, Rotation2d robotAngle)
    {
        var oneSecond = TimeSpan.FromSeconds(1);
        // clockwise rotation into robot frame, so negate the angle
        var chassisFrameVelocity = new Translation2d(vx * oneSecond, vy * oneSecond).RotateBy(-robotAngle);
        return new(chassisFrameVelocity.X / oneSecond, chassisFrameVelocity.Y / oneSecond, omega);
    }

    /// <summary>
    /// Converts a user provided field-relative set of speeds into a robot-relative ChassisSpeeds object.
    /// </summary>
    /// <param name="fieldRelativeSpeeds">Field-relative ChassisSpeeds.</param>
    /// <param name="robotAngle">The angle of the robot as measured by a gyroscope. The robot's angle is 
    /// considered to be zero when it is facing directly away from your alliance station wall.
    /// Remember that this should be CCW positive.</param>
    /// <returns>ChassisSpeeds object representing the speeds in the robot's frame of reference.</returns>
    public static ChassisSpeeds FromFieldRelativeSpeeds(ChassisSpeeds fieldRelativeSpeeds, Rotation2d robotAngle) =>
        FromFieldRelativeSpeeds(fieldRelativeSpeeds.Vx, fieldRelativeSpeeds.Vy, fieldRelativeSpeeds.Omega, robotAngle);

    /// <summary>
    /// Converts a user provided robot-relative set of speeds into a field-relative ChassisSpeeds object.
    /// </summary>
    /// <param name="vx">The component of speed in the x direction relative to the robot. Positive x is towards the 
    /// robot's front.</param>
    /// <param name="vy">The component of speed in the y direction relative to the robot. Positive y is towards the 
    /// robot's left.</param>
    /// <param name="omega">The angular velocity of the robot.</param>
    /// <param name="robotAngle">The angle of the robot as measured by a gyroscope. The robot's angle is 
    /// considered to be zero when it is facing directly away from your alliance station wall.
    /// Remember that this should be CCW positive.</param>
    /// <returns>ChassisSpeeds object representing the speeds in the field's frame of reference.</returns>
    public static ChassisSpeeds FromRobotRelativeSpeeds(Speed vx, Speed vy, RotationalSpeed omega, Rotation2d robotAngle)
    {
        var oneSecond = TimeSpan.FromSeconds(1);
        // counter-clockwise rotation out of robot frame
        var chassisFrameVelocity = new Translation2d(vx * oneSecond, vy * oneSecond).RotateBy(robotAngle);
        return new(chassisFrameVelocity.X / oneSecond, chassisFrameVelocity.Y / oneSecond, omega);
    }

    /// <summary>
    /// Converts a user provided robot-relative ChassisSpeeds object into a field-relative ChassisSpeeds object.
    /// </summary>
    /// <param name="robotRelativeSpeeds"> The ChassisSpeeds object representing the speeds in the robot frame 
    /// of reference. Positive x is towards the robot's front. Positive y is towards the robot's left.</param>
    /// <param name="robotAngle">The angle of the robot as measured by a gyroscope. The robot's angle is 
    /// considered to be zero when it is facing directly away from your alliance station wall.
    /// Remember that this should be CCW positive.</param>
    /// <returns>ChassisSpeeds object representing the speeds in the field's frame of reference.</returns>
    public static ChassisSpeeds FromRobotRelativeSpeeds(ChassisSpeeds robotRelativeSpeeds, Rotation2d robotAngle) =>
        FromRobotRelativeSpeeds(robotRelativeSpeeds.Vx, robotRelativeSpeeds.Vy, robotRelativeSpeeds.Omega, robotAngle);

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

    public bool Equals(ChassisSpeeds other)
    {
        return
            Math.Abs(Vx.MetersPerSecond - other.Vx.MetersPerSecond) < 1E-9 &&
            Math.Abs(Vy.MetersPerSecond - other.Vy.MetersPerSecond) < 1E-9 &&
            Math.Abs(Omega.RadiansPerSecond - other.Omega.RadiansPerSecond) < 1E-9;
    }

    public override bool Equals(object? obj)
    {
        return obj is ChassisSpeeds speeds && Equals(speeds);
    }

    public static bool operator ==(ChassisSpeeds left, ChassisSpeeds right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ChassisSpeeds left, ChassisSpeeds right)
    {
        return !(left == right);
    }

    public override int GetHashCode() => HashCode.Combine(Vx, Vy, Omega);
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
