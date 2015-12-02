using System;
using System.Collections.Generic;

namespace HAL.Simulator
{
    /// <summary>
    /// This class allows us to listen for changes in a specific part of the dictionary. 
    /// </summary>
    /// <remarks>Not everything is wrapped, because we don't care about changes in certain things.
    /// <para/>Note that since the indexer is not marked virtual, if you set a value in the
    /// dictionary manually, the notify callback will not be called unless you first
    /// box the dictionary as a NotifyDict.
    /// </remarks>
    /// <typeparam name="TKey">Please use dynamic</typeparam>
    /// <typeparam name="TValue">Please use dynamic</typeparam>
    public class NotifyDict<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, Action<dynamic, dynamic>> m_callbacks = new Dictionary<TKey, Action<dynamic, dynamic>>();


        /// <summary>
        /// Register a notify function to get called when the field updates.
        /// </summary>
        /// <param name="key">The key to watch</param>
        /// <param name="action">The callback function</param>
        /// <param name="notify">Whether to notify immediately</param>
        public void Register(TKey key, Action<dynamic, dynamic> action, bool notify = false)
        {
            if (!ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(key), "Cannot register for non existent key");
            }
            if (!m_callbacks.ContainsKey(key))
            {
                m_callbacks.Add(key, action);
            }
            else
            {
                m_callbacks[key] += action;
            }
            if (notify)
            {
                m_callbacks[key]?.Invoke(key, this[key]);
            }
        }

        /// <summary>
        /// Cancel a notify function
        /// </summary>
        /// <param name="key">The key the function is waiting for</param>
        /// <param name="action">The callback function to cancel.</param>
        public void Cancel(TKey key, Action<dynamic, dynamic> action)
        {
            if (action != null && m_callbacks.ContainsKey(key)) m_callbacks[key] -= action;
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key. If a callback exists for the key, call it.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set
            {
                base[key] = value;
                if (m_callbacks.ContainsKey(key))
                {
                    m_callbacks[key]?.Invoke(key, value);
                }
            }
        }

    }
}