using System;
using HAL.Base;

namespace HAL.Simulator.Data
{
    /// <summary>
    /// Counter Sim Data
    /// </summary>
    /// <seealso cref="EncoderData" />
    public class CounterData : EncoderData
    {
        internal CounterData() { }

        private Mode m_mode = 0;
        private int m_averageSize = 0;

        private uint m_upSourceChannel = 0;
        private bool m_upSourceTrigger = false;
        private uint m_downSourceChannel = 0;
        private bool m_downSourceTrigger = false;

        private bool m_updateWheEmpty = false;

        private bool m_upRisingEdge = false;
        private bool m_upFallingEdge = false;
        private bool m_downRisingEdge = false;
        private bool m_downFallingEdge = false;

        private double m_pulseLengthThreshold = 0;

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_mode = 0;
            m_averageSize = 0;

            m_upSourceChannel = 0;
            m_upSourceTrigger = false;
            m_downSourceChannel = 0;
            m_downSourceTrigger = false;

            m_updateWheEmpty = false;

            m_upRisingEdge = false;
            m_upFallingEdge = false;
            m_downRisingEdge = false;
            m_downFallingEdge = false;

            m_pulseLengthThreshold = 0;

            UpCallback = null;
            DownCallback = null;

            base.ResetData();
        }
        /// <summary>
        /// Gets the pulse length threshold.
        /// </summary>
        /// <value>
        /// The pulse length threshold.
        /// </value>
        public double PulseLengthThreshold
        {
            get { return m_pulseLengthThreshold; }
            internal set
            {
                if (value.Equals(m_pulseLengthThreshold)) return;
                m_pulseLengthThreshold = value;
                OnPropertyChanged(value);
            }
        }


        /// <summary>
        /// Gets up callback.
        /// </summary>
        /// <value>
        /// Up callback.
        /// </value>
        public Action<string, dynamic> UpCallback { get; internal set; }
        /// <summary>
        /// Gets down callback.
        /// </summary>
        /// <value>
        /// Down callback.
        /// </value>
        public Action<string, dynamic> DownCallback { get; internal set; }

        /// <summary>
        /// Gets up source channel.
        /// </summary>
        /// <value>
        /// Up source channel.
        /// </value>
        public uint UpSourceChannel
        {
            get { return m_upSourceChannel; }
            internal set
            {
                if (m_upSourceChannel == value) return;
                m_upSourceChannel = value;
                OnPropertyChanged(value);
            }
        }
        /// <summary>
        /// Gets down source channel.
        /// </summary>
        /// <value>
        /// Down source channel.
        /// </value>
        public uint DownSourceChannel
        {
            get { return m_downSourceChannel; }
            internal set
            {
                if (m_downSourceChannel == value) return;
                m_downSourceChannel = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the average size.
        /// </summary>
        /// <value>
        /// The average size.
        /// </value>
        public int AverageSize
        {
            get { return m_averageSize; }
            internal set
            {
                if (m_averageSize == value) return;
                m_averageSize = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public Mode Mode
        {
            get { return m_mode; }
            internal set
            {
                if (m_mode == value) return;
                m_mode = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [up source trigger].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [up source trigger]; otherwise, <c>false</c>.
        /// </value>
        public bool UpSourceTrigger
        {
            get { return m_upSourceTrigger; }
            internal set
            {
                if (m_upSourceTrigger == value) return;
                m_upSourceTrigger = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [down source trigger].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [down source trigger]; otherwise, <c>false</c>.
        /// </value>
        public bool DownSourceTrigger
        {
            get { return m_downSourceTrigger; }
            internal set
            {
                if (m_downSourceTrigger == value) return;
                m_downSourceTrigger = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [update when empty].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [update when empty]; otherwise, <c>false</c>.
        /// </value>
        public bool UpdateWhenEmpty
        {
            get { return m_updateWheEmpty; }
            internal set
            {
                if (m_updateWheEmpty == value) return;
                m_updateWheEmpty = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [up rising edge].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [up rising edge]; otherwise, <c>false</c>.
        /// </value>
        public bool UpRisingEdge
        {
            get { return m_upRisingEdge; }
            internal set
            {
                if (m_upRisingEdge == value) return;
                m_upRisingEdge = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [down rising edge].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [down rising edge]; otherwise, <c>false</c>.
        /// </value>
        public bool DownRisingEdge
        {
            get { return m_downRisingEdge; }
            internal set
            {
                if (m_downRisingEdge == value) return;
                m_downRisingEdge = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [up falling edge].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [up falling edge]; otherwise, <c>false</c>.
        /// </value>
        public bool UpFallingEdge
        {
            get { return m_upFallingEdge; }
            internal set
            {
                if (m_upFallingEdge == value) return;
                m_upFallingEdge = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [down falling edge].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [down falling edge]; otherwise, <c>false</c>.
        /// </value>
        public bool DownFallingEdge
        {
            get { return m_downFallingEdge; }
            internal set
            {
                if (m_downFallingEdge == value) return;
                m_downFallingEdge = value;
                OnPropertyChanged(value);
            }
        }
    }
}
