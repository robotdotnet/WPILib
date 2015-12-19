
using System;

namespace WPILib.Buttons
{
    /// <summary>
    /// Creates a new command button that uses a joytick button.
    /// </summary>
    public class JoystickButton : Button, IEquatable<JoystickButton>
    {
        /// <summary>
        /// The joystick the bbutton is on.
        /// </summary>
        public GenericHID Joystick { get; }
        /// <summary>
        /// The button number on the joystick.
        /// </summary>
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

        /// <inheritdoc/>
        public bool Equals(JoystickButton other)
        {
            return other != null && Joystick.Equals(other.Joystick) && ButtonNumber == other.ButtonNumber;
        }
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return base.Equals(obj as Joystick);
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Joystick.GetHashCode() * 13 + ButtonNumber;
        }
    }
}
