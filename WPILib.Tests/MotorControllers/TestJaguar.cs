using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
// ReSharper disable UnusedVariable

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestJaguar : TestBase
    {
        [Test]
        public void TestJaguarInitialized()
        {
            using (Jaguar t = new Jaguar(2))
                Assert.AreEqual(SimData.PWM[2].Type, ControllerType.Jaguar);
        }

        [Test]
        public void TestJaguarStarts0()
        {
            using (Jaguar t = new Jaguar(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [Test]
        public void TestJaguarSet()
        {
            using (Jaguar t = new Jaguar(2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [Test]
        public void TestPWMHelpers()
        {
            using (Jaguar t = new Jaguar(2))
            {
                t.Set(1);
                Assert.AreEqual(SimData.PWM[2].Value, 1);
            }
        }

        [Test]
        public void TestPIDWrite()
        {
            using (Jaguar t = new Jaguar(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [Test]
        public void TestPWMHelpersPID()
        {
            using (Jaguar t = new Jaguar(2))
            {
                t.PidWrite(-1);
                Assert.AreEqual(SimData.PWM[2].Value, -1);
            }
        }
    }
}
