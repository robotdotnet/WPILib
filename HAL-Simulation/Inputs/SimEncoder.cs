using System;
using HAL_Simulator.Data;

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

    public class SimEncoder : IServoFeedback
    {
        //public  NotifyDict<dynamic, dynamic> Dictionary { get; private set; } = null;
        internal EncoderData EncoderData { get; private set; } = null;

        public bool IsEncoder { get; private set; } = true;
        private bool k2x = true;

        public SimEncoder(int pin, EncodingType type = EncodingType.K4X)
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
                var encoder = SimData.Encoder[i];
                if (!encoder.Initialized)
                    continue;
                if (encoder.Config != null)
                {
                    if (encoder.Config["ASource_Channel"] == pin || encoder.Config["BSource_Channel"] == pin)
                    {
                        index = i;
                    }
                }
            }

            if (index == -1)
            {
                throw new InvalidOperationException($"Encoder not found for pin {pin}");
            }

            EncoderData = SimData.Encoder[index];
        }

        private void InitCounter(int pin)
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

            EncoderData = SimData.Counter[index];
            IsEncoder = false;
            k2x = SimData.Counter[index].AverageSize == 2;
        }

        public void SetPosition(double value)
        {
            if (IsEncoder)
            {
                //All encoders are 4x. So we will multiply by 4.
                EncoderData.Count = (int) (value*4);
            }
            else
            {
                EncoderData.Count = (int)(value * (k2x ? 2 : 1));
            }
        }

        public void SetPeriod(double period)
        {
            EncoderData.Period = period;
        }
    }
}
