using System;
using HAL_Base;
using WPILib.LiveWindows;
using AccelerometerRange = WPILib.Interfaces.AccelerometerRange;

namespace WPILib
{
    /// <summary>
    /// ADXL345 Accelerometer interfaced over I2C.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class ADXL345_I2C : ADXL345
    {
        private const byte Address = 0x1D;
        private const byte AlternateAddress = 0x53;

        private readonly I2C m_i2C;

        /// <summary>
        /// Creates a new ADXL345_I2C Accelerometer using the default address of 0x1D.
        /// </summary>
        /// <param name="port">The I2C port the accelerometer is attached to</param>
        /// <param name="range">The range (+ or -) that the accelerometer will measure.</param>
        public ADXL345_I2C(I2C.Port port, AccelerometerRange range)
        {
            m_i2C = new I2C(port, Address);
            m_i2C.Write(PowerCtlRegister, (int)PowerCtl.Measure);
            AccelerometerRange = range;
            HAL.Report(ResourceType.kResourceType_ADXL345, Instances.kADXL345_I2C);
            LiveWindow.AddSensor("ADXL345_I2C", (byte)port, this);
        }

        /// <summary>
        /// Creates a new ADXL345_I2C Accelerometer, specifing the address.
        /// </summary>
        /// <param name="port">The I2C port the accelerometer is attached to</param>
        /// <param name="range">The range (+ or -) that the accelerometer will measure.</param>
        /// <param name="useAlternateAddress">True to use address 0x53, which is used by the sparkfun board.
        /// False to use address 0x1D, which is used by the KOP board.</param>
        public ADXL345_I2C(I2C.Port port, AccelerometerRange range, bool useAlternateAddress)
        {
            byte address = useAlternateAddress ? AlternateAddress : Address;
            m_i2C = new I2C(port, address);
            m_i2C.Write(PowerCtlRegister, (int)PowerCtl.Measure);
            AccelerometerRange = range;
            HAL.Report(ResourceType.kResourceType_ADXL345, Instances.kADXL345_I2C);
            LiveWindow.AddSensor("ADXL345_I2C", (byte)port, this);
        }

        /// <summary>
        /// Writes the range to the specified interface.
        /// </summary>
        /// <param name="value">The Range to write.</param>
        protected override void WriteRange(byte value)
        {
            m_i2C.Write(DataFormatRegister, (byte)DataFormat.FullRes | value);
        }


        /// <summary>
        /// Get the acceleration of one axis in Gs.
        /// </summary>
        /// <param name="axis">The axis to read from.</param>
        /// <returns>Acceleration of the ADXL345 in Gs.</returns>
        public override double GetAcceleration(Axes axis)
        {
            byte[] rawAccel = new byte[2];
            m_i2C.Read(DataRegister + (byte)axis, rawAccel.Length, rawAccel);
            return BitConverter.ToInt16(rawAccel, 0) * GsPerLSB;
        }

        /// <summary>
        /// Get the acceleration of all axes in Gs.
        /// </summary>
        /// <returns>An object containing the acceleration measured on each side of the ADXL345 in Gs.</returns>
        public override AllAxes GetAccelerations()
        {
            AllAxes data = new AllAxes();
            byte[] rawData = new byte[6];
            m_i2C.Read(DataRegister, rawData.Length, rawData);
            data.XAxis = BitConverter.ToInt16(rawData, 0) * GsPerLSB;
            data.YAxis = BitConverter.ToInt16(rawData, 2) * GsPerLSB;
            data.ZAxis = BitConverter.ToInt16(rawData, 4) * GsPerLSB;
            return data;
        }

    }
}
