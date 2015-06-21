using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator
{
    internal static class PortConverters
    {
        public static Port GetHalPort(IntPtr ptr)
        {
            return (Port)Marshal.PtrToStructure(ptr, typeof(Port));
        }

        public static DigitalPort GetDigitalPort(IntPtr ptr)
        {
            return (DigitalPort)Marshal.PtrToStructure(ptr, typeof(DigitalPort));
        }

        public static SolenoidPort GetSolenoidPort(IntPtr ptr)
        {
            return (SolenoidPort)Marshal.PtrToStructure(ptr, typeof(SolenoidPort));
        }

        public static AnalogPort GetAnalogPort(IntPtr ptr)
        {
            return (AnalogPort)Marshal.PtrToStructure(ptr, typeof(AnalogPort));
        }

        public static AnalogTrigger GetAnalogTrigger(IntPtr ptr)
        {
            return (AnalogTrigger)Marshal.PtrToStructure(ptr, typeof(AnalogTrigger));
        }
    }
}
