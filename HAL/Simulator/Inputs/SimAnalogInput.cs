using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
{
    public class SimAnalogInput : IServoFeedback
    {
        private readonly HALSimAnalogInData m_analogInData;
         
        public SimAnalogInput(int pin)
        {
            m_analogInData = SimData.AnalogIn[pin];
        }

        //Volts
        public void SetPosition(double value)
        {
            if (value > 5.0)
                value = 5.0;
            if (value < 0.0)
                value = 0.0;
            m_analogInData.SetVoltage(value);
        }

        public void SetRate(double rate)
        {
        }

        public double GetVoltage() => m_analogInData.GetVoltage();
    }
}
