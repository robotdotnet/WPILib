using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.Interfaces
{
    interface Potentiometer : PIDSource
    {
        double Get();
    }
}
