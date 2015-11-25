namespace HAL_Simulator.Data
{
    /// <summary>
    /// The type of PWM Controller
    /// </summary>
    public enum ControllerType
    {
        /// <summary>
        /// none
        /// </summary>
        None,
        /// <summary>
        /// jaguar
        /// </summary>
        Jaguar,
        /// <summary>
        /// talon
        /// </summary>
        Talon,
        /// <summary>
        /// talon SRX
        /// </summary>
        TalonSRX,
        /// <summary>
        /// victor
        /// </summary>
        Victor,
        /// <summary>
        /// victor sp
        /// </summary>
        VictorSP,
        /// <summary>
        /// servo
        /// </summary>
        Servo
    }

    /// <summary>
    /// PWM Sim Data
    /// </summary>
    /// <seealso cref="HAL_Simulator.Data.NotifyDataBase" />
    public class PWMData : NotifyDataBase
    {
        private bool m_initialized = false;
        private ControllerType m_type = ControllerType.None;
        private uint m_rawValue = 0;
        private double m_value = 0.0;
        private uint m_periodScale = 0;
        private bool m_zeroLatch = false;

        internal PWMData() { }

        /// <inheritdoc/>
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="PWMData"/> is initialized.
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
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
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

        /// <summary>
        /// Gets the raw value.
        /// </summary>
        /// <value>
        /// The raw value.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether [zero latch].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [zero latch]; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets the period scale.
        /// </summary>
        /// <value>
        /// The period scale.
        /// </value>
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

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
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
