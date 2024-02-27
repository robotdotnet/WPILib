using UnitsNet;
using UnitsNet.NumberExtensions.NumberToFrequency;
using UnitsNet.NumberExtensions.NumberToSpeed;
using WPIMath;
using Xunit;

namespace wpimath.test;

public class ChassisSpeedsTest
{
    // test:
    // all operators
    // robot relative
    // field relative
    // serialize
    // deserialize
    // tostring

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
        Assert.Equal(first.Vx.MetersPerSecond + second.Vx.MetersPerSecond, sum.Vx.MetersPerSecond, Epsilon);
        Assert.Equal(first.Vy.MetersPerSecond + second.Vy.MetersPerSecond, sum.Vy.MetersPerSecond, Epsilon);
        Assert.Equal(first.Omega.RadiansPerSecond + second.Omega.RadiansPerSecond, sum.Omega.RadiansPerSecond, Epsilon);
    }

    [Fact]
    public void TestMinus()
    {
        // Given
    
        // When
    
        // Then
    }
}
