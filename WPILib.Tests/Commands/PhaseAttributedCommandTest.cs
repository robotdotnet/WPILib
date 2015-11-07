using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Context;
using WPILib.Extras.AttributedCommandModel;

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
                robot._RobotInit();
                robot._AutonomousInit();
                robot._AutonomousPeriodic();
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
                robot._RobotInit();
                robot._TeleopInit();
                robot._TeleopPeriodic();
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
                robot._RobotInit();
                robot._DisabledInit();
                robot._DisabledPeriodic();
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
                robot._RobotInit();
                robot._TestInit();
                robot._TestPeriodic();
                foreach (var command in robot.PhaseCommands[MatchPhase.Test])
                {
                    Assert.IsTrue(command.IsRunning());
                }
            }
        }
    }
}
