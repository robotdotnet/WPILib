namespace HAL.Simulator.Data
{
    /// <summary>
    /// DIO Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
    public class DIOData : NotifyDataBase
    {
        private bool m_initialized = false;
        private bool m_value = true;
        private double m_pulseLength = 0;
        private bool m_isInput = true;
        private int m_filterIdx = -1;

        internal DIOData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_initialized = false;
            m_value = true;
            m_pulseLength = 0;
            m_isInput = true;
            m_filterIdx = -1;

            base.ResetData();
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DIOData"/> is initialized.
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
        /// Gets or sets a value indicating whether this <see cref="DIOData"/> is value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if value; otherwise, <c>false</c>.
        /// </value>
        public bool Value
        {
            get { return m_value; }
            set
            {
                if (value == m_value) return;
                m_value = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the length of the pulse.
        /// </summary>
        /// <value>
        /// The length of the pulse.
        /// </value>
        public double PulseLength
        {
            get { return m_pulseLength; }
            internal set
            {
                if (value.Equals(m_pulseLength)) return;
                m_pulseLength = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is input.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is input; otherwise, <c>false</c>.
        /// </value>
        public bool IsInput
        {
            get { return m_isInput; }
            internal set
            {
                if (value == m_isInput) return;
                m_isInput = value;
                OnPropertyChanged(value);
            }
        }

        public int FilterIndex
        {
            get { return m_filterIdx; }
            internal set
            {
                if (value == m_filterIdx) return;
                m_filterIdx = value;
                OnPropertyChanged(value);
            }
        }
    }

    public class DigitalGlitchFilterData : NotifyDataBase
    {
        private bool m_enabled = false;
        private uint m_period = 0;

        public override void ResetData()
        {
            m_enabled = false;
            m_period = 0;

            base.ResetData();
        }

        public bool Enabled
        {
            get { return m_enabled; }
            internal set
            {
                if (value == m_enabled) return;
                m_enabled = value;
                OnPropertyChanged(value);
            }
        }

        public uint Period
        {
            get { return m_period; }
            internal set
            {
                if (value == m_period) return;
                m_period = value;
                OnPropertyChanged(value);
            }
        }
    }
}
