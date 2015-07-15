using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace

namespace HAL_Simulator
{
    internal static class PortConverters
    {
        internal static Port GetHalPort(IntPtr ptr)
        {
            return (Port)Marshal.PtrToStructure(ptr, typeof(Port));
        }

        internal static DigitalPort GetDigitalPort(IntPtr ptr)
        {
            return (DigitalPort)Marshal.PtrToStructure(ptr, typeof(DigitalPort));
        }

        internal static SolenoidPort GetSolenoidPort(IntPtr ptr)
        {
            return (SolenoidPort)Marshal.PtrToStructure(ptr, typeof(SolenoidPort));
        }

        internal static AnalogPort GetAnalogPort(IntPtr ptr)
        {
            return (AnalogPort)Marshal.PtrToStructure(ptr, typeof(AnalogPort));
        }

        internal static AnalogTrigger GetAnalogTrigger(IntPtr ptr)
        {
            return (AnalogTrigger)Marshal.PtrToStructure(ptr, typeof(AnalogTrigger));
        }

        internal static PWM GetPWM(IntPtr ptr)
        {
            return (PWM)Marshal.PtrToStructure(ptr, typeof(PWM));
        }

        internal static Encoder GetEncoder(IntPtr ptr)
        {
            return (Encoder)Marshal.PtrToStructure(ptr, typeof(Encoder));
        }

        internal static Counter GetCounter(IntPtr ptr)
        {
            return (Counter)Marshal.PtrToStructure(ptr, typeof(Counter));
        }

        internal static int GetTalonSRX(IntPtr ptr)
        {
            return ((TalonSRX)Marshal.PtrToStructure(ptr, typeof(TalonSRX))).deviceNumber;
        }
    }
}
