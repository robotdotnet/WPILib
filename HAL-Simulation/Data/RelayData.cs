using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Data
{
    public class RelayData : NotifyDataBase 
    {
        private bool m_initialized = false;
        private bool m_forward = false;
        private bool m_reverse = false;

        public override void ResetData()
        {
            m_initialized = false;
            m_forward = false;
            m_reverse = false;
        }

        public bool Initialized
        {
            get { return m_initialized; }
            set
            {
                if (m_initialized == value) return;
                m_initialized = value;
                OnPropertyChanged(value);
            }
        }

        public bool Forward
        {
            get { return m_forward; }
            set
            {
                if (m_forward == value) return;
                m_forward = value;
                OnPropertyChanged(value);
            }
        }

        public bool Reverse
        {
            get { return m_reverse; }
            set
            {
                if (m_reverse == value) return;
                m_reverse = value;
                OnPropertyChanged(value);
            }
        }
    }
}
