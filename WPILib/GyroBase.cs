using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// Gyro base is the common base class for Gyro implementations such as
    /// <see cref="AnalogGyro"/>.
    /// </summary>
    public abstract class GyroBase : SensorBase, IGyro, IPIDSource, ILiveWindowSendable
    {
        /// <summary>
        /// Gets or Sets which parameter of the gyro you are using as process control.
        /// </summary>
        /// <remarks>
        /// The <see cref="IGyro"/> class supports the rate and displacement parameters. 
        /// </remarks>
        public PIDSourceType PIDSourceType { get; set; } = PIDSourceType.Displacement;

        /// <inheritdoc/>
        public abstract void InitGyro();

        /// <inheritdoc/>
        public abstract void Reset();

        /// <inheritdoc/>
        public abstract double GetAngle();

        /// <inheritdoc/>
        public abstract double GetRate();

        /// <summary>
        /// Get the output of the gyro for use with <see cref="PIDController">PIDControllers.</see>
        /// May be the angle or rate depending on the set <see cref="PIDSourceType"/>.
        /// </summary>
        /// <returns>The output of the gyro.</returns>
        public double PidGet()
        {
            switch (PIDSourceType)
            {
                case PIDSourceType.Displacement:
                    return GetAngle();
                case PIDSourceType.Rate:
                    return GetRate();
                default:
                    return 0.0;
            }
        }

        /// <inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        /// <inheritdoc/>
        public ITable Table { get; private set; }

        /// <inheritdoc/>
        public virtual string SmartDashboardType => "Gyro";

        /// <inheritdoc/>
        public void UpdateTable()
        {
            Table?.PutNumber("Value", GetAngle());
        }

        /// <inheritdoc/>
        public void StartLiveWindowMode()
        {
        }
        /// <inheritdoc/>
        public void StopLiveWindowMode()
        {
        }
    }
}
