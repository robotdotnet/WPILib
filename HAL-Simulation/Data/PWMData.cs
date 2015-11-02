namespace HAL_Simulator.Data
{
    public enum ControllerType
    {
        None,
        Jaguar,
        Talon,
        TalonSRX,
        Victor,
        VictorSP,
        Servo
    }

    public class PWMData : NotifyDataBase
    {
        private bool m_initialized = false;
        private ControllerType m_type = ControllerType.None;
        private uint m_rawValue = 0;
        private double m_value = 0.0;
        private uint m_periodScale = 0;
        private bool m_zeroLatch = false;

        internal PWMData() { }

        public override void ResetData()
        {
            m_initialized = false;
            m_type = ControllerType.None;
            m_rawValue = 0;
            m_value = 0.0;
            m_periodScale = 0;
            m_zeroLatch = false;

            base.ResetData();
        }

        public bool Initialized
        {
            get { return m_initialized; }
            internal set
            {
                if (value == m_initialized) return;
                m_initialized = value;
                OnPropertyChanged(value);
            }
        }

        public ControllerType Type
        {
            get { return m_type; }
            internal set
            {
                if (value == m_type) return;
                m_type = value;
                OnPropertyChanged(value);
            }
        }

        public uint RawValue
        {
            get { return m_rawValue; }
            internal set
            {
                if (value == m_rawValue) return;
                m_rawValue = value;
                OnPropertyChanged(value);
            }
        }

        public bool ZeroLatch
        {
            get { return m_zeroLatch; }
            internal set
            {
                if (value == m_zeroLatch) return;
                m_zeroLatch = value;
                OnPropertyChanged(value);
            }
        }

        public uint PeriodScale
        {
            get { return m_periodScale; }
            internal set
            {
                if (value == m_periodScale) return;
                m_periodScale = value;
                OnPropertyChanged(value);
            }
        }

        public double Value
        {
            get { return m_value; }
            internal set
            {
                if (value.Equals(m_value)) return;
                m_value = value;
                OnPropertyChanged(value);
            }
        }
    }
}
