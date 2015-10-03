using System.Collections.Generic;
using NetworkTables.Tables;
using WPILib.Interfaces;

namespace WPILib.Commands
{
    /// <summary>
    /// This class defines a major component of the robot.
    /// </summary>
    /// <remarks>A good example of a subsystem is the drivebase, or a claw if the robot has one.
    /// <para> Subsystems are used within the command system as requirements for <see cref="Command"/>.
    /// Only one command which requires a subsystem can run at a time. Also, subsystems can have default commands
    /// which are started if there is no command running which requires this subsystem.</para></remarks>
    public abstract class Subsystem : INamedSendable
    {
        private bool m_initializedDefaultCommand = false;
        private Command m_currentCommand;
        private bool m_currentCommandChanged;

        private Command m_defaultCommand;

        private static List<Subsystem> s_allSubsystems = new List<Subsystem>();

        /// <summary>
        /// Creates a subsystem with the given name.
        /// </summary>
        /// <param name="name">The name of the subsystem.</param>
        protected Subsystem(string name)
        {
            Name = name;
            Scheduler.Instance.RegisterSubsystem(this);
        }

        /// <summary>
        /// Creates a subsystem. This will set the name to the name of the class.
        /// </summary>
        protected Subsystem()
        {
            Name = GetType().Name.Substring(GetType().Name.LastIndexOf('.') + 1);
            Scheduler.Instance.RegisterSubsystem(this);
            m_currentCommandChanged = true;
        }

        /// <summary>
        /// Initialize the default command for a subsystem.
        /// </summary>
        /// <remarks>By default subsystems have no default command, but if they do,
        /// the default command is set with this method. It is called on all Subsystems
        /// by CommandBase in the users program after all the Subsystems are created.</remarks>
        protected abstract void InitDefaultCommand();

        /// <summary>
        /// Sets the default command.
        /// </summary>
        /// <remarks>If this is not called, or is called with null, then there will be no default
        /// command for the subsystem.
        /// <para/><b>WARNING:</b> This should <b>NOT</b> be called in a constructor if the 
        /// subsystem is a singleton.</remarks>
        /// <param name="command">The default command(or null if there should be none.</param>
        /// <exception cref="IllegalUseOfCommandException">If the command does not require the subsystem.</exception>
        protected internal void SetDefaultCommand(Command command)
        {
            if (command == null)
            {
                m_defaultCommand = null;
            }
            else
            {
                bool found = false;

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

            if (Table != null)
            {
                if (m_defaultCommand != null)
                {
                    Table.PutBoolean("hasDefault", true);
                    Table.PutString("default", m_defaultCommand.Name);
                }
                else
                {
                    Table.PutBoolean("hasDefault", false);
                }
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
                if (Table != null)
                {
                    if (m_currentCommand != null)
                    {
                        Table.PutBoolean("hasCommand", true);
                        Table.PutString("default", m_currentCommand.Name);
                    }
                    else
                    {
                        Table.PutBoolean("hasCommand", false);
                    }
                }
                m_currentCommandChanged = false;
            }
        }

        /// <summary>
        /// Returns the command which currently claims this subsystem.
        /// </summary>
        /// <returns>The command which currently claims this subsystem.</returns>
        public Command GetCurrentCommand()
        {
            return m_currentCommand;
        }

        ///<inheritdoc/>
        public string Name { get; }
        ///<inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            if (Table != null)
            {
                if (m_defaultCommand != null)
                {
                    Table.PutBoolean("hasDefault", true);
                    Table.PutString("default", m_defaultCommand.Name);
                }
                else
                {
                    Table.PutBoolean("hasDefault", false);
                }
                if (m_currentCommand != null)
                {
                    Table.PutBoolean("hasCommand", true);
                    Table.PutString("command", m_currentCommand.Name);
                }
                else
                {
                    Table.PutBoolean("hasCommand", false);
                }
            }
        }

        ///<inheritdoc/>
        public ITable Table { get; private set; }
        ///<inheritdoc/>
        public string SmartDashboardType => "Subsystem";
    }
}
