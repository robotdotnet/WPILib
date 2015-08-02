using System;
using System.Collections.Generic;
using System.Linq;
using NetworkTables.NetworkTables2.Type;
using NetworkTables.Tables;
using WPILib.Buttons;
using WPILib.Interfaces;

namespace WPILib.Commands
{
    /// <summary>
    /// The <see cref="Scheduler"/> is a singleton which holds the top-level running commands.
    /// </summary>
    /// <remarks>
    /// It is in charge of both calling the command's <see cref="Command.Run">Run()</see> method
    /// and to make sure that there are no two command with conflicting requirements running.
    /// <para/>If is fine if teams with to take control of the <see cref="Scheduler"/> themselves.
    /// All that needs to be done is to call <see cref="Scheduler.Instance">Scheduler.Instance.<see cref="Run"/></see>
    /// often to have <see cref="Command">Commands</see> function correctly. However, this is
    /// already done for you if you use the CommandBased Robot Template. 
    /// </remarks>
    /// <seealso cref="Command"/>
    public class Scheduler : INamedSendable
    {
        /// The Singleton Instance
        private static Scheduler s_instance = null;

        private HashSet<Subsystem> m_subsystems = new HashSet<Subsystem>();
        private HashSet<Command> m_commands = new HashSet<Command>();
        private List<Command> m_additions = new List<Command>();
        private List<ButtonScheduler> m_buttons = new List<ButtonScheduler>();

        private object m_buttonsLock = new object();
        private object m_additionsLock = new object();
        private static object s_instanceLock = new object();

        private bool m_adding;
        private bool m_enabled = true;

        private bool m_runningCommandsChanged = false;

        private Scheduler()
        {
            HLUsageReporting.ReportScheduler();
        }

        /// <summary>
        /// Returns the <see cref="Scheduler"/>, creating it if one does not exist.
        /// </summary>
        public static Scheduler Instance
        {
            get
            {
                lock (s_instanceLock)
                {
                    return s_instance ?? (s_instance = new Scheduler());
                }
            }
        }

        public void AddCommand(Command command)
        {
            lock (m_additionsLock)
            {
                if (command != null)
                    m_additions.Add(command);
            }
        }

        public void AddButton(ButtonScheduler button)
        {
            lock (m_buttons)
            {
                if (button != null)
                    m_buttons.Add(button);
            }
        }

        private void ProcessCommandAddition(Command command)
        {
            if (command == null)
                return;
            if (m_adding)
            {
                Console.Error.WriteLine("WARNING: Can not start command from cancel method.  Ignoring:" + command);
                return;
            }

            if (!m_commands.Contains(command))
            {
                if (command.GetRequirements().Any())
                {
                    if (command.GetRequirements().Any(s => s.GetCurrentCommand() != null && !s.GetCurrentCommand().Interruptible))
                    {
                        return;
                    } 
                }

                m_adding = true;

                foreach (var s in command.GetRequirements())
                {
                    if (s.GetCurrentCommand() != null)
                    {
                        s.GetCurrentCommand().Cancel();
                        Remove(s.GetCurrentCommand());
                    }
                    s.SetCurrentCommand(command);
                }

                m_adding = false;

                m_commands.Add(command);
                command.StartRunning();
                m_runningCommandsChanged = true;
            }
        }

        public void Run()
        {
            m_runningCommandsChanged = false;
            if (!m_enabled)
                return;
            lock (m_buttonsLock)
            {
                if (m_buttons != null)
                {
                    for (int i = m_buttons.Count - 1; i >= 0; i--)
                    {
                        m_buttons[i].Execute();
                    }
                }
            }

            m_runningCommandsChanged = false;

            foreach (var s in m_commands.Where(s => !s.Run()))
            {
                Remove(s);
                m_runningCommandsChanged = true;
            }

            lock (m_additionsLock)
            {
                foreach (var s in m_additions)
                {
                    ProcessCommandAddition(s);
                }
                m_additions.Clear();
            }

            foreach (var s in m_subsystems)
            {
                if (s.GetCurrentCommand() == null)
                {
                    ProcessCommandAddition(s.GetDefaultCommand());
                }
                s.ConfirmCommand();
            }

            UpdateTable();

        }

        public void RegisterSubsystem(Subsystem system)
        {
            if (system != null)
            {
                m_subsystems.Add(system);
            }
        }

        public void Remove(Command command)
        {
            if (command == null)
                return;
            if (!m_commands.Remove(command))
                return;

            foreach (var s in command.GetRequirements())
            {
                s.SetCurrentCommand(null);
            }
            command.Removed();
        }

        public void RemoveAll()
        {
            while (m_commands.Count > 0)
            {
                Remove(m_commands.First());
            }
        }

        public void Disable()
        {
            m_enabled = false;
        }

        public void Enable()
        {
            m_enabled = true;
        }

        private StringArray commands;
        private NumberArray ids, toCancel;

        ///<inheritdoc/>
        public string Name => "Scheduler";

        ///<inheritdoc/>
        public new string GetType()
        {
            return "Scheduler";
        }

        ///<inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            commands = new StringArray();
            ids = new NumberArray();
            toCancel = new NumberArray();

            Table.PutValue("Names", commands);
            Table.PutValue("Ids", ids);
            Table.PutValue("Cancel", toCancel);
        }

        private void UpdateTable()
        {
            if (Table == null) return;
            Table.RetrieveValue("Cancel", toCancel);
            if (toCancel.Size() > 0)
            {
                foreach (var command in m_commands)
                {
                    for (int i = 0; i < toCancel.Size(); i++)
                    {
                        if (command.GetHashCode() == toCancel.Get(i))
                        {
                            command.Cancel();
                        }
                    }
                }
                toCancel.SetSize(0);
                Table.PutValue("Cancel", toCancel);
            }

            if (m_runningCommandsChanged)
            {
                commands.SetSize(0);
                ids.SetSize(0);

                foreach (var command in m_commands)
                {
                    commands.Add(command.Name);
                    ids.Add(command.GetHashCode());
                }
                Table.PutValue("Names", commands);
                Table.PutValue("Ids", ids);
            }

        }

        ///<inheritdoc/>
        public ITable Table { get; private set; }

        ///<inheritdoc/>
        public string SmartDashboardType => "Scheduler";
    }
}
