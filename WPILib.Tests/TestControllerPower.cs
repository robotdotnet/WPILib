using System.Collections.Generic;
using HAL_Base;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestControllerPower : TestBase
    {
        private RoboRioData GetData()
        {
            return SimData.RoboRioData;
        }

        [Test]
        public void TestInputVoltage()
        {
            const float testVal = 3.14f;
            GetData().VInVoltage = testVal;
            Assert.AreEqual(ControllerPower.GetInputVoltage(), testVal, 0.0001);
        }

        [Test]
        public void TestInputCurrent()
        {
            const float testVal = 3.14f;
            GetData().VInCurrent = testVal;
            Assert.AreEqual(ControllerPower.GetInputCurrrent(), testVal, 0.0001);
        }

        [Test]
        public void TestVoltage3V3()
        {
            const float testVal = 3.14f;
            GetData().UserVoltage3V3 = testVal;
            Assert.AreEqual(ControllerPower.GetVoltage3V3(), testVal, 0.0001);
        }

        [Test]
        public void TestCurrent3V3()
        {
            const float testVal = 3.14f;
            GetData().UserCurrent3V3 = testVal;
            Assert.AreEqual(ControllerPower.GetCurrent3V3(), testVal, 0.0001);
        }

        [Test]
        public void TestEnabled3V3()
        {
            GetData().UserActive3V3 = true;
            Assert.IsTrue(ControllerPower.GetEnabled3V3());
            GetData().UserActive3V3 = false;
            Assert.IsFalse(ControllerPower.GetEnabled3V3());
        }

        [Test]
        public void TestFaultCount3V3()
        {
            const int testVal = 3;
            GetData().UserFaults3V3 = testVal;
            Assert.AreEqual(ControllerPower.GetFaultCount3V3(), testVal);
        }

        [Test]
        public void TestVoltage5V()
        {
            const float testVal = 3.14f;
            GetData().UserVoltage5V = testVal;
            Assert.AreEqual(ControllerPower.GetVoltage5V(), testVal, 0.0001);
        }

        [Test]
        public void TestCurrent5V()
        {
            const float testVal = 3.14f;
            GetData().UserCurrent5V = testVal;
            Assert.AreEqual(ControllerPower.GetCurrent5V(), testVal, 0.0001);
        }

        [Test]
        public void TestEnabled5V()
        {
            GetData().UserActive5V = true;
            Assert.IsTrue(ControllerPower.GetEnabled5V());
            GetData().UserActive5V = false;
            Assert.IsFalse(ControllerPower.GetEnabled5V());
        }

        [Test]
        public void TestFaultCount5V()
        {
            const int testVal = 3;
            GetData().UserFaults5V = testVal;
            Assert.AreEqual(ControllerPower.GetFaultCount5V(), testVal);
        }

        [Test]
        public void TestVoltage6V()
        {
            const float testVal = 3.14f;
            GetData().UserVoltage6V = testVal;
            Assert.AreEqual(ControllerPower.GetVoltage6V(), testVal, 0.0001);
        }

        [Test]
        public void TestCurrent6V()
        {
            const float testVal = 3.14f;
            GetData().UserCurrent6V = testVal;
            Assert.AreEqual(ControllerPower.GetCurrent6V(), testVal, 0.0001);
        }

        [Test]
        public void TestEnabled6V()
        {
            GetData().UserActive6V = true;
            Assert.IsTrue(ControllerPower.GetEnabled6V());
            GetData().UserActive6V = false;
            Assert.IsFalse(ControllerPower.GetEnabled6V());
        }

        [Test]
        public void TestFaultCount6V()
        {
            const int testVal = 3;
            GetData().UserFaults6V = testVal;
            Assert.AreEqual(ControllerPower.GetFaultCount6V(), testVal);
        }
    }
}
