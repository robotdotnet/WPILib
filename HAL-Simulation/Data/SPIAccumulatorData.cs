using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Data
{
    public class SPIAccumulatorData : DataBase
    {
        private bool m_hasSource = false;
        private bool m_initialized = false;
        private long m_accumulatorValue = 0;
        private uint m_accumulatorCount = 0;

        internal SPIAccumulatorData() { }

        public override void ResetData()
        {
            m_hasSource = false;
            m_initialized = false;
            m_accumulatorCount = 0;
            m_accumulatorValue = 0;
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
