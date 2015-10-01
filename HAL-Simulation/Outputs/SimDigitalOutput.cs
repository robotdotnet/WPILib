using HAL_Simulator.Data;

namespace HAL_Simulator.Outputs
{
    class SimDigitalOutput
    {
        readonly DIOData DIOData = null;

        public SimDigitalOutput(int pin)
        {
            DIOData = SimData.DIO[pin];
        }

        public bool Get()
        {
            return DIOData.Value;
        }
    }
}
