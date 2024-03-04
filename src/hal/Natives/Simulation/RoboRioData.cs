using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil;
using WPIUtil.Marshal;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;
using unsafe HAL_RoboRioStringCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, nuint, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalRoboRioData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetRoboRioData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetRoboRioData();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioFPGAButtonCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioFPGAButtonCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioFPGAButtonCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioFPGAButtonCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioFPGAButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRoboRioFPGAButton();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioFPGAButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioFPGAButton([MarshalAs(UnmanagedType.I4)] bool fPGAButton);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioVInVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioVInVoltageCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioVInVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioVInVoltageCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioVInVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioVInVoltage();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioVInVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioVInVoltage(double vInVoltage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioVInCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioVInCurrentCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioVInCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioVInCurrentCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioVInCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioVInCurrent();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioVInCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioVInCurrent(double vInCurrent);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserVoltage6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserVoltage6VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserVoltage6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserVoltage6VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserVoltage6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioUserVoltage6V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserVoltage6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserVoltage6V(double userVoltage6V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserCurrent6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserCurrent6VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserCurrent6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserCurrent6VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserCurrent6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioUserCurrent6V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserCurrent6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserCurrent6V(double userCurrent6V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserActive6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserActive6VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserActive6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserActive6VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserActive6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRoboRioUserActive6V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserActive6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserActive6V([MarshalAs(UnmanagedType.I4)] bool userActive6V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserVoltage5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserVoltage5VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserVoltage5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserVoltage5VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserVoltage5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioUserVoltage5V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserVoltage5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserVoltage5V(double userVoltage5V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserCurrent5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserCurrent5VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserCurrent5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserCurrent5VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserCurrent5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioUserCurrent5V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserCurrent5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserCurrent5V(double userCurrent5V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserActive5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserActive5VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserActive5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserActive5VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserActive5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRoboRioUserActive5V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserActive5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserActive5V([MarshalAs(UnmanagedType.I4)] bool userActive5V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserVoltage3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserVoltage3V3Callback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserVoltage3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserVoltage3V3Callback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserVoltage3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioUserVoltage3V3();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserVoltage3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserVoltage3V3(double userVoltage3V3);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserCurrent3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserCurrent3V3Callback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserCurrent3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserCurrent3V3Callback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserCurrent3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioUserCurrent3V3();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserCurrent3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserCurrent3V3(double userCurrent3V3);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserActive3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserActive3V3Callback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserActive3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserActive3V3Callback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserActive3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRoboRioUserActive3V3();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserActive3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserActive3V3([MarshalAs(UnmanagedType.I4)] bool userActive3V3);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserFaults6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserFaults6VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserFaults6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserFaults6VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserFaults6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetRoboRioUserFaults6V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserFaults6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserFaults6V(int userFaults6V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserFaults5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserFaults5VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserFaults5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserFaults5VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserFaults5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetRoboRioUserFaults5V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserFaults5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserFaults5V(int userFaults5V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserFaults3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioUserFaults3V3Callback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserFaults3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioUserFaults3V3Callback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserFaults3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetRoboRioUserFaults3V3();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserFaults3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioUserFaults3V3(int userFaults3V3);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioBrownoutVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioBrownoutVoltageCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioBrownoutVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioBrownoutVoltageCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioBrownoutVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioBrownoutVoltage();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioBrownoutVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioBrownoutVoltage(double brownoutVoltage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioTeamNumberCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioTeamNumberCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioTeamNumberCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioTeamNumberCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioTeamNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetRoboRioTeamNumber();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioTeamNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioTeamNumber(int teamNumber);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioSerialNumberCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioSerialNumberCallback(HAL_RoboRioStringCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioSerialNumberCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioSerialNumberCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioSerialNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint GetRoboRioSerialNumber([MarshalUsing(typeof(WpiStringMarshaller))] out string serialNumber);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioSerialNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioSerialNumber(WpiString serialNumber);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioCommentsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioCommentsCallback(HAL_RoboRioStringCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioCommentsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioCommentsCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioComments")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint GetRoboRioComments([MarshalUsing(typeof(WpiStringMarshaller))] out string comments);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioComments")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioComments(WpiString comments);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioCPUTempCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioCPUTempCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioCPUTempCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioCPUTempCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioCPUTemp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRoboRioCPUTemp();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioCPUTemp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioCPUTemp(double cpuTemp);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioRadioLEDStateCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRoboRioRadioLEDStateCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioRadioLEDStateCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRoboRioRadioLEDStateCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial RadioLEDState GetRoboRioRadioLEDState();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRoboRioRadioLEDState(RadioLEDState state);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterRoboRioAllCallbacks(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
