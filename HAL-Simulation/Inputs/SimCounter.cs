using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Inputs
{
    public class SimCounter : IServoFeedback
    {
        private Dictionary<dynamic, dynamic> dictionary = null;
        private bool k2x = true;
         
        public SimCounter(int pin)
        {
            int index = -1;
            for (int i = 0; i < 8; i++)
            {
                var counter = SimData.halData["counter"][i];
                if (!counter["initialized"])
                    continue;
                if (counter["up_source_channel"] == pin || counter["down_source_channel"] == pin)
                {
                    index = i;
                }
            }

            if (index == -1)
            {
                throw new InvalidOperationException($"Counter not found for pin {pin}");
            }

            dictionary = SimData.halData["counter"][index];
            k2x = dictionary["up_falling_edge"];
        }

        public void Set(double value)
        {
            dictionary["count"] = (int)(value * (k2x ? 2 : 1));
        }
    }
}
