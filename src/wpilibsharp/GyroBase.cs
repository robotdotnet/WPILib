using UnitsNet;
using WPILib.Interfaces;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public abstract class GyroBase : IGyro, ISendable
    {
        public abstract Angle Angle { get; }
        public abstract RotationalSpeed Rate { get; }

        public abstract void Calibrate();
        public abstract void Dispose();

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Gyro";
            builder.AddDoubleProperty("Value", () => Angle.Degrees, null);
        }

        public abstract void Reset();
    }
}
