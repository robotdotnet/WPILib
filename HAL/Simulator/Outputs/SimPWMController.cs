using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    public class SimPWMController : ISimSpeedController
    {
        readonly PWMData PWMData = null;

        public SimPWMController(int port)
        {
            PWMData = SimData.PWM[port];
        }

        public double Get()
        {
            return PWMData.Value;
        }
    }
}
