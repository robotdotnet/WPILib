using System;
using System.Collections.Generic;
using System.Linq;

namespace WPILib.Commands
{
    /// <summary>
    /// A <see cref="CommandGroup"/> is a list of commands which are executed in sequence.
    /// </summary>
    /// <remarks>
    /// Commands in a <see cref="CommandGroup"/> are added using the <see cref="AddSequential(Command)"/> method
    /// and are called sequentially. <see cref="CommandGroup">Command Groups</see> are themselves <see cref="Command">
    /// commands</see> and can be given to other <see cref="CommandGroup">Command Groups.</see>
    /// <para>
    /// <see cref="CommandGroup">Command Groups</see> will carry all of the requirements of their <see cref="Command">
    /// subcommands</see> Additional requirements can be specified by calling <see cref="Command.Requires(Subsystem)"/>
    /// normally in the constructor.
    /// </para>
    /// <para>
    /// Command Groups can also execute commands in parallel, simply by adding them using
    /// <see cref="AddParallel(Command)"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="Command"/>
    /// <seealso cref="Subsystem"/>
    /// <seealso cref="IllegalUseOfCommandException"/>
    public class CommandGroup : Command
    {
        private readonly List<Entry> m_commands = new List<Entry>();
        private readonly List<Entry> m_children = new List<Entry>();

        internal List<Entry> Children => m_children; 

        private int m_currentCommandIndex = -1;
        private readonly object m_syncRoot = new object();

        /// <summary>
        /// Creates a new <see cref="CommandGroup"/>. The name of this command
        /// will be set to its class name.
        /// </summary>
        public CommandGroup()
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="CommandGroup"/> with the given name.
        /// </summary>
        /// <param name="name">The name for this command group.</param>
        /// <exception cref="ArgumentNullException">If name is null</exception>
        public CommandGroup(string name) : base(name)
        {
            
        }

        /// <summary>
        /// Adds a new <see cref="Command"/> to the group.
        /// </summary>
        /// <remarks>
        /// The <see cref="Command"/> will be started after all the previously added <see cref="Command">commands</see>.
        /// <para>
        /// Note that any requirements thee given <see cref="Command"/> has will be added to the group. For this reason, a 
        /// <see cref="Command">Command's</see> requirements cannot be changed after being added to a group.
        /// </para>
        /// <para>
        /// It is recommended that this method be called in the constructor.
        /// </para>
        /// </remarks>
        /// <param name="command">The <see cref="Command"/> to be added.</param>
        /// <exception cref="IllegalUseOfCommandException">If the command has been started before or been given to another group.</exception>
        /// <exception cref="ArgumentNullException">If the given <see cref="Command"/> is null</exception>
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

        /// <summary>
        /// Adds a new <see cref="Command"/> to the group with a given timeout.
        /// </summary>
        /// <remarks>
        /// The <see cref="Command"/> will be started after all the previously added <see cref="Command">commands</see>.
        /// <para>
        /// Once the <see cref="Command"/> is started, it will be run until it finishes or the time expires, whichever is sooner.
        /// Note that the give <see cref="Command"/> will have no knowledge that it is on a timer.
        /// </para>
        /// <para>
        /// Note that any requirements thee given <see cref="Command"/> has will be added to the group. For this reason, a 
        /// <see cref="Command">Command's</see> requirements cannot be changed after being added to a group.
        /// </para>
        /// <para>
        /// It is recommended that this method be called in the constructor.
        /// </para>
        /// </remarks>
        /// <param name="command">The <see cref="Command"/> to be added.</param>
        /// <param name="timeout">The timeout (in seconds).</param>
        /// <exception cref="IllegalUseOfCommandException">If the group has been started before or been given to another group.</exception>
        /// <exception cref="ArgumentNullException">If the given <see cref="Command"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the given timeout is negative.</exception>
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

