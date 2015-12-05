using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    internal class I2cIO :IIo
    {
        private readonly I2C m_i2c;

        public I2cIO(I2C.Port port)
        {
            m_i2c = new I2C(port, 0x32);
        }

        public void Dispose()
        {
            m_i2c.Dispose();
        }

        public byte[] Read(byte readSize, DeviceRegisters deviceRegister)
        {
            m_i2c.LvWrite((byte)deviceRegister, new byte[] { readSize });
            byte[] buffer = new byte[readSize];
            m_i2c.LvRead(new byte[0], readSize, ref buffer);
            return buffer;
        }

        public void Write(DeviceRegisters deviceRegister, byte[] message)
        {
            byte reg = (byte)((byte)deviceRegister | 0x80);
            m_i2c.LvWrite(reg, message);
        }
    }
}
