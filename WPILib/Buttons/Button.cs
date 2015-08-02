using WPILib.Commands;

namespace WPILib.Buttons
{
    /// <summary>
    /// This class provides an easy way to link commands to OI inputs.
    /// </summary>
    /// <remarks>
    /// It is very easy to link a button to a command.  For instance, you could
    /// link the trigger button of a joystick to a "score" command.
    ///
    /// <para/>This class represents a subclass of Trigger that is specifically aimed at
    /// buttons on an operator interface as a common use case of the more generalized
    /// Trigger objects. This is a simple wrapper around Trigger with the method names
    /// renamed to fit the Button object use.
    /// </remarks>
    public abstract class Button : Trigger
    {
        /// <summary>
        /// Starts the given command whenever the button is newly pressed
        /// </summary>
        /// <param name="command">The command to start</param>
        public void WhenPressed(Command command)
        {
            WhenActive(command);
        }

        /// <summary>
        /// Constantly starts the given command while the button is held.
        /// </summary><remarks>
        /// <see cref="Command.Start()"/> will be called repeatedly while the button is held,
        /// and will be canceled when the button is released.
        /// </remarks>
        /// <param name="command">The command to start</param>
        public void WhileHeld(Command command)
        {
            WhileActive(command);
        }

        /// <summary>
        /// Starts the command when the button is released
        /// </summary>
        /// <param name="command">The command to start</param>
        public void WhenReleased(Command command)
        {
            WhenInactive(command);
        }

        /// <summary>
        /// Toggles the command whenever the button is pressed (on then off then on)
        /// </summary>
        /// <param name="command">The command to start</param>
        public void ToggleWhenPressed(Command command)
        {
            ToggleWhenActive(command);
        }

        /// <summary>
        /// Cancel the command when the button is pressed.
        /// </summary>
        /// <param name="command">The command to start</param>
        public void CancelWhenPressed(Command command)
        {
            CancelWhenActive(command);
        }
    }
}
