using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WPILib.SmartDashboardNS
{
    internal class StrongSendableDictionary : ISendableDictionary
    {
        private readonly Dictionary<ISendable, SendableRegistry.Component> components = new Dictionary<ISendable, SendableRegistry.Component>();

        public void Add(ISendable key, SendableRegistry.Component value)
        {
            components.Add(key, value);
        }

        public IEnumerator<KeyValuePair<ISendable, SendableRegistry.Component>> GetEnumerator()
        {
            return components.GetEnumerator();
        }

        public bool Remove(ISendable key)
        {
            return components.Remove(key);
        }

        public bool TryGetValue(ISendable key, [MaybeNullWhen(false)]out SendableRegistry.Component value)
        {
            return components.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
