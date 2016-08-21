using System.Runtime.CompilerServices;
using static HAL.Base.HAL;

namespace HAL.SimulatorHAL.Handles
{
    internal enum HALHandleEnum
    {
        Undefined = 0,
        DIO = 1,
        Port = 2,
        Notifier = 3,
        Interrupt = 4,
        AnalogOutput = 5,
        AnalogInput = 6,
        AnalogTrigger = 7,
        Relay = 8,
        PWM = 9,
        DigitalPWM = 10,
        Counter = 11,
        FPGAEncoder = 12,
        Encoder = 13,
        Compressor = 14,
        Solenoid = 15,
        AnalogGyro = 16
    }

    internal static class Handle
    {
        internal const short InvalidHandleIndex = -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static short GetHandleIndex(int handle)
        {
            return (short)(handle & 0xffff);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static HALHandleEnum GetHandleType(int handle)
        {
            return (HALHandleEnum) ((handle >> 24) & 0xff);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsHandleType(int handle, HALHandleEnum handleType)
        {
            return handleType == GetHandleType(handle);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static short GetHandleTypedIndex(int handle, HALHandleEnum enumType)
        {
            if (!IsHandleType(handle, enumType)) return InvalidHandleIndex;
            return GetHandleIndex(handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static short GetPortHandlePin(int handle)
        {
            if (!IsHandleType(handle, HALHandleEnum.Port)) return InvalidHandleIndex;
            return (short)(handle & 0xff);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static short GetPortHandleModule(int handle)
        {
            if (!IsHandleType(handle, HALHandleEnum.Port)) return InvalidHandleIndex;
            return (short) ((handle >> 8) & 0xff);
        }

        internal static int CreatePortHandle(byte pin, byte module)
        {
            // set last 8 bits, then shift to first 8 bits
            int handle = (int)(HALHandleEnum.Port);
            handle = handle << 24;
            // shift module and add to 3rd set of 8 bits
            int temp = module;
            temp = (temp << 8) & 0xff00;
            handle += temp;
            // add pin to last 8 bits
            handle += pin;
            return handle;
        }

        internal static int CreateHandle(short index, HALHandleEnum handleType)
        {
            if (index < 0) return 0;
            byte hType = (byte)(handleType);
            if (hType == 0 || hType > 127) return HALInvalidHandle;
            // set last 8 bits, then shift to first 8 bits
            int handle = hType;
            handle = handle << 24;
            // add index to set last 16 bits
            handle += index;
            return handle;
        }
    }
}
