using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Util;

namespace WPILib
{
    public class RobotState
    {
        private static Interface s_impl;

        public static void SetImplementation(Interface i)
        {
            s_impl = i;
        }

        public static bool IsDisabled()
        {
            if (s_impl != null)
            {
                return s_impl.IsDisabled();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(RobotBase));
            }
        }

        public static bool IsEnabled()
        {
            if (s_impl != null)
            {
                return s_impl.IsEnabled();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(RobotBase));
            }
        }

        public static bool IsOperatorControl()
        {
            if (s_impl != null)
            {
                return s_impl.IsOperatorControl();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(RobotBase));
            }
        }

        public static bool IsAutonomous()
        {
            if (s_impl != null)
            {
                return s_impl.IsAutonomous();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(RobotBase));
            }
        }

        public static bool IsTest()
        {
            if (s_impl != null)
            {
                return s_impl.IsTest();
            }
            else
            {
                throw new BaseSystemNotInitializedException(s_impl, typeof(RobotBase));
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
