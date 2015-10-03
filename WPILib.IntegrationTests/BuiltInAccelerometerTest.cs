using NUnit.Framework;
using WPILib.IntegrationTests.Test;
using WPILib.Interfaces;

namespace WPILib.IntegrationTests
{
    [TestFixture(AccelerometerRange.k2G)]
    [TestFixture(AccelerometerRange.k4G)]
    [TestFixture(AccelerometerRange.k8G)]
    public class BuiltInAccelerometerTest : AbstractComsSetup
    {
        private static readonly double AccelerationTolerance = 0.1;
        private readonly BuiltInAccelerometer m_accelerometer;

        public BuiltInAccelerometerTest(AccelerometerRange range)
        {
            m_accelerometer = new BuiltInAccelerometer(range);
        }

        [TestFixtureSetUp]
        public static void WaitASecond()
        {
            Timer.Delay(0.1);
        }

        [Test]
        public void TestAccelerometer()
        {
            Assert.AreEqual(0.0, m_accelerometer.GetX(), AccelerationTolerance);
            Assert.AreEqual(0.0, m_accelerometer.GetY(), AccelerationTolerance);
            Assert.AreEqual(1.0, m_accelerometer.GetZ(), AccelerationTolerance);
        }
    }
}
