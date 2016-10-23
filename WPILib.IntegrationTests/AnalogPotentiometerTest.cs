using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.MockHardware;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture]
    public class AnalogPotentiometerTest : AbstractComsSetup
    {
        private AnalogCrossConnectFixture m_analogIo;
        private FakePotentiometerSource m_potSource;
        private AnalogPotentiometer m_pot;

        private const double DoubleComparisonDelta = 2.0;

        [SetUp]
        public void Setup()
        {
            m_analogIo = TestBench.GetAnalogCrossConnectFixture();
            m_potSource = new FakePotentiometerSource(m_analogIo.GetOutput(), 360);
            m_pot = new AnalogPotentiometer(m_analogIo.GetInput(), 360.0, 0);
        }

        [TearDown]
        public void TearDown()
        {
            m_potSource.Reset();
            m_pot.Dispose();
            m_analogIo.Teardown();
        }

        [Test, Ignore("But in initial value needs to be fixed")]
        public void TestInitialSettings()
        {
            Assert.AreEqual(0, m_pot.Get(), DoubleComparisonDelta);
        }

        [Test]
        public void TestRangeValues()
        {
            for (double i = 0; i < 360.0; i = i + 1.0)
            {
                m_potSource.SetAngle(i);
                m_potSource.SetMaxVoltage(ControllerPower.GetVoltage5V());
                Timer.Delay(0.02);
                Assert.AreEqual(i, m_pot.Get(), DoubleComparisonDelta);
            }
        }
    }
}
