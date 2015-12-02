using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindow;

namespace WPILib
{
    /// <summary>
    /// Base class for the ADXL345 Accelerometer
    /// </summary>
    public abstract class ADXL345 : SensorBase, IAccelerometer, ILiveWindowSendable
    {
        protected const byte PowerCtlRegister = 0x2D;
        protected const byte DataFormatRegister = 0x31;
        protected const byte DataRegister = 0x32;
        protected const double GsPerLSB = 0.00390625;

        /// <summary>
        /// Power Control Settings for ADXL345
        /// </summary>
        protected enum PowerCtl : byte
        {
            Link = 0x20,
            AutoSleep = 0x10,
            Measure = 0x08,
            Sleep = 0x04
        }

        /// <summary>
        /// Data Format Settings for ADXL345
        /// </summary>
        protected enum DataFormat : byte
        {
            SelfTest = 0x80,
            SPI = 0x40,
            IntInvert = 0x20,
            FullRes = 0x08,
            Justify = 0x04
        }

        /// <summary>
        /// Axes Index's for ADXL345
        /// </summary>
        public enum Axes
        {
            X = 1,
            Y = 1 << 1,
            Z = 1 << 2
        }

        /// <summary>
        /// Writes the range to the specified interface.
        /// </summary>
        /// <param name="value">The Range to write.</param>
        protected abstract void WriteRange(byte value);

        /// <summary>
        /// Common interface for setting the measuring range of an accelerometer
        /// </summary>
        /// <value>The maximum acceleration, positive or negative, that the 
        ///   accelerometer will measure. Not all accelerometers support all ranges</value>
        public AccelerometerRange AccelerometerRange
        {
            set
            {
                byte retVal = 0;
                switch (value)
                {
                    case AccelerometerRange.k2G:
                        retVal = 0;
                        break;
                    case AccelerometerRange.k4G:
                        retVal = 1;
                        break;
                    case AccelerometerRange.k8G:
                        retVal = 2;
                        break;
                    case AccelerometerRange.k16G:
                        retVal = 3;
                        break;
                }
                WriteRange(retVal);
            }
        }

        /// <summary>
        /// Common interface for getting the x axis acceleration
        /// </summary>
        /// <returns>The acceleration along the x axis in g-forces</returns>
        public double GetX() => GetAcceleration(Axes.X);

        /// <summary>
        /// Common interface for getting the y axis acceleration
        /// </summary>
        /// <returns>The acceleration along the y axis in g-forces</returns>
        public double GetY() => GetAcceleration(Axes.Y);


        /// <summary>
        /// Common interface for getting the z axis acceleration
        /// </summary>
        /// <returns>The acceleration along the z axis in g-forces</returns>
        public double GetZ() => GetAcceleration(Axes.Z);

        /// <inheritdoc/>
        public virtual AllAxes GetAllAxes() => GetAccelerations();

        /// <summary>
        /// Get the acceleration of one axis in Gs.
        /// </summary>
        /// <param name="axis">The axis to read from.</param>
        /// <returns>Acceleration of the ADXL345 in Gs.</returns>
        public abstract double GetAcceleration(Axes axis);

        /// <summary>
        /// Get the acceleration of all axes in Gs.
        /// </summary>
        /// <returns>An object containing the acceleration measured on each side of the ADXL345 in Gs.</returns>
        public abstract AllAxes GetAccelerations();


        /// <summary>
        /// Returns the string representation of the named data type that will be used by the smart dashboard for this sendable
        /// </summary>
        public string SmartDashboardType => "3AxisAccelerometer";

        /// <summary>
        /// Initialize a table for this sendable object.
        /// </summary>
        /// <param name="subtable">The table to put the values in.</param>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        /// <summary>
        /// Update the table for this sendable object with the latest
        /// values.
        /// </summary>
        public void UpdateTable()
        {
            if (Table != null)
            {
                Table.PutNumber("X", GetX());
                Table.PutNumber("Y", GetY());
                Table.PutNumber("Z", GetZ());
            }
        }

        /// <summary>
        /// Returns the table that is currently associated with the sendable
        /// </summary>
        public ITable Table { get; private set; }

        /// <summary>
        /// Start having this sendable object automatically respond to
        /// value changes reflect the value on the table.
        /// </summary>
        public void StartLiveWindowMode() { }

        /// <summary>
        /// Stop having this sendable object automatically respond to value changes.
        /// </summary>
        public void StopLiveWindowMode() { }

    }
}
