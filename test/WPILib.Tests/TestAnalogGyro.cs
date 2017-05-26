//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HAL.Simulator;
//using HAL.Simulator.Data;
//using HAL.Simulator.Inputs;
//using NetworkTables.Tables;
//using NUnit.Framework;
//using WPILib.Exceptions;
//using WPILib.Interfaces;

//namespace WPILib.Tests
//{
//    [TestFixture]
//    public class TestAnalogGyro : TestBase
//    {
//        public AnalogInData GetGyroData(int pin)
//        {
//            return SimData.AnalogIn[pin];
//        }

//        public AnalogGyro GetAnalogGyro(int pin)
//        {
//            return new AnalogGyro(pin);
//        }

//        [SetUp]
//        public void TestSetup()
//        {
//            SimData.ResetHALData(false);
//        }

//        [Test]
//        public void TestCreateExistingAnalogInput()
//        {
//            using (AnalogInput aIn = new AnalogInput(0))
//            using (AnalogGyro input = new AnalogGyro(aIn))
//            {
//                Assert.IsTrue(GetGyroData(0).Initialized);
//            }
//        }

//        public void TestCreateAnalogInputNull()
//        {
//            Assert.Throws<ArgumentNullException>(() =>
//            {
//                AnalogGyro input = new AnalogGyro(null);
//            });
//        }

//        [Test]
//        public void TestInputCreateUnderLimit()
//        {
//            Assert.Throws<ArgumentOutOfRangeException>(() =>
//            {
//                var input = GetAnalogGyro(-1);
//            });
//        }

//        [Test]
//        public void TestInputCreateOverLimit()
//        {
//            Assert.Throws<ArgumentOutOfRangeException>(() =>
//            {
//                var input = GetAnalogGyro(NumAccumulatorChannels);
//            });
//        }

//        [Test]
//        public void TestInputCreate()
//        {
//            using (AnalogGyro input = GetAnalogGyro(0))
//            {
//                Assert.IsTrue(GetGyroData(0).Initialized);
//            }
//        }

//        [Test]
//        public void TestInputDoubleCreate()
//        {
//            using (AnalogGyro input = GetAnalogGyro(0))
//            {
//                Assert.Throws<AllocationException>(() =>
//                {
//                    var io2 = GetAnalogGyro(0);
//                });
//            }
//        }

//        const int NumAccumulatorChannels = 2;

//        [Test]
//        public void TestAnalogGyroCreateAll()
//        {
//            List<AnalogGyro> inputs = new List<AnalogGyro>();
//            for (int i = 0; i < NumAccumulatorChannels; i++)
//            {
//                inputs.Add(GetAnalogGyro(i));
//                Assert.IsTrue(GetGyroData(i).Initialized);
//            }

//            foreach (var input in inputs)
//            {
//                input.Dispose();
//            }

//            for (int i = 0; i < NumAccumulatorChannels; i++)
//            {
//                Assert.IsFalse(GetGyroData(i).Initialized);
//            }
//        }

//        [Test]
//        public void TestAnalogGyroUnderCreate()
//        {
//            Assert.Throws<ArgumentOutOfRangeException>(() =>
//            {
//                var input = GetAnalogGyro(-1);
//            });
//        }

//        [Test]
//        public void TestAnalogGyroOverCreate()
//        {
//            Assert.Throws<ArgumentOutOfRangeException>(() =>
//            {
//                var input = GetAnalogGyro(NumAccumulatorChannels);
//            });
//        }

//        [Test]
//        public void TestCreationPreCreatedInputNull()
//        {
//            Assert.Throws<ArgumentNullException>(() =>
//            {
//                AnalogGyro gyro = new AnalogGyro(null);
//            });
//        }

//        [Test]
//        public void TestReset()
//        {
//            using (AnalogGyro input = GetAnalogGyro(0))
//            {
//                SimAnalogGyro gyro = new SimAnalogGyro(0);
//                gyro.SetRate(1.025);
//                gyro.SetPosition(1.124);
//                Assert.AreEqual(1.025, input.GetRate(), 0.0001);
//                Assert.AreEqual(1.124, input.GetAngle(), 0.0001);
//                Assert.That(GetGyroData(0).AccumulatorCount, Is.Not.EqualTo(0));
//                Assert.That(GetGyroData(0).AccumulatorValue, Is.Not.EqualTo(0));
//                input.Reset();
//                Assert.That(GetGyroData(0).AccumulatorCount, Is.EqualTo(0));
//                Assert.That(GetGyroData(0).AccumulatorValue, Is.EqualTo(0));

//            }
//        }

//        [Test]
//        public void TestPidSourceTypeGetSet()
//        {
//            using (AnalogGyro gyro = GetAnalogGyro(0))
//            {
//                Assert.That(gyro.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
//                gyro.PIDSourceType = PIDSourceType.Rate;
//                Assert.That(gyro.PIDSourceType, Is.EqualTo(PIDSourceType.Rate));
//            }
//        }

//        [Test]
//        public void TestPidSourceTypeGetSetInterfacee()
//        {
//            using (AnalogGyro input = GetAnalogGyro(0))
//            {
//                IPIDSource gyro = input;
//                Assert.That(gyro.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
//                gyro.PIDSourceType = PIDSourceType.Rate;
//                Assert.That(gyro.PIDSourceType, Is.EqualTo(PIDSourceType.Rate));
//            }
//        }



