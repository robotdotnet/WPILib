using System;
using UnitsNet;
using WPILib.Interfaces;
using WPILib.SmartDashboardNS;

namespace WPILib
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    public abstract class GyroBase : IGyro, ISendable
#pragma warning restore CA1063 // Implement IDisposable Correctly
    {
        public abstract Angle Angle { get; }
        public abstract RotationalSpeed Rate { get; }

        public abstract void Calibrate();
#pragma warning disable CA1063 // Implement IDisposable Correctly
        public abstract void Dispose();
#pragma warning restore CA1063 // Implement IDisposable Correctly

        public virtual void InitSendable(ISendableBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.SmartDashboardType = "Gyro";
            builder.AddDoubleProperty("Value", () => Angle.Degrees, null);
        }

        public abstract void Reset();
    }
}
