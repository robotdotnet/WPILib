using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetworkTables;
using WPILib.Buttons;
using WPILib.Commands;
using System.Collections.ObjectModel;
using HAL;
using HAL.Base;
using WPILib.LiveWindows;
using static HAL.Base.HAL;

namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// The AttributedRobot class.  Derive from this class to use the Attributed Command Model
    /// </summary>
    public class AttributedRobot : RobotBase
    {
        private readonly ReflectionContext m_reflectionContext;

        private readonly List<KeyValuePair<Subsystem, string>> m_subsystems = new List<KeyValuePair<Subsystem, string>>();

        /// <summary>
        /// The subsystems created when the robot object was initialized.
        /// </summary>
        public ICollection<Subsystem> Subsystems => new ReadOnlyCollection<Subsystem>(m_subsystems.Select(pair => pair.Key).ToList());

        private readonly List<Button> m_buttons = new List<Button>();

        /// <summary>
        /// The Button objects created when the robot was initialized.
        /// </summary>
        public ICollection<Button> Buttons => m_buttons;

        private readonly IDictionary<MatchPhase, IList<Command>> m_phaseCommands = new Dictionary<MatchPhase, IList<Command>>();

        /// <summary>
        /// Commands sorted by the phase that they will start.
        /// </summary>
        public IReadOnlyDictionary<MatchPhase, IList<Command>> PhaseCommands
        {
            get
            {
                var dictionary = new ReadOnlyDictionary<MatchPhase, IList<Command>>(m_phaseCommands);
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
            m_reflectionContext = reflectionContext;
        }

        /// <summary>
        /// Constructs an <see cref="AttributedRobot"/> that will automatically load <see cref="Subsystem"/>s and <see cref="Command"/>s.
        /// </summary>
        public AttributedRobot()
            :this(null)
        {
            m_autonomousInitialized = false;
            m_disabledInitialized = false;
            m_teleopInitialized = false;
            m_testInitialized = false;
        }

        /// <summary>
        /// Initializes the <see cref="AttributedRobot"/> and loads and sets up all of the subsystems and commands as they are specified.
        /// </summary>
        internal void _RobotInit()
        {
            var assemblies = GetAssemblies();
            var types = assemblies.SelectMany(assembly =>
                {
                    // This is wrapped in a try-catch with catching NotSupportedException to prevent
                    // the program from crashing if a dynamic assembly is loaded in the AppDomain
                    try
                    {
                        return assembly.GetExportedTypes()
                            .Select(type => m_reflectionContext != null ? m_reflectionContext.MapType(type.GetTypeInfo())
                                                                      : type.GetTypeInfo());
                    }
                    catch (NotSupportedException)
                    {
                        return Enumerable.Empty<TypeInfo>();
                    }
                });
            var exportedSubsystems = types.Where(type => type.GetCustomAttributes<ExportSubsystemAttribute>().Any()
                                                                  && typeof(Subsystem).IsAssignableFrom(type));
            m_subsystems.AddRange(exportedSubsystems.SelectMany(type => EnumerateGeneratedSubsystems(type)));
            var exportedCommands = types.Where(type => type.GetCustomAttributes<RunCommandAttribute>().Any()
                                                                             && typeof(Command).IsAssignableFrom(type));
            foreach (var command in exportedCommands)
            {
                GenerateCommands(command);
            }
            RobotInit();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_RobotInit"/>.
        /// </summary>
        /// <remarks>
        /// Robot-wide initialization code should go here.
        /// Users should override this method for default Robot-wide initialiation which will be called
        /// when the robot is first powered on. It will be called exactly one time.
        /// <para></para>
        /// Warning: The Driver Station "Robot Code" light and FMS "Robot Ready" indicators will be off until
        /// <see cref="RobotInit"/> exits. Code in <see cref="RobotInit"/> that waits for enable will cause
        /// the robot to never indicate that the code is ready, causing the robot to be bypassed in a match.
        /// </remarks>
        public virtual void RobotInit()
        {

        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Select(assembly => m_reflectionContext != null ? m_reflectionContext.MapAssembly(assembly)
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
                if (!m_phaseCommands.ContainsKey(attr.Phase))
                    m_phaseCommands.Add(attr.Phase, new List<Command>());
                m_phaseCommands[attr.Phase].Add(CreateCommand(commandType));
            }
            foreach (var attr in commandType.GetCustomAttributes<RunCommandOnJoystickAttribute>())
            {
                var button = m_buttons.OfType<JoystickButton>().Where(btn => btn.Joystick is Joystick)
                    .FirstOrDefault(btn => (btn.Joystick as Joystick).Port == attr.ControllerId && btn.ButtonNumber == attr.ButtonId);
                if (button == null)
                {
                    m_buttons.Add(button = new JoystickButton(new Joystick(attr.ControllerId), attr.ButtonId));
                }
                AttachCommandToButton(commandType, button, attr.ButtonMethod);
            }
            foreach (var attr in commandType.GetCustomAttributes<RunCommandOnNetworkKeyAttribute>())
            {
                var button = m_buttons.OfType<NetworkButton>().FirstOrDefault(btn => btn.SourceTable == NetworkTable.GetTable(attr.TableName) && btn.Field == attr.Key);
                if(button == null)
                {
                    m_buttons.Add(button = new NetworkButton(attr.TableName, attr.Key));
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
                    requiredSubsystems.Add(m_subsystems.First(pair => pair.Key.GetType() == subsystemType).Key);
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
        internal void _AutonomousInit()
        {
            StartPhaseCommands(MatchPhase.Autonomous);
            AutonomousInit();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_AutonomousInit"/>.
        /// </summary>
        public virtual void AutonomousInit()
        { }

        /// <summary>
        /// Runs the <see cref="Scheduler"/> one step.
        /// </summary>
        internal void _AutonomousPeriodic()
        {
            Scheduler.Instance.Run();
            AutonomousPeriodic();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_AutonomousPeriodic"/>.
        /// </summary>
        public virtual void AutonomousPeriodic()
        { }

        /// <summary>
        /// Starts the commands that were specified to start in teleoperated mode.
        /// </summary>
        internal void _TeleopInit()
        {
            StartPhaseCommands(MatchPhase.Teleoperated);
            TeleopInit();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_TeleopInit"/>.
        /// </summary>
        public virtual void TeleopInit()
        { }

        /// <summary>
        /// Runs the <see cref="Scheduler"/> one step.
        /// </summary>
        internal void _TeleopPeriodic()
        {
            Scheduler.Instance.Run();
            TeleopPeriodic();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_TeleopPeriodic"/>.
        /// </summary>
        public virtual void TeleopPeriodic()
        { }

        /// <summary>
        /// Starts the commands that were specified to start in disabled mode.
        /// </summary>
        internal void _DisabledInit()
        {
            StartPhaseCommands(MatchPhase.Disabled);
            DisabledInit();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_DisabledInit"/>.
        /// </summary>
        public virtual void DisabledInit()
        { }

        /// <summary>
        /// Runs the <see cref="Scheduler"/> one step.
        /// </summary>
        internal void _DisabledPeriodic()
        {
            Scheduler.Instance.Run();
            DisabledPeriodic();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_DisabledPeriodic"/>.
        /// </summary>
        public virtual void DisabledPeriodic()
        { }

        /// <summary>
        /// Starts the commands that were specified to start in test mode.
        /// </summary>
        internal void _TestInit()
        {
            StartPhaseCommands(MatchPhase.Test);
            TestInit();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_TestInit"/>.
        /// </summary>
        public virtual void TestInit()
        { }

        /// <summary>
        /// Runs the <see cref="Scheduler"/> one step.
        /// </summary>
        internal void _TestPeriodic()
        {
            Scheduler.Instance.Run();
            TestPeriodic();
        }

        /// <summary>
        /// Override this method to add additional code that executes after <see cref="_TestPeriodic"/>.
        /// </summary>
        public virtual void TestPeriodic()
        { }



        private bool m_disabledInitialized;
        private bool m_autonomousInitialized;
        private bool m_teleopInitialized;
        private bool m_testInitialized;

        /// <summary>
        /// Provide an alternate "main loop" via startCompetition().
        /// </summary>
        public override void StartCompetition()
        {
            Report(ResourceType.kResourceType_Framework, Instances.kFramework_Iterative);

            _RobotInit();

            HALNetworkCommunicationObserveUserProgramStarting();

            LiveWindow.LiveWindow.SetEnabled(false);
            while (true)
            {
                //Console.WriteLine("RobotLoop");
                // Call the appropriate function depending upon the current robot mode
                if (IsDisabled)
                {
                    // call DisabledInit() if we are now just entering disabled mode from
                    // either a different mode or from power-on
                    if (!m_disabledInitialized)
                    {
                        LiveWindow.LiveWindow.SetEnabled(false);
                        _DisabledInit();
                        m_disabledInitialized = true;
                        // reset the initialization flags for the other modes
                        m_autonomousInitialized = false;
                        m_teleopInitialized = false;
                        m_testInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramDisabled();
                        _DisabledPeriodic();
                    }
                }
                else if (IsTest)
                {
                    // call TestInit() if we are now just entering test mode from either
                    // a different mode or from power-on
                    if (!m_testInitialized)
                    {
                        LiveWindow.LiveWindow.SetEnabled(true);
                        _TestInit();
                        m_testInitialized = true;
                        m_autonomousInitialized = false;
                        m_teleopInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramTest();
                        _TestPeriodic();
                    }
                }
                else if (IsAutonomous)
                {
                    // call Autonomous_Init() if this is the first time
                    // we've entered autonomous_mode
                    if (!m_autonomousInitialized)
                    {
                        LiveWindow.LiveWindow.SetEnabled(false);
                        // KBS NOTE: old code reset all PWMs and relays to "safe values"
                        // whenever entering autonomous mode, before calling
                        // "Autonomous_Init()"
                        _AutonomousInit();
                        m_autonomousInitialized = true;
                        m_testInitialized = false;
                        m_teleopInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramAutonomous();
                        _AutonomousPeriodic();
                    }
                }
                else
                {
                    // call Teleop_Init() if this is the first time
                    // we've entered teleop_mode
                    if (!m_teleopInitialized)
                    {
                        LiveWindow.LiveWindow.SetEnabled(false);
                        _TeleopInit();
                        m_teleopInitialized = true;
                        m_testInitialized = false;
                        m_autonomousInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        //HAL.NetworkCommunicationObserveUserProgramTeleop();
                        HALNetworkCommunicationObserveUserProgramTeleop();
                        _TeleopPeriodic();
                    }
                }
                m_ds.WaitForData();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private bool NextPeriodReady => m_ds.NewControlData;
    }
}
