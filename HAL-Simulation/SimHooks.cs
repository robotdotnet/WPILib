using System;
using System.Threading;

namespace HAL_Simulator
{
    /// <summary>
    /// This class contains static hooks that can be used by simulators to get specific data.
    /// </summary>
    public static class SimHooks
    {
        /// <summary>
        /// Gets the FPGA time.
        /// </summary>
        /// <remarks>This value is in increments of 10 nanoseconds. Use <see cref="GetFPGATimestamp"/>
        /// to get the timestamp in seconds.</remarks>
        /// <returns>The FPGA time on a 10 nanosecond scale.</returns>
        public static long GetFPGATime()
        {
            return (long)((DateTime.Now.Ticks - SimData.halData["time"]["program_start"]) / 10.0);
        }

        /// <summary>
        /// Gets the FPGA time in seconds.
        /// </summary>
        /// <returns>The FPGA time in seconds</returns>
        public static double GetFPGATimestamp()
        {
            return GetFPGATime()/ 1000000.0;
        }

        /// <summary>
        /// Gets the current number of 100 nanosecond ticks from the system.
        /// </summary>
        /// <returns></returns>
        public static long GetTime()
        {
            return (DateTime.Now.Ticks);
        }

        /// <summary>
        /// Delays the current thread by a number of milliseconds.
        /// </summary>
        /// <param name="ms">Delay time</param>
        public static void DelayMillis(double ms)
        {
            Thread.Sleep((int)ms);
        }

        /// <summary>
        /// Delays the current thread by a number of seconds.
        /// </summary>
        /// <param name="s">Delay time</param>
        public static void DelaySeconds(double s)
        {
            Thread.Sleep((int)s * 1000);
        } 
    }
}
