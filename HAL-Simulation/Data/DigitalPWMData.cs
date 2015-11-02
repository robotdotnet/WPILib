namespace HAL_Simulator.Data
{
    public class DigitalPWMData : DataBase
    {
        private bool m_initialized = false;
        private double m_dutyCycle = 0;
        private uint m_pin = 0;

        internal DigitalPWMData() { }

        public override void ResetData()
        {
            m_initialized = false;
            m_dutyCycle = 0;
            m_pin = 0;
        }

        public bool Initialized
        {
            get { return m_initialized; }
            internal set
            {
                m_initialized = value;
            }
        }

        public double DutyCycle
        {
            get { return m_dutyCycle; }
            internal set
            {
                m_dutyCycle = value;
            }
        }

        public uint Pin
        {
            get { return m_pin; }
            internal set
            {
                m_pin = value;
            }
        }
    }
}
