using WPILib.Util;

namespace WPILib
{
    public class Timer
    {
        private static StaticInterface s_impl;

        public static void SetImplementation(StaticInterface ti)
        {
            s_impl = ti;
        }

        /// <summary>
        /// Return the system clock time in seconds. Return the time from the
        /// FPGA hardware clock in seconds since the FPGA started.
        /// </summary>
        /// <returns>Robot running time in seconds</returns>
        public static double GetFPGATimestamp()
        {
            if (s_impl != null)
            {
                return s_impl.GetFPGATimestamp();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(Timer));
            }
        }

        /// <summary>
        /// Return the approximate match time
        /// <para />The FMS does not currently send the official match time to the robots
        /// <para />This returns the time since the enable signal sent from the Driver Station
        /// <para />At the beginning of autonomous, the time is reset to 0.0 seconds
        /// <para />At the beginning of teleop, the time is reset to +15.0 seconds
        /// <para />If the robot is disabled, this returns 0.0 seconds
        /// <para />Warning: This is not an official time (so it cannot be used to argue with referees)
        /// </summary>
        /// <returns>Match time in seconds since the beginning of autonomous</returns>
        public static double GetMatchTime()
        {
            if (s_impl != null)
            {
                return s_impl.GetMatchTime();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(Timer));
            }
        }

        /// <summary>
        /// Pause the thread for a specified time. Pause the execution of the
        /// thread for a specified period of time given in seconds. Motors will
        /// continue to run at their last assigned values, and sensors will continue
        /// to update. Only the task containing the wait will pause until the wait
        /// time is expired.
        /// </summary>
        /// <param name="seconds">Length of time to pause</param>
        public static void Delay(double seconds)
        {
            if (s_impl != null)
            {
                s_impl.Delay(seconds);
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(Timer));
            }
        }

        public interface StaticInterface
        {
            double GetFPGATimestamp();
            double GetMatchTime();
            void Delay(double seconds);
            Interface NewTimer();
        }

        private Interface m_timer;

        public Timer()
        {
            if (s_impl != null)
                m_timer = s_impl.NewTimer();
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(Timer));
            }
        }

        /// <summary>
        /// Get the current time from the timer. If the clock is running it is derived from
        /// the current system clock the start time stored in the timer class. If the clock
        /// is not running, then return the time when it was last stopped.
        /// </summary>
        /// <returns>Current time value for this timer in seconds</returns>
        public double Get()
        {
            return m_timer.Get();
        }

        /// <summary>
        /// Reset the timer by setting the time to 0.
        /// Make the timer startTime the current time so new requests will be relative now
        /// </summary>
        public void Reset()
        {
            m_timer.Reset();
        }

        /// <summary>
        /// Start the timer running.
        /// Just set the running flag to true indicating that all time requests should be
        /// relative to the system clock.
        /// </summary>
        public void Start()
        {
            m_timer.Start();
        }

        /// <summary>
        /// Stop the timer.
        /// This computes the time as of now and clears the running flag, causing all
        /// subsequent time requests to be read from the accumulated time rather than
        /// looking at the system clock.
        /// </summary>
        public void Stop()
        {
            m_timer.Stop();
        }

        /// <summary>
        /// Check if the period specified has passed and if it has, advance the start
        /// time by that period. This is useful to decide if it's time to do periodic
        /// work without drifting later by the time it took to get around to checking.
        /// </summary>
        /// <param name="period">The period to check for (in seconds)</param>
        /// <returns>If the period has passed.</returns>
        public bool HasPeriodPassed(double period)
        {
            return m_timer.HasPeriodPassed(period);
        }
        public interface Interface
        {
            double Get();
            void Reset();
            void Start();
            void Stop();
            bool HasPeriodPassed(double period);
        }
    }
}
