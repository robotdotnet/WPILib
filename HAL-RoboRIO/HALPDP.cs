//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALPDP
    {
        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializePDP")]
        public static extern void initializePDP(int module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPTemperature")]
        public static extern double getPDPTemperature(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPVoltage")]
        public static extern double getPDPVoltage(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPChannelCurrent")]
        public static extern double getPDPChannelCurrent(byte channel, ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPTotalCurrent")]
        public static extern double getPDPTotalCurrent(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPTotalPower")]
        public static extern double getPDPTotalPower(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPDPTotalEnergy")]
        public static extern double getPDPTotalEnergy(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetPDPTotalEnergy")]
        public static extern void resetPDPTotalEnergy(ref int status, byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "clearPDPStickyFaults")]
        public static extern void clearPDPStickyFaults(ref int status, byte module);
    }
}
