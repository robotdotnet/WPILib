//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCanTalonSRX
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALCanTalonSRX.C_TalonSRX_Create = (Base.HALCanTalonSRX.C_TalonSRX_CreateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_Create"), typeof(Base.HALCanTalonSRX.C_TalonSRX_CreateDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_Destroy = (Base.HALCanTalonSRX.C_TalonSRX_DestroyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_Destroy"), typeof(Base.HALCanTalonSRX.C_TalonSRX_DestroyDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetParam = (Base.HALCanTalonSRX.C_TalonSRX_SetParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetParam"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetParamDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_RequestParam = (Base.HALCanTalonSRX.C_TalonSRX_RequestParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_RequestParam"), typeof(Base.HALCanTalonSRX.C_TalonSRX_RequestParamDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetParamResponse = (Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetParamResponse"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32 = (Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetParamResponseInt32"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32Delegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRate = (Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetStatusFrameRate"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRateDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaults = (Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_ClearStickyFaults"), typeof(Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaultsDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTemp = (Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_OverTemp"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTempDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltage = (Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_UnderVoltage"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltageDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLim = (Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_ForLim"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLimDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLim = (Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_RevLim"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLimDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailure = (Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_HardwareFailure"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailureDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLim = (Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_ForSoftLim"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLimDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLim = (Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_RevSoftLim"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLimDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTemp = (Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_OverTemp"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTempDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltage = (Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_UnderVoltage"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltageDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLim = (Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_ForLim"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLimDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLim = (Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_RevLim"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLimDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLim = (Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_ForSoftLim"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLimDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLim = (Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_RevSoftLim"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLimDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottle = (Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAppliedThrottle"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottleDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErr = (Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErrDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetCloseLoopErr"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErrDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelect = (Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFeedbackDeviceSelect"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelectDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetModeSelect = (Base.HALCanTalonSRX.C_TalonSRX_GetModeSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetModeSelect"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetModeSelectDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEn = (Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchEn"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEnDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedFor = (Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedForDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchClosedFor"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedForDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRev = (Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRevDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchClosedRev"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRevDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetSensorPosition = (Base.HALCanTalonSRX.C_TalonSRX_GetSensorPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetSensorPosition"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetSensorPositionDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocity = (Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetSensorVelocity"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocityDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetCurrent = (Base.HALCanTalonSRX.C_TalonSRX_GetCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetCurrent"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetCurrentDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabled = (Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetBrakeIsEnabled"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabledDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetEncPosition = (Base.HALCanTalonSRX.C_TalonSRX_GetEncPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncPosition"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetEncPositionDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetEncVel = (Base.HALCanTalonSRX.C_TalonSRX_GetEncVelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncVel"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetEncVelDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEvents = (Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEventsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncIndexRiseEvents"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEventsDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetQuadApin = (Base.HALCanTalonSRX.C_TalonSRX_GetQuadApinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadApin"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetQuadApinDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpin = (Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadBpin"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpinDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpin = (Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadIdxpin"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpinDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOv = (Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOvDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAnalogInWithOv"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOvDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVel = (Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAnalogInVel"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVelDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetTemp = (Base.HALCanTalonSRX.C_TalonSRX_GetTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetTemp"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetTempDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetBatteryV = (Base.HALCanTalonSRX.C_TalonSRX_GetBatteryVDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetBatteryV"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetBatteryVDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetResetCount = (Base.HALCanTalonSRX.C_TalonSRX_GetResetCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetResetCount"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetResetCountDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetResetFlags = (Base.HALCanTalonSRX.C_TalonSRX_GetResetFlagsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetResetFlags"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetResetFlagsDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetFirmVers = (Base.HALCanTalonSRX.C_TalonSRX_GetFirmVersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFirmVers"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetFirmVersDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetDemand = (Base.HALCanTalonSRX.C_TalonSRX_SetDemandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetDemand"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetDemandDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEn = (Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetOverrideLimitSwitchEn"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEnDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelect = (Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetFeedbackDeviceSelect"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelectDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEn = (Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRevMotDuringCloseLoopEn"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeType = (Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetOverrideBrakeType"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeTypeDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect = (Base.HALCanTalonSRX.C_TalonSRX_SetModeSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetModeSelect"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetModeSelectDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2 = (Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetModeSelect2"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2Delegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelect = (Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetProfileSlotSelect"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelectDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottle = (Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRampThrottle"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottleDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensor = (Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRevFeedbackSensor"), typeof(Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensorDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPosition = (Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseWidthPosition"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPositionDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocity = (Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseWidthVelocityr"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocityDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUs = (Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseRiseToFallUs"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUsDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUs = (Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetPulseRiseToRiseUs"), typeof(Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate));

            Base.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresent = (Base.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_IsPulseWidthSensorPresent"), typeof(Base.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresentDelegate));

        }
    }
}
