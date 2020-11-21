using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class PDPLowLevelNative : IPDP
    {
        [NativeFunctionPointer("HAL_CheckPDPChannel")]
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckPDPChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckPDPChannel(int channel)
        {
            return HAL_CheckPDPChannelFunc(channel);
        }


        [NativeFunctionPointer("HAL_CheckPDPModule")]
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckPDPModuleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckPDPModule(int module)
        {
            return HAL_CheckPDPModuleFunc(module);
        }


        [NativeFunctionPointer("HAL_CleanPDP")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_CleanPDPFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CleanPDP(int handle)
        {
            HAL_CleanPDPFunc(handle);
        }


        [NativeFunctionPointer("HAL_ClearPDPStickyFaults")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ClearPDPStickyFaultsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ClearPDPStickyFaults(int handle)
        {
            int status = 0;
            HAL_ClearPDPStickyFaultsFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_GetPDPAllChannelCurrents")]
        private readonly delegate* unmanaged[Cdecl]<int, double*, int*, void> HAL_GetPDPAllChannelCurrentsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_GetPDPAllChannelCurrents(int handle, double* currents)
        {
            int status = 0;
            HAL_GetPDPAllChannelCurrentsFunc(handle, currents, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_GetPDPChannelCurrent")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, double> HAL_GetPDPChannelCurrentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPChannelCurrent(int handle, int channel)
        {
            int status = 0;
            var retVal = HAL_GetPDPChannelCurrentFunc(handle, channel, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPDPTemperature")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPTemperatureFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPTemperature(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPTemperatureFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPDPTotalCurrent")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPTotalCurrentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPTotalCurrent(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPTotalCurrentFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPDPTotalEnergy")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPTotalEnergyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPTotalEnergy(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPTotalEnergyFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPDPTotalPower")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPTotalPowerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPTotalPower(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPTotalPowerFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPDPVoltage")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPVoltage(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPVoltageFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializePDP")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializePDPFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializePDP(int module)
        {
            int status = 0;
            var retVal = HAL_InitializePDPFunc(module, &status);
            Hal.StatusHandling.PDPStatusCheck(status, module);
            return retVal;
        }


        [NativeFunctionPointer("HAL_ResetPDPTotalEnergy")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ResetPDPTotalEnergyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ResetPDPTotalEnergy(int handle)
        {
            int status = 0;
            HAL_ResetPDPTotalEnergyFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
