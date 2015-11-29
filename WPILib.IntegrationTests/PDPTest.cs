using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture(typeof(TalonMotorFixture), 0.0)]
    [TestFixture(typeof(VictorMotorFixture), 0.0)]
    [TestFixture(typeof(JaguarMotorFixture), 0.0)]
    public class PDPTest : AbstractComsSetup
    {
        private static PowerDistributionPanel pdp;
        private static MotorEncoderFixture fixture;

        private readonly double expectedStoppedCurrentDraw;

        [TestFixtureSetUp]
        public static void BeforeClass()
        {
            pdp = new PowerDistributionPanel();
        }

        [TestFixtureTearDown]
        public static void AfterClass()
        {
            pdp.Dispose();
            pdp = null;
            fixture.Teardown();
            fixture = null;
        }

        public PDPTest(Type type, double expectedCurrentDraw)
        {
            MotorEncoderFixture mef = (MotorEncoderFixture)Activator.CreateInstance(type);

            if (fixture != null && !fixture.Equals(mef))
            {
                fixture.Teardown();
            }
            fixture = mef;
            fixture.Setup();

            this.expectedStoppedCurrentDraw = expectedCurrentDraw;
        }
        [TearDown]
        public void After()
        {
            fixture.Reset();
        }

        [Test]
        public void CheckStoppedCurrentForSpeedController()
        {
            Timer.Delay(0.25);

            Assert.That(pdp.GetCurrent(fixture.GetPdpChannel()), Is.EqualTo(expectedStoppedCurrentDraw).Within(0.001));
        }

        [Test]
        public void CheckRunningCurrentForSpeedController()
        {
            fixture.GetMotor().Set(1.0);
            Timer.Delay(0.25);

            Assert.That(pdp.GetCurrent(fixture.GetPdpChannel()), Is.GreaterThan(expectedStoppedCurrentDraw));
        }

    }
}
