using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Data;

namespace HAL_Simulator.Inputs
{
    public class SimDigitalInput
    {
        DIOData DIOData = null;

        public SimDigitalInput(int pin)
        {
            DIOData = SimData.DIO[pin];
        }

        public void Set(bool value)
        {
            DIOData.Value = value;
        }

        public bool GetInput() => DIOData.Value;
    }
}
