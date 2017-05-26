using System;

namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// Run a command on a <see cref="WPILib.Buttons.JoystickButton"/> event.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class RunCommandOnJoystickAttribute : RunCommandAttribute
    {
        /// <summary>
        /// Gets the Id of the controller to run the command on.
        /// </summary>
        public int ControllerId { get; }
        /// <summary>
        /// Gets the Id of the button to run the command on.
        /// </summary>
        public int ButtonId { get; }
        /// <summary>
        /// Gets the <see cref="ButtonMethod"/> to run the command on.
        /// </summary>
        public ButtonMethod ButtonMethod { get; }

        /// <summary>
        /// Creates a new <see cref="RunCommandOnJoystickAttribute"/>.
        /// </summary>
        /// <param name="controllerId">The controller to run the command on.</param>
        /// <param name="buttonId">The button to run the command on.</param>
        /// <param name="buttonMethod">The <see cref="ButtonMethod"/> to run the command on.</param>
        public RunCommandOnJoystickAttribute(int controllerId, int buttonId, ButtonMethod buttonMethod)
        {
            ControllerId = controllerId;
            ButtonId = buttonId;
            ButtonMethod = buttonMethod;
        }
    }
}
