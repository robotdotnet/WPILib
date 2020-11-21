using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class SimDeviceLowLevelNative
    {
        [NativeFunctionPointer("HAL_CreateSimDevice")]
        private readonly delegate* unmanaged[Cdecl]<byte*, int> HAL_CreateSimDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CreateSimDevice(byte* name)
        {
            return HAL_CreateSimDeviceFunc(name);
        }


        [NativeFunctionPointer("HAL_CreateSimValue")]
        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, Value*, int> HAL_CreateSimValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CreateSimValue(int device, byte* name, int rdonly, Value* initialValue)
        {
            return HAL_CreateSimValueFunc(device, name, rdonly, initialValue);
        }


        [NativeFunctionPointer("HAL_CreateSimValueEnum")]
        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, int, byte**, int, int> HAL_CreateSimValueEnumFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CreateSimValueEnum(int device, byte* name, int rdonly, int numOptions, byte** options, int initialValue)
        {
            return HAL_CreateSimValueEnumFunc(device, name, rdonly, numOptions, options, initialValue);
        }


        [NativeFunctionPointer("HAL_FreeSimDevice")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeSimDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeSimDevice(int handle)
        {
            HAL_FreeSimDeviceFunc(handle);
        }


        [NativeFunctionPointer("HAL_GetSimValue")]
        private readonly delegate* unmanaged[Cdecl]<int, Value*, void> HAL_GetSimValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_GetSimValue(int handle, Value* value)
        {
            HAL_GetSimValueFunc(handle, value);
        }


        [NativeFunctionPointer("HAL_SetSimValue")]
        private readonly delegate* unmanaged[Cdecl]<int, Value*, void> HAL_SetSimValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSimValue(int handle, Value* value)
        {
            HAL_SetSimValueFunc(handle, value);
        }



    }
}
