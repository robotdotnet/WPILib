using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace WPILib.Commands
{
    public class CommandGroup : Command
    {
        private List<Entry> m_commands = new List<Entry>();
        private List<Entry> m_children = new List<Entry>();
        //private Hashtable m_commands = new Hashtable();
        //private List<?> m_children = new List<?>();

        private int m_currentCommandIndex = -1;
        private object syncRoot = new object();
        public CommandGroup()
        {
            
        }

        public CommandGroup(string name) : base(name)
        {
            
        }

        public void AddSequential(Command command)
        {
            lock (syncRoot)
            {
                Validate("Can not add new command to command group");
                if (command == null)
                    throw new ArgumentNullException("Given null command");
                command.SetParent(this);
                m_commands.Add(new Entry(command, Entry.IN_SEQUENCE));
                foreach (Subsystem e in command.GetRequirements())
                {
                    Requires(e);
                } 
            }
        }

        public void AddSequential(Command command, double timeout)
        {
            lock (syncRoot)
            {
                Validate("Can not add new command to command group");
                if (command == null)
                    throw new ArgumentNullException("Given null command");
                if (timeout < 0)
                    throw new ArgumentOutOfRangeException("Can not be given a negative timeout");
                command.SetParent(this);
                //Figure out what to do about timeout
                m_commands.Add(new Entry(command, Entry.IN_SEQUENCE, timeout));
                foreach (Subsystem e in command.GetRequirements())
                {
                    Requires(e);
                } 
            }
        }

        public void AddParallel(Command command)
        {
            lock (syncRoot)
            {

                Validate("Can not add new command to command group");
                if (command == null)
                    throw new ArgumentNullException("Given null command");
                command.SetParent(this);
                m_commands.Add(new Entry(command, Entry.BRANCH_CHILD));
                foreach (Subsystem e in command.GetRequirements())
                {
                    Requires(e);
                } 
            }
        }

        public void AddParallel(Command command, double timeout)
        {
            lock (syncRoot)
            {

                Validate("Can not add new command to command group");
                if (command == null)
                    throw new ArgumentNullException("Given null command");
                if (timeout < 0)
                    throw new ArgumentOutOfRangeException("Can not be given a negative timeout");
                command.SetParent(this);
                //Figure out what to do about timeout
                m_commands.Add(new Entry(command, Entry.BRANCH_CHILD, timeout));
                foreach (Subsystem e in command.GetRequirements())
                {
                    Requires(e);
                } 
            }
        }

        void _Initialize()
        {
            m_currentCommandIndex = -1;
        }

        void _Execute()
        {
            Entry entry = null;
            Command cmd = null;
            bool firstRun = false;
            if (m_currentCommandIndex == -1)
            {
                firstRun = true;
                m_currentCommandIndex = 0;
            }

            while (m_currentCommandIndex < m_commands.Count())
            {
                if (cmd != null)
                {
                    if (entry.IsTimedOut())
                    {
                        cmd._Cancel();
                    }
                    if (cmd.Run())
                    {
                        break;
                    }
                    else
                    {
                        cmd.Removed();
                        m_currentCommandIndex++;
                        firstRun = true;
                        cmd = null;
                        continue;
                    }
                }

                entry = m_commands[m_currentCommandIndex];
                cmd = null;

                switch (entry.state)
                {
                    case Entry.IN_SEQUENCE:
                        cmd = entry.command;
                        if (firstRun)
                        {
                            cmd.StartRunning();
                            CancelConflicts(cmd);
                        }
                        firstRun = false;
                        break;
                    case Entry.BRANCH_PEER:
                        m_currentCommandIndex++;
                        entry.command.Start();
                        break;
                    case Entry.BRANCH_CHILD:
                        m_currentCommandIndex++;
                        CancelConflicts(entry.command);
                        entry.command.StartRunning();
                        m_children.Add(entry);
                        break;
                }
            }

            for (int i = 0; i < m_children.Count; i++)
            {
                entry = m_children[i];
                Command child = entry.command;
                if (entry.IsTimedOut())
                    child._Cancel();
                if (!child.Run())
                {
                    child.Removed();
                    m_children.RemoveAt(i--);
                }
            }


        }

        void _End()
        {
            if (m_currentCommandIndex != -1 && m_currentCommandIndex < m_commands.Count)
            {
                Command cmd = m_commands[m_currentCommandIndex].command;
                cmd._Cancel();
                cmd.Removed();
            }

            foreach (var s in m_children)
            {
                Command cmd = s.command;
                cmd._Cancel();
                cmd.Removed();
            }
            m_children.Clear();
        }

        void _Interrupted()
        {
            _End();
        }

        protected override bool IsFinished()
        {
            return m_currentCommandIndex >= m_commands.Count && m_children.Count == 0;
        }

        protected override void Initialize()
        {
            
        }

        protected override void Execute()
        {
            
        }

        protected override void End()
        {
            //throw new NotImplementedException();
        }

        protected override void Interrupted()
        {
            //throw new NotImplementedException();
        }

        public new bool IsInterruptible()
        {
            lock (syncRoot)
            {

                if (!base.IsInterruptible())
                {
                    return false;
                }

                if (m_currentCommandIndex != -1 && m_currentCommandIndex < m_commands.Count)
                {
                    Command cmd = m_commands[m_currentCommandIndex].command;
                    if (!cmd.IsInterruptible())
                    {
                        return false;
                    }
                }

                for (int i = 0; i < m_children.Count; i++)
                {
                    if (!(m_children[i]).command.IsInterruptible())
                    {
                        return false;
                    }
                }

                return true; 
            }
        }

        private void CancelConflicts(Command command)
        {
            for (int i = 0; i < m_children.Count; i++)
            {
                Command child = m_children[i].command;

                //Enumeration requirements = command.getRequirements();
                foreach (var s in command.GetRequirements())
                {
                    if (child.DoesRequire(s))
                    {
                        child._Cancel();
                        child.Removed();
                        m_children.RemoveAt(i--);
                        break;
                    }
                }
            }
        }



        private class Entry
        {
            internal const int IN_SEQUENCE = 0;
            internal const int BRANCH_PEER = 1;
            internal const int BRANCH_CHILD = 2;

            internal Command command;
            internal int state;
            internal double timeout;

            internal Entry(Command command, int state)
            {
                this.command = command;
                this.state = state;
                this.timeout = -1;
            }

            internal Entry(Command command, int state, double timeout)
            {
                this.command = command;
                this.state = state;
                this.timeout = timeout;
            }

            internal bool IsTimedOut()
            {
                if (timeout == -1)
                    return false;
                else
                {
                    double time = command.TimeSinceInitialized();
                    return time == 0 ? false : time >= timeout;
                }
            }

        }
    }
}
