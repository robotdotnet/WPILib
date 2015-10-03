using HAL_Simulator.Data;

namespace HAL_Simulator.Outputs
{
    class SimAnalogOutput
    {
        AnalogOutData AnalogOutData = null;

        public SimAnalogOutput(int pin)
        {
            AnalogOutData = SimData.AnalogOut[pin];
        }

        public double Get()
        {
            return AnalogOutData.Voltage;
        }
    }
}
