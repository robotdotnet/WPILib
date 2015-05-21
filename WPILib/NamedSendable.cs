using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib
{
    public interface NamedSendable : Sendable
    {
        string GetName();
    }
}
