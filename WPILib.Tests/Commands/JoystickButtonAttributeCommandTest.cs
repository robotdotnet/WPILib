using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPILib.Buttons;
using WPILib.Commands;
using WPILib.Extras.AttributedCommandModel;
using HAL_Simulator;
using WPILib.Extras;

namespace WPILib.Tests.Commands
{
    [TestFixture]
    //[Ignore("Ignored because DriverStationHelper doesn't actuallly send data, so there is not a way with built in classes to verify the button registration")]
    public class JoystickButtonAttributeCommandTest : AbstractCommandTest
    {
        private static bool commandStarted;
        [RunCommandOnJoystick(0, 1, ButtonMethod.WhenPressed)]
        public class MockWhenPressed : MockCommand
        {
            public MockWhenPressed() { }
            protected override void Execute()
            {
                commandStarted = true;
                base.Execute();
            }
        }

        [SetUp]
        public void Setup()
        {
            commandStarted = false;
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
                Assert.IsFalse(commandStarted, "Command has not started");
                DriverStationHelper.SetJoystickButton(0, 1, true);
                Thread.Sleep(40);
                robot.TeleopPeriodic();
                robot.TeleopPeriodic();
                Assert.IsTrue(commandStarted, "Command has started");
            }
        }
    }
}
