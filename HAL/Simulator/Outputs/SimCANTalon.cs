using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    public class SimCANTalon : ISimSpeedController
    {
        public CanTalonData Data { get; }
        public SimCANTalon(int id)
        {
            Data = SimData.GetCanTalon(id);
        }

        public double Get()
        {
            return Data.Demand / 1023.0d;
        }
    }
}
