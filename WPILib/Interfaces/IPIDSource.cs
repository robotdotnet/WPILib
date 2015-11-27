namespace WPILib.Interfaces
{
    /*
    /// <summary>
    /// A description for the type of output value to provide to a <see cref="PIDController"/>
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
    */

    /// <summary>
    /// The PID source type for this PID source.
    /// </summary>
    public enum PIDSourceType
    {
        /// <summary>
        /// Use displacement as the source.
        /// </summary>
        Displacement = 0,
        /// <summary>
        /// Use rate as the source
        /// </summary>
        Rate = 1,
    }

    /// <summary>
    /// This interface allows for <see cref="PIDController"/> to automatically read from this object
    /// </summary>
    public interface IPIDSource
    {
        /// <summary>
        /// Get the result to use in <see cref="PIDController"/>
        /// </summary>
        /// <returns>The result to use in <see cref="PIDController"/></returns>
        double PidGet();

        /// <summary>
        /// Gets or sets the Source type for this <see cref="IPIDSource"/>.
        /// </summary>
        PIDSourceType PIDSourceType { get; set; }
    }
}
