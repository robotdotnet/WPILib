//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALPDP
    {
        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializePDP")]
        internal static extern void initializePDP(int module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPTemperature")]
        internal static extern double getPDPTemperature(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPVoltage")]
        internal static extern double getPDPVoltage(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPChannelCurrent")]
        internal static extern double getPDPChannelCurrent(byte channel, ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPTotalCurrent")]
        internal static extern double getPDPTotalCurrent(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPTotalPower")]
        internal static extern double getPDPTotalPower(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPTotalEnergy")]
        internal static extern double getPDPTotalEnergy(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetPDPTotalEnergy")]
        internal static extern void resetPDPTotalEnergy(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "clearPDPStickyFaults")]
        internal static extern void clearPDPStickyFaults(ref int status, byte module);
    }
}
