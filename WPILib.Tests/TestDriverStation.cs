using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestDriverStation
    {
        [TestFixtureSetUp]
        public static void Initialize()
        {
            TestBase.StartCode();
        }

        [TestFixtureTearDown]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [Test]
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

        [Test]
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
