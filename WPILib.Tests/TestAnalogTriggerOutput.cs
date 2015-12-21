using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Base;
using HAL.Simulator;
using NUnit.Framework;
using WPILib.Exceptions;
using HAL = HAL.Base.HAL;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestAnalogTriggerOutput : TestBase
    {
        [SetUp]
        public void TestSetup()
        {
            SimData.ResetHALData(false);
        }

        [Test]
        public void TestAnalogTriggerOutputCreation()
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                using (AnalogTriggerOutput output = new AnalogTriggerOutput(trigger, AnalogTriggerType.State))
                {
                    Assert.That(SimData.Reports.ContainsKey((byte)ResourceType.kResourceType_AnalogTriggerOutput));
                    Assert.That(SimData.Reports[(byte)ResourceType.kResourceType_AnalogTriggerOutput].Contains((byte)trigger.Index));
                }
            }
        }

        [Test]
        public void TestAnalogTriggerOutputCreationNullTrigger()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                AnalogTriggerOutput output = new AnalogTriggerOutput(null, AnalogTriggerType.State);
            });
        }

        [Test]
        [TestCase(AnalogTriggerType.InWindow)]
        [TestCase(AnalogTriggerType.FallingPulse)]
        [TestCase(AnalogTriggerType.RisingPulse)]
        [TestCase(AnalogTriggerType.State)]
        public void TestAnalogTriggerOutputRoutings(AnalogTriggerType type)
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                using (AnalogTriggerOutput output = new AnalogTriggerOutput(trigger, type))
                {
                    Assert.That(output.AnalogTriggerForRouting, Is.True);
                    int routingChannel = (trigger.Index << 2) + (int)type;
                    byte routingModule = (byte)(trigger.Index >> 2);

                    Assert.That(output.ChannelForRouting, Is.EqualTo(routingChannel));
                    Assert.That(output.ModuleForRouting, Is.EqualTo(routingModule));
                }
            }
        }

        [Test]
        [TestCase(AnalogTriggerType.InWindow)]
        [TestCase(AnalogTriggerType.FallingPulse)]
        [TestCase(AnalogTriggerType.RisingPulse)]
        [TestCase(AnalogTriggerType.State)]
        public void TestAnalogTriggerOutputRoutingsDigitalSource(AnalogTriggerType type)
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                using (AnalogTriggerOutput trigOut = new AnalogTriggerOutput(trigger, type))
                {
                    DigitalSource output = trigOut;
                    Assert.That(output.AnalogTriggerForRouting, Is.True);
                    int routingChannel = (trigger.Index << 2) + (int)type;
                    byte routingModule = (byte)(trigger.Index >> 2);

                    Assert.That(output.ChannelForRouting, Is.EqualTo(routingChannel));
                    Assert.That(output.ModuleForRouting, Is.EqualTo(routingModule));
                }
            }
        }

        [Test]
        [TestCase(AnalogTriggerType.InWindow)]
        [TestCase(AnalogTriggerType.FallingPulse)]
        [TestCase(AnalogTriggerType.RisingPulse)]
        [TestCase(AnalogTriggerType.State)]
        public void TestAnalogTriggerOutputRoutingsInterruptableBase(AnalogTriggerType type)
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                using (AnalogTriggerOutput trigOut = new AnalogTriggerOutput(trigger, type))
                {
                    InterruptableSensorBase output = trigOut;
                    Assert.That(output.AnalogTriggerForRouting, Is.True);
                    int routingChannel = (trigger.Index << 2) + (int)type;
                    byte routingModule = (byte)(trigger.Index >> 2);

                    Assert.That(output.ChannelForRouting, Is.EqualTo(routingChannel));
                    Assert.That(output.ModuleForRouting, Is.EqualTo(routingModule));
                }
            }
        }

        [Test]
        public void TestAnalogTriggerOutputGetState()
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                using (AnalogTriggerOutput output = new AnalogTriggerOutput(trigger, AnalogTriggerType.State))
                {
                    HALAnalog.GetAnalogTriggerTriggerState = (IntPtr pointer, ref int status) =>
                    {
                        status = 0;
                        return false;
                    };
                    Assert.That(output.Get(), Is.False);
                    HALAnalog.GetAnalogTriggerTriggerState = (IntPtr pointer, ref int status) =>
                    {
                        status = 0;
                        return true;
                    };
                    Assert.That(output.Get(), Is.True);
                    HALAnalog.GetAnalogTriggerTriggerState = global::HAL.SimulatorHAL.HALAnalog.getAnalogTriggerTriggerState;
                }
            }
        }

        [Test]
        public void TestAnalogTriggerOutputGetInWindow()
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                using (AnalogTriggerOutput output = new AnalogTriggerOutput(trigger, AnalogTriggerType.InWindow))
                {
                    HALAnalog.GetAnalogTriggerInWindow = (IntPtr pointer, ref int status) =>
                    {
                        status = 0;
                        return false;
                    };
                    Assert.That(output.Get(), Is.False);
                    HALAnalog.GetAnalogTriggerInWindow = (IntPtr pointer, ref int status) =>
                    {
                        status = 0;
                        return true;
                    };
                    Assert.That(output.Get(), Is.True);
                    HALAnalog.GetAnalogTriggerInWindow = global::HAL.SimulatorHAL.HALAnalog.getAnalogTriggerInWindow;
                }
            }
        }

        [Test]
        public void TestAnalogTriggerOutputGetInvalidType()
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                using (AnalogTriggerOutput output = new AnalogTriggerOutput(trigger, AnalogTriggerType.FallingPulse))
                {
                    Assert.Throws<UncleanStatusException>(() =>
                    {
                        output.Get();
                    });
                }
            }
        }
    }
}
