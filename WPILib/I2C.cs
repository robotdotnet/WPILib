using System;
using HAL.Base;
using WPILib.Exceptions;
using static WPILib.Utility;
using static HAL.Base.HAL;
using static HAL.Base.HALDigital;

namespace WPILib
{
    /// <summary>
    /// I2C bus interface class.
    /// </summary>
    public class I2C : SensorBase
    {
        /// <summary>
        /// Enumeration of all the avalible I2C ports.
        /// </summary>
        public enum Port : byte
        {
            /// <summary>Onboard Port</summary>
            Onboard = 0,
            /// <summary>MXP Port</summary>
            MXP = 1
        }

        private readonly object m_synchronizeRoot = new object();
        private readonly Port m_port;
        private readonly int m_deviceAddress;
        
        /// <summary>
        /// Creates a new I2C interface class
        /// </summary>
        /// <param name="port">The I2C port on the RoboRIO the device is connected to.</param>
        /// <param name="deviceAddress">The address of the device on the bud</param>
        public I2C(Port port, int deviceAddress)
        {
            m_port = port;
            m_deviceAddress = deviceAddress;
            int status = 0;
            I2CInitialize((byte)port, ref status);
            CheckStatus(status);
            Report(ResourceType.kResourceType_I2C, (byte)deviceAddress);
        }

        /// <summary>
        /// Starts a generic I2C transaction.
        /// </summary>
        /// <remarks>This is a lower-level interface to the I2C hardware giving you more control over
        /// each transaction.</remarks>
        /// <param name="dataToSend">Buffer of data to send as part of the transaction.</param>
        /// <param name="sendSize">Number of bytes to send as part of the transactions.</param>
        /// <param name="dataRecieved">Buffer to read data into.</param>
        /// <param name="receiveSize">Number of bytes to read from the device.</param>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool Transaction(byte[] dataToSend, int sendSize, byte[] dataRecieved, int receiveSize)
        {
            lock (m_synchronizeRoot)
            {
                byte[] sendBuffer = new byte[sendSize];
                if (sendSize > 0 && dataToSend != null)
                {
                    Array.Copy(dataToSend, sendBuffer, Math.Min(sendSize, dataToSend.Length));
                }
                byte[] receiveBuffer = new byte[receiveSize];
                bool aborted = true;
                aborted = I2CTransaction((byte)m_port, (byte)m_deviceAddress, sendBuffer, (byte)sendSize, receiveBuffer, (byte)receiveSize) != 0;
                if (receiveSize > 0 && dataRecieved != null)
                    Array.Copy(receiveBuffer, dataRecieved, Math.Min(receiveSize, dataRecieved.Length));
                return aborted;
            }
        }

        /// <summary>
        /// Attempts to address a device on the I2C bus.
        /// </summary>
        /// <remarks>This allows you to figure out if there is a device on the I2C bus that responds to
        /// the address specified in the constructor.</remarks>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool AddressOnly()
        {
            return Transaction(null, 0, null, 0);
        }

        /// <summary>
        /// Execute a write transaction with the device.
        /// </summary>
        /// <param name="registerAddress">The address of the register on the device
        /// to be written</param>
        /// <param name="data">The byte to write to the register on the device</param>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool Write(int registerAddress, int data)
        {
            lock (m_synchronizeRoot)
            {
                byte[] buffer = new byte[2];
                buffer[0] = (byte)registerAddress;
                buffer[1] = (byte)data;
                return I2CWrite((byte)m_port, (byte)m_deviceAddress, buffer, (byte)buffer.Length) < 0;
            }
        }

        /// <summary>
        /// Executes a multiple byte write with the device.
        /// </summary>
        /// <remarks>
        /// Write multiple bytes to a register on a device and wait for the transaction to complete.
        /// </remarks>
        /// <param name="data">The data to write to the device.</param>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool WriteBulk(byte[] data)
        {
            lock (m_synchronizeRoot)
            {
                return I2CWrite((byte)m_port, (byte)m_deviceAddress, data, (byte)data.Length) < 0;
            }
        }

