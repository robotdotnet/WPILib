using System.Collections.Generic;
using HAL_Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPILib.Tests
{
    [TestClass]
    public class TestUtility
    {
        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            TestBase.StartCode();
        }

        [ClassCleanup]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [TestMethod]
        public void TestGetFPGAVersion()
        {
            Assert.AreEqual(2015, Utility.GetFPGAVersion());
        }

        [TestMethod]
        public void TestFPGAGetRevision()
        {
            Assert.AreEqual(0, Utility.GetFPGARevision());
        }


        [TestMethod]
        public void TestGetUserButton()
        {
            HalData()["fpga_button"] = true;
            Assert.IsTrue(Utility.GetUserButton());
        }
    }
}
