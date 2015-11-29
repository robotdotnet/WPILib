using System;
using HAL_Base;
using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindows;
using AccelerometerRange = WPILib.Interfaces.AccelerometerRange;

namespace WPILib
{
    public class BuiltInAccelerometer : IAccelerometer, ILiveWindowSendable
    {
        public BuiltInAccelerometer(AccelerometerRange range)
        {
            AccelerometerRange = range;
            HAL.Report(ResourceType.kResourceType_Accelerometer, (byte)0, 0, "Built-in accelerometer");
            LiveWindow.AddSensor("BuiltInAccel", 0, this);
        }

        public AccelerometerRange AccelerometerRange
        {
            set
            {
                HALAccelerometer.SetAccelerometerActive(false);

                switch (value)
                {
                    case AccelerometerRange.k2G:
                        HALAccelerometer.SetAccelerometerRange(HAL_Base.HALAccelerometerRange.Range_2G);
                        break;
                    case AccelerometerRange.k4G:
                        HALAccelerometer.SetAccelerometerRange(HAL_Base.HALAccelerometerRange.Range_4G);
                        break;
                    case AccelerometerRange.k8G:
                        HALAccelerometer.SetAccelerometerRange(HAL_Base.HALAccelerometerRange.Range_8G);
                        break;
                    case AccelerometerRange.k16G:
                        throw new ArgumentOutOfRangeException(nameof(value), "16G range not supported (use k2G, k4G, or k8G)");
                }

                HALAccelerometer.SetAccelerometerActive(true);
            }
        }

        public virtual double GetX() => HALAccelerometer.GetAccelerometerX();

        public virtual double GetY() => HALAccelerometer.GetAccelerometerY();

        public virtual double GetZ() => HALAccelerometer.GetAccelerometerZ();

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
