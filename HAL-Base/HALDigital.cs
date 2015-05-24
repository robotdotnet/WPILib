using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System;

namespace HAL_Base
{
    public partial class HALDigital
    {
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiWrite")]
        public static extern int SpiWriteArray(byte port, byte[] dataToSend, byte sendSize);
    }
}
