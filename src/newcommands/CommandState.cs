using System;
using System.Collections.Generic;
using System.Text;
using WPILib;

namespace WPILib2.Commands
{
    internal readonly struct CommandState
    {
        private readonly TimeSpan m_startTime;

        internal CommandState(bool interruptible)
        {
            IsInterruptible = interruptible;
            m_startTime = Timer.FPGATimestamp;
        }

        internal readonly bool IsInterruptible { get; }
        internal readonly TimeSpan TimeSinceInitialized =>
            m_startTime != System.Threading.Timeout.InfiniteTimeSpan ? Timer.FPGATimestamp - m_startTime : m_startTime;
    }
}
