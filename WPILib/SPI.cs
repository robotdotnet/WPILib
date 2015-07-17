using System;
using HAL_Base;
using static WPILib.Utility;
using static HAL_Base.HAL;
using static HAL_Base.HALDigital;

namespace WPILib
{
    /// <summary>
    /// This class is used to interface with the SPI ports on the RoboRIO
    /// </summary>
    public class SPI : SensorBase
    {
        /// <summary>
        /// The SPI ports available
        /// </summary>
        public enum Port : byte
        {
            ///The Onboard CS0 Port
            OnboardCS0 = 0,
            ///The Onboard CS1 Port
            OnboardCS1 = 1,
            ///The Onboard CS2 Port
            OnboardCS2 = 2,
            ///The Onboard CS3 Port
            OnboardCS3 = 3,
            ///The MXP SPI Port
            MXP = 4
        }

        private static byte s_devices = 0;

        private Port m_port;
        private int m_bitOrder;
        private int m_clockPolarity;
        private int m_dataOnTrailing;

        /// <summary>
        /// Creates a new SPI class.
        /// </summary>
        /// <param name="port">The physical SPI Port</param>
        public SPI(Port port)
        {
            int status = 0;
            m_port = port;
            ++s_devices;
            SpiInitialize((byte)port, ref status);
            CheckStatus(status);
            Report(ResourceType.kResourceType_SPI, s_devices);
        }

        ///<inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            SpiClose((byte)m_port);
        }

        /// <summary>
        /// Sets the Generated SPI Clock Rate
        /// </summary>
        /// <remarks>The default value is 500,000 Hz. The maximum value is 4,000,000 Hz.</remarks>
        /// <param name="hz">Rate in Hz.</param>
        public void SetClockRate(int hz)
        {
            SpiSetSpeed((byte)m_port, (uint)hz);
        }

        private void UpdateOpts()
        {
            SpiSetOpts((byte)m_port, m_bitOrder, m_dataOnTrailing, m_clockPolarity);
        }

        /// <summary>
        /// Set the port to send and receive the MSB First.
        /// </summary>
        public void SetMSBFirst()
        {
            m_bitOrder = 1;
            UpdateOpts();
        }

        /// <summary>
        /// Set the port to send and receive the LSB First.
        /// </summary>
        public void SetLSBFirst()
        {
            m_bitOrder = 0;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the clock line to be active low.
        /// </summary>
        public void SetClockActiveLow()
        {
            m_clockPolarity = 1;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the clock line to be active low.
        /// </summary>
        public void SetClockActiveHigh()
        {
            m_clockPolarity = 0;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the data to be stable on the falling edge and changing on the rising edge.
        /// </summary>
        public void SetSampleDataOnFalling()
        {
            m_dataOnTrailing = 1;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the data to be stable on the rising edge and changing on the falling edge.
        /// </summary>
        public void SetSampleDataOnRising()
        {
            m_dataOnTrailing = 0;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the chip select line to be active high.
        /// </summary>
        public void SetChipSelectActiveHigh()
        {
            int status = 0;
            SpiSetChipSelectActiveHigh((byte)m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Configure the chip select line to be active low.
        /// </summary>
        public void SetChipSelectActiveLow()
        {
            int status = 0;
            SpiSetChipSelectActiveLow((byte)m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Writes the data to the slave device.
        /// </summary>
        /// <remarks>Note that this will block until there is space in the output
        /// buffer.</remarks>
        /// <param name="dataToSend">The byte array to send.</param>
        /// <param name="size">The size of the byte array.</param>
        /// <returns></returns>
        public int Write(byte[] dataToSend, int size)
        {
            byte[] sendBuffer = new byte[size];
            Array.Copy(dataToSend, sendBuffer, Math.Min(dataToSend.Length, size));
            return SpiWrite((byte)m_port, sendBuffer, (byte)size);
        }

        public int Read(bool initiate, byte[] dataReceived, int size)
        {
            int retVal = 0;
            byte[] receivedBuffer = new byte[size];
            byte[] sendBuffer = new byte[size];
            retVal = initiate ? SpiTransaction((byte)m_port, sendBuffer, receivedBuffer, (byte)size) : SpiRead((byte)m_port, receivedBuffer, (byte)size);
            Array.Copy(receivedBuffer, dataReceived, Math.Min(size, dataReceived.Length));
            return retVal;
        }

        public int Transaction(byte[] dataToSend, byte[] dataReceived, int size)
        {
            int retVal = 0;
            byte[] sendBuffer = new byte[size];
            Array.Copy(dataToSend, sendBuffer, Math.Min(dataToSend.Length, size));
            byte[] receivedBuffer = new byte[size];
            retVal = SpiTransaction((byte)m_port, sendBuffer, receivedBuffer, (byte)size);
            Array.Copy(receivedBuffer, dataReceived, Math.Min(receivedBuffer.Length, dataReceived.Length));
            return retVal;
        }
    }
}
