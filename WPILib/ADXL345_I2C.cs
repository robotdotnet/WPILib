using System;
using HAL_Base;
using WPILib.Interfaces;
using WPILib.LiveWindows;
using AccelerometerRange = WPILib.Interfaces.AccelerometerRange;

namespace WPILib
{
    public class ADXL345_I2C : ADXL345
    {
        private const byte Address = 0x1D;

        private I2C m_i2C;

        public ADXL345_I2C(I2C.Port port, AccelerometerRange range)
        {
            m_i2C = new I2C(port, Address);
            m_i2C.Write(PowerCtlRegister, (int)PowerCtl.Measure);
            AccelerometerRange = range;
            HAL.Report(ResourceType.kResourceType_ADXL345, Instances.kADXL345_I2C);
            LiveWindow.AddSensor("ADXL345_I2C", (byte)port, this);
        }

        protected override void WriteRange(byte value)
        {
            m_i2C.Write(DataFormatRegister, (byte)DataFormat.FullRes | value);
        }

        public override double GetAcceleration(Axes axis)
        {
            byte[] rawAccel = new byte[2];
            m_i2C.Read(DataRegister + (byte)axis, rawAccel.Length, rawAccel);
            return BitConverter.ToInt16(rawAccel, 0) * GsPerLSB;
        }

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
