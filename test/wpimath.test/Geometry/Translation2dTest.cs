using UnitsNet.NumberExtensions.NumberToAngle;
using UnitsNet.NumberExtensions.NumberToLength;
using WPIMath.Geometry;
using Xunit;

namespace WPIMath.Test.Geometry
{
    public class Translation2dTest
    {
        [Fact]
        public void TestTranslation2dRotateBy()
        {
            Translation2d start = new(5.Meters(), 0.Meters());
            Rotation2d rotation = new(90.Degrees());
            var end = start.RotateBy(rotation);

            Assert.Equal(new Translation2d(0.Meters(), 5.Meters()), end);
        }

        [Fact]
        public void TestTranslation2dAddition()
        {
            Translation2d start = new(5.Meters(), 0.Meters());
            Translation2d offset = new(2.Meters(), 3.Meters());
            Translation2d end = new(7.Meters(), 3.Meters());

            Assert.Equal(end, start + offset);
        }
    }
}
