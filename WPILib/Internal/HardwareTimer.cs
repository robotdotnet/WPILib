using System.Threading;
using static WPILib.Utility;

namespace WPILib.Internal
{
    /// <summary>
    /// Timer objects measure accumulated time in milliseconds.
    /// </summary>
    /// <remarks>    
    /// The timer object functions like a stopwatch.It can be started, stopped, and cleared. When the
    /// timer is running its value counts up in milliseconds.When stopped, the timer holds the current
    /// value. The implementation simply records the time when started and subtracts the current time
    /// whenever the value is requested.</remarks>
    public class HardwareTimer : Timer.IStaticTimerInterface
    {
        /// <summary>
        /// Returns the system clock time in seconds.
        /// </summary>
        public double GetFPGATimestamp() => GetFPGATime()/1000000.0;

        /// <summary>
        /// Returns the Match Time in seconds
        /// </summary>
        public double GetMatchTime() => DriverStation.Instance.GetMatchTime();

        /// <summary>
        /// Pause the thread for a specified time
        /// </summary>
        /// <param name="seconds">Length of time to pause</param>
        public void Delay(double seconds)
        {
            Thread.Sleep((int)(seconds * 1e3));
        }

        /// <summary>
        /// Creates a new Timer
        /// </summary>
        /// <returns>A new timer</returns>
        public Timer.ITimerInterface NewTimer()
        {
            return new TimerImpl();
        }

        /// <summary>
        /// A hardware timer implementation
        /// </summary>
        public class TimerImpl : Timer.ITimerInterface
        {
            internal long m_startTime;
            internal double m_accumulatedTime;
            internal bool m_running;

            private readonly object m_lockObject = new object();

            /// <summary>
            /// Create a new timer object
            /// </summary>
            /// <remarks>The timer starts at zero, and is initially not running</remarks>
            public TimerImpl()
            {
                Reset();
            }

            private static long MsClock => (long)GetFPGATime() / 1000;

            /// <summary>
            /// Get the current time from the timer
            /// </summary>
            /// <remarks>If clock is running, it returns the run time. If clock is 
            /// not running, it returns the time from when it was last stopped.</remarks>
            /// <returns>Current time in seconds</returns>
            public double Get()
            {
                lock (m_lockObject)
                {
                    if (m_running)
                    {
                        return ((MsClock - m_startTime) + m_accumulatedTime) / 1000.0;
                    }
                    else
                    {
                        return m_accumulatedTime;
                    }
                }
            }

            /// <summary>
            /// Reset the timer, and start the timer.
            /// </summary>
            public void Reset()
            {
                lock (m_lockObject)
                {
                    m_accumulatedTime = 0;
                    m_startTime = MsClock;
                }
            }

            /// <summary>
            /// Start the timer running
            /// </summary>
            public void Start()
            {
                lock (m_lockObject)
                {
                    m_startTime = MsClock;
                    m_running = true;
                }
            }

            /// <summary>
            /// Stop the timer
            /// </summary>
            public void Stop()
            {
                lock (m_lockObject)
                {
                    double temp = Get();
                    m_accumulatedTime = temp;
                    m_running = false;
                }
            }

            /// <summary>
            /// Check if the specified period has passed.
            /// If so, advance the start time by that period.
            /// </summary>
            /// <remarks>Advancing the period makes the start times not drift.</remarks>
            /// <param name="period">The period to check for (seconds)</param>
            /// <returns>If the period has passed.</returns>
            public bool HasPeriodPassed(double period)
            {
                lock (m_lockObject)
                {
                    if (Get() > period)
                    {
                        m_startTime += (long)(period * 1000);
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}
