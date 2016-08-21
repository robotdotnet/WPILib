using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
    internal class HALDriverStation
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {

            Base.HALDriverStation.HAL_SetErrorData = HALSetErrorData;

            NativeHALSetErrorData = (NativeHALSetErrorDataDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetErrorData"), typeof(NativeHALSetErrorDataDelegate));

            Base.HALDriverStation.HAL_SendError = HALSendError;

            NativeHALSendError = (NativeHALSendErrorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ndError"), typeof(NativeHALSendErrorDelegate));


            Base.HALDriverStation.HAL_GetControlWord = HALGetControlWord;
            NativeHALGetControlWord = (NativeHALGetControlWordDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetControlWord"), typeof(NativeHALGetControlWordDelegate));

            Base.HALDriverStation.HAL_GetAllianceStation = (Base.HALDriverStation.HAL_GetAllianceStationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAllianceStation"), typeof(Base.HALDriverStation.HAL_GetAllianceStationDelegate));

            Base.HALDriverStation.HAL_GetJoystickAxes = (Base.HALDriverStation.HAL_GetJoystickAxesDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetJoystickAxes"), typeof(Base.HALDriverStation.HAL_GetJoystickAxesDelegate));

            Base.HALDriverStation.HAL_GetJoystickPOVs = (Base.HALDriverStation.HAL_GetJoystickPOVsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetJoystickPOVs"), typeof(Base.HALDriverStation.HAL_GetJoystickPOVsDelegate));

            Base.HALDriverStation.HAL_GetJoystickButtons = (Base.HALDriverStation.HAL_GetJoystickButtonsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetJoystickButtons"), typeof(Base.HALDriverStation.HAL_GetJoystickButtonsDelegate));

            Base.HALDriverStation.HAL_GetJoystickDescriptor = (Base.HALDriverStation.HAL_GetJoystickDescriptorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetJoystickDescriptor"), typeof(Base.HALDriverStation.HAL_GetJoystickDescriptorDelegate));

            Base.HALDriverStation.HAL_GetJoystickIsXbox = (Base.HALDriverStation.HAL_GetJoystickIsXboxDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetJoystickIsXbox"), typeof(Base.HALDriverStation.HAL_GetJoystickIsXboxDelegate));

            Base.HALDriverStation.HAL_GetJoystickType = (Base.HALDriverStation.HAL_GetJoystickTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetJoystickType"), typeof(Base.HALDriverStation.HAL_GetJoystickTypeDelegate));

            Base.HALDriverStation.HAL_GetJoystickName = (Base.HALDriverStation.HAL_GetJoystickNameDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetJoystickName"), typeof(Base.HALDriverStation.HAL_GetJoystickNameDelegate));

            Base.HALDriverStation.HAL_GetJoystickAxisType = (Base.HALDriverStation.HAL_GetJoystickAxisTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetJoystickAxisType"), typeof(Base.HALDriverStation.HAL_GetJoystickAxisTypeDelegate));

            Base.HALDriverStation.HAL_SetJoystickOutputs = (Base.HALDriverStation.HAL_SetJoystickOutputsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetJoystickOutputs"), typeof(Base.HALDriverStation.HAL_SetJoystickOutputsDelegate));

            Base.HALDriverStation.HAL_GetMatchTime = (Base.HALDriverStation.HAL_GetMatchTimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetMatchTime"), typeof(Base.HALDriverStation.HAL_GetMatchTimeDelegate));

            Base.HALDriverStation.HAL_WaitForDSData = (Base.HALDriverStation.HAL_WaitForDSDataDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_WaitForDSData"), typeof(Base.HALDriverStation.HAL_WaitForDSDataDelegate));

            Base.HALDriverStation.HAL_InitializeDriverStation = (Base.HALDriverStation.HAL_InitializeDriverStationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeDriverStation"), typeof(Base.HALDriverStation.HAL_InitializeDriverStationDelegate));

            Base.HALDriverStation.HAL_ObserveUserProgramStarting = (Base.HALDriverStation.HAL_ObserveUserProgramStartingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ObserveUserProgramStarting"), typeof(Base.HALDriverStation.HAL_ObserveUserProgramStartingDelegate));

            Base.HALDriverStation.HAL_ObserveUserProgramDisabled = (Base.HALDriverStation.HAL_ObserveUserProgramDisabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ObserveUserProgramDisabled"), typeof(Base.HALDriverStation.HAL_ObserveUserProgramDisabledDelegate));

            Base.HALDriverStation.HAL_ObserveUserProgramAutonomous = (Base.HALDriverStation.HAL_ObserveUserProgramAutonomousDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ObserveUserProgramAutonomous"), typeof(Base.HALDriverStation.HAL_ObserveUserProgramAutonomousDelegate));

            Base.HALDriverStation.HAL_ObserveUserProgramTeleop = (Base.HALDriverStation.HAL_ObserveUserProgramTeleopDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ObserveUserProgramTeleop"), typeof(Base.HALDriverStation.HAL_ObserveUserProgramTeleopDelegate));

            Base.HALDriverStation.HAL_ObserveUserProgramTest = (Base.HALDriverStation.HAL_ObserveUserProgramTestDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ObserveUserProgramTest"), typeof(Base.HALDriverStation.HAL_ObserveUserProgramTestDelegate));
        }

        private delegate int NativeHALSetErrorDataDelegate(byte[] errors, int errorsLength, int waitMs);

        private static NativeHALSetErrorDataDelegate NativeHALSetErrorData;

        public static int HALSetErrorData(string errors, int waitMs)
        {
            int len;
            byte[] errorB = HAL.CreateUTF8String(errors, out len);
            return NativeHALSetErrorData(errorB, len, waitMs);
        }

        private delegate int NativeHALSendErrorDelegate(int isError, int errorCode, int isLVCode,
            byte[] details, byte[] location, byte[] callStack, int printMsg);

        private static NativeHALSendErrorDelegate NativeHALSendError;

        public static int HALSendError(bool isError, int errorCode, bool isLVCode, string details,
            string location, string callStack, bool printMsg)
        {
            int len;
            byte[] loc = HAL.CreateUTF8String(location, out len);
            byte[] det = HAL.CreateUTF8String(details, out len);
            byte[] stack = HAL.CreateUTF8String(callStack, out len);
            return NativeHALSendError(isError ? 1 : 0, errorCode, isLVCode ? 1 : 0, det, loc, stack, printMsg ? 1 : 0);
        }

        private delegate int NativeHALGetControlWordDelegate(ref uint data);

        private static NativeHALGetControlWordDelegate NativeHALGetControlWord;

        /// <summary>
        /// Gets the HAL Control Word
        /// </summary>
        /// <returns></returns>
        public static int HALGetControlWord(ref HALControlWord controlWord)
        {
            //HALControlWord temp = new HALControlWord();
            uint word = 0;
            int ret = NativeHALGetControlWord(ref word);
            controlWord = new HALControlWord((word & 1) != 0, ((word >> 1) & 1) != 0, ((word >> 2) & 1) != 0,
                ((word >> 3) & 1) != 0, ((word >> 4) & 1) != 0, ((word >> 5) & 1) != 0);
            return ret;
        }
    }
}

