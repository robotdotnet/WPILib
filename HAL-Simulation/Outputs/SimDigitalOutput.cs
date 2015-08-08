using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Outputs
{
    class SimDigitalOutput
    {
        NotifyDict<dynamic, dynamic> dictionary = null;

        public SimDigitalOutput(int pin)
        {
            dictionary = SimData.halData["dio"][pin];
        }

        public bool Get()
        {
            return dictionary["value"];
        }
    }
}
