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
        private readonly object lockObject = new object();
        private bool trace = true;

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
            bool success;
            lock (lockObject)
            {
                success = port.Write(address | 0x80, value);
            }
            if (!success && trace) Console.WriteLine("navX-MXP I2C Write Error");
            return success;
        }

        const int MAX_WPILIB_I2C_READ_BYTES = 127;


        public bool Read(byte first_address, byte[] buffer)
        {
            int len = buffer.Length;
            int buffer_offset = 0;
            while (len > 0)
            {
                int read_len = (len > MAX_WPILIB_I2C_READ_BYTES) ? MAX_WPILIB_I2C_READ_BYTES : len;
                byte[] read_buffer = new byte[read_len];
                bool write_ok;
                bool read_ok = false;
                lock (lockObject)
                {
                    write_ok = port.Write(first_address + buffer_offset, read_len);
                    if (write_ok)
                    {
                        read_ok = port.ReadOnly(read_buffer, read_len);
                    }
                }
                if (write_ok && read_ok)
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
