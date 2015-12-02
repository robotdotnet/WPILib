using System;
using System.Diagnostics;
using System.Threading;

namespace HAL.Simulator
{
    /// <summary>
    /// This class contains static hooks that can be used by simulators to get specific data.
    /// </summary>
    public static class SimHooks
    {
        private static Stopwatch s_timer = new Stopwatch();

        internal static void RestartTiming()
        {
            s_timer.Restart();
        }

        /// <summary>
        /// Gets the FPGA time.
        /// </summary>
        /// <remarks>This value is in increments of 10 nanoseconds. Use <see cref="GetFPGATimestamp"/>
        /// to get the timestamp in seconds.</remarks>
        /// <returns>The FPGA time on a 10 nanosecond scale.</returns>
        public static long GetFPGATime()
        {
            var seconds = (double)s_timer.ElapsedTicks / Stopwatch.Frequency;
            return (long)(seconds * 1000000);
        }

        /// <summary>
        /// Gets the FPGA time in seconds.
        /// </summary>
        /// <returns>The FPGA time in seconds</returns>
        public static double GetFPGATimestamp()
        {
            return (double)s_timer.ElapsedTicks / Stopwatch.Frequency;
        }

        /// <summary>
        /// Gets the current number of 100 nanosecond ticks from the system.
        /// </summary>
        /// <returns></returns>
        public static long GetTime()
        {
            return (DateTime.UtcNow.Ticks);
        }

        /// <summary>
        /// Delays the current thread by a number of milliseconds.
        /// </summary>
        /// <param name="ms">Delay time</param>
        public static void DelayMillis(double ms)
        {
            int milliSeconds = (int) ms;
            var sw = Stopwatch.StartNew();

            if (milliSeconds >= 20)
            {
                Thread.Sleep(milliSeconds - 12);
            }
            while (sw.ElapsedMilliseconds < milliSeconds) ;
        }

        /// <summary>
        /// Delays the current thread by a number of seconds.
        /// </summary>
        /// <param name="s">Delay time</param>
        public static void DelaySeconds(double s)
        {
            int milliSeconds = (int)(s * 1e3);
            var sw = Stopwatch.StartNew();

            if (milliSeconds >= 20)
            {
                Thread.Sleep(milliSeconds - 12);
            }
            while (sw.ElapsedMilliseconds < milliSeconds) ;
        }

        /// <summary>
        /// Waits for the robot program to say it has started. In the 2 primary robot bases, this is called when RobotInit returns.
        /// </summary>
        public static void WaitForProgramStart()
        {
            int count = 0;
            Console.WriteLine("Waiting for program to start. If you are using a Gyro, this should take about 5 seconds. " +
                              "Otherwise it should start immediately.");
            while (!SimData.GlobalData.ProgramStarted)
            {
                count++;
                Console.WriteLine("Waiting for program start signal: " + count);
                Thread.Sleep(500);
            }
        }
    }
}
