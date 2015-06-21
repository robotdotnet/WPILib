using System;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HAL_Simulator
{
    //Semaphore

    [StructLayout(LayoutKind.Sequential)]
    internal struct MUTEX_ID
    {
        public object lockObject;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct SEMAPHORE_ID
    {
        public Semaphore semaphore;
        //public object lockObject;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MULTIWAIT_ID
    {
        public object lockObject;
    }


    //HAL
    [StructLayout(LayoutKind.Sequential)]
    internal struct Port
    {
        public byte pin;
        public byte module;
    }

    //Analog
    [StructLayout(LayoutKind.Sequential)]
    internal struct AnalogPort
    {
        public Port port;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AnalogTrigger
    {
        public IntPtr portPtr;
        public Port port;
        public uint index;
    }

    //Compressor
    [StructLayout(LayoutKind.Sequential)]
    internal struct PCM
    {
        public byte module;
    }

    //Digital
    [StructLayout(LayoutKind.Sequential)]
    internal struct DigitalPort
    {
        public Port port;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PWM
    {
        public uint idx;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Counter
    {
        public uint idx;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Encoder
    {
        public uint idx;
    }

    //Interrupts
    [StructLayout(LayoutKind.Sequential)]
    internal struct Interrupt
    {
        //NotImplemented
        public bool idx;
    }

    //Notifier
    [StructLayout(LayoutKind.Sequential)]
    internal struct Notifier
    {
        public Action<uint, IntPtr> Callback;
        public Thread alarm;
    }

    //Solenoid
    [StructLayout(LayoutKind.Sequential)]
    internal struct SolenoidPort
    {
        public Port port;
    }

    //TalonSRX
    [StructLayout(LayoutKind.Sequential)]
    internal struct TalonSRX
    {
        public uint deviceNumber;
    }

}
