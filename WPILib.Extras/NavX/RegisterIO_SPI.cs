using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    class RegisterIoSpi : IRegisterIo
    {
        SPI m_port;
        int m_bitrate;

        const int DefaultSpiBitrateHz = 500000;

        public RegisterIoSpi(SPI spiPort)
        {
            m_port = spiPort;
            m_bitrate = DefaultSpiBitrateHz;
        }

        public RegisterIoSpi(SPI spiPort, int bitrate)
        {
            m_port = spiPort;
            this.m_bitrate = bitrate;
        }

        
    public bool Init()
        {
            m_port.SetClockRate(m_bitrate);
            m_port.SetMSBFirst();
            m_port.SetSampleDataOnFalling();
            m_port.SetClockActiveLow();
            m_port.SetChipSelectActiveLow();
            return true;
        }

        
    public bool Write(byte address, byte value)
        {
            byte[] cmd = new byte[3];
            cmd[0] = (byte)(address | (byte)0x80);
            cmd[1] = value;
            cmd[2] = AHRSProtocol.GetCrc(cmd, 2);
            if (m_port.Write(cmd, cmd.Length) != cmd.Length)
            {
                return false; // WRITE ERROR
            }
            return true;
        }

        
    public bool Read(byte firstAddress, byte[] buffer)
        {
            byte[] cmd = new byte[3];
            cmd[0] = firstAddress;
            cmd[1] = (byte)buffer.Length;
            cmd[2] = AHRSProtocol.GetCrc(cmd, 2);
            if (m_port.Write(cmd, cmd.Length) != cmd.Length)
            {
                return false; // WRITE ERROR
            }
            // delay 200 us /* TODO:  What is min. granularity of delay()? */
            Timer.Delay(0.0002);
            byte[] receivedData = new byte[buffer.Length + 1];
            if (m_port.Read(true, receivedData, receivedData.Length) != receivedData.Length)
            {
                return false; // READ ERROR
            }
            byte crc = AHRSProtocol.GetCrc(receivedData, receivedData.Length - 1);
            if (crc != receivedData[receivedData.Length - 1])
            {
                return false; // CRC ERROR
            }
            Array.Copy(receivedData, 0, buffer, 0 ,receivedData.Length - 1);
            return true;
        }

        
    public bool Shutdown()
        {
            return true;
        }
    }
}
