using System;
using System.Collections.Generic;
using System.Linq;
using NetworkTables.Tables;
using static WPILib.Timer;

namespace WPILib.Commands
{
    /// <summary>
    /// The Command class is at the very core of the entire command framework.
    /// </summary><remarks>
    /// Every command can be started with a call to <see cref="Command.Start()"/>.
    /// Once a command is started it will call <see cref="Command.Initialize()"/>, and then
    /// will repeatedly call <see cref="Command.Execute()"/> until the <see cref="Command.IsFinished()"/>
    /// returns true.  Once it does, <see cref="Command.End()"/> will be called.
    /// <para>However, if at any point while it is running <see cref="Command.Cancel()"/> is called, then
    /// the command will be stopped and <see cref="Command.Interrupted()"/> will be called.</para>
    /// <para>If a command uses a <see cref="Subsystem"/>, then it should specify that it does so by
    /// calling the <see cref="Command.Requires(Subsystem)"/> method
    /// in its constructor. Note that a Command may have multiple requirements, and
    /// <see cref="Command.Requires(Subsystem)"/> should be
    /// called for each one.</para>
    /// <para>If a command is running and a new command with shared requirements is started,
    /// then one of two things will happen.  If the active command is interruptible,
    /// then <see cref="Command.Cancel()"/> will be called and the command will be removed
    /// to make way for the new one.  If the active command is not interruptible, the
    /// other one will not even be started, and the active one will continue functioning.</para>
    /// </remarks>
    /// <seealso cref="Subsystem"/>
    /// <seealso cref="CommandGroup"/>
    /// <seealso cref="IllegalUseOfCommandException"/>
    public abstract class Command : INamedSendable, ITableListener
    {
        private double m_startTime = -1;
        private double m_timeout = -1;
        private bool m_initialized = false;
        private HashSet<Subsystem> m_requirements;
        private bool m_running = false;
        private bool m_interruptible = true;
        private bool m_canceled = false;
        private bool m_locked = false;

        private object m_syncRoot = new object();

        private CommandGroup m_parent;

        /// <summary>
        /// Creates a new command.
        /// </summary><remarks>
        /// The name of this command will be set to its class name.
        /// </remarks>
        protected Command()
        {
            Name = GetType().Name;
            Name = Name.Substring(Name.LastIndexOf('.') + 1);
        }

        /// <summary>
        /// Creates a new command with the given name.
        /// </summary>
        /// <param name="name">The name for this command</param>
        /// <exception cref="ArgumentNullException">If name is null</exception>
        protected Command(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "Name must not be null.");
            Name = name;
        }

        /// <summary>
        /// Creates a new command with the given timeout and a default name.
        /// </summary>
        /// <remarks>The default name is the name of the class.</remarks>
        /// <param name="timeout">The time (in seconds) before this command "times out"</param>
        /// <exception cref="ArgumentOutOfRangeException">If given a negative timeout</exception>
        /// <seealso cref="Command.IsTimedOut()"/>
        protected Command(double timeout)
            : this()
        {
            if (timeout < 0)
                throw new ArgumentOutOfRangeException("Timeout must not be negative. Given:" + timeout);
            m_timeout = timeout;
        }

        /// <summary>
        /// Creates a new command with the given name and timeout.
        /// </summary>
        /// <param name="name">The name of the command</param>
        /// <param name="timeout">The time (in seconds) before this command "times out"</param>
        /// <exception cref="ArgumentOutOfRangeException">If given a negative timeout</exception>
        /// <seealso cref="Command.IsTimedOut()"/>
        public Command(string name, double timeout)
            : this(name)
        {
            if (timeout < 0)
                throw new ArgumentOutOfRangeException("Timeout must not be negative. Given:" + timeout);
            m_timeout = timeout;
        }

        /// <summary>
        /// Returns the name of this command.
        /// </summary><remarks>
        /// If no name was specified in the constructor, 
        /// then the default is the name of the class.
        /// </remarks>
        public string Name { get; }

        /// <summary>
        /// Sets the timeout of this command.
        /// </summary>
        /// <param name="seconds">the timeout (in seconds)</param>
        /// <exception cref="ArgumentOutOfRangeException">If given a negative timeout</exception>
        /// <seealso cref="Command.IsTimedOut()"/>
        protected void SetTimeout(double seconds)
        {
            lock (m_syncRoot)
            {

                if (seconds < 0)
                    throw new ArgumentOutOfRangeException("Timeout must not be negative. Given:" + seconds);
                m_timeout = seconds;
            }
        }

        /// <summary>
        /// Returns the time since this command was initialized (in seconds).
        /// </summary><remarks>
        /// This function will work even if there is no specified timeout
        /// </remarks>
        /// <returns>The time since this command was initialize (in seconds).</returns>
        public double TimeSinceInitialized()
        {
            lock (m_syncRoot)
            {

                return m_startTime < 0 ? 0 : GetFPGATimestamp() - m_startTime;
            }
        }

