using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace WPILib.Commands
{
    public abstract class Command : NamedSendable
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

        private CommandGroup m_parent;

        public Command()
        {
            m_name = GetType().Name;
            m_name = m_name.Substring(m_name.LastIndexOf('.') + 1);
        }

        public Command(string name)
        {
            if (name == null)
                throw new ArgumentNullException("NAme must not be null.");
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void SetTimeout(double seconds)
        {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException("Timeout must not be negative. Given:" + seconds);
            m_timeout = seconds;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double TimeSinceInitialized()
        {
            return m_startTime < 0 ? 0 : Timer.GetFPGATimestamp() - m_startTime;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void Requires(Subsystem subsystem)
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void Removed()
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal bool Run()
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected bool IsTimedOut()
        {
            return m_timeout != -1 && TimeSinceInitialized() >= m_timeout;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal IEnumerable<Subsystem> GetRequirements()
        {
            return m_requirements == null ? Enumerable.Empty<Subsystem>() : m_requirements;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void LockChanges()
        {
            m_locked = true;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void Validate(String message)
        {
            if (m_locked)
            {
                throw new IllegalUseOfCommandException(message + " after being started or being added to a command group");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void SetParent(CommandGroup parent)
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start()
        {
            LockChanges();
            if (m_parent != null)
            {
                throw new IllegalUseOfCommandException("Can not start a command that is a part of a command group");
            }
            //Sheduler Add
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void StartRunning()
        {
            m_running = true;
            m_startTime = -1;

            //TAdd Table
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsRunning()
        {
            return m_running;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Cancel()
        {
            if (m_parent != null)
            {
                throw new IllegalUseOfCommandException("Can not manually cancel a command in a command group");
            }
            _Cancel();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void _Cancel()
        {
            if (IsRunning())
            {
                m_canceled = true;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsCanceled()
        {
            return m_canceled;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsInterruptible()
        {
            return m_interruptible;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void SetInterruptable(bool interruptible)
        {
            this.m_interruptible = interruptible;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool DoesRequire(Subsystem system)
        {
            return m_requirements != null && m_requirements.Contains(system);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CommandGroup GetGroup()
        {
            return m_parent;
        }

        public void SetRunWhenDisabled(bool run)
        {
            m_runWhenDisabled = run;
        }

        public bool WillRunWhenDisabled()
        {
            return m_runWhenDisabled;
        }

        public override string ToString()
        {
            return this.GetName();
        }
    }
}
