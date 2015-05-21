namespace WPILib.Extras
{
    /// <summary>
    /// Handle input from Xbox 360 or Xbox One controllers connected to the Driver Station.
    /// This class handles xbox input that comes from the Driver Station. Each time a value is requested
    /// the most recent value is returned. There is a single class instance for each joystick and the mapping
    /// of ports to hardware buttons depends on the code in the driver station.
    /// </summary>
    public class XboxController : Joystick
    {
        /// <summary>
        /// Get A Button
        /// </summary>
        public bool A
        {
            get { return GetRawButton(1); }
        }

        /// <summary>
        /// Get B Button
        /// </summary>
        public bool B
        {
            get { return GetRawButton(2); }
        }

        /// <summary>
        /// Get X Button
        /// </summary>
        public bool X
        {
            get { return GetRawButton(3); }
        }

        /// <summary>
        /// Get Y Button
        /// </summary>
        public bool Y
        {
            get { return GetRawButton(4); }
        }

        /// <summary>
        /// Get Left Stick Button 
        /// </summary>
        public bool LeftStickButton
        {
            get { return GetRawButton(9); }
        }

        /// <summary>
        /// Get Right Stick Button
        /// </summary>
        public bool RightStickButton
        {
            get { return GetRawButton(10); }
        }

        /// <summary>
        /// Get Left Bumper
        /// </summary>
        public bool LeftBumper
        {
            get { return GetRawButton(5); }
        }

        /// <summary>
        /// Get Right Bumper
        /// </summary>
        public bool RightBumper
        {
            get { return GetRawButton(6); }
        }

        /// <summary>
        /// Get Back Button
        /// </summary>
        public bool Back
        {
            get { return GetRawButton(7); }
        }

        /// <summary>
        /// Get Start Button
        /// </summary>
        public bool Start
        {
            get { return GetRawButton(8); }
        }

        /// <summary>
        /// Get Left X Axis
        /// </summary>
        public double LeftXAxis
        {
            get { return GetRawAxis(0); }
        }

        /// <summary>
        /// Get Left Y Axis
        /// </summary>
        public double LeftYAxis
        {
            get { return GetRawAxis(1); }
        }

        /// <summary>
        /// Get Right X Axis
        /// </summary>
        public double RightXAxis
        {
            get { return GetRawAxis(4); }
        }

        /// <summary>
        /// Get Right Y Axis
        /// </summary>
        public double RightYAxis
        {
            get { return GetRawAxis(5); }
        }

        /// <summary>
        /// Get Left Trigger
        /// </summary>
        public double LeftTrigger
        {
            get { return GetRawAxis(2); }
        }

        /// <summary>
        /// Get Right Trigger
        /// </summary>
        public double RightTrigger
        {
            get { return GetRawAxis(3); }
        }

        /// <summary>
        /// Set Left Rumble
        /// </summary>
        public double LeftRumble
        {
            set { SetRumble(RumbleType.LeftRumble, (float)value); }
        }

        /// <summary>
        /// Set Right Rumble
        /// </summary>
        public double RightRumble
        {
            set { SetRumble(RumbleType.RightRumble, (float)value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public XboxController(int port)
            : base(port)
        {

        }

    }
}
