using System.Collections.Generic;
using HAL_Base;
using HAL_Simulator;
using NUnit.Framework;

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestTalon : TestBase
    {

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return SimData.halData;
        }

        [Test]
        public void TestTalonInitialized()
        {
            using (Talon t = new Talon(2))
                Assert.AreEqual(HalData()["pwm"][2]["type"], "talon");
        }

        [Test]
        public void TestTalonStarts0()
        {
            using (Talon t = new Talon(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [Test]
        public void TestTalonSet()
        {
            using (Talon t = new Talon(2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [Test]
        public void TestPWMHelpers()
        {
            using (Talon t = new Talon(2))
            {
                t.Set(1);
                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(Talon), HalData()["pwm"][2]["raw_value"]), 1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), 1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], 1);
            }
        }

        [Test]
        public void TestPIDWrite()
        {
            using (Talon t = new Talon(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [Test]
        public void TestPWMHelpersPID()
        {
            using (Talon t = new Talon(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(Talon), HalData()["pwm"][2]["raw_value"]), -1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), -1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], -1);
            }
        }
    }
}
