using System.Collections.Generic;

namespace HAL_Simulator.Data
{
    /// <summary>
    /// Encoder Sim Data
    /// </summary>
    /// <seealso cref="HAL_Simulator.Data.NotifyDataBase" />
    public class EncoderData : NotifyDataBase
    {
        private bool m_hasSource = false;
        private bool m_initialized = false;
        private int count = 0;
        private double period = double.MaxValue;
        private bool reset = false;
        private double maxPeriod = 0;
        private bool direction = false;
        private bool reverseDirection = false;
        private uint samplesToAverage = 0;

        /// <summary>
        /// The configuration
        /// </summary>
        public Dictionary<string, dynamic> Config = null;

        internal EncoderData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_hasSource = false;
            m_initialized = false;
            count = 0;
            period = double.MaxValue;
            reset = false;
            maxPeriod = 0;
            direction = false;
            reverseDirection = false;
            samplesToAverage = 0;

            base.ResetData();
        }

        /// <summary>
        /// Gets the samples to average.
        /// </summary>
        /// <value>
        /// The samples to average.
        /// </value>
        public uint SamplesToAverage
        {
            get { return samplesToAverage; }
            internal set
            {
                if (value == samplesToAverage) return;
                samplesToAverage = value;
                OnPropertyChanged(value);
            }
        }



        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return count; }
            set
            {
                if (count == value) return;
                count = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public double Period
        {
            get { return period; }
            set
            {
                if (value.Equals(period)) return;
                period = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the maximum period.
        /// </summary>
        /// <value>
        /// The maximum period.
        /// </value>
        public double MaxPeriod
        {
            get { return maxPeriod; }
            internal set
            {
                if (value.Equals(maxPeriod)) return;
                maxPeriod = value;
                OnPropertyChanged(value);
            }
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
                if (m_hasSource == value) return;
                m_hasSource = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="EncoderData"/> is initialized.
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
        /// Gets a value indicating whether this <see cref="EncoderData"/> is reset.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reset; otherwise, <c>false</c>.
        /// </value>
        public bool Reset
        {
            get { return reset; }
            internal set
            {
                if (reset == value) return;
                reset = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EncoderData"/> is direction.
        /// </summary>
        /// <value>
        ///   <c>true</c> if direction; otherwise, <c>false</c>.
        /// </value>
        public bool Direction
        {
            get { return direction; }
            set
            {
                if (direction == value) return;
                direction = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [reverse direction].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [reverse direction]; otherwise, <c>false</c>.
        /// </value>
        public bool ReverseDirection
        {
            get { return reverseDirection; }
            internal set
            {
                if (reverseDirection == value) return;
                reverseDirection = value;
                OnPropertyChanged(value);
            }
        }

    }
}