        /// <summary>
        /// Adds a new <see cref="Command"/> to the group.
        /// </summary>
        /// <remarks>
        /// The <see cref="Command"/> will be started after all the previously added <see cref="Command">commands</see>.
        /// <para>
        /// Instead of waiting for the child to finish, a <see cref="CommandGroup"/> will have it run
        /// at the same time as the subsequent <see cref="Command">Commands</see>. The child wil run
        /// either until it finishes, a new child with conflicting requirements is started, or the main
        /// sequence runs a <see cref="Command"/> with conflicting requirements. In the latter two cases,
        /// the child will be canceled even if it says it can't be interrupted.
        /// </para>
        /// <para>
        /// Note that any requirements thee given <see cref="Command"/> has will be added to the group. For this reason, a 
        /// <see cref="Command">Command's</see> requirements cannot be changed after being added to a group.
        /// </para>
        /// <para>
        /// It is recommended that this method be called in the constructor.
        /// </para>
        /// </remarks>
        /// <param name="command">The <see cref="Command"/> to be added.</param>
        /// <exception cref="IllegalUseOfCommandException">If the command has been started before or been given to another group.</exception>
        /// <exception cref="ArgumentNullException">If the given <see cref="Command"/> is null</exception>
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

        /// <summary>
        /// Adds a new <see cref="Command"/> to the group with the given timeout.
        /// </summary>
        /// <remarks>
        /// The <see cref="Command"/> will be started after all the previously added <see cref="Command">commands</see>.
        /// /// <para>
        /// Once the <see cref="Command"/> is started, it will be run until it finishes or the time expires, whichever is sooner.
        /// Note that the give <see cref="Command"/> will have no knowledge that it is on a timer.
        /// </para>
        /// <para>
        /// Instead of waiting for the child to finish, a <see cref="CommandGroup"/> will have it run
        /// at the same time as the subsequent <see cref="Command">Commands</see>. The child wil run
        /// either until it finishes, a new child with conflicting requirements is started, or the main
        /// sequence runs a <see cref="Command"/> with conflicting requirements. In the latter two cases,
        /// the child will be canceled even if it says it can't be interrupted.
        /// </para>
        /// <para>
        /// Note that any requirements thee given <see cref="Command"/> has will be added to the group. For this reason, a 
        /// <see cref="Command">Command's</see> requirements cannot be changed after being added to a group.
        /// </para>
        /// <para>
        /// It is recommended that this method be called in the constructor.
        /// </para>
        /// </remarks>
        /// <param name="command">The <see cref="Command"/> to be added.</param>
        /// <param name="timeout">The timeout (in seconds).</param>
        /// <exception cref="IllegalUseOfCommandException">If the command has been started before or been given to another group.</exception>
        /// <exception cref="ArgumentNullException">If the given <see cref="Command"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the given timeout is negative.</exception>
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

        /// <inheritdoc/>
        internal override void _Initialize() => m_currentCommandIndex = -1;

        /// <inheritdoc/>
        internal override void _Execute()
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
                {
                    child._Cancel();
                }
                if (!child.Run())
                {
                    child.Removed();
                    m_children.RemoveAt(i--);
                }
            }
        }

        /// <inheritdoc/>
        internal override void _End()
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

        /// <inheritdoc/>
        internal override void _Interrupted()
        {
            _End();
        }

        /// <summary>
        /// Returns true if all the <see cref="Command">Commands</see> in this group have
        /// been started and have finished.
        /// </summary>
        /// <remarks>
        /// Teams may override this method, although they probably should reference base.IsFinished()
        /// if they do.
        /// </remarks>
        /// <returns>Whether this <see cref="CommandGroup"/> is finished.</returns>
        protected override bool IsFinished()
        {
            return m_currentCommandIndex >= m_commands.Count && m_children.Count == 0;
        }

        /// <inheritdoc/>
        protected override void Execute()
        {
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
        }

        /// <inheritdoc/>
        protected override void End()
        {
        }

        /// <inheritdoc/>
        protected override void Interrupted()
        {
        }

        /// <summary>
        /// Returns whether or not this group is interruptible.
        /// </summary>
        /// <remarks>A command group will be uninterruptible if <see cref="Interruptible"/> has been set to false or if 
        /// it is currently running an uninterruptible <see cref="Command"/> or child</remarks>
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
            protected set { base.Interruptible = value; }
        }

        private void CancelConflicts(Command command)
        {
            for (int i = 0; i < m_children.Count; i++)
            {
                Command child = m_children[i].command;
                foreach (var requirement in command.GetRequirements())
                {
                    if (child.DoesRequire(requirement))
                    {
                        child._Cancel();
                        child.Removed();
                        m_children.RemoveAt(i--);
                    }
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
