using WPILib.Exceptions;

namespace WPILib
{
    /// <summary>
    /// Support for High Level Usage Reporting.
    /// </summary>
    public class HLUsageReporting
    {
        /// <summary>
        /// Gets or Sets the HLUsageReporting interface.
        /// </summary>
        public static Interface Implementation { get; set; }

        /// <summary>
        /// Reports usage of the scheduler
        /// </summary>
        public static void ReportScheduler()
        {
            if (Implementation != null)
            {
                Implementation.ReportScheduler();
            }
            else
            {
                throw new BaseSystemNotInitializedException(Implementation, typeof(HLUsageReporting));
            }
        }

        /// <summary>
        /// Reports usages of a PID Controller
        /// </summary>
        /// <param name="num"></param>
        public static void ReportPIDController(int num)
        {
            if (Implementation != null)
            {
                Implementation.ReportPIDController(num);
            }
            else
            {
                throw new BaseSystemNotInitializedException(Implementation, typeof(HLUsageReporting));
            }
        }

        /// <summary>
        /// Reports usage of the Smart Dashboard.
        /// </summary>
        public static void ReportSmartDashboard()
        {
            if (Implementation != null)
            {
                Implementation.ReportSmartDashboard();
            }
            else
            {
                throw new BaseSystemNotInitializedException(Implementation, typeof(HLUsageReporting));
            }
        }

        /// <summary>
        /// The interface to use for High Level usage reporting.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public interface Interface
        {
            /// <summary>
            /// Reports the scheduler
            /// </summary>
            void ReportScheduler();
            /// <summary>
            /// Reports the PID Controller
            /// </summary>
            /// <param name="num"></param>
            void ReportPIDController(int num);
            /// <summary>
            /// Reports the SmartDashboard.
            /// </summary>
            void ReportSmartDashboard();
        }
    }
}
