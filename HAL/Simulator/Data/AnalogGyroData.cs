using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL.Simulator.Data
{
    public class AnalogGyroData : NotifyDataBase
    {
        private double m_angle = 0.0;
        private double m_rate = 0.0;
        private bool m_initialized = false;

        internal AnalogGyroData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_angle = 0.0;
            m_rate = 0.0;
            m_initialized = false;
            base.ResetData();
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="AnalogGyroData"/> is initialized.
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
        /// Gets the Rate.
        /// </summary>
        /// <value>
        /// The voltage.
        /// </value>
        public double Rate
        {
            get { return m_rate; }
            internal set
            {
                if (value.Equals(m_rate)) return;
                m_rate = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the Angle.
        /// </summary>
        /// <value>
        /// The voltage.
        /// </value>
        public double Angle
        {
            get { return m_angle; }
            internal set
            {
                if (value.Equals(m_angle)) return;
                m_angle = value;
                OnPropertyChanged(value);
            }
        }
    }
}
