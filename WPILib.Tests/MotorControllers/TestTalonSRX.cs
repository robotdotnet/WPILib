using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
// ReSharper disable UnusedVariable

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestTalonSrx : TestBase
    {
        [Test]
        public void TestTalonSrxInitialized()
        {
            using (TalonSRX t = new TalonSRX(2))
                Assert.AreEqual(SimData.PWM[2].Type, ControllerType.TalonSRX);
        }

        [Test]
        public void TestTalonSrxStarts0()
        {
            using (TalonSRX t = new TalonSRX(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [Test]
        public void TestTalonSrxSet()
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
                Assert.AreEqual(SimData.PWM[2].Value, 1);
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
                Assert.AreEqual(SimData.PWM[2].Value, -1);
            }
        }
    }
}
