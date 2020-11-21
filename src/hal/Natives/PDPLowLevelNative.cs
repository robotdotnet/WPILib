using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class PDPLowLevelNative
    {
        public PDPLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CheckPDPChannelFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckPDPChannel");
            HAL_CheckPDPModuleFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckPDPModule");
            HAL_CleanPDPFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_CleanPDP");
            HAL_ClearPDPStickyFaultsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ClearPDPStickyFaults");
            HAL_GetPDPAllChannelCurrentsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double *, int *, void >)loader.GetProcAddress("HAL_GetPDPAllChannelCurrents");
            HAL_GetPDPChannelCurrentFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetPDPChannelCurrent");
            HAL_GetPDPTemperatureFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetPDPTemperature");
            HAL_GetPDPTotalCurrentFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetPDPTotalCurrent");
            HAL_GetPDPTotalEnergyFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetPDPTotalEnergy");
            HAL_GetPDPTotalPowerFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetPDPTotalPower");
            HAL_GetPDPVoltageFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetPDPVoltage");
            HAL_InitializePDPFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializePDP");
            HAL_ResetPDPTotalEnergyFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ResetPDPTotalEnergy");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckPDPChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckPDPChannel(int channel)
        {
            return HAL_CheckPDPChannelFunc(channel);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckPDPModuleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckPDPModule(int module)
        {
            return HAL_CheckPDPModuleFunc(module);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_CleanPDPFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CleanPDP(int handle)
        {
            HAL_CleanPDPFunc(handle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ClearPDPStickyFaultsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ClearPDPStickyFaults(int handle)
        {
            int status = 0;
            HAL_ClearPDPStickyFaultsFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double*, int*, void> HAL_GetPDPAllChannelCurrentsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_GetPDPAllChannelCurrents(int handle, double* currents)
        {
            int status = 0;
            HAL_GetPDPAllChannelCurrentsFunc(handle, currents, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, double> HAL_GetPDPChannelCurrentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPChannelCurrent(int handle, int channel)
        {
            int status = 0;
            var retVal = HAL_GetPDPChannelCurrentFunc(handle, channel, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPTemperatureFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPTemperature(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPTemperatureFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPTotalCurrentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPTotalCurrent(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPTotalCurrentFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPTotalEnergyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPTotalEnergy(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPTotalEnergyFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPTotalPowerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPTotalPower(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPTotalPowerFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPDPVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPDPVoltage(int handle)
        {
            int status = 0;
            var retVal = HAL_GetPDPVoltageFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializePDPFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializePDP(int module)
        {
            int status = 0;
            var retVal = HAL_InitializePDPFunc(module, &status);
            Hal.StatusHandling.PDPStatusCheck(status, module);
            return retVal;
        }



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
