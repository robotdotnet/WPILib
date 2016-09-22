//using System;
//using System.Collections.Generic;
//using HAL.Simulator;
//using HAL.Simulator.Data;
//using NetworkTables.Tables;
//using NUnit.Framework;
//using WPILib.Exceptions;
//using WPILib.Interfaces;

//// ReSharper disable UnusedVariable

//namespace WPILib.Tests
//{
//    [TestFixture]
//    public class TestAnalogInput : TestBase
//    {
//        public AnalogInData GetInputData(int pin)
//        {
//            return SimData.AnalogIn[pin];
//        }

//        public AnalogInput GetAnalogInput(int pin)
//        {
//            return new AnalogInput(pin);
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
//                var input = GetAnalogInput(-1);
//            });
//        }

//        [Test]
//        public void TestInputCreateOverLimit()
//        {
//            Assert.Throws<ArgumentOutOfRangeException>(() =>
//            {
//                var input = GetAnalogInput(AnalogInputChannels);
//            });
//        }

//        [Test]
//        public void TestInputCreate()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                Assert.IsTrue(GetInputData(0).Initialized);
//            }
//        }

//        [Test]
//        public void TestInputDoubleCreate()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                Assert.Throws<AllocationException>(() =>
//                {
//                    var io2 = GetAnalogInput(0);
//                });
//            }
//        }

//        [Test]
//        public void TestAnalogInputCreateAll()
//        {
//            List<AnalogInput> inputs = new List<AnalogInput>();
//            for (int i = 0; i < AnalogInputChannels; i++)
//            {
//                inputs.Add(GetAnalogInput(i));
//                Assert.IsTrue(GetInputData(i).Initialized);
//            }

//            foreach (var input in inputs)
//            {
//                input.Dispose();
//            }
//        }

//        [Test]
//        public void TestPidSourceTypeGetSet()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                Assert.That(input.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
//                input.PIDSourceType = PIDSourceType.Rate;
//                Assert.That(input.PIDSourceType, Is.EqualTo(PIDSourceType.Rate));
//            }
//        }

//        [Test]
//        public void TestPidSourceTypeGetSetInterfacee()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                IPIDSource analogInput = input;
//                Assert.That(analogInput.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
//                analogInput.PIDSourceType = PIDSourceType.Rate;
//                Assert.That(analogInput.PIDSourceType, Is.EqualTo(PIDSourceType.Rate));
//            }
//        }

//        [Test]
//        public void TestAnalogInputDispose()
//        {
//            AnalogInput input = GetAnalogInput(0);
//            Assert.IsTrue(GetInputData(0).Initialized);
//            input.Dispose();
//            Assert.IsFalse(GetInputData(0).Initialized);
//            SimData.ResetHALData(false);
//            input = GetAnalogInput(0);
//            Assert.IsTrue(GetInputData(0).Initialized);
//            input.Dispose();
//        }

//        [Test]
//        public void TestAnalogInputGet([Range(0, AnalogInputChannels - 1)]int channel)
//        {
//            using (AnalogInput input = GetAnalogInput(channel))
//            {
//                GetInputData(channel).Voltage = (channel + 1.0) * 0.1;
//                Assert.AreEqual((channel + 1.0) * 0.1, input.GetVoltage(), 0.0001);
//            }
//        }

//        [Test]
//        public void TestGetValue()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                double voltage = 4.0;
//                GetInputData(0).Voltage = voltage;
//                short value = GetAnalogVoltageToValue(0, voltage);
//                Assert.That(input.GetValue(), Is.EqualTo(value));
//            }
//        }

//        [Test]
//        public void TestGetAverageValue()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                double voltage = 4.0;
//                GetInputData(0).Voltage = voltage;
//                short value = GetAnalogVoltageToValue(0, voltage);
//                Assert.That(input.GetAverageValue(), Is.EqualTo(value));
//            }
//        }

//        private short GetAnalogVoltageToValue(int channel, double voltage)
//        {
//            if (voltage > 5.0)
//            {
//                voltage = 5.0;
//            }
//            else if (voltage < 0.0)
//            {
//                voltage = 0.0;
//            }

//            long LSBWeight = SimData.AnalogIn[channel].LSBWeight;
//            int offset = SimData.AnalogIn[channel].Offset;
//            return (short)((voltage + offset * 1.0e-9) / (LSBWeight * 1.0e-9));
//        }

