using System;
using HAL.Base;
using static WPILib.Utility;
using static HAL.Base.HAL;
using static HAL.Base.HALSPI;

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

        private readonly Port m_port;
        private bool m_bitOrder;
        private bool m_clockPolarity;
        private bool m_dataOnTrailing;

        /// <summary>
        /// Creates a new SPI class.
        /// </summary>
        /// <param name="port">The physical SPI Port</param>
        public SPI(Port port)
        {
            int status = 0;
            m_port = port;
            ++s_devices;
            HAL_InitializeSPI((byte)port, ref status);
            CheckStatusForceThrow(status);
            Report(ResourceType.kResourceType_SPI, s_devices);
        }

        ///<inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            HAL_CloseSPI((byte)m_port);
        }

        /// <summary>
        /// Sets the Generated SPI Clock Rate
        /// </summary>
        /// <remarks>The default value is 500,000 Hz. The maximum value is 4,000,000 Hz.</remarks>
        /// <param name="hz">Rate in Hz.</param>
        public void SetClockRate(int hz)
        {
            HAL_SetSPISpeed((byte)m_port, hz);
        }

        private void UpdateOpts()
        {
            HAL_SetSPIOpts((byte)m_port, m_bitOrder, m_dataOnTrailing, m_clockPolarity);
        }

        /// <summary>
        /// Set the port to send and receive the MSB First.
        /// </summary>
        public void SetMSBFirst()
        {
            m_bitOrder = true;
            UpdateOpts();
        }

        /// <summary>
        /// Set the port to send and receive the LSB First.
        /// </summary>
        public void SetLSBFirst()
        {
            m_bitOrder = false;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the clock line to be active low.
        /// </summary>
        public void SetClockActiveLow()
        {
            m_clockPolarity = true;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the clock line to be active low.
        /// </summary>
        public void SetClockActiveHigh()
        {
            m_clockPolarity = false;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the data to be stable on the falling edge and changing on the rising edge.
        /// </summary>
        public void SetSampleDataOnFalling()
        {
            m_dataOnTrailing = true;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the data to be stable on the rising edge and changing on the falling edge.
        /// </summary>
        public void SetSampleDataOnRising()
        {
            m_dataOnTrailing = false;
            UpdateOpts();
        }

        /// <summary>
        /// Configure the chip select line to be active high.
        /// </summary>
        public void SetChipSelectActiveHigh()
        {
            int status = 0;
            HAL_SetSPIChipSelectActiveHigh((byte)m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Configure the chip select line to be active low.
        /// </summary>
        public void SetChipSelectActiveLow()
        {
            int status = 0;
            HAL_SetSPIChipSelectActiveLow((byte)m_port, ref status);
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
            return HAL_WriteSPI((byte)m_port, sendBuffer, (byte)size);
        }

        /// <summary>
        /// Reads a word from the receive FIFO.
        /// </summary>
        /// <remarks>
        /// Waits for the current transfer to complete if the FIFO array is empty.
        /// If the receive FIFO is empty, there is no active transfer, and initiate is false, errors.
        /// </remarks>
        /// <param name="initiate">If true, this method pushes "0" into the transmit buffer and 
        /// initiates a transfer.</param>
        /// <param name="dataReceived">An array to hold the data received.</param>
        /// <param name="size">The size of data to receive.</param>
        /// <returns></returns>
        public int Read(bool initiate, byte[] dataReceived, int size)
        {
            int retVal = 0;
            byte[] receivedBuffer = new byte[size];
            byte[] sendBuffer = new byte[size];
            retVal = initiate ? HAL_TransactionSPI((byte)m_port, sendBuffer, receivedBuffer, (byte)size) : HAL_ReadSPI((byte)m_port, receivedBuffer, (byte)size);
            Array.Copy(receivedBuffer, dataReceived, Math.Min(size, dataReceived.Length));
            return retVal;
        }

        /// <summary>
        /// Perform a simultaneous read/write transaction with the device.
        /// </summary>
        /// <param name="dataToSend">The data to be written out to the device.</param>
        /// <param name="dataReceived">Buffer to receive data from the device.</param>
        /// <param name="size">The length of the transaction, in bytes.</param>
        /// <returns></returns>
        public int Transaction(byte[] dataToSend, byte[] dataReceived, int size)
        {
            int retVal = 0;
            byte[] sendBuffer = new byte[size];
            Array.Copy(dataToSend, sendBuffer, Math.Min(dataToSend.Length, size));
            byte[] receivedBuffer = new byte[size];
            retVal = HAL_TransactionSPI((byte)m_port, sendBuffer, receivedBuffer, (byte)size);
            Array.Copy(receivedBuffer, dataReceived, Math.Min(receivedBuffer.Length, dataReceived.Length));
            return retVal;
        }

        /// <summary>
        /// Initialize the accumulator.
        /// </summary>
        /// <param name="period">Time between reads.</param>
        /// <param name="cmd">SPI command to send to request data</param>
        /// <param name="xferSize">SPI transfer size, in bytes</param>
        /// <param name="validMask">Mask to apply to received data for validity checking.</param>
        /// <param name="validValue">After validMask is applied, required matching value for
        /// validity checking.</param>
        /// <param name="dataShift">Bit shift to apply to received data to get actual data value</param>
        /// <param name="dataSize">Size (int bits) of data field</param>
        /// <param name="isSigned">Is data field signed?</param>
        /// <param name="bigEndian">Is device big endian?</param>
        public void InitAccumulator(double period, int cmd, int xferSize, int validMask, int validValue,
            int dataShift, int dataSize, bool isSigned, bool bigEndian)
        {
            int status = 0;
            HAL_InitSPIAccumulator((byte)m_port, (int)(period * 1.0e6), cmd, (byte)xferSize, validMask,
                validValue, (byte)dataShift, (byte)dataSize, isSigned, bigEndian, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Frees the accumulator.
        /// </summary>
        public void FreeAccumulator()
        {
            int status = 0;
            HAL_FreeSPIAccumulator((byte)m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Resets the accumulator to zero.
        /// </summary>
        public void ResetAccumulator()
        {
            int status = 0;
            HAL_ResetSPIAccumulator((byte)m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Sets the center value of the accumulator.
        /// </summary>
        /// <remarks>
        /// The center value is subtracted from each value before it is added to the 
        /// accumulator. This is used for the center value of devices like gyros and
        /// accelerometers to make integration work and to take the device offset
        /// into account when integrating.
        /// </remarks>
        /// <param name="center">The center value to set.</param>
        public void SetAccumulatorCenter(int center)
        {
            int status = 0;
            HAL_SetSPIAccumulatorCenter((byte)m_port, center, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Sets the accumulator's deadband.
        /// </summary>
        /// <param name="deadband">The deadband to set.</param>
        public void SetAccumulatorDeadband(int deadband)
        {
            int status = 0;
            HAL_SetSPIAccumulatorDeadband((byte)m_port, deadband, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Read the last value read by the accumulator engine.
        /// </summary>
        /// <returns>The last value from the accumulator</returns>
        public int GetAccumulatorLastValue()
        {
            int status = 0;
            int retVal = HAL_GetSPIAccumulatorLastValue((byte)m_port, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Read the accumulated value.
        /// </summary>
        /// <returns>The 64-bit value accumulated since the last Reset().</returns>
        public long GetAccumulatorValue()
        {
            int status = 0;
            long retVal = HAL_GetSPIAccumulatorValue((byte)m_port, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Read the number of accumulated values.
        /// </summary>
        /// <returns>The number of times samples from the channel were accumulated.</returns>
        public long GetAccumulatorCount()
        {
            int status = 0;
            long retVal = HAL_GetSPIAccumulatorCount((byte)m_port, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Read the average of the accumulated value.
        /// </summary>
        /// <returns>The accumulated average value (value / count)</returns>
        public double GetAccumulatorAverage()
        {
            int status = 0;
            double retVal = HAL_GetSPIAccumulatorAverage((byte)m_port, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Read the accumulated value and the number of accumulated values atomically
        /// </summary>
        /// <param name="value">The 64 bit accumulated output</param>
        /// <param name="count">The number of accumulation cycles</param>
        public void GetAccumulatorOutput(ref long value, ref long count)
        {
            int status = 0;
            HAL_GetSPIAccumulatorOutput((byte)m_port, ref value, ref count, ref status);
            CheckStatus(status);
        }
    }
}
