using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NetworkTablesDotNet.Tables;

namespace WPILib.Commands
{
    public abstract class Command : NamedSendable, ITableListener
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

        private object syncRoot = new object();

        private CommandGroup m_parent;

        public Command()
        {
            m_name = GetType().Name;
            m_name = m_name.Substring(m_name.LastIndexOf('.') + 1);
        }

        public Command(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Name must not be null.");
            m_name = name;
        }

        public Command(double timeout)
            : this()
        {
            if (timeout < 0)
                throw new ArgumentOutOfRangeException("Timeout must not be negative. Given:" + timeout);
            m_timeout = timeout;
        }

        public Command(string name, double timeout)
            : this(name)
        {
            if (timeout < 0)
                throw new ArgumentOutOfRangeException("Timeout must not be negative. Given:" + timeout);
            m_timeout = timeout;
        }

        public string GetName()
        {
            return m_name;
        }

        protected void SetTimeout(double seconds)
        {
            lock (syncRoot)
            {

                if (seconds < 0)
                    throw new ArgumentOutOfRangeException("Timeout must not be negative. Given:" + seconds);
                m_timeout = seconds; 
            }
        }

        public double TimeSinceInitialized()
        {
            lock (syncRoot)
            {

                return m_startTime < 0 ? 0 : Timer.GetFPGATimestamp() - m_startTime; 
            }
        }

        protected void Requires(Subsystem subsystem)
        {
            lock (syncRoot)
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

        internal void Removed()
        {
            lock (syncRoot)
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

                //Add table 
            }
        }

        internal bool Run()
        {
            lock (syncRoot)
            {

                if (!m_runWhenDisabled && m_parent == null && RobotState.IsDisabled())
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

        protected abstract void Initialize();

        private void _Initialize()
        {

        }

        protected abstract void Execute();

        private void _Execute()
        {

        }

        protected abstract bool IsFinished();

        protected abstract void End();

        private void _End()
        {

        }

        protected abstract void Interrupted();

        private void _Interrupted()
        {

        }

        private void StartTiming()
        {
            m_startTime = Timer.GetFPGATimestamp();
        }

        protected bool IsTimedOut()
        {
            lock (syncRoot)
            {
                return m_timeout != -1 && TimeSinceInitialized() >= m_timeout; 
            }
        }

        internal IEnumerable<Subsystem> GetRequirements()
        {
            lock (syncRoot)
            {

                return m_requirements == null ? Enumerable.Empty<Subsystem>() : m_requirements; 
            }
        }

        internal void LockChanges()
        {
            lock (syncRoot)
            {
                m_locked = true; 
            }
        }

        internal void Validate(String message)
        {
            lock (syncRoot)
            {

                if (m_locked)
                {
                    throw new IllegalUseOfCommandException(message + " after being started or being added to a command group");
                } 
            }
        }

        internal void SetParent(CommandGroup parent)
        {
            lock (syncRoot)
            {

                if (this.m_parent != null)
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
            lock (syncRoot)
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
            lock (syncRoot)
            {

                m_running = true;
                m_startTime = -1;
                if (m_table != null)
                    m_table.PutBoolean("running", true);
            }
        }

        public bool IsRunning()
        {
            lock (syncRoot)
            {
                return m_running; 
            }
        }

        public void Cancel()
        {
            lock (syncRoot)
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
            lock (syncRoot)
            {

                if (IsRunning())
                {
                    m_canceled = true;
                } 
            }
        }

        public bool IsCanceled()
        {
            lock (syncRoot)
            {
                return m_canceled; 
            }
        }

        public bool IsInterruptible()
        {
            lock (syncRoot)
            {
                return m_interruptible; 
            }
        }

        protected void SetInterruptable(bool interruptible)
        {
            lock (syncRoot)
            {
                this.m_interruptible = interruptible; 
            }
        }

        public bool DoesRequire(Subsystem system)
        {
            lock (syncRoot)
            {
                return m_requirements != null && m_requirements.Contains(system); 
            }
        }

        public CommandGroup GetGroup()
        {
            lock (syncRoot)
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
            return this.GetName();
        }

        public void InitTable(NetworkTablesDotNet.Tables.ITable subtable)
        {
            if (this.m_table != null)
                this.m_table.RemoveTableListener(this);
            this.m_table = subtable;
            if (m_table != null)
            {
                m_table.PutString("name", GetName());
                m_table.PutBoolean("running", IsRunning());
                m_table.PutBoolean("isParented", m_parent != null);
                m_table.AddTableListener("running", this, false);
            }
        }

        public NetworkTablesDotNet.Tables.ITable GetTable()
        {
            return m_table;
        }

        public string GetSmartDashboardType()
        {
            return "Command";
        }

        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            if ((bool) value)
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
