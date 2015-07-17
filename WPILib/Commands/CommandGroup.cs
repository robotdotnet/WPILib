using System;
using System.Collections.Generic;
using System.Linq;

namespace WPILib.Commands
{
    /// <summary>
    /// A CommandGroup is a list of commands which are executed in sequence.
    /// </summary>
    public class CommandGroup : Command
    {
        private List<Entry> m_commands = new List<Entry>();
        private LinkedList<Entry> m_children = new LinkedList<Entry>();

        internal LinkedList<Entry> Children => m_children;

        private int m_currentCommandIndex = -1;
        private object m_syncRoot = new object();
        public CommandGroup()
        {
            
        }

        public CommandGroup(string name) : base(name)
        {
            
        }

        public void AddSequential(Command command)
        {
            lock (m_syncRoot)
            {
                Validate("Can not add new command to command group");
                if (command == null)
                    throw new ArgumentNullException(nameof(command), "Given null command");
                command.SetParent(this);
                m_commands.Add(new Entry(command, Entry.IN_SEQUENCE));
                foreach (var e in command.GetRequirements())
                {
                    Requires(e);
                } 
            }
        }

        public void AddSequential(Command command, double timeout)
        {
            lock (m_syncRoot)
            {
                Validate("Can not add new command to command group");
                if (command == null)
                    throw new ArgumentNullException(nameof(command), "Given null command");
                if (timeout < 0)
                    throw new ArgumentOutOfRangeException(nameof(timeout), "Can not be given a negative timeout");
                command.SetParent(this);
                m_commands.Add(new Entry(command, Entry.IN_SEQUENCE, timeout));
                foreach (Subsystem e in command.GetRequirements())
                {
                    Requires(e);
                } 
            }
        }

        public void AddParallel(Command command)
        {
            lock (m_syncRoot)
            {

                Validate("Can not add new command to command group");
                if (command == null)
                    throw new ArgumentNullException(nameof(command), "Given null command");
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
            lock (m_syncRoot)
            {

                Validate("Can not add new command to command group");
                if (command == null)
                    throw new ArgumentNullException(nameof(command), "Given null command");
                if (timeout < 0)
                    throw new ArgumentOutOfRangeException(nameof(command), "Can not be given a negative timeout");
                command.SetParent(this);

                m_commands.Add(new Entry(command, Entry.BRANCH_CHILD, timeout));
                foreach (Subsystem e in command.GetRequirements())
                {
                    Requires(e);
                } 
            }
        }

        protected new virtual void _Initialize()
        {
            m_currentCommandIndex = -1;
        }

        protected new virtual void _Execute()
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
                        m_children.AddLast(entry);
                        break;
                }
            }
            var iter = m_children.First;

            for (; iter != m_children.Last;)
            {
                entry = iter.Value;
                Command child = entry.command;
                if (entry.IsTimedOut())
                {
                    child.Cancel();
                }
                if (!child.Run())
                {
                    child.Removed();
                    var iterTemp = iter.Next;
                    m_children.Remove(iter);
                    iter = iterTemp;
                }
                else
                {
                    iter = iter.Next;
                }
            }
        }

        protected new void _End()
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

        protected new virtual void _Interrupted()
        {
            _End();
        }

        protected override bool IsFinished()
        {
            return m_currentCommandIndex >= m_commands.Count && m_children.Count == 0;
        }


        protected override void Execute()
        {
            
        }

        protected override void Initialize()
        {
        }

        protected override void End()
        {
        }

        protected override void Interrupted()
        {
        }

        public new bool Interruptible
        {
            get
            {
                lock (m_syncRoot)
                {

                    if (!base.Interruptible)
                    {
                        return false;
                    }

                    if (m_currentCommandIndex != -1 && m_currentCommandIndex < m_commands.Count)
                    {
                        Command cmd = m_commands[m_currentCommandIndex].command;
                        if (!cmd.Interruptible)
                        {
                            return false;
                        }
                    }

                    return m_children.All(s => s.command.Interruptible);
                }
            }
        }

        private void CancelConflicts(Command command)
        {
            var childIter = m_children.First;

            for (; childIter != m_children.Last;)
            {
                Command child = childIter.Value.command;
                bool erased = false;

                if (command.GetRequirements().Any(child.DoesRequire))
                {
                    child._Cancel();
                    child.Removed();
                    var childTemp = childIter.Next;
                    m_children.Remove(childIter);
                    childIter = childTemp;
                    erased = true;
                }
                if (!erased)
                {
                    childIter = childIter.Next;
                }
            }
        }



        internal class Entry
        {

// ReSharper disable InconsistentNaming
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
                timeout = -1;
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
// ReSharper restore InconsistentNaming

        }
    }
}
