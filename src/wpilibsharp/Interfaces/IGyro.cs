using System;
using UnitsNet;

namespace WPILib.Interfaces
{
    public interface IGyro : IDisposable
    {
        void Calibrate();
        void Reset();
        Angle Angle { get; }

        RotationalSpeed Rate { get; }
    }
}
