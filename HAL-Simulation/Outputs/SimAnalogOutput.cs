using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Outputs
{
    class SimAnalogOutput
    {
        NotifyDict<dynamic, dynamic> dictionary = null;

        public SimAnalogOutput(int pin)
        {
            dictionary = SimData.halData["analog_out"][pin];
        }

        public double Get()
        {
            return dictionary["voltage"];
        }
    }
}
