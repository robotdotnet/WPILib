using HAL.Simulator;
using HAL.Simulator.Data;
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
                Assert.AreEqual(SimData.PWM[2].Type, ControllerType.Servo);
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
        public void TestStartLiveWindowModeTableNull()
        {
            using (Servo s = NewServo())
            {
            }
        }

        [Test]
        public void TestStopLiveWindowModeTableNull()
        {
            using (Servo s = NewServo())
            {
            }
        }

        [Test]
        public void TestStartLiveWindowModeTable()
        {
            using (Servo s = NewServo())
            {
                ITable table = new MockNetworkTable();
                s.InitTable(table);
            }
        }

        [Test]
        public void TestStopLiveWindowModeTable()
        {
            using (Servo s = NewServo())
            {
                ITable table = new MockNetworkTable();
                s.InitTable(table);
            }
        }

        [Test]
        public void TestValueChanged()
        {
            using (Servo s = NewServo())
            {
                s.SetAngle(80.0);
                Assert.That(s.GetAngle, Is.EqualTo(80.0).Within(0.1));
                s.ValueChanged(null, null, .56, NetworkTables.NotifyFlags.NotifyLocal);
                Assert.That(s.Get, Is.EqualTo(.56).Within(0.01));
                s.ValueChanged(null, null, -.58, NetworkTables.NotifyFlags.NotifyLocal);
                Assert.That(s.Get, Is.EqualTo(0.0).Within(0.01));
            }
        }

    }
}
