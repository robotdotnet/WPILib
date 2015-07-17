using WPILib.Exceptions;
using WPILib.Internal;

namespace WPILib
{
    /// <summary>
    /// This class is used to create timers
    /// </summary>
    /// <remarks>This will use the Hardware implementation of the timer that is set by the library.
    /// This can currently be found in <see cref="Internal.HardwareTimer"/></remarks>
    public class Timer
    {
        internal static IStaticTimerInterface Implementation { private get; set; }

        /// <summary>
        /// Return the system clock time in seconds.
        /// </summary><remarks>Return the time from the
        /// FPGA hardware clock in seconds since the FPGA started.
        /// </remarks>
        /// <returns>The FPGA timestamp in seconds.</returns>
        public static double GetFPGATimestamp()
        {
            if (Implementation != null)
            {
                return Implementation.GetFPGATimestamp();
            }
            throw new BaseSystemNotInitializedException(Implementation, typeof (Timer));
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
        /// <returns>Match time since the beginning of autonomous.</returns>
        public static double GetMatchTime()
        {
            if (Implementation != null)
            {
                return Implementation.GetMatchTime();
            }
            throw new BaseSystemNotInitializedException(Implementation, typeof (Timer));
        }

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
            if (Implementation != null)
            {
                Implementation.Delay(seconds);
            }
            else
            {
                throw new BaseSystemNotInitializedException(Implementation, typeof(Timer));
            }
        }

        /// <summary>
        /// This interface is used to specify the static timer functions to be used by the <see cref="Timer"/> class.
        /// </summary>
        public interface IStaticTimerInterface
        {
            /// <inheritdoc cref="HardwareTimer.GetFPGATimestamp"/>
            double GetFPGATimestamp();
            /// <inheritdoc cref="HardwareTimer.GetMatchTime"/>
            double GetMatchTime();
            /// <inheritdoc cref="HardwareTimer.Delay"/>
            void Delay(double seconds);
            /// <inheritdoc cref="HardwareTimer.NewTimer"/>
            ITimerInterface NewTimer();
        }

        private ITimerInterface m_timer;

        /// <summary>
        /// Creates a new Timer
        /// </summary>
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
        /// Get the current time from the timer.</summary>
        /// <remarks>If the clock is running it is derived from
        /// the current system clock the start time stored in the timer class. If the clock
        /// is not running, then return the time when it was last stopped.
        /// </remarks>
        /// <returns>Current time value for this timer in seconds</returns>
        public double Get()
        {
            return m_timer.Get();
        }

        /// <summary>
        /// Reset the timer by setting the time to 0.
        /// </summary><remarks>
        /// Make the timer start time the current time so new requests will be relative now
        /// </remarks>
        public void Reset()
        {
            m_timer.Reset();
        }

        /// <summary>
        /// Start the timer running.
        /// </summary><remarks>
        /// Just set the running flag to true indicating that all time requests should be
        /// relative to the system clock.
        /// </remarks>
        public void Start()
        {
            m_timer.Start();
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
            m_timer.Stop();
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
            return m_timer.HasPeriodPassed(period);
        }

        /// <summary>
        /// This interface is used to specify the instance timer functions to be used by the <see cref="Timer"/> class.
        /// </summary>
        public interface ITimerInterface
        {
            /// <inheritdoc cref="HardwareTimer.TimerImpl.Get"/>
            double Get();
            /// <inheritdoc cref="HardwareTimer.TimerImpl.Reset"/>
            void Reset();
            /// <inheritdoc cref="HardwareTimer.TimerImpl.Start"/>
            void Start();
            /// <inheritdoc cref="HardwareTimer.TimerImpl.Stop"/>
            void Stop();
            /// <inheritdoc cref="HardwareTimer.TimerImpl.HasPeriodPassed"/>
            bool HasPeriodPassed(double period);
        }
    }
}
