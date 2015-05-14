

using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Interfaces
{
    public enum PIDSourceParameter
    {
        kDistance = 0,
        kRate = 1,
        kAngle = 2,
    }
    public interface PIDSource
    {
        double PidGet();
    }
}
