namespace WPILib.Extras
{
    /// <summary>
    /// Handle input from Xbox 360 or Xbox One controllers connected to the Driver Station.
    /// </summary><remarks>
    /// This class handles xbox input that comes from the Driver Station. Each time a value is requested
    /// the most recent value is returned. There is a single class instance for each joystick and the mapping
    /// of ports to hardware buttons depends on the code in the driver station.
    /// </remarks>
    public class XboxController : Joystick
    {
        /// <summary>
        /// Get A Button
        /// </summary>
        public bool A => GetRawButton(1);

        /// <summary>
        /// Get B Button
        /// </summary>
        public bool B => GetRawButton(2);

        /// <summary>
        /// Get X Button
        /// </summary>
        public bool X => GetRawButton(3);

        /// <summary>
        /// Get Y Button
        /// </summary>
        public bool Y => GetRawButton(4);

        /// <summary>
        /// Get Left Stick Button 
        /// </summary>
        public bool LeftStickButton => GetRawButton(9);

        /// <summary>
        /// Get Right Stick Button
        /// </summary>
        public bool RightStickButton => GetRawButton(10);

        /// <summary>
        /// Get Left Bumper
        /// </summary>
        public bool LeftBumper => GetRawButton(5);

        /// <summary>
        /// Get Right Bumper
        /// </summary>
        public bool RightBumper => GetRawButton(6);

        /// <summary>
        /// Get Back Button
        /// </summary>
        public bool Back => GetRawButton(7);

        /// <summary>
        /// Get Start Button
        /// </summary>
        public bool Start => GetRawButton(8);

        /// <summary>
        /// Get Left X Axis
        /// </summary>
        public double LeftXAxis => GetRawAxis(0);

        /// <summary>
        /// Get Left Y Axis
        /// </summary>
        public double LeftYAxis => GetRawAxis(1);

        /// <summary>
        /// Get Right X Axis
        /// </summary>
        public double RightXAxis => GetRawAxis(4);

        /// <summary>
        /// Get Right Y Axis
        /// </summary>
        public double RightYAxis => GetRawAxis(5);

        /// <summary>
        /// Get Left Trigger
        /// </summary>
        public double LeftTrigger => GetRawAxis(2);

        /// <summary>
        /// Get Right Trigger
        /// </summary>
        public double RightTrigger => GetRawAxis(3);

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
        /// Initializes an instance of the <see cref="XboxController"/> class
        /// </summary>
        /// <param name="port">The port the controller is in.</param>
        public XboxController(int port)
            : base(port)
        {

        }

    }
}
