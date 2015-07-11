using System.Collections.Generic;
using HAL_Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests.MotorControllers
{
    [TestClass]
    public class TestTalonSRX
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
            return HAL_Base.HAL.halData;
        }

        [TestMethod]
        public void TestTalonSRXInitialized()
        {
            using (TalonSRX t = new TalonSRX(2))
                Assert.AreEqual(HalData()["pwm"][2]["type"], "talonsrx");
        }

        [TestMethod]
        public void TestTalonSRXStarts0()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [TestMethod]
        public void TestTalonSRXSet()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [TestMethod]
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

        [TestMethod]
        public void TestPIDWrite()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [TestMethod]
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
