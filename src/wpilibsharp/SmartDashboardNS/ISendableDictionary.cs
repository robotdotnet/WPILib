using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WPILib.SmartDashboardNS
{
    internal interface ISendableDictionary : IEnumerable<KeyValuePair<ISendable, SendableRegistry.Component>>
    {
        bool TryGetValue(ISendable key, [MaybeNullWhen(false)]out SendableRegistry.Component value);
        void Add(ISendable key, SendableRegistry.Component value);
        bool Remove(ISendable key);
    }
}
