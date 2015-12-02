using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HAL.Simulator.Data
{
    /// <summary>
    /// Data base with a notifier
    /// </summary>
    /// <seealso cref="DataBase" />
    public abstract class NotifyDataBase : DataBase
    {
        private readonly Dictionary<string, Action<string, dynamic>> m_callbacks = new Dictionary<string, Action<string, dynamic>>();

        /// <summary>
        /// Registers the specified key with a callback.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="action">The callabac.</param>
        /// <param name="initialNotify">if set to <c>true</c> [initial notify].</param>
        public void Register(string key, Action<string, dynamic> action, bool initialNotify = false)
        {
            if (!m_callbacks.ContainsKey(key))
            {
                m_callbacks.Add(key, action);
            }
            else
            {
                m_callbacks[key] += action;
            }

            if (initialNotify)
            {
                dynamic val;
                try
                {
                    val = GetType().GetProperty(key).GetValue(this, null);

                }
                catch (Exception)
                {
                    return;
                }
                action(key, val);
            }
        }
        /// <summary>
        /// Called when any property is changed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected override void OnPropertyChanged(dynamic value, [CallerMemberName] string propertyName = null)
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

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_callbacks.Clear();
        }
    }
}
