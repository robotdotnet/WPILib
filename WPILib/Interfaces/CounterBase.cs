

using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Interfaces
{
    public enum EncodingType
    {
        k1X_val = 0,
        k2X_val = 1,
        k4X_val = 2,
    }
    public interface CounterBase
    {
        int Get();
        void Reset();
        double GetPeriod();
        void SetMaxPeriod(double maxPeriod);
        bool GetStopped();
        bool GetDirection();
    }
}
