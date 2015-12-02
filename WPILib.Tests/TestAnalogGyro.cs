using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator;
using HAL_Simulator.Data;
using HAL_Simulator.Inputs;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestAnalogGyro : TestBase
    {
        public AnalogInData GetGyroData(int pin)
        {
            return SimData.AnalogIn[pin];
        }

        public AnalogGyro GetAnalogGyro(int pin)
        {
            return new AnalogGyro(pin);
        }

        [SetUp]
        public void TestSetup()
        {
            SimData.ResetHALData(false);
        }

        [Test]
        public void TestCreateExistingAnalogInput()
        {
            using (AnalogInput aIn = new AnalogInput(0))
            using (AnalogGyro input = new AnalogGyro(aIn))
            {
                Assert.IsTrue(GetGyroData(0).Initialized);
            }
        }

        public void TestCreateAnalogInputNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                AnalogGyro input = new AnalogGyro(null);
            });
        }

        [Test]
        public void TestInputCreateUnderLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetAnalogGyro(-1);
            });
        }

        [Test]
        public void TestInputCreateOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetAnalogGyro(NumAccumulatorChannels);
            });
        }

        [Test]
        public void TestInputCreate()
        {
            using (AnalogGyro input = GetAnalogGyro(0))
            {
                Assert.IsTrue(GetGyroData(0).Initialized);
            }
        }

        [Test]
        public void TestInputDoubleCreate()
        {
            using (AnalogGyro input = GetAnalogGyro(0))
            {
                Assert.Throws<AllocationException>(() =>
                {
                    var io2 = GetAnalogGyro(0);
                });
            }
        }

        const int NumAccumulatorChannels = 2;

        [Test]
        public void TestAnalogGyroCreateAll()
        {
            List<AnalogGyro> inputs = new List<AnalogGyro>();
            for (int i = 0; i < NumAccumulatorChannels; i++)
            {
                inputs.Add(GetAnalogGyro(i));
                Assert.IsTrue(GetGyroData(i).Initialized);
            }

            foreach (var input in inputs)
            {
                input.Dispose();
            }

            for (int i = 0; i < NumAccumulatorChannels; i++)
            {
                Assert.IsFalse(GetGyroData(i).Initialized);
            }
        }

        [Test]
        public void TestAnalogGyroUnderCreate()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetAnalogGyro(-1);
            });
        }

        [Test]
        public void TestAnalogGyroOverCreate()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var input = GetAnalogGyro(NumAccumulatorChannels);
            });
        }



        [Test]
        public void TestAnalogGyroDispose()
        {
            AnalogGyro input = GetAnalogGyro(0);
            Assert.IsTrue(GetGyroData(0).Initialized);
            Assert.IsTrue(GetGyroData(0).AccumulatorInitialized);
            input.Dispose();
            Assert.IsFalse(GetGyroData(0).Initialized);
            Assert.IsFalse(GetGyroData(0).AccumulatorInitialized);
            SimData.ResetHALData(false);
            input = GetAnalogGyro(0);
            Assert.IsTrue(GetGyroData(0).Initialized);
            Assert.IsTrue(GetGyroData(0).AccumulatorInitialized);
            input.Dispose();
        }

        [Test]
        public void TestAnalogGyroGetRate([Range(0, NumAccumulatorChannels - 1)]int channel)
        {
            
            using (AnalogGyro input = GetAnalogGyro(channel))
            {
                SimAnalogGyro gyro = new SimAnalogGyro(channel);
                gyro.SetRate(1.025);
                Assert.AreEqual(1.025, input.GetRate(), 0.0001);
                gyro.SetRate(-1.0835);
                Assert.AreEqual(-1.0835, input.GetRate(), 0.0001);
            }
        }

        [Test]
        public void TestAnalogGyroGetAngle([Range(0, NumAccumulatorChannels - 1)]int channel)
        {

            using (AnalogGyro input = GetAnalogGyro(channel))
            {
                SimAnalogGyro gyro = new SimAnalogGyro(channel);
                gyro.SetPosition(1.025);
                Assert.AreEqual(1.025, input.GetAngle(), 0.0001);
                gyro.SetPosition(-1.0835);
                Assert.AreEqual(-1.0835, input.GetAngle(), 0.0001);
            }
        }
    }
}
