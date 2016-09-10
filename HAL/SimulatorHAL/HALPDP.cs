using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALPDP
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALPDP.HAL_InitializePDP = HAL_InitializePDP;
            Base.HALPDP.HAL_CheckPDPModule = HAL_CheckPDPModule;
            Base.HALPDP.HAL_GetPDPTemperature = HAL_GetPDPTemperature;
            Base.HALPDP.HAL_GetPDPVoltage = HAL_GetPDPVoltage;
            Base.HALPDP.HAL_GetPDPChannelCurrent = HAL_GetPDPChannelCurrent;
            Base.HALPDP.HAL_GetPDPTotalCurrent = HAL_GetPDPTotalCurrent;
            Base.HALPDP.HAL_GetPDPTotalPower = HAL_GetPDPTotalPower;
            Base.HALPDP.HAL_GetPDPTotalEnergy = HAL_GetPDPTotalEnergy;
            Base.HALPDP.HAL_ResetPDPTotalEnergy = HAL_ResetPDPTotalEnergy;
            Base.HALPDP.HAL_ClearPDPStickyFaults = HAL_ClearPDPStickyFaults;
        }

        public static void HAL_InitializePDP(int module, ref int status)
        {
        }

        public static bool HAL_CheckPDPModule(int module)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetPDPTemperature(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetPDPVoltage(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetPDPChannelCurrent(int module, int channel, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetPDPTotalCurrent(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetPDPTotalPower(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetPDPTotalEnergy(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_ResetPDPTotalEnergy(int module, ref int status)
        {
        }

        public static void HAL_ClearPDPStickyFaults(int module, ref int status)
        {
        }
    }
}

