using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Outputs
{
    class SimPWMController : ISimSpeedController
    {
        NotifyDict<dynamic, dynamic> dictionary = null;

        public SimPWMController(int port)
        {
            dictionary = SimData.halData["pwm"][port];
        }

        public double Get()
        {
            return (double)dictionary["value"];
        }
    }
}
