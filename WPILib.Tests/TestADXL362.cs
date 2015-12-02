using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator;
using HAL.Simulator.Data;
using NUnit.Framework;
using WPILib.Interfaces;

namespace WPILib.Tests
{
    [TestFixture(SPI.Port.MXP)]
    [TestFixture(SPI.Port.OnboardCS0)]
    [TestFixture(SPI.Port.OnboardCS1)]
    [TestFixture(SPI.Port.OnboardCS2)]
    [TestFixture(SPI.Port.OnboardCS3)]
    public class TestADXL362 : TestBase
    {
        private int m_port;
        private static ADXL362 m_accel;
        private static bool started = false;
    
        public TestADXL362(SPI.Port port)
        {
            m_accel = new ADXL362(port, AccelerometerRange.k2G);
            m_port = (int) port;
            started = true;
        }

        private SPIAccelerometerData GetData()
        {
            return SimData.SPIAccelerometer[m_port];
        }

        [TestFixtureTearDown]
        public void AfterClass()
        {
            if (started)
            {
                m_accel.Dispose();
            }
        }

        [Test]
        public void TestDisposeAndInitialize()
        {
            Assert.That(GetData().Active);
            m_accel.Dispose();
            Assert.That(GetData().Active, Is.False);
            m_accel = new ADXL362((SPI.Port)m_port, AccelerometerRange.k2G);
            Assert.That(GetData().Active);
        }


        [Test]
        [TestCase(AccelerometerRange.k2G)]
        [TestCase(AccelerometerRange.k4G)]
        [TestCase(AccelerometerRange.k8G)]
        [TestCase(AccelerometerRange.k16G)]
        public void TestSetRange(AccelerometerRange range)
        {
            m_accel.AccelerometerRange = range;
            Assert.AreEqual(GetData().Range, (byte)range);

            GetData().Active = false;
        }

        [Test]
        public void TestGetX()
        {
            const double testVal = 3.14;
            GetData().X = testVal;
            Assert.AreEqual(m_accel.GetX(), testVal);
        }

        [Test]
        public void TestGetY()
        {
            const double testVal = 3.14;
            GetData().Y = testVal;
            Assert.AreEqual(m_accel.GetY(), testVal);
        }

        [Test]
        public void TestGetZ()
        {
            const double testVal = 3.14;
            GetData().Z = testVal;
            Assert.AreEqual(m_accel.GetZ(), testVal);
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

            var all = m_accel.GetAllAxes();
            Assert.That(all.XAxis, Is.EqualTo(x).Within(0.01));
            Assert.That(all.YAxis, Is.EqualTo(y).Within(0.01));
            Assert.That(all.ZAxis, Is.EqualTo(z).Within(0.01));
        }

        [Test]
        public void TestBaccGetSmartDashboardType()
        {
            Assert.AreEqual("3AxisAccelerometer", new BuiltInAccelerometer(AccelerometerRange.k2G).SmartDashboardType);
        }
    }
}
