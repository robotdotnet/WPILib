using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
{
    public class SimNavXGyro : IServoFeedback
    {
        private readonly NavXData data = SimData.NavXData;

        public void SetPosition(double value)
        {
            data.GyroAngleYaw = value;
        }

        public void SetRate(double rate)
        {
            data.GyroRateYaw = rate;
        }
    }
}
