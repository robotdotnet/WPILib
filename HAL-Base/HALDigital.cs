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

    }
}
