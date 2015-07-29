using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Inputs
{
    public class SimEncoder : IServoFeedback
    {
        private Dictionary<dynamic, dynamic> dictionary = null;

        public SimEncoder(int pin)
        {
            int index = -1;
            for (int i = 0; i < 4; i++)
            {
                var encoder = SimData.halData["encoder"][i];
                if (!encoder["initialized"])
                    continue;
                if (encoder.ContainsKey("config"))
                {
                    if (encoder["config"]["ASource_Channel"] == pin || encoder["config"]["BSource_Channel"] == pin)
                    {
                        index = i;
                    }
                }
            }

            if (index == -1)
            {
                throw new InvalidOperationException($"Encoder not found for pin {pin}");
            }

            dictionary = SimData.halData["encoder"][index];
        } 
        public void Set(double value)
        {
            //All encoders are 4x. So we will multiply by 4.
            dictionary["count"] = (int) (value*4);
        }
    }
}
