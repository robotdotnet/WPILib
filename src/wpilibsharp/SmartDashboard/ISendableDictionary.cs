using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.SmartDashboard
{
    internal interface ISendableDictionary : IEnumerable<KeyValuePair<ISendable, SendableRegistry.Component>>
    {
        bool TryGetValue(ISendable key, out SendableRegistry.Component value);
        void Add(ISendable key, SendableRegistry.Component value);
        bool Remove(ISendable key);
    }
}
