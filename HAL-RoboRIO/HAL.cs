//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HAL
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getPort")]
        public static extern System.IntPtr getPort(byte pin);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getPortWithModule")]
        public static extern System.IntPtr getPortWithModule(byte module, byte pin);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getHALErrorMessage")]
        public static extern string getHALErrorMessage(int code);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getFPGAVersion")]
        public static extern ushort getFPGAVersion(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getFPGARevision")]
        public static extern uint getFPGARevision(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getFPGATime")]
        public static extern uint getFPGATime(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getFPGAButton")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getFPGAButton(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALSetErrorData")]
        public static extern int HALSetErrorData(string errors, int errorsLength, int wait_ms);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetControlWord")]
        public static extern int HALGetControlWord(ref HALControlWord data);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetAllianceStation")]
        public static extern int HALGetAllianceStation(ref HALAllianceStationID allianceStation);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickAxes")]
        public static extern int HALGetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickPOVs")]
        public static extern int HALGetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickButtons")]
        public static extern int HALGetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickDescriptor")]
        public static extern int HALGetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALSetJoystickOutputs")]
        public static extern int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetMatchTime")]
        public static extern int HALGetMatchTime(ref float matchTime);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALSetNewDataSem")]
        public static extern void HALSetNewDataSem(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetSystemActive")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool HALGetSystemActive(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetBrownedOut")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool HALGetBrownedOut(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALInitialize")]
        public static extern int HALInitialize(int mode);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramStarting")]
        public static extern void HALNetworkCommunicationObserveUserProgramStarting();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramDisabled")]
        public static extern void HALNetworkCommunicationObserveUserProgramDisabled();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramAutonomous")]
        public static extern void HALNetworkCommunicationObserveUserProgramAutonomous();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTeleop")]
        public static extern void HALNetworkCommunicationObserveUserProgramTeleop();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTest")]
        public static extern void HALNetworkCommunicationObserveUserProgramTest();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALReport")]
        public static extern uint HALReport(byte resource, byte instanceNumber, byte context, string feature = null);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "NumericArrayResize")]
        public static extern void NumericArrayResize();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "RTSetCleanupProc")]
        public static extern void RTSetCleanupProc();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "EDVR_CreateReference")]
        public static extern void EDVR_CreateReference();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "Occur")]
        public static extern void Occur();
    }
}
