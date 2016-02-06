//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    /// <summary>
    /// This attributes are placed on strings we want to force be allowed in the impl test.
    /// </summary>
    public class HALAllowNonBlittable : Attribute { }

    public partial class HAL
    {
        static HAL()
        {
            Initialize();
        }

        public delegate HALPortSafeHandle GetPortDelegate(byte pin);
        public static GetPortDelegate GetPort;

        public delegate HALPortSafeHandle GetPortWithModuleDelegate(byte module, byte pin);
        public static GetPortWithModuleDelegate GetPortWithModule;

        public delegate void FreePortDelegate(HALPortSafeHandle port_pointer);
        public static FreePortDelegate FreePort;

        [return:HALAllowNonBlittable]
        public delegate string GetHALErrorMessageDelegate(int code);
        public static GetHALErrorMessageDelegate GetHALErrorMessage;

        public delegate ushort GetFPGAVersionDelegate(ref int status);
        public static GetFPGAVersionDelegate GetFPGAVersion;

        public delegate uint GetFPGARevisionDelegate(ref int status);
        public static GetFPGARevisionDelegate GetFPGARevision;

        public delegate ulong GetFPGATimeDelegate(ref int status);
        public static GetFPGATimeDelegate GetFPGATime;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetFPGAButtonDelegate(ref int status);
        public static GetFPGAButtonDelegate GetFPGAButton;

        public delegate int HALSetErrorDataDelegate([HALAllowNonBlittable]string errors, int wait_ms);
        public static HALSetErrorDataDelegate HALSetErrorData;

        public delegate HALControlWord HALGetControlWordDelegate();
        public static HALGetControlWordDelegate GetControlWord;

        public delegate int HALGetAllianceStationDelegate(ref HALAllianceStationID allianceStation);
        public static HALGetAllianceStationDelegate HALGetAllianceStation;

        public delegate int HALGetJoystickAxesDelegate(byte joystickNum, ref HALJoystickAxes axes);
        public static HALGetJoystickAxesDelegate HALGetJoystickAxes;

        public delegate int HALGetJoystickPOVsDelegate(byte joystickNum, ref HALJoystickPOVs povs);
        public static HALGetJoystickPOVsDelegate HALGetJoystickPOVs;

        public delegate int HALGetJoystickButtonsDelegate(byte joystickNum, ref HALJoystickButtons buttons);
        public static HALGetJoystickButtonsDelegate HALGetJoystickButtons;

        public delegate int HALGetJoystickDescriptorDelegate(byte joystickNum, ref HALJoystickDescriptor desc);
        public static HALGetJoystickDescriptorDelegate HALGetJoystickDescriptor;

        public delegate int HALGetJoystickIsXboxDelegate(byte joystickNum);
        public static HALGetJoystickIsXboxDelegate HALGetJoystickIsXbox;

        public delegate int HALGetJoystickTypeDelegate(byte joystickNum);
        public static HALGetJoystickTypeDelegate HALGetJoystickType;

        public delegate int HALGetJoystickAxisTypeDelegate(byte joystickNum, byte axis);
        public static HALGetJoystickAxisTypeDelegate HALGetJoystickAxisType;

        public delegate int HALSetJoystickOutputsDelegate(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);
        public static HALSetJoystickOutputsDelegate HALSetJoystickOutputs;

        public delegate int HALGetMatchTimeDelegate(ref float matchTime);
        public static HALGetMatchTimeDelegate HALGetMatchTime;

        public delegate void HALSetNewDataSemDelegate(MultiWaitSafeHandle sem);
        public static HALSetNewDataSemDelegate HALSetNewDataSem;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool HALGetSystemActiveDelegate(ref int status);
        public static HALGetSystemActiveDelegate HALGetSystemActive;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool HALGetBrownedOutDelegate(ref int status);
        public static HALGetBrownedOutDelegate HALGetBrownedOut;

        public delegate int HALInitializeDelegate(int mode);
        public static HALInitializeDelegate HALInitialize;

        public delegate void HALNetworkCommunicationObserveUserProgramStartingDelegate();
        public static HALNetworkCommunicationObserveUserProgramStartingDelegate HALNetworkCommunicationObserveUserProgramStarting;

        public delegate void HALNetworkCommunicationObserveUserProgramDisabledDelegate();
        public static HALNetworkCommunicationObserveUserProgramDisabledDelegate HALNetworkCommunicationObserveUserProgramDisabled;

        public delegate void HALNetworkCommunicationObserveUserProgramAutonomousDelegate();
        public static HALNetworkCommunicationObserveUserProgramAutonomousDelegate HALNetworkCommunicationObserveUserProgramAutonomous;

        public delegate void HALNetworkCommunicationObserveUserProgramTeleopDelegate();
        public static HALNetworkCommunicationObserveUserProgramTeleopDelegate HALNetworkCommunicationObserveUserProgramTeleop;

        public delegate void HALNetworkCommunicationObserveUserProgramTestDelegate();
        public static HALNetworkCommunicationObserveUserProgramTestDelegate HALNetworkCommunicationObserveUserProgramTest;

        public delegate uint HALReportDelegate(byte resource, byte instanceNumber, byte context, [HALAllowNonBlittable]string feature = null);
        public static HALReportDelegate HALReport;
    }
}
