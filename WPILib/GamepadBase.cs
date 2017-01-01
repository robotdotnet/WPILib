namespace WPILib
{
    /// <summary>
    /// Base for all Gamepads
    /// </summary>
    public abstract class GamepadBase : GenericHID
    {
        /// <summary>
        /// Creates a new Gamepad on the specified port
        /// </summary>
        /// <param name="port">The joystick port</param>
        protected GamepadBase(int port) : base(port)
        {

        }

        /// <summary>
        /// Gets if the bumper is pressed
        /// </summary>
        /// <returns>True if the bumper is pressed</returns>
        public bool GetBumper()
        {
            return GetBumper(JoystickHand.Right);
        }

        /// <summary>
        /// Gets if the bupper is pressed
        /// </summary>
        /// <param name="hand">The hand to check</param>
        /// <returns>True if the bumper is pressed</returns>
        public abstract bool GetBumper(JoystickHand hand);

        /// <summary>
        /// Gets if the button on the stick is pressed
        /// </summary>
        /// <param name="hand">The hand to check</param>
        /// <returns>True if the button is pressed</returns>
        public abstract bool GetStickButton(JoystickHand hand);
    }
}
