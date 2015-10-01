using System;
using HAL_Base;

namespace HAL_Simulator.Data
{
    public enum TrigerType
    {
        Filtered,
        Averaged,
        Unassigned
    }

    public class AnalogTriggerData : DataBase
    {
        private bool m_hasSource = false;
        private bool m_initialized = false;
        private TrigerType m_trigType = TrigerType.Unassigned;
        private bool m_trigState = false;
        private double m_trigUpper = 0;
        private double m_trigLower = 0;

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

        public int AnalogPin { get; internal set; } = -1;

        public long TriggerPointer { get; internal set; } = -1;

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

        public bool GetTriggerValue(AnalogTriggerType type, ref int status)
        {
            return HALAnalog.getAnalogTriggerOutput((IntPtr) TriggerPointer, type, ref status);
        }
    }
}
