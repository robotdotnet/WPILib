using System;
using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
{
    /*
    public class SimCounter : IServoFeedback
    {
        private readonly CounterData m_counterData = null;
        private readonly bool m_k2X;
         
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

            m_counterData = SimData.Counter[index];
            m_k2X = m_counterData.UpFallingEdge;
        }

        public void SetPosition(double value)
        {
            m_counterData.Count = (int)(value * (m_k2X ? 2 : 1));
        }

        public void SetRate(double rate)
        {
            double output;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (rate == 0)
            {
                output = double.NaN;
            }
            else
            {
                output = 1 / rate;
            }
            m_counterData.Period = output;
        }
    }
    */
}
