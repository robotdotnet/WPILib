using WPILib.Util;

namespace WPILib
{
    public class HLUsageReporting
    {
        private static Interface s_impl;

        public static void SetImplementation(Interface i)
        {
            s_impl = i;
        }

        public static void ReportScheduler()
        {
            if (s_impl != null)
            {
                s_impl.ReportScheduler();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(HLUsageReporting));
            }
        }

        public static void ReportPIDController(int num)
        {
            if (s_impl != null)
            {
                s_impl.ReportPIDController(num);
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(HLUsageReporting));
            }
        }

        public static void ReportSmartDashboard()
        {
            if (s_impl != null)
            {
                s_impl.ReportSmartDashboard();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(HLUsageReporting));
            }
        }

        public interface Interface
        {
            void ReportScheduler();
            void ReportPIDController(int num);
            void ReportSmartDashboard();
        }
        
        public class Null : Interface
        {
            public void ReportScheduler()
            {

            }

            public void ReportPIDController(int num)
            {

            }

            public void ReportSmartDashboard()
            {

            }
        }
    }
}
