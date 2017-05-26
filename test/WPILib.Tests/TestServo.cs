using HAL.Simulator;
using HAL.Simulator.Data;
using NetworkTables;
using NetworkTables.Tables;
using NUnit.Framework;
// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture]
    public class TestServo : TestBase
    {


        public Servo NewServo()
        {
            return new Servo(2);
        }


        [Test]
        public void TestServoInitialized()
        {
            using (Servo s = NewServo())
            {
                Assert.That(SimData.PWM[2].GetInitialized());
            }
        }
        [Test]
        public void TestServoStarts0()
        {
            using (Servo s = NewServo())
            {
                Assert.AreEqual(s.Get(), 0);
            }
        }
        [Test]
        public void TestServoSet()
        {
            using (Servo s = NewServo())
            {
                s.Set(1.0);
                Assert.AreEqual(s.Get(), 1.0);
            }
        }
        [Test]
        public void TestServoSetAngle()
        {
            using (Servo s = NewServo())
            {
                s.SetAngle(76.5);
                Assert.AreEqual(s.GetAngle(), 76.5);
            }
        }

        [Test]
        public void TestServeSetOutsideAngleOver()
        {
            using (Servo s = NewServo())
            {
                s.SetAngle(185.9);
                Assert.AreEqual(s.GetAngle(), 180.0, .1);
            }
        }

        [Test]
        public void TestServeSetOutsideAngleUnder()
        {
            using (Servo s = NewServo())
            {
                s.SetAngle(-10.5);
                Assert.AreEqual(s.GetAngle(), 0.0, .1);
            }
        }


        [Test]
        public void TestSmartDashboardType()
        {
            using (Servo s = NewServo())
            {
                Assert.That(s.SmartDashboardType, Is.EqualTo("Servo"));
            }
        }

        [Test]
        public void TestUpdateTableNull()
        {
            using (Servo s = NewServo())
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
            using (Servo s = NewServo())
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
            using (Servo s = NewServo())
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
            using (Servo s = NewServo())
            {
                Assert.DoesNotThrow(() =>
                {
                    s.StopLiveWindowMode();
                });
            }
        }

        [Test]
        public void TestValueChanged()
        {
            using (Servo s = NewServo())
            {
                s.SetAngle(80.0);
                Assert.That(s.GetAngle, Is.EqualTo(80.0).Within(0.1));
                s.ValueChanged(null, null, Value.MakeDouble(.56), NetworkTables.NotifyFlags.NotifyLocal);
                Assert.That(s.Get, Is.EqualTo(.56).Within(0.01));
                s.ValueChanged(null, null, Value.MakeDouble(-.58), NetworkTables.NotifyFlags.NotifyLocal);
                Assert.That(s.Get, Is.EqualTo(0.0).Within(0.01));
            }
        }

    }
}
