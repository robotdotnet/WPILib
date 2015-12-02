namespace HAL.Simulator.Data
{
    /// <summary>
    /// Relay Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
    public class RelayData : NotifyDataBase 
    {
        private bool m_initialized = false;
        private bool m_forward = false;
        private bool m_reverse = false;

        internal RelayData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_initialized = false;
            m_forward = false;
            m_reverse = false;
            base.ResetData();
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RelayData"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized
        {
            get { return m_initialized; }
            internal set
            {
                if (m_initialized == value) return;
                m_initialized = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RelayData"/> is forward.
        /// </summary>
        /// <value>
        ///   <c>true</c> if forward; otherwise, <c>false</c>.
        /// </value>
        public bool Forward
        {
            get { return m_forward; }
            internal set
            {
                if (m_forward == value) return;
                m_forward = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RelayData"/> is reverse.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reverse; otherwise, <c>false</c>.
        /// </value>
        public bool Reverse
        {
            get { return m_reverse; }
            internal set
            {
                if (m_reverse == value) return;
                m_reverse = value;
                OnPropertyChanged(value);
            }
        }
    }
}
