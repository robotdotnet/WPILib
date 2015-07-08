using System.Collections.Generic;
using HAL_Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests.MotorControllers
{
    [TestClass]
    public class TestVictor
    {
        [ClassInitialize]
        public static void Initialize(TestContext c)
        {
            RobotBase.InitializeHardwareConfiguration();
            HAL.Initialize();
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
        public void TestVictorInitialized()
        {
            using (Victor t = new Victor(2))
                Assert.AreEqual(HalData()["pwm"][2]["type"], "victor");
        }

        [TestMethod]
        public void TestVictorStarts0()
        {
            using (Victor t = new Victor(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [TestMethod]
        public void TestVictorSet()
        {
            using (Victor t = new Victor(2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [TestMethod]
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

        [TestMethod]
        public void TestPIDWrite()
        {
            using (Victor t = new Victor(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [TestMethod]
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
