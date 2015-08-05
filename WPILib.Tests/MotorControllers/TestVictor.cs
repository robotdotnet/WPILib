using System.Collections.Generic;
using HAL_Base;
using HAL_Simulator;
using NUnit.Framework;

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestVictor : TestBase
    {

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return SimData.halData;
        }

        [Test]
        public void TestVictorInitialized()
        {
            using (Victor t = new Victor(2))
                Assert.AreEqual(HalData()["pwm"][2]["type"], "victor");
        }

        [Test]
        public void TestVictorStarts0()
        {
            using (Victor t = new Victor(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [Test]
        public void TestVictorSet()
        {
            using (Victor t = new Victor(2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [Test]
        public void TestPWMHelpers()
        {
            using (Victor t = new Victor(2))
            {
                t.Set(1);
                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(Victor), HalData()["pwm"][2]["raw_value"]), 1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), 1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], 1);
            }
        }

        [Test]
        public void TestPIDWrite()
        {
            using (Victor t = new Victor(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [Test]
        public void TestPWMHelpersPID()
        {
            using (Victor t = new Victor(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(Victor), HalData()["pwm"][2]["raw_value"]), -1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), -1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], -1);
            }
        }
    }
}
