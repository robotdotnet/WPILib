using System;
using HAL.Base;
using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindow;
using AccelerometerRange = WPILib.Interfaces.AccelerometerRange;
using HALAccelerometer = HAL.Base.HALAccelerometer;
using static HAL.Base.HALAccelerometer;

namespace WPILib
{
    /// <summary>
    /// Class for accessing the RoboRIO's internal accelerometer.
    /// </summary>
    public class BuiltInAccelerometer : IAccelerometer, ILiveWindowSendable
    {
        /// <summary>
        /// Creates a new <see cref="BuiltInAccelerometer"/>.
        /// </summary>
        /// <param name="range">The range for the accelerometer to measure</param>
        public BuiltInAccelerometer(AccelerometerRange range = AccelerometerRange.k8G)
        {
            AccelerometerRange = range;
            HAL.Base.HAL.Report(ResourceType.kResourceType_Accelerometer, 0, 0, "Built-in accelerometer");
            LiveWindow.LiveWindow.AddSensor("BuiltInAccel", 0, this);
        }

        /// <inheritdoc/>
        public AccelerometerRange AccelerometerRange
        {
            set
            {
                HAL_SetAccelerometerActive(false);

                switch (value)
                {
                    case AccelerometerRange.k2G:
                        HAL_SetAccelerometerRange(HALAccelerometerRange.Range_2G);
                        break;
                    case AccelerometerRange.k4G:
                        HAL_SetAccelerometerRange(HALAccelerometerRange.Range_4G);
                        break;
                    case AccelerometerRange.k8G:
                        HAL_SetAccelerometerRange(HALAccelerometerRange.Range_8G);
                        break;
                    default:
                    case AccelerometerRange.k16G:
                        throw new ArgumentOutOfRangeException(nameof(value), "16G range not supported (use k2G, k4G, or k8G)");
                }

                HAL_SetAccelerometerActive(true);
            }
        }
        /// <inheritdoc/>
        public virtual double GetX() => HAL_GetAccelerometerX();
        /// <inheritdoc/>
        public virtual double GetY() => HAL_GetAccelerometerY();
        /// <inheritdoc/>
        public virtual double GetZ() => HAL_GetAccelerometerZ();

        /// <inheritdoc/>
        public virtual AllAxes GetAllAxes()
        {
            return new AllAxes(GetX(), GetY(), GetZ());   
        }

        ///<inheritdoc />
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        ///<inheritdoc />
        public ITable Table { get; private set; }

        ///<inheritdoc />
        public string SmartDashboardType => "3AxisAccelerometer";

        ///<inheritdoc />
        public void UpdateTable()
        {
            if (Table != null)
            {
                Table.PutNumber("X", GetX());
                Table.PutNumber("Y", GetY());
                Table.PutNumber("Z", GetZ());
            }
        }

        ///<inheritdoc />
        public void StartLiveWindowMode()
        {
        }

        ///<inheritdoc />
        public void StopLiveWindowMode()
        {
        }
    }
}
