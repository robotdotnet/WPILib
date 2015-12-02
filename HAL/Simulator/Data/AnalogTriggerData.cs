using System;
using HAL.Base;

namespace HAL.Simulator.Data
{
    /// <summary>
    /// The analog trigger type.
    /// </summary>
    public enum TrigerType
    {
        /// <summary>
        /// filtered
        /// </summary>
        Filtered,
        /// <summary>
        /// averaged
        /// </summary>
        Averaged,
        /// <summary>
        /// unassigned
        /// </summary>
        Unassigned
    }

    /// <summary>
    /// Analog Trigger Sim Data.
    /// </summary>
    /// <seealso cref="DataBase" />
    public class AnalogTriggerData : DataBase
    {
        private bool m_hasSource = false;
        private bool m_initialized = false;
        private TrigerType m_trigType = TrigerType.Unassigned;
        private bool m_trigState = false;
        private double m_trigUpper = 0;
        private double m_trigLower = 0;

        internal AnalogTriggerData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_hasSource = false;
            m_initialized = false;
            AnalogPin = -1;
            TriggerPointer = -1;
            m_trigType = TrigerType.Unassigned;
            m_trigState = false;
            m_trigUpper = 0;
            m_trigLower = 0;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has source.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has source; otherwise, <c>false</c>.
        /// </value>
        public bool HasSource
        {
            get { return m_hasSource;}
            set
            {
                if (value == m_hasSource) return;
                m_hasSource = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="AnalogTriggerData"/> is initialized.
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
        /// Gets the analog pin.
        /// </summary>
        /// <value>
        /// The analog pin.
        /// </value>
        public int AnalogPin { get; internal set; } = -1;

        /// <summary>
        /// Gets the trigger pointer.
        /// </summary>
        /// <value>
        /// The trigger pointer.
        /// </value>
        public long TriggerPointer { get; internal set; } = -1;

        /// <summary>
        /// Gets the type of the trig.
        /// </summary>
        /// <value>
        /// The type of the trig.
        /// </value>
        public TrigerType TrigType
        {
            get { return m_trigType; }
            internal set
            {
                if (value == m_trigType) return;
                m_trigType = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [trig state].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [trig state]; otherwise, <c>false</c>.
        /// </value>
        public bool TrigState
        {
            get { return m_trigState; }
            internal set
            {
                if (value == m_trigState) return;
                m_trigState = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the trig upper.
        /// </summary>
        /// <value>
        /// The trig upper.
        /// </value>
        public double TrigUpper
        {
            get { return m_trigUpper; }
            internal set
            {
                if (value.Equals(m_trigUpper)) return;
                m_trigUpper = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the trig lower.
        /// </summary>
        /// <value>
        /// The trig lower.
        /// </value>
        public double TrigLower
        {
            get { return m_trigLower; }
            internal set
            {
                if (value.Equals(m_trigLower)) return;
                m_trigLower = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the trigger value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public bool GetTriggerValue(AnalogTriggerType type, ref int status)
        {
            return SimulatorHAL.HALAnalog.getAnalogTriggerOutput((IntPtr) TriggerPointer, type, ref status);
        }
    }
}
