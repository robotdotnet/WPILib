//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using HAL.Simulator;
//using HAL.Simulator.Data;
//using NetworkTables.Tables;
//using NUnit.Framework;
//using WPILib.Exceptions;
//using WPILib.Interfaces;

//namespace WPILib.Tests
//{
//    public class TestAnalogAccelerometer : TestBase
//    {
//        public AnalogInData GetInputData(int pin)
//        {
//            return SimData.AnalogIn[pin];
//        }

//        public AnalogAccelerometer GetAnalogAccelerometer(int pin)
//        {
//            return new AnalogAccelerometer(pin);
//        }

//        [SetUp]
//        public void TestSetup()
//        {
//            SimData.ResetHALData(false);
//        }

//        [Test]
//        public void TestInputCreateUnderLimit()
//        {
//            Assert.Throws<ArgumentOutOfRangeException>(() =>
//            {
//                var input = GetAnalogAccelerometer(-1);
//            });
//        }

//        [Test]
//        public void TestInputCreateOverLimit()
//        {
//            Assert.Throws<ArgumentOutOfRangeException>(() =>
//            {
//                var input = GetAnalogAccelerometer(AnalogInputChannels);
//            });
//        }

//        [Test]
//        public void TestInputCreate()
//        {
//            using (AnalogAccelerometer input = GetAnalogAccelerometer(0))
//            {
//                Assert.IsTrue(GetInputData(0).Initialized);
//            }
//        }

//        [Test]
//        public void TestInputDoubleCreate()
//        {
//            using (AnalogAccelerometer input = GetAnalogAccelerometer(0))
//            {
//                Assert.Throws<AllocationException>(() =>
//                {
//                    var io2 = GetAnalogAccelerometer(0);
//                });
//            }
//        }

//        [Test]
//        public void TestAnalogAccelerometerCreateAll()
//        {
//            List<AnalogAccelerometer> inputs = new List<AnalogAccelerometer>();
//            for (int i = 0; i < AnalogInputChannels; i++)
//            {
//                inputs.Add(GetAnalogAccelerometer(i));
//                Assert.IsTrue(GetInputData(i).Initialized);
//            }

//            foreach (var input in inputs)
//            {
//                input.Dispose();
//            }
//        }

//        [Test]
//        public void TestCreationPreCreatedInput()
//        {
//            using (AnalogInput input = new AnalogInput(0))
//            {
//                Assert.IsTrue(SimData.AnalogIn[0].Initialized);
//                using (AnalogAccelerometer accel = new AnalogAccelerometer(input))
//                {
//                    Assert.IsTrue(GetInputData(0).Initialized);
//                }
//                Assert.IsTrue(SimData.AnalogIn[0].Initialized);
//            }
//            Thread.Sleep(100);
//            Assert.IsFalse(GetInputData(0).Initialized);
//            Assert.IsFalse(SimData.AnalogIn[0].Initialized);
//        }

//        [Test]
//        public void TestCreationPreCreatedInputNull()
//        {
//            Assert.Throws<ArgumentNullException>(() =>
//            {
//                AnalogAccelerometer accel = new AnalogAccelerometer(null);
//            });
//        }

//        [Test]
//        public void TestPidSourceTypeGetSet()
//        {
//            using (AnalogAccelerometer accel = GetAnalogAccelerometer(0))
//            {
//                Assert.That(accel.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
//                accel.PIDSourceType = PIDSourceType.Rate;
//                Assert.That(accel.PIDSourceType, Is.EqualTo(PIDSourceType.Rate));
//            }
//        }

//        [Test]
//        public void TestPidSourceTypeGetSetInterfacee()
//        {
//            using (AnalogAccelerometer input = GetAnalogAccelerometer(0))
//            {
//                IPIDSource accel = input;
//                Assert.That(accel.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
//                accel.PIDSourceType = PIDSourceType.Rate;
//                Assert.That(accel.PIDSourceType, Is.EqualTo(PIDSourceType.Rate));
//            }
//        }

