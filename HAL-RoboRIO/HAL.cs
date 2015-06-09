//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    public class HAL
    {

        [DllImport("libHALAthena_shared.so", EntryPoint = "getPort")]
        public static extern IntPtr getPort(byte pin);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getPortWithModule")]
        public static extern IntPtr getPortWithModule(byte module, byte pin);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getHALErrorMessage")]
        public static extern IntPtr getHALErrorMessage(int code);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getFPGAVersion")]
        public static extern ushort getFPGAVersion(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getFPGARevision")]
        public static extern uint getFPGARevision(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getFPGATime")]
        public static extern uint getFPGATime(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getFPGAButton")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getFPGAButton(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALSetErrorData")]
        public static extern int HALSetErrorData(string errors, int errorsLength, int wait_ms);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetControlWord")]
        public static extern int HALGetControlWord(ref HALControlWord data);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetAllianceStation")]
        public static extern int HALGetAllianceStation(ref HALAllianceStationID allianceStation);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickAxes")]
        public static extern int HALGetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickPOVs")]
        public static extern int HALGetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickButtons")]
        public static extern int HALGetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickDescriptor")]
        public static extern int HALGetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALSetJoystickOutputs")]
        public static extern int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetMatchTime")]
        public static extern int HALGetMatchTime(ref float matchTime);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALSetNewDataSem")]
        public static extern void HALSetNewDataSem(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetSystemActive")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool HALGetSystemActive(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetBrownedOut")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool HALGetBrownedOut(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALInitialize")]
        public static extern int HALInitialize(int mode);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramStarting")]
        public static extern void HALNetworkCommunicationObserveUserProgramStarting();

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramDisabled")]
        public static extern void HALNetworkCommunicationObserveUserProgramDisabled();

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramAutonomous")]
        public static extern void HALNetworkCommunicationObserveUserProgramAutonomous();

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTeleop")]
        public static extern void HALNetworkCommunicationObserveUserProgramTeleop();

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTest")]
        public static extern void HALNetworkCommunicationObserveUserProgramTest();

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALReport")]
        public static extern uint HALReport(byte resource, byte instanceNumber, byte context, string feature = null);

        [DllImport("libHALAthena_shared.so", EntryPoint = "NumericArrayResize")]
        public static extern void NumericArrayResize();

        [DllImport("libHALAthena_shared.so", EntryPoint = "RTSetCleanupProc")]
        public static extern void RTSetCleanupProc();

        [DllImport("libHALAthena_shared.so", EntryPoint = "EDVR_CreateReference")]
        public static extern void EDVR_CreateReference();

        [DllImport("libHALAthena_shared.so", EntryPoint = "Occur")]
        public static extern void Occur();
    }
}
