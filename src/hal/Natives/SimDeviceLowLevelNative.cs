using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class SimDeviceLowLevelNative
    {
        public SimDeviceLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CreateSimDeviceFunc = (delegate* unmanaged[Cdecl] < System.Byte *, System.Int32 >)loader.GetProcAddress("HAL_CreateSimDevice");
            HAL_CreateSimValueFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte *, System.Int32, Hal.Value *, System.Int32 >)loader.GetProcAddress("HAL_CreateSimValue");
            HAL_CreateSimValueEnumFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte *, System.Int32, System.Int32, System.Byte * *, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CreateSimValueEnum");
            HAL_FreeSimDeviceFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeSimDevice");
            HAL_GetSimValueFunc = (delegate* unmanaged[Cdecl] < System.Int32, Hal.Value *, void >)loader.GetProcAddress("HAL_GetSimValue");
            HAL_SetSimValueFunc = (delegate* unmanaged[Cdecl] < System.Int32, Hal.Value *, void >)loader.GetProcAddress("HAL_SetSimValue");
        }

        private readonly delegate* unmanaged[Cdecl]<byte*, int> HAL_CreateSimDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CreateSimDevice(byte* name)
        {
            return HAL_CreateSimDeviceFunc(name);
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, Value*, int> HAL_CreateSimValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CreateSimValue(int device, byte* name, int rdonly, Value* initialValue)
        {
            return HAL_CreateSimValueFunc(device, name, rdonly, initialValue);
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, int, byte**, int, int> HAL_CreateSimValueEnumFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CreateSimValueEnum(int device, byte* name, int rdonly, int numOptions, byte** options, int initialValue)
        {
            return HAL_CreateSimValueEnumFunc(device, name, rdonly, numOptions, options, initialValue);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeSimDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeSimDevice(int handle)
        {
            HAL_FreeSimDeviceFunc(handle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, Value*, void> HAL_GetSimValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_GetSimValue(int handle, Value* value)
        {
            HAL_GetSimValueFunc(handle, value);
        }



        private readonly delegate* unmanaged[Cdecl]<int, Value*, void> HAL_SetSimValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSimValue(int handle, Value* value)
        {
            HAL_SetSimValueFunc(handle, value);
        }



    }
}
