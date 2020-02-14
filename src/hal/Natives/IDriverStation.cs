using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IDriverStation
    {
        int HAL_SendError(int isError, int errorCode, int isLVCode, byte* details, byte* location, byte* callStack, int printMsg);

        void HAL_GetControlWord(ControlWord* controlWord);

        AllianceStationID HAL_GetAllianceStation(int* status);

        int HAL_GetJoystickAxes(int joystickNum, JoystickAxes* axes);

        int HAL_GetJoystickPOVs(int joystickNum, JoystickPOVs* povs);

        int HAL_GetJoystickButtons(int joystickNum, JoystickButtons* buttons);

        int HAL_GetJoystickDescriptor(int joystickNum, JoystickDescriptor* desc);

        int HAL_GetJoystickIsXbox(int joystickNum);

        int HAL_GetJoystickType(int joystickNum);

        byte* HAL_GetJoystickName(int joystickNum);

        void HAL_FreeJoystickName(byte* name);

        int HAL_GetJoystickAxisType(int joystickNum, int axis);

        int HAL_SetJoystickOutputs(int joystickNum, long outputs,
                                       int leftRumble, int rightRumble);

        double HAL_GetMatchTime(int* status);

        int HAL_GetMatchInfo(MatchInfo* info);

        void HAL_ReleaseDSMutex();

        void HAL_WaitForCachedControlData();

        int HAL_WaitForCachedControlDataTimeout(double timeout);

        int HAL_IsNewControlData();

        void HAL_WaitForDSData();

        int HAL_WaitForDSDataTimeout(double timeout);

        void HAL_InitializeDriverStation();

        void HAL_ObserveUserProgramStarting();

        void HAL_ObserveUserProgramDisabled();

        void HAL_ObserveUserProgramAutonomous();

        void HAL_ObserveUserProgramTeleop();

        void HAL_ObserveUserProgramTest();
    }
}
