using System;
using System.Collections.Generic;
using HAL_Simulator;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestDigitalOutput : TestBase
    {
        public Dictionary<dynamic, dynamic> GetOutputDictionary(int pin)
        {
            return SimData.HalData["dio"][pin];
        }

        public Dictionary<dynamic, dynamic> GetOutputMXPDictionary(int pin)
        {
            if (pin < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(pin), "Pin is not an MXP pin");
            }
            return SimData.HalData["mxp"][pin - 10];
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
                Assert.IsTrue(GetOutputDictionary(0)["initialized"]);
                Assert.IsFalse(GetOutputDictionary(0)["is_input"]);
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
                Assert.IsTrue(GetOutputDictionary(0)["initialized"]);
                Assert.IsFalse(GetOutputDictionary(0)["is_input"]);
                if (i > 9)
                {
                    Assert.IsTrue(GetOutputMXPDictionary(i)["initialized"]);
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
            Assert.IsTrue(GetOutputDictionary(0)["initialized"]);
            output.Dispose();
            Assert.IsFalse(GetOutputDictionary(0)["initialized"]);
            output = null;
            output = GetDigitalOutput(0);
            Assert.IsTrue(GetOutputDictionary(0)["initialized"]);
            output.Dispose();
        }

        [Test]
        public void TestDigitalOutputSet([Range(0, DigitalChannels - 1)]int channel)
        {
            using (DigitalOutput output = GetDigitalOutput(channel))
            {
                output.Set(false);
                Assert.IsFalse(GetOutputDictionary(channel)["value"]);
                output.Set(true);
                Assert.IsTrue(GetOutputDictionary(channel)["value"]);
            }
        }
    }
}
