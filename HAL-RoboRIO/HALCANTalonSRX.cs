//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCanTalonSRX
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALCanTalonSRX.C_TalonSRX_Create = (HAL_Base.HALCanTalonSRX.C_TalonSRX_CreateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_Create"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_CreateDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_Destroy = (HAL_Base.HALCanTalonSRX.C_TalonSRX_DestroyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_Destroy"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_DestroyDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetParam = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetParam"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetParamDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_RequestParam = (HAL_Base.HALCanTalonSRX.C_TalonSRX_RequestParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_RequestParam"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_RequestParamDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponse = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetParamResponse"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32 = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetParamResponseInt32"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32Delegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRate = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetStatusFrameRate"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRateDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaults = (HAL_Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_ClearStickyFaults"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaultsDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTemp = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_OverTemp"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTempDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltage = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_UnderVoltage"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltageDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_ForLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_RevLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailure = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_HardwareFailure"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailureDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_ForSoftLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_RevSoftLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTemp = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_OverTemp"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTempDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltage = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_UnderVoltage"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltageDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_ForLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_RevLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_ForSoftLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_RevSoftLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottle = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAppliedThrottle"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottleDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErr = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErrDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetCloseLoopErr"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErrDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFeedbackDeviceSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetModeSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetModeSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetModeSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetModeSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEn = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchEn"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEnDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedFor = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedForDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchClosedFor"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedForDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRev = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRevDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchClosedRev"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRevDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorPosition = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetSensorPosition"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorPositionDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocity = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetSensorVelocity"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocityDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCurrent = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetCurrent"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCurrentDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabled = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetBrakeIsEnabled"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabledDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncPosition = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncPosition"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncPositionDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncVel = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncVelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncVel"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncVelDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEvents = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEventsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncIndexRiseEvents"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEventsDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadApin = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadApinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadApin"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadApinDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpin = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadBpin"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpinDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpin = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadIdxpin"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpinDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOv = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOvDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAnalogInWithOv"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOvDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVel = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAnalogInVel"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVelDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetTemp = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetTemp"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetTempDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBatteryV = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBatteryVDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetBatteryV"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBatteryVDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetCount = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetResetCount"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetCountDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetFlags = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetFlagsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetResetFlags"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetFlagsDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFirmVers = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFirmVersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFirmVers"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFirmVersDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetDemand = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetDemandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetDemand"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetDemandDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEn = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetOverrideLimitSwitchEn"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEnDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetFeedbackDeviceSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEn = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRevMotDuringCloseLoopEn"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeType = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetOverrideBrakeType"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeTypeDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetModeSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2 = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetModeSelect2"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2Delegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetProfileSlotSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottle = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRampThrottle"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottleDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensor = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRevFeedbackSensor"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensorDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPosition = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseWidthPosition"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPositionDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocity = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseWidthVelocityr"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocityDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUs = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseRiseToFallUs"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUsDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUs = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseRiseToRiseUs"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresent = (HAL_Base.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_IsPulseWidthSensorPresent"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresentDelegate));

        }
    }
}