//        [Test]
//        public void TestAnalogGyroDispose()
//        {
//            AnalogGyro input = GetAnalogGyro(0);
//            Assert.IsTrue(GetGyroData(0).Initialized);
//            Assert.IsTrue(GetGyroData(0).AccumulatorInitialized);
//            input.Dispose();
//            Assert.IsFalse(GetGyroData(0).Initialized);
//            Assert.IsFalse(GetGyroData(0).AccumulatorInitialized);
//            SimData.ResetHALData(false);
//            input = GetAnalogGyro(0);
//            Assert.IsTrue(GetGyroData(0).Initialized);
//            Assert.IsTrue(GetGyroData(0).AccumulatorInitialized);
//            input.Dispose();
//        }

//        [Test]
//        public void TestAnalogGyroGetRate([Range(0, NumAccumulatorChannels - 1)]int channel)
//        {
            
//            using (AnalogGyro input = GetAnalogGyro(channel))
//            {
//                SimAnalogGyro gyro = new SimAnalogGyro(channel);
//                gyro.SetRate(1.025);
//                Assert.AreEqual(1.025, input.GetRate(), 0.0001);
//                gyro.SetRate(-1.0835);
//                Assert.AreEqual(-1.0835, input.GetRate(), 0.0001);
//            }
//        }

//        [Test]
//        public void TestAnalogGyroGetAngle([Range(0, NumAccumulatorChannels - 1)]int channel)
//        {

//            using (AnalogGyro input = GetAnalogGyro(channel))
//            {
//                SimAnalogGyro gyro = new SimAnalogGyro(channel);
//                gyro.SetPosition(1.025);
//                Assert.AreEqual(1.025, input.GetAngle(), 0.0001);
//                gyro.SetPosition(-1.0835);
//                Assert.AreEqual(-1.0835, input.GetAngle(), 0.0001);
//            }
//        }

//        [Test]
//        public void TestSmartDashboardType()
//        {
//            using (AnalogGyro s = GetAnalogGyro(0))
//            {
//                Assert.That(s.SmartDashboardType, Is.EqualTo("Gyro"));
//            }
//        }

//        [Test]
//        public void TestPidGetDisplacement()
//        {
//            using (AnalogGyro c = GetAnalogGyro(0))
//            {
//                SimAnalogGyro gyro = new SimAnalogGyro(0);
//                c.PIDSourceType = PIDSourceType.Displacement;
//                gyro.SetPosition(1.025);
//                Assert.That(c.PidGet(), Is.EqualTo(1.025).Within(0.001));
//                gyro.SetPosition(-1.0835);
//                Assert.That(c.PidGet(), Is.EqualTo(-1.0835).Within(0.001));
//            }
//        }

//        [Test]
//        public void TestPidGetRate()
//        {
//            using (AnalogGyro c = GetAnalogGyro(0))
//            {
//                SimAnalogGyro gyro = new SimAnalogGyro(0);
//                c.PIDSourceType = PIDSourceType.Rate;
//                gyro.SetRate(1.025);
//                Assert.That(c.PidGet(), Is.EqualTo(1.025).Within(0.001));
//                gyro.SetRate(-1.0835);
//                Assert.That(c.PidGet(), Is.EqualTo(-1.0835).Within(0.001));
//            }
//        }

//        [Test]
//        public void TestUpdateTableNull()
//        {
//            using (AnalogGyro s = GetAnalogGyro(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    s.UpdateTable();
//                });
//            }
//        }

//        [Test]
//        public void TestInitTable()
//        {
//            using (AnalogGyro s = GetAnalogGyro(0))
//            {
//                ITable table = new MockNetworkTable();
//                Assert.DoesNotThrow(() =>
//                {
//                    s.InitTable(table);
//                });
//                Assert.That(s.Table, Is.EqualTo(table));
//            }

//        }

//        [Test]
//        public void TestStartLiveWindowMode()
//        {
//            using (AnalogGyro s = GetAnalogGyro(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    s.StartLiveWindowMode();
//                });
//            }
//        }

//        [Test]
//        public void TestStopLiveWindowMode()
//        {
//            using (AnalogGyro s = GetAnalogGyro(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    s.StopLiveWindowMode();
//                });
//            }
//        }

//        [Test]
//        public void TestUpdateTableNullBase()
//        {
//            using (GyroBase s = GetAnalogGyro(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    s.UpdateTable();
//                });
//            }
//        }

//        [Test]
//        public void TestInitTableBase()
//        {
//            using (GyroBase s = GetAnalogGyro(0))
//            {
//                ITable table = new MockNetworkTable();
//                Assert.DoesNotThrow(() =>
//                {
//                    s.InitTable(table);
//                });
//                Assert.That(s.Table, Is.EqualTo(table));
//            }

//        }

//        [Test]
//        public void TestStartLiveWindowModeBase()
//        {
//            using (GyroBase s = GetAnalogGyro(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    s.StartLiveWindowMode();
//                });
//            }
//        }

//        [Test]
//        public void TestStopLiveWindowModeBase()
//        {
//            using (GyroBase s = GetAnalogGyro(0))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    s.StopLiveWindowMode();
//                });
//            }
//        }
//    }
//}
