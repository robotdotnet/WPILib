using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    /// <summary>
    /// Class for interfacing with all PWM speed controllers.
    /// </summary>
    public class SimPWMController : ISimSpeedController
    {
        readonly HALSimPWMData PWMData = null;

        /// <summary>
        /// Creates a new PWM Speed controller for the Sim to use.
        /// </summary>
        /// <param name="port">The PWM port the Speed controller is attached to.</param>
        public SimPWMController(int port)
        {
            PWMData = SimData.PWM[port];
        }

        /// <inheritdoc/>
        public double Get()
        {
            // TODO Fix this, We need to grab the right value
            return PWMData.GetSpeed();
        }
    }
}
