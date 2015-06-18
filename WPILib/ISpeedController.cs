namespace WPILib
{
    /// <summary>
    /// Interface for speed controlling devices
    /// </summary>
    public interface ISpeedController : IPIDOutput
    {
        /// <summary>
        /// Sets the output value for this speed controller.
        /// </summary>
        /// <remarks>
        /// The PWM value is set using a range of -1.0 to 1.0, appropriately
        /// scaling the value for the FPGA.
        /// </remarks>
        /// <param name="value">The output value between -1.0 and 1.0</param>
        void Set(double value);

        /// <summary>
        /// Returns the last value set to this speed controller.
        /// </summary>
        /// <remarks>
        /// The PWM value is returned using a range of -1.0 to 1.0, appropriately
        /// scaling the value from the FPGA.
        /// </remarks>
        /// <returns>The output value between -1.0 and 1.0</returns>
        double Get();

        /// <summary>
        /// Sets the output value for this speed controller.
        /// </summary>
        /// <remarks>
        /// The PWM value is set using a range of -1.0 to 1.0, appropriately
        /// scaling the value for the FPGA.
        /// </remarks>
        /// <param name="value">The output value between -1.0 and 1.0</param>
        /// <param name="syncGroup">The update group to add this Set() to, pending UpdateSyncGroup().  If 0, update immediately.</param>
        void Set(double value, byte syncGroup);

        /// <summary>
        /// Disable the speed controller.
        /// </summary>
        /// <remarks>
        /// If this speed controller is set up as a <see cref="IMotorSafety">Safe Motor</see>,
        /// this will be called if the controller does not get updated within the safety period.
        /// </remarks>
        void Disable();
    }
}
