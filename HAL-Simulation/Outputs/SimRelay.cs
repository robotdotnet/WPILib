using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Outputs
{
    public class SimRelay
    {
        NotifyDict<dynamic, dynamic> dictionary = null;

        public SimRelay(int port)
        {
            dictionary = SimData.halData["relay"][port];
        }

        public bool GetForward()
        {
            return dictionary["fwd"];
        }

        public bool GetReverse()
        {
            return dictionary["rev"];
        }
    }
}
