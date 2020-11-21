using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class AddressableLEDLowLevelNative
    {
        public AddressableLEDLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_FreeAddressableLEDFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeAddressableLED");
            HAL_InitializeAddressableLEDFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeAddressableLED");
            HAL_SetAddressableLEDBitTimingFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAddressableLEDBitTiming");
            HAL_SetAddressableLEDLengthFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAddressableLEDLength");
            HAL_SetAddressableLEDOutputPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAddressableLEDOutputPort");
            HAL_SetAddressableLEDSyncTimeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAddressableLEDSyncTime");
            HAL_StartAddressableLEDOutputFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_StartAddressableLEDOutput");
            HAL_StopAddressableLEDOutputFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_StopAddressableLEDOutput");
            HAL_WriteAddressableLEDDataFunc = (delegate* unmanaged[Cdecl] < System.Int32, Hal.AddressableLEDData *, System.Int32, int *, void >)loader.GetProcAddress("HAL_WriteAddressableLEDData");
        }

        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeAddressableLEDFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeAddressableLED(int handle)
        {
            HAL_FreeAddressableLEDFunc(handle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeAddressableLEDFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeAddressableLED(int outputPort)
        {
            int status = 0;
            var retVal = HAL_InitializeAddressableLEDFunc(outputPort, &status);
            Hal.StatusHandling.AddressableLEDStatusCheck(status, outputPort);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int, int, int, int*, void> HAL_SetAddressableLEDBitTimingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAddressableLEDBitTiming(int handle, int lowTime0NanoSeconds, int highTime0NanoSeconds, int lowTime1NanoSeconds, int highTime1NanoSeconds)
        {
            int status = 0;
            HAL_SetAddressableLEDBitTimingFunc(handle, lowTime0NanoSeconds, highTime0NanoSeconds, lowTime1NanoSeconds, highTime1NanoSeconds, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAddressableLEDLengthFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAddressableLEDLength(int handle, int length)
        {
            int status = 0;
            HAL_SetAddressableLEDLengthFunc(handle, length, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAddressableLEDOutputPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAddressableLEDOutputPort(int handle, int outputPort)
        {
            int status = 0;
            HAL_SetAddressableLEDOutputPortFunc(handle, outputPort, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAddressableLEDSyncTimeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAddressableLEDSyncTime(int handle, int syncTimeMicroSeconds)
        {
            int status = 0;
            HAL_SetAddressableLEDSyncTimeFunc(handle, syncTimeMicroSeconds, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_StartAddressableLEDOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StartAddressableLEDOutput(int handle)
        {
            int status = 0;
            HAL_StartAddressableLEDOutputFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_StopAddressableLEDOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StopAddressableLEDOutput(int handle)
        {
            int status = 0;
            HAL_StopAddressableLEDOutputFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, AddressableLEDData*, int, int*, void> HAL_WriteAddressableLEDDataFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_WriteAddressableLEDData(int handle, AddressableLEDData* data, int length)
        {
            int status = 0;
            HAL_WriteAddressableLEDDataFunc(handle, data, length, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
