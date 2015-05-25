using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Util;

namespace WPILib
{
    public class HLUsageReporting
    {
        private static Interface impl;

        public static void SetImplementation(Interface i)
        {
            impl = i;
        }

        public static void ReportScheduler()
        {
            if (impl != null)
            {
                impl.ReportScheduler();
            }
            else
            {
                throw new BaseSystemNotInitializedException("");
            }
        }

        public static void ReportPIDController(int num)
        {
            if (impl != null)
            {
                impl.ReportPIDController(num);
            }
            else
            {
                throw new BaseSystemNotInitializedException("");
            }
        }

        public static void ReportSmartDashboard()
        {
            if (impl != null)
            {
                impl.ReportSmartDashboard();
            }
            else
            {
                throw new BaseSystemNotInitializedException("");
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
