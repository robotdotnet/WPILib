using System;
using System.Collections.Generic;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
using WPILib.Exceptions;
// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture]
    public class TestAnalogInput : TestBase
    {
        public AnalogInData GetInputData(int pin)
        {
            return SimData.AnalogIn[pin];
        }

        public AnalogInput GetAnalogInput(int pin)
        {
            return new AnalogInput(pin);
        }

        [SetUp]
        public void TestSetup()
        {
            SimData.ResetHALData(false);
        }

        [Test]
        public void TestInputCreateUnderLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetAnalogInput(-1);
            });
        }

        [Test]
        public void TestInputCreateOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetAnalogInput(AnalogInputChannels);
            });
        }

        [Test]
        public void TestInputCreate()
        {
            using (AnalogInput input = GetAnalogInput(0))
            {
                Assert.IsTrue(GetInputData(0).Initialized);
            }
        }

        [Test]
        public void TestInputDoubleCreate()
        {
            using (AnalogInput input = GetAnalogInput(0))
            {
                Assert.Throws<AllocationException>(() =>
                {
                    var io2 = GetAnalogInput(0);
                });
            }
        }

        [Test]
        public void TestAnalogInputCreateAll()
        {
            List<AnalogInput> inputs = new List<AnalogInput>();
            for (int i = 0; i < AnalogInputChannels; i++)
            {
                inputs.Add(GetAnalogInput(i));
                Assert.IsTrue(GetInputData(i).Initialized);
            }

            foreach (var input in inputs)
            {
                input.Dispose();
            }
        }

        [Test]
        public void TestAnalogInputDispose()
        {
            AnalogInput input = GetAnalogInput(0);
            Assert.IsTrue(GetInputData(0).Initialized);
            input.Dispose();
            Assert.IsFalse(GetInputData(0).Initialized);
            SimData.ResetHALData(false);
            input = GetAnalogInput(0);
            Assert.IsTrue(GetInputData(0).Initialized);
            input.Dispose();
        }

        [Test]
        public void TestAnalogInputGet([Range(0, AnalogInputChannels - 1)]int channel)
        {
            using (AnalogInput input = GetAnalogInput(channel))
            {
                GetInputData(channel).Voltage = (channel + 1.0) * 0.1;
                Assert.AreEqual((channel + 1.0) * 0.1, input.GetVoltage(), 0.0001);
            }
        }
    }
}
