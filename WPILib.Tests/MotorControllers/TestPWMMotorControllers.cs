using System;
using HAL.Simulator;
using HAL.Simulator.Data;
using NUnit.Framework;
// ReSharper disable UnusedVariable

namespace WPILib.Tests.MotorControllers
{
    [TestFixture(typeof(Jaguar))]
    [TestFixture(typeof(Talon))]
    [TestFixture(typeof(TalonSRX))]
    [TestFixture(typeof(Victor))]
    [TestFixture(typeof(VictorSP))]
    [TestFixture(typeof(Spark))]
    [TestFixture(typeof(SD540))]
    public class TestPWMMotorControllers : TestBase
    {
        private readonly Type m_type;

        public TestPWMMotorControllers(Type type)
        {
            m_type = type;
        }

        [Test]
        public void TestPWMSpeedControllerInitialized()
        {
            using (PWMSpeedController t = (PWMSpeedController)Activator.CreateInstance(m_type, 2))
            {
                ControllerType controllerType = ControllerType.None;
                if (m_type == typeof(Jaguar))
                {
                    controllerType = ControllerType.Jaguar;
                }
                else if (m_type == typeof(Talon))
                {
                    controllerType = ControllerType.Talon;
                }
                else if (m_type == typeof(TalonSRX))
                {
                    controllerType = ControllerType.TalonSRX;
                }
                else if (m_type == typeof(VictorSP))
                {
                    controllerType = ControllerType.VictorSP;
                }
                else if (m_type == typeof(Victor))
                {
                    controllerType = ControllerType.Victor;
                }
                else if (m_type == typeof(Spark))
                {
                    controllerType = ControllerType.Spark;
                }
                else if (m_type == typeof(SD540))
                {
                    controllerType = ControllerType.SD540;
                }
                Assert.AreEqual(SimData.PWM[2].Type, controllerType);
            }
        }

        [Test]
        public void TestPWMSpeedControllerStarts0()
        {
            using (PWMSpeedController t = (PWMSpeedController)Activator.CreateInstance(m_type, 2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [Test]
        public void TestPWMSpeedControllerSet()
        {
            using (PWMSpeedController t = (PWMSpeedController)Activator.CreateInstance(m_type, 2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [Test]
        public void TestPWMHelpers()
        {
            using (PWMSpeedController t = (PWMSpeedController)Activator.CreateInstance(m_type, 2))
            {
                t.Set(1);
                Assert.AreEqual(SimData.PWM[2].Value, 1);
            }
        }

        [Test]
        public void TestPIDWrite()
        {
            using (PWMSpeedController t = (PWMSpeedController)Activator.CreateInstance(m_type, 2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [Test]
        public void TestPWMHelpersPID()
        {
            using (PWMSpeedController t = (PWMSpeedController)Activator.CreateInstance(m_type, 2))
            {
                t.PidWrite(-1);
                Assert.AreEqual(SimData.PWM[2].Value, -1);
            }
        }



    }
}
