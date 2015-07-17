using System.Runtime.InteropServices;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    public class HALPDP
    {
        [CalledSimFunction]
        public static void initializePDP(int module)
        {

        }

        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTemperature")]
        [CalledSimFunction]
        public static extern double getPDPTemperature(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPVoltage")]
        [CalledSimFunction]
        public static extern double getPDPVoltage(ref int status, byte module);


        /// Return Type: double
        ///channel: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPChannelCurrent")]
        [CalledSimFunction]
        public static extern double getPDPChannelCurrent(byte channel, ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalCurrent")]
        [CalledSimFunction]
        public static extern double getPDPTotalCurrent(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalPower")]
        [CalledSimFunction]
        public static extern double getPDPTotalPower(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalEnergy")]
        [CalledSimFunction]
        public static extern double getPDPTotalEnergy(ref int status, byte module);


        /// Return Type: void
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "resetPDPTotalEnergy")]
        [CalledSimFunction]
        public static extern void resetPDPTotalEnergy(ref int status, byte module);


        /// Return Type: void
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "clearPDPStickyFaults")]
        [CalledSimFunction]
        public static extern void clearPDPStickyFaults(ref int status, byte module);
    }
}
