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

    public enum PIDSourceType
    {
        Displacement = 0,
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

        void SetPIDSourceType(PIDSourceType pidSource);

        PIDSourceType GetPIDSourceType();
    }
}
