using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator;
using HAL.Simulator.Data;
using NetworkTables.Tables;
using NUnit.Framework;
using WPILib.Exceptions;
using WPILib.Interfaces;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestAnalogPotentiometer : TestBase
    {
        public AnalogInData GetInputData(int pin)
        {
            return SimData.AnalogIn[pin];
        }

        public AnalogPotentiometer GetAnalogInput(int pin)
        {
            return new AnalogPotentiometer(pin);
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
            using (AnalogPotentiometer input = GetAnalogInput(0))
            {
                Assert.IsTrue(GetInputData(0).Initialized);
            }
        }

        [Test]
        public void TestInputDoubleCreate()
        {
            using (AnalogPotentiometer input = GetAnalogInput(0))
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
            List<AnalogPotentiometer> inputs = new List<AnalogPotentiometer>();
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
        public void TestPidSourceTypeGetSet()
        {
            using (AnalogPotentiometer input = GetAnalogInput(0))
            {
                Assert.That(input.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    input.PIDSourceType = PIDSourceType.Rate;
                });
                
                Assert.That(input.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
            }
        }

        [Test]
        public void TestPidSourceTypeGetSetInterfacee()
        {
            using (AnalogPotentiometer input = GetAnalogInput(0))
            {
                IPIDSource analogInput = input;
                Assert.That(analogInput.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    analogInput.PIDSourceType = PIDSourceType.Rate;
                });

                Assert.That(analogInput.PIDSourceType, Is.EqualTo(PIDSourceType.Displacement));
            }
        }

        [Test]
        public void TestAnalogInputDispose()
        {
            AnalogPotentiometer input = GetAnalogInput(0);
            Assert.IsTrue(GetInputData(0).Initialized);
            input.Dispose();
            Assert.IsFalse(GetInputData(0).Initialized);
            SimData.ResetHALData(false);
            input = GetAnalogInput(0);
            Assert.IsTrue(GetInputData(0).Initialized);
            input.Dispose();
        }

        [Test]
        public void TestAnalogInputGet([Range(0.0,1.0, 0.2)] double fullRange, [Range(0.0, 1.0, 1.0)] double offset)
        {
            using (AnalogPotentiometer input = new AnalogPotentiometer(0, fullRange, offset))
            {
                GetInputData(0).Voltage = 3.0;
                // result = (get / 5v) * range + offset
                // (result - offset) / range = get /5v
                // (result - offset * 5v) / range = get
                double expected = (3.0 / ControllerPower.GetVoltage5V()) * fullRange + offset;

                Assert.AreEqual(expected, input.Get(), 0.0001);
            }
        }
        [Test]
        public void TestPidGet()
        {
            using (AnalogPotentiometer input = GetAnalogInput(4))
            {
                GetInputData(4).Voltage = 4.25;
                double expected = (4.25 / ControllerPower.GetVoltage5V()) * 1 + 0;
                Assert.That(input.PidGet(), Is.EqualTo(expected).Within(0.001));
            }
        }

        [Test]
        public void TestPidGetInterface()
        {
            using (AnalogPotentiometer input = GetAnalogInput(4))
            {
                IPIDSource interInput = input;
                GetInputData(4).Voltage = 4.25;
                double expected = (4.25 / ControllerPower.GetVoltage5V()) * 1 + 0;
                Assert.That(interInput.PidGet(), Is.EqualTo(expected).Within(0.001));
            }
        }

        [Test]
        public void TestSmartDashboardType()
        {
            using (AnalogPotentiometer s = GetAnalogInput(4))
            {
                Assert.That(s.SmartDashboardType, Is.EqualTo("Analog Input"));
            }
        }

        [Test]
        public void TestUpdateTableNull()
        {
            using (AnalogPotentiometer s = GetAnalogInput(4))
            {
                Assert.DoesNotThrow(() =>
                {
                    s.UpdateTable();
                });
            }
        }

        [Test]
        public void TestInitTable()
        {
            using (AnalogPotentiometer s = GetAnalogInput(4))
            {
                ITable table = new MockNetworkTable();
                Assert.DoesNotThrow(() =>
                {
                    s.InitTable(table);
                });
                Assert.That(s.Table, Is.EqualTo(table));
            }

        }

        [Test]
        public void TestStartLiveWindowMode()
        {
            using (AnalogPotentiometer s = GetAnalogInput(4))
            {
                Assert.DoesNotThrow(() =>
                {
                    s.StartLiveWindowMode();
                });
            }
        }

        [Test]
        public void TestStopLiveWindowMode()
        {
            using (AnalogPotentiometer s = GetAnalogInput(4))
            {
                Assert.DoesNotThrow(() =>
                {
                    s.StopLiveWindowMode();
                });
            }
        }
    }
}
