using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;
using WPILib.Interfaces;

namespace WPILib
{
    class BuiltInAccelerometer : Accelerometer
    {
        public BuiltInAccelerometer(Range range)
        {
            SetRange(range);
            HAL.Report(ResourceType.kResourceType_Accelerometer, (byte)0, 0, "Built-in accelerometer");
        }

        public void SetRange(Range range)
        {
            HALAccelerometer.SetAccelerometerActive(false);

            switch (range)
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

        public double GetX()
        {
            return HALAccelerometer.GetAccelerometerX();
        }

        public double GetY()
        {
            return HALAccelerometer.GetAccelerometerY();
        }

        public double GetZ()
        {
            return HALAccelerometer.GetAccelerometerZ();
        }
    }
}
