using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Data
{
    public class MXPData : DataBase
    {
        private bool m_initialized = false;

        public override void ResetData()
        {
            m_initialized = false;
        }

        public bool Initialized
        {
            get { return m_initialized; }
            set
            {
                m_initialized = value;
            }
        }
    }
}
