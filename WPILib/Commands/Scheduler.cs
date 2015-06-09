using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using HAL_Base;
using WPILib.Buttons;
using NetworkTablesDotNet.Tables;

namespace WPILib.Commands
{
    public class Scheduler : INamedSendable
    {
        private static Scheduler s_instance = null;

        private HashSet<Subsystem> m_subsystems = new HashSet<Subsystem>();
        private HashSet<Command> m_commands = new HashSet<Command>();
        private List<Command> m_additions = new List<Command>();
        private List<ButtonScheduler> m_buttons = new List<ButtonScheduler>();

        private object m_buttonsLock = new object();
        private object m_additionsLock = new object();

        private bool m_adding;
        private bool m_enabled;

        private ITable m_table;

        private bool m_runningCommandsChanged;

        private Scheduler()
        {
            HLUsageReporting.ReportScheduler();

            m_table = null;
            m_enabled = true;
            m_runningCommandsChanged = false;
        }

        public static Scheduler GetInstance()
        {
            if (s_instance == null)
                s_instance = new Scheduler();
            return s_instance;
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
                if (command.GetRequirements().Any(s => s.GetCurrentCommand() != null && !s.GetCurrentCommand().IsInterruptible()))
                {
                    return;
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

            foreach (var s in m_commands)
            {
                if (!s.Run())
                {
                    Remove(s);
                    m_runningCommandsChanged = true;
                }
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

        public void ResetAll()
        {
            RemoveAll();
            m_subsystems.Clear();
            m_buttons.Clear();
            m_additions.Clear();
            m_commands.Clear();
            m_table = null;
        }


        public string Name
        {
            get { return "Scheduler"; }
        }

        public new string GetType()
        {
            return "Scheduler";
        }

        public void InitTable(ITable subtable)
        {
            m_table = subtable;
        }

        public ITable Table
        {
            get { return m_table; }
        }

        public string SmartDashboardType
        {
            get { return "Scheduler"; }
        }
    }
}
