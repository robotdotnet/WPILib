using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Annotations;

namespace HAL_Simulator.Data
{
    internal class Dio
    {
        private bool m_initialized = false;
        private bool m_hasSource = false;
        private bool m_value = true;
        private double m_pulseLength = 0;
        private bool m_isInput = true;
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

        public bool Value
        {
            get { return m_value; }
            set
            {
                if (value == m_value) return;
                m_value = value;
                OnPropertyChanged(value);
            }
        }

        public double PulseLength
        {
            get { return m_pulseLength; }
            set
            {
                if (value.Equals(m_pulseLength)) return;
                m_pulseLength = value;
                OnPropertyChanged(value);
            }
        }

        public bool IsInput
        {
            get { return m_isInput; }
            set
            {
                if (value == m_isInput) return;
                m_isInput = value;
                OnPropertyChanged(value);
            }
        }
    }
}
