namespace HAL.Simulator.Data
{
    /// <summary>
    /// Analog Output Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
    public class AnalogOutData : NotifyDataBase
    {
        private double m_voltage = 0.0;
        private bool m_initialized = false;

        internal AnalogOutData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_voltage = 0.0;
            m_initialized = false;
            base.ResetData();
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="AnalogOutData"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets the voltage.
        /// </summary>
        /// <value>
        /// The voltage.
        /// </value>
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
