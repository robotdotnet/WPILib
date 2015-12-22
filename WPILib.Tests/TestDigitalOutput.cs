using System;
using System.Collections.Generic;
using HAL.Simulator;
using HAL.Simulator.Data;
using NUnit.Framework;
using WPILib.Exceptions;
// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture]
    public class TestDigitalOutput : TestBase
    {
        public DIOData GetOutputDictionary(int pin)
        {
            return SimData.DIO[pin];
        }

        public MXPData GetOutputMXPDictionary(int pin)
        {
            if (pin < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(pin), "AnalogPin is not an MXP pin");
            }
            return SimData.MXP[pin - 10];
        } 

        public DigitalOutput GetDigitalOutput(int pin)
        {
            return new DigitalOutput(pin);
        }

        [Test]
        public void TestOutputCreateUnderLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var output = GetDigitalOutput(-1);
            });
        }

        [Test]
        public void TestOutputCreateOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var output = GetDigitalOutput(DigitalChannels);
            });
        }

        [Test]
        public void TestOutputCreate()
        {
            using (DigitalOutput output = GetDigitalOutput(0))
            {
                Assert.IsTrue(GetOutputDictionary(0).Initialized);
                Assert.IsFalse(GetOutputDictionary(0).IsInput);
            }
        }

        [Test]
        public void TestOutputDoubleCreate()
        {
            using (DigitalOutput output = GetDigitalOutput(0))
            {
                Assert.Throws<AllocationException>(() =>
                {
                    var io2 = GetDigitalOutput(0);
                });
            }
        }

        [Test]
        public void TestDigitalOutputCreateAll()
        {
            List<DigitalOutput> outputs = new List<DigitalOutput>();
            for (int i = 0; i < DigitalChannels; i++)
            {
                outputs.Add(GetDigitalOutput(i));
                Assert.IsTrue(GetOutputDictionary(0).Initialized);
                Assert.IsFalse(GetOutputDictionary(0).IsInput);
                if (i > 9)
                {
                    Assert.IsTrue(GetOutputMXPDictionary(i).Initialized);
                }
            }

            foreach (var output in outputs)
            {
                output.Dispose();
            }
        }

        [Test]
        public void TestDigitalOutputDispose()
        {
            DigitalOutput output = GetDigitalOutput(0);
            Assert.IsTrue(GetOutputDictionary(0).Initialized);
            output.Dispose();
            Assert.IsFalse(GetOutputDictionary(0).Initialized);
            output = GetDigitalOutput(0);
            Assert.IsTrue(GetOutputDictionary(0).Initialized);
            output.Dispose();
        }

        [Test]
        public void TestDigitalOutputSet([Range(0, DigitalChannels - 1)]int channel)
        {
            using (DigitalOutput output = GetDigitalOutput(channel))
            {
                output.Set(false);
                Assert.IsFalse(GetOutputDictionary(channel).Value);
                output.Set(true);
                Assert.IsTrue(GetOutputDictionary(channel).Value);
            }
        }

        [Test]
        public void TestGetChannel()
        {
            using (DigitalOutput output = GetDigitalOutput(0))
            {
                Assert.That(output.Channel, Is.EqualTo(0));
            }
        }

        [Test]
        public void TestPulseFloat()
        {
            using (DigitalOutput output = GetDigitalOutput(0))
            {
                output.Pulse(50.0f);
                Assert.That(GetOutputDictionary(0).PulseLength, Is.EqualTo(50.0).Within(0.001));
            }
        }

        [Test]
        public void TestPulseInt()
        {
            using (DigitalOutput output = GetDigitalOutput(0))
            {
                const int pulseLength = 1285;
                int status = 0;
                var loopTiming = HAL.Base.HALDigital.GetLoopTiming(ref status);
                float setVal = (float)(pulseLength / 1.0e9 * (loopTiming * 25));
                output.Pulse(pulseLength);
                Assert.That(GetOutputDictionary(0).PulseLength, Is.EqualTo(setVal).Within(0.001));
            }
        }

        [Test]
        public void TestIsPulsing()
        {
            using (DigitalOutput output = GetDigitalOutput(0))
            {
                Assert.That(output.Pulsing, Is.False);

                output.Pulse(50.0f);

                Assert.That(output.Pulsing, Is.True);

                output.Pulse(0.0f);

                Assert.That(output.Pulsing, Is.False);
            }
        }

        [Test]
        public void TestSetPWMRate()
        {
            using (DigitalOutput output = GetDigitalOutput(0))
            {
                double originalRate = SimData.GlobalData.DigitalPWMRate;

                output.PWMRate = 100.0;
                Assert.That(SimData.GlobalData.DigitalPWMRate, Is.EqualTo(100.0).Within(0.001));

                output.PWMRate = originalRate;
                Assert.That(SimData.GlobalData.DigitalPWMRate, Is.EqualTo(originalRate).Within(0.001));
            }
        }

        [Test]
        public void TestDigitalPWMAllocateAll()
        {
            List<DigitalOutput> counters = new List<DigitalOutput>();
            Assert.DoesNotThrow(() =>
            {
                for (int i = 0; i < 6; i++)
                {
                    DigitalOutput output = new DigitalOutput(i);
                    counters.Add(output);
                    output.EnablePWM(0.5);
                }
            });
            foreach (var counter in counters)
            {
                counter?.Dispose();
            }
        }
        /*
        [Test]
        public void TestDigitalPWMOverAllocate()
        {
            List<DigitalOutput> counters = new List<DigitalOutput>();
            for (int i = 0; i < 6; i++)
            {
                DigitalOutput output = new DigitalOutput(i);
                counters.Add(output);
                output.EnablePWM(0.5);
            }

            DigitalOutput counter = new DigitalOutput(NumCounters); ;
            Assert.Throws<UncleanStatusException>(() =>
            {
               
                counter.EnablePWM(0.5);
            });
            counter?.Dispose();

            foreach (var c in counters)
            {
                c?.Dispose();
            }

        }
        */
        [Test]
        public void TestEnablePWMSingle()
        {
            using (DigitalOutput output = GetDigitalOutput(0))
            {
                //Assert.That();
            }
        }
    }
}
