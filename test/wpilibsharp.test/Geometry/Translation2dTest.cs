using UnitsNet;
using WPILib.Geometry;
using Xunit;

namespace wpilibsharp.test
{
    public class Translation2dTest
    {
        private void AssertEqualLengths(Length expected, Length actual)
        {
            Assert.Equal(expected.Meters, actual.Meters, 9);
        }

        [Fact]
        public void TestSum()
        {
            var one = new Translation2d(Length.FromMeters(1), Length.FromMeters(3));
            var two = new Translation2d(Length.FromMeters(2), Length.FromMeters(5));

            var sum = one + two;

            AssertEqualLengths(Length.FromMeters(3.0), sum.X);
            AssertEqualLengths(Length.FromMeters(8.0), sum.Y);
        }

        [Fact]
        public void TestDifference()
        {
            var one = new Translation2d(Length.FromMeters(1), Length.FromMeters(3));
            var two = new Translation2d(Length.FromMeters(2), Length.FromMeters(5));

            var diff = one - two;

            AssertEqualLengths(Length.FromMeters(-1.0), diff.X);
            AssertEqualLengths(Length.FromMeters(-2.0), diff.Y);
        }

        [Fact]
        public void TestRotateBy()
        {
            var another = new Translation2d(Length.FromMeters(3), Length.FromMeters(0));
            var rotated = another.RotateBy(new Rotation2d(Angle.FromDegrees(90)));

            AssertEqualLengths(Length.FromMeters(0.0), rotated.X);
            AssertEqualLengths(Length.FromMeters(3.0), rotated.Y);
        }
    }
}
