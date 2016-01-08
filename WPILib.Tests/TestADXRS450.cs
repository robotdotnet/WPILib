using HAL.Simulator;
using HAL.Simulator.Data;
using HAL.Simulator.Inputs;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture(SPI.Port.MXP)]
    [TestFixture(SPI.Port.OnboardCS0)]
    [TestFixture(SPI.Port.OnboardCS1)]
    [TestFixture(SPI.Port.OnboardCS2)]
    [TestFixture(SPI.Port.OnboardCS3)]
    public class TestADXRS450 : TestBase
    {
        private int m_port;
        private static ADXRS450_Gyro m_gyro;
        private static bool started = false;

        public TestADXRS450(SPI.Port port)
        {
            m_gyro = new ADXRS450_Gyro(port);
            m_port = (int)port;
            started = true;
        }

        private SPIAccumulatorData GetData()
        {
            return SimData.SPIAccumulator[m_port];
        }

        [TestFixtureTearDown]
        public void AfterClass()
        {
            if (started)
            {
                m_gyro.Dispose();
            }
        }

        [Test]
        public void TestDisposeAndInitialize()
        {
            Assert.That(GetData().Initialized);
            m_gyro.Dispose();
            Assert.That(GetData().Initialized, Is.False);
            m_gyro = new ADXRS450_Gyro((SPI.Port)m_port);
            Assert.That(GetData().Initialized);
        }

        [Test]
        public void TestAnalogGyroGetRate()
        {
            SimSPIGyro gyro = new SimSPIGyro((SPIPorts)m_port);
            gyro.SetRate(1.025);
            Assert.AreEqual(1.025, m_gyro.GetRate(), 0.0001);
            gyro.SetRate(-1.0835);
            Assert.AreEqual(-1.0835, m_gyro.GetRate(), 0.0001);
        }

        [Test]
        public void TestAnalogGyroGetAngle()
        {

            SimSPIGyro gyro = new SimSPIGyro((SPIPorts)m_port);
            gyro.SetPosition(1.025);
            Assert.AreEqual(1.025, m_gyro.GetAngle(), 0.0001);
            gyro.SetPosition(-1.0835);
            Assert.AreEqual(-1.0835, m_gyro.GetAngle(), 0.0001);
        }

        [Test]
        public void TestReset()
        {
            SimSPIGyro gyro = new SimSPIGyro((SPIPorts)m_port);
            gyro.SetPosition(1.025);
            Assert.AreEqual(1.025, m_gyro.GetAngle(), 0.0001);
            m_gyro.Reset();
            Assert.AreEqual(0.0, m_gyro.GetAngle(), 0.0001);
        }

        [Test]
        public void TestSmartDashboardType()
        {
            Assert.That(m_gyro.SmartDashboardType, Is.EqualTo("Gyro"));
        }
    }
}
