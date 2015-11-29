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
    [TestFixture(typeof(TalonMotorFixture))]
    [TestFixture(typeof(VictorMotorFixture))]
    [TestFixture(typeof(JaguarMotorFixture))]
    public class MotorInvertingTest : AbstractComsSetup
    {
        static MotorEncoderFixture fixture = null;
        private readonly double motorSpeed = 0.35;
        private readonly double delayTime = 0.3;

        public MotorInvertingTest(Type type)
        {
            if (RobotBase.IsSimulation)
            {
                return;
            }
            MotorEncoderFixture mef = (MotorEncoderFixture)Activator.CreateInstance(type);

            if (fixture != null && !fixture.Equals(mef))
            {
                fixture.Teardown();
            }
            fixture = mef;
            fixture.Setup();
        }

        [TestFixtureSetUp]
        public static void SetUpBeforeClass()
        {
            //TODO: Get mock encoders working in sim
            if (RobotBase.IsSimulation)
            {
                Assert.Ignore();
            }
        }

        [SetUp]
        public void SetUp()
        {
            fixture.Reset();
        }

        [TestFixtureTearDown]
        public static void FixtureTearDown()
        {
            if (RobotBase.IsSimulation)
            {
                return;
            }
            fixture.GetMotor().Inverted = false;
            fixture.Teardown();
        }

        [Test]
        public void TestInvertingPositive()
        {
            fixture.GetMotor().Inverted = true;
            fixture.GetMotor().Set(motorSpeed);
            Timer.Delay(delayTime);
        }
    }
}
