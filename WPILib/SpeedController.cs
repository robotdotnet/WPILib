namespace WPILib
{
    //Interface for speed controlling devices
    public interface ISpeedController : IPIDOutput
    {
        /// <summary>
        /// Common interface for getting the current set speed of a speed controller.
        /// </summary>
        /// <value>The current set speed. Value is between -1.0 and 1.0.</value>
        double Value { get; set; }

        /// <summary>
        /// Common interface for setting the speed of a speed controller.
        /// </summary>
        /// <param name="value">The speed to set. Value should be between -1.0 and 1.0.</param>
        /// <param name="syncGroup">The update group to add this Set() to, pending UpdateSyncGroup().  If 0, update immediately.</param>
        void Set(double value, byte syncGroup);

        /// <summary>
        /// Disable the speed controller.
        /// </summary>
        void Disable();
    }
}
