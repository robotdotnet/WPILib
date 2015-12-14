using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    /// <summary>
    /// Class for interfacing with Relays in the simulator.
    /// </summary>
    public class SimRelay
    {
        readonly RelayData RelayData = null;

        /// <summary>
        /// Creates a new Relay to use with the simulator.
        /// </summary>
        /// <param name="port">The relay port to read.</param>
        public SimRelay(int port)
        {
            RelayData = SimData.Relay[port];
        }

        /// <summary>
        /// Gets if the relay forward is set.
        /// </summary>
        /// <returns>True if forward is set.</returns>
        public bool GetForward()
        {
            return RelayData.Forward;
        }

        /// <summary>
        /// Gets if the relay reverse is set.
        /// </summary>
        /// <returns>True if reverse is set.</returns>
        public bool GetReverse()
        {
            return RelayData.Reverse;
        }
    }
}
