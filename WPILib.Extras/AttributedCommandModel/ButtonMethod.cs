namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// An enumeration that represents different methods on the <see cref="WPILib.Buttons.Button" /> class.
    /// </summary>
    public enum ButtonMethod
    {
        /// <summary>
        /// Starts when the button is pressed.
        /// </summary>
        WhenPressed,
        /// <summary>
        /// Starts when the button is released.
        /// </summary>
        WhenReleased,
        /// <summary>
        /// Runs while the button is held.
        /// </summary>
        WhileHeld,
        /// <summary>
        /// Toggles the command on or off when pressed.
        /// </summary>
        ToggleWhenPressed,
        /// <summary>
        /// Cancels the command when pressed.
        /// </summary>
        CancelWhenPressed
    }

}
