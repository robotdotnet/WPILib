using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private static byte devices = 0;

        private Port port;
        private int bitOrder;
        private int clockPolarity;
        private int dataOnTrailing;

        public SPI(Port port)
        {
            int status = 0;
            this.port = port;
            ++devices;
            HAL_Base.HALDigital.SpiInitialize((byte)port, ref status);
            WPILib.Utility.CheckStatus(status);
            HAL_Base.HAL.Report(HAL_Base.ResourceType.kResourceType_SPI, devices);
        }

        public override void Free()
        {
            base.Free();
            HAL_Base.HALDigital.SpiClose((byte)port);
        }

        public void SetClockRate(int hz)
        {
            HAL_Base.HALDigital.SpiSetSpeed((byte)port, (uint)hz);
        }

        private void UpdateOpts()
        {
            HAL_Base.HALDigital.SpiSetOpts((byte)port, bitOrder, dataOnTrailing, clockPolarity);
        }

        public void SetMSBFirst()
        {
            bitOrder = 1;
            UpdateOpts();
        }

        public void SetLSBFirst()
        {
            bitOrder = 0;
            UpdateOpts();
        }

        public void SetClockActiveLow()
        {
            clockPolarity = 1;
            UpdateOpts();
        }

        public void SetClockActiveHigh()
        {
            clockPolarity = 0;
            UpdateOpts();
        }

        public void SetSampleDataOnFalling()
        {
            dataOnTrailing = 1;
            UpdateOpts();
        }

        public void SetSampleDataOnRising()
        {
            dataOnTrailing = 0;
            UpdateOpts();
        }

        public void SetChipSelectActiveHigh()
        {
            int status = 0;
            HAL_Base.HALDigital.SpiSetChipSelectActiveHigh((byte)port, ref status);
            WPILib.Utility.CheckStatus(status);
        }

        public void SetChipSelectActiveLow()
        {
            int status = 0;
            HAL_Base.HALDigital.SpiSetChipSelectActiveLow((byte)port, ref status);
            WPILib.Utility.CheckStatus(status);
        }

        public int Read(bool initiate, byte[] dataReceived, int size)
        {
            int retVal = 0;
            byte[] receivedBuffer = new byte[size];
            byte[] sendBuffer = new byte[size];
            if (initiate)
                retVal = HAL_Base.HALDigital.SpiTransaction((byte)port, sendBuffer, receivedBuffer, (byte)size);
            else
                retVal = HAL_Base.HALDigital.SpiRead((byte)port, receivedBuffer, (byte)size);
            Array.Copy(receivedBuffer, dataReceived, Math.Min(size, dataReceived.Length));
            return retVal;
        }

        public int Transaction(byte[] dataToSend, byte[] dataReceived, int size)
        {
            int retVal = 0;
            byte[] sendBuffer = new byte[size];
            Array.Copy(dataToSend, sendBuffer, Math.Min(dataToSend.Length, size));
            byte[] receivedBuffer = new byte[size];
            retVal = HAL_Base.HALDigital.SpiTransaction((byte)port, sendBuffer, receivedBuffer, (byte)size);
            Array.Copy(receivedBuffer, dataReceived, Math.Min(receivedBuffer.Length, dataReceived.Length));
            return retVal;
        }
    }
}
