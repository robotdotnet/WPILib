namespace WPILib
{
    /// <summary>
    /// A description for the type of output value to provide to a PIDController
    /// </summary>
    public enum PIDSourceParameter
    {
        ///The source is a Distance
        Distance = 0,
        ///The source is a Rate
        Rate = 1,
        ///The source is an Angle
        Angle = 2,
    }

    /// <summary>
    /// This interface allows for PIDController to automatically read from this object
    /// </summary>
    public interface IPIDSource
    {
        /// <summary>
        /// Get the result to use in PIDController
        /// </summary>
        /// <returns>The result to use in PIDController</returns>
        double PidGet();
    }
}
