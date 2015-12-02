using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
{
    public class SimDigitalInput
    {
        readonly DIOData DIOData = null;

        public SimDigitalInput(int pin)
        {
            DIOData = SimData.DIO[pin];
        }

        public void Set(bool value)
        {
            DIOData.Value = value;
        }

        public bool GetInput() => DIOData.Value;
    }
}
