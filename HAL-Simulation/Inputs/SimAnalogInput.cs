
using HAL_Simulator.Data;

namespace HAL_Simulator.Inputs
{
    public class SimAnalogInput : IServoFeedback
    {
        private AnalogInData AnalogInData = null;
         
        public SimAnalogInput(int pin)
        {
            AnalogInData = SimData.AnalogIn[pin];
        }

        //Volts
        public void SetPosition(double value)
        {
            if (value > 5.0)
                value = 5.0;
            if (value < 0.0)
                value = 0.0;
            AnalogInData.Voltage = value;
        }

        public void SetPeriod(double value)
        {
        }

        public double GetVoltage() => AnalogInData.Voltage;
    }
}
