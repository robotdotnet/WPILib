namespace WPILib.Interfaces
{
    /// <summary>
    /// An interface for PID loops.
    /// </summary>
    /// <remarks>
    /// Used for either devices that contain PID functionality (i.e. CANTalon)
    /// or software PID implementations (i.e. <see cref="PIDController"/>).
    /// </remarks>
    public interface IPIDInterface: IController
    {
        /// <summary>
        /// Sets the P, I and D constants for the loop.
        /// </summary>
        /// <param name="p">The proportional gain constant.</param>
        /// <param name="i">The integral gain constant.</param>
        /// <param name="d">The derivative gain constant.</param>
        void SetPID(double p, double i, double d);
        /// <summary>
        /// Gets the proportaional gain constant.
        /// </summary>
        double P { get; }
        /// <summary>
        /// Gets the integral gain constant.
        /// </summary>
        double I { get; }
        /// <summary>
        /// Gets the derivative gain constant.
        /// </summary>
        double D { get; }
        /// <summary>
        /// Gets or Sets the loop setpoint.
        /// </summary>
        double Setpoint { get; set; }
        /// <summary>
        /// Gets the difference between the <see cref="Setpoint"/> and the actual position.
        /// </summary>
        /// <returns></returns>
        double GetError();
        /// <summary>
        /// Gets whether the control loop is enabled.
        /// </summary>
        bool Enabled { get; }
        /// <summary>
        /// Resets the PID controller.
        /// </summary>
        void Reset();
    }
}
