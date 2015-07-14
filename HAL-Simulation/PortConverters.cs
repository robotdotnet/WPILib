using System;
using System.Runtime.InteropServices;

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

        public static PWM GetPWM(IntPtr ptr)
        {
            return (PWM)Marshal.PtrToStructure(ptr, typeof(PWM));
        }

        public static Encoder GetEncoder(IntPtr ptr)
        {
            return (Encoder)Marshal.PtrToStructure(ptr, typeof(Encoder));
        }

        public static Counter GetCounter(IntPtr ptr)
        {
            return (Counter)Marshal.PtrToStructure(ptr, typeof(Counter));
        }

        public static int GetTalonSRX(IntPtr ptr)
        {
            return ((TalonSRX)Marshal.PtrToStructure(ptr, typeof(TalonSRX))).deviceNumber;
        }
    }
}
