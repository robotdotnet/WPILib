using System.Runtime.InteropServices;
using static HAL_Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALPDP
    {
        [CalledSimFunction]
        internal static void initializePDP(int module)
        {
            InitializeNewPDP(module);
        }

        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTemperature")]
        [CalledSimFunction]
        internal static extern double getPDPTemperature(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPVoltage")]
        [CalledSimFunction]
        internal static extern double getPDPVoltage(ref int status, byte module);


        /// Return Type: double
        ///channel: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPChannelCurrent")]
        [CalledSimFunction]
        internal static extern double getPDPChannelCurrent(byte channel, ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalCurrent")]
        [CalledSimFunction]
        internal static extern double getPDPTotalCurrent(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalPower")]
        [CalledSimFunction]
        internal static extern double getPDPTotalPower(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalEnergy")]
        [CalledSimFunction]
        internal static extern double getPDPTotalEnergy(ref int status, byte module);


        /// Return Type: void
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "resetPDPTotalEnergy")]
        [CalledSimFunction]
        internal static extern void resetPDPTotalEnergy(ref int status, byte module);


        /// Return Type: void
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "clearPDPStickyFaults")]
        [CalledSimFunction]
        internal static extern void clearPDPStickyFaults(ref int status, byte module);
    }
}
