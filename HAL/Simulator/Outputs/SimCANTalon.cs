using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    public class SimCANTalon : ISimSpeedController
    {
        public CanTalonData data = null;
        public SimCANTalon(int id)
        {
            data = SimData.GetCanTalon(id);
        }

        public double Get()
        {
            return data.Demand / 1023.0d;
        }
    }
}
