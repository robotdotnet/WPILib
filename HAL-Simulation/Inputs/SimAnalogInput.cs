
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Set(double value)
        {
            if (value > 5.0)
                value = 5.0;
            if (value < 0.0)
                value = 0.0;
            AnalogInData.Voltage = value;
        }

        public double GetVoltage() => AnalogInData.Voltage;
    }
}
