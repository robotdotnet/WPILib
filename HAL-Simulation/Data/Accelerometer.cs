using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HAL_Base;
using HAL_Simulator.Annotations;

namespace HAL_Simulator.Data
{
    public class Accelerometer : INotifyPropertyChanged
    {
        private bool m_hasSource = false;
        private bool m_active = false;
        private AccelerometerRange m_range = AccelerometerRange.Range_2G;
        private double m_x = 0;
        private double m_y = 0;
        private double m_z = 0;
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

        public bool Active
        {
            get { return m_active; }
            set
            {
                if (value == m_active) return;
                m_active = value;
                OnPropertyChanged();
            }
        }

        public AccelerometerRange Range
        {
            get { return m_range; }
            set
            {
                if (value == m_range) return;
                m_range = value;
                OnPropertyChanged();
            }
        }

        public double X
        {
            get { return m_x; }
            set
            {
                if (value.Equals(m_x)) return;
                m_x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get { return m_y; }
            set
            {
                if (value.Equals(m_y)) return;
                m_y = value;
                OnPropertyChanged();
            }
        }

        public double Z
        {
            get { return m_z; }
            set
            {
                if (value.Equals(m_z)) return;
                m_z = value;
                OnPropertyChanged();
            }
        }
    }
}
