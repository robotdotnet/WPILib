
namespace WPILib.Interfaces
{
    /// <summary>
    /// Interface for Potentiometers
    /// </summary>
    interface IPotentiometer : IPIDSource
    {
        /// <summary>
        /// Get the value of the potentiometer
        /// </summary>
        /// <returns>The value of the potentiometer</returns>
        double Get();
    }
}
