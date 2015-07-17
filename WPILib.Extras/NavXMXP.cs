using System;
using System.Threading;

namespace WPILib.Extras
{
    public class NavXMXP_SPI
    {
        public enum DeviceRegister
        {
            WHOAMI = 0,
            HW_REV = 1,
            FW_VER_MAJOR = 2,
        }

        private const byte Address = 0x00;
        private SPI m_spi;

        public NavXMXP_SPI(SPI.Port port, byte updateRate = 50)
        {
            m_spi = new SPI(port);
            Thread.Sleep(250);
            //Read(DeviceRegister.WHOAMI, 2);
            //Read(DeviceRegister.WHOAMI, 2);
            Thread.Sleep(250);

        }
        /*
        public bool Read(DeviceRegister register, byte readSize, byte[] retData)
        {
            SPIRead()
        }
        8?*/
        

        private void SPIWrite(byte register, byte[] message, ref byte[] retData)
        {
            if (register == 0)
                register = 0x80;
            byte[] sendData = new byte[message.Length + 1];

            sendData[0] = register;
            for (int i = 0; i < message.Length; i++)
            {
                sendData[i + 1] = message[i];
            }
            SPICRC(ref sendData);
            retData = new byte[sendData.Length];
            m_spi.Transaction(sendData, retData, sendData.Length);

            //m_i2C.LVWrite(register, message);
        }

        private bool SPIRead(byte bytesToRead, byte register, ref byte[] returnData)
        {
            if (returnData.Length < bytesToRead)
            {
                returnData = new byte[bytesToRead];
            }
            byte[] sendData = {register, bytesToRead};
            SPICRC(ref sendData);

            m_spi.Write(sendData, sendData.Length);
            byte[] receiveData = new byte[bytesToRead + 1];
            m_spi.Read(false, receiveData, bytesToRead + 1);

            Array.Copy(receiveData, returnData, bytesToRead);

            var retCRC = SPICRC(ref receiveData);
            return retCRC == bytesToRead;
        }

        private byte SPICRC(ref byte[] MSG)
        {
            var MSG_L = MSG.Length;
            byte i, j, crc = 0;
            for (i = 0; i < MSG_L; ++i)
            {
                crc ^= MSG[i];
                for (j = 0; j < 8; ++j)
                {
                    if ((crc & 1) != 0)
                    {
                        crc ^= 0x91;
                    }
                    crc >>= 1;
                }
            }
            byte[] newMSG = new byte[MSG_L + 1];
            for (int m = 0; m < MSG_L; m++)
            {
                newMSG[m] = MSG[m];
            }
            newMSG[MSG_L] = crc;
            MSG = newMSG;
            return crc;
        }
    }
}
