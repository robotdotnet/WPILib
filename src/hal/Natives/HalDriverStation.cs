using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;
using WPIUtil.Marshal;

namespace WPIHal.Natives;

public static partial class HalDriverStation
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAllianceStation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial AllianceStationID GetAllianceStationRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetControlWord")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetControlWord(ControlWord controlWord);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickAxes(int joystickNum, out JoystickAxes axes);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickAxisType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickAxisType(int joystickNum, int axis);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickButtons(int joystickNum, out JoystickButtons buttons);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickDescriptor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickDescriptor(int joystickNum, out JoystickDescriptor desc);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickIsXbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickIsXbox(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<JoystickNameStringFree>))]
    public static partial string GetJoystickName(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeJoystickName(byte* joystickName);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickPOVs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickPOVs(int joystickNum, out JoystickPOVs povs);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickType(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetMatchInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetMatchInfo(out MatchInfo info);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetMatchTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetMatchTimeRefShim(ref HalStatus status);

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

    [LibraryImport("wpiHal", EntryPoint = "HAL_SendError", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SendError(int isError, int errorCode, int isLVCode, string details, string location, string callStack, int printMsg);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetJoystickOutputs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetJoystickOutputs(int joystickNum, long outputs, int leftRumble, int rightRumble);


}
