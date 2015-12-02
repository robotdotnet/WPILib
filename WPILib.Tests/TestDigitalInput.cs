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
    public class TestDigitalInput : TestBase
    {
        public DIOData GetInputDictionary(int pin)
        {
            return SimData.DIO[pin];
        }

        public MXPData GetInputMXPDictionary(int pin)
        {
            if (pin < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(pin), "AnalogPin is not an MXP pin");
            }
            return SimData.MXP[pin - 10];
        }

        public DigitalInput GetDigitalInput(int pin)
        {
            return new DigitalInput(pin);
        }

        [Test]
        public void TestInputCreateUnderLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetDigitalInput(-1);
            });
        }

        [Test]
        public void TestInputCreateOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetDigitalInput(DigitalChannels);
            });
        }

        [Test]
        public void TestInputCreate()
        {
            using (DigitalInput input = GetDigitalInput(0))
            {
                Assert.IsTrue(GetInputDictionary(0).Initialized);
                Assert.IsTrue(GetInputDictionary(0).IsInput);
            }
        }

        [Test]
        public void TestInputDoubleCreate()
        {
            using (DigitalInput input = GetDigitalInput(0))
            {
                Assert.Throws<AllocationException>(() =>
                {
                    var io2 = GetDigitalInput(0);
                });
            }
        }

        [Test]
        public void TestDigitalInputCreateAll()
        {
            List<DigitalInput> inputs = new List<DigitalInput>();
            for (int i = 0; i < DigitalChannels; i++)
            {
                inputs.Add(GetDigitalInput(i));
                Assert.IsTrue(GetInputDictionary(0).Initialized);
                Assert.IsTrue(GetInputDictionary(0).IsInput);
                if (i > 9)
                {
                    Assert.IsTrue(GetInputMXPDictionary(i).Initialized);
                }
            }

            foreach (var input in inputs)
            {
                input.Dispose();
            }
        }

        [Test]
        public void TestDigitalInputDispose()
        {
            DigitalInput input = GetDigitalInput(0);
            Assert.IsTrue(GetInputDictionary(0).Initialized);
            input.Dispose();
            Assert.IsFalse(GetInputDictionary(0).Initialized);
            input = GetDigitalInput(0);
            Assert.IsTrue(GetInputDictionary(0).Initialized);
            input.Dispose();
        }

        [Test]
        public void TestDigitalInputGet([Range(0, DigitalChannels - 1)]int channel)
        {
            using (DigitalInput input = GetDigitalInput(channel))
            {
                GetInputDictionary(channel).Value = false;
                Assert.IsFalse(input.Get());
                GetInputDictionary(channel).Value = true;
                Assert.IsTrue(input.Get());
            }
        }
    }
}
