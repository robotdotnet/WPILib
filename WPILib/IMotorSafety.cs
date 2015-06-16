namespace WPILib
{
    /// <summary>
    /// Interface for creating Safe Motors.
    /// </summary>
    public interface IMotorSafety
    {
        /// <summary>
        /// Gets or Sets the expiration time of the motor.
        /// </summary>
        double Expiration { set; get; }
        /// <summary>
        /// Gets whether the motor is alive.
        /// </summary>
        bool Alive { get; }
        /// <summary>
        /// Stop the motor.
        /// </summary>
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
