namespace WPILib
{
    /// <summary>
    /// This interface allows PIDController to write it's results to its output.
    /// </summary>
    public interface PIDOutput
    {
        /// <summary>
        /// Set the output to the value calculated by PIDController
        /// </summary>
        /// <param name="output">Output the value calculated by PIDController</param>
        void PidWrite(double output);
    }
}
