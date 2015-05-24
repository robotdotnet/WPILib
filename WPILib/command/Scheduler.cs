using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using HAL_Base;
using WPILib.Buttons;

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

        private List<Button> buttons;

        private bool m_runningCommandsChanged;

        private Scheduler()
        {
            HAL.Report(ResourceType.kResourceType_Command, Instances.kCommand_Scheduler);
        }

        public void Add(Command command)
        {
            if (command != null)
                additions.Add(command);
        }

        //Add Button

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
            }




        }



        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
