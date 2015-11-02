namespace HAL_Simulator.Data
{
    public class AnalogOutData : NotifyDataBase
    {
        private double m_voltage = 0.0;
        private bool m_initialized = false;

        internal AnalogOutData() { }

        public override void ResetData()
        {
            m_voltage = 0.0;
            m_initialized = false;
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

        public double Voltage
        {
            get { return m_voltage; }
            internal set
            {
                if (value.Equals(m_voltage)) return;
                m_voltage = value;
                OnPropertyChanged(value);
            }
        }
    }
}