        /// <summary>
        /// This method specifies that the given <see cref="Subsystem"/> is used by this command.
        /// </summary><remarks>
        /// This method is crucial to the functioning of the Command System in general.
        /// <para> </para>
        /// Note that the recommended way to call this method is in the constructor.
        /// </remarks>
        /// <param name="subsystem">The <see cref="Subsystem"/> required</param>
        /// <exception cref="ArgumentNullException">If subsystem is null</exception>
        /// <exception cref="IllegalUseOfCommandException">If this command has started before or if it has been given to a <see cref="CommandGroup"/></exception>
        /// <seealso cref="Subsystem"/>
        protected void Requires(Subsystem subsystem)
        {
            lock (m_syncRoot)
            {

                Validate("Can not add new requirement to command");
                if (subsystem != null)
                {
                    if (m_requirements == null)
                        m_requirements = new HashSet<Subsystem>();
                    m_requirements.Add(subsystem);
                }
                else
                {
                    throw new ArgumentNullException(nameof(subsystem), "Subsystem must not be null.");
                }
            }
        }


        /// <summary>
        /// Called when the command has been removed.
        /// </summary><remarks>
        /// This will call <see cref="Command.Interrupted()"/> or <see cref="Command.End()"/>.
        /// </remarks>
        internal void Removed()
        {
            lock (m_syncRoot)
            {

                if (m_initialized)
                {
                    if (Canceled)
                    {
                        Interrupted();
                        _Interrupted();
                    }
                    else
                    {
                        End();
                        _End();
                    }
                }
                m_initialized = false;
                m_canceled = false;
                m_running = false;

                Table?.PutBoolean("running", false);
            }
        }

        /// <summary>
        /// The run method is used internally to actually run the commands.
        /// </summary>
        /// <returns>Whether or not the command should stay within the <see cref="Scheduler"/>.</returns>
        internal bool Run()
        {
            lock (m_syncRoot)
            {

                if (!RunWhenDisabled && m_parent == null && RobotState.Disabled)
                {
                    Cancel();
                }
                if (Canceled)
                {
                    return false;
                }
                if (!m_initialized)
                {
                    m_initialized = true;
                    StartTiming();
                    _Initialize();
                    Initialize();
                }
                _Execute();
                Execute();
                return !IsFinished();
            }
        }

        /// <summary>
        /// The initialize method is called the first time this Command is run after
        /// being started.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// A shadow method called before <see cref="Command.Initialize()"/>
        /// </summary>
        protected virtual void _Initialize()
        {

        }
        
        /// <summary>
        /// The execute method is called repeatedly until this <see cref="Command"/>
        /// either finishes or is canceled.
        /// </summary>
        protected abstract void Execute();

        internal virtual void _Execute()
        {

        }

        /// <summary>
        /// Returns whether this command is finished.
        /// </summary>
        /// <remarks>If it is, then the command will be removed and
        /// <see cref="Command.End()"/> will be called.
        /// <para/>It may be useful for a team to reference the <see cref="Command.IsTimedOut()"/>
        /// method for time-sensitive commands.</remarks>
        /// <returns>Whether this command is finished</returns>
        /// <seealso cref="Command.IsTimedOut()"/>
        protected abstract bool IsFinished();

        /// <summary>
        /// Called when the command ended peacefully
        /// </summary>
        /// <remarks>This is where you may want to wrap up loose ends, 
        /// like shutting off a motor that was being used in the command.</remarks>
        protected abstract void End();

        internal virtual void _End()
        {

        }

        /// <summary>
        /// Called when the command ends because somebody called <see cref="Cancel"/> or another command
        /// shared the same requirements as this one, and booted it out.
        /// </summary>
        /// <remarks>This is where you may want to wrap up loose ends,
        /// like shutting off a motor that was being used in the command.
        /// Generally, it is useful to call the <see cref="End"/> method
        /// within this method.</remarks>
        protected abstract void Interrupted();

        internal virtual void _Interrupted()
        {

        }


        private void StartTiming()
        {
            m_startTime = GetFPGATimestamp();
        }

        /// <summary>
        /// Returns whether or not the <see cref="Command.TimeSinceInitialized()"/> 
        /// method returns a number which is greater then or equal to the timeout for the command.
        /// </summary>
        /// <remarks>If there is no timeout, this will always return false.</remarks>
        /// <returns>Whether the time has expired.</returns>
        protected bool IsTimedOut()
        {
            lock (m_syncRoot)
            {
                return m_timeout != -1 && TimeSinceInitialized() >= m_timeout;
            }
        }

        internal IEnumerable<Subsystem> GetRequirements()
        {
            lock (m_syncRoot)
            {

                return m_requirements ?? Enumerable.Empty<Subsystem>();
            }
        }

        internal void LockChanges()
        {
            lock (m_syncRoot)
            {
                m_locked = true;
            }
        }

        internal void Validate(string message)
        {
            lock (m_syncRoot)
            {

                if (m_locked)
                {
                    throw new IllegalUseOfCommandException(message + " after being started or being added to a command group");
                }
            }
        }

