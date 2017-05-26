using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    /// <summary>
    /// Class for interfacing with an Analog Output in the simulator
    /// </summary>
    public class SimAnalogOutput
    {
        readonly HALSimAnalogOutData m_analogOutData;

        /// <summary>
        /// Creates a new Sim Analog Output reader.
        /// </summary>
        /// <param name="pin">The analog output pin to use.</param>
        public SimAnalogOutput(int pin)
        {
            m_analogOutData = SimData.AnalogOut[pin];
        }

        /// <summary>
        /// Gets the current voltage of the Analog Output.
        /// </summary>
        /// <returns>The voltage commanded by the robot.</returns>
        public double Get()
        {
            return m_analogOutData.GetVoltage();
        }
    }
}
