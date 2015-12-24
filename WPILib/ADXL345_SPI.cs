using System;
using HAL.Base;
using WPILib.Interfaces;
using AccelerometerRange = WPILib.Interfaces.AccelerometerRange;

namespace WPILib
{
    /// <summary>
    /// ADXL345 Accelerometer interfaced over I2C.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class ADXL345_SPI : ADXL345
    {
        private const int AddressRead = 0x80;
        private const int AddressMultiByte = 0x40;

        private readonly SPI m_spi;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">The SPI port the accelerometer is attached to</param>
        /// <param name="range">The range (+ or -) that the accelerometer will measure.</param>
        public ADXL345_SPI(SPI.Port port, AccelerometerRange range)
        {
            m_spi = new SPI(port);
            m_spi.SetClockRate(500000);
            m_spi.SetSampleDataOnFalling();
            m_spi.SetClockActiveLow();
            m_spi.SetChipSelectActiveHigh();
            byte[] commands = new byte[2];
            commands[0] = PowerCtlRegister;
            commands[1] = (byte)PowerCtl.Measure;
            m_spi.Write(commands, 2);
            AccelerometerRange = range;
            HAL.Base.HAL.Report(ResourceType.kResourceType_ADXL345, Instances.kADXL345_SPI);
            LiveWindow.LiveWindow.AddSensor("ADXL345_SPI", (byte)port, this);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            m_spi.Dispose();
            base.Dispose();
        }

        /// <summary>
        /// Writes the range to the specified interface.
        /// </summary>
        /// <param name="value">The Range to write.</param>
        protected override void WriteRange(byte value)
        {
            byte[] commands = new byte[] { DataFormatRegister, (byte)((byte)DataFormat.FullRes | value) };
            m_spi.Write(commands, commands.Length);
        }

        /// <summary>
        /// Get the acceleration of one axis in Gs.
        /// </summary>
        /// <param name="axis">The axis to read from.</param>
        /// <returns>Acceleration of the ADXL345 in Gs.</returns>
        public override double GetAcceleration(Axes axis)
        {
            byte[] transferBuffer = new byte[3];
            transferBuffer[0] = (byte)((AddressRead | AddressMultiByte | DataRegister) + (byte)axis);
            m_spi.Transaction(transferBuffer, transferBuffer, 3);
            return BitConverter.ToInt16(transferBuffer, 1) * GsPerLSB;
        }

        /// <summary>
        /// Get the acceleration of all axes in Gs.
        /// </summary>
        /// <returns>An object containing the acceleration measured on each side of the ADXL345 in Gs.</returns>
        public override AllAxes GetAccelerations()
        {
            byte[] dataBuffer = new byte[7];
            if (m_spi != null)
            {
                dataBuffer[0] = (byte)(AddressRead | AddressMultiByte | DataRegister);
                m_spi.Transaction(dataBuffer, dataBuffer, 7);
                double XAxis = BitConverter.ToInt16(dataBuffer, 1) * GsPerLSB;
                double YAxis = BitConverter.ToInt16(dataBuffer, 3) * GsPerLSB;
                double ZAxis = BitConverter.ToInt16(dataBuffer, 5) * GsPerLSB;
                return new AllAxes(XAxis, YAxis, ZAxis);
            }
            return new AllAxes();
        }
    }
}
