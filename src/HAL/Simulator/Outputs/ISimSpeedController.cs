namespace HAL.Simulator.Outputs
{
    /// <summary>
    /// Interface for working with Speed Controllers in the simulator
    /// </summary>
    public interface ISimSpeedController
    {
        /// <summary>
        /// Gets the current value commanded by the robot to the motor.
        /// </summary>
        /// <returns>The current value commanded by the robot code.</returns>
        double Get();
    }
}
