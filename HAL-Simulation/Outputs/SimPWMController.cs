using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Data;

namespace HAL_Simulator.Outputs
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
