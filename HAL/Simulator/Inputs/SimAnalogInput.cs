
using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
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

        public void SetRate(double rate)
        {
        }

        public double GetVoltage() => AnalogInData.Voltage;
    }
}
