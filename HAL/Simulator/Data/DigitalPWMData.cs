namespace HAL.Simulator.Data
{
    /// <summary>
    /// Digital PWM Sim Data
    /// </summary>
    /// <seealso cref="DataBase" />
    public class DigitalPWMData : DataBase
    {
        private bool m_initialized = false;
        private double m_dutyCycle = 0;
        private uint m_pin = 0;

        internal DigitalPWMData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_initialized = false;
            m_dutyCycle = 0;
            m_pin = 0;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DigitalPWMData"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized
        {
            get { return m_initialized; }
            internal set
            {
                m_initialized = value;
            }
        }

        /// <summary>
        /// Gets the duty cycle.
        /// </summary>
        /// <value>
        /// The duty cycle.
        /// </value>
        public double DutyCycle
        {
            get { return m_dutyCycle; }
            internal set
            {
                m_dutyCycle = value;
            }
        }

        /// <summary>
        /// Gets the pin.
        /// </summary>
        /// <value>
        /// The pin.
        /// </value>
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