//        [Test]
//        public void TestAnalogAccelerometerDispose()
//        {
//            AnalogAccelerometer input = GetAnalogAccelerometer(0);
//            Assert.IsTrue(GetInputData(0).Initialized);
//            input.Dispose();
//            Assert.IsFalse(GetInputData(0).Initialized);
//            SimData.ResetHALData(false);
//            input = GetAnalogAccelerometer(0);
//            Assert.IsTrue(GetInputData(0).Initialized);
//            input.Dispose();
//        }

//        [Test]
//        public void TestAnalogInputGetDefaultSensitivity([Range(0, AnalogInputChannels - 1)]int channel)
//        {
//            using (AnalogAccelerometer input = GetAnalogAccelerometer(channel))
//            {
//                double voltage = (channel + 1.0) * 0.1;
//                GetInputData(channel).Voltage = voltage;
//                double zero = input.Zero;
//                double sensitivity = input.Sensitivity;
//                double acceleration = (voltage - zero) / sensitivity;
//                Assert.That(input.GetAcceleration(), Is.EqualTo(acceleration).Within(0.001));
//            }
//        }

//        [Test]
//        public void TestAnalogInputGetChangingSensitivity([Range(0, AnalogInputChannels - 1)]int channel,
//            [Range(1.0, 2.0, 1.0)]double sensitivity, [Range(1.0, 2.0, 1.0)]double zero)
//        {
//            using (AnalogAccelerometer input = GetAnalogAccelerometer(channel))
//            {
//                double voltage = (channel + 1.0) * 0.1;
//                GetInputData(channel).Voltage = voltage;
//                input.Sensitivity = sensitivity;
//                input.Zero = zero;
//                double acceleration = (voltage - zero) / sensitivity;
//                Assert.That(input.GetAcceleration(), Is.EqualTo(acceleration).Within(0.001));
//            }
//        }

//        [Test]
//        public void TestAnalogAccelerometerPidGet([Range(0, AnalogInputChannels - 1)]int channel)
//        {
//            using (AnalogAccelerometer input = GetAnalogAccelerometer(channel))
//            {
//                IPIDSource pidInput = input;
//                double voltage = (channel + 1.0) * 0.1;
//                GetInputData(channel).Voltage = voltage;
//                double zero = input.Zero;
//                double sensitivity = input.Sensitivity;
//                double acceleration = (voltage - zero) / sensitivity;
//                Assert.That(input.GetAcceleration(), Is.EqualTo(acceleration).Within(0.001));
//                Assert.That(pidInput.PidGet(), Is.EqualTo(acceleration).Within(0.001));
//            }
//        }


//        [Test]
//        public void TestGetSmartDashboardType()
//        {
//            using (AnalogAccelerometer m_accel = GetAnalogAccelerometer(0))
//            {
//                Assert.AreEqual("Accelerometer", m_accel.SmartDashboardType);
//            }
//        }

//        [Test]
//        public void TestUpdateTableNull()
//        {
//            using (AnalogAccelerometer m_accel = GetAnalogAccelerometer(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    m_accel.UpdateTable();
//                });
//            }
//        }

//        [Test]
//        public void TestStartLiveWindowMode()
//        {
//            using (AnalogAccelerometer m_accel = GetAnalogAccelerometer(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    m_accel.StartLiveWindowMode();
//                });
//            }
//        }

//        [Test]
//        public void TestStopLiveWindowMode()
//        {
//            using (AnalogAccelerometer m_accel = GetAnalogAccelerometer(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    m_accel.StopLiveWindowMode();
//                });
//            }
//        }

//        [Test]
//        public void TestStartLiveWindowModeTable()
//        {
//            using (AnalogAccelerometer m_accel = GetAnalogAccelerometer(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    ITable table = new MockNetworkTable();
//                    m_accel.InitTable(table);
//                });
//            }


//        }

//        [Test]
//        public void TestInitTable()
//        {
//            using (AnalogAccelerometer m_accel = GetAnalogAccelerometer(0))
//            {
//                ITable table = new MockNetworkTable();
//                Assert.DoesNotThrow(() =>
//                {
//                    m_accel.InitTable(table);
//                });
//                Assert.That(m_accel.Table, Is.EqualTo(table));
//            }
//        }

//    }
//}
