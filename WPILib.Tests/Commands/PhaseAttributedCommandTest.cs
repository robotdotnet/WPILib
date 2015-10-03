using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Context;
using System.Text;
using System.Threading.Tasks;
using WPILib.Commands;
using WPILib.Extras.AttributedCommandModel;
using System.Reflection;

namespace WPILib.Tests.Commands
{
    [TestFixture]
    public class PhaseAttributedCommandTest : AbstractCommandTest
    {
        class TestReflectionContext : CustomReflectionContext
        {
            protected override IEnumerable<object> GetCustomAttributes(MemberInfo member, IEnumerable<object> declaredAttributes)
            {
                if(member == typeof(MockCommand))
                {
                    yield return new RunCommandAtPhaseStartAttribute(MatchPhase.Autonomous);
                    yield return new RunCommandAtPhaseStartAttribute(MatchPhase.Teleoperated);
                    yield return new RunCommandAtPhaseStartAttribute(MatchPhase.Disabled);
                    yield return new RunCommandAtPhaseStartAttribute(MatchPhase.Test);
                }
                foreach (var attr in base.GetCustomAttributes(member, declaredAttributes)) yield return attr;
            }
        }

        [Test]
        public void CommandsRegisteredForAutoStartOnAuto()
        {
            using (AttributedRobot robot = new AttributedRobot(new TestReflectionContext()))
            {
                robot.RobotInit();
                robot.AutonomousInit();
                robot.AutonomousPeriodic();
                foreach (var command in robot.PhaseCommands[MatchPhase.Autonomous])
                {
                    Assert.IsTrue(command.IsRunning());
                }
            }
        }

        [Test]
        public void CommandsRegisteredForTeleopStartOnTeleop()
        {
            using (AttributedRobot robot = new AttributedRobot(new TestReflectionContext()))
            {
                robot.RobotInit();
                robot.TeleopInit();
                robot.TeleopPeriodic();
                foreach (var command in robot.PhaseCommands[MatchPhase.Teleoperated])
                {
                    Assert.IsTrue(command.IsRunning());
                }
            }
        }

        [Test]
        public void CommandsRegisteredForDisabledStartOnDisabled()
        {
            using (AttributedRobot robot = new AttributedRobot(new TestReflectionContext()))
            {
                robot.RobotInit();
                robot.DisabledInit();
                robot.DisabledPeriodic();
                foreach (var command in robot.PhaseCommands[MatchPhase.Disabled])
                {
                    Assert.IsTrue(command.IsRunning());
                }
            }
        }

        [Test]
        public void CommandsRegisteredForTestStartOnTest()
        {
            using (AttributedRobot robot = new AttributedRobot(new TestReflectionContext()))
            {
                robot.RobotInit();
                robot.TestInit();
                robot.TestPeriodic();
                foreach (var command in robot.PhaseCommands[MatchPhase.Test])
                {
                    Assert.IsTrue(command.IsRunning());
                }
            }
        }
    }
}
