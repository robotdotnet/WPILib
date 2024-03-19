using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil;
using WPIUtil.Handles;
using WPIUtil.Marshal;

namespace WPIHal.Natives;

public static partial class HalDriverStation
{
    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAllianceStation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial AllianceStationID GetAllianceStation(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetControlWord")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetControlWord(out ControlWord controlWord);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetControlWord")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial HalStatus GetControlWordNative(uint* controlWord);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickAxes(int joystickNum, out JoystickAxes axes);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickAxisType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickAxisType(int joystickNum, int axis);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickButtons(int joystickNum, out JoystickButtons buttons);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickDescriptor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickDescriptor(int joystickNum, out JoystickDescriptor desc);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickIsXbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetJoystickIsXbox(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetJoystickName([MarshalUsing(typeof(WpiStringMarshaller))] out string name, int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickPOVs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetJoystickPOVs(int joystickNum, out JoystickPOVs povs);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetJoystickType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetJoystickType(int joystickNum);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetMatchInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalStatus GetMatchInfo(out MatchInfo info);

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

    [LibraryImport("wpiHal", EntryPoint = "HAL_SendError", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SendError([MarshalAs(UnmanagedType.I4)] bool isError, int errorCode, [MarshalAs(UnmanagedType.I4)] bool isLVCode, string details, string location, string callStack, [MarshalAs(UnmanagedType.I4)] bool printMsg);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SendConsoleLine", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SendConsoleLine(string line);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPrintErrorImpl")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SetPrintErrorImpl(delegate* unmanaged[Cdecl]<byte*, nuint, void> func);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetJoystickOutputs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetJoystickOutputs(int joystickNum, long outputs, int leftRumble, int rightRumble);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAllJoystickData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetAllJoystickData(Span<JoystickAxes> axes, Span<JoystickPOVs> povs, Span<JoystickButtons> buttons);

    [LibraryImport("wpiHal", EntryPoint = "HAL_RefreshDSData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool RefrehshDSData();

    [LibraryImport("wpiHal", EntryPoint = "HAL_ProvideNewDataEventHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProvideNewDataEventHandle(WpiEventHandle eventHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_RemoveNewDataEventHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RemoveNewDataEventHandle(WpiEventHandle eventHandle);
}
