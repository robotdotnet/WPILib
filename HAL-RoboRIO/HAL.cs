//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    internal class HAL
    {
        internal const string LibhalathenaSharedSo = "libHALAthena_shared.so";

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getPort")]
        internal static extern IntPtr getPort(byte pin);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getPortWithModule")]
        internal static extern IntPtr getPortWithModule(byte module, byte pin);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getHALErrorMessage")]
        internal static extern IntPtr getHALErrorMessage(int code);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getFPGAVersion")]
        internal static extern ushort getFPGAVersion(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getFPGARevision")]
        internal static extern uint getFPGARevision(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getFPGATime")]
        internal static extern uint getFPGATime(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getFPGAButton")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getFPGAButton(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALSetErrorData")]
        internal static extern int HALSetErrorData(string errors, int errorsLength, int wait_ms);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetControlWord")]
        private static extern int NativeHALGetControlWord(ref uint data);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetAllianceStation")]
        internal static extern int HALGetAllianceStation(ref HALAllianceStationID allianceStation);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickAxes")]
        internal static extern int HALGetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickPOVs")]
        internal static extern int HALGetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickButtons")]
        internal static extern int HALGetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickDescriptor")]
        internal static extern int HALGetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickIsXbox")]
        internal static extern int HALGetJoystickIsXbox(byte joystickNum);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickType")]
        internal static extern int HALGetJoystickType(byte joystickNum);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickName")]
        internal static extern IntPtr HALGetJoystickName(byte joystickNum);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickAxisType")]
        internal static extern int HALGetJoystickAxisType(byte joystickNum, byte axis);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALSetJoystickOutputs")]
        internal static extern int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetMatchTime")]
        internal static extern int HALGetMatchTime(ref float matchTime);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALSetNewDataSem")]
        internal static extern void HALSetNewDataSem(IntPtr sem);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetSystemActive")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool HALGetSystemActive(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetBrownedOut")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool HALGetBrownedOut(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALInitialize")]
        internal static extern int HALInitialize(int mode);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramStarting")]
        internal static extern void HALNetworkCommunicationObserveUserProgramStarting();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramDisabled")]
        internal static extern void HALNetworkCommunicationObserveUserProgramDisabled();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramAutonomous")]
        internal static extern void HALNetworkCommunicationObserveUserProgramAutonomous();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramTeleop")]
        internal static extern void HALNetworkCommunicationObserveUserProgramTeleop();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramTest")]
        internal static extern void HALNetworkCommunicationObserveUserProgramTest();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALReport")]
        internal static extern uint HALReport(byte resource, byte instanceNumber, byte context, string feature = null);

        
        /// <summary>
        /// Gets the HAL Control Word
        /// </summary>
        /// <returns></returns>
        internal static HALControlWord HALGetControlWord()
        {
            //HALControlWord temp = new HALControlWord();
            uint word = 0;
            NativeHALGetControlWord(ref word);
            return new HALControlWord((word & 1) != 0, ((word >> 1) & 1) != 0, ((word >> 2) & 1) != 0,
                ((word >> 3) & 1) != 0, ((word >> 4) & 1) != 0, ((word >> 5) & 1) != 0);
        }
    }
}
