namespace HAL_Simulator.Data
{
    /// <summary>
    /// The robot program state
    /// </summary>
    public enum ProgramState
    {
        None,
        Starting,
        Disabled,
        Autonomous,
        Teleop,
        Test
    }

    /// <summary>
    /// Global Sim Robot Data
    /// </summary>
    /// <seealso cref="HAL_Simulator.Data.DataBase" />
    public class GlobalData : DataBase
    {
        private bool m_programStarted = false;
        private long m_programStart = SimHooks.GetTime();
        private double m_analogSampleRate = HALAnalog.DefaultSampleRate;

        private ushort m_pwmLoopTiming = 40;

        internal GlobalData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_programStarted = false;
            m_programStart = SimHooks.GetTime();
            m_analogSampleRate = HALAnalog.DefaultSampleRate;
            m_pwmLoopTiming = 40;
            DigitalPWMRate = 0;
            m_programState = ProgramState.None;
        }

        private ProgramState m_programState;

        /// <summary>
        /// Gets or sets the state of the user program.
        /// </summary>
        /// <value>
        /// The state of the user program.
        /// </value>
        public ProgramState UserProgramState
        {
            get { return m_programState; }
            set
            {
                if (m_programState == value) return;
                m_programState = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [program started].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [program started]; otherwise, <c>false</c>.
        /// </value>
        public bool ProgramStarted
        {
            get { return m_programStarted; }
            set
            {
                if (value == m_programStarted) return;
                m_programStarted = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the program start time.
        /// </summary>
        /// <value>
        /// The program start time.
        /// </value>
        public long ProgramStartTime
        {
            get { return m_programStart; }
            set
            {
                if (value == m_programStart) return;
                m_programStart = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the analog sample rate.
        /// </summary>
        /// <value>
        /// The analog sample rate.
        /// </value>
        public double AnalogSampleRate
        {
            get { return m_analogSampleRate; }
            internal set
            {
                if (value.Equals(m_analogSampleRate)) return;
                m_analogSampleRate = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the PWM loop timing.
        /// </summary>
        /// <value>
        /// The PWM loop timing.
        /// </value>
        public ushort PWMLoopTiming
        {
            get { return m_pwmLoopTiming; }
            set
            {
                if (value == m_pwmLoopTiming) return;
                m_pwmLoopTiming = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the digital PWM rate.
        /// </summary>
        /// <value>
        /// The digital PWM rate.
        /// </value>
        public double DigitalPWMRate { get; internal set; } = 0;
    }
}
