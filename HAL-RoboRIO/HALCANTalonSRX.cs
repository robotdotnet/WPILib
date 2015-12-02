//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCanTalonSRX
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALCanTalonSRX.C_TalonSRX_Create = (global::HAL.HALCanTalonSRX.C_TalonSRX_CreateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_Create"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_CreateDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_Destroy = (global::HAL.HALCanTalonSRX.C_TalonSRX_DestroyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_Destroy"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_DestroyDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetParam = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetParam"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetParamDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_RequestParam = (global::HAL.HALCanTalonSRX.C_TalonSRX_RequestParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_RequestParam"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_RequestParamDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetParamResponse = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetParamResponseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetParamResponse"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetParamResponseDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32 = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetParamResponseInt32"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32Delegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRate = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetStatusFrameRate"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRateDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_ClearStickyFaults = (global::HAL.HALCanTalonSRX.C_TalonSRX_ClearStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_ClearStickyFaults"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_ClearStickyFaultsDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_OverTemp = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_OverTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_OverTemp"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_OverTempDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltage = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_UnderVoltage"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltageDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_ForLim = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_ForLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_ForLim"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_ForLimDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_RevLim = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_RevLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_RevLim"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_RevLimDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailure = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_HardwareFailure"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailureDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLim = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_ForSoftLim"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLimDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLim = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_RevSoftLim"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLimDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTemp = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_OverTemp"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTempDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltage = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_UnderVoltage"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltageDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLim = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_ForLim"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLimDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLim = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_RevLim"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLimDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLim = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_ForSoftLim"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLimDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLim = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_RevSoftLim"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLimDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottle = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAppliedThrottle"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottleDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErr = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErrDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetCloseLoopErr"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErrDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelect = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFeedbackDeviceSelect"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelectDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetModeSelect = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetModeSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetModeSelect"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetModeSelectDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEn = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchEn"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEnDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedFor = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedForDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchClosedFor"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedForDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRev = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRevDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchClosedRev"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRevDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetSensorPosition = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetSensorPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetSensorPosition"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetSensorPositionDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetSensorVelocity = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetSensorVelocityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetSensorVelocity"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetSensorVelocityDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetCurrent = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetCurrent"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetCurrentDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabled = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetBrakeIsEnabled"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabledDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncPosition = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncPosition"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncPositionDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncVel = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncVelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncVel"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncVelDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEvents = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEventsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncIndexRiseEvents"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEventsDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadApin = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadApinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadApin"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadApinDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadBpin = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadBpinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadBpin"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadBpinDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpin = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadIdxpin"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpinDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOv = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOvDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAnalogInWithOv"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOvDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetAnalogInVel = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetAnalogInVelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAnalogInVel"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetAnalogInVelDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetTemp = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetTemp"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetTempDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetBatteryV = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetBatteryVDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetBatteryV"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetBatteryVDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetResetCount = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetResetCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetResetCount"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetResetCountDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetResetFlags = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetResetFlagsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetResetFlags"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetResetFlagsDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFirmVers = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetFirmVersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFirmVers"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetFirmVersDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetDemand = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetDemandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetDemand"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetDemandDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEn = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetOverrideLimitSwitchEn"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEnDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelect = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetFeedbackDeviceSelect"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelectDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEn = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRevMotDuringCloseLoopEn"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeType = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetOverrideBrakeType"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeTypeDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetModeSelect = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetModeSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetModeSelect"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetModeSelectDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetModeSelect2 = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetModeSelect2Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetModeSelect2"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetModeSelect2Delegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelect = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetProfileSlotSelect"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelectDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetRampThrottle = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetRampThrottleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRampThrottle"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetRampThrottleDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensor = (global::HAL.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRevFeedbackSensor"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensorDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPosition = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseWidthPosition"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPositionDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocity = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseWidthVelocityr"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocityDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUs = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseRiseToFallUs"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUsDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUs = (global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseRiseToRiseUs"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate));

            global::HAL.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresent = (global::HAL.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_IsPulseWidthSensorPresent"), typeof(global::HAL.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresentDelegate));

        }
    }
}
