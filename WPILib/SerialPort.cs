using System;
using System.Text;
using HAL_Base;
using static HAL_Base.HALSerialPort;
using static HAL_Base.HAL;
using static WPILib.Utility;

namespace WPILib
{
    public enum Port
    {
        Onboard = 0,
        MXP = 1,
        USB = 2,
    }

    public enum Parity
    {
        None = 0,
        Odd = 1,
        Even = 2,
        Mark = 3,
        Space = 4,
    }

    public enum StopBits
    {
        One = 10,
        OnePointFive = 15,
        Two = 20,
    }

    public enum FlowControl
    {
        None = 0,
        XonXoff = 1,
        RtsCts = 2,
        DtrDsr = 4,
    }

    public enum WriteBufferMode
    {
        FlushOnAccess = 1,
        FlushWhenFull = 2,
    }

    public class SerialPort : IDisposable
    {
        private byte m_port;

        

        public SerialPort(int baudRate, Port port, int dataBits, Parity parity, StopBits stopBits)
        {
            int status = 0;
            m_port = (byte)port;

            SerialInitializePort(m_port, ref status);
            CheckStatus(status);
            SerialSetBaudRate(m_port, (uint) baudRate, ref status);
            CheckStatus(status);
            SerialSetDataBits(m_port, (byte)dataBits, ref status);
            CheckStatus(status);
            SerialSetParity(m_port, (byte)parity, ref status);
            CheckStatus(status);
            SerialSetStopBits(m_port, (byte)stopBits, ref status);
            CheckStatus(status);

            ReadBufferSize = 1;

            Timeout = 0.5f;

            WriteBufferMode = WriteBufferMode.FlushOnAccess;

            DisableTermination();

            Report(ResourceType.kResourceType_SerialPort, (byte) 0);

        }

        public SerialPort(int baudRate, Port port, int dataBits, Parity parity)
            : this(baudRate, port, dataBits, parity, StopBits.One)
        {
            
        }

        public SerialPort(int baudRate, Port port, int dataBits)
            : this(baudRate, port, dataBits, Parity.None, StopBits.One)
        {
            
        }

        public SerialPort(int baudRate, Port port)
            : this(baudRate, port, 8, Parity.None, StopBits.One)
        {
            
        }

        public void Dispose()
        {
            int status = 0;
            SerialClose(m_port, ref status);
            CheckStatus(status);
        }

        public FlowControl FlowControl
        {
            set
            {
                int status = 0;
                SerialSetFlowControl(m_port, (byte) value, ref status);
                CheckStatus(status);
            }
        }

        public void EnableTermination(char terminator)
        {
            int status = 0;
            SerialEnableTermination(m_port, (byte) terminator, ref status);
            CheckStatus(status);
        }

        public void EnableTermination()
        {
            EnableTermination('\n');
        }

        public void DisableTermination()
        {
            int status = 0;
            SerialDisableTermination(m_port, ref status);
            CheckStatus(status);
        }

        public int BytesReceived
        {
            get
            {
                int retVal = 0;
                int status = 0;
                retVal = SerialGetBytesReceived(m_port, ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public string ReadString()
        {
            return ReadString(BytesReceived);
        }

        public string ReadString(int count)
        {
            byte[] output = Read(count);
            try
            {
                return Encoding.ASCII.GetString(output);
            }
            catch
            {
                return "";
            }
        }

        public byte[] Read(int count)
        {
            int status = 0;
            byte[] data = new byte[count];
            int gotten = (int)SerialRead(m_port, data, count, ref status);
            CheckStatus(status);
            byte[] retVal = new byte[gotten];
            for (int i = 0; i < gotten; i++)
            {
                retVal[i] = data[i];
            }
            return retVal;
        }

        public int Write(byte[] buffer, int count)
        {
            int status = 0;
            int retVal = (int)SerialWrite(m_port, buffer, count, ref status);
            CheckStatus(status);
            return retVal;
        }

        public int WriteString(string data)
        {
            byte[] dataB = Encoding.ASCII.GetBytes(data);
            return Write(dataB, dataB.Length);
        }

        public double Timeout
        {
            set
            {
                int status = 0;
                SerialSetTimeout(m_port, (float) value, ref status);
                CheckStatus(status);
            }
        }

        public int ReadBufferSize
        {
            set
            {
                int status = 0;
                SerialSetReadBufferSize(m_port, (uint) value, ref status);
                CheckStatus(status);
            }
        }

        public int WriteBufferSize
        {
            set
            {
                int status = 0;
                SerialSetWriteBufferSize(m_port, (uint) value, ref status);
                CheckStatus(status);
            }
        }

        public WriteBufferMode WriteBufferMode
        {
            set
            {
                int status = 0;
                SerialSetWriteMode(m_port, (byte) value, ref status);
                CheckStatus(status);
            }
        }

        public void Flush()
        {
            int status = 0;
            SerialFlush(m_port, ref status);
            CheckStatus(status);
        }

        public void Reset()
        {
            int status = 0;
            SerialClear(m_port, ref status);
            CheckStatus(status);
        }
    }
}
