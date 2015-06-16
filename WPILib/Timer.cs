using WPILib.Exceptions;

namespace WPILib
{
    /// <summary>
    /// WPILib Timer Class
    /// </summary>
    public class Timer
    {
        //TODO: Add remakrs to these methods.
        internal static IStaticInterface Implementation { private get; set; }

        /// <summary>
        /// Return the system clock time in seconds.
        /// </summary><remarks>Return the time from the
        /// FPGA hardware clock in seconds since the FPGA started.
        /// </remarks>
        public static double FPGATimestamp
        {
            get
            {
                if (Implementation != null)
                {
                    return Implementation.FPGATimestamp;
                }
                throw new BaseSystemNotInitializedException(Implementation, typeof (Timer));
            }
        }

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
        public static double MatchTime
        {
            get
            {
                if (Implementation != null)
                {
                    return Implementation.MatchTime;
                }
                throw new BaseSystemNotInitializedException(Implementation, typeof(Timer));
            }
        }

        /// <summary>
        /// Pause the thread for a specified time.</summary>
        /// <remarks>Pause the execution of the
        /// thread for a specified period of time given in seconds. Motors will
        /// continue to run at their last assigned values, and sensors will continue
        /// to update. Only the task containing the wait will pause until the wait
        /// time is expired.
        /// </remarks>
        /// <param name="seconds">Length of time to pause</param>
        public static void Delay(double seconds)
        {
            if (Implementation != null)
            {
                Implementation.Delay(seconds);
            }
            else
            {
                throw new BaseSystemNotInitializedException(Implementation, typeof(Timer));
            }
        }
        public interface IStaticInterface
        {
            double FPGATimestamp { get; }
            double MatchTime { get; }
            void Delay(double seconds);
            Interface NewTimer();
        }

        private Interface m_timer;

        public Timer()
        {
            if (Implementation != null)
                m_timer = Implementation.NewTimer();
            else
            {
                throw new BaseSystemNotInitializedException(Implementation, typeof(Timer));
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

        /// <summary>
        /// Interface for a Timer
        /// </summary>
        // ReSharper disable once InconsistentNaming
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
