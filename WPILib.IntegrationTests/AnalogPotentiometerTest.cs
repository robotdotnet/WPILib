using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.MockHardware;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    public class AnalogPotentiometerTest : AbstractComsSetup
    {
        private AnalogCrossConnectFixture analogIO;
        private FakePotentiometerSource potSource;
        private AnalogPotentiometer pot;

        private const double DOUBLE_COMPARISON_DELTA = 2.0;

        [SetUp]
        public void Setup()
        {
            analogIO = TestBench.GetAnalogCrossConnectFixture();
            potSource = new FakePotentiometerSource(analogIO.GetOutput(), 360);
            pot = new AnalogPotentiometer(analogIO.GetInput(), 360.0, 0);
        }

        [TearDown]
        public void TearDown()
        {
            potSource.Reset();
            pot.Dispose();
            analogIO.Teardown();
        }

        [Test]
        public void TestInitialSettings()
        {
            Assert.AreEqual(0, pot.Get(), DOUBLE_COMPARISON_DELTA);
        }

        [Test]
        public void TestRangeValues()
        {
            for (double i = 0; i < 360.0; i = i + 1.0)
            {
                potSource.SetAngle(i);
                potSource.SetMaxVoltage(ControllerPower.GetVoltage5V());
                Timer.Delay(0.02);
                Assert.AreEqual(i, pot.Get(), DOUBLE_COMPARISON_DELTA);
            }
        }
    }
}
