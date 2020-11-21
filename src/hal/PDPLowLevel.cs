
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class PDPLowLevel
    {
        internal static PDPLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static int CheckChannel(int channel)
        {
            return lowLevel.HAL_CheckPDPChannel(channel);
        }

        public static int CheckModule(int module)
        {
            return lowLevel.HAL_CheckPDPModule(module);
        }

        public static void Clean(int handle)
        {
            lowLevel.HAL_CleanPDP(handle);
        }

        public static void ClearStickyFaults(int handle)
        {
            lowLevel.HAL_ClearPDPStickyFaults(handle);
        }

        public static void GetAllChannelCurrents(int handle, double* currents)
        {
            lowLevel.HAL_GetPDPAllChannelCurrents(handle, currents);
        }

        public static double GetChannelCurrent(int handle, int channel)
        {
            return lowLevel.HAL_GetPDPChannelCurrent(handle, channel);
        }

        public static double GetTemperature(int handle)
        {
            return lowLevel.HAL_GetPDPTemperature(handle);
        }

        public static double GetTotalCurrent(int handle)
        {
            return lowLevel.HAL_GetPDPTotalCurrent(handle);
        }

        public static double GetTotalEnergy(int handle)
        {
            return lowLevel.HAL_GetPDPTotalEnergy(handle);
        }

        public static double GetTotalPower(int handle)
        {
            return lowLevel.HAL_GetPDPTotalPower(handle);
        }

        public static double GetVoltage(int handle)
        {
            return lowLevel.HAL_GetPDPVoltage(handle);
        }

        public static int Initialize(int module)
        {
            return lowLevel.HAL_InitializePDP(module);
        }

        public static void ResetTotalEnergy(int handle)
        {
            lowLevel.HAL_ResetPDPTotalEnergy(handle);
        }

    }
}
