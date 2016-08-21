using HAL.SimulatorHAL;

namespace HAL.Simulator.Data
{
    /// <summary>
    /// Analog Input Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
    public class AnalogInData : NotifyDataBase
    {
        private bool m_initialized = false;
        private int m_averageBits = HALAnalogInput.DefaultAverageBits;
        private int m_oversampleBits = HALAnalogInput.DefaultOversampleBits;
        private double m_voltage = 0.0;
        private bool m_accumulatorInitialized = false;
        private long m_accumulatorValue = 0;
        private long m_accumulatorCount = 0;
        private int m_accumulatorCenter = 0;
        private int m_accumulatorDeadband = 0;

        internal AnalogInData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_initialized = false;
            m_averageBits = HALAnalogInput.DefaultAverageBits;
            m_oversampleBits = HALAnalogInput.DefaultOversampleBits;
            m_voltage = 0.0;
            m_accumulatorInitialized = false;
            m_accumulatorValue = 0;
            m_accumulatorCount = 0;
            m_accumulatorCenter = 0;
            m_accumulatorDeadband = 0;
            base.ResetData();
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
        public int AverageBits
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
        public int OversampleBits
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
        public long AccumulatorCount
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
