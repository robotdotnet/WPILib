using Hal.Natives;
using System;
using System.Text;
using WPIUtil;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IDriverStation))]
    public unsafe static class DriverStation
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IDriverStation driverStation;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static bool SendError(bool isError, int errorCode, bool isLVCode, ReadOnlySpan<char> details, ReadOnlySpan<char> location, ReadOnlySpan<char> callStack, bool printMsg)
        {
            UTF8String locationStr = new UTF8String(location);
            UTF8String detailsStr = new UTF8String(details);
            UTF8String callStackStr = new UTF8String(callStack);

            fixed (byte* loc = locationStr.Buffer)
            fixed (byte* det = detailsStr.Buffer)
            fixed (byte* cs = callStackStr.Buffer)
            {
                return driverStation.HAL_SendError(isError ? 1 : 0, errorCode, isLVCode ? 1 : 0, det, loc, cs, printMsg ? 1 : 0) != 0;
            }
        }

        public static ControlWord GetControlWord()
        {
            ControlWord cw;
            driverStation.HAL_GetControlWord(&cw);
            return cw;
        }

        public static AllianceStationID GetAllianceStation()
        {
            int status = 0;
            return driverStation.HAL_GetAllianceStation(&status);
        }

        public static JoystickAxes GetJoystickAxes(int joystickNum)
        {
            JoystickAxes js;
            driverStation.HAL_GetJoystickAxes(joystickNum, &js);
            return js;
        }

        public static JoystickPOVs GetJoystickPOVs(int joystickNum)
        {
            JoystickPOVs povs;
            driverStation.HAL_GetJoystickPOVs(joystickNum, &povs);
            return povs;
        }

        public static JoystickButtons GetJoystickButtons(int joystickNum)
        {
            JoystickButtons buttons;
            driverStation.HAL_GetJoystickButtons(joystickNum, &buttons);
            return buttons;
        }

        public static JoystickDescriptor GetJoystickDescriptor(int joystickNum)
        {
            JoystickDescriptor descriptor;
            driverStation.HAL_GetJoystickDescriptor(joystickNum, &descriptor);
            return descriptor;
        }

        public static bool GetJoystickIsXbox(int joystickNum)
        {
            return driverStation.HAL_GetJoystickIsXbox(joystickNum) != 0;
        }

        public static int GetJoystickType(int joystickNum)
        {
            return driverStation.HAL_GetJoystickType(joystickNum);
        }

        public static string GetJoystickName(int joystickNum)
        {
            byte* name = driverStation.HAL_GetJoystickName(joystickNum);
            string strName = UTF8String.ReadUTF8String(name);
            driverStation.HAL_FreeJoystickName(name);
            return strName;
        }

        public static int GetJoystickAxisType(int joystickNum, int axis)
        {
            return driverStation.HAL_GetJoystickAxisType(joystickNum, axis);
        }

        public static void SetJoystickOutputs(int joystickNum, long outputs, int leftRumble, int rightRumble)
        {
            driverStation.HAL_SetJoystickOutputs(joystickNum, outputs, leftRumble, rightRumble);
        }

        public static double GetMatchTime()
        {
            int status = 0;
            return driverStation.HAL_GetMatchTime(&status);
        }

        public static MatchInfo GetMatchInfo()
        {
            MatchInfo info;
            driverStation.HAL_GetMatchInfo(&info);
            return info;
        }

        public static void ReleaseDSMutex()
        {
            driverStation.HAL_ReleaseDSMutex();
        }

        public static void WaitForCachedControlData()
        {
            driverStation.HAL_WaitForCachedControlData();
        }

        public static void WaitForCachedControlData(double timeout)
        {
            driverStation.HAL_WaitForCachedControlDataTimeout(timeout);
        }

        public static bool IsNewControlData()
        {
            return driverStation.HAL_IsNewControlData() != 0;
        }

        public static void WaitForDSData()
        {
            driverStation.HAL_WaitForDSData();
        }

        public static void WaitForDSData(double timeout)
        {
            driverStation.HAL_WaitForDSDataTimeout(timeout);
        }

        public static void InitializeDriverStation()
        {
            driverStation.HAL_InitializeDriverStation();
        }

        public static void ObserveUserProgramStarting()
        {
            driverStation.HAL_ObserveUserProgramStarting();
        }

        public static void ObserveUserProgramDisabled()
        {
            driverStation.HAL_ObserveUserProgramDisabled();
        }

        public static void ObserveUserProgramAutonomous()
        {
            driverStation.HAL_ObserveUserProgramAutonomous();
        }

        public static void ObserveUserProgramTeleop()
        {
            driverStation.HAL_ObserveUserProgramTeleop();
        }

        public static void ObserveUserProgramTest()
        {
            driverStation.HAL_ObserveUserProgramTest();
        }
    }
}