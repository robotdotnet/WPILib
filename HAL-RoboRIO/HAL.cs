using System;
using System.Reflection;
using HAL_Base;


namespace HAL_RoboRIO
{
    public class HAL
    {
        /// Return Type: void*
        ///pin: byte
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPort")]
        public static extern System.IntPtr GetPort(byte pin);


        /// Return Type: void*
        ///module: byte
        ///pin: byte
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPortWithModule")]
        public static extern System.IntPtr GetPortWithModule(byte module, byte pin);


        /// Return Type: char*
        ///code: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getHALErrorMessage")]
        public static extern string GetHALErrorMessage(int code);


        /// Return Type: unsigned short
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getFPGAVersion")]
        public static extern ushort GetFPGAVersion(ref int status);


        /// Return Type: unsigned int
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getFPGARevision")]
        public static extern uint GetFPGARevision(ref int status);


        /// Return Type: unsigned int
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getFPGATime")]
        public static extern uint GetFPGATime(ref int status);


        /// Return Type: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getFPGAButton")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool GetFPGAButton(ref int status);


        /// Return Type: int
        ///errors: char*
        ///errorsLength: int
        ///wait_ms: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALSetErrorData")]
        public static extern int SetErrorData([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string errors, int errorsLength, int waitMs);


        /// Return Type: int
        ///data: HALControlWord*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetControlWord")]
        public static extern int GetControlWord(ref uint data);


        /// Return Type: int
        ///allianceStation: HALAllianceStationID*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetAllianceStation")]
        public static extern int GetAllianceStation(ref HALAllianceStationID allianceStation);


        /// Return Type: int
        ///joystickNum: byte
        ///axes: HALJoystickAxes*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetJoystickAxes")]
        public static extern int GetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes);


        /// Return Type: int
        ///joystickNum: byte
        ///povs: HALJoystickPOVs*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetJoystickPOVs")]
        public static extern int GetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs);


        /// Return Type: int
        ///joystickNum: byte
        ///buttons: HALJoystickButtons*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetJoystickButtons")]
        public static extern int GetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons);


        /// Return Type: int
        ///joystickNum: byte
        ///desc: HALJoystickDescriptor*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetJoystickDescriptor")]
        public static extern int GetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc);


        /// Return Type: int
        ///joystickNum: byte
        ///outputs: unsigned int
        ///leftRumble: unsigned short
        ///rightRumble: unsigned short
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALSetJoystickOutputs")]
        public static extern int SetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);


        /// Return Type: int
        ///matchTime: float*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetMatchTime")]
        public static extern int GetMatchTime(ref float matchTime);


        /// Return Type: void
        ///sem: void*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALSetNewDataSem")]
        public static extern void SetNewDataSem(System.IntPtr sem);


        /// Return Type: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetSystemActive")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool GetSystemActive(ref int status);


        /// Return Type: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetBrownedOut")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool GetBrownedOut(ref int status);


        /// Return Type: int
        ///mode: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALInitialize")]
        public static extern int HALInitialize(int mode = 0);


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramStarting")]
        public static extern void NetworkCommunicationObserveUserProgramStarting();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramDisabled")]
        public static extern void NetworkCommunicationObserveUserProgramDisabled();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramAutonomous")]
        public static extern void NetworkCommunicationObserveUserProgramAutonomous();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTeleop")]
        public static extern void NetworkCommunicationObserveUserProgramTeleop();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTest")]
        public static extern void NetworkCommunicationObserveUserProgramTest();


        /// Return Type: unsigned int
        ///resource: byte
        ///instanceNumber: byte
        ///context: byte
        ///feature: char*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALReport")]
        public static extern uint HALReport(byte resource, byte instanceNumber, byte context = 0, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string feature = null);


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "NumericArrayResize")]
        public static extern void NumericArrayResize();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "RTSetCleanupProc")]
        public static extern void RTSetCleanupProc();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "EDVR_CreateReference")]
        public static extern void EDVR_CreateReference();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "Occur")]
        public static extern void Occur();
    }
}
