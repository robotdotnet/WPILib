namespace HAL.Simulator.Data
{
    /// <summary>
    /// DIO Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
    public class DIOData : NotifyDataBase
    {
        private bool m_initialized = false;
        private bool m_hasSource = false;
        private bool m_value = true;
        private double m_pulseLength = 0;
        private bool m_isInput = true;

        internal DIOData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_initialized = false;
            m_hasSource = false;
            m_value = true;
            m_pulseLength = 0;
            m_isInput = true;

            base.ResetData();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has source.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has source; otherwise, <c>false</c>.
        /// </value>
        public bool HasSource
        {
            get { return m_hasSource; }
            set
            {
                if (value == m_hasSource) return;
                m_hasSource = value;
                OnPropertyChanged(value);
            }
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
    }
}
