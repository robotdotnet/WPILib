namespace HAL_Simulator.Data
{
    public enum ProgramState
    {
        None,
        Starting,
        Disabled,
        Autonomous,
        Teleop,
        Test
    }

    public class GlobalData : DataBase
    {
        private bool m_programStarted = false;
        private long m_programStart = SimHooks.GetTime();
        private double m_analogSampleRate = HALAnalog.DefaultSampleRate;

        private ushort m_pwmLoopTiming = 40;

        internal GlobalData() { }

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

        public double DigitalPWMRate { get; internal set; } = 0;
    }
}
