using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib
{
    public class I2C : SensorBase
    {
        public enum Port : byte
        {
            Onboard = 0,
            MXP = 1
        }

        private object synchronizeRoot = new object();
        private Port port;
        private int deviceAddress;

        public I2C(Port port, int deviceAddress)
        {
            this.port = port;
            this.deviceAddress = deviceAddress;
            int status = 0;
            HAL_Base.HALDigital.I2CInitialize((byte)port, ref status);
            //TODO: Check status
            HAL_Base.HAL.Report(HAL_Base.ResourceType.kResourceType_I2C, (byte)deviceAddress);
        }

        public bool Transaction(byte[] dataToSend, int sendSize, byte[] dataRecieved, int receiveSize)
        {
            lock (synchronizeRoot)
            {
                byte[] sendBuffer = new byte[sendSize];
                Array.Copy(dataToSend, sendBuffer, Math.Min(sendSize, dataToSend.Length));
                byte[] receiveBuffer = new byte[receiveSize];
                bool aborted = true;
                aborted = HAL_Base.HALDigital
                    .I2CTransactionArray((byte)port, (byte)deviceAddress, sendBuffer, (byte)sendSize, receiveBuffer, (byte)receiveSize) != 0;
                if (receiveSize > 0 && receiveBuffer != null)
                    Array.Copy(receiveBuffer, dataRecieved, Math.Min(receiveSize, dataRecieved.Length));
                return aborted;
            }
        }

        public bool AddressOnly()
        {
            return Transaction(null, 0, null, 0);
        }

        public bool Write(int registerAddress, int data)
        {
            lock (synchronizeRoot)
            {
                byte[] buffer = new byte[2];
                buffer[0] = (byte)registerAddress;
                buffer[1] = (byte)data;
                return HAL_Base.HALDigital.I2CWriteArray((byte)port, (byte)deviceAddress, buffer, (byte)buffer.Length) < 0;
            }
        }

        public bool WriteBulk(byte[] data)
        {
            lock (synchronizeRoot)
            {
                return HAL_Base.HALDigital.I2CWriteArray((byte)port, (byte)deviceAddress, data, (byte)data.Length) < 0;
            }
        }

        public bool Read(int registerAddress, int count, byte[] buffer)
        {
            WPILib.Util.BoundaryException.AssertWithinBounds(count, 1, 7);
            if (buffer == null) throw new ArgumentNullException("buffer");
            return Transaction(new byte[1] { (byte)registerAddress }, 1, buffer, count);
        }

        public bool ReadOnly(byte[] buffer, int count)
        {
            WPILib.Util.BoundaryException.AssertWithinBounds(count, 1, 7);
            if (buffer == null) throw new ArgumentNullException("buffer");
            byte[] received = new byte[count];
            int retVal = HAL_Base.HALDigital.I2CReadArray((byte)port, (byte)deviceAddress, received, (byte)count);
            Array.Copy(received, buffer, Math.Min(buffer.Length, count));
            return retVal < 0;
        }

        public void Broadcast(int registerAddress, int data)
        {
            //NOTE: Is also not implemented in the Java implementation of WPILib
            throw new NotImplementedException();
        }

        public bool VerifySensor(int registerAddress, int count, byte[] expected)
        {
            // TODO: Make use of all 7 read bytes
            byte[] deviceData = new byte[4];
            for (int i = 0, curRegisterAddress = registerAddress; i < count; i += 4, curRegisterAddress += 4)
            {
                int toRead = count - i < 4 ? count - i : 4;
                // Read the chunk of data. Return false if the sensor does not
                // respond.
                if (Read(curRegisterAddress, toRead, deviceData))
                {
                    return false;
                }

                for (byte j = 0; j < toRead; j++)
                {
                    if (deviceData[j] != expected[i + j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
