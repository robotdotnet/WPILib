using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class RelayLowLevelNative
    {
        public RelayLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CheckRelayChannelFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckRelayChannel");
            HAL_FreeRelayPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeRelayPort");
            HAL_GetRelayFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetRelay");
            HAL_InitializeRelayPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeRelayPort");
            HAL_SetRelayFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetRelay");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckRelayChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckRelayChannel(int channel)
        {
            return HAL_CheckRelayChannelFunc(channel);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeRelayPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeRelayPort(int relayPortHandle)
        {
            HAL_FreeRelayPortFunc(relayPortHandle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetRelayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetRelay(int relayPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetRelayFunc(relayPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, int> HAL_InitializeRelayPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeRelayPort(int portHandle, int fwd)
        {
            int status = 0;
            var retVal = HAL_InitializeRelayPortFunc(portHandle, fwd, &status);
            Hal.StatusHandling.RelayStatusCheck(status, portHandle);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetRelayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetRelay(int relayPortHandle, int setOn)
        {
            int status = 0;
            HAL_SetRelayFunc(relayPortHandle, setOn, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
