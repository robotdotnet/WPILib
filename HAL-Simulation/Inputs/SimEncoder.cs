using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Inputs
{
    /// <summary>
    /// Encoding Types for Counters and Encoders.
    /// </summary>
    public enum EncodingType
    {
        K1X = 0,
        K2X = 1,
        K4X = 2,
    }

    public class SimEncoder
    {
        private Dictionary<dynamic, dynamic> dictionary = null;

        private bool encoder = true;
        private bool k2x = true;

        public SimEncoder(int pin, EncodingType type)
        {
            if (type == EncodingType.K4X)
            {
                InitEncoder(pin);
            }
            else
            {
                InitCounter(pin);
            }
        }

        private void InitEncoder(int pin)
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

        private void InitCounter(int pin)
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

        public void SetCount(double value)
        {
            if (encoder)
            {
                //All encoders are 4x. So we will multiply by 4.
                dictionary["count"] = (int) (value*4);
            }
            else
            {
                dictionary["count"] = (int)(value * (k2x ? 2 : 1));
            }
        }

        public void SetPeriod(double period)
        {
            dictionary["period"] = (float) period;
        }
    }
}
