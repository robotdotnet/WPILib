using System.Collections.Generic;
using HAL_Base;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestVictorSP : TestBase
    {

        [Test]
        public void TestVictorSPInitialized()
        {
            using (VictorSP t = new VictorSP(2))
                Assert.AreEqual(SimData.PWM[2].Type, ControllerType.VictorSP);
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
                Assert.AreEqual(SimData.PWM[2].Value, 1);
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
                Assert.AreEqual(SimData.PWM[2].Value, -1);
            }
        }
    }
}
