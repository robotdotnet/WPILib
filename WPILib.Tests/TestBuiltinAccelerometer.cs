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

        private static BuiltInAccelerometer s_instance = null;

        private static BuiltInAccelerometer GetAcc()
        {
            return s_instance ?? new BuiltInAccelerometer(AccelerometerRange.k2G);
        }

        [Test]
        public void TestSetRange()
        {
            GetAcc().AccelerometerRange = AccelerometerRange.k2G;
            Assert.AreEqual(GetData()["range"], (int)AccelerometerRange.k2G);

            GetAcc().AccelerometerRange = AccelerometerRange.k4G;
            Assert.AreEqual(GetData()["range"], (int)AccelerometerRange.k4G);

            GetAcc().AccelerometerRange = AccelerometerRange.k8G;
            Assert.AreEqual(GetData()["range"], (int)AccelerometerRange.k8G);

            GetData()["active"] = false;
        }

        [Test]
        public void Test16Failure()
        {
            try
            {
                var x = new BuiltInAccelerometer(AccelerometerRange.k16G);
                Assert.Fail("16G should throw an exception");
            }
            catch (ArgumentOutOfRangeException)
            {
            }
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


    }
}
