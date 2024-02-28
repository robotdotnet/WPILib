using UnitsNet.NumberExtensions.NumberToAngle;
using UnitsNet.NumberExtensions.NumberToLength;
using WPIMath.Geometry;
using Xunit;

namespace WPIMath.Test.Geometry
{
    public class Twist2dTest
    {
        [Fact]
        public void TestPose2dLog()
        {
            Pose2d start = new();
            Pose2d end = new(new Translation2d(5.Meters(), 5.Meters()), new Rotation2d(90.Degrees()));

            Twist2d twist = start.Log(end);

            Twist2d expected = new((5.0 / 2.0 * Math.PI).Meters(), 0.Meters(), (Math.PI / 2.0).Radians());

            Assert.Equal(expected, twist);

            // ensure the twist gives us the real end pose
            Pose2d applied = start.Exp(twist);
            Assert.Equal(end, applied);
        }
        // TODO: Move tests to own classes to match with their relevant geometry types
        [Fact]
        public void TestPose2dTransformBy()
        {
            Pose2d start = new();

            Transform2d transform = new(new Translation2d(5.Meters(), 5.Meters()), new Rotation2d(90.Degrees()));

            Pose2d end = new(new Translation2d(5.Meters(), 5.Meters()), new Rotation2d(90.Degrees()));

            Assert.Equal(end, start.TransformBy(transform));
        }

        [Fact]
        public void TestTranslation2dRotateBy()
        {
            Translation2d start = new(5.Meters(), 0.Meters());
            Rotation2d rotation = new(90.Degrees());
            Translation2d end = start.RotateBy(rotation);

            Assert.Equal(new Translation2d(0.Meters(), 5.Meters()), end);
        }
    }
}
