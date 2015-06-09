using System;
using System.Collections.Generic;
using System.Linq;
using NetworkTablesDotNet.Tables;
using static WPILib.Timer;

namespace WPILib.Commands
{
    /// <summary>
    /// The Command class is at the very core of the entire command framework.
    /// Every command can be started with a call to <see cref="Command.Start()"/>.
    /// Once a command is started it will call <see cref="Command.Initialize()"/>, and then
    /// will repeatedly call <see cref="Command.Execute()"/> until the <see cref="Command.IsFinished()"/>
    /// returns true.  Once it does, <see cref="Command.End()"/> will be called.
    /// <para> </para>
    /// <para>However, if at any point while it is running <see cref="Command.Cancel()"/> is called, then
    /// the command will be stopped and <see cref="Command.Interrupted()"/> will be called.</para>
    /// <para> </para>
    /// <para>If a command uses a <see cref="Subsystem"/>, then it should specify that it does so by
    /// calling the <see cref="Command.Requires(Subsystem)"/> method
    /// in its constructor. Note that a Command may have multiple requirements, and
    /// <see cref="Command.Requires(Subsystem)"/> should be
    /// called for each one.</para>
    /// <para> </para>
    /// <para>If a command is running and a new command with shared requirements is started,
    /// then one of two things will happen.  If the active command is interruptible,
    /// then <see cref="Command.Cancel()"/> will be called and the command will be removed
    /// to make way for the new one.  If the active command is not interruptible, the
    /// other one will not even be started, and the active one will continue functioning.</para>
    /// </summary>
    /// <seealso cref="Subsystem"/>
    /// <seealso cref="CommandGroup"/>
    /// <seealso cref="IllegalUseOfCommandException"/>
    public abstract class Command : INamedSendable, ITableListener
    {
        private string m_name;

        private double m_startTime = -1;
        private double m_timeout = -1;
        private bool m_initialized = false;
        private HashSet<Subsystem> m_requirements;
        private bool m_running = false;
        private bool m_interruptible = true;
        private bool m_canceled = false;
        private bool m_locked = false;
        private bool m_runWhenDisabled = false;

        private object m_syncRoot = new object();

        private CommandGroup m_parent;

        /// <summary>
        /// Creates a new command.
        /// The name of this command will be set to its class name.
        /// </summary>
        public Command()
        {
            m_name = GetType().Name;
            m_name = m_name.Substring(m_name.LastIndexOf('.') + 1);
        }

        /// <summary>
        /// Creates a new command with the given name.
        /// </summary>
        /// <param name="name">The name for this command</param>
        /// <exception cref="ArgumentNullException">If name is null</exception>
        public Command(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Name must not be null.");
            m_name = name;
        }

        /// <summary>
        /// Creates a new command with the given timeout and a default name.
        /// The default name is the name of the class.
        /// </summary>
        /// <param name="timeout">The time (in seconds) before this command "times out"</param>
        /// <exception cref="ArgumentOutOfRangeException">If given a negative timeout</exception>
        /// <seealso cref="Command.IsTimedOut()"/>
        public Command(double timeout)
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
        /// If no name was specified in the constructor, 
        /// then the default is the name of the class.
        /// </summary>
        /// <value>The name of the command</value>
        public string Name => m_name;

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
        /// This function will work even if there is no specified timeout
        /// </summary>
        /// <returns>The time since this command was initialize (in seconds).</returns>
        public double TimeSinceInitialized()
        {
            lock (m_syncRoot)
            {

                return m_startTime < 0 ? 0 : FPGATimestamp - m_startTime;
            }
        }

        /// <summary>
        /// This method specifies that the given <see cref="Subsystem"/> is used by this command.
        /// This method is crucial to the functioning of the Command System in general.
        /// <para> </para>
        /// Note that the recommended way to call this method is in the constructor.
        /// </summary>
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
                    throw new ArgumentNullException("Subsystem must not be null.");
                }
            }
        }


        /// <summary>
        /// Called when the command has been removed.
        /// This will call <see cref="Command.Interrupted()"/> or <see cref="Command.End()"/>.
        /// </summary>
        internal void Removed()
        {
            lock (m_syncRoot)
            {

                if (m_initialized)
                {
                    if (IsCanceled())
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

                //TODO:Add Table
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

                if (!m_runWhenDisabled && m_parent == null && RobotState.Disabled)
                {
                    Cancel();
                }
                if (IsCanceled())
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

        protected abstract void Execute();

        protected virtual void _Execute()
        {

        }

        protected abstract bool IsFinished();

        protected abstract void End();

        protected virtual void _End()
        {

        }

        protected abstract void Interrupted();

        protected virtual void _Interrupted()
        {

        }

        private void StartTiming()
        {
            m_startTime = FPGATimestamp;
        }

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

        internal void Validate(String message)
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

                //Add table 
            }
        }

        public void Start()
        {
            lock (m_syncRoot)
            {

                LockChanges();
                if (m_parent != null)
                {
                    throw new IllegalUseOfCommandException("Can not start a command that is a part of a command group");
                }
                Scheduler.GetInstance().AddCommand(this);
            }
        }

        internal void StartRunning()
        {
            lock (m_syncRoot)
            {

                m_running = true;
                m_startTime = -1;
                m_table?.PutBoolean("running", true);
            }
        }

        public bool IsRunning()
        {
            lock (m_syncRoot)
            {
                return m_running;
            }
        }

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

        public bool IsCanceled()
        {
            lock (m_syncRoot)
            {
                return m_canceled;
            }
        }

        public bool IsInterruptible()
        {
            lock (m_syncRoot)
            {
                return m_interruptible;
            }
        }

        protected void SetInterruptable(bool interruptible)
        {
            lock (m_syncRoot)
            {
                m_interruptible = interruptible;
            }
        }

        public bool DoesRequire(Subsystem system)
        {
            lock (m_syncRoot)
            {
                return m_requirements != null && m_requirements.Contains(system);
            }
        }

        public CommandGroup GetGroup()
        {
            lock (m_syncRoot)
            {
                return m_parent;
            }
        }

        public void SetRunWhenDisabled(bool run)
        {
            m_runWhenDisabled = run;
        }

        public bool WillRunWhenDisabled()
        {
            return m_runWhenDisabled;
        }

        private ITable m_table;
        public override string ToString()
        {
            return Name;
        }

        public void InitTable(ITable subtable)
        {
            m_table?.RemoveTableListener(this);
            m_table = subtable;
            if (m_table != null)
            {
                m_table.PutString("name", Name);
                m_table.PutBoolean("running", IsRunning());
                m_table.PutBoolean("isParented", m_parent != null);
                m_table.AddTableListener("running", this, false);
            }
        }

        public ITable Table => m_table;

        public string SmartDashboardType => "Command";

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
