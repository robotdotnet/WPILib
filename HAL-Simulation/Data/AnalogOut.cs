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
    public class AnalogOutData : NotifyDataBase
    {
        private double m_voltage = 0.0;
        private bool m_hasSource = false;
        private bool m_initialized = false;

        public override void ResetData()
        {
            m_voltage = 0.0;
            m_hasSource = false;
            m_initialized = false;
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
    }
}
