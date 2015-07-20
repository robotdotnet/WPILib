namespace WPILib
{
    /// <summary>
    /// Interface for creating Safe Motors.
    /// </summary>
    public interface IMotorSafety
    {
        /// <summary>
        /// Gets or Sets the expiration time of the motor in seconds.
        /// </summary>
        double Expiration { set; get; }
        /// <summary>
        /// Gets whether the motor is alive.
        /// </summary>
        bool Alive { get; }
        /// <summary>
        /// Stop the motor associated with this PWM object.
        /// </summary>
        /// <remarks>This is called by the MotorSafetyHelper object
        /// when it has timed out and need to stop the motor.</remarks>
        void StopMotor();
        /// <summary>
        /// Gets or Sets whether safety is enabled.
        /// </summary>
        bool SafetyEnabled { set; get; }
        /// <summary>
        /// Gets a description for the Safe Motor Object.
        /// </summary>
        string Description { get; }
    }
}
