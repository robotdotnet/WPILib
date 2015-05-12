using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Interfaces
{
    public interface SpeedController : PIDOutput
    {
        double Get();

        void Set(double speed, byte syncGroup);

        void Set(double speed);

        void Disable();
    }
}
