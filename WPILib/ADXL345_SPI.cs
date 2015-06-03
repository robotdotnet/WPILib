using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib
{
    public class ADXL345_SPI : ADXL345
    {
        private const int Address_Read = 0x80;
        private const int Address_MultiByte = 0x40;

        private SPI m_spi;

        public ADXL345_SPI(SPI.Port port, Interfaces.Range range)
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
            SetRange(range);
            HAL_Base.HAL.Report(HAL_Base.ResourceType.kResourceType_ADXL345, HAL_Base.Instances.kADXL345_SPI);
        }

        protected override void WriteRange(byte value)
        {
            byte[] commands = new byte[] { DataFormatRegister, (byte)((byte)DataFormat.FullRes | value) };
            m_spi.Write(commands, commands.Length);
        }

        public override double GetAcceleration(ADXL345.Axes axis)
        {
            byte[] transferBuffer = new byte[3];
            transferBuffer[0] = (byte)((Address_Read | Address_MultiByte | DataRegister) + (byte)axis);
            m_spi.Transaction(transferBuffer, transferBuffer, 3);
            return BitConverter.ToInt16(transferBuffer, 1) * GsPerLSB;
        }

        public override ADXL345.AllAxes GetAccelerations()
        {
            ADXL345.AllAxes data = new ADXL345.AllAxes();
            byte[] dataBuffer = new byte[7];
            if (m_spi != null)
            {
                dataBuffer[0] = (byte)(Address_Read | Address_MultiByte | DataRegister);
                m_spi.Transaction(dataBuffer, dataBuffer, 7);
                data.XAxis = BitConverter.ToInt16(dataBuffer, 1) * GsPerLSB;
                data.YAxis = BitConverter.ToInt16(dataBuffer, 3) * GsPerLSB;
                data.ZAxis = BitConverter.ToInt16(dataBuffer, 5) * GsPerLSB;
            }
            return data;
        }
    }
}
