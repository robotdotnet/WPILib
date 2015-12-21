using System;
using HAL;
using HAL.Base;
using HAL.Simulator;
using HAL.Simulator.Data;
using NetworkTables.Tables;
using NUnit.Framework;
using WPILib.Interfaces;
// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture]
    public class TestBuiltinAccelerometer
    {
        private AccelerometerData GetData()
        {
            return SimData.Accelerometer;
        }

        private static readonly BuiltInAccelerometer s_instance = null;

        private static BuiltInAccelerometer GetAcc()
        {
            return s_instance ?? new BuiltInAccelerometer(AccelerometerRange.k2G);
        }

        [TestFixtureSetUp]
        public static void Setup()
        {
            GetAcc();
        }

        [Test]
        [TestCase(AccelerometerRange.k2G)]
        [TestCase(AccelerometerRange.k4G)]
        [TestCase(AccelerometerRange.k8G)]
        public void TestSetRange(AccelerometerRange range)
        {
            GetAcc().AccelerometerRange = range;
            Assert.AreEqual(GetData().Range, (HALAccelerometerRange)range);

            GetData().Active = false;
        }

        [Test]
        public void TestRangeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var x = new BuiltInAccelerometer(AccelerometerRange.k16G);
            });
            Assert.IsFalse(GetData().Active);
        }

        [Test]
        public void TestRangeOutofEnumRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var x = new BuiltInAccelerometer((AccelerometerRange)11);
            });
            Assert.IsFalse(GetData().Active);
        }

        [Test]
        public void TestGetX()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData().X = testVal;
            Assert.AreEqual(GetAcc().GetX(), testVal);
        }

        [Test]
        public void TestGetY()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData().Y = testVal;
            Assert.AreEqual(GetAcc().GetY(), testVal);
        }

        [Test]
        public void TestGetZ()
        {
            GetAcc();
            const double testVal = 3.14;
            GetData().Z = testVal;
            Assert.AreEqual(GetAcc().GetZ(), testVal);
        }

        [Test]
        public void TestGetAll()
        {
            const double x = 5.85;
            const double y = 6.82;
            const double z = 1.923;
            GetData().X = x;
            GetData().Y = y;
            GetData().Z = z;

            var all = GetAcc().GetAllAxes();
            Assert.That(all.XAxis, Is.EqualTo(x).Within(0.01));
            Assert.That(all.YAxis, Is.EqualTo(y).Within(0.01));
            Assert.That(all.ZAxis, Is.EqualTo(z).Within(0.01));
        }

        [Test]
        public void TestBaccGetSmartDashboardType()
        {
            Assert.AreEqual("3AxisAccelerometer", new BuiltInAccelerometer(AccelerometerRange.k2G).SmartDashboardType);
        }

        [Test]
        public void TestUpdateTableNull()
        {
            BuiltInAccelerometer m_accel = new BuiltInAccelerometer(AccelerometerRange.k4G);
            Assert.DoesNotThrow(() =>
            {
                m_accel.UpdateTable();
            });
        }

        [Test]
        public void TestStartLiveWindowMode()
        {
            BuiltInAccelerometer m_accel = new BuiltInAccelerometer(AccelerometerRange.k4G);
            Assert.DoesNotThrow(() =>
            {
                m_accel.StartLiveWindowMode();
            });
        }

        [Test]
        public void TestStopLiveWindowMode()
        {
            BuiltInAccelerometer m_accel = new BuiltInAccelerometer(AccelerometerRange.k4G);
            Assert.DoesNotThrow(() =>
            {
                m_accel.StopLiveWindowMode();
            });
        }

        [Test]
        public void TestStartLiveWindowModeTable()
        {
            BuiltInAccelerometer m_accel = new BuiltInAccelerometer(AccelerometerRange.k4G);
            Assert.DoesNotThrow(() =>
            {
                ITable table = new MockNetworkTable();
                m_accel.InitTable(table);
            });


        }

        [Test]
        public void TestInitTable()
        {
            BuiltInAccelerometer m_accel = new BuiltInAccelerometer(AccelerometerRange.k4G);
            ITable table = new MockNetworkTable();
            Assert.DoesNotThrow(() =>
            {
                m_accel.InitTable(table);
            });
            Assert.That(m_accel.Table, Is.EqualTo(table));
        }
    }
}
