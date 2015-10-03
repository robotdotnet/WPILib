using System.Collections.Generic;
using HAL_Base;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestVictor : TestBase
    {

        [Test]
        public void TestVictorInitialized()
        {
            using (Victor t = new Victor(2))
                Assert.AreEqual(SimData.PWM[2].Type, ControllerType.Victor);
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
                Assert.AreEqual(SimData.PWM[2].Value, 1);
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
                Assert.AreEqual(SimData.PWM[2].Value, -1);
            }
        }
    }
}
