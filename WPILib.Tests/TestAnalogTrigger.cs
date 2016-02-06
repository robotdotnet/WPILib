using System;
using HAL.Base;
using HAL.Simulator;
using HAL.Simulator.Data;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestAnalogTrigger : TestBase
    {

        private AnalogTriggerData GetData(int index)
        {
            return SimData.AnalogTrigger[index];
        }

        [Test]
        public void TestAnalogTriggerInitFree([Range(0, 7)]int pin)
        {
            int index;
            using (AnalogTrigger trigger = new AnalogTrigger(pin))
            {
                index = trigger.Index;
                Assert.IsTrue(GetData(index).Initialized);
                Assert.AreEqual(pin, GetData(index).AnalogPin);
            }
            Assert.IsFalse(GetData(index).Initialized);
        }

        [Test]
        public void TestAnalogTriggerNullInput()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                AnalogTrigger trigger = new AnalogTrigger(null);
            });
        }

        [Test]
        public void TestAnalogTriggerTakeAnalogInput()
        {
            using (AnalogInput aIn = new AnalogInput(0))
            {
                Assert.That(SimData.AnalogIn[0].Initialized, Is.True);
                int index = 0;
                using (AnalogTrigger trigger = new AnalogTrigger(aIn))
                {
                    index = trigger.Index;
                    Assert.That(GetData(index).Initialized, Is.True);
                }
                Assert.That(GetData(index).Initialized, Is.False);
                Assert.That(SimData.AnalogIn[0].Initialized, Is.True);
            }
            Assert.That(SimData.AnalogIn[0].Initialized, Is.False);
        }

        [Test]
        public void TestSetAveraged()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                at.Filtered = false;
                at.Averaged = true;
                Assert.AreEqual(TrigerType.Averaged, GetData(at.Index).TrigType);
            }
        }

        [Test]
        public void TestSetFiltered()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                at.Averaged = false;
                at.Filtered = true;
                Assert.AreEqual(TrigerType.Filtered, GetData(at.Index).TrigType);
            }
        }

        [Test]
        public void TestSetFilteredThenAverage()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                SimData.DriverStation.ControlData.DsAttached = true;
                SimData.ErrorData = null;
                at.Averaged = true;
                at.Filtered = true;
                Assert.That(SimData.ErrorData, Is.Not.Null);
                SimData.ErrorData = null;
            }
        }

        [Test]
        public void TestSetAveragedThenFiltered()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                SimData.DriverStation.ControlData.DsAttached = true;
                SimData.ErrorData = null;
                at.Filtered = true;
                at.Averaged = true;
                Assert.That(SimData.ErrorData, Is.Not.Null);
                SimData.ErrorData = null;
            }
        }

        [Test]
        public void TestSetLimitsRawValid()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                at.SetLimitsRaw(50, 150);
                Assert.That(GetAnalogVoltageToValue(2, GetData(at.Index).TrigLower), Is.EqualTo(50));
                Assert.That(GetAnalogVoltageToValue(2, GetData(at.Index).TrigUpper), Is.EqualTo(150));
            }
        }

        [Test]
        public void TestSetLimitsRawInvalid()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                Assert.Throws<BoundaryException>(() =>
                {
                    at.SetLimitsRaw(150, 50);
                });

            }
        }

        [Test]
        public void TestSetLimitsVoltageValid()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                at.SetLimitsVoltage(2.2, 4.4);
                Assert.That(GetData(at.Index).TrigLower, Is.EqualTo(2.2));
                Assert.That(GetData(at.Index).TrigUpper, Is.EqualTo(4.4));
            }
        }

        [Test]
        public void TestSetLimitsVoltageInvalid()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                Assert.Throws<BoundaryException>(() =>
                {
                    at.SetLimitsVoltage(4.0, 2.0);
                });

            }
        }

        private int GetAnalogVoltageToValue(int channel, double value)
        {
            long LSBWeight = SimData.AnalogIn[channel].LSBWeight;
            int offset = SimData.AnalogIn[channel].Offset;

            double ret = (value + offset * 1.0e-9) / (LSBWeight * 1.0e-9);
            return (int)ret;
        }

        [Test]
        public void TestAnalogTriggerGetState()
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                HALAnalog.GetAnalogTriggerTriggerState = (AnalogTriggerPortSafeHandle pointer, ref int status) =>
                {
                    status = 0;
                    return false;
                };
                Assert.That(trigger.GetTriggerState(), Is.False);
                HALAnalog.GetAnalogTriggerTriggerState = (AnalogTriggerPortSafeHandle pointer, ref int status) =>
                {
                    status = 0;
                    return true;
                };
                Assert.That(trigger.GetTriggerState(), Is.True);
                HALAnalog.GetAnalogTriggerTriggerState = HAL.SimulatorHAL.HALAnalog.getAnalogTriggerTriggerState;

            }
        }

        [Test]
        public void TestAnalogTriggerGetInWindow()
        {
            using (AnalogTrigger trigger = new AnalogTrigger(0))
            {
                HALAnalog.GetAnalogTriggerInWindow = (AnalogTriggerPortSafeHandle pointer, ref int status) =>
                {
                    status = 0;
                    return false;
                };
                Assert.That(trigger.GetInWindow(), Is.False);
                HALAnalog.GetAnalogTriggerInWindow = (AnalogTriggerPortSafeHandle pointer, ref int status) =>
                {
                    status = 0;
                    return true;
                };
                Assert.That(trigger.GetInWindow(), Is.True);
                HALAnalog.GetAnalogTriggerInWindow = HAL.SimulatorHAL.HALAnalog.getAnalogTriggerInWindow;
            }
        }
    }
}
