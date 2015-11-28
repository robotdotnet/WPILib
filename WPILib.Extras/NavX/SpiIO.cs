using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    internal class SpiIO :IIo
    {
        public byte[] CalculateCrc(byte[] message, out byte crc)
        {
            crc = CalculateCrc(message);
            byte[] messageWithCrc = new byte[message.Length + 1];
            Array.Copy(message, messageWithCrc, message.Length);
            messageWithCrc[message.Length] = crc;
            return messageWithCrc;
        }

        public byte CalculateCrc(byte[] message)
        {
            byte i = 0, j = 0;
            byte crc = 0;
            for (i = 0; i < message.Length; ++i)
            {
                crc ^= message[i];
                for (j = 0; j < 8; ++j)
                {
                    if ((crc & 1) != 0)
                    {
                        crc ^= 0x91;
                    }
                    crc >>= 1;
                }
            }
            return crc;
        }

        private readonly SPI m_spi;

        public SpiIO(SPI.Port port)
        {
            m_spi = new SPI(port);
            m_spi.SetClockRate(2000000);
        }

        public void Dispose()
        {
            m_spi.Dispose();
        }

        public byte[] Read(DeviceRegisters deviceRegister, byte readSize)
        {
            byte crc;
            byte[] toWrite = CalculateCrc(new[] {(byte)deviceRegister, readSize}, out crc);
            m_spi.Write(toWrite, toWrite.Length);
            byte[] readBytes = new byte[readSize + 1];
            m_spi.Read(false, readBytes, readBytes.Length);

            crc = readBytes[readBytes.Length];
            
            byte[] readBytesWithoutCrc = new byte[readBytes.Length - 1];
            Array.Copy(readBytes, readBytesWithoutCrc, readBytesWithoutCrc.Length);

            byte calcCrc = CalculateCrc(readBytesWithoutCrc);

            if (crc != calcCrc) return null;

            return readBytesWithoutCrc;
        }

        public void Write(DeviceRegisters deviceRegister, byte[] message)
        {
            byte reg = (byte)((byte)deviceRegister | 0x80);
            byte[] messageToCrc = new byte[message.Length + 1];
            messageToCrc[0] = reg;
            Array.Copy(message, 0, messageToCrc, 1, message.Length);
            byte crc;
            byte[] messageWithCrc = CalculateCrc(messageToCrc, out crc);

            m_spi.Write(messageWithCrc, messageWithCrc.Length);
        }
    }
}
