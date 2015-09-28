using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Data
{
    public abstract class NotifyDataBase : DataBase
    {
        private readonly Dictionary<string, Action<string, dynamic>> m_callbacks = new Dictionary<string, Action<string, dynamic>>();

        public void Register(string key, Action<string, dynamic> action)
        {
            if (!m_callbacks.ContainsKey(key))
            {
                m_callbacks.Add(key, action);
            }
            else
            {
                m_callbacks[key] += action;
            }
        }
        protected virtual void OnPropertyChanged(dynamic value, [CallerMemberName] string propertyName = null)
        {
            Action<string, dynamic> v;
            var callback = m_callbacks.TryGetValue(propertyName, out v);

            if (callback)
            {
                v?.Invoke(propertyName, value);
            }
        }

        /// <summary>
        /// Cancel a notify function
        /// </summary>
        /// <param name="key">The key the function is waiting for</param>
        /// <param name="action">The callback function to cancel.</param>
        public void Cancel(string key, Action<string, dynamic> action)
        {
            if (action != null && m_callbacks.ContainsKey(key)) m_callbacks[key] -= action;
        }
    }
}
