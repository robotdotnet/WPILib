
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Inputs
{
    public class SimAnalogInput : IServoFeedback
    {
        private NotifyDict<dynamic, dynamic> dictionary = null;
         
        public SimAnalogInput(int pin)
        {
            dictionary = SimData.halData["analog_in"][pin];
        }

        //Volts
        public void Set(double value)
        {
            if (value > 5.0)
                value = 5.0;
            if (value < 0.0)
                value = 0.0;
            dictionary["voltage"] = value;
        }

        public double GetVoltage() => dictionary["voltage"];
    }
}
