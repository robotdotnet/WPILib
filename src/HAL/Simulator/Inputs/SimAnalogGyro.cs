using System;
using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
{
    public class SimAnalogGyro : IServoFeedback
    {
        private readonly HALSimAnalogGyroData data;

        public SimAnalogGyro(int port)
        {
            data = SimData.AnalogGyro[port];
        }

        public void SetPosition(double value)
        {
            data.SetAngle(value);
        }

        public void SetRate(double rate)
        {
            data.SetRate(rate);
        }
    }
}
