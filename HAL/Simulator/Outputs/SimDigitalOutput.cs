using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    /// <summary>
    /// Class for interfacing with a Digital Output in the simulator
    /// </summary>
    public class SimDigitalOutput
    {
        readonly DIOData DIOData = null;

        /// <summary>
        /// Creates a new Sim Digital Output reader.
        /// </summary>
        /// <param name="pin">The digital output pin to use.</param>
        public SimDigitalOutput(int pin)
        {
            DIOData = SimData.DIO[pin];
        }

        /// <summary>
        /// Gets the current value of the Digital Output.
        /// </summary>
        /// <returns>The current value of the output.</returns>
        public bool Get()
        {
            return DIOData.Value;
        }
    }
}
