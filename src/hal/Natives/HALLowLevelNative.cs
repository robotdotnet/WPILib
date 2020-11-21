using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class HALLowLevelNative
    {
        public HALLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_InitializeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_Initialize");
            HAL_ExpandFPGATimeFunc = (delegate* unmanaged[Cdecl] < System.UInt32, int *, System.UInt64 >)loader.GetProcAddress("HAL_ExpandFPGATime");
            HAL_GetBrownedOutFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetBrownedOut");
            HAL_GetErrorMessageFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte *>)loader.GetProcAddress("HAL_GetErrorMessage");
            HAL_GetFPGAButtonFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetFPGAButton");
            HAL_GetFPGARevisionFunc = (delegate* unmanaged[Cdecl] < int *, System.Int64 >)loader.GetProcAddress("HAL_GetFPGARevision");
            HAL_GetFPGATimeFunc = (delegate* unmanaged[Cdecl] < int *, System.UInt64 >)loader.GetProcAddress("HAL_GetFPGATime");
            HAL_GetFPGAVersionFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetFPGAVersion");
            HAL_GetPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_GetPort");
            HAL_GetPortWithModuleFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_GetPortWithModule");
            HAL_GetRuntimeTypeFunc = (delegate* unmanaged[Cdecl] < Hal.RuntimeType >)loader.GetProcAddress("HAL_GetRuntimeType");
            HAL_GetSystemActiveFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetSystemActive");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int, int> HAL_InitializeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_Initialize(int timeout, int mode)
        {
            return HAL_InitializeFunc(timeout, mode);
        }



        private readonly delegate* unmanaged[Cdecl]<uint, int*, ulong> HAL_ExpandFPGATimeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong HAL_ExpandFPGATime(uint unexpanded_lower)
        {
            int status = 0;
            var retVal = HAL_ExpandFPGATimeFunc(unexpanded_lower, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetBrownedOutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetBrownedOut()
        {
            int status = 0;
            var retVal = HAL_GetBrownedOutFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte*> HAL_GetErrorMessageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* HAL_GetErrorMessage(int code)
        {
            return HAL_GetErrorMessageFunc(code);
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetFPGAButtonFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetFPGAButton()
        {
            int status = 0;
            var retVal = HAL_GetFPGAButtonFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, long> HAL_GetFPGARevisionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long HAL_GetFPGARevision()
        {
            int status = 0;
            var retVal = HAL_GetFPGARevisionFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, ulong> HAL_GetFPGATimeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong HAL_GetFPGATime()
        {
            int status = 0;
            var retVal = HAL_GetFPGATimeFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetFPGAVersionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetFPGAVersion()
        {
            int status = 0;
            var retVal = HAL_GetFPGAVersionFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_GetPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPort(int channel)
        {
            return HAL_GetPortFunc(channel);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int> HAL_GetPortWithModuleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPortWithModule(int module, int channel)
        {
            return HAL_GetPortWithModuleFunc(module, channel);
        }



        private readonly delegate* unmanaged[Cdecl]<RuntimeType> HAL_GetRuntimeTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RuntimeType HAL_GetRuntimeType()
        {
            return HAL_GetRuntimeTypeFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetSystemActiveFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSystemActive()
        {
            int status = 0;
            var retVal = HAL_GetSystemActiveFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



    }
}
