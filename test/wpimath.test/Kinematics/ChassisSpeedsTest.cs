using UnitsNet;
using UnitsNet.NumberExtensions.NumberToAngle;
using UnitsNet.NumberExtensions.NumberToSpeed;
using WPIMath.Geometry;
using Xunit;

namespace WPIMath.Test.Kinematics;

public class ChassisSpeedsTest
{

    private static readonly double Epsilon = 1e-9;

    [Fact]
    public void TestAddition()
    {
        // Given
        ChassisSpeeds first = new(1.MetersPerSecond(), 2.MetersPerSecond(), RotationalSpeed.FromRadiansPerSecond(3));
        ChassisSpeeds second = new(4.MetersPerSecond(), 5.MetersPerSecond(), RotationalSpeed.FromRadiansPerSecond(6));
        // When
        ChassisSpeeds sum = first + second;
        // Then
        Assert.Equal(5.0, sum.Vx.MetersPerSecond, Epsilon);
        Assert.Equal(7.0, sum.Vy.MetersPerSecond, Epsilon);
        Assert.Equal(9.0, sum.Omega.RadiansPerSecond, Epsilon);
    }

    [Fact]
    public void TestMinus()
    {
        // Given
        ChassisSpeeds first = new(1.MetersPerSecond(), 2.MetersPerSecond(), RotationalSpeed.FromRadiansPerSecond(3));
        ChassisSpeeds second = new(4.MetersPerSecond(), 5.MetersPerSecond(), RotationalSpeed.FromRadiansPerSecond(6));
        // When
        ChassisSpeeds difference = first - second;
        // Then
        Assert.Equal(-3.0, difference.Vx.MetersPerSecond, Epsilon);
        Assert.Equal(-3.0, difference.Vy.MetersPerSecond, Epsilon);
        Assert.Equal(-3.0, difference.Omega.RadiansPerSecond, Epsilon);
    }

    [Fact]
    public void TestMultiply()
    {
        // Given
        ChassisSpeeds speed = new(1.MetersPerSecond(), 2.MetersPerSecond(), RotationalSpeed.FromRadiansPerSecond(3));
        double scalar = 3.0;
        // When
        ChassisSpeeds product = speed * scalar;
        // Then
        Assert.Equal(3.0, product.Vx.MetersPerSecond, Epsilon);
        Assert.Equal(6.0, product.Vy.MetersPerSecond, Epsilon);
        Assert.Equal(9.0, product.Omega.RadiansPerSecond, Epsilon);
    }

    [Fact]
    public void TestDivide()
    {
        // Given
        ChassisSpeeds speed = new(1.MetersPerSecond(), 2.MetersPerSecond(), RotationalSpeed.FromRadiansPerSecond(3));
        double scalar = 5.0;
        // When
        ChassisSpeeds quotient = speed / scalar;
        // Then
        Assert.Equal(0.2, quotient.Vx.MetersPerSecond, Epsilon);
        Assert.Equal(0.4, quotient.Vy.MetersPerSecond, Epsilon);
        Assert.Equal(0.6, quotient.Omega.RadiansPerSecond, Epsilon);
    }

    [Fact]
    public void TestFromFieldRelative()
    {

        ChassisSpeeds fieldSpeeds = new(1.0, 0.0, 0.5);
        ChassisSpeeds robotSpeeds = ChassisSpeeds.FromFieldRelativeSpeeds(fieldSpeeds, new Rotation2d(-90.Degrees()));

        Assert.Equal(0.0, robotSpeeds.Vx.MetersPerSecond, Epsilon);
        Assert.Equal(1.0, robotSpeeds.Vy.MetersPerSecond, Epsilon);
        Assert.Equal(0.5, robotSpeeds.Omega.RadiansPerSecond, Epsilon);
    }

    [Fact]
    public void TestFromRobotRelative()
    {
        ChassisSpeeds robotSpeeds = new(1.0, 0.0, 0.5);
        ChassisSpeeds fieldSpeeds = ChassisSpeeds.FromRobotRelativeSpeeds(robotSpeeds, new Rotation2d(45.Degrees()));

        Assert.Equal(1.0 / Math.Sqrt(2.0), fieldSpeeds.Vx.MetersPerSecond, Epsilon);
        Assert.Equal(1.0 / Math.Sqrt(2.0), fieldSpeeds.Vy.MetersPerSecond, Epsilon);
        Assert.Equal(0.5, fieldSpeeds.Omega.RadiansPerSecond, Epsilon);
    }

    [Fact]
    public void TestDiscretize()
    {
        ChassisSpeeds target = new(1, 0.0, 0.5);
        TimeSpan duration = TimeSpan.FromSeconds(1);
        TimeSpan dt = TimeSpan.FromSeconds(0.01);

        ChassisSpeeds speeds = ChassisSpeeds.Discretize(target, duration);
        Twist2d twist = new(
            speeds.Vx * dt,
            speeds.Vy * dt,
            speeds.Omega * dt
        );

        Pose2d pose = new();
        for (double time = 0; time < duration.TotalSeconds; time += dt.TotalSeconds)
        {
            pose = pose.Exp(twist);
        }

        Assert.Equal((target.Vx * duration).Meters, pose.X.Meters, Epsilon);
        Assert.Equal((target.Vy * duration).Meters, pose.Y.Meters, Epsilon);
        Assert.Equal((target.Omega * duration).Radians, pose.Rotation.Angle.Radians, Epsilon);
    }
}
