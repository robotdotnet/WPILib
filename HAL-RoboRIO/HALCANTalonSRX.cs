//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    internal class HALCanTalonSRX
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_Create")]
        internal static extern IntPtr c_TalonSRX_Create(int deviceNumber, int controlPeriodMs);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_Destroy")]
        internal static extern void c_TalonSRX_Destroy(IntPtr handle);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetParam")]
        internal static extern CTR_Code c_TalonSRX_SetParam(IntPtr handle, int paramEnum, double value);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_RequestParam")]
        internal static extern CTR_Code c_TalonSRX_RequestParam(IntPtr handle, int paramEnum);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetParamResponse")]
        internal static extern CTR_Code c_TalonSRX_GetParamResponse(IntPtr handle, int paramEnum, ref double value);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetParamResponseInt32")]
        internal static extern CTR_Code c_TalonSRX_GetParamResponseInt32(IntPtr handle, int paramEnum, ref int value);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetStatusFrameRate")]
        internal static extern CTR_Code c_TalonSRX_SetStatusFrameRate(IntPtr handle, uint frameEnum, uint periodMs);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_ClearStickyFaults")]
        internal static extern CTR_Code c_TalonSRX_ClearStickyFaults(IntPtr handle);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_OverTemp")]
        internal static extern CTR_Code c_TalonSRX_GetFault_OverTemp(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_UnderVoltage")]
        internal static extern CTR_Code c_TalonSRX_GetFault_UnderVoltage(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_ForLim")]
        internal static extern CTR_Code c_TalonSRX_GetFault_ForLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_RevLim")]
        internal static extern CTR_Code c_TalonSRX_GetFault_RevLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_HardwareFailure")]
        internal static extern CTR_Code c_TalonSRX_GetFault_HardwareFailure(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_ForSoftLim")]
        internal static extern CTR_Code c_TalonSRX_GetFault_ForSoftLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_RevSoftLim")]
        internal static extern CTR_Code c_TalonSRX_GetFault_RevSoftLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_OverTemp")]
        internal static extern CTR_Code c_TalonSRX_GetStckyFault_OverTemp(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_UnderVoltage")]
        internal static extern CTR_Code c_TalonSRX_GetStckyFault_UnderVoltage(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_ForLim")]
        internal static extern CTR_Code c_TalonSRX_GetStckyFault_ForLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_RevLim")]
        internal static extern CTR_Code c_TalonSRX_GetStckyFault_RevLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_ForSoftLim")]
        internal static extern CTR_Code c_TalonSRX_GetStckyFault_ForSoftLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_RevSoftLim")]
        internal static extern CTR_Code c_TalonSRX_GetStckyFault_RevSoftLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetAppliedThrottle")]
        internal static extern CTR_Code c_TalonSRX_GetAppliedThrottle(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetCloseLoopErr")]
        internal static extern CTR_Code c_TalonSRX_GetCloseLoopErr(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFeedbackDeviceSelect")]
        internal static extern CTR_Code c_TalonSRX_GetFeedbackDeviceSelect(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetModeSelect")]
        internal static extern CTR_Code c_TalonSRX_GetModeSelect(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetLimitSwitchEn")]
        internal static extern CTR_Code c_TalonSRX_GetLimitSwitchEn(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetLimitSwitchClosedFor")]
        internal static extern CTR_Code c_TalonSRX_GetLimitSwitchClosedFor(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetLimitSwitchClosedRev")]
        internal static extern CTR_Code c_TalonSRX_GetLimitSwitchClosedRev(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetSensorPosition")]
        internal static extern CTR_Code c_TalonSRX_GetSensorPosition(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetSensorVelocity")]
        internal static extern CTR_Code c_TalonSRX_GetSensorVelocity(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetCurrent")]
        internal static extern CTR_Code c_TalonSRX_GetCurrent(IntPtr handle, ref double param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetBrakeIsEnabled")]
        internal static extern CTR_Code c_TalonSRX_GetBrakeIsEnabled(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetEncPosition")]
        internal static extern CTR_Code c_TalonSRX_GetEncPosition(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetEncVel")]
        internal static extern CTR_Code c_TalonSRX_GetEncVel(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetEncIndexRiseEvents")]
        internal static extern CTR_Code c_TalonSRX_GetEncIndexRiseEvents(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetQuadApin")]
        internal static extern CTR_Code c_TalonSRX_GetQuadApin(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetQuadBpin")]
        internal static extern CTR_Code c_TalonSRX_GetQuadBpin(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetQuadIdxpin")]
        internal static extern CTR_Code c_TalonSRX_GetQuadIdxpin(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetAnalogInWithOv")]
        internal static extern CTR_Code c_TalonSRX_GetAnalogInWithOv(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetAnalogInVel")]
        internal static extern CTR_Code c_TalonSRX_GetAnalogInVel(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetTemp")]
        internal static extern CTR_Code c_TalonSRX_GetTemp(IntPtr handle, ref double param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetBatteryV")]
        internal static extern CTR_Code c_TalonSRX_GetBatteryV(IntPtr handle, ref double param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetResetCount")]
        internal static extern CTR_Code c_TalonSRX_GetResetCount(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetResetFlags")]
        internal static extern CTR_Code c_TalonSRX_GetResetFlags(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFirmVers")]
        internal static extern CTR_Code c_TalonSRX_GetFirmVers(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetDemand")]
        internal static extern CTR_Code c_TalonSRX_SetDemand(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetOverrideLimitSwitchEn")]
        internal static extern CTR_Code c_TalonSRX_SetOverrideLimitSwitchEn(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetFeedbackDeviceSelect")]
        internal static extern CTR_Code c_TalonSRX_SetFeedbackDeviceSelect(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetRevMotDuringCloseLoopEn")]
        internal static extern CTR_Code c_TalonSRX_SetRevMotDuringCloseLoopEn(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetOverrideBrakeType")]
        internal static extern CTR_Code c_TalonSRX_SetOverrideBrakeType(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetModeSelect")]
        internal static extern CTR_Code c_TalonSRX_SetModeSelect(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetModeSelect2")]
        internal static extern CTR_Code c_TalonSRX_SetModeSelect2(IntPtr handle, int modeSelect, int demand);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetProfileSlotSelect")]
        internal static extern CTR_Code c_TalonSRX_SetProfileSlotSelect(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetRampThrottle")]
        internal static extern CTR_Code c_TalonSRX_SetRampThrottle(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetRevFeedbackSensor")]
        internal static extern CTR_Code c_TalonSRX_SetRevFeedbackSensor(IntPtr handle, int param);
    }
}
