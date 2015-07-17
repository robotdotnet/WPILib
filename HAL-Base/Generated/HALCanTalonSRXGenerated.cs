//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALCanTalonSRX
    {
        static HALCanTalonSRX()
        {
            HAL.Initialize();
        }

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            C_TalonSRX_Create = (C_TalonSRX_CreateDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_CreateDelegate), type.GetMethod("c_TalonSRX_Create"));
            C_TalonSRX_Destroy = (C_TalonSRX_DestroyDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_DestroyDelegate), type.GetMethod("c_TalonSRX_Destroy"));
            C_TalonSRX_SetParam = (C_TalonSRX_SetParamDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetParamDelegate), type.GetMethod("c_TalonSRX_SetParam"));
            C_TalonSRX_RequestParam = (C_TalonSRX_RequestParamDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_RequestParamDelegate), type.GetMethod("c_TalonSRX_RequestParam"));
            C_TalonSRX_GetParamResponse = (C_TalonSRX_GetParamResponseDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetParamResponseDelegate), type.GetMethod("c_TalonSRX_GetParamResponse"));
            C_TalonSRX_GetParamResponseInt32 = (C_TalonSRX_GetParamResponseInt32Delegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetParamResponseInt32Delegate), type.GetMethod("c_TalonSRX_GetParamResponseInt32"));
            C_TalonSRX_SetStatusFrameRate = (C_TalonSRX_SetStatusFrameRateDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetStatusFrameRateDelegate), type.GetMethod("c_TalonSRX_SetStatusFrameRate"));
            C_TalonSRX_ClearStickyFaults = (C_TalonSRX_ClearStickyFaultsDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_ClearStickyFaultsDelegate), type.GetMethod("c_TalonSRX_ClearStickyFaults"));
            C_TalonSRX_GetFault_OverTemp = (C_TalonSRX_GetFault_OverTempDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFault_OverTempDelegate), type.GetMethod("c_TalonSRX_GetFault_OverTemp"));
            C_TalonSRX_GetFault_UnderVoltage = (C_TalonSRX_GetFault_UnderVoltageDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFault_UnderVoltageDelegate), type.GetMethod("c_TalonSRX_GetFault_UnderVoltage"));
            C_TalonSRX_GetFault_ForLim = (C_TalonSRX_GetFault_ForLimDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFault_ForLimDelegate), type.GetMethod("c_TalonSRX_GetFault_ForLim"));
            C_TalonSRX_GetFault_RevLim = (C_TalonSRX_GetFault_RevLimDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFault_RevLimDelegate), type.GetMethod("c_TalonSRX_GetFault_RevLim"));
            C_TalonSRX_GetFault_HardwareFailure = (C_TalonSRX_GetFault_HardwareFailureDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFault_HardwareFailureDelegate), type.GetMethod("c_TalonSRX_GetFault_HardwareFailure"));
            C_TalonSRX_GetFault_ForSoftLim = (C_TalonSRX_GetFault_ForSoftLimDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFault_ForSoftLimDelegate), type.GetMethod("c_TalonSRX_GetFault_ForSoftLim"));
            C_TalonSRX_GetFault_RevSoftLim = (C_TalonSRX_GetFault_RevSoftLimDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFault_RevSoftLimDelegate), type.GetMethod("c_TalonSRX_GetFault_RevSoftLim"));
            C_TalonSRX_GetStckyFault_OverTemp = (C_TalonSRX_GetStckyFault_OverTempDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetStckyFault_OverTempDelegate), type.GetMethod("c_TalonSRX_GetStckyFault_OverTemp"));
            C_TalonSRX_GetStckyFault_UnderVoltage = (C_TalonSRX_GetStckyFault_UnderVoltageDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetStckyFault_UnderVoltageDelegate), type.GetMethod("c_TalonSRX_GetStckyFault_UnderVoltage"));
            C_TalonSRX_GetStckyFault_ForLim = (C_TalonSRX_GetStckyFault_ForLimDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetStckyFault_ForLimDelegate), type.GetMethod("c_TalonSRX_GetStckyFault_ForLim"));
            C_TalonSRX_GetStckyFault_RevLim = (C_TalonSRX_GetStckyFault_RevLimDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetStckyFault_RevLimDelegate), type.GetMethod("c_TalonSRX_GetStckyFault_RevLim"));
            C_TalonSRX_GetStckyFault_ForSoftLim = (C_TalonSRX_GetStckyFault_ForSoftLimDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetStckyFault_ForSoftLimDelegate), type.GetMethod("c_TalonSRX_GetStckyFault_ForSoftLim"));
            C_TalonSRX_GetStckyFault_RevSoftLim = (C_TalonSRX_GetStckyFault_RevSoftLimDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetStckyFault_RevSoftLimDelegate), type.GetMethod("c_TalonSRX_GetStckyFault_RevSoftLim"));
            C_TalonSRX_GetAppliedThrottle = (C_TalonSRX_GetAppliedThrottleDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetAppliedThrottleDelegate), type.GetMethod("c_TalonSRX_GetAppliedThrottle"));
            C_TalonSRX_GetCloseLoopErr = (C_TalonSRX_GetCloseLoopErrDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetCloseLoopErrDelegate), type.GetMethod("c_TalonSRX_GetCloseLoopErr"));
            C_TalonSRX_GetFeedbackDeviceSelect = (C_TalonSRX_GetFeedbackDeviceSelectDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFeedbackDeviceSelectDelegate), type.GetMethod("c_TalonSRX_GetFeedbackDeviceSelect"));
            C_TalonSRX_GetModeSelect = (C_TalonSRX_GetModeSelectDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetModeSelectDelegate), type.GetMethod("c_TalonSRX_GetModeSelect"));
            C_TalonSRX_GetLimitSwitchEn = (C_TalonSRX_GetLimitSwitchEnDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetLimitSwitchEnDelegate), type.GetMethod("c_TalonSRX_GetLimitSwitchEn"));
            C_TalonSRX_GetLimitSwitchClosedFor = (C_TalonSRX_GetLimitSwitchClosedForDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetLimitSwitchClosedForDelegate), type.GetMethod("c_TalonSRX_GetLimitSwitchClosedFor"));
            C_TalonSRX_GetLimitSwitchClosedRev = (C_TalonSRX_GetLimitSwitchClosedRevDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetLimitSwitchClosedRevDelegate), type.GetMethod("c_TalonSRX_GetLimitSwitchClosedRev"));
            C_TalonSRX_GetSensorPosition = (C_TalonSRX_GetSensorPositionDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetSensorPositionDelegate), type.GetMethod("c_TalonSRX_GetSensorPosition"));
            C_TalonSRX_GetSensorVelocity = (C_TalonSRX_GetSensorVelocityDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetSensorVelocityDelegate), type.GetMethod("c_TalonSRX_GetSensorVelocity"));
            C_TalonSRX_GetCurrent = (C_TalonSRX_GetCurrentDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetCurrentDelegate), type.GetMethod("c_TalonSRX_GetCurrent"));
            C_TalonSRX_GetBrakeIsEnabled = (C_TalonSRX_GetBrakeIsEnabledDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetBrakeIsEnabledDelegate), type.GetMethod("c_TalonSRX_GetBrakeIsEnabled"));
            C_TalonSRX_GetEncPosition = (C_TalonSRX_GetEncPositionDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetEncPositionDelegate), type.GetMethod("c_TalonSRX_GetEncPosition"));
            C_TalonSRX_GetEncVel = (C_TalonSRX_GetEncVelDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetEncVelDelegate), type.GetMethod("c_TalonSRX_GetEncVel"));
            C_TalonSRX_GetEncIndexRiseEvents = (C_TalonSRX_GetEncIndexRiseEventsDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetEncIndexRiseEventsDelegate), type.GetMethod("c_TalonSRX_GetEncIndexRiseEvents"));
            C_TalonSRX_GetQuadApin = (C_TalonSRX_GetQuadApinDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetQuadApinDelegate), type.GetMethod("c_TalonSRX_GetQuadApin"));
            C_TalonSRX_GetQuadBpin = (C_TalonSRX_GetQuadBpinDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetQuadBpinDelegate), type.GetMethod("c_TalonSRX_GetQuadBpin"));
            C_TalonSRX_GetQuadIdxpin = (C_TalonSRX_GetQuadIdxpinDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetQuadIdxpinDelegate), type.GetMethod("c_TalonSRX_GetQuadIdxpin"));
            C_TalonSRX_GetAnalogInWithOv = (C_TalonSRX_GetAnalogInWithOvDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetAnalogInWithOvDelegate), type.GetMethod("c_TalonSRX_GetAnalogInWithOv"));
            C_TalonSRX_GetAnalogInVel = (C_TalonSRX_GetAnalogInVelDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetAnalogInVelDelegate), type.GetMethod("c_TalonSRX_GetAnalogInVel"));
            C_TalonSRX_GetTemp = (C_TalonSRX_GetTempDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetTempDelegate), type.GetMethod("c_TalonSRX_GetTemp"));
            C_TalonSRX_GetBatteryV = (C_TalonSRX_GetBatteryVDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetBatteryVDelegate), type.GetMethod("c_TalonSRX_GetBatteryV"));
            C_TalonSRX_GetResetCount = (C_TalonSRX_GetResetCountDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetResetCountDelegate), type.GetMethod("c_TalonSRX_GetResetCount"));
            C_TalonSRX_GetResetFlags = (C_TalonSRX_GetResetFlagsDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetResetFlagsDelegate), type.GetMethod("c_TalonSRX_GetResetFlags"));
            C_TalonSRX_GetFirmVers = (C_TalonSRX_GetFirmVersDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_GetFirmVersDelegate), type.GetMethod("c_TalonSRX_GetFirmVers"));
            C_TalonSRX_SetDemand = (C_TalonSRX_SetDemandDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetDemandDelegate), type.GetMethod("c_TalonSRX_SetDemand"));
            C_TalonSRX_SetOverrideLimitSwitchEn = (C_TalonSRX_SetOverrideLimitSwitchEnDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetOverrideLimitSwitchEnDelegate), type.GetMethod("c_TalonSRX_SetOverrideLimitSwitchEn"));
            C_TalonSRX_SetFeedbackDeviceSelect = (C_TalonSRX_SetFeedbackDeviceSelectDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetFeedbackDeviceSelectDelegate), type.GetMethod("c_TalonSRX_SetFeedbackDeviceSelect"));
            C_TalonSRX_SetRevMotDuringCloseLoopEn = (C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate), type.GetMethod("c_TalonSRX_SetRevMotDuringCloseLoopEn"));
            C_TalonSRX_SetOverrideBrakeType = (C_TalonSRX_SetOverrideBrakeTypeDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetOverrideBrakeTypeDelegate), type.GetMethod("c_TalonSRX_SetOverrideBrakeType"));
            C_TalonSRX_SetModeSelect = (C_TalonSRX_SetModeSelectDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetModeSelectDelegate), type.GetMethod("c_TalonSRX_SetModeSelect"));
            C_TalonSRX_SetModeSelect2 = (C_TalonSRX_SetModeSelect2Delegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetModeSelect2Delegate), type.GetMethod("c_TalonSRX_SetModeSelect2"));
            C_TalonSRX_SetProfileSlotSelect = (C_TalonSRX_SetProfileSlotSelectDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetProfileSlotSelectDelegate), type.GetMethod("c_TalonSRX_SetProfileSlotSelect"));
            C_TalonSRX_SetRampThrottle = (C_TalonSRX_SetRampThrottleDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetRampThrottleDelegate), type.GetMethod("c_TalonSRX_SetRampThrottle"));
            C_TalonSRX_SetRevFeedbackSensor = (C_TalonSRX_SetRevFeedbackSensorDelegate)Delegate.CreateDelegate(typeof(C_TalonSRX_SetRevFeedbackSensorDelegate), type.GetMethod("c_TalonSRX_SetRevFeedbackSensor"));
        }

        public delegate IntPtr C_TalonSRX_CreateDelegate(int deviceNumber, int controlPeriodMs);
        public static C_TalonSRX_CreateDelegate C_TalonSRX_Create;

        public delegate void C_TalonSRX_DestroyDelegate(IntPtr handle);
        public static C_TalonSRX_DestroyDelegate C_TalonSRX_Destroy;

        public delegate CTR_Code C_TalonSRX_SetParamDelegate(IntPtr handle, int paramEnum, double value);
        public static C_TalonSRX_SetParamDelegate C_TalonSRX_SetParam;

        public delegate CTR_Code C_TalonSRX_RequestParamDelegate(IntPtr handle, int paramEnum);
        public static C_TalonSRX_RequestParamDelegate C_TalonSRX_RequestParam;

        public delegate CTR_Code C_TalonSRX_GetParamResponseDelegate(IntPtr handle, int paramEnum, ref double value);
        public static C_TalonSRX_GetParamResponseDelegate C_TalonSRX_GetParamResponse;

        public delegate CTR_Code C_TalonSRX_GetParamResponseInt32Delegate(IntPtr handle, int paramEnum, ref int value);
        public static C_TalonSRX_GetParamResponseInt32Delegate C_TalonSRX_GetParamResponseInt32;

        public delegate CTR_Code C_TalonSRX_SetStatusFrameRateDelegate(IntPtr handle, uint frameEnum, uint periodMs);
        public static C_TalonSRX_SetStatusFrameRateDelegate C_TalonSRX_SetStatusFrameRate;

        public delegate CTR_Code C_TalonSRX_ClearStickyFaultsDelegate(IntPtr handle);
        public static C_TalonSRX_ClearStickyFaultsDelegate C_TalonSRX_ClearStickyFaults;

        public delegate CTR_Code C_TalonSRX_GetFault_OverTempDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFault_OverTempDelegate C_TalonSRX_GetFault_OverTemp;

        public delegate CTR_Code C_TalonSRX_GetFault_UnderVoltageDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFault_UnderVoltageDelegate C_TalonSRX_GetFault_UnderVoltage;

        public delegate CTR_Code C_TalonSRX_GetFault_ForLimDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFault_ForLimDelegate C_TalonSRX_GetFault_ForLim;

        public delegate CTR_Code C_TalonSRX_GetFault_RevLimDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFault_RevLimDelegate C_TalonSRX_GetFault_RevLim;

        public delegate CTR_Code C_TalonSRX_GetFault_HardwareFailureDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFault_HardwareFailureDelegate C_TalonSRX_GetFault_HardwareFailure;

        public delegate CTR_Code C_TalonSRX_GetFault_ForSoftLimDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFault_ForSoftLimDelegate C_TalonSRX_GetFault_ForSoftLim;

        public delegate CTR_Code C_TalonSRX_GetFault_RevSoftLimDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFault_RevSoftLimDelegate C_TalonSRX_GetFault_RevSoftLim;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_OverTempDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetStckyFault_OverTempDelegate C_TalonSRX_GetStckyFault_OverTemp;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_UnderVoltageDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetStckyFault_UnderVoltageDelegate C_TalonSRX_GetStckyFault_UnderVoltage;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_ForLimDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetStckyFault_ForLimDelegate C_TalonSRX_GetStckyFault_ForLim;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_RevLimDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetStckyFault_RevLimDelegate C_TalonSRX_GetStckyFault_RevLim;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_ForSoftLimDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetStckyFault_ForSoftLimDelegate C_TalonSRX_GetStckyFault_ForSoftLim;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_RevSoftLimDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetStckyFault_RevSoftLimDelegate C_TalonSRX_GetStckyFault_RevSoftLim;

        public delegate CTR_Code C_TalonSRX_GetAppliedThrottleDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetAppliedThrottleDelegate C_TalonSRX_GetAppliedThrottle;

        public delegate CTR_Code C_TalonSRX_GetCloseLoopErrDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetCloseLoopErrDelegate C_TalonSRX_GetCloseLoopErr;

        public delegate CTR_Code C_TalonSRX_GetFeedbackDeviceSelectDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFeedbackDeviceSelectDelegate C_TalonSRX_GetFeedbackDeviceSelect;

        public delegate CTR_Code C_TalonSRX_GetModeSelectDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetModeSelectDelegate C_TalonSRX_GetModeSelect;

        public delegate CTR_Code C_TalonSRX_GetLimitSwitchEnDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetLimitSwitchEnDelegate C_TalonSRX_GetLimitSwitchEn;

        public delegate CTR_Code C_TalonSRX_GetLimitSwitchClosedForDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetLimitSwitchClosedForDelegate C_TalonSRX_GetLimitSwitchClosedFor;

        public delegate CTR_Code C_TalonSRX_GetLimitSwitchClosedRevDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetLimitSwitchClosedRevDelegate C_TalonSRX_GetLimitSwitchClosedRev;

        public delegate CTR_Code C_TalonSRX_GetSensorPositionDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetSensorPositionDelegate C_TalonSRX_GetSensorPosition;

        public delegate CTR_Code C_TalonSRX_GetSensorVelocityDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetSensorVelocityDelegate C_TalonSRX_GetSensorVelocity;

        public delegate CTR_Code C_TalonSRX_GetCurrentDelegate(IntPtr handle, ref double param);
        public static C_TalonSRX_GetCurrentDelegate C_TalonSRX_GetCurrent;

        public delegate CTR_Code C_TalonSRX_GetBrakeIsEnabledDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetBrakeIsEnabledDelegate C_TalonSRX_GetBrakeIsEnabled;

        public delegate CTR_Code C_TalonSRX_GetEncPositionDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetEncPositionDelegate C_TalonSRX_GetEncPosition;

        public delegate CTR_Code C_TalonSRX_GetEncVelDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetEncVelDelegate C_TalonSRX_GetEncVel;

        public delegate CTR_Code C_TalonSRX_GetEncIndexRiseEventsDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetEncIndexRiseEventsDelegate C_TalonSRX_GetEncIndexRiseEvents;

        public delegate CTR_Code C_TalonSRX_GetQuadApinDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetQuadApinDelegate C_TalonSRX_GetQuadApin;

        public delegate CTR_Code C_TalonSRX_GetQuadBpinDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetQuadBpinDelegate C_TalonSRX_GetQuadBpin;

        public delegate CTR_Code C_TalonSRX_GetQuadIdxpinDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetQuadIdxpinDelegate C_TalonSRX_GetQuadIdxpin;

        public delegate CTR_Code C_TalonSRX_GetAnalogInWithOvDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetAnalogInWithOvDelegate C_TalonSRX_GetAnalogInWithOv;

        public delegate CTR_Code C_TalonSRX_GetAnalogInVelDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetAnalogInVelDelegate C_TalonSRX_GetAnalogInVel;

        public delegate CTR_Code C_TalonSRX_GetTempDelegate(IntPtr handle, ref double param);
        public static C_TalonSRX_GetTempDelegate C_TalonSRX_GetTemp;

        public delegate CTR_Code C_TalonSRX_GetBatteryVDelegate(IntPtr handle, ref double param);
        public static C_TalonSRX_GetBatteryVDelegate C_TalonSRX_GetBatteryV;

        public delegate CTR_Code C_TalonSRX_GetResetCountDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetResetCountDelegate C_TalonSRX_GetResetCount;

        public delegate CTR_Code C_TalonSRX_GetResetFlagsDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetResetFlagsDelegate C_TalonSRX_GetResetFlags;

        public delegate CTR_Code C_TalonSRX_GetFirmVersDelegate(IntPtr handle, ref int param);
        public static C_TalonSRX_GetFirmVersDelegate C_TalonSRX_GetFirmVers;

        public delegate CTR_Code C_TalonSRX_SetDemandDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetDemandDelegate C_TalonSRX_SetDemand;

        public delegate CTR_Code C_TalonSRX_SetOverrideLimitSwitchEnDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetOverrideLimitSwitchEnDelegate C_TalonSRX_SetOverrideLimitSwitchEn;

        public delegate CTR_Code C_TalonSRX_SetFeedbackDeviceSelectDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetFeedbackDeviceSelectDelegate C_TalonSRX_SetFeedbackDeviceSelect;

        public delegate CTR_Code C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate C_TalonSRX_SetRevMotDuringCloseLoopEn;

        public delegate CTR_Code C_TalonSRX_SetOverrideBrakeTypeDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetOverrideBrakeTypeDelegate C_TalonSRX_SetOverrideBrakeType;

        public delegate CTR_Code C_TalonSRX_SetModeSelectDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetModeSelectDelegate C_TalonSRX_SetModeSelect;

        public delegate CTR_Code C_TalonSRX_SetModeSelect2Delegate(IntPtr handle, int modeSelect, int demand);
        public static C_TalonSRX_SetModeSelect2Delegate C_TalonSRX_SetModeSelect2;

        public delegate CTR_Code C_TalonSRX_SetProfileSlotSelectDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetProfileSlotSelectDelegate C_TalonSRX_SetProfileSlotSelect;

        public delegate CTR_Code C_TalonSRX_SetRampThrottleDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetRampThrottleDelegate C_TalonSRX_SetRampThrottle;

        public delegate CTR_Code C_TalonSRX_SetRevFeedbackSensorDelegate(IntPtr handle, int param);
        public static C_TalonSRX_SetRevFeedbackSensorDelegate C_TalonSRX_SetRevFeedbackSensor;
    }
}
