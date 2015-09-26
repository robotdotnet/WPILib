
using System;

namespace WPILib.Buttons
{
    public class JoystickButton : Button, IEquatable<JoystickButton>
    {

        public GenericHID Joystick { get; }
        public int ButtonNumber { get; }

        /// <summary>
        /// Create a joystick button for triggering commands
        /// </summary>
        /// <param name="joystick">The GenericHID object that has the button (e.g Joystick, KinectStick, etc)</param>
        /// <param name="buttonNumber">The button number (see <see cref="GenericHID.GetRawButton(int)"/>)</param>
        public JoystickButton(GenericHID joystick, int buttonNumber)
        {
            Joystick = joystick;
            ButtonNumber = buttonNumber;
        }

        /// <summary>
        /// Gets the value of the joystick button
        /// </summary>
        /// <returns>The value of the joystick button</returns>
        public override bool Get()
        {
            return Joystick.GetRawButton(ButtonNumber);
        }

        public bool Equals(JoystickButton other)
        {
            return other != null && Joystick.Equals(other.Joystick) && ButtonNumber == other.ButtonNumber;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Joystick);
        }

        public override int GetHashCode()
        {
            return Joystick.GetHashCode() * 13 + ButtonNumber;
        }
    }
}
