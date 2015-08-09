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

        [Test]
        public void TestCurrent3V3()
        {
            const double testVal = 3.14;
            GetData()["user_current_3v3"] = testVal;
            Assert.AreEqual(ControllerPower.GetCurrent3V3(), testVal, 0.0001);
        }

        [Test]
        public void TestEnabled3V3()
        {
            GetData()["user_active_3v3"] = true;
            Assert.IsTrue(ControllerPower.GetEnabled3V3());
            GetData()["user_active_3v3"] = false;
            Assert.IsFalse(ControllerPower.GetEnabled3V3());
        }

        [Test]
        public void TestFaultCount3V3()
        {
            const int testVal = 3;
            GetData()["user_faults_3v3"] = testVal;
            Assert.AreEqual(ControllerPower.GetFaultCount3V3(), testVal);
        }

        [Test]
        public void TestVoltage5V()
        {
            const double testVal = 3.14;
            GetData()["user_voltage_5v"] = testVal;
            Assert.AreEqual(ControllerPower.GetVoltage5V(), testVal, 0.0001);
        }

        [Test]
        public void TestCurrent5V()
        {
            const double testVal = 3.14;
            GetData()["user_current_5v"] = testVal;
            Assert.AreEqual(ControllerPower.GetCurrent5V(), testVal, 0.0001);
        }

        [Test]
        public void TestEnabled5V()
        {
            GetData()["user_active_5v"] = true;
            Assert.IsTrue(ControllerPower.GetEnabled5V());
            GetData()["user_active_5v"] = false;
            Assert.IsFalse(ControllerPower.GetEnabled5V());
        }

        [Test]
        public void TestFaultCount5V()
        {
            const int testVal = 3;
            GetData()["user_faults_5v"] = testVal;
            Assert.AreEqual(ControllerPower.GetFaultCount5V(), testVal);
        }

        [Test]
        public void TestVoltage6V()
        {
            const double testVal = 3.14;
            GetData()["user_voltage_6v"] = testVal;
            Assert.AreEqual(ControllerPower.GetVoltage6V(), testVal, 0.0001);
        }

        [Test]
        public void TestCurrent6V()
        {
            const double testVal = 3.14;
            GetData()["user_current_6v"] = testVal;
            Assert.AreEqual(ControllerPower.GetCurrent6V(), testVal, 0.0001);
        }

        [Test]
        public void TestEnabled6V()
        {
            GetData()["user_active_6v"] = true;
            Assert.IsTrue(ControllerPower.GetEnabled6V());
            GetData()["user_active_6v"] = false;
            Assert.IsFalse(ControllerPower.GetEnabled6V());
        }

        [Test]
        public void TestFaultCount6V()
        {
            const int testVal = 3;
            GetData()["user_faults_6v"] = testVal;
            Assert.AreEqual(ControllerPower.GetFaultCount6V(), testVal);
        }
    }
}
