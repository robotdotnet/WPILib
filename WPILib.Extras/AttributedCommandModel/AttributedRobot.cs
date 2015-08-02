using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.Buttons;
using WPILib.Commands;


namespace WPILib.Extras.AttributedCommandModel
{
    public class AttributedRobot : IterativeRobot
    {
        private List<Subsystem> subsystems;

        public ICollection<Subsystem> Subsystems => subsystems;

        private List<Button> buttons;

        public ICollection<Button> Buttons => buttons;

        private Dictionary<MatchPhase, Command> phaseCommands;

        public IDictionary<MatchPhase, Command> PhaseCommands => phaseCommands;

        public override void RobotInit()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var types = assembly.GetExportedTypes();
            var exportedSubsystems = types.Where(type => type.CustomAttributes
                                                                .Any(attr => attr.AttributeType == typeof(ExportSubsystemAttribute))
                                                                &&
                                                                  typeof(Subsystem).IsAssignableFrom(type));
            subsystems = new List<Subsystem>();
            subsystems.AddRange(exportedSubsystems.SelectMany(type => GenerateSubsystems(type)));
        }

        private static IEnumerable<Subsystem> GenerateSubsystems(Type subsystemType)
        {
            foreach (var attr in subsystemType.CustomAttributes.Where(attr => attr.AttributeType == typeof(ExportSubsystemAttribute)))
            {
                var subsystem = (Subsystem)Activator.CreateInstance(subsystemType);
                if (attr.NamedArguments.Any(arg => arg.MemberName == nameof(ExportSubsystemAttribute.DefaultCommandType)) && subsystem is IAttributedSubsystem)
                {
                    var defaultCommandType = (Type)attr.NamedArguments
                                .First(arg => arg.MemberName == nameof(ExportSubsystemAttribute.DefaultCommandType)).TypedValue.Value;
                    if (!typeof(Command).IsAssignableFrom(defaultCommandType))
                    {
                        throw new IllegalUseOfCommandException("Default command type is not a commmand.");
                    }
                    var defaultCommand = (Command)Activator.CreateInstance(defaultCommandType);
                    ((IAttributedSubsystem)subsystem).InitDefaultCommand(defaultCommand);
                }
                yield return subsystem;
            }
        }

        private void GenerateCommands(Type commandType)
        {
            foreach (var attr in commandType.GetCustomAttributes(typeof(RunCommandAtPhaseStartAttribute), false).OfType<RunCommandAtPhaseStartAttribute>())
            {
                phaseCommands.Add(attr.Phase, (Command)Activator.CreateInstance(commandType));   
            }
            foreach (var attr in commandType.GetCustomAttributes(typeof(RunCommandOnJoystickAttribute), false).OfType<RunCommandOnJoystickAttribute>())
            {
                var button = buttons.OfType<JoystickButton>().Where(btn => btn.Joystick is Joystick)
                    .FirstOrDefault(btn => (btn.Joystick as Joystick).Port == attr.ControllerId && btn.ButtonNumber == attr.ButtonId);
                if(button == null)
                {
                    buttons.Add(button = new JoystickButton(new Joystick(attr.ControllerId), attr.ButtonId));
                }
                switch (attr.ButtonMethod)
                {
                    case ButtonMethod.WhenPressed:
                        button.WhenPressed((Command)Activator.CreateInstance(commandType));
                        break;
                    case ButtonMethod.WhenReleased:
                        button.WhenReleased((Command)Activator.CreateInstance(commandType));
                        break;
                    case ButtonMethod.WhileHeld:
                        button.WhileHeld((Command)Activator.CreateInstance(commandType));
                        break;
                    case ButtonMethod.ToggleWhenPressed:
                        button.ToggleWhenPressed((Command)Activator.CreateInstance(commandType));
                        break;
                    case ButtonMethod.CancelWhenPressed:
                        button.CancelWhenPressed((Command)Activator.CreateInstance(commandType));
                        break;
                    default:
                        throw new NotSupportedException("The button method specified is not supported.");
                }
            }
        }

        
        private void StartPhaseCommands(MatchPhase phase)
        {
            foreach (var command in PhaseCommands.Where(entry => entry.Key == phase))
            {
                command.Value.Start();
            }
        }

        public override void AutonomousInit()
        {
            StartPhaseCommands(MatchPhase.Autonomous);
        }

        public override void AutonomousPeriodic()
        {
            Scheduler.Instance.Run();
        }

        public override void TeleopInit()
        {
            StartPhaseCommands(MatchPhase.Teleoperated);
        }

        public override void TeleopPeriodic()
        {
            Scheduler.Instance.Run();
        }

        public override void DisabledInit()
        {
            StartPhaseCommands(MatchPhase.Disabled);
        }

        public override void DisabledPeriodic()
        {
            Scheduler.Instance.Run();
        }

        public override void TestInit()
        {
            StartPhaseCommands(MatchPhase.Test);
        }

        public override void TestPeriodic()
        {
            Scheduler.Instance.Run();
        }
    }
}
