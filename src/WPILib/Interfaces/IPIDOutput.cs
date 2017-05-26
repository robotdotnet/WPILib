namespace WPILib.Interfaces
{
    /// <summary>
    /// This interface allows <see cref="PIDController"/> to write it's results to its output.
    /// </summary>
    public interface IPIDOutput
    {
        /// <summary>
        /// Set the output to the value calculated by <see cref="PIDController"/>
        /// </summary>
        /// <param name="value">Output the value calculated by <see cref="PIDController"/></param>
        void PidWrite(double value);
    }
}
