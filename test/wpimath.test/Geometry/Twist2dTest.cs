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
    }
}
