namespace WPILib
{
    /// <summary>
    /// An interface for controllers. Controllers run control loops, the most command
    /// are PID controllers and there variants, but this includes anything that is
    /// controlling an actuator in a separate thread.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Alllows the control loop to run
        /// </summary>
        void Enable();

        /// <summary>
        /// Stops the control loop from running until explicitly re-enabled by calling
        /// <see cref="Enable()"/>
        /// </summary>
        void Disable();
    }
}
