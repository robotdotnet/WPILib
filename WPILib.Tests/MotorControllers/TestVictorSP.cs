using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
// ReSharper disable UnusedVariable

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestVictorSp : TestBase
    {

        [Test]
        public void TestVictorSpInitialized()
        {
            using (VictorSP t = new VictorSP(2))
                Assert.AreEqual(SimData.PWM[2].Type, ControllerType.VictorSP);
        }

        [Test]
        public void TestVictorSpStarts0()
        {
            using (VictorSP t = new VictorSP(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [Test]
        public void TestVictorSpSet()
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
