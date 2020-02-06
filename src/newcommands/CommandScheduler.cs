using Hal;
using NetworkTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WPILib;
using WPILib.LiveWindow;
using WPILib.SmartDashboard;

namespace WPILib2.Commands
{
    public sealed class CommandScheduler : ISendable, IDisposable
    {
        private static readonly Lazy<CommandScheduler> instance = new Lazy<CommandScheduler>(() => new CommandScheduler(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static CommandScheduler Instance => instance.Value;

        private readonly Dictionary<ICommand, CommandState> m_scheduledCommands = new Dictionary<ICommand, CommandState>();

        private readonly Dictionary<ISubsystem, ICommand> m_requirements = new Dictionary<ISubsystem, ICommand>();

        private readonly Dictionary<ISubsystem, ICommand?> m_subsystems = new Dictionary<ISubsystem, ICommand?>();

        private readonly HashSet<Action> m_buttons = new HashSet<Action>();

        private bool m_disabled;

        private bool m_inRunLoop;
        private readonly Dictionary<ICommand, bool> m_toSchedule = new Dictionary<ICommand, bool>();
        private readonly List<ICommand> m_toCancel = new List<ICommand>();
        private readonly List<ICommand> m_toRemoveFromRunning = new List<ICommand>();

        internal CommandScheduler()
        {
            UsageReporting.Report(ResourceType.Command, Instances.Command2_Scheduler);
            SendableRegistry.Instance.AddLW(this, "Scheduler");
            LiveWindow.EnabledListener = () =>
            {
                Disable();
                CancelAll();
            };
            LiveWindow.DisabledListener = () =>
            {
                Enable();
            };
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            LiveWindow.EnabledListener = null;
            LiveWindow.DisabledListener = null;
        }

        public void AddButton(Action button)
        {
            m_buttons.Add(button);
        }

        public void ClearButtons()
        {
            m_buttons.Clear();
        }

        private void InitCommand(ICommand command, bool interruptible, HashSet<ISubsystem> requirements)
        {
            command.Initialize();
            var scheduledCommand = new CommandState(interruptible);
            m_scheduledCommands.Add(command, scheduledCommand);
            OnCommandInitialize?.Invoke(command);
            foreach (var requirement in requirements)
            {
                m_requirements.Add(requirement, command);
            }
        }

        private void Schedule(bool interruptible, ICommand command)
        {
            if (m_inRunLoop)
            {
                m_toSchedule.Add(command, interruptible);
                return;
            }

            if (m_disabled || (RobotState.IsDisabled && !command.RunsWhenDisabled))
            {
                return;
            }

            var requirements = command.Requirements;

            var takenRequirements = requirements.Intersect(m_requirements.Keys).ToList();

            if (takenRequirements.Count == 0)
            {
                InitCommand(command, interruptible, requirements);
            }
            else
            {
                // check if requirements all have interruptable
                foreach (var requirement in takenRequirements)
                {
                    if (!m_scheduledCommands[m_requirements[requirement]].IsInterruptible)
                    {
                        return;
                    }
                }
                foreach (var requirement in takenRequirements)
                {
                    Cancel(m_requirements[requirement]);
                }
                InitCommand(command, interruptible, requirements);
            }
        }

        public void Schedule(bool interruptible, params ICommand[] commands)
        {
            foreach (var command in commands)
            {
                Schedule(interruptible, command);
            }
        }

        public void Schedule(params ICommand[] commands)
        {
            Schedule(true, commands);
        }

        public void Run()
        {
            if (m_disabled)
            {
                return;
            }

            foreach (var subystem in m_subsystems.Keys)
            {
                subystem.Periodic();
            }

            foreach (var button in m_buttons)
            {
                button();
            }

            m_inRunLoop = true;

            m_toRemoveFromRunning.Clear();

            foreach (var command in m_scheduledCommands.Keys)
            {
                if (!command.RunsWhenDisabled && RobotState.IsDisabled)
                {
                    command.End(true);
                    OnCommandInterrupt?.Invoke(command);

                    foreach (var requirement in command.Requirements)
                    {
                        m_requirements.Remove(requirement);
                    }
                    m_toRemoveFromRunning.Add(command);
                    continue;
                }

                command.Execute();

                OnCommandExecute?.Invoke(command);

                if (command.IsFinished)
                {
                    command.End(false);
                    OnCommandFinish?.Invoke(command);

                    foreach (var requirement in command.Requirements)
                    {
                        m_requirements.Remove(requirement);
                    }
                    m_toRemoveFromRunning.Add(command);
                }
            }

            foreach (var item in m_toRemoveFromRunning)
            {
                m_scheduledCommands.Remove(item);
            }

            m_inRunLoop = false;

            foreach (var commandInterruptible in m_toSchedule)
            {
                Schedule(commandInterruptible.Value, commandInterruptible.Key);
            }

            foreach (var command in m_toCancel)
            {
                Cancel(command);
            }

            m_toSchedule.Clear();
            m_toCancel.Clear();

            foreach (var subsystemCommand in m_subsystems)
            {
                if (!m_requirements.ContainsKey(subsystemCommand.Key) && subsystemCommand.Value != null)
                {
                    Schedule(subsystemCommand.Value);
                }
            }
        }

        public void RegisterSubsystem(params ISubsystem[] subsystems)
        {
            foreach (var subsystem in subsystems)
            {
                m_subsystems.Add(subsystem, null);
            }
        }

        public void UnregisterSubsystem(params ISubsystem[] subsystems)
        {
            foreach (var s in subsystems)
            {
                m_subsystems.Remove(s);
            }
        }

        public void SetDefaultCommand(ISubsystem subsystem, ICommand defaultCommand)
        {
            if (!defaultCommand.Requirements.Contains(subsystem))
            {
                throw new ArgumentOutOfRangeException("Default commands must require their subsystem!");
            }

            if (defaultCommand.IsFinished)
            {
                throw new ArgumentOutOfRangeException("Default commands should not end");
            }

            m_subsystems.Add(subsystem, defaultCommand);
        }

        public ICommand? GetDefaultCommand(ISubsystem subsystem)
        {
            if (m_subsystems.TryGetValue(subsystem, out var command))
            {
                return command;
            }
            return null;
        }

        public void Cancel(params ICommand[] commands)
        {
            if (m_inRunLoop)
            {
                m_toCancel.AddRange(commands);
                return;
            }

            foreach (var command in commands)
            {
                if (!m_scheduledCommands.ContainsKey(command))
                {
                    continue;
                }

                command.End(true);

                OnCommandInterrupt?.Invoke(command);

                m_scheduledCommands.Remove(command);
                foreach (var requirement in command.Requirements)
                {
                    m_requirements.Remove(requirement);
                }
            }
        }

        public void CancelAll()
        {
            foreach (var command in m_scheduledCommands.Keys)
            {
                Cancel(command);
            }
        }

        public TimeSpan TimeSinceScheduled(ICommand command)
        {
            if (m_scheduledCommands.TryGetValue(command, out var state))
            {
                return state.TimeSinceInitialized;
            }
            return Timeout.InfiniteTimeSpan;
        }

        public bool IsScheduled(params ICommand[] commands)
        {
            foreach (var command in commands)
            {
                if (!m_scheduledCommands.ContainsKey(command))
                {
                    return false;
                }
            }
            return true;
        }

        public ICommand? Requiring(ISubsystem subsystem)
        {
            if (m_requirements.TryGetValue(subsystem, out var command))
            {
                return command;
            }
            return null;
        }

        public void Disable()
        {
            m_disabled = true;
        }

        public void Enable()
        {
            m_disabled = false;
        }

        public event Action<ICommand>? OnCommandInitialize;
        public event Action<ICommand>? OnCommandExecute;
        public event Action<ICommand>? OnCommandInterrupt;
        public event Action<ICommand>? OnCommandFinish;

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Scheduler";
            NetworkTableEntry namesEntry = builder.GetEntry("Names");
            NetworkTableEntry idsEntry = builder.GetEntry("Ids");
            NetworkTableEntry cancelEntry = builder.GetEntry("Cancel");

            builder.UpdateTable = () =>
            {
                var ids = new Dictionary<double, ICommand>();

                foreach (var command in m_scheduledCommands.Keys)
                {
                    ids.Add(command.GetHashCode(), command);
                }

                var toCancel = cancelEntry.GetDoubleArray(ReadOnlySpan<double>.Empty);
                foreach (var hash in toCancel)
                {
                    if (ids.TryGetValue(hash, out var command))
                    {
                        Cancel(command);
                        ids.Remove(hash);
                    }
                    cancelEntry.SetDoubleArray(ReadOnlySpan<double>.Empty);
                }

                var names = new List<string>();

                foreach (var command in ids.Values)
                {
                    names.Add(command.Name);
                }

                namesEntry.SetStringArray(names.ToArray());
                idsEntry.SetDoubleArray(ids.Keys.ToArray());
            };
        }
    }
}
