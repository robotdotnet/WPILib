using System.Diagnostics;
using System.Threading;
using WPILib.Exceptions;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// This class is used to create timers
    /// </summary>
    /// <remarks>This will use the Hardware implementation of the timer that is set by the library.
    ///</remarks>
    public class Timer
    {

        /// <summary>
        /// Return the system clock time in seconds.
        /// </summary><remarks>Return the time from the
        /// FPGA hardware clock in seconds since the FPGA started.
        /// </remarks>
        /// <returns>The FPGA timestamp in seconds.</returns>
        public static double GetFPGATimestamp() => GetFPGATime()/1000000.0;

        /// <summary>
        /// Return the approximate match time since the beginning of autonomous </summary>
        /// <remarks>
        /// The FMS does not currently send the official match time to the robots
        /// This returns the time since the enable signal sent from the Driver Station
        /// At the beginning of autonomous, the time is reset to 0.0 seconds
        /// At the beginning of teleop, the time is reset to +15.0 seconds
        /// If the robot is disabled, this returns 0.0 seconds
        /// <para />Warning: This is not an official time (so it cannot be used to argue with referees)
        /// </remarks>
        /// <returns>Match time since the beginning of autonomous.</returns>
        public static double GetMatchTime() => DriverStation.Instance.GetMatchTime();

        /// <summary>
        /// Pause the thread for a specified time.</summary>
        /// <remarks>Pause the execution of the
        /// thread for a specified period of time given in seconds. Motors will
        /// continue to run at their last assigned values, and sensors will continue
        /// to update. Only the task containing the wait will pause until the wait
        /// time is expired.
        /// </remarks>
        /// <param name="seconds">Length of time to pause (seconds)</param>
        public static void Delay(double seconds)
        {
            Thread.Sleep((int)(seconds * 1e3));
        }

        /// <summary>
        /// Puases the thread for a specific time using a SpinLoop
        /// </summary>
        /// <param name="seconds"></param>
        public static void PreciseDelay(double seconds)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double ticks = (Stopwatch.Frequency * seconds);

            int milliSeconds = (int)(seconds * 1e3);

            if (milliSeconds >= 20)
            {
                Thread.Sleep(milliSeconds - 12);
            }
            // ReSharper disable once EmptyEmbeddedStatement
            while (sw.ElapsedTicks < ticks) ;
            sw.Stop();
        }

        internal double m_startTime;
        internal double m_accumulatedTime;
        internal bool m_running;

        private readonly object m_lockObject = new object();

        /// <summary>
        /// Creates a new Timer
        /// </summary>
        public Timer()
        {
            Reset();
        }

        private static double MsClock => GetFPGATime() / 1000.0;

        /// <summary>
        /// Get the current time from the timer.</summary>
        /// <remarks>If the clock is running it is derived from
        /// the current system clock the start time stored in the timer class. If the clock
        /// is not running, then return the time when it was last stopped.
        /// </remarks>
        /// <returns>Current time value for this timer in seconds</returns>
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
        /// Reset the timer by setting the time to 0.
        /// </summary><remarks>
        /// Make the timer start time the current time so new requests will be relative now
        /// </remarks>
        public void Reset()
        {
            lock (m_lockObject)
            {
                m_accumulatedTime = 0;
                m_startTime = MsClock;
            }
        }

        /// <summary>
        /// Start the timer running.
        /// </summary><remarks>
        /// Just set the running flag to true indicating that all time requests should be
        /// relative to the system clock.
        /// </remarks>
        public void Start()
        {
            lock (m_lockObject)
            {
                m_startTime = MsClock;
                m_running = true;
            }
        }

        /// <summary>
        /// Stop the timer. </summary>
        /// <remarks>
        /// This computes the time as of now and clears the running flag, causing all
        /// subsequent time requests to be read from the accumulated time rather than
        /// looking at the system clock.
        /// </remarks>
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
        /// Check if the period specified has passed and if it has, advance the start
        /// time by that period.</summary>
        /// <remarks>This is useful to decide if it's time to do periodic
        /// work without drifting later by the time it took to get around to checking.
        /// </remarks>
        /// <param name="period">The period to check for (in seconds)</param>
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
