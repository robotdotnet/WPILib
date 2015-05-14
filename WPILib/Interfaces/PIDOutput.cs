

using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Interfaces
{
    public interface PIDOutput
    {
        void PidWrite(double output);
    }
}
