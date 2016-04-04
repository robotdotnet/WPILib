using WPILib.Exceptions;

namespace WPILib
{
    /// <summary>
    /// High level robot state reporting.
    /// </summary>
    public static class RobotState
    {

        /// <summary>
        /// Gets if robot is disabled
        /// </summary>
        public static bool Disabled => DriverStation.Instance.Disabled;

        /// <summary>
        /// Gets if robot is enabled.
        /// </summary>
        public static bool Enabled => DriverStation.Instance.Enabled;

        /// <summary>
        /// Gets if robot is operator control.
        /// </summary>
        public static bool OperatorControl => DriverStation.Instance.OperatorControl;

        /// <summary>
        /// Gets if robot is in autonomous.
        /// </summary>
        public static bool Autonomous => DriverStation.Instance.Autonomous;

        /// <summary>
        /// Gets if robot is in test
        /// </summary>
        public static bool Test => DriverStation.Instance.Test;
    }
}
