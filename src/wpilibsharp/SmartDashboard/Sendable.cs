using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.SmartDashboard
{
    public interface Sendable
    {
        void InitSendable(SendableBuilder builder);
    }
}
