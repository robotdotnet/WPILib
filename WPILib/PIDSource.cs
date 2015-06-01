namespace WPILib
{
    /// <summary>
    /// A description for the type of output value to provide to a PIDController
    /// </summary>
    public enum PIDSourceParameter
    {
        Distance = 0,
        Rate = 1,
        Angle = 2,
    }

    /// <summary>
    /// This interface allows for PIDController to automatically read from this object
    /// </summary>
    public interface PIDSource
    {
        /// <summary>
        /// Get the result to use in PIDController
        /// </summary>
        /// <returns>The result to use in PIDController</returns>
        double PidGet();
    }
}