        /// <summary>
        /// Executes a read transaction with the device
        /// </summary>
        /// <remarks>
        /// Read bytes from a device. Most I2C devices will auto-increment the register pointer
        /// interanlly allowing you to read consecutive registers in a single transaction.
        /// </remarks>
        /// <param name="registerAddress">The register to read first in the transaction.</param>
        /// <param name="count">The number of bytes to read in the transaction.</param>
        /// <param name="buffer">A buffer to read the data to. </param>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool Read(int registerAddress, int count, byte[] buffer)
        {
            if (count < 1)
            {
                throw new BoundaryException($"Value given must be at least 1, {count} given");
            }
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            return Transaction(new byte[] { (byte)registerAddress }, 1, buffer, count);
        }

        /// <summary>
        /// Execute a read only transaction with the device.
        /// </summary>
        /// <remarks>Read bytes from a device. This method does not 
        /// write any data to prompt the device</remarks>
        /// <param name="buffer">A buffer to read the data to.</param>
        /// <param name="count">The number of bytes to read from the device.</param>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool ReadOnly(byte[] buffer, int count)
        {
            if (count < 1)
            {
                throw new BoundaryException($"Value given must be at least 1, {count} given");
            }
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            byte[] received = new byte[count];
            int retVal = I2CRead((byte)m_port, (byte)m_deviceAddress, received, (byte)count);
            Array.Copy(received, buffer, Math.Min(buffer.Length, count));
            return retVal < 0;
        }

        /// <summary>
        /// Sends a broadcast write to all devices on the I2C bus.
        /// </summary>
        /// <remarks>This is currently not implemented.</remarks>
        /// <param name="registerAddress">The register to write on all devices on the bus.</param>
        /// <param name="data">The value to write to the devices.</param>
        /// /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool Broadcast(int registerAddress, int data)
        {
            //NOTE: Is also not implemented in the Java implementation of WPILib
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verify that a device's registers contain expected values.
        /// </summary>
        /// <remarks>Most devices will have a set of registers that contain a known value that
        /// can be used to identify them. This allows an I2C device driver to easily
        /// verify that the device contains the expected value.</remarks>
        /// <param name="registerAddress">The base register to start reading from the device.</param>
        /// <param name="count">The size of the field to be verified.</param>
        /// <param name="expected">A buffer containing the values expected from the device.</param>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
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

        /// <summary>
        /// Executes a write transaction with the device using the LabVIEW style API.
        /// </summary>
        /// <remarks>This method makes it easier to port Libraries from LabVIEW,
        /// such as the NavX MXP.</remarks>
        /// <param name="register">The register to write to.</param>
        /// <param name="message">The message to send to the register.</param>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool LvWrite(byte register, byte[] message)
        {
            byte[] newData = new byte[message.Length + 1];

            newData[0] = register;
            for (int i = 0; i < message.Length; i++)
            {
                newData[i + 1] = message[i];
            }
            lock (m_synchronizeRoot)
            {
                return I2CWrite((byte) m_port, (byte) m_deviceAddress, newData, (byte) newData.Length) < 0;
            }
        }

        /// <summary>
        /// Executes a read transaction with the device using the LabVIEW style API.
        /// </summary>
        /// <remarks>This method makes it easier to port Libraries from LabVIEW,
        /// such as the NavX MXP.</remarks>
        /// <param name="register">The register to start reading from.</param>
        /// <param name="bytesToRead">The number of bytes to read from the register.</param>
        /// <param name="returnData">A buffer for the data returned from the device.</param>
        /// <returns>True if transfer was aborted, otherwise false.</returns>
        public bool LvRead(byte[] register, byte bytesToRead, ref byte[] returnData)
        {
            lock (m_synchronizeRoot)
            {
                if (returnData.Length < bytesToRead)
                {
                    returnData = new byte[bytesToRead];
                }
                if (register.Length == 0)
                {
                    return I2CRead((byte) m_port, (byte) m_deviceAddress, returnData, bytesToRead) < 0;
                }
                else
                {
                    return I2CTransaction((byte) m_port, (byte) m_deviceAddress, register, (byte) register.Length, returnData,
                        bytesToRead) < 0;
                }
            }
        }
    }
}
