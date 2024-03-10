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
    public static partial void ResetData();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioFPGAButtonCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterFPGAButtonCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioFPGAButtonCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelFPGAButtonCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioFPGAButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetFPGAButton();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioFPGAButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetFPGAButton([MarshalAs(UnmanagedType.I4)] bool fPGAButton);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioVInVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterVInVoltageCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioVInVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelVInVoltageCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioVInVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetVInVoltage();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioVInVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetVInVoltage(double vInVoltage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioVInCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterVInCurrentCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioVInCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelVInCurrentCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioVInCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetVInCurrent();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioVInCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetVInCurrent(double vInCurrent);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserVoltage6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserVoltage6VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserVoltage6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserVoltage6VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserVoltage6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserVoltage6V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserVoltage6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserVoltage6V(double userVoltage6V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserCurrent6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserCurrent6VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserCurrent6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserCurrent6VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserCurrent6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserCurrent6V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserCurrent6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserCurrent6V(double userCurrent6V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserActive6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserActive6VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserActive6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserActive6VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserActive6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetUserActive6V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserActive6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserActive6V([MarshalAs(UnmanagedType.I4)] bool userActive6V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserVoltage5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserVoltage5VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserVoltage5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserVoltage5VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserVoltage5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserVoltage5V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserVoltage5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserVoltage5V(double userVoltage5V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserCurrent5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserCurrent5VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserCurrent5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserCurrent5VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserCurrent5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserCurrent5V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserCurrent5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserCurrent5V(double userCurrent5V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserActive5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserActive5VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserActive5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserActive5VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserActive5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetUserActive5V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserActive5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserActive5V([MarshalAs(UnmanagedType.I4)] bool userActive5V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserVoltage3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserVoltage3V3Callback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserVoltage3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserVoltage3V3Callback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserVoltage3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserVoltage3V3();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserVoltage3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserVoltage3V3(double userVoltage3V3);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserCurrent3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserCurrent3V3Callback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserCurrent3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserCurrent3V3Callback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserCurrent3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserCurrent3V3();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserCurrent3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserCurrent3V3(double userCurrent3V3);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserActive3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserActive3V3Callback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserActive3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserActive3V3Callback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserActive3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetUserActive3V3();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserActive3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserActive3V3([MarshalAs(UnmanagedType.I4)] bool userActive3V3);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserFaults6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserFaults6VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserFaults6VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserFaults6VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserFaults6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserFaults6V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserFaults6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserFaults6V(int userFaults6V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserFaults5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserFaults5VCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserFaults5VCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserFaults5VCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserFaults5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserFaults5V();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserFaults5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserFaults5V(int userFaults5V);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioUserFaults3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterUserFaults3V3Callback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioUserFaults3V3Callback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelUserFaults3V3Callback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioUserFaults3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserFaults3V3();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioUserFaults3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserFaults3V3(int userFaults3V3);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioBrownoutVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterBrownoutVoltageCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioBrownoutVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelBrownoutVoltageCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioBrownoutVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetBrownoutVoltage();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioBrownoutVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetBrownoutVoltage(double brownoutVoltage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioTeamNumberCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterTeamNumberCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioTeamNumberCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelTeamNumberCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioTeamNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetTeamNumber();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioTeamNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTeamNumber(int teamNumber);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioSerialNumberCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSerialNumberCallback(HAL_RoboRioStringCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioSerialNumberCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSerialNumberCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioSerialNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint GetSerialNumber([MarshalUsing(typeof(WpiStringMarshaller))] out string serialNumber);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioSerialNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSerialNumber(WpiString serialNumber);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioCommentsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCommentsCallback(HAL_RoboRioStringCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioCommentsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCommentsCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioComments")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint GetComments([MarshalUsing(typeof(WpiStringMarshaller))] out string comments);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioComments")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetComments(WpiString comments);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioCPUTempCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCPUTempCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioCPUTempCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCPUTempCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioCPUTemp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetCPUTemp();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioCPUTemp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCPUTemp(double cpuTemp);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioRadioLEDStateCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRadioLEDStateCallback(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRoboRioRadioLEDStateCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRadioLEDStateCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRoboRioRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial RadioLEDState GetRadioLEDState();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRoboRioRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRadioLEDState(RadioLEDState state);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRoboRioAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
