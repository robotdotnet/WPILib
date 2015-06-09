namespace WPILib
{
    /// <summary>
    /// This interface allows PIDController to write it's results to its output.
    /// </summary>
    public interface IPIDOutput
    {
        /// <summary>
        /// Set the output to the value calculated by PIDController
        /// </summary>
        /// <value>Output the value calculated by PIDController</value>
        double PidWrite { set; }
    }
}
