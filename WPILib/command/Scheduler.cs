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
    public class Scheduler : NamedSendable
    {
        private static Scheduler s_instance;
        private static object s_lockObject = new object();

        public static Scheduler GetInstance()
        {
            lock (s_lockObject)
            {
                return s_instance == null ? s_instance = new Scheduler() : s_instance;
            }
        }

        private Hashtable m_commandTable = new Hashtable();

        private HashSet<Subsystem> m_subsystems = new HashSet<Subsystem>();


        private LinkedList<Command> m_commands = new LinkedList<Command>();
        //private LinkedListNode<Command> m_firstCommand;
        //private LinkedListNode<Command> m_lastCommand;

        private bool adding = false;
        private bool disabled = false;

        private List<Command> additions = new List<Command>();

        private List<ButtonScheduler> buttons;

        private bool m_runningCommandsChanged;

        private Scheduler()
        {
            HLUsageReporting.ReportScheduler();
        }

        public void Add(Command command)
        {
            if (command != null)
                additions.Add(command);
        }

        public void AddButton(ButtonScheduler button)
        {
            if (buttons == null)
            {
                buttons = new List<ButtonScheduler>();
            }
            buttons.Add(button);
        }

        private void _Add(Command command)
        {
            if (command == null)
            {
                return;
            }

            if (adding)
            {
                Console.Error.WriteLine("WARNING: Can not start command from cancel method.  Ignoring:" + command);
                return;
            }

            if (!m_commandTable.ContainsKey(command))
            {
                foreach (var s in command.GetRequirements())
                {
                    if (s.GetCurrentCommand() != null && !s.GetCurrentCommand().IsInterruptible())
                        return;
                }

                adding = true;
                foreach (var s in command.GetRequirements())
                {
                    if (s.GetCurrentCommand() != null)
                    {
                        s.GetCurrentCommand().Cancel();
                        //Remove(s.GetCurrentCommand);
                    }
                }
                adding = false;

                
                if (m_commands.Count == 0)
                {
                    m_commands.AddFirst(command);
                }
                else
                {
                    m_commands.AddLast(command);
                }
                
                //m_commandTable.Add(Command, command);

                m_runningCommandsChanged = true;

                command.StartRunning();
            }




        }

        


        public string GetName()
        {
            return "Scheduler";
        }

        public new string GetType()
        {
            return "Scheduler";
        }

        private List<string> commands;
        private List<int> ids;
        private List<int> toCancel;


        public ITable table;
        public void InitTable(NetworkTablesDotNet.Tables.ITable subtable)
        {
            table = subtable;
        }

        public NetworkTablesDotNet.Tables.ITable GetTable()
        {
            throw new NotImplementedException();
        }

        public string GetSmartDashboardType()
        {
            return "Scheduler";
        }
    }
}
