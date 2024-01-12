using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalDriverStation
{
    //     [LibraryImport("wpiHal", EntryPoint = "The returned array must be freed with HAL_FreeJoystickName. * * Will be null terminated. * * @param joystickNum the joystick number * @return the joystick name */ byte* HAL_GetJoystickName")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  * The returned array must be freed with FreeJoystickName. * * Will be null terminated. * * @param joystickNum the joystick number * @return the joystick name */ byte* GetJoystickName(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAllianceStation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial AllianceStationID GetAllianceStation(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetControlWord")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetControlWord(ControlWord* controlWord);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickAxes(int joystickNum, JoystickAxes* axes);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickAxisType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickAxisType(int joystickNum, int axis);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickButtons(int joystickNum, JoystickButtons* buttons);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickDescriptor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickDescriptor(int joystickNum, JoystickDescriptor* desc);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickIsXbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickIsXbox(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickName(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickPOVs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickPOVs(int joystickNum, JoystickPOVs* povs);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickType(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetMatchInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetMatchInfo(MatchInfo* info);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetMatchTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetMatchTime(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ObserveUserProgramAutonomous")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ObserveUserProgramAutonomous();

    [LibraryImport("wpiHal", EntryPoint = "HAL_ObserveUserProgramDisabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ObserveUserProgramDisabled();

    [LibraryImport("wpiHal", EntryPoint = "HAL_ObserveUserProgramStarting")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ObserveUserProgramStarting();

    [LibraryImport("wpiHal", EntryPoint = "HAL_ObserveUserProgramTeleop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ObserveUserProgramTeleop();

    [LibraryImport("wpiHal", EntryPoint = "HAL_ObserveUserProgramTest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ObserveUserProgramTest();

    [LibraryImport("wpiHal", EntryPoint = "HAL_SendError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SendError(int isError, int errorCode, int isLVCode, string details, string location, string callStack, int printMsg);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetJoystickOutputs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetJoystickOutputs(int joystickNum, long outputs, int leftRumble, int rightRumble);


}
