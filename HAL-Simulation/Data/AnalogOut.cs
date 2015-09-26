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
    public class AnalogOutData
    {
        private double m_voltage = 0.0;
        private bool m_hasSource = false;
        private bool m_initialized = false;
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

        public bool Initialized
        {
            get { return m_initialized;}
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
