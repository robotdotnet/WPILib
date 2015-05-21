using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.Interfaces
{
    public enum Range
    {
        k2G,
        k4G,
        k8G,
        k16G,
    }
    public interface Accelerometer
    {
        void SetRange(Range range);
        double GetX();
        double GetY();
        double GetZ();

    }
}
