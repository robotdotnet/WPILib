using System;
using System.Collections.Generic;
using System.Linq;
using NetworkTables.Tables;
using WPILib.Buttons;
using WPILib.Interfaces;

namespace WPILib.Commands
{
    /// <summary>
    /// The <see cref="Scheduler"/> is a singleton which holds the top-level running m_commands.
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

        private readonly HashSet<Subsystem> m_subsystems = new HashSet<Subsystem>();
        private readonly Dictionary<Command, LinkedListElement> m_commandTable = new Dictionary<Command, LinkedListElement>();
        private readonly List<Command> m_additions = new List<Command>();
        private List<ButtonScheduler> m_buttons = new List<ButtonScheduler>();

        private static readonly object s_instanceLock = new object();

        private bool m_adding;
        private bool m_enabled = true;

        private bool m_runningCommandsChanged = false;

        private LinkedListElement m_firstCommand;
        private LinkedListElement m_lastCommand;

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
            if (command != null)
            {
                m_additions.Add(command);
            }
        }

        public void AddButton(ButtonScheduler button)
        {
            if (m_buttons == null)
            {
                m_buttons = new List<ButtonScheduler>();
            }
            m_buttons.Add(button);
        }

        private void _Add(Command command)
        {
            if (command == null)
            {
                return;
            }
            if (m_adding)
            {
                Console.Error.WriteLine($"WARNING: Cannot start command from cancel method. Ignoring: {command}");
            }
            if (!m_commandTable.ContainsKey(command))
            {
                IEnumerable<Subsystem> requirements = command.GetRequirements();
                if (requirements.Any(subsystem => subsystem.GetCurrentCommand() != null && !subsystem.GetCurrentCommand().Interruptible))
                {
                    return;
                }

                m_adding = true;
                requirements = command.GetRequirements();
                foreach (var subsystem in requirements)
                {
                    if (subsystem.GetCurrentCommand() != null)
                    {
                        subsystem.GetCurrentCommand().Cancel();
                        Remove(subsystem.GetCurrentCommand());
                    }
                    subsystem.SetCurrentCommand(command);
                }
                m_adding = false;

                LinkedListElement element = new LinkedListElement();
                element.SetData(command);
                if (m_firstCommand == null)
                {
                    m_firstCommand = m_lastCommand = element;
                }
                else
                {
                    m_lastCommand.Add(element);
                    m_lastCommand = element;
                }
                m_commandTable.Add(command, element);
                m_runningCommandsChanged = true;
                command.StartRunning();
            }
        }

        public void Run()
        {
            m_runningCommandsChanged = false;

            if (!m_enabled)
                return;
            if (m_buttons != null)
            {
                for (int i = m_buttons.Count - 1; i >= 0; i--)
                {
                    m_buttons[i].Execute();
                }
            }

            LinkedListElement e = m_firstCommand;
            while (e != null)
            {
                Command c = e.GetData();
                e = e.GetNext();
                if (!c.Run())
                {
                    Remove(c);
                    m_runningCommandsChanged = true;
                }
            }

            foreach (Command t in m_additions)
            {
                _Add(t);
            }

            m_additions.Clear();

            foreach (var subsystem in m_subsystems)
            {
                if (subsystem.GetCurrentCommand() == null)
                {
                    _Add(subsystem.GetDefaultCommand());
                }
                subsystem.ConfirmCommand();
            }
            UpdateTable();
        }

        internal void RegisterSubsystem(Subsystem system)
        {
            if (system != null)
            {
                m_subsystems.Add(system);
            }
        }

        internal void Remove(Command command)
        {
            if (command == null || !m_commandTable.ContainsKey(command))
            {
                return;
            }

            LinkedListElement e = m_commandTable[command];
            m_commandTable.Remove(command);

            if (e.Equals(m_lastCommand))
            {
                m_lastCommand = e.GetPrevious();
            }
            if (e.Equals(m_firstCommand))
            {
                m_firstCommand = e.GetNext();
            }
            e.Remove();

            var requirements = command.GetRequirements();
            foreach (var requirement in requirements)
            {
                requirement.SetCurrentCommand(null);
            }

            command.Removed();
        }

        public void RemoveAll()
        {
            while (m_firstCommand != null)
            {
                Remove(m_firstCommand.GetData());
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

        ///<inheritdoc/>
        public string Name => "Scheduler";

        ///<inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;

            Table.PutStringArray("Names", new string[0]);
            Table.PutNumberArray("Ids", new double[0]);
            Table.PutNumberArray("Cancel", new double[0]);
        }

        private void UpdateTable()
        {
            if (Table == null) return;
            double[] toCancel = Table.GetNumberArray("Cancel", new double[0]);
            if (toCancel.Length > 0)
            {
                for (LinkedListElement e = m_firstCommand; e != null; e = e.GetNext())
                {
                    for (int i = 0; i < toCancel.Length; i++)
                    {
                        if (e.GetData().GetHashCode() == toCancel[i])
                        {
                            e.GetData().Cancel();
                        }
                    }
                }
                Table.PutNumberArray("Cancel", new double[0]);
            }

            if (m_runningCommandsChanged)
            {
                int n = 0;
                for (LinkedListElement e = m_firstCommand; e != null; e = e.GetNext())
                {
                    n++;
                }

                string[] commands = new string[n];
                double[] ids = new double[n];

                n = 0;
                for (LinkedListElement e = m_firstCommand; e != null; e = e.GetNext())
                {
                    commands[n] = e.GetData().Name;
                    ids[n] = e.GetNext().GetHashCode();
                    n++;
                }

                Table.PutStringArray("Names", commands);
                Table.PutNumberArray("Ids", ids);
            }
        }

        ///<inheritdoc/>
        public ITable Table { get; private set; }

        ///<inheritdoc/>
        public string SmartDashboardType => "Scheduler";
    }
}
