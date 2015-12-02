using System;
using System.Runtime.InteropServices;

namespace HAL.SimulatorHAL
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

        internal static DigitalPWMStruct GetPWM(IntPtr ptr)
        {
            return (DigitalPWMStruct)Marshal.PtrToStructure(ptr, typeof(DigitalPWMStruct));
        }

        internal static EncoderStruct GetEncoder(IntPtr ptr)
        {
            return (EncoderStruct)Marshal.PtrToStructure(ptr, typeof(EncoderStruct));
        }

        internal static CounterStruct GetCounter(IntPtr ptr)
        {
            return (CounterStruct)Marshal.PtrToStructure(ptr, typeof(CounterStruct));
        }

        internal static int GetTalonSRX(IntPtr ptr)
        {
            return ((TalonSRX)Marshal.PtrToStructure(ptr, typeof(TalonSRX))).deviceNumber;
        }

        internal static int GetPCM(IntPtr ptr)
        {
            return ((PCM) Marshal.PtrToStructure(ptr, typeof (PCM))).module;
        }
    }
}
