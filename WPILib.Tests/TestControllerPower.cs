using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestControllerPower
    {
        private Dictionary<dynamic, dynamic> GetData()
        {
            return HAL.halData["power"];
        }

        [Test]
        public void TestInputVoltage()
        {
            const double testVal = 3.14;
            GetData()["vin_voltage"] = testVal;
            Assert.AreEqual(ControllerPower.GetInputVoltage(), testVal, 0.0001);
        }

        [Test]
        public void TestInputCurrent()
        {
            const double testVal = 3.14;
            GetData()["vin_current"] = testVal;
            Assert.AreEqual(ControllerPower.GetInputCurrrent(), testVal, 0.0001);
        }

        [Test]
        public void TestVoltage3V3()
        {
            const double testVal = 3.14;
            GetData()["user_voltage_3v3"] = testVal;
            Assert.AreEqual(ControllerPower.GetVoltage3V3(), testVal, 0.0001);
        }
    }
}
