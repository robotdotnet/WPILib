using System.Collections.Generic;

namespace WPILib.SmartDashboardNS
{
    internal interface ISendableDictionary : IEnumerable<KeyValuePair<ISendable, SendableRegistry.Component>>
    {
        bool TryGetValue(ISendable key, out SendableRegistry.Component value);
        void Add(ISendable key, SendableRegistry.Component value);
        bool Remove(ISendable key);
    }
}
