using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestAnalogOutput : TestBase
    {
        public Dictionary<dynamic, dynamic> GetOutputDictionary(int pin)
        {
            return SimData.HalData["analog_out"][pin];
        }

        public AnalogOutput GetAnalogOutput(int pin)
        {
            return new AnalogOutput(pin);
        }

        [SetUp]
        public void TestSetup()
        {
            SimData.ResetHALData(false);
        }

        [Test]
        public void TestOutputCreateUnderLimit()
        {
            Assert.Throws<AllocationException>(() =>
            {
                var input = GetAnalogOutput(-1);
            });
        }

        [Test]
        public void TestOutputCreateOverLimit()
        {
            Assert.Throws<AllocationException>(() =>
            {
                var input = GetAnalogOutput(AnalogOutputChannels);
            });
        }

        [Test]
        public void TestOutputCreate()
        {
            using (AnalogOutput input = GetAnalogOutput(0))
            {
                Assert.IsTrue(GetOutputDictionary(0)["initialized"]);
            }
        }

        [Test]
        public void TestOutputDoubleCreate()
        {
            using (AnalogOutput input = GetAnalogOutput(0))
            {
                Assert.Throws<AllocationException>(() =>
                {
                    var io2 = GetAnalogOutput(0);
                });
            }
        }

        [Test]
        public void TestAnalogOutputCreateAll()
        {
            List<AnalogOutput> inputs = new List<AnalogOutput>();
            for (int i = 0; i < AnalogOutputChannels; i++)
            {
                inputs.Add(GetAnalogOutput(i));
                Assert.IsTrue(GetOutputDictionary(0)["initialized"]);
            }

            foreach (var input in inputs)
            {
                input.Dispose();
            }
        }

        [Test]
        public void TestAnalogOutputDispose()
        {
            AnalogOutput input = GetAnalogOutput(0);
            Assert.IsTrue(GetOutputDictionary(0)["initialized"]);
            input.Dispose();
            input = null;
            input = GetAnalogOutput(0);
            Assert.IsTrue(GetOutputDictionary(0)["initialized"]);
            input.Dispose();
        }

        [Test]
        public void TestAnalogOutputSet([Range(0, AnalogOutputChannels - 1)]int channel)
        {
            using (AnalogOutput output = GetAnalogOutput(channel))
            {
                output.SetVoltage(0.5);
                Assert.AreEqual(0.5, GetOutputDictionary(channel)["voltage"], 0.0001);
                output.SetVoltage(4.25);
                Assert.AreEqual(4.25, GetOutputDictionary(channel)["voltage"], 0.0001);
            }
        }
    }
}
