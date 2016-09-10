using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALDriverStation
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            //Base.HALDriverStation.HAL_SetErrorData = HAL_SetErrorData;
            //Base.HALDriverStation.HAL_SendError = HAL_SendError;
            //Base.HALDriverStation.HAL_GetControlWord = HAL_GetControlWord;
            Base.HALDriverStation.HAL_GetAllianceStation = HAL_GetAllianceStation;
            Base.HALDriverStation.HAL_GetJoystickAxes = HAL_GetJoystickAxes;
            Base.HALDriverStation.HAL_GetJoystickPOVs = HAL_GetJoystickPOVs;
            Base.HALDriverStation.HAL_GetJoystickButtons = HAL_GetJoystickButtons;
            Base.HALDriverStation.HAL_GetJoystickDescriptor = HAL_GetJoystickDescriptor;
            Base.HALDriverStation.HAL_GetJoystickIsXbox = HAL_GetJoystickIsXbox;
            Base.HALDriverStation.HAL_GetJoystickType = HAL_GetJoystickType;
            Base.HALDriverStation.HAL_GetJoystickName = HAL_GetJoystickName;
            Base.HALDriverStation.HAL_GetJoystickAxisType = HAL_GetJoystickAxisType;
            Base.HALDriverStation.HAL_SetJoystickOutputs = HAL_SetJoystickOutputs;
            Base.HALDriverStation.HAL_GetMatchTime = HAL_GetMatchTime;
            Base.HALDriverStation.HAL_WaitForDSData = HAL_WaitForDSData;
            Base.HALDriverStation.HAL_InitializeDriverStation = HAL_InitializeDriverStation;
            Base.HALDriverStation.HAL_ObserveUserProgramStarting = HAL_ObserveUserProgramStarting;
            Base.HALDriverStation.HAL_ObserveUserProgramDisabled = HAL_ObserveUserProgramDisabled;
            Base.HALDriverStation.HAL_ObserveUserProgramAutonomous = HAL_ObserveUserProgramAutonomous;
            Base.HALDriverStation.HAL_ObserveUserProgramTeleop = HAL_ObserveUserProgramTeleop;
            Base.HALDriverStation.HAL_ObserveUserProgramTest = HAL_ObserveUserProgramTest;
        }

        public static int HAL_SetErrorData(string errors, int wait_ms)
        {
            throw new NotImplementedException();
        }

        public static int HAL_SendError(bool isError, int errorCode, bool isLVCode, string details, string location, string callStack, bool printMsg)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetControlWord(ref HALControlWord controlWord)
        {
            throw new NotImplementedException();
        }

        public static HALAllianceStationID HAL_GetAllianceStation(ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetJoystickAxes(int joystickNum, ref HALJoystickAxes axes)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetJoystickPOVs(int joystickNum, ref HALJoystickPOVs povs)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetJoystickButtons(int joystickNum, ref HALJoystickButtons buttons)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetJoystickDescriptor(int joystickNum, ref HALJoystickDescriptor desc)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetJoystickIsXbox(int joystickNum)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetJoystickType(int joystickNum)
        {
            throw new NotImplementedException();
        }

        public static string HAL_GetJoystickName(int joystickNum)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetJoystickAxisType(int joystickNum, int axis)
        {
            throw new NotImplementedException();
        }

        public static int HAL_SetJoystickOutputs(int joystickNum, long outputs, int leftRumble, int rightRumble)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetMatchTime(ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_WaitForDSData()
        {
        }

        public static void HAL_InitializeDriverStation()
        {
        }

        public static void HAL_ObserveUserProgramStarting()
        {
        }

        public static void HAL_ObserveUserProgramDisabled()
        {
        }

        public static void HAL_ObserveUserProgramAutonomous()
        {
        }

        public static void HAL_ObserveUserProgramTeleop()
        {
        }

        public static void HAL_ObserveUserProgramTest()
        {
        }
    }
}

