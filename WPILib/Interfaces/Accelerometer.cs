
namespace WPILib.Interfaces
{
    public enum Range
    {
// ReSharper disable InconsistentNaming
        k2G,
        k4G,
        k8G,
        k16G,
// ReSharper restore InconsistentNaming
    }

    /// <summary>
    /// Interface for 3-axis accelerometers
    /// </summary>
    public interface Accelerometer
    {
        /// <summary>
        /// Common interface for setting the measuring range of an accelerometer
        /// </summary>
        /// <param name="range">The maximu acceleration, positive or negative, that the 
        /// accelerometer will measure. Not all accelerometers support all ranges</param>
        void SetRange(Range range);

        /// <summary>
        /// Common interface for getting the x axis acceleration
        /// </summary>
        /// <returns>The acceleration along the x axis in g-forces</returns>
        double GetX();

        /// <summary>
        /// Common interface for getting the y axis acceleration
        /// </summary>
        /// <returns>The acceleration along the y axis in g-forces</returns>
        double GetY();

        /// <summary>
        /// Common interface for getting the z axis acceleration
        /// </summary>
        /// <returns>The acceleration along the z axis in g-forces</returns>
        double GetZ();

    }
}
