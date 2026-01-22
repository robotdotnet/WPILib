using UnitsNet.NumberExtensions.NumberToAngle;
using UnitsNet.NumberExtensions.NumberToLength;
using WPIMath.Geometry;
using Xunit;

namespace WPIMath.Test.Geometry
{
    public class Pose2dTest
    {
        [Fact]
        public void TestPose2dTransformBy()
        {
            Pose2d start = new();

            Transform2d transform = new(new Translation2d(5.Meters(), 5.Meters()), new Rotation2d(90.Degrees()));

            Pose2d end = new(new Translation2d(5.Meters(), 5.Meters()), new Rotation2d(90.Degrees()));

            Assert.Equal(end, start.TransformBy(transform));
        }
    }
}
