using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;

namespace WPILib
{
    public class SerialPort
    {
        private byte m_port;

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

        public SerialPort(int baudRate, Port port, int dataBits, Parity parity, StopBits stopBits)
        {
            int status = 0;
            m_port = (byte)port;

            HALSerialPort.SerialInitializePort(m_port, ref status);
            Utility.CheckStatus(status);
            HALSerialPort.SerialSetBaudRate(m_port, (uint) baudRate, ref status);
            Utility.CheckStatus(status);
            HALSerialPort.SerialSetDataBits(m_port, (byte)dataBits, ref status);
            Utility.CheckStatus(status);
            HALSerialPort.SerialSetParity(m_port, (byte)parity, ref status);
            Utility.CheckStatus(status);
            HALSerialPort.SerialSetStopBits(m_port, (byte)stopBits, ref status);
            Utility.CheckStatus(status);

            SetReadBufferSize(1);

            SetTimeout(0.5f);

            SetWriteBufferMode(WriteBufferMode.FlushOnAccess);

            DisableTermination();

            HAL.Report(ResourceType.kResourceType_SerialPort, (byte) 0);

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

        public void Free()
        {
            int status = 0;
            HALSerialPort.SerialClose(m_port, ref status);
            Utility.CheckStatus(status);
        }

        public void SetFlowControl(FlowControl flowControl)
        {
            int status = 0;
            HALSerialPort.SerialSetFlowControl(m_port, (byte) flowControl, ref status);
            Utility.CheckStatus(status);
        }

        public void EnableTermination(char terminator)
        {
            int status = 0;
            HALSerialPort.SerialEnableTermination(m_port, (byte) terminator, ref status);
            Utility.CheckStatus(status);
        }

        public void EnableTermination()
        {
            EnableTermination('\n');
        }

        public void DisableTermination()
        {
            int status = 0;
            HALSerialPort.SerialDisableTermination(m_port, ref status);
            Utility.CheckStatus(status);
        }

        public int GetBytesReceived()
        {
            int retVal = 0;
            int status = 0;
            retVal = HALSerialPort.SerialGetBytesReceived(m_port, ref status);
            Utility.CheckStatus(status);
            return retVal;
        }

        public string ReadString()
        {
            return ReadString(GetBytesReceived());
        }

        public string ReadString(int count)
        {
            byte[] output = Read(count);
            try
            {
                return System.Text.Encoding.ASCII.GetString(output);
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
            int gotten = (int)HALSerialPort.SerialRead(m_port, data, count, ref status);
            Utility.CheckStatus(status);
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
            int retVal = (int)HALSerialPort.SerialWrite(m_port, buffer, count, ref status);
            Utility.CheckStatus(status);
            return retVal;
        }

        public int WriteString(string data)
        {
            byte[] dataB = Encoding.ASCII.GetBytes(data);
            return Write(dataB, dataB.Length);
        }

        public void SetTimeout(double timeout)
        {
            int status = 0;
            HALSerialPort.SerialSetTimeout(m_port, (float)timeout, ref status);
            Utility.CheckStatus(status);
        }

        public void SetReadBufferSize(int size)
        {
            int status = 0;
            HALSerialPort.SerialSetReadBufferSize(m_port, (uint) size, ref status);
            Utility.CheckStatus(status);
        }

        public void SetWriteBufferSize(int size)
        {
            int status = 0;
            HALSerialPort.SerialSetWriteBufferSize(m_port, (uint)size, ref status);
            Utility.CheckStatus(status);
        }

        public void SetWriteBufferMode(WriteBufferMode mode)
        {
            int status = 0;
            HALSerialPort.SerialSetWriteMode(m_port, (byte)mode, ref status);
            Utility.CheckStatus(status);
        }

        public void Flush()
        {
            int status = 0;
            HALSerialPort.SerialFlush(m_port, ref status);
            Utility.CheckStatus(status);
        }

        public void Reset()
        {
            int status = 0;
            HALSerialPort.SerialClear(m_port, ref status);
            Utility.CheckStatus(status);
        }
    }
}
