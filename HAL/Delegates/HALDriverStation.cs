using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALDriverStation
    {
        static HALDriverStation()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALDriverStation>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        /*
        public delegate int HAL_SetErrorDataDelegate([HALAllowNonBlittable]string errors, int wait_ms);
        [NativeDelegate] public static HAL_SetErrorDataDelegate HAL_SetErrorData;
        */
        /*
        public delegate int HAL_SendErrorDelegate([MarshalAs(UnmanagedType.I4)]bool isError, int errorCode, [MarshalAs(UnmanagedType.I4)]bool isLVCode, [HALAllowNonBlittable]string details, [HALAllowNonBlittable]string location, [HALAllowNonBlittable]string callStack, [MarshalAs(UnmanagedType.I4)]bool printMsg);
        [NativeDelegate] public static HAL_SendErrorDelegate HAL_SendError;
        */
        /*
        public delegate int HAL_GetControlWordDelegate(ref HALControlWord controlWord);
        [NativeDelegate] public static HAL_GetControlWordDelegate HAL_GetControlWord;
        */

        public delegate HALAllianceStationID HAL_GetAllianceStationDelegate(ref int status);
        [NativeDelegate] public static HAL_GetAllianceStationDelegate HAL_GetAllianceStation;

        public delegate int HAL_GetJoystickAxesDelegate(int joystickNum, ref HALJoystickAxes axes);
        [NativeDelegate] public static HAL_GetJoystickAxesDelegate HAL_GetJoystickAxes;

        public delegate int HAL_GetJoystickPOVsDelegate(int joystickNum, ref HALJoystickPOVs povs);
        [NativeDelegate] public static HAL_GetJoystickPOVsDelegate HAL_GetJoystickPOVs;

        public delegate int HAL_GetJoystickButtonsDelegate(int joystickNum, ref HALJoystickButtons buttons);
        [NativeDelegate] public static HAL_GetJoystickButtonsDelegate HAL_GetJoystickButtons;

        public delegate int HAL_GetJoystickDescriptorDelegate(int joystickNum, ref HALJoystickDescriptor desc);
        [NativeDelegate] public static HAL_GetJoystickDescriptorDelegate HAL_GetJoystickDescriptor;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetJoystickIsXboxDelegate(int joystickNum);
        [NativeDelegate] public static HAL_GetJoystickIsXboxDelegate HAL_GetJoystickIsXbox;

        public delegate int HAL_GetJoystickTypeDelegate(int joystickNum);
        [NativeDelegate] public static HAL_GetJoystickTypeDelegate HAL_GetJoystickType;

        [HALAllowNonBlittable]
        public delegate string HAL_GetJoystickNameDelegate(int joystickNum);
        [NativeDelegate] public static HAL_GetJoystickNameDelegate HAL_GetJoystickName;

        public delegate int HAL_GetJoystickAxisTypeDelegate(int joystickNum, int axis);
        [NativeDelegate] public static HAL_GetJoystickAxisTypeDelegate HAL_GetJoystickAxisType;

        public delegate int HAL_SetJoystickOutputsDelegate(int joystickNum, long outputs, int leftRumble, int rightRumble);
        [NativeDelegate] public static HAL_SetJoystickOutputsDelegate HAL_SetJoystickOutputs;

        public delegate double HAL_GetMatchTimeDelegate(ref int status);
        [NativeDelegate] public static HAL_GetMatchTimeDelegate HAL_GetMatchTime;

        public delegate void HAL_WaitForDSDataDelegate();
        [NativeDelegate] public static HAL_WaitForDSDataDelegate HAL_WaitForDSData;

        public delegate void HAL_InitializeDriverStationDelegate();
        [NativeDelegate] public static HAL_InitializeDriverStationDelegate HAL_InitializeDriverStation;

        public delegate void HAL_ObserveUserProgramStartingDelegate();
        [NativeDelegate] public static HAL_ObserveUserProgramStartingDelegate HAL_ObserveUserProgramStarting;

        public delegate void HAL_ObserveUserProgramDisabledDelegate();
        [NativeDelegate] public static HAL_ObserveUserProgramDisabledDelegate HAL_ObserveUserProgramDisabled;

        public delegate void HAL_ObserveUserProgramAutonomousDelegate();
        [NativeDelegate] public static HAL_ObserveUserProgramAutonomousDelegate HAL_ObserveUserProgramAutonomous;

        public delegate void HAL_ObserveUserProgramTeleopDelegate();
        [NativeDelegate] public static HAL_ObserveUserProgramTeleopDelegate HAL_ObserveUserProgramTeleop;

        public delegate void HAL_ObserveUserProgramTestDelegate();
        [NativeDelegate] public static HAL_ObserveUserProgramTestDelegate HAL_ObserveUserProgramTest;



        private delegate int NativeHALSetErrorDataDelegate(byte[] errors, int errorsLength, int waitMs);
        [NativeDelegate("HAL_SetErrorData")]
        private static NativeHALSetErrorDataDelegate NativeHALSetErrorData;

        public static int HAL_SetErrorData(string errors, int waitMs)
        {
            int len;
            byte[] errorB = HAL.CreateUTF8String(errors, out len);
            return NativeHALSetErrorData(errorB, len, waitMs);
        }

        private delegate int NativeHALSendErrorDelegate(int isError, int errorCode, int isLVCode,
            byte[] details, byte[] location, byte[] callStack, int printMsg);
        [NativeDelegate("HAL_SendError")]
        private static NativeHALSendErrorDelegate NativeHALSendError;

        public static int HAL_SendError(bool isError, int errorCode, bool isLVCode, string details,
            string location, string callStack, bool printMsg)
        {
            int len;
            byte[] loc = HAL.CreateUTF8String(location, out len);
            byte[] det = HAL.CreateUTF8String(details, out len);
            byte[] stack = HAL.CreateUTF8String(callStack, out len);
            return NativeHALSendError(isError ? 1 : 0, errorCode, isLVCode ? 1 : 0, det, loc, stack, printMsg ? 1 : 0);
        }

        private delegate int NativeHALGetControlWordDelegate(ref uint data);

        [NativeDelegate("HAL_GetControlWord")]
        private static NativeHALGetControlWordDelegate NativeHALGetControlWord;

        /// <summary>
        /// Gets the HAL Control Word
        /// </summary>
        /// <returns></returns>
        public static int HAL_GetControlWord(ref HALControlWord controlWord)
        {
            uint word = 0;
            int ret = NativeHALGetControlWord(ref word);
            controlWord = new HALControlWord((word & 1) != 0, ((word >> 1) & 1) != 0, ((word >> 2) & 1) != 0,
                ((word >> 3) & 1) != 0, ((word >> 4) & 1) != 0, ((word >> 5) & 1) != 0);
            return ret;
        }
    }
}

