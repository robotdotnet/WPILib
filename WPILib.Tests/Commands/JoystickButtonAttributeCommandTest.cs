using NUnit.Framework;
using System.Threading;
using WPILib.Extras.AttributedCommandModel;
using HAL_Simulator;

// ReSharper disable EmptyConstructor

namespace WPILib.Tests.Commands
{
    [TestFixture]
    public class JoystickButtonAttributeCommandTest : AbstractCommandTest
    {
        private static bool s_commandStarted;
        [RunCommandOnJoystick(0, 1, ButtonMethod.WhenPressed)]
        public class MockWhenPressed : MockCommand
        {
            public MockWhenPressed() { }
            protected override void Execute()
            {
                s_commandStarted = true;
                base.Execute();
            }
        }

        [SetUp]
        public void Setup()
        {
            s_commandStarted = false;
        }

        [Test]
        public void WhenPressed()
        {
            using (var robot = new AttributedRobot())
           {
                DriverStationHelper.SetJoystickButton(0, 1, false);
                Thread.Sleep(40);
                robot.RobotInit();
                robot.TeleopInit();
                robot.TeleopPeriodic();
                Assert.IsFalse(s_commandStarted, "Command has not started");
                DriverStationHelper.SetJoystickButton(0, 1, true);
                Thread.Sleep(40);
                robot.TeleopPeriodic();
                robot.TeleopPeriodic();
                Assert.IsTrue(s_commandStarted, "Command has started");
            }
        }
    }
}
