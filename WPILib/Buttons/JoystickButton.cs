
namespace WPILib.Buttons
{
    class JoystickButton : Button
    {
        private GenericHID m_joystick;
        private int m_buttonNumber;

        /// <summary>
        /// Create a joystick button for triggering commands
        /// </summary>
        /// <param name="joystick">The GenericHID object that has the button (e.g Joystick, KinectStick, etc)</param>
        /// <param name="buttonNumber">The button number (see <see cref="GenericHID.GetRawButton(int)"/>)</param>
        public JoystickButton(GenericHID joystick, int buttonNumber)
        {
            m_joystick = joystick;
            m_buttonNumber = buttonNumber;
        }

        /// <summary>
        /// Gets the value of the joytick button
        /// </summary>
        /// <returns>The value of the joystick button</returns>
        public override bool Get()
        {
            return m_joystick.GetRawButton(m_buttonNumber);
        }
    }
}
