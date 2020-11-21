using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class RelayLowLevelNative : IRelay
    {
        [NativeFunctionPointer("HAL_CheckRelayChannel")]
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckRelayChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckRelayChannel(int channel)
        {
            return HAL_CheckRelayChannelFunc(channel);
        }


        [NativeFunctionPointer("HAL_FreeRelayPort")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeRelayPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeRelayPort(int relayPortHandle)
        {
            HAL_FreeRelayPortFunc(relayPortHandle);
        }


        [NativeFunctionPointer("HAL_GetRelay")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetRelayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetRelay(int relayPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetRelayFunc(relayPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializeRelayPort")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, int> HAL_InitializeRelayPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeRelayPort(int portHandle, int fwd)
        {
            int status = 0;
            var retVal = HAL_InitializeRelayPortFunc(portHandle, fwd, &status);
            Hal.StatusHandling.RelayStatusCheck(status, portHandle);
            return retVal;
        }


        [NativeFunctionPointer("HAL_SetRelay")]
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
