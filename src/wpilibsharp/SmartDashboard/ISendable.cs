using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.SmartDashboard
{
    public interface ISendable
    {
        void InitSendable(ISendableBuilder builder);
    }
}
