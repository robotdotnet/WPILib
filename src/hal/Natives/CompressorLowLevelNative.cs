using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class CompressorLowLevelNative
    {
        
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckCompressorModuleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckCompressorModule(int module)
        {
            return HAL_CheckCompressorModuleFunc(module);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressor(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorClosedLoopControlFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressorClosedLoopControl(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorClosedLoopControlFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetCompressorCurrentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetCompressorCurrent(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorCurrentFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorCurrentTooHighFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressorCurrentTooHighFault(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorCurrentTooHighFaultFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorCurrentTooHighStickyFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressorCurrentTooHighStickyFault(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorCurrentTooHighStickyFaultFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorNotConnectedFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressorNotConnectedFault(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorNotConnectedFaultFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorNotConnectedStickyFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressorNotConnectedStickyFault(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorNotConnectedStickyFaultFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorPressureSwitchFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressorPressureSwitch(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorPressureSwitchFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorShortedFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressorShortedFault(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorShortedFaultFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCompressorShortedStickyFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCompressorShortedStickyFault(int compressorHandle)
        {
            int status = 0;
            var retVal = HAL_GetCompressorShortedStickyFaultFunc(compressorHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeCompressorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeCompressor(int module)
        {
            int status = 0;
            var retVal = HAL_InitializeCompressorFunc(module, &status);
            Hal.StatusHandling.CompressorStatusCheck(status, module);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetCompressorClosedLoopControlFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCompressorClosedLoopControl(int compressorHandle, int value)
        {
            int status = 0;
            HAL_SetCompressorClosedLoopControlFunc(compressorHandle, value, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
