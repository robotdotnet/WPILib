using System.Collections.Generic;
using NetworkTablesDotNet.Tables;

namespace WPILib.Commands
{
    public abstract class Subsystem : INamedSendable
    {
        private bool m_initializedDefaultCommand = false;
        private Command m_currentCommand;
        private bool m_currentCommandChanged;

        private Command m_defaultCommand;

        private static List<Subsystem> s_allSubsystems = new List<Subsystem>();

        public Subsystem(string name)
        {
            Name = name;
            Scheduler.GetInstance().RegisterSubsystem(this);
        }

        public Subsystem()
        {
            Name = GetType().Name.Substring(GetType().Name.LastIndexOf('.') + 1);
            Scheduler.GetInstance().RegisterSubsystem(this);
            m_currentCommandChanged = true;
        }

        protected abstract void InitDefaultCommand();

        protected void SetDefaultCommand(Command command)
        {
            if (command == null)
            {
                m_defaultCommand = null;
            }
            else
            {
                bool found = false;
                //var requirements = command.GetRequirements();

                foreach (var s in command.GetRequirements())
                {
                    if (s.Equals(this))
                        found = true;
                }
                if (!found)
                {
                    throw new IllegalUseOfCommandException("A default command must require the subsystem");
                }
                m_defaultCommand = command;
            }
        }

        internal Command GetDefaultCommand()
        {
            if (!m_initializedDefaultCommand)
            {
                m_initializedDefaultCommand = true;
                InitDefaultCommand();
            }
            return m_defaultCommand;
        }

        internal void SetCurrentCommand(Command command)
        {
            m_currentCommand = command;
            m_currentCommandChanged = true;
        }

        internal void ConfirmCommand()
        {
            if (m_currentCommandChanged)
            {
                //Add Table
                m_currentCommandChanged = false;
            }
        }

        public Command GetCurrentCommand()
        {
            return m_currentCommand;
        }

        public string Name { get; }

        private ITable m_table;

        public void InitTable(ITable subtable)
        {
            m_table = subtable;
            if (m_table != null)
            {
                if (m_defaultCommand != null)
                {
                    m_table.PutBoolean("hasDefault", true);
                    m_table.PutString("default", m_defaultCommand.Name);
                }
                else
                {
                    m_table.PutBoolean("hasDefault", false);
                }
                if (m_currentCommand != null)
                {
                    m_table.PutBoolean("hasCommand", true);
                    m_table.PutString("command", m_currentCommand.Name);
                }
                else
                {
                    m_table.PutBoolean("hasCommand", false);
                }
            }
        }

        public ITable Table => m_table;

        public string SmartDashboardType => "Subsystem";
    }
}
