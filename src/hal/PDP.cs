
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IPDP))]
    public unsafe static class PDP
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IPDP lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
