using System;
using HAL_Base;
using WPILib.Interfaces;
using AccelerometerRange = WPILib.Interfaces.AccelerometerRange;

namespace WPILib
{
    class BuiltInAccelerometer : IAccelerometer
    {
        public BuiltInAccelerometer(AccelerometerRange range)
        {
            AccelerometerRange = range;
            HAL.Report(ResourceType.kResourceType_Accelerometer, (byte)0, 0, "Built-in accelerometer");
        }

        public AccelerometerRange AccelerometerRange
        {
            set
            {
                HALAccelerometer.SetAccelerometerActive(false);

                switch (value)
                {
                    case AccelerometerRange.k2G:
                        HALAccelerometer.SetAccelerometerRange(HAL_Base.AccelerometerRange.Range_2G);
                        break;
                    case AccelerometerRange.k4G:
                        HALAccelerometer.SetAccelerometerRange(HAL_Base.AccelerometerRange.Range_4G);
                        break;
                    case AccelerometerRange.k8G:
                        HALAccelerometer.SetAccelerometerRange(HAL_Base.AccelerometerRange.Range_8G);
                        break;
                    case AccelerometerRange.k16G:
                        throw new SystemException("16G range not supported (use k2G, k4G, or k8G)");
                }

                HALAccelerometer.SetAccelerometerActive(true);
            }
        }

        public double GetX() => HALAccelerometer.GetAccelerometerX();

        public double GetY() => HALAccelerometer.GetAccelerometerY();

        public double GetZ() => HALAccelerometer.GetAccelerometerZ();
    }
}
