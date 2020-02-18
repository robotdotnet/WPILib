using System;
using Hal;
using UnitsNet;
using WPILib.Interfaces;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public sealed class BuiltInAccelerometer : IAccelerometer, ISendable, IDisposable
    {
        public BuiltInAccelerometer(AccelerometerRange range = AccelerometerRange.k8G)
        {
            Range = range;
        }

        public AccelerometerRange Range
        {
            set
            {

                switch (value)
                {
                    case AccelerometerRange.k2G:
                    case AccelerometerRange.k4G:
                    case AccelerometerRange.k8G:
                        Hal.AccelerometerLowLevel.SetActive(false);
                        Hal.AccelerometerLowLevel.SetRange(value);
                        Hal.AccelerometerLowLevel.SetActive(true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"{value} range not supported (use k2G, k4G, or k8G");
                }
            }
        }

        public Acceleration X => Acceleration.FromStandardGravity(Hal.AccelerometerLowLevel.GetX());

        public Acceleration Y => Acceleration.FromStandardGravity(Hal.AccelerometerLowLevel.GetY());

        public Acceleration Z => Acceleration.FromStandardGravity(Hal.AccelerometerLowLevel.GetZ());

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "3AxisAccelerometer";
            builder.AddDoubleProperty("X", () => X.StandardGravity, null);
            builder.AddDoubleProperty("Y", () => Y.StandardGravity, null);
            builder.AddDoubleProperty("Z", () => Z.StandardGravity, null);
        }
    }
}
