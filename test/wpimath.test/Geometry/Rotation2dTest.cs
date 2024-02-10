using System.Text.Json;
using UnitsNet.NumberExtensions.NumberToAngle;
using WPIMath.Geometry;
using Xunit;

namespace WPIMath.Test.Geometry;

public class Rotation2dTest
{
    private static readonly double Epsilon = 1e-9;

    [Fact]
    public void TestNewWithMeasures()
    {
        Rotation2d rot = 45.Degrees();

        Assert.Equal(Math.PI / 4, rot.Angle.Radians, Epsilon);
    }

    [Fact]
    public void TestRadiansToDegrees()
    {
        Rotation2d rot1 = (Math.PI / 3).Radians();
        Rotation2d rot2 = (Math.PI / 4).Radians();

        Assert.Equal(60.0, rot1.Angle.Degrees, Epsilon);
        Assert.Equal(45.0, rot2.Angle.Degrees, Epsilon);
    }

    [Fact]
    public void TestRadiansAndDegrees()
    {
        Rotation2d rot1 = 45.Degrees();
        Rotation2d rot2 = 30.Degrees();

        Assert.Equal(Math.PI / 4, rot1.Angle.Radians, Epsilon);
        Assert.Equal(Math.PI / 6, rot2.Angle.Radians, Epsilon);
    }

    [Fact]
    public void TestRotationByFromZero()
    {
        var zero = Rotation2d.AdditiveIdentity;
        var rotated = zero.RotateBy(90.Degrees());

        Assert.Equal(Math.PI / 2, rotated.Angle.Radians, Epsilon);
        Assert.Equal(90.0, rotated.Angle.Degrees, Epsilon);
    }

    [Fact]
    public void TestRotationByNonZero()
    {
        Rotation2d rot = 90.Degrees();
        rot += 30.Degrees();

        Assert.Equal(2 * Math.PI / 3, rot.Angle.Radians, Epsilon);
        Assert.Equal(120, rot.Angle.Degrees, Epsilon);
    }

    [Fact]
    public void TestMinus()
    {
        Rotation2d rot1 = 70.Degrees();
        Rotation2d rot2 = 30.Degrees();

        Assert.Equal(40.0, (rot1 - rot2).Angle.Degrees, Epsilon);
    }

    [Fact]
    public void TestUrnaryMinus()
    {
        Rotation2d rot1 = 20.Degrees();

        Assert.Equal(-20.0, (-rot1).Angle.Degrees, Epsilon);
    }

    [Fact]
    public void TestMultiply()
    {
        Rotation2d rot1 = 10.Degrees();

        Assert.Equal(30.0, (rot1 * 3.0).Angle.Degrees, Epsilon);
        Assert.Equal(410.0, (rot1 * 41.0).Angle.Degrees, Epsilon);
    }


    [Fact]
    public void TestEquality()
    {
        Rotation2d rot1 = 43.Degrees();
        Rotation2d rot2 = 43.Degrees();

        Assert.Equal(rot1, rot2);
        Assert.True(rot1 == rot2);
        Assert.False(rot1 != rot2);
        Assert.True(rot1.Equals(rot2));

        rot1 = -180.Degrees();
        rot2 = 180.Degrees();

        Assert.Equal(rot1, rot2);
        Assert.True(rot1 == rot2);
        Assert.True(rot1.Equals(rot2));
    }

    [Fact]
    public void TestInequality()
    {
        Rotation2d rot1 = 43.Degrees();
        Rotation2d rot2 = 43.5.Degrees();

        Assert.NotEqual(rot1, rot2);
        Assert.True(rot1 != rot2);
        Assert.False(rot1 == rot2);
        Assert.False(rot1.Equals(rot2));
    }

    [Fact]
    public void TestInterpolate()
    {
        // 50 + (70 - 50) * 0.5 = 60
        Rotation2d rot1 = 50.Degrees();
        Rotation2d rot2 = 70.Degrees();
        var interpolated = MathExtras.Lerp(rot1, rot2, 0.5);
        Assert.Equal(60.0, interpolated.Angle.Degrees, Epsilon);

        // -160 minus half distance between 170 and -160 (15) = -175
        rot1 = 170.Degrees();
        rot2 = -160.Degrees();
        interpolated = MathExtras.Lerp(rot1, rot2, 0.5);
        Assert.Equal(-175.0, interpolated.Angle.Degrees, Epsilon);
    }

    [Fact]
    public void TestDeserialization()
    {

        Rotation2d r = JsonSerializer.Deserialize<Rotation2d>("{\"radians\":5}");
        Assert.Equal(5, r.Angle.Radians, Epsilon);
    }

    [Fact]
    public void TestSerialization()
    {
        Rotation2d rot = 5.Radians();

        var serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
        };
        string serialized = JsonSerializer.Serialize(rot, serializerOptions);
        Assert.Equal("{\"radians\":5}", serialized);
    }

    [Fact]
    public void TestDeserializationSourceGenerated()
    {
        Rotation2d r = JsonSerializer.Deserialize("{\"radians\":5}", Rotation2dJsonContext.Default.Rotation2d);
        Assert.Equal(5, r.Angle.Radians, Epsilon);
    }

    [Fact]
    public void TestSerializationSourceGenerated()
    {
        Rotation2d rot = 5.Radians();
        string serialized = JsonSerializer.Serialize(rot, Rotation2dJsonContext.Default.Rotation2d);
        Assert.Equal("{\"radians\":5}", serialized);
    }
}