        internal void SetParent(CommandGroup parent)
        {
            lock (m_syncRoot)
            {

                if (m_parent != null)
                {
                    throw new IllegalUseOfCommandException(
                        "Can not give command to a command group after already being put in a command group");
                }

                LockChanges();
                m_parent = parent;

                Table?.PutBoolean("isParented", true);
            }
        }

        /// <summary>
        /// Starts up the command.
        /// </summary>
        /// <remarks>Gets the command ready to start.
        /// <para/>Note that the command will eventually start, however it will not necessarily
        /// do so immediately, and may in fact be canceled before initialize is even called.</remarks>
        public void Start()
        {
            lock (m_syncRoot)
            {

                LockChanges();
                if (m_parent != null)
                {
                    throw new IllegalUseOfCommandException("Can not start a command that is a part of a command group");
                }
                Scheduler.Instance.AddCommand(this);
            }
        }

        internal void StartRunning()
        {
            lock (m_syncRoot)
            {

                m_running = true;
                m_startTime = -1;
                Table?.PutBoolean("running", true);
            }
        }

        /// <summary>
        /// Returns whether or not the command is running.
        /// </summary>
        /// <remarks>This may return true even if the command has been canceled,
        /// as it may not have yet called <see cref="Command.Interrupted()"/></remarks>
        /// <returns>Whether or not the command is running</returns>
        public bool IsRunning()
        {
            lock (m_syncRoot)
            {
                return m_running;
            }
        }

        /// <summary>
        /// This will cancel the current command.
        /// </summary>
        /// <remarks>This will cancel the current command eventually. It can be called multiple times.
        /// And it can be called when the command is not running. If the command is running though,
        /// then the command will be marked as canceled and eventually removed.
        /// <para/>A command cannot be canceled if it is a part of a <see cref="CommandGroup">command group</see>,
        /// you must cancel the <see cref="CommandGroup">command group</see> instead.</remarks>
        public void Cancel()
        {
            lock (m_syncRoot)
            {

                if (m_parent != null)
                {
                    throw new IllegalUseOfCommandException("Can not manually cancel a command in a command group");
                }
                _Cancel();
            }
        }

        internal void _Cancel()
        {
            lock (m_syncRoot)
            {

                if (IsRunning())
                {
                    m_canceled = true;
                }
            }
        }

        /// <summary>
        /// Gets whether or not this has been canceled.
        /// </summary>
        public bool Canceled
        {
            get
            {
                lock (m_syncRoot)
                {
                    return m_canceled;
                }
            }
        }

        /// <summary>
        /// Get or Set whether or not this command can be interrupted.
        /// </summary>
        public bool Interruptible
        {
            get
            {
                lock (m_syncRoot)
                {
                    return m_interruptible;
                }
            }
            protected set
            {
                lock (m_syncRoot)
                {
                    m_interruptible = value;
                }
            }
        }

        /// <summary>
        /// Checks if the command requires the given <see cref="Subsystem"/>.
        /// </summary>
        /// <param name="system">The system</param>
        /// <returns>Whether or not the subsystem is required, or false if given null.</returns>
        public bool DoesRequire(Subsystem system)
        {
            lock (m_syncRoot)
            {
                return m_requirements?.Contains(system) ?? false;
            }
        }

        /// <summary>
        /// Returns the <see cref="CommandGroup"/> this command is a part of.
        /// </summary>
        /// <remarks>Will return null if this <see cref="Command"/> is not in a group.</remarks>
        /// <returns>The <see cref="CommandGroup"/> that this command is a part of (or null if not in a group)</returns>
        public CommandGroup GetGroup()
        {
            lock (m_syncRoot)
            {
                return m_parent;
            }
        }

        /// <summary>
        /// Gets or Sets whether or not this <see cref="Command"/> should run when the robot is disabled.
        /// </summary>
        /// <remarks>By default a command will not run when the robot is disabled, and will in fact be canceled.</remarks>
        public bool RunWhenDisabled { set; get; } = false;

        /// <summary>
        /// The string representation for a <see cref="Command"/> is by default its name.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Initialize a table for this sendable object.
        /// </summary>
        /// <param name="subtable">The table to put the values in.</param>
        public void InitTable(ITable subtable)
        {
            Table?.RemoveTableListener(this);
            Table = subtable;
            if (Table != null)
            {
                Table.PutString("name", Name);
                Table.PutBoolean("running", IsRunning());
                Table.PutBoolean("isParented", m_parent != null);
                Table.AddTableListener("running", this, false);
            }
        }

        /// <summary>
        /// Returns the table that is currently associated with the sendable
        /// </summary>
        public ITable Table { get; private set; }

        /// <summary>
        /// Returns the string representation of the named data type that will be used by the smart dashboard for this sendable
        /// </summary>
        public string SmartDashboardType => "Command";

        /// <summary>
        /// This function is called whenever the value is changed on the NetworkTable.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isNew"></param>
        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            if ((bool)value)
            {
                Start();
            }
            else
            {
                Cancel();
            }
        }
    }
}
