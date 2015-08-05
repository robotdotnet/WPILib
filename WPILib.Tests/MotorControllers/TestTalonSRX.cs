using System.Collections.Generic;
using HAL_Base;
using HAL_Simulator;
using NUnit.Framework;

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestTalonSRX : TestBase
    {

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return SimData.halData;
        }

        [Test]
        public void TestTalonSRXInitialized()
        {
            using (TalonSRX t = new TalonSRX(2))
                Assert.AreEqual(HalData()["pwm"][2]["type"], "talonsrx");
        }

        [Test]
        public void TestTalonSRXStarts0()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [Test]
        public void TestTalonSRXSet()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [Test]
        public void TestPWMHelpers()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                t.Set(1);
                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(TalonSRX), HalData()["pwm"][2]["raw_value"]), 1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), 1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], 1);
            }
        }

        [Test]
        public void TestPIDWrite()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [Test]
        public void TestPWMHelpersPID()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(TalonSRX), HalData()["pwm"][2]["raw_value"]), -1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), -1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], -1);
            }
        }
    }
}
