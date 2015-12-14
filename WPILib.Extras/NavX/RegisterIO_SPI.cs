using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.Extras.NavX.Protocols;

namespace WPILib.Extras.NavX
{
    class RegisterIO_SPI : IRegisterIo
    {
        SPI port;
        int bitrate;

        const int DEFAULT_SPI_BITRATE_HZ = 500000;

        public RegisterIO_SPI(SPI spi_port)
        {
            port = spi_port;
            bitrate = DEFAULT_SPI_BITRATE_HZ;
        }

        public RegisterIO_SPI(SPI spi_port, int bitrate)
        {
            port = spi_port;
            this.bitrate = bitrate;
        }

    public bool Init()
        {
            port.SetClockRate(bitrate);
            port.SetMSBFirst();
            port.SetSampleDataOnFalling();
            port.SetClockActiveLow();
            port.SetChipSelectActiveLow();
            return true;
        }

    public bool Write(byte address, byte value)
        {
            byte[] cmd = new byte[3];
            cmd[0] = (byte)(address | (byte)0x80);
            cmd[1] = value;
            cmd[2] = AHRSProtocol.getCRC(cmd, 2);
            if (port.Write(cmd, cmd.Length) != cmd.Length)
            {
                return false; // WRITE ERROR
            }
            return true;
        }

    public bool Read(byte first_address, byte[] buffer)
        {
            byte[] cmd = new byte[3];
            cmd[0] = first_address;
            cmd[1] = (byte)buffer.Length;
            cmd[2] = AHRSProtocol.getCRC(cmd, 2);
            if (port.Write(cmd, cmd.Length) != cmd.Length)
            {
                return false; // WRITE ERROR
            }
            // delay 200 us /* TODO:  What is min. granularity of delay()? */
            Timer.Delay(0.001);
            byte[] received_data = new byte[buffer.Length + 1];
            if (port.Read(true, received_data, received_data.Length) != received_data.Length)
            {
                return false; // READ ERROR
            }
            byte crc = AHRSProtocol.getCRC(received_data, received_data.Length - 1);
            if (crc != received_data[received_data.Length - 1])
            {
                return false; // CRC ERROR
            }
            Array.Copy(received_data, 0, buffer, 0, received_data.Length - 1);
            return true;
        }
    public bool Shutdown()
        {
            return true;
        }
    }
}
