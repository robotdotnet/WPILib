using WPILib.Exceptions;

namespace WPILib
{
    /// <summary>
    /// High level robot state reporting.
    /// </summary>
    public static class RobotState
    {
        /// <summary>
        /// Sets the implementation of the Robot State
        /// </summary>
        public static Interface Implementation { private get; set; }

        /// <summary>
        /// Gets if robot is disabled
        /// </summary>
        public static bool Disabled
        {
            get
            {
                if (Implementation != null)
                {
                    return Implementation.Disabled;
                }
                throw new BaseSystemNotInitializedException(Implementation, typeof (RobotBase));
            }
        }

        /// <summary>
        /// Gets if robot is enabled.
        /// </summary>
        public static bool Enabled
        {
            get
            {
                if (Implementation != null)
                {
                    return Implementation.Enabled;
                }
                throw new BaseSystemNotInitializedException(Implementation, typeof (RobotBase));
            }
        }

        /// <summary>
        /// Gets if robot is operator control.
        /// </summary>
        public static bool OperatorControl
        {
            get
            {
                if (Implementation != null)
                {
                    return Implementation.OperatorControl;
                }
                throw new BaseSystemNotInitializedException(Implementation, typeof (RobotBase));
            }
        }

        /// <summary>
        /// Gets if robot is in autonomous.
        /// </summary>
        public static bool Autonomous
        {
            get
            {
                if (Implementation != null)
                {
                    return Implementation.Autonomous;
                }
                throw new BaseSystemNotInitializedException(Implementation, typeof (RobotBase));
            }
        }

        /// <summary>
        /// Gets if robot is in test
        /// </summary>
        public static bool Test
        {
            get
            {
                if (Implementation != null)
                {
                    return Implementation.Test;
                }
                throw new BaseSystemNotInitializedException(Implementation, typeof (RobotBase));
            }
        }

        /// <summary>
        /// Interface for robot state reporters.
        /// </summary>
        public interface Interface
        {
            /// <summary>
            /// disabled
            /// </summary>
            bool Disabled { get; }
            /// <summary>
            /// enabled
            /// </summary>
            bool Enabled { get; }
            /// <summary>
            /// operater control
            /// </summary>
            bool OperatorControl { get; }
            /// <summary>
            /// autonomous
            /// </summary>
            bool Autonomous { get; }
            /// <summary>
            /// test
            /// </summary>
            bool Test{ get; }
        }
    }
}
