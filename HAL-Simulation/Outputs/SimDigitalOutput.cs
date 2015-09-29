using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Data;

namespace HAL_Simulator.Outputs
{
    class SimDigitalOutput
    {
        readonly DIOData DIOData = null;

        public SimDigitalOutput(int pin)
        {
            DIOData = SimData.DIO[pin];
        }

        public bool Get()
        {
            return DIOData.Value;
        }
    }
}
