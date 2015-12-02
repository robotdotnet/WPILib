using HAL.Simulator;
using NUnit.Framework;

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
