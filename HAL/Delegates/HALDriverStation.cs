using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALDriverStation
    {
        static HALDriverStation()
        {
            HAL.Initialize();
        }

        public delegate int HAL_SetErrorDataDelegate([HALAllowNonBlittable]string errors, int wait_ms);
        public static HAL_SetErrorDataDelegate HAL_SetErrorData;

        public delegate int HAL_SendErrorDelegate([MarshalAs(UnmanagedType.I4)]bool isError, int errorCode, [MarshalAs(UnmanagedType.I4)]bool isLVCode, [HALAllowNonBlittable]string details, [HALAllowNonBlittable]string location, [HALAllowNonBlittable]string callStack, [MarshalAs(UnmanagedType.I4)]bool printMsg);
        public static HAL_SendErrorDelegate HAL_SendError;

        public delegate int HAL_GetControlWordDelegate(ref HALControlWord controlWord);
        public static HAL_GetControlWordDelegate HAL_GetControlWord;

        public delegate HALAllianceStationID HAL_GetAllianceStationDelegate(ref int status);
        public static HAL_GetAllianceStationDelegate HAL_GetAllianceStation;

        public delegate int HAL_GetJoystickAxesDelegate(int joystickNum, ref HALJoystickAxes axes);
        public static HAL_GetJoystickAxesDelegate HAL_GetJoystickAxes;

        public delegate int HAL_GetJoystickPOVsDelegate(int joystickNum, ref HALJoystickPOVs povs);
        public static HAL_GetJoystickPOVsDelegate HAL_GetJoystickPOVs;

        public delegate int HAL_GetJoystickButtonsDelegate(int joystickNum, ref HALJoystickButtons buttons);
        public static HAL_GetJoystickButtonsDelegate HAL_GetJoystickButtons;

        public delegate int HAL_GetJoystickDescriptorDelegate(int joystickNum, ref HALJoystickDescriptor desc);
        public static HAL_GetJoystickDescriptorDelegate HAL_GetJoystickDescriptor;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetJoystickIsXboxDelegate(int joystickNum);
        public static HAL_GetJoystickIsXboxDelegate HAL_GetJoystickIsXbox;

        public delegate int HAL_GetJoystickTypeDelegate(int joystickNum);
        public static HAL_GetJoystickTypeDelegate HAL_GetJoystickType;

        [HALAllowNonBlittable]
        public delegate string HAL_GetJoystickNameDelegate(int joystickNum);
        public static HAL_GetJoystickNameDelegate HAL_GetJoystickName;

        public delegate int HAL_GetJoystickAxisTypeDelegate(int joystickNum, int axis);
        public static HAL_GetJoystickAxisTypeDelegate HAL_GetJoystickAxisType;

        public delegate int HAL_SetJoystickOutputsDelegate(int joystickNum, long outputs, int leftRumble, int rightRumble);
        public static HAL_SetJoystickOutputsDelegate HAL_SetJoystickOutputs;

        public delegate double HAL_GetMatchTimeDelegate(ref int status);
        public static HAL_GetMatchTimeDelegate HAL_GetMatchTime;

        public delegate void HAL_WaitForDSDataDelegate();
        public static HAL_WaitForDSDataDelegate HAL_WaitForDSData;

        public delegate void HAL_InitializeDriverStationDelegate();
        public static HAL_InitializeDriverStationDelegate HAL_InitializeDriverStation;

        public delegate void HAL_ObserveUserProgramStartingDelegate();
        public static HAL_ObserveUserProgramStartingDelegate HAL_ObserveUserProgramStarting;

        public delegate void HAL_ObserveUserProgramDisabledDelegate();
        public static HAL_ObserveUserProgramDisabledDelegate HAL_ObserveUserProgramDisabled;

        public delegate void HAL_ObserveUserProgramAutonomousDelegate();
        public static HAL_ObserveUserProgramAutonomousDelegate HAL_ObserveUserProgramAutonomous;

        public delegate void HAL_ObserveUserProgramTeleopDelegate();
        public static HAL_ObserveUserProgramTeleopDelegate HAL_ObserveUserProgramTeleop;

        public delegate void HAL_ObserveUserProgramTestDelegate();
        public static HAL_ObserveUserProgramTestDelegate HAL_ObserveUserProgramTest;
    }
}

