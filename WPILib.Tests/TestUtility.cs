using System.Collections.Generic;
using HAL_Simulator;
using NUnit.Framework;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestUtility : TestBase
    {

        [Test]
        public void TestGetFPGAVersion()
        {
            Assert.AreEqual(2015, Utility.GetFPGAVersion());
        }

        [Test]
        public void TestFPGAGetRevision()
        {
            Assert.AreEqual(0, Utility.GetFPGARevision());
        }


        [Test]
        public void TestGetUserButton()
        {
            SimData.RoboRioData.FPGAButton = true;
            Assert.IsTrue(Utility.GetUserButton());
        }
    }
}
