using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            //Scheduler
        }

        public Subsystem()
        {
            this.name = GetType().Name.Substring(GetType().Name.LastIndexOf('.') + 1);
            //Scheduler
            currentCommandChanged = true;
        }

        protected abstract void InitDefualtCommand();

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

        protected Command GetDefaultCommand()
        {
            if (!initializedDefaultCommand)
            {
                initializedDefaultCommand = true;
                InitDefualtCommand();
            }
            return defaultCommand;
        }

        void SetCurrentCommand(Command command)
        {
            currentCommand = command;
            currentCommandChanged = true;
        }

        void ConfirmCommand()
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

        public void InitTable(NetworkTablesDotNet.Tables.ITable subtable)
        {
            throw new NotImplementedException();
        }

        public NetworkTablesDotNet.Tables.ITable GetTable()
        {
            throw new NotImplementedException();
        }

        public string GetSmartDashboardType()
        {
            throw new NotImplementedException();
        }
    }
}
