using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    /// <summary>
    /// Class for interfacing with Solenoids in the simulator.
    /// </summary>
    public class SimSolenoid
    {
        private readonly SolenoidData m_solenoidData = null;

        /// <summary>
        /// Creates a new solenoid for use with the simulator.
        /// </summary>
        /// <param name="port">The port on the PCM the solenoid is attached to.</param>
        /// <param name="module">The module number for the PCM.</param>
        public SimSolenoid(int port, int module = 0)
        {
            m_solenoidData = SimData.GetPCM(module).Solenoids[port];
        }

        /// <summary>
        /// Gets if the solenoid is outputting or not.
        /// </summary>
        /// <returns>True if the solenoid is set.</returns>
        public bool Get()
        {
            return m_solenoidData.Value;
        }
    }
}
