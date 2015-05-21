using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.livewindow
{
    public interface LiveWindowSendable : Sendable
    {
        void UpdateTable();

        void StartLiveWindowMode();

        void StopLiveWindowMode();
    }
}
