using System;
using static WPILib.Utility;
using static HAL_Base.HAL;
using static HAL_Base.HALDigital;

namespace WPILib
{
    public class SPI : SensorBase
    {
        public enum Port : byte
        {
            OnboardCS0 = 0,
            OnboardCS1 = 1,
            OnboardCS2 = 2, 
            OnboardCS3 = 3,
            MXP = 4
        }

        private static byte s_devices = 0;

        private Port m_port;
        private int m_bitOrder;
        private int m_clockPolarity;
        private int m_dataOnTrailing;

        public SPI(Port port)
        {
            int status = 0;
            m_port = port;
            ++s_devices;
            SpiInitialize((byte)port, ref status);
            CheckStatus(status);
            Report(HAL_Base.ResourceType.kResourceType_SPI, s_devices);
        }

        public override void Dispose()
        {
            base.Dispose();
            SpiClose((byte)m_port);
        }

        public void SetClockRate(int hz)
        {
            SpiSetSpeed((byte)m_port, (uint)hz);
        }

        private void UpdateOpts()
        {
            SpiSetOpts((byte)m_port, m_bitOrder, m_dataOnTrailing, m_clockPolarity);
        }

        public void SetMSBFirst()
        {
            m_bitOrder = 1;
            UpdateOpts();
        }

        public void SetLSBFirst()
        {
            m_bitOrder = 0;
            UpdateOpts();
        }

        public void SetClockActiveLow()
        {
            m_clockPolarity = 1;
            UpdateOpts();
        }

        public void SetClockActiveHigh()
        {
            m_clockPolarity = 0;
            UpdateOpts();
        }

        public void SetSampleDataOnFalling()
        {
            m_dataOnTrailing = 1;
            UpdateOpts();
        }

        public void SetSampleDataOnRising()
        {
            m_dataOnTrailing = 0;
            UpdateOpts();
        }

        public void SetChipSelectActiveHigh()
        {
            int status = 0;
            SpiSetChipSelectActiveHigh((byte)m_port, ref status);
            CheckStatus(status);
        }

        public void SetChipSelectActiveLow()
        {
            int status = 0;
            SpiSetChipSelectActiveLow((byte)m_port, ref status);
            CheckStatus(status);
        }

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
            if (initiate)
                retVal = SpiTransaction((byte)m_port, sendBuffer, receivedBuffer, (byte)size);
            else
                retVal = SpiRead((byte)m_port, receivedBuffer, (byte)size);
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
