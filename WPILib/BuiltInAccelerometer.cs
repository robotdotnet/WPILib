using System;
using HAL_Base;
using WPILib.Interfaces;

namespace WPILib
{
    class BuiltInAccelerometer : Accelerometer
    {
        public BuiltInAccelerometer(Range range)
        {
            Range = range;
            HAL.Report(ResourceType.kResourceType_Accelerometer, (byte)0, 0, "Built-in accelerometer");
        }

        public Range Range
        {
            set
            {
                HALAccelerometer.SetAccelerometerActive(false);

                switch (value)
                {
                    case Range.k2G:
                        HALAccelerometer.SetAccelerometerRange(AccelerometerRange.Range_2G);
                        break;
                    case Range.k4G:
                        HALAccelerometer.SetAccelerometerRange(AccelerometerRange.Range_4G);
                        break;
                    case Range.k8G:
                        HALAccelerometer.SetAccelerometerRange(AccelerometerRange.Range_8G);
                        break;
                    case Range.k16G:
                        throw new SystemException("16G range not supported (use k2G, k4G, or k8G)");
                }

                HALAccelerometer.SetAccelerometerActive(true);
            }
        }

        public double X => HALAccelerometer.GetAccelerometerX();

        public double Y => HALAccelerometer.GetAccelerometerY();

        public double Z => HALAccelerometer.GetAccelerometerZ();
    }
}
