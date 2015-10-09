using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    class RegisterIoI2C : IRegisterIo
    {
        I2C m_port;

        public RegisterIoI2C(I2C i2CPort)
        {
            m_port = i2CPort;
        }


        public bool Init()
        {
            return true;
        }


        public bool Write(byte address, byte value)
        {
            return m_port.Write(address | 0x80, value);
        }

        const int MaxWpilibI2CReadBytes = 7;


        public bool Read(byte firstAddress, byte[] buffer)
        {
            int len = buffer.Length;
            int bufferOffset = 0;
            while (len > 0)
            {
                int readLen = (len > MaxWpilibI2CReadBytes) ? MaxWpilibI2CReadBytes : len;
                byte[] readBuffer = new byte[readLen];
                if (!m_port.Write(firstAddress + bufferOffset, readLen) &&
                    !m_port.ReadOnly(readBuffer, readLen))
                {
                    Array.Copy(readBuffer, 0, buffer, bufferOffset, readLen);
                    bufferOffset += readLen;
                    len -= readLen;
                }
                else
                {
                    break;
                }
            }
            return (len == 0);
        }


        public bool Shutdown()
        {
            return true;
        }
    }
}
