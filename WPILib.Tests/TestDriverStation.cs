using System.Collections.Generic;
using HAL_Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPILib.Tests
{
    [TestClass]
    public class TestDriverStation
    {
        [ClassInitialize]
        public static void Initialize(TestContext c)
        {
            TestBase.StartCode();
        }

        [ClassCleanup]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [TestMethod]
        public void TestControlWordTrue()
        {
            HalData()["control"]["enabled"] = true;
            HalData()["control"]["autonomous"] = true;
            HalData()["control"]["test"] = true;
            HalData()["control"]["eStop"] = true;
            HalData()["control"]["ds_attached"] = true;
            HalData()["control"]["fms_attached"] = true;

            var ct = HAL.GetControlWord();

            Assert.IsTrue(ct.GetEnabled());
            Assert.IsTrue(ct.GetAutonomous());
            Assert.IsTrue(ct.GetTest());
            Assert.IsTrue(ct.GetEStop());
            Assert.IsTrue(ct.GetDSAttached());
            Assert.IsTrue(ct.GetFMSAttached());
        }

        [TestMethod]
        public void TestControlWordFalse()
        {
            HalData()["control"]["enabled"] = false;
            HalData()["control"]["autonomous"] = false;
            HalData()["control"]["test"] = false;
            HalData()["control"]["eStop"] = false;
            HalData()["control"]["ds_attached"] = false;
            HalData()["control"]["fms_attached"] = false;

            var ct = HAL.GetControlWord();

            Assert.IsFalse(ct.GetEnabled());
            Assert.IsFalse(ct.GetAutonomous());
            Assert.IsFalse(ct.GetTest());
            Assert.IsFalse(ct.GetEStop());
            Assert.IsFalse(ct.GetDSAttached());
            Assert.IsFalse(ct.GetFMSAttached());
        }


        public void TestGetMatchTime()
        {
            
        }
    }
}
