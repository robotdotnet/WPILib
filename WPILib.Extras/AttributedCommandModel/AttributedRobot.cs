using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetworkTables;
using WPILib.Buttons;
using WPILib.Commands;
using System.Collections.ObjectModel;

namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// The AttributedRobot class.  Derive from this class to use the Attributed Command Model
    /// </summary>
    public class AttributedRobot : IterativeRobot
    {
        private readonly ReflectionContext reflectionContext;

        private readonly List<KeyValuePair<Subsystem, string>> subsystems = new List<KeyValuePair<Subsystem, string>>();

        /// <summary>
        /// The subsystems created when the robot object was initialized.
        /// </summary>
        public ICollection<Subsystem> Subsystems => new ReadOnlyCollection<Subsystem>(subsystems.Select(pair => pair.Key).ToList());

        private readonly List<Button> buttons = new List<Button>();

        /// <summary>
        /// The Button objects created when the robot was initialized.
        /// </summary>
        public ICollection<Button> Buttons => buttons;

        private readonly IDictionary<MatchPhase, IList<Command>> phaseCommands = new Dictionary<MatchPhase, IList<Command>>();

        /// <summary>
        /// Commands sorted by the phase that they will start.
        /// </summary>
        public IReadOnlyDictionary<MatchPhase, IList<Command>> PhaseCommands
        {
            get
            {
                var dictionary = new ReadOnlyDictionary<MatchPhase, IList<Command>>(phaseCommands);
                return dictionary;
            }
        }

        /// <summary>
        /// Creates an <see cref="AttributedRobot"/> with a <see cref="ReflectionContext"/> object to find types through.
        /// </summary>
        /// <param name="reflectionContext">The context to find types through.</param>
        /// <remarks>
        /// This constructor only needed when using commands from a library that you cannot edit the source code for.  Otherwise use the other constructor.
        /// </remarks>
        public AttributedRobot(ReflectionContext reflectionContext)
        {
            this.reflectionContext = reflectionContext;
        }

        /// <summary>
        /// Constructs an <see cref="AttributedRobot"/> that will automatically load <see cref="Subsystem"/>s and <see cref="Command"/>s.
        /// </summary>
        public AttributedRobot()
            :this(null)
        {
        }

        /// <summary>
        /// Initializes the <see cref="AttributedRobot"/> and loads and sets up all of the subsystems and commands as they are specified.
        /// </summary>
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

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="RobotInit"/>.
        /// </summary>
        protected virtual void RobotInitCore()
        {

        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Select(assembly => reflectionContext != null ? reflectionContext.MapAssembly(assembly)
                : assembly);
        }

        private static IEnumerable<KeyValuePair<Subsystem, string>> EnumerateGeneratedSubsystems(TypeInfo subsystemType)
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
                    subsystem.SetDefaultCommand(defaultCommand);
                }
                yield return new KeyValuePair<Subsystem, string>(subsystem, attr.Name);
            }
        }

        private void GenerateCommands(TypeInfo commandType)
        {
            foreach (var attr in commandType.GetCustomAttributes<RunCommandAtPhaseStartAttribute>())
            {
                if (!phaseCommands.ContainsKey(attr.Phase))
                    phaseCommands.Add(attr.Phase, new List<Command>());
                phaseCommands[attr.Phase].Add(CreateCommand(commandType));
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

        private void AttachCommandToButton(Type commandType, Button button, ButtonMethod method)
        {
            switch (method)
            {
                case ButtonMethod.WhenPressed:
                    button.WhenPressed(CreateCommand(commandType));
                    break;
                case ButtonMethod.WhenReleased:
                    button.WhenReleased(CreateCommand(commandType));
                    break;
                case ButtonMethod.WhileHeld:
                    button.WhileHeld(CreateCommand(commandType));
                    break;
                case ButtonMethod.ToggleWhenPressed:
                    button.ToggleWhenPressed(CreateCommand(commandType));
                    break;
                case ButtonMethod.CancelWhenPressed:
                    button.CancelWhenPressed(CreateCommand(commandType));
                    break;
                default:
                    throw new NotSupportedException("The button method specified is not supported.");
            }
        }

        private Command CreateCommand(Type commandType)
        {
            var constructors = commandType.GetConstructors();
            //If command has a constructor that takes parameters, that constructor is passed subsystems of the specified types.
            foreach(var constructor in constructors.Where(constr => constr.GetParameters().Length > 0))
            {
                if (constructor.GetParameters().Any(param => !typeof(Subsystem).IsAssignableFrom(param.ParameterType)))
                    continue;
                var requiredSubsystems = new List<Subsystem>();
                foreach (var subsystemParam in constructor.GetParameters())
                {
                    var subsystemType = subsystemParam.ParameterType;
                    var name = subsystemParam.GetCustomAttribute<ImportSubsystemAttribute>()?.Name;
                    requiredSubsystems.Add(subsystems.First(pair => pair.Key.GetType() == subsystemType).Key);
                }
                return (Command)constructor.Invoke(requiredSubsystems.ToArray());
            }
            return (Command)Activator.CreateInstance(commandType);
        }

        private void StartPhaseCommands(MatchPhase phase)
        {
            if (!PhaseCommands.ContainsKey(phase)) return;
            foreach (var command in PhaseCommands[phase])
            {
                command.Start();
            }
        }

        /// <summary>
        /// Starts the commands that were specified to start in autonomous mode.
        /// </summary>
        public sealed override void AutonomousInit()
        {
            StartPhaseCommands(MatchPhase.Autonomous);
            AutonomousInitCore();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="AutonomousInit"/>.
        /// </summary>
        protected virtual void AutonomousInitCore()
        { }

        /// <summary>
        /// Runs the <see cref="Scheduler"/> one step.
        /// </summary>
        public sealed override void AutonomousPeriodic()
        {
            Scheduler.Instance.Run();
            AutonomousPeriodicCore();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="AutonomousPeriodic"/>.
        /// </summary>
        protected virtual void AutonomousPeriodicCore()
        { }

        /// <summary>
        /// Starts the commands that were specified to start in teleoperated mode.
        /// </summary>
        public sealed override void TeleopInit()
        {
            StartPhaseCommands(MatchPhase.Teleoperated);
            TeleopInitCore();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="TeleopInit"/>.
        /// </summary>
        protected virtual void TeleopInitCore()
        { }

        /// <summary>
        /// Runs the <see cref="Scheduler"/> one step.
        /// </summary>
        public sealed override void TeleopPeriodic()
        {
            Scheduler.Instance.Run();
            TeleopPeriodicCore();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="TeleopPeriodic"/>.
        /// </summary>
        protected virtual void TeleopPeriodicCore()
        { }

        /// <summary>
        /// Starts the commands that were specified to start in disabled mode.
        /// </summary>
        public sealed override void DisabledInit()
        {
            StartPhaseCommands(MatchPhase.Disabled);
            DisabledInitCore();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="DisabledInit"/>.
        /// </summary>
        protected virtual void DisabledInitCore()
        { }

        /// <summary>
        /// Runs the <see cref="Scheduler"/> one step.
        /// </summary>
        public sealed override void DisabledPeriodic()
        {
            Scheduler.Instance.Run();
            DisabledPeriodicCore();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="DisabledPeriodic"/>.
        /// </summary>
        protected virtual void DisabledPeriodicCore()
        { }

        /// <summary>
        /// Starts the commands that were specified to start in test mode.
        /// </summary>
        public sealed override void TestInit()
        {
            StartPhaseCommands(MatchPhase.Test);
            TestInitCore();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="TestInit"/>.
        /// </summary>
        protected virtual void TestInitCore()
        { }

        /// <summary>
        /// Runs the <see cref="Scheduler"/> one step.
        /// </summary>
        public sealed override void TestPeriodic()
        {
            Scheduler.Instance.Run();
            TestPeriodicCore();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="TestPeriodic"/>.
        /// </summary>
        protected virtual void TestPeriodicCore()
        { }

    }
}
