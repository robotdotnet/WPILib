using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    class RegisterIO_I2C : IRegisterIo
    {
        I2C port;

        public RegisterIO_I2C(I2C i2c_port)
        {
            port = i2c_port;
        }


        public bool Init()
        {
            return true;
        }


        public bool Write(byte address, byte value)
        {
            return port.Write(address | 0x80, value);
        }

        const int MAX_WPILIB_I2C_READ_BYTES = 7;


        public bool Read(byte first_address, byte[] buffer)
        {
            int len = buffer.Length;
            int buffer_offset = 0;
            while (len > 0)
            {
                int read_len = (len > MAX_WPILIB_I2C_READ_BYTES) ? MAX_WPILIB_I2C_READ_BYTES : len;
                byte[] read_buffer = new byte[read_len];
                if (!port.Write(first_address + buffer_offset, read_len) &&
                    !port.ReadOnly(read_buffer, read_len))
                {
                    Array.Copy(read_buffer, 0, buffer, buffer_offset, read_len);
                    buffer_offset += read_len;
                    len -= read_len;
                }
                else {
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
