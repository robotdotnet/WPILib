using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Inputs
{
    public interface IServoFeedback
    {
        void Set(double value);
    }
}
