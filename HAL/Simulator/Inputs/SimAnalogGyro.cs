using System;
using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
{
    public class SimAnalogGyro : IServoFeedback
    {
        private readonly AnalogInData data;

        public SimAnalogGyro(int port)
        {
            data = SimData.AnalogIn[port];
        }

        public void SetPosition(double value)
        {
            data.AccumulatorValue = BitConverter.DoubleToInt64Bits(value);
        }

        public void SetRate(double rate)
        {
            data.AccumulatorCount = BitConverter.ToUInt32(BitConverter.GetBytes((float) rate), 0);
        }
    }
}
