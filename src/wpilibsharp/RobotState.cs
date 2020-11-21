namespace WPILib
{
    public static class RobotState
    {
        private static readonly DriverStation dsInstance = DriverStation.Instance;

        public static bool IsDisabled => dsInstance.IsDisabled;
        public static bool IsTest => dsInstance.IsTest;
    }
}
