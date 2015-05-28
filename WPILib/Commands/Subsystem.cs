using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkTablesDotNet.Tables;

namespace WPILib.Commands
{
    public abstract class Subsystem : NamedSendable
    {
        private bool initializedDefaultCommand = false;
        private Command currentCommand;
        private bool currentCommandChanged;

        private Command defaultCommand;
        private string name;

        private static List<Subsystem> allSubsystems = new List<Subsystem>();

        public Subsystem(string name)
        {
            this.name = name;
            Scheduler.GetInstance().RegisterSubsystem(this);
        }

        public Subsystem()
        {
            this.name = GetType().Name.Substring(GetType().Name.LastIndexOf('.') + 1);
            Scheduler.GetInstance().RegisterSubsystem(this);
            currentCommandChanged = true;
        }

        protected abstract void InitDefaultCommand();

        protected void SetDefaultCommand(Command command)
        {
            if (command == null)
            {
                defaultCommand = null;
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
                defaultCommand = command;
            }
        }

        internal Command GetDefaultCommand()
        {
            if (!initializedDefaultCommand)
            {
                initializedDefaultCommand = true;
                InitDefaultCommand();
            }
            return defaultCommand;
        }

        internal void SetCurrentCommand(Command command)
        {
            currentCommand = command;
            currentCommandChanged = true;
        }

        internal void ConfirmCommand()
        {
            if (currentCommandChanged)
            {
                //Add Table
                currentCommandChanged = false;
            }
        }

        public Command GetCurrentCommand()
        {
            return currentCommand;
        }

        public string GetName()
        {
            return name;
        }

        private ITable table;

        public void InitTable(NetworkTablesDotNet.Tables.ITable subtable)
        {
            this.table = subtable;
            if (table != null)
            {
                if (defaultCommand != null)
                {
                    table.PutBoolean("hasDefault", true);
                    table.PutString("default", defaultCommand.GetName());
                }
                else
                {
                    table.PutBoolean("hasDefault", false);
                }
                if (currentCommand != null)
                {
                    table.PutBoolean("hasCommand", true);
                    table.PutString("command", currentCommand.GetName());
                }
                else
                {
                    table.PutBoolean("hasCommand", false);
                }
            }
        }

        public NetworkTablesDotNet.Tables.ITable GetTable()
        {
            return table;
        }

        public string GetSmartDashboardType()
        {
            return "Subsystem";
        }
    }
}
