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
    public class Accelerometer
    {
        private bool m_hasSource = false;
        private bool m_active = false;
        private AccelerometerRange m_range = AccelerometerRange.Range_2G;
        private double m_x = 0;
        private double m_y = 0;
        private double m_z = 0;

        private readonly Dictionary<string, Action<string, dynamic>> callbacks = new Dictionary<string, Action<string, dynamic>>();

        public void Register(string key, Action<string, dynamic> action, bool notify = false)
        {
            if (!callbacks.ContainsKey(key))
            {
                callbacks.Add(key, action);
            }
            else
            {
                callbacks[key] += action;
            }
        }
        protected virtual void OnPropertyChanged(dynamic value, [CallerMemberName] string propertyName = null)
        {
            Action<string, dynamic> v;
            var callback = callbacks.TryGetValue(propertyName, out v);

            if (callback)
            {
                v?.Invoke(propertyName, value);
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

        public bool Active
        {
            get { return m_active; }
            set
            {
                if (value == m_active) return;
                m_active = value;
                OnPropertyChanged(value);
            }
        }

        public AccelerometerRange Range
        {
            get { return m_range; }
            set
            {
                if (value == m_range) return;
                m_range = value;
                OnPropertyChanged(value);
            }
        }

        public double X
        {
            get { return m_x; }
            set
            {
                if (value.Equals(m_x)) return;
                m_x = value;
                OnPropertyChanged(value);
            }
        }

        public double Y
        {
            get { return m_y; }
            set
            {
                if (value.Equals(m_y)) return;
                m_y = value;
                OnPropertyChanged(value);
            }
        }

        public double Z
        {
            get { return m_z; }
            set
            {
                if (value.Equals(m_z)) return;
                m_z = value;
                OnPropertyChanged(value);
            }
        }
    }
}
