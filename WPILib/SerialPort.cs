using System;
using System.Text;
using HAL;
using static HAL.HALSerialPort;
using static HAL.HAL;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Driver for the serial ports onboard the RoboRIO.
    /// </summary>
    public class SerialPort : IDisposable
    {
        /// <summary>
        /// Specifies the serial port to use on the RoboRIO.
        /// </summary>
        public enum Port
        {
            /// Onboard
            Onboard = 0,
            /// MXP
            MXP = 1,
            /// USB
            USB = 2,
        }

        /// <summary>
        /// Specifies the parity bit for a <see cref="SerialPort"/> object.
        /// </summary>
        public enum Parity
        {
            /// None
            None = 0,
            /// Odd
            Odd = 1,
            /// Even
            Even = 2,
            /// Mark
            Mark = 3,
            /// Space
            Space = 4,
        }

        /// <summary>
        /// Specifies the number of stop bits used for a <see cref="SerialPort"/> object.
        /// </summary>
        public enum StopBits
        {
            /// One
            One = 10,
            /// OnePointFive
            OnePointFive = 15,
            /// Two
            Two = 20,
        }

        /// <summary>
        /// Specifies the FlowControl settings for a <see cref="SerialPort"/> object
        /// </summary>
        public enum FlowControlEnum
        {
            /// None
            None = 0,
            /// XonXoff
            XonXoff = 1,
            /// RtsCts
            RtsCts = 2,
            /// DtrDsr
            DtrDsr = 4,

        }

        /// <summary>
        /// Specifies the WriteBufferMode for a <see cref="SerialPort"/> object.
        /// </summary>
        public enum WriteBufferModeEnum
        {
            /// Flushes on access.
            FlushOnAccess = 1,
            /// Flushes when full.
            FlushWhenFull = 2,
        }


        private readonly byte m_port;

        
        /// <summary>
        /// Creates an instance of the Serial Port class.
        /// </summary>
        /// <param name="baudRate">The baud rate to configure the serial port at.</param>
        /// <param name="port">The serial port to use.</param>
        /// <param name="dataBits">The number of data bits per transfer, between 5 and 8</param>
        /// <param name="parity">The type of parity checking to use</param>
        /// <param name="stopBits">The number of stop bits to use</param>
        public SerialPort(int baudRate, Port port, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One)
        {
            int status = 0;
            m_port = (byte)port;
            System.IO.Ports.SerialPort p  = new System.IO.Ports.SerialPort();
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

            //Set the default read buffer size to 1 to return bytes immediately
            ReadBufferSize = 1;

            //Set the default timeout to 5 seconds
            Timeout = 5.0f;

            //Don't wait until the buffer is full to transmit
            WriteBufferMode = WriteBufferModeEnum.FlushOnAccess;

            DisableTermination();

            Report(ResourceType.kResourceType_SerialPort, (byte) 0);

        }

        /// <inheritdoc/>
        public void Dispose()
        {
            int status = 0;
            SerialClose(m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Sets the type of flow control to enable on this port.
        /// </summary>
        /// <remarks>By default, flow control is disabled.</remarks>
        public FlowControlEnum FlowControl
        {
            set
            {
                int status = 0;
                SerialSetFlowControl(m_port, (byte) value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Enables termination with the specified terminator
        /// </summary>
        /// <remarks>Termation is currently only implemented for receive. When the terminator
        /// is received, the <see cref="Read"/> or <see cref="ReadString()"/> will return
        /// fewer bytes than requested, stopping after the terminator.</remarks>
        /// <param name="terminator">The character to use for termination.</param>
        public void EnableTermination(char terminator)
        {
            int status = 0;
            SerialEnableTermination(m_port, (byte) terminator, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Enables termination with the default terminator '\n'.
        /// </summary>
        /// <remarks>Termation is currently only implemented for receive. When the terminator
        /// is received, the <see cref="Read"/> or <see cref="ReadString()"/> will return
        /// fewer bytes than requested, stopping after the terminator.</remarks>
        public void EnableTermination()
        {
            EnableTermination('\n');
        }

        /// <summary>
        /// Disables termination
        /// </summary>
        public void DisableTermination()
        {
            int status = 0;
            SerialDisableTermination(m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Gets the number of bytes current available to read from the serial port.
        /// </summary>
        /// <returns>The number of bytes received.</returns>
        public int GetBytesReceived()
        {
                int retVal = 0;
                int status = 0;
                retVal = SerialGetBytesReceived(m_port, ref status);
                CheckStatus(status);
                return retVal;
        }

        /// <summary>
        /// Reads a string out of the buffer, reading the full buffer.
        /// </summary>
        /// <returns>The read string</returns>
        public string ReadString()
        {
            return ReadString(GetBytesReceived());
        }

        /// <summary>
        /// Reads a string out of the buffer.
        /// </summary>
        /// <param name="count">The number of characters to read into the string</param>
        /// <returns>The read string</returns>
        public string ReadString(int count)
        {
            byte[] output = Read(count);
            try
            {
                return Encoding.ASCII.GetString(output);
            }
            catch (DecoderFallbackException e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        /// <summary>
        /// Read raw bytes out of the buffer.
        /// </summary>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>An array of the read bytes</returns>
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

        /// <summary>
        /// Write raw bytes to the serial port.
        /// </summary>
        /// <param name="buffer">The buffer of bytes to write.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <returns>The number of bytes actually written to the port.</returns>
        public int Write(byte[] buffer, int count)
        {
            int status = 0;
            int retVal = (int)SerialWrite(m_port, buffer, count, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Writes a string to the serial port
        /// </summary>
        /// <param name="data">The string to write to the serial port.</param>
        /// <returns>The number of bytes written  to the port.</returns>
        public int WriteString(string data)
        {
            byte[] dataB = Encoding.ASCII.GetBytes(data);
            return Write(dataB, dataB.Length);
        }

        /// <summary>
        /// Configures the timeout of the serial port.
        /// </summary>
        /// <remarks>This defined the timeout for transactions with the hardware. It will affect
        /// reads if less bytes are available then the read buffer size (defaults to 1)
        /// and very large writes.</remarks>
        public double Timeout
        {
            set
            {
                int status = 0;
                SerialSetTimeout(m_port, (float) value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Sets the size of the input buffer.
        /// </summary>
        /// <remarks>Specify the amount of data that can be stored before
        /// data from the device is returned to <see cref="Read"/>. If you want
        /// data that is received immediately, set this to 1.
        /// <para/>If the buffer is not filled before the read timeout expired,
        /// all data that has been received so far will be returned.</remarks>
        public int ReadBufferSize
        {
            set
            {
                int status = 0;
                SerialSetReadBufferSize(m_port, (uint) value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Sets the size of the output buffer.
        /// </summary>
        /// <remarks>Specify the amount of data that can be stored before being transmitted to the device.</remarks>
        public int WriteBufferSize
        {
            set
            {
                int status = 0;
                SerialSetWriteBufferSize(m_port, (uint) value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Sets the flushing behavior of the output buffer
        /// </summary>
        /// <remarks>When set to <see cref="WriteBufferModeEnum.FlushOnAccess"/>, data is 
        /// synchronously written to the serial port after each call to <see cref="Write"/>
        /// <para/>When set to <see cref="WriteBufferModeEnum.FlushWhenFull"/>, data is only
        /// written to the serial port when the buffer is full or
        /// <see cref="Flush"/> is called.</remarks>
        public WriteBufferModeEnum WriteBufferMode
        {
            set
            {
                int status = 0;
                SerialSetWriteMode(m_port, (byte) value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Force the output buffer to be written to the port.
        /// </summary>
        /// <remarks>This is used when <see cref="WriteBufferMode"/> is set to <see cref="WriteBufferModeEnum.FlushWhenFull"/> 
        /// to force a flush before the buffer is full.</remarks>
        public void Flush()
        {
            int status = 0;
            SerialFlush(m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Reset the serial port driver to a know state.
        /// </summary>
        /// <remarks>Empties the transmit and receive buffers in the device
        /// and formatted I/O</remarks>
        public void Reset()
        {
            int status = 0;
            SerialClear(m_port, ref status);
            CheckStatus(status);
        }
    }
}
