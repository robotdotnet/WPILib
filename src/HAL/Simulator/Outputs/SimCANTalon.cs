using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    /*
    /// <summary>
    /// Class for interfacing with a CAN Talon from the simulator.
    /// </summary>
    /// <remarks>
    /// Note that currently this only works in Percent VBus mode for the talon.
    /// </remarks>
    public class SimCANTalon : ISimSpeedController
    {
        /// <summary>
        /// Gets the data structure for the CAN Talon
        /// </summary>
        public CanTalonData Data { get; }
        /// <summary>
        /// Creates a new CAN Talon in the simulator.
        /// </summary>
        /// <param name="id">The ID of the talon to read.</param>
        public SimCANTalon(int id)
        {
            Data = SimData.GetCanTalon(id);
        }

        /// <inheritdoc/>
        public double Get()
        {
            return Data.PercentVBusValue;
        }
    }
    */
}
