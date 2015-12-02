using System;
using HAL_Simulator.Data;

namespace HAL_Simulator.Inputs
{
    public class SimCounter : IServoFeedback
    {
        private readonly CounterData CounterData = null;
        private readonly bool k2x = true;
         
        public SimCounter(int pin)
        {
            int index = -1;
            for (int i = 0; i < 8; i++)
            {
                var counter = SimData.Counter[i];
                if (!counter.Initialized)
                    continue;
                if (counter.UpSourceChannel == pin || counter.DownSourceChannel == pin)
                {
                    index = i;
                }
            }

            if (index == -1)
            {
                throw new InvalidOperationException($"Counter not found for pin {pin}");
            }

            CounterData = SimData.Counter[index];
            k2x = CounterData.UpFallingEdge;
        }

        public void SetPosition(double value)
        {
            CounterData.Count = (int)(value * (k2x ? 2 : 1));
        }

        public void SetRate(double rate)
        {
            double output;
            if (rate == 0)
            {
                output = double.NaN;
            }
            else
            {
                output = 1 / rate;
            }
            CounterData.Period = output;
        }
    }
}