//        [Test]
//        public void TestGetOffset()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                Assert.That(input.Offset, Is.EqualTo(GetInputData(0).Offset));
//            }
//        }

//        [Test]
//        public void TestInitAccumulatorValidChannel()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                Assert.That(GetInputData(0).AccumulatorInitialized, Is.False);
//                Assert.DoesNotThrow(() =>
//                {
//                    input.InitAccumulator();
//                });
//                Assert.That(GetInputData(0).AccumulatorInitialized, Is.True);
//            }
//        }

//        [Test]
//        public void TestInitAccumulatorInvalidChannel()
//        {
//            using (AnalogInput input = GetAnalogInput(4))
//            {
//                Assert.That(GetInputData(0).AccumulatorInitialized, Is.False);
//                Assert.Throws<AllocationException>(() =>
//                {
//                    input.InitAccumulator();
//                });
//                Assert.That(GetInputData(0).AccumulatorInitialized, Is.False);
//            }
//        }

//        [Test]
//        public void TestResetAccumulator()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                GetInputData(0).AccumulatorCenter = 5550;
//                GetInputData(0).AccumulatorCount = 5550;
//                GetInputData(0).AccumulatorValue = 5550;
//                input.ResetAccumulator();
//                Assert.That(GetInputData(0).AccumulatorCenter, Is.EqualTo(0));
//                Assert.That(GetInputData(0).AccumulatorCount, Is.EqualTo(0));
//                Assert.That(GetInputData(0).AccumulatorValue, Is.EqualTo(0));
//            }
//        }

//        [Test]
//        public void TestSetAccumulatorCenter()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                GetInputData(0).AccumulatorCenter = 0;
//                input.AccumulatorCenter = 55;
//                Assert.That(GetInputData(0).AccumulatorCenter, Is.EqualTo(55));
//            }
//        }

//        [Test]
//        public void TestGetAccumulatorOutput()
//        {
//            using (AnalogInput input = GetAnalogInput(0))
//            {
//                input.AccumulatorInitialValue = 255;
//                GetInputData(0).AccumulatorCount = 5550;
//                GetInputData(0).AccumulatorValue = 5550;

//                long value = 0;
//                uint count = 0;
//                input.GetAccumulatorOutput(ref value, ref count);
//                Assert.That(count, Is.EqualTo(5550));
//                Assert.That(value, Is.EqualTo(5550 + 255));
//            }
//        }

//        [Test]
//        public void TestGetAccumulatorOutputInvalid()
//        {
//            using (AnalogInput input = GetAnalogInput(4))
//            {
//                input.AccumulatorInitialValue = 255;
//                GetInputData(0).AccumulatorCount = 5550;
//                GetInputData(0).AccumulatorValue = 5550;

//                long value = 0;
//                uint count = 0;
//                Assert.Throws<ArgumentException>(() =>
//                {
//                    input.GetAccumulatorOutput(ref value, ref count);
//                });
//            }
//        }

//        [Test]
//        public void TestPidGet()
//        {
//            using (AnalogInput input = GetAnalogInput(4))
//            {
//                GetInputData(4).Voltage = 4.25;
//                Assert.That(input.PidGet(), Is.EqualTo(4.25).Within(0.001));
//            }
//        }

//        [Test]
//        public void TestPidGetInterface()
//        {
//            using (AnalogInput input = GetAnalogInput(4))
//            {
//                IPIDSource interInput = input;
//                GetInputData(4).Voltage = 4.25;
//                Assert.That(interInput.PidGet(), Is.EqualTo(4.25).Within(0.001));
//            }
//        }

//        [Test]
//        public void TestSmartDashboardType()
//        {
//            using (AnalogInput s = GetAnalogInput(4))
//            {
//                Assert.That(s.SmartDashboardType, Is.EqualTo("Analog Input"));
//            }
//        }

//        [Test]
//        public void TestUpdateTableNull()
//        {
//            using (AnalogInput s = GetAnalogInput(4))
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
//            using (AnalogInput s = GetAnalogInput(4))
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
//            using (AnalogInput s = GetAnalogInput(4))
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
//            using (AnalogInput s = GetAnalogInput(4))
//            {
//                Assert.DoesNotThrow(() =>
//                {
//                    s.StopLiveWindowMode();
//                });
//            }
//        }
//    }
//}
