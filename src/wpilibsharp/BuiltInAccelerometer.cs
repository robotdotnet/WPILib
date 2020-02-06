using System;
using Hal;
using UnitsNet;
using WPILib.Interfaces;
using WPILib.SmartDashboard;

namespace WPILib
{
    public class BuiltInAccelerometer : IAccelerometer, ISendable, IDisposable
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
                        Hal.Accelerometer.SetActive(false);
                        Hal.Accelerometer.SetRange(value);
                        Hal.Accelerometer.SetActive(true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"{value} range not supported (use k2G, k4G, or k8G");
                }
            }
        }

        public Acceleration X => Acceleration.FromStandardGravity(Hal.Accelerometer.GetX());

        public Acceleration Y => Acceleration.FromStandardGravity(Hal.Accelerometer.GetY());

        public Acceleration Z => Acceleration.FromStandardGravity(Hal.Accelerometer.GetZ());

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
