namespace HAL.Simulator.Data
{
    public class SPIAccumulatorData : DataBase
    {
        private bool m_initialized = false;
        private long m_accumulatorValue = 0;
        private uint m_accumulatorCount = 0;

        internal SPIAccumulatorData() { }

        public override void ResetData()
        {
            m_initialized = false;
            m_accumulatorCount = 0;
            m_accumulatorValue = 0;
        }

        /// <summary>
        /// Gets a value indicating whether this input is initialized.
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
        /// Gets or sets the accumulator value.
        /// </summary>
        /// <value>
        /// The accumulator value.
        /// </value>
        public long AccumulatorValue
        {
            get { return m_accumulatorValue; }
            set
            {
                if (value == m_accumulatorValue) return;
                m_accumulatorValue = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the accumulator count.
        /// </summary>
        /// <value>
        /// The accumulator count.
        /// </value>
        public uint AccumulatorCount
        {
            get { return m_accumulatorCount; }
            set
            {
                if (value == m_accumulatorCount) return;
                m_accumulatorCount = value;
                OnPropertyChanged(value);
            }
        }
    }
}
