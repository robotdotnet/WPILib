using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
{
    public class SimDigitalInput
    {
        readonly HALSimDIOData DIOData = null;

        public SimDigitalInput(int pin)
        {
            DIOData = SimData.DIO[pin];
        }

        public void Set(bool value)
        {
            DIOData.SetValue(value);
        }

        public bool GetInput() => DIOData.GetValue();
    }
}
