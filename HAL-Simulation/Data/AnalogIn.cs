using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Annotations;

namespace HAL_Simulator.Data
{
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
        private long m_accumulatorValue = 1;
        private uint m_accumulatorCount = 1;
        private int m_accumulatorCenter = 0;
        private int m_accumulatorDeadband = 0;

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
            m_accumulatorValue = 1;
            m_accumulatorCount = 1;
            m_accumulatorCenter = 0;
            m_accumulatorDeadband = 0;
        }

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

        public bool Initialized
        {
            get { return m_initialized; }
            set
            {
                if (value == m_initialized) return;
                m_initialized = value;
                OnPropertyChanged(value);
            }
        }

        public uint AverageBits
        {
            get { return m_averageBits; }
            set
            {
                if (value == m_averageBits) return;
                m_averageBits = value;
                OnPropertyChanged(value);
            }
        }

        public uint OversampleBits
        {
            get { return m_oversampleBits; }
            set
            {
                if (value == m_oversampleBits) return;
                m_oversampleBits = value;
                OnPropertyChanged(value);
            }
        }

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

        public bool AccumulatorInitialized
        {
            get { return m_accumulatorInitialized; }
            set
            {
                if (value == m_accumulatorInitialized) return;
                m_accumulatorInitialized = value;
                OnPropertyChanged(value);
            }
        }

        public int AccumulatorCenter
        {
            get { return m_accumulatorCenter; }
            set
            {
                if (value == m_accumulatorCenter) return;
                m_accumulatorCenter = value;
                OnPropertyChanged(value);
            }
        }

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

        public int AccumulatorDeadband
        {
            get { return m_accumulatorDeadband; }
            set
            {
                if (value == m_accumulatorDeadband) return;
                m_accumulatorDeadband = value;
                OnPropertyChanged(value);
            }
        }
    }
}
