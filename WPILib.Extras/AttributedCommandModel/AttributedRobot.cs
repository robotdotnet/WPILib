using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetworkTables;
using WPILib.Buttons;
using WPILib.Commands;

namespace WPILib.Extras.AttributedCommandModel
{
    public class AttributedRobot : IterativeRobot
    {
        private readonly ReflectionContext reflectionContext;

        private readonly List<Subsystem> subsystems = new List<Subsystem>();

        public ICollection<Subsystem> Subsystems => subsystems;

        private readonly List<Button> buttons = new List<Button>();

        public ICollection<Button> Buttons => buttons;

        private readonly MultiValueDictionary<MatchPhase, Command> phaseCommands = new MultiValueDictionary<MatchPhase, Command>();

        public IReadOnlyDictionary<MatchPhase, IReadOnlyCollection<Command>> PhaseCommands => phaseCommands;

        public AttributedRobot(ReflectionContext reflectionContext)
        {
            this.reflectionContext = reflectionContext;
        }

        public AttributedRobot()
            :this(null)
        {
        }

        public sealed override void RobotInit()
        {
            var assemblies = GetAssemblies();
            var types = assemblies.SelectMany(assembly =>
                {
                    // This is wrapped in a try-catch with catching NotSupportedException to prevent
                    // the program from crashing if a dynamic assembly is loaded in the AppDomain
                    try
                    {
                        return assembly.GetExportedTypes()
                            .Select(type => reflectionContext != null ? reflectionContext.MapType(type.GetTypeInfo())
                                                                      : type.GetTypeInfo());
                    }
                    catch (NotSupportedException)
                    {
                        return Enumerable.Empty<TypeInfo>();
                    }
                });
            var exportedSubsystems = types.Where(type => type.GetCustomAttributes<ExportSubsystemAttribute>().Any()
                                                                  && typeof(Subsystem).IsAssignableFrom(type));
            subsystems.AddRange(exportedSubsystems.SelectMany(type => EnumerateGeneratedSubsystems(type)));
            var exportedCommands = types.Where(type => type.GetCustomAttributes<RunCommandAttribute>().Any()
                                                                             && typeof(Command).IsAssignableFrom(type));
            foreach (var command in exportedCommands)
            {
                GenerateCommands(command);
            }
            RobotInitCore();
        }

        protected virtual void RobotInitCore()
        {

        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Select(assembly => reflectionContext != null ? reflectionContext.MapAssembly(assembly)
                : assembly);
        }

        private static IEnumerable<Subsystem> EnumerateGeneratedSubsystems(TypeInfo subsystemType)
        {
            foreach (var attr in subsystemType.GetCustomAttributes<ExportSubsystemAttribute>())
            {
                var subsystem = (Subsystem)Activator.CreateInstance(subsystemType);
                if (attr.DefaultCommandType != null && subsystem is Subsystem)
                {
                    var defaultCommandType = attr.DefaultCommandType;
                    if (!typeof(Command).IsAssignableFrom(defaultCommandType))
                    {
                        throw new IllegalUseOfCommandException("Default command type is not an attributed commmand.");
                    }
                    var defaultCommand = (Command)Activator.CreateInstance(defaultCommandType, subsystem);
                    if (!defaultCommand.DoesRequire(subsystem))
                    {
                    defaultCommand.Requires(subsystem);
                    }
                    (subsystem).SetDefaultCommand(defaultCommand);
                }
                yield return subsystem;
            }
        }

        private void GenerateCommands(TypeInfo commandType)
        {
            foreach (var attr in commandType.GetCustomAttributes<RunCommandAtPhaseStartAttribute>())
            {
                phaseCommands.Add(attr.Phase, (Command)Activator.CreateInstance(commandType));
            }
            foreach (var attr in commandType.GetCustomAttributes<RunCommandOnJoystickAttribute>())
            {
                var button = buttons.OfType<JoystickButton>().Where(btn => btn.Joystick is Joystick)
                    .FirstOrDefault(btn => (btn.Joystick as Joystick).Port == attr.ControllerId && btn.ButtonNumber == attr.ButtonId);
                if (button == null)
                {
                    buttons.Add(button = new JoystickButton(new Joystick(attr.ControllerId), attr.ButtonId));
                }
                AttachCommandToButton(commandType, button, attr.ButtonMethod);
            }
            foreach (var attr in commandType.GetCustomAttributes<RunCommandOnNetworkKeyAttribute>())
            {
                var button = buttons.OfType<NetworkButton>().FirstOrDefault(btn => btn.SourceTable == NetworkTable.GetTable(attr.TableName) && btn.Field == attr.Key);
                if(button == null)
                {
                    buttons.Add(button = new NetworkButton(attr.TableName, attr.Key));
                }
                AttachCommandToButton(commandType, button, attr.ButtonMethod);
            }
        }

        private static void AttachCommandToButton(Type commandType, Button button, ButtonMethod method)
        {
            switch (method)
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

        private void StartPhaseCommands(MatchPhase phase)
        {
            foreach (var command in PhaseCommands[phase])
            {
                command.Start();
            }
        }

        public sealed override void AutonomousInit()
        {
            StartPhaseCommands(MatchPhase.Autonomous);
            AutonomousInitCore();
        }

        protected virtual void AutonomousInitCore()
        { }

        public sealed override void AutonomousPeriodic()
        {
            Scheduler.Instance.Run();
            AutonomousPeriodicCore();
        }

        protected virtual void AutonomousPeriodicCore()
        { }

        public sealed override void TeleopInit()
        {
            StartPhaseCommands(MatchPhase.Teleoperated);
            TeleopInitCore();
        }

        protected virtual void TeleopInitCore()
        { }

        public sealed override void TeleopPeriodic()
        {
            Scheduler.Instance.Run();
            TeleopPeriodicCore();
        }

        protected virtual void TeleopPeriodicCore()
        { }

        public sealed override void DisabledInit()
        {
            StartPhaseCommands(MatchPhase.Disabled);
            DisabledInitCore();
        }

        protected virtual void DisabledInitCore()
        { }

        public sealed override void DisabledPeriodic()
        {
            Scheduler.Instance.Run();
            DisabledPeriodicCore();
        }

        protected virtual void DisabledPeriodicCore()
        { }

        public sealed override void TestInit()
        {
            StartPhaseCommands(MatchPhase.Test);
            TestInitCore();
        }

        protected virtual void TestInitCore()
        { }

        public sealed override void TestPeriodic()
        {
            Scheduler.Instance.Run();
            TestPeriodicCore();
        }

        protected virtual void TestPeriodicCore()
        { }

    }
}
