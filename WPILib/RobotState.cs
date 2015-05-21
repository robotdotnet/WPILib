using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Util;

namespace WPILib
{
    public class RobotState
    {
        private static Interface impl;

        public static void SetImplementation(Interface i)
        {
            impl = i;
        }

        public static bool IsDisabled()
        {
            if (impl != null)
            {
                return impl.IsDisabled();
            }
            else
            {
                throw new BaseSystemNotInitializedException("Interface", "RobotState");
            }
        }

        public static bool IsEnabled()
        {
            if (impl != null)
            {
                return impl.IsEnabled();
            }
            else
            {
                throw new BaseSystemNotInitializedException("Interface", "RobotState");
            }
        }

        public static bool IsOperatorControl()
        {
            if (impl != null)
            {
                return impl.IsOperatorControl();
            }
            else
            {
                throw new BaseSystemNotInitializedException("Interface", "RobotState");
            }
        }

        public static bool IsAutonomous()
        {
            if (impl != null)
            {
                return impl.IsAutonomous();
            }
            else
            {
                throw new BaseSystemNotInitializedException("Interface", "RobotState");
            }
        }

        public static bool IsTest()
        {
            if (impl != null)
            {
                return impl.IsTest();
            }
            else
            {
                throw new BaseSystemNotInitializedException("Interface", "RobotState");
            }
        }

        public interface Interface
        {
            bool IsDisabled();
            bool IsEnabled();
            bool IsOperatorControl();
            bool IsAutonomous();
            bool IsTest();
        }
    }
}
