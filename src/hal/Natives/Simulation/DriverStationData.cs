using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil;
using unsafe HAL_JoystickAxesCallback = delegate* unmanaged[Cdecl]<byte*, void*, int, WPIHal.JoystickAxes*, void>;
using unsafe HAL_JoystickButtonsCallback = delegate* unmanaged[Cdecl]<byte*, void*, int, WPIHal.JoystickButtons*, void>;
using unsafe HAL_JoystickDescriptorCallback = delegate* unmanaged[Cdecl]<byte*, void*, int, WPIHal.JoystickDescriptorMarshaller.NativeJoystickDescriptor*, void>;
using unsafe HAL_JoystickOutputsCallback = delegate* unmanaged[Cdecl]<byte*, void*, int, long, int, int, void>;
using unsafe HAL_JoystickPOVsCallback = delegate* unmanaged[Cdecl]<byte*, void*, int, WPIHal.JoystickPOVs*, void>;
using unsafe HAL_MatchInfoCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.MatchInfoMarshaller.NativeMatchInfo*, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalDriverStationData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetDriverStationData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetDriverStationData();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationEnabledCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationEnabledCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationEnabledCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationEnabledCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDriverStationEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDriverStationEnabled();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDriverStationEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDriverStationEnabled([MarshalAs(UnmanagedType.I4)] bool enabled);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationAutonomousCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationAutonomousCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationAutonomousCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationAutonomousCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDriverStationAutonomous")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDriverStationAutonomous();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDriverStationAutonomous")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDriverStationAutonomous([MarshalAs(UnmanagedType.I4)] bool autonomous);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationTestCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationTestCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationTestCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationTestCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDriverStationTest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDriverStationTest();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDriverStationTest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDriverStationTest([MarshalAs(UnmanagedType.I4)] bool test);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationEStopCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationEStopCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationEStopCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationEStopCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDriverStationEStop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDriverStationEStop();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDriverStationEStop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDriverStationEStop([MarshalAs(UnmanagedType.I4)] bool eStop);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationFmsAttachedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationFmsAttachedCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationFmsAttachedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationFmsAttachedCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDriverStationFmsAttached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDriverStationFmsAttached();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDriverStationFmsAttached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDriverStationFmsAttached([MarshalAs(UnmanagedType.I4)] bool fmsAttached);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationDsAttachedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationDsAttachedCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationDsAttachedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationDsAttachedCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDriverStationDsAttached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDriverStationDsAttached();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDriverStationDsAttached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDriverStationDsAttached([MarshalAs(UnmanagedType.I4)] bool dsAttached);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationAllianceStationIdCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationAllianceStationIdCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationAllianceStationIdCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationAllianceStationIdCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDriverStationAllianceStationId")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.AllianceStationID GetDriverStationAllianceStationId();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDriverStationAllianceStationId")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDriverStationAllianceStationId(WPIHal.AllianceStationID allianceStationId);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationMatchTimeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationMatchTimeCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationMatchTimeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationMatchTimeCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDriverStationMatchTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetDriverStationMatchTime();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDriverStationMatchTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDriverStationMatchTime(double matchTime);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterJoystickAxesCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterJoystickAxesCallback(int joystickNum, HAL_JoystickAxesCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelJoystickAxesCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelJoystickAxesCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetJoystickAxes(int joystickNum, Span<JoystickAxes> axes);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickAxes(int joystickNum, ReadOnlySpan<JoystickAxes> axes);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterJoystickPOVsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterJoystickPOVsCallback(int joystickNum, HAL_JoystickPOVsCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelJoystickPOVsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelJoystickPOVsCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetJoystickPOVs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetJoystickPOVs(int joystickNum, Span<JoystickPOVs> povs);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickPOVs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickPOVs(int joystickNum, ReadOnlySpan<JoystickPOVs> povs);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterJoystickButtonsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterJoystickButtonsCallback(int joystickNum, HAL_JoystickButtonsCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelJoystickButtonsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelJoystickButtonsCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetJoystickButtons(int joystickNum, Span<JoystickButtons> buttons);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickButtons(int joystickNum, ReadOnlySpan<JoystickButtons> buttons);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterJoystickDescriptorCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterJoystickDescriptorCallback(int joystickNum, HAL_JoystickDescriptorCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelJoystickDescriptorCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelJoystickDescriptorCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetJoystickDescriptor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetJoystickDescriptor(int joystickNum, out JoystickDescriptor descriptor);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickDescriptor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickDescriptor(int joystickNum, in JoystickDescriptor descriptor);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterJoystickOutputsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterJoystickOutputsCallback(int joystickNum, HAL_JoystickOutputsCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelJoystickOutputsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelJoystickOutputsCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetJoystickOutputs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetJoystickOutputs(int joystickNum, long* outputs, int* leftRumble, int* rightRumble);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickOutputs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickOutputs(int joystickNum, long outputs, int leftRumble, int rightRumble);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterMatchInfoCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterMatchInfoCallback(HAL_MatchInfoCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelMatchInfoCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelMatchInfoCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetMatchInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMatchInfo(out MatchInfo info);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetMatchInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetMatchInfo(in MatchInfo info);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickButton(int stick, int button, [MarshalAs(UnmanagedType.I4)] bool state);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickAxis(int stick, int axis, double value);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickPOV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickPOV(int stick, int pov, int value);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickButtonsValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickButtonsValue(int stick, uint buttons);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickAxisCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickAxisCount(int stick, int count);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickPOVCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickPOVCount(int stick, int count);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickButtonCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickButtonCount(int stick, int count);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetJoystickCounts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetJoystickCounts(int stick, int* axisCount, int* buttonCount, int* povCount);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickIsXbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickIsXbox(int stick, [MarshalAs(UnmanagedType.I4)] bool isXbox);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickType(int stick, int type);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickName(int stick, WpiString name);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetJoystickAxisType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickAxisType(int stick, int axis, int type);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetGameSpecificMessage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGameSpecificMessage(WpiString message);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEventName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEventName(WpiString name);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetMatchType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetMatchType(MatchType type);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetMatchNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetMatchNumber(int matchNumber);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetReplayNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetReplayNumber(int replayNumber);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterDriverStationAllCallbacks(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDriverStationNewDataCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDriverStationNewDataCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDriverStationNewDataCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDriverStationNewDataCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_NotifyDriverStationNewData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void NotifyDriverStationNewData();

}
