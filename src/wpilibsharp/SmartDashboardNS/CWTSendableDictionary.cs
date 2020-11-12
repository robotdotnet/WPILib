using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WPILib.SmartDashboardNS
{
    internal class CWTSendableDictionary : ISendableDictionary
    {
        private readonly ConditionalWeakTable<ISendable, SendableRegistry.Component> components = new ConditionalWeakTable<ISendable, SendableRegistry.Component>();
        private readonly Func<IEnumerator<KeyValuePair<ISendable, SendableRegistry.Component>>> enumeratorGetter;

        public CWTSendableDictionary(MethodInfo getEnumeratorMethod)
        {
            enumeratorGetter = (Func<IEnumerator<KeyValuePair<ISendable, SendableRegistry.Component>>>)getEnumeratorMethod.CreateDelegate(typeof(Func<IEnumerator<KeyValuePair<ISendable, SendableRegistry.Component>>>), components);
        }

        public void Add(ISendable key, SendableRegistry.Component value)
        {
            components.Add(key, value);
        }

        public IEnumerator<KeyValuePair<ISendable, SendableRegistry.Component>> GetEnumerator()
        {
            return enumeratorGetter();
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
