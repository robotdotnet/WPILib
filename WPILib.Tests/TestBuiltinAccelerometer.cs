using System;
using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;
using AccelerometerRange = WPILib.Interfaces.AccelerometerRange;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestBuiltinAccelerometer
    {
        private Dictionary<dynamic, dynamic> GetData()
        {
            return HAL.halData["accelerometer"];
        }

        private static readonly BuiltInAccelerometer s_instance = null;

        private static BuiltInAccelerometer GetAcc()
        {
            return s_instance ?? new BuiltInAccelerometer(AccelerometerRange.k2G);
        }

        [Test]
        [TestCase(AccelerometerRange.k2G)]
        [TestCase(AccelerometerRange.k4G)]
        [TestCase(AccelerometerRange.k8G)]
        public void TestSetRange(AccelerometerRange range)
        {
            GetAcc().AccelerometerRange = range;
            Assert.AreEqual(GetData()["range"], (int)range);

            GetData()["active"] = false;
        }

        [Test]
        public void TestRangeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var x = new BuiltInAccelerometer(AccelerometerRange.k16G);
            });
            Assert.IsFalse(GetData()["active"]);
        }

        [Test]
        public void TestGetX()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData()["x"] = testVal;
            Assert.AreEqual(GetAcc().GetX(), testVal);
        }

        [Test]
        public void TestGetY()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData()["y"] = testVal;
            Assert.AreEqual(GetAcc().GetY(), testVal);
        }

        [Test]
        public void TestGetZ()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData()["z"] = testVal;
            Assert.AreEqual(GetAcc().GetZ(), testVal);
        }

        [Test]
        public void TestBaccGetSmartDashboardType()
        {
            Assert.AreEqual("3AxisAccelerometer", new BuiltInAccelerometer(AccelerometerRange.k2G).SmartDashboardType);
        }
    }
}
