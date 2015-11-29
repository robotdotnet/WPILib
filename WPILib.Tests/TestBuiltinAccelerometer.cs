using System;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
using WPILib.Interfaces;
// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture]
    public class TestBuiltinAccelerometer
    {
        private AccelerometerData GetData()
        {
            return SimData.Accelerometer;
        }

        private static readonly BuiltInAccelerometer s_instance = null;

        private static BuiltInAccelerometer GetAcc()
        {
            return s_instance ?? new BuiltInAccelerometer(AccelerometerRange.k2G);
        }

        [TestFixtureSetUp]
        public static void Setup()
        {
            GetAcc();
        }

        [Test]
        [TestCase(AccelerometerRange.k2G)]
        [TestCase(AccelerometerRange.k4G)]
        [TestCase(AccelerometerRange.k8G)]
        public void TestSetRange(AccelerometerRange range)
        {
            GetAcc().AccelerometerRange = range;
            Assert.AreEqual(GetData().Range, (HAL_Base.HALAccelerometerRange)range);

            GetData().Active = false;
        }

        [Test]
        public void TestRangeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var x = new BuiltInAccelerometer(AccelerometerRange.k16G);
            });
            Assert.IsFalse(GetData().Active);
        }

        [Test]
        public void TestGetX()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData().X = testVal;
            Assert.AreEqual(GetAcc().GetX(), testVal);
        }

        [Test]
        public void TestGetY()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData().Y = testVal;
            Assert.AreEqual(GetAcc().GetY(), testVal);
        }

        [Test]
        public void TestGetZ()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData().Z = testVal;
            Assert.AreEqual(GetAcc().GetZ(), testVal);
        }

        [Test]
        public void TestGetAll()
        {
            const double x = 5.85;
            const double y = 6.82;
            const double z = 1.923;
            GetData().X = x;
            GetData().Y = y;
            GetData().Z = z;

            var all = GetAcc().GetAllAxes();
            Assert.That(all.XAxis, Is.EqualTo(x).Within(0.01));
            Assert.That(all.YAxis, Is.EqualTo(y).Within(0.01));
            Assert.That(all.ZAxis, Is.EqualTo(z).Within(0.01));
        }

        [Test]
        public void TestBaccGetSmartDashboardType()
        {
            Assert.AreEqual("3AxisAccelerometer", new BuiltInAccelerometer(AccelerometerRange.k2G).SmartDashboardType);
        }
    }
}
