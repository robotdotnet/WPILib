using System.Runtime.InteropServices;

namespace HAL_Simulator
{
    public class HALPDP
    {
        public static void initializePDP(int module)
        {
            
        }

        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTemperature")]
        public static extern double getPDPTemperature(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPVoltage")]
        public static extern double getPDPVoltage(ref int status, byte module);


        /// Return Type: double
        ///channel: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPChannelCurrent")]
        public static extern double getPDPChannelCurrent(byte channel, ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalCurrent")]
        public static extern double getPDPTotalCurrent(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalPower")]
        public static extern double getPDPTotalPower(ref int status, byte module);


        /// Return Type: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPDPTotalEnergy")]
        public static extern double getPDPTotalEnergy(ref int status, byte module);


        /// Return Type: void
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "resetPDPTotalEnergy")]
        public static extern void resetPDPTotalEnergy(ref int status, byte module);


        /// Return Type: void
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "clearPDPStickyFaults")]
        public static extern void clearPDPStickyFaults(ref int status, byte module);
    }
}
