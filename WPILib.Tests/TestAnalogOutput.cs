using System.Collections.Generic;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
using WPILib.Exceptions;
// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture]
    public class TestAnalogOutput : TestBase
    {
        public AnalogOutData GetOutputData(int pin)
        {
            return SimData.AnalogOut[pin];
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
                Assert.IsTrue(GetOutputData(0).Initialized);
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
                Assert.IsTrue(GetOutputData(i).Initialized);
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
            Assert.IsTrue(GetOutputData(0).Initialized);
            input.Dispose();
            Assert.IsFalse(GetOutputData(0).Initialized);
            input = GetAnalogOutput(0);
            Assert.IsTrue(GetOutputData(0).Initialized);
            input.Dispose();
        }

        [Test]
        public void TestAnalogOutputSet([Range(0, AnalogOutputChannels - 1)]int channel)
        {
            using (AnalogOutput output = GetAnalogOutput(channel))
            {
                output.SetVoltage(0.5);
                Assert.AreEqual(0.5, GetOutputData(channel).Voltage, 0.0001);
                output.SetVoltage(4.25);
                Assert.AreEqual(4.25, GetOutputData(channel).Voltage, 0.0001);
            }
        }
    }
}
