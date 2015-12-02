namespace HAL.Simulator.Data
{
    /// <summary>
    /// Analog Input Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
    public class AnalogInData : NotifyDataBase
    {
        private bool m_hasSource = false;
        private bool m_initialized = false;
        private uint m_averageBits = HALAnalog.DefaultAverageBits;
        private uint m_oversampleBits = HALAnalog.DefaultOversampleBits;
        private double m_voltage = 0.0;
        private long m_lsbWeight = HALAnalog.DefaultLSBWeight;
        private int m_offset = HALAnalog.DefaultOffset;
        private bool m_accumulatorInitialized = false;
        private long m_accumulatorValue = 0;
        private uint m_accumulatorCount = 0;
        private int m_accumulatorCenter = 0;
        private int m_accumulatorDeadband = 0;

        internal AnalogInData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_hasSource = false;
            m_initialized = false;
            m_averageBits = HALAnalog.DefaultAverageBits;
            m_oversampleBits = HALAnalog.DefaultOversampleBits;
            m_voltage = 0.0;
            m_lsbWeight = HALAnalog.DefaultLSBWeight;
            m_offset = HALAnalog.DefaultOffset;
            m_accumulatorInitialized = false;
            m_accumulatorValue = 0;
            m_accumulatorCount = 0;
            m_accumulatorCenter = 0;
            m_accumulatorDeadband = 0;
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
        /// Gets the average bits.
        /// </summary>
        /// <value>
        /// The average bits.
        /// </value>
        public uint AverageBits
        {
            get { return m_averageBits; }
            internal set
            {
                if (value == m_averageBits) return;
                m_averageBits = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the oversample bits.
        /// </summary>
        /// <value>
        /// The oversample bits.
        /// </value>
        public uint OversampleBits
        {
            get { return m_oversampleBits; }
            internal set
            {
                if (value == m_oversampleBits) return;
                m_oversampleBits = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the voltage.
        /// </summary>
        /// <value>
        /// The voltage.
        /// </value>
        public double Voltage
        {
            get { return m_voltage; }
            set
            {
                if (value.Equals(m_voltage)) return;
                m_voltage = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the LSB weight.
        /// </summary>
        /// <value>
        /// The LSB weight.
        /// </value>
        public long LSBWeight
        {
            get { return m_lsbWeight; }
            set
            {
                if (value == m_lsbWeight) return;
                m_lsbWeight = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        public int Offset
        {
            get { return m_offset; }
            set
            {
                if (value == m_offset) return;
                m_offset = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [accumulator initialized].
        /// </summary>
        /// <value>
        /// <c>true</c> if [accumulator initialized]; otherwise, <c>false</c>.
        /// </value>
        public bool AccumulatorInitialized
        {
            get { return m_accumulatorInitialized; }
            internal set
            {
                if (value == m_accumulatorInitialized) return;
                m_accumulatorInitialized = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the accumulator center.
        /// </summary>
        /// <value>
        /// The accumulator center.
        /// </value>
        public int AccumulatorCenter
        {
            get { return m_accumulatorCenter; }
            internal set
            {
                if (value == m_accumulatorCenter) return;
                m_accumulatorCenter = value;
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

        /// <summary>
        /// Gets the accumulator deadband.
        /// </summary>
        /// <value>
        /// The accumulator deadband.
        /// </value>
        public int AccumulatorDeadband
        {
            get { return m_accumulatorDeadband; }
            internal set
            {
                if (value == m_accumulatorDeadband) return;
                m_accumulatorDeadband = value;
                OnPropertyChanged(value);
            }
        }
    }
}
