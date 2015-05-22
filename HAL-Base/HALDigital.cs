using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System;

namespace HAL_Base
{
    public partial class HALDigital
    {
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CTransaction")]
        public static extern int I2CTransactionArray(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize, byte[] dataReceived, byte receiveSize);

        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CWrite")]
        public static extern int I2CWriteArray(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize);

        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CRead")]
        public static extern int I2CReadArray(byte port, byte deviceAddress, byte[] buffer, byte count);

        [DllImport("libHALAthena_shared.so", EntryPoint = "spiTransaction")]
        public static extern int SpiTransactionArray(byte port, byte[] dataToSend, byte[] dataReceived, byte size);

        [DllImport("libHALAthena_shared.so", EntryPoint = "spiWrite")]
        public static extern int SpiWriteArray(byte port, byte[] dataToSend, byte sendSize);

        [DllImport("libHALAthena_shared.so", EntryPoint = "spiRead")]
        public static extern int SpiReadArray(byte port, byte[] buffer, byte count);
    }
}
