using System;
using UnitsNet;
using WPILib.Geometry;
using Xunit;

namespace wpilibsharp.test
{
    public class Rotation2dTest
    {
        private void AssertEqualAngles(Angle expected, Angle actual)
        {
            Assert.Equal(expected.Degrees, actual.Degrees, 9);
        }

        [Fact]
        public void TestRadiansToDegrees()
        {
            var one = new Rotation2d(Angle.FromRadians(Math.PI) / 3);
            var two = new Rotation2d(Angle.FromRadians(Math.PI) / 4);

            AssertEqualAngles(Angle.FromDegrees(60), one.Angle);
            AssertEqualAngles(Angle.FromDegrees(45), two.Angle);
        }

        [Fact]
        public void TestDegreesToRadians()
        {
            var one = new Rotation2d(Angle.FromDegrees(45));
            var two = new Rotation2d(Angle.FromDegrees(30));

            AssertEqualAngles(Angle.FromDegrees(45), one.Angle);
            AssertEqualAngles(Angle.FromDegrees(30), two.Angle);
        }

        [Fact]
        public void TestRotateByFromZero()
        {
            var zero = new Rotation2d(Angle.FromDegrees(0));
            var sum = zero + new Rotation2d(Angle.FromDegrees(90));

            AssertEqualAngles(Angle.FromDegrees(90), sum.Angle);
        }

        [Fact]
        public void TestRotateByNonZero()
        {
            var rot = new Rotation2d(Angle.FromDegrees(90));
            rot += new Rotation2d(Angle.FromDegrees(30));

            AssertEqualAngles(Angle.FromDegrees(120), rot.Angle);
        }

        [Fact]
        public void TestMinus()
        {
            var one = new Rotation2d(Angle.FromDegrees(70));
            var two = new Rotation2d(Angle.FromDegrees(30));

            AssertEqualAngles(Angle.FromDegrees(40), (one - two).Angle);
        }

        [Fact]
        public void TestEquality()
        {
            var one = new Rotation2d(Angle.FromDegrees(42));
            var two = new Rotation2d(Angle.FromDegrees(42));

            Assert.True(one.Equals(two));
        }

        [Fact]
        public void TestEqualityOperator()
        {
            var one = new Rotation2d(Angle.FromDegrees(42));
            var two = new Rotation2d(Angle.FromDegrees(42));

            Assert.True(one == two);
        }

        [Fact]
        public void TestInequality()
        {
            var one = new Rotation2d(Angle.FromDegrees(42));
            var two = new Rotation2d(Angle.FromDegrees(42.5));

            Assert.False(one.Equals(two));
        }

        [Fact]
        public void TestInequalityOperator()
        {
            var one = new Rotation2d(Angle.FromDegrees(42));
            var two = new Rotation2d(Angle.FromDegrees(42.5));

            Assert.False(one == two);
        }
    }
}

