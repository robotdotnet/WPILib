using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    class SimAnalogOutput
    {
        readonly AnalogOutData m_analogOutData;

        public SimAnalogOutput(int pin)
        {
            m_analogOutData = SimData.AnalogOut[pin];
        }

        public double Get()
        {
            return m_analogOutData.Voltage;
        }
    }
}
