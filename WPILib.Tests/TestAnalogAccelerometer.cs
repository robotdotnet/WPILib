using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    public class TestAnalogAccelerometer : TestBase
    {
        public AnalogInData GetInputData(int pin)
        {
            return SimData.AnalogIn[pin];
        }

        public AnalogAccelerometer GetAnalogAccelerometer(int pin)
        {
            return new AnalogAccelerometer(pin);
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
                var input = GetAnalogAccelerometer(-1);
            });
        }

        [Test]
        public void TestInputCreateOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetAnalogAccelerometer(AnalogInputChannels);
            });
        }

        [Test]
        public void TestInputCreate()
        {
            using (AnalogAccelerometer input = GetAnalogAccelerometer(0))
            {
                Assert.IsTrue(GetInputData(0).Initialized);
            }
        }

        [Test]
        public void TestInputDoubleCreate()
        {
            using (AnalogAccelerometer input = GetAnalogAccelerometer(0))
            {
                Assert.Throws<AllocationException>(() =>
                {
                    var io2 = GetAnalogAccelerometer(0);
                });
            }
        }

        [Test]
        public void TestAnalogAccelerometerCreateAll()
        {
            List<AnalogAccelerometer> inputs = new List<AnalogAccelerometer>();
            for (int i = 0; i < AnalogInputChannels; i++)
            {
                inputs.Add(GetAnalogAccelerometer(i));
                Assert.IsTrue(GetInputData(i).Initialized);
            }

            foreach (var input in inputs)
            {
                input.Dispose();
            }
        }

        [Test]
        public void TestCreationPreCreatedInput()
        {
            using (AnalogInput input = new AnalogInput(0))
            {
                Assert.IsTrue(SimData.AnalogIn[0].Initialized);
                using (AnalogAccelerometer accel = new AnalogAccelerometer(input))
                {
                }
                Assert.IsTrue(SimData.AnalogIn[0].Initialized);
            }
            Thread.Sleep(100);
            Assert.IsFalse(SimData.AnalogIn[0].Initialized);
        }

        [Test]
        public void TestAnalogAccelerometerDispose()
        {
            AnalogAccelerometer input = GetAnalogAccelerometer(0);
            Assert.IsTrue(GetInputData(0).Initialized);
            input.Dispose();
            Assert.IsFalse(GetInputData(0).Initialized);
            SimData.ResetHALData(false);
            input = GetAnalogAccelerometer(0);
            Assert.IsTrue(GetInputData(0).Initialized);
            input.Dispose();
        }
        
        [Test]
        public void TestAnalogInputGetDefaultSensitivity([Range(0, AnalogInputChannels - 1)]int channel)
        {
            using (AnalogAccelerometer input = GetAnalogAccelerometer(channel))
            {
                double voltage = (channel + 1.0) * 0.1;
                GetInputData(channel).Voltage = voltage;
                double zero = input.Zero;
                double sensitivity = input.Sensitivity;
                double acceleration = (voltage - zero) / sensitivity;
                Assert.That(input.GetAcceleration(), Is.EqualTo(acceleration).Within(0.001));
            }
        }

        [Test]
        public void TestAnalogInputGetChangingSensitivity([Range(0, AnalogInputChannels - 1)]int channel,
            [Range(1.0, 2.0, 1.0)]double sensitivity, [Range(1.0, 2.0, 1.0)]double zero)
        {
            using (AnalogAccelerometer input = GetAnalogAccelerometer(channel))
            {
                double voltage = (channel + 1.0) * 0.1;
                GetInputData(channel).Voltage = voltage;
                input.Sensitivity = sensitivity;
                input.Zero = zero;
                double acceleration = (voltage - zero) / sensitivity;
                Assert.That(input.GetAcceleration(), Is.EqualTo(acceleration).Within(0.001));
            }
        }

    }
}
