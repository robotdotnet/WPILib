using System.Collections.Generic;
using HAL_Base;
using HAL_Simulator;
using NUnit.Framework;

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestVictorSP : TestBase
    {

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return SimData.halData;
        }

        [Test]
        public void TestVictorSPInitialized()
        {
            using (VictorSP t = new VictorSP(2))
                Assert.AreEqual(HalData()["pwm"][2]["type"], "victorsp");
        }

        [Test]
        public void TestVictorSPStarts0()
        {
            using (VictorSP t = new VictorSP(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [Test]
        public void TestVictorSPSet()
        {
            using (VictorSP t = new VictorSP(2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [Test]
        public void TestPWMHelpers()
        {
            using (VictorSP t = new VictorSP(2))
            {
                t.Set(1);
                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(VictorSP), HalData()["pwm"][2]["raw_value"]), 1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), 1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], 1);
            }
        }

        [Test]
        public void TestPIDWrite()
        {
            using (VictorSP t = new VictorSP(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [Test]
        public void TestPWMHelpersPID()
        {
            using (VictorSP t = new VictorSP(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(VictorSP), HalData()["pwm"][2]["raw_value"]), -1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), -1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], -1);
            }
        }
    }
}
