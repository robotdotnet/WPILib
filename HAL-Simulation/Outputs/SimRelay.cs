using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Data;

namespace HAL_Simulator.Outputs
{
    public class SimRelay
    {
        readonly RelayData RelayData = null;

        public SimRelay(int port)
        {
            RelayData = SimData.Relay[port];
        }

        public bool GetForward()
        {
            return RelayData.Forward;
        }

        public bool GetReverse()
        {
            return RelayData.Reverse;
        }
    }
}
