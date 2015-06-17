using System;
using System.Runtime.InteropServices;
using static HAL_Simulator.SimData;

namespace HAL_Simulator
{
    public class HALSolenoid
    {
        private static SolenoidPort GetSolenoidPort(IntPtr sPort)
        {
            return (SolenoidPort)Marshal.PtrToStructure(sPort, typeof (SolenoidPort));
        }

        public static IntPtr initializeSolenoidPort(IntPtr port_pointer, ref int status)
        {
            SolenoidPort p = new SolenoidPort {port = (Port)Marshal.PtrToStructure(port_pointer, typeof(Port))};
            status = 0;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);
            return ptr;
        }

        public static bool checkSolenoidModule(byte module)
        {
            return module < 63;
        }

        public static bool getSolenoid(IntPtr solenoid_port_pointer, ref int status)
        {
            status = 0;
            var p = GetSolenoidPort(solenoid_port_pointer);
            return halData["solenoid"][p.port.pin]["value"];
        }


        public static void setSolenoid(IntPtr solenoid_port_pointer, bool value,
            ref int status)
        {
            status = 0;
            halData["solenoid"][GetSolenoidPort(solenoid_port_pointer).port.pin]["value"] = value;
        }

        public static int getPCMSolenoidBlackList(IntPtr solenoid_port_pointer, ref int status)
        {
            status = 0;
            return 0;
        }

        public static bool getPCMSolenoidVoltageStickyFault(IntPtr solenoid_port_pointer, ref int status)
        {
            status = 0;
            return false;
        }


        public static bool  getPCMSolenoidVoltageFault(IntPtr solenoid_port_pointer, ref int status)
        {
            status = 0;
            return false;
        }


        public static void clearAllPCMStickyFaults_sol(IntPtr solenoid_port_pointer, ref int status)
        {
            status = 0;
        }
    }
}
