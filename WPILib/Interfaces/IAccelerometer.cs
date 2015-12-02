
namespace WPILib.Interfaces
{
    /// <summary>
    /// A struct containing all axes of an accelerometer.
    /// </summary>
    public struct AllAxes
    {
        /// <summary>
        /// The X Axis
        /// </summary>
        public double XAxis { get; }
        /// <summary>
        /// The Y Axis
        /// </summary>
        public double YAxis { get; }
        /// <summary>
        /// The Z Axis
        /// </summary>
        public double ZAxis { get; }

        /// <summary>
        /// Creates a new <see cref="AllAxes"/> struct.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <param name="z">The z value.</param>
        public AllAxes(double x, double y, double z)
        {
            XAxis = x;
            YAxis = y;
            ZAxis = z;
        }
    }

    /// <summary>
    /// Ranges allowed for Accelerometers
    /// </summary>
    public enum AccelerometerRange
    {
// ReSharper disable InconsistentNaming
        /// <summary>
        /// 2G Maximum
        /// </summary>
        k2G,
        /// <summary>
        /// 4G Maximum
        /// </summary>
        k4G,
        /// <summary>
        /// 8G Maximum
        /// </summary>
        k8G,
        /// <summary>
        /// 16G Maximum
        /// </summary>
        k16G,
// ReSharper restore InconsistentNaming
    }

    /// <summary>
    /// Interface for 3-axis accelerometers
    /// </summary>
    public interface IAccelerometer
    {
        /// <summary>
        /// Common interface for setting the measuring range of an accelerometer
        /// </summary>
        /// <value>The maximum acceleration, positive or negative, that the 
        /// accelerometer will measure. Not all accelerometers support all ranges
        /// </value>
        AccelerometerRange AccelerometerRange { set; }

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

        /// <summary>
        /// Common interface for getting all acceleration axes
        /// </summary>
        /// <returns>A struct containing all axes</returns>
        AllAxes GetAllAxes();
    }
}
