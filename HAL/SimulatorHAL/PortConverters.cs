using HAL.Base;
using System;
using System.Runtime.InteropServices;

namespace HAL.SimulatorHAL
{
    internal static class PortConverters
    {
        internal static Port GetHalPort(HALPortSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
        }

        internal static DigitalPort GetDigitalPort(DigitalPortSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
           // return (DigitalPort)Marshal.PtrToStructure(ptr, typeof(DigitalPort));
        }

        internal static SolenoidPort GetSolenoidPort(SolenoidPortSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
            //return (SolenoidPort)Marshal.PtrToStructure(ptr, typeof(SolenoidPort));
        }

        internal static AnalogPort GetAnalogPort(AnalogInputPortSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
            //return (AnalogPort)Marshal.PtrToStructure(ptr, typeof(AnalogPort));
        }

        internal static AnalogPort GetAnalogOutputPort(AnalogOutputPortSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
            //return (AnalogPort)Marshal.PtrToStructure(ptr, typeof(AnalogPort));
        }

        internal static AnalogTrigger GetAnalogTrigger(AnalogTriggerPortSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
            //return (AnalogTrigger)Marshal.PtrToStructure(ptr, typeof(AnalogTrigger));
        }

        internal static DigitalPWMStruct GetPWM(IntPtr ptr)
        {
            return (DigitalPWMStruct)Marshal.PtrToStructure(ptr, typeof(DigitalPWMStruct));
        }

        internal static EncoderStruct GetEncoder(EncoderSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
            //return (EncoderStruct)Marshal.PtrToStructure(ptr, typeof(EncoderStruct));
        }

        internal static CounterStruct GetCounter(CounterSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
            //return (CounterStruct)Marshal.PtrToStructure(ptr, typeof(CounterStruct));
        }

        internal static int GetTalonSRX(CANTalonSafeHandle ptr)
        {
            return ptr.GetSimulatorPort().deviceNumber;
            //return ((TalonSRX)Marshal.PtrToStructure(ptr, typeof(TalonSRX))).deviceNumber;
        }

        internal static int GetPCM(IntPtr ptr)
        {
            return ((PCM) Marshal.PtrToStructure(ptr, typeof (PCM))).module;
        }
    }
}
