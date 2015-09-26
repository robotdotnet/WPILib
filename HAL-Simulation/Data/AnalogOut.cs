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
    public class AnalogOutData : INotifyPropertyChanged
    {
        private double m_voltage = 0.0;
        private bool m_hasSource = false;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool HasSource
        {
            get { return m_hasSource; }
            set
            {
                if (value == m_hasSource) return;
                m_hasSource = value;
                OnPropertyChanged();
            }
        }

        public double Voltage
        {
            get { return m_voltage; }
            set
            {
                if (value.Equals(m_voltage)) return;
                m_voltage = value;
                OnPropertyChanged();
            }
        }
    }
}
