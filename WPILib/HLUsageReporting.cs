using WPILib.Exceptions;

namespace WPILib
{
    /// <summary>
    /// Support for High Level Usage Reporting.
    /// </summary>
    public class HLUsageReporting
    {
        public static Interface Implementation { get; set; }

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
        public interface Interface
        {
            void ReportScheduler();
            void ReportPIDController(int num);
            void ReportSmartDashboard();
        }
    }
}
