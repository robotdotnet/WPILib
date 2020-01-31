
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(ISimDevice))]
    public unsafe static class SimDevice
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static ISimDevice lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static int Create(byte* name)
        {
            return lowLevel.HAL_CreateSimDevice(name);
        }

        public static int CreateSimValue(int device, byte* name, int rdonly, Value* initialValue)
        {
            return lowLevel.HAL_CreateSimValue(device, name, rdonly, initialValue);
        }

        public static int CreateSimValueEnum(int device, byte* name, int rdonly, int numOptions, byte** options, int initialValue)
        {
            return lowLevel.HAL_CreateSimValueEnum(device, name, rdonly, numOptions, options, initialValue);
        }

        public static void Free(int handle)
        {
            lowLevel.HAL_FreeSimDevice(handle);
        }

        public static void GetSimValue(int handle, Value* value)
        {
            lowLevel.HAL_GetSimValue(handle, value);
        }

        public static void SetSimValue(int handle, Value* value)
        {
            lowLevel.HAL_SetSimValue(handle, value);
        }

    }
}
