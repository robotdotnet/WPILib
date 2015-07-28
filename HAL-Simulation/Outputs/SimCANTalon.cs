using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Outputs
{
    public class SimCANTalon : ISimSpeedController
    {
        public NotifyDict<dynamic, dynamic> dictionary = null;
        public SimCANTalon(int id)
        {

        }

        public double Get()
        {
            return 0.0;
        }
    }
}
