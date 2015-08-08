using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Inputs
{
    public class SimDigitalInput
    {
        NotifyDict<dynamic, dynamic> dictionary = null;

        public SimDigitalInput(int pin)
        {
            dictionary = SimData.halData["dio"][pin];
        }

        public void Set(bool value)
        {
            dictionary["value"] = value;
        }

        public bool GetInput() => dictionary["value"];
    }
}
