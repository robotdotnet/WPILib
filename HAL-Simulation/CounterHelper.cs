using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HAL_Simulator.SimData;

namespace HAL_Simulator
{
    public static class CounterHelper
    {
        public static int GetCounterFromPin(int pin)
        {
            for (int i = 0; i < 8; i++)
            {
                var counter = halData["counter"][i];
                if (!counter["initialized"])
                    continue;
                if (counter["up_source_channel"] == pin || counter["down_source_channel"] == pin)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void SetCounterCount(int count)
        {
            
        }

        public static void SetCounterPeriod(double period)
        {
            
        }

        public static int GetEncoderFromPin(int pin)
        {
            for (int i = 0; i < 4; i++)
            {
                var encoder = halData["encoder"][i];
                if (!encoder["initialized"])
                    continue;
                if (encoder.ContainsKey("config"))
                {
                    if (encoder["config"]["ASource_Channel"] == pin || encoder["config"]["BSource_Channel"] == pin)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}
