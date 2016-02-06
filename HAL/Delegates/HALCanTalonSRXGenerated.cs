//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALCanTalonSRX
    {
        static HALCanTalonSRX()
        {
            HAL.Initialize();
        }

        public delegate CANTalonSafeHandle C_TalonSRX_Create3Delegate(int deviceNumber, int controlPeriodMs, int enablePeriodMs);
        public static C_TalonSRX_Create3Delegate C_TalonSRX_Create3;

        public delegate void C_TalonSRX_DestroyDelegate(CANTalonSafeHandle handle);
        public static C_TalonSRX_DestroyDelegate C_TalonSRX_Destroy;

        public delegate void C_TalonSRX_SetDelegate(CANTalonSafeHandle handle, double value);
        public static C_TalonSRX_SetDelegate C_TalonSRX_Set;

        public delegate CTR_Code C_TalonSRX_SetParamDelegate(CANTalonSafeHandle handle, int paramEnum, double value);
        public static C_TalonSRX_SetParamDelegate C_TalonSRX_SetParam;

        public delegate CTR_Code C_TalonSRX_RequestParamDelegate(CANTalonSafeHandle handle, int paramEnum);
        public static C_TalonSRX_RequestParamDelegate C_TalonSRX_RequestParam;

        public delegate CTR_Code C_TalonSRX_GetParamResponseDelegate(CANTalonSafeHandle handle, int paramEnum, ref double value);
        public static C_TalonSRX_GetParamResponseDelegate C_TalonSRX_GetParamResponse;

        public delegate CTR_Code C_TalonSRX_GetParamResponseInt32Delegate(CANTalonSafeHandle handle, int paramEnum, ref int value);
        public static C_TalonSRX_GetParamResponseInt32Delegate C_TalonSRX_GetParamResponseInt32;

        public delegate CTR_Code C_TalonSRX_SetPgainDelegate(CANTalonSafeHandle handle, int slotIdx, double gain);
        public static C_TalonSRX_SetPgainDelegate C_TalonSRX_SetPgain;

        public delegate CTR_Code C_TalonSRX_SetIgainDelegate(CANTalonSafeHandle handle, int slotIdx, double gain);
        public static C_TalonSRX_SetIgainDelegate C_TalonSRX_SetIgain;

        public delegate CTR_Code C_TalonSRX_SetDgainDelegate(CANTalonSafeHandle handle, int slotIdx, double gain);
        public static C_TalonSRX_SetDgainDelegate C_TalonSRX_SetDgain;

        public delegate CTR_Code C_TalonSRX_SetFgainDelegate(CANTalonSafeHandle handle, int slotIdx, double gain);
        public static C_TalonSRX_SetFgainDelegate C_TalonSRX_SetFgain;

        public delegate CTR_Code C_TalonSRX_SetIzoneDelegate(CANTalonSafeHandle handle, int slotIdx, int zone);
        public static C_TalonSRX_SetIzoneDelegate C_TalonSRX_SetIzone;

        public delegate CTR_Code C_TalonSRX_SetCloseLoopRampRateDelegate(CANTalonSafeHandle handle, int slotIdx, int closeLoopRampRate);
        public static C_TalonSRX_SetCloseLoopRampRateDelegate C_TalonSRX_SetCloseLoopRampRate;

        public delegate CTR_Code C_TalonSRX_SetVoltageCompensationRateDelegate(CANTalonSafeHandle handle, double voltagePerMs);
        public static C_TalonSRX_SetVoltageCompensationRateDelegate C_TalonSRX_SetVoltageCompensationRate;

        public delegate CTR_Code C_TalonSRX_SetSensorPositionDelegate(CANTalonSafeHandle handle, int pos);
        public static C_TalonSRX_SetSensorPositionDelegate C_TalonSRX_SetSensorPosition;

        public delegate CTR_Code C_TalonSRX_SetForwardSoftLimitDelegate(CANTalonSafeHandle handle, int forwardLimit);
        public static C_TalonSRX_SetForwardSoftLimitDelegate C_TalonSRX_SetForwardSoftLimit;

        public delegate CTR_Code C_TalonSRX_SetReverseSoftLimitDelegate(CANTalonSafeHandle handle, int reverseLimit);
        public static C_TalonSRX_SetReverseSoftLimitDelegate C_TalonSRX_SetReverseSoftLimit;

        public delegate CTR_Code C_TalonSRX_SetForwardSoftEnableDelegate(CANTalonSafeHandle handle, int enable);
        public static C_TalonSRX_SetForwardSoftEnableDelegate C_TalonSRX_SetForwardSoftEnable;

        public delegate CTR_Code C_TalonSRX_SetReverseSoftEnableDelegate(CANTalonSafeHandle handle, int enable);
        public static C_TalonSRX_SetReverseSoftEnableDelegate C_TalonSRX_SetReverseSoftEnable;

        public delegate CTR_Code C_TalonSRX_GetPgainDelegate(CANTalonSafeHandle handle, int slotIdx, ref double gain);
        public static C_TalonSRX_GetPgainDelegate C_TalonSRX_GetPgain;

        public delegate CTR_Code C_TalonSRX_GetIgainDelegate(CANTalonSafeHandle handle, int slotIdx, ref double gain);
        public static C_TalonSRX_GetIgainDelegate C_TalonSRX_GetIgain;

        public delegate CTR_Code C_TalonSRX_GetDgainDelegate(CANTalonSafeHandle handle, int slotIdx, ref double gain);
        public static C_TalonSRX_GetDgainDelegate C_TalonSRX_GetDgain;

        public delegate CTR_Code C_TalonSRX_GetFgainDelegate(CANTalonSafeHandle handle, int slotIdx, ref double gain);
        public static C_TalonSRX_GetFgainDelegate C_TalonSRX_GetFgain;

        public delegate CTR_Code C_TalonSRX_GetIzoneDelegate(CANTalonSafeHandle handle, int slotIdx, ref int zone);
        public static C_TalonSRX_GetIzoneDelegate C_TalonSRX_GetIzone;

        public delegate CTR_Code C_TalonSRX_GetCloseLoopRampRateDelegate(CANTalonSafeHandle handle, int slotIdx, ref int closeLoopRampRate);
        public static C_TalonSRX_GetCloseLoopRampRateDelegate C_TalonSRX_GetCloseLoopRampRate;

        public delegate CTR_Code C_TalonSRX_GetVoltageCompensationRateDelegate(CANTalonSafeHandle handle, ref double voltagePerMs);
        public static C_TalonSRX_GetVoltageCompensationRateDelegate C_TalonSRX_GetVoltageCompensationRate;

        public delegate CTR_Code C_TalonSRX_GetForwardSoftLimitDelegate(CANTalonSafeHandle handle, ref int forwardLimit);
        public static C_TalonSRX_GetForwardSoftLimitDelegate C_TalonSRX_GetForwardSoftLimit;

        public delegate CTR_Code C_TalonSRX_GetReverseSoftLimitDelegate(CANTalonSafeHandle handle, ref int reverseLimit);
        public static C_TalonSRX_GetReverseSoftLimitDelegate C_TalonSRX_GetReverseSoftLimit;

        public delegate CTR_Code C_TalonSRX_GetForwardSoftEnableDelegate(CANTalonSafeHandle handle, ref int enable);
        public static C_TalonSRX_GetForwardSoftEnableDelegate C_TalonSRX_GetForwardSoftEnable;

        public delegate CTR_Code C_TalonSRX_GetReverseSoftEnableDelegate(CANTalonSafeHandle handle, ref int enable);
        public static C_TalonSRX_GetReverseSoftEnableDelegate C_TalonSRX_GetReverseSoftEnable;

        public delegate CTR_Code C_TalonSRX_GetPulseWidthRiseToFallUsDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetPulseWidthRiseToFallUsDelegate C_TalonSRX_GetPulseWidthRiseToFallUs;

        public delegate CTR_Code C_TalonSRX_IsPulseWidthSensorPresentDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_IsPulseWidthSensorPresentDelegate C_TalonSRX_IsPulseWidthSensorPresent;

        public delegate CTR_Code C_TalonSRX_SetModeSelect2Delegate(CANTalonSafeHandle handle, int modeSelect, int demand);
        public static C_TalonSRX_SetModeSelect2Delegate C_TalonSRX_SetModeSelect2;

        public delegate CTR_Code C_TalonSRX_SetStatusFrameRateDelegate(CANTalonSafeHandle handle, int frameEnum, int periodMs);
        public static C_TalonSRX_SetStatusFrameRateDelegate C_TalonSRX_SetStatusFrameRate;

        public delegate CTR_Code C_TalonSRX_ClearStickyFaultsDelegate(CANTalonSafeHandle handle);
        public static C_TalonSRX_ClearStickyFaultsDelegate C_TalonSRX_ClearStickyFaults;

        public delegate void C_TalonSRX_ChangeMotionControlFramePeriodDelegate(CANTalonSafeHandle handle, int periodMs);
        public static C_TalonSRX_ChangeMotionControlFramePeriodDelegate C_TalonSRX_ChangeMotionControlFramePeriod;

        public delegate void C_TalonSRX_ClearMotionProfileTrajectoriesDelegate(CANTalonSafeHandle handle);
        public static C_TalonSRX_ClearMotionProfileTrajectoriesDelegate C_TalonSRX_ClearMotionProfileTrajectories;

        public delegate int C_TalonSRX_GetMotionProfileTopLevelBufferCountDelegate(CANTalonSafeHandle handle);
        public static C_TalonSRX_GetMotionProfileTopLevelBufferCountDelegate C_TalonSRX_GetMotionProfileTopLevelBufferCount;

        public delegate int C_TalonSRX_IsMotionProfileTopLevelBufferFullDelegate(CANTalonSafeHandle handle);
        public static C_TalonSRX_IsMotionProfileTopLevelBufferFullDelegate C_TalonSRX_IsMotionProfileTopLevelBufferFull;

        public delegate CTR_Code C_TalonSRX_PushMotionProfileTrajectoryDelegate(CANTalonSafeHandle handle, int targPos, int targVel, int profileSlotSelect, int timeDurMs, int velOnly, int isLastPoint, int zeroPos);
        public static C_TalonSRX_PushMotionProfileTrajectoryDelegate C_TalonSRX_PushMotionProfileTrajectory;

        public delegate void C_TalonSRX_ProcessMotionProfileBufferDelegate(CANTalonSafeHandle handle);
        public static C_TalonSRX_ProcessMotionProfileBufferDelegate C_TalonSRX_ProcessMotionProfileBuffer;

        public delegate CTR_Code C_TalonSRX_GetMotionProfileStatusDelegate(CANTalonSafeHandle handle, ref int flags, ref int profileSlotSelect, ref int targPos, ref int targVel, ref int topBufferRemaining, ref int topBufferCnt, ref int btmBufferCnt, ref int outputEnable);
        public static C_TalonSRX_GetMotionProfileStatusDelegate C_TalonSRX_GetMotionProfileStatus;

        public delegate CTR_Code C_TalonSRX_GetFault_OverTempDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFault_OverTempDelegate C_TalonSRX_GetFault_OverTemp;

        public delegate CTR_Code C_TalonSRX_GetFault_UnderVoltageDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFault_UnderVoltageDelegate C_TalonSRX_GetFault_UnderVoltage;

        public delegate CTR_Code C_TalonSRX_GetFault_ForLimDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFault_ForLimDelegate C_TalonSRX_GetFault_ForLim;

        public delegate CTR_Code C_TalonSRX_GetFault_RevLimDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFault_RevLimDelegate C_TalonSRX_GetFault_RevLim;

        public delegate CTR_Code C_TalonSRX_GetFault_HardwareFailureDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFault_HardwareFailureDelegate C_TalonSRX_GetFault_HardwareFailure;

        public delegate CTR_Code C_TalonSRX_GetFault_ForSoftLimDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFault_ForSoftLimDelegate C_TalonSRX_GetFault_ForSoftLim;

        public delegate CTR_Code C_TalonSRX_GetFault_RevSoftLimDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFault_RevSoftLimDelegate C_TalonSRX_GetFault_RevSoftLim;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_OverTempDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetStckyFault_OverTempDelegate C_TalonSRX_GetStckyFault_OverTemp;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_UnderVoltageDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetStckyFault_UnderVoltageDelegate C_TalonSRX_GetStckyFault_UnderVoltage;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_ForLimDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetStckyFault_ForLimDelegate C_TalonSRX_GetStckyFault_ForLim;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_RevLimDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetStckyFault_RevLimDelegate C_TalonSRX_GetStckyFault_RevLim;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_ForSoftLimDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetStckyFault_ForSoftLimDelegate C_TalonSRX_GetStckyFault_ForSoftLim;

        public delegate CTR_Code C_TalonSRX_GetStckyFault_RevSoftLimDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetStckyFault_RevSoftLimDelegate C_TalonSRX_GetStckyFault_RevSoftLim;

        public delegate CTR_Code C_TalonSRX_GetAppliedThrottleDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetAppliedThrottleDelegate C_TalonSRX_GetAppliedThrottle;

        public delegate CTR_Code C_TalonSRX_GetCloseLoopErrDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetCloseLoopErrDelegate C_TalonSRX_GetCloseLoopErr;

        public delegate CTR_Code C_TalonSRX_GetFeedbackDeviceSelectDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFeedbackDeviceSelectDelegate C_TalonSRX_GetFeedbackDeviceSelect;

        public delegate CTR_Code C_TalonSRX_GetModeSelectDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetModeSelectDelegate C_TalonSRX_GetModeSelect;

        public delegate CTR_Code C_TalonSRX_GetLimitSwitchEnDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetLimitSwitchEnDelegate C_TalonSRX_GetLimitSwitchEn;

        public delegate CTR_Code C_TalonSRX_GetLimitSwitchClosedForDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetLimitSwitchClosedForDelegate C_TalonSRX_GetLimitSwitchClosedFor;

        public delegate CTR_Code C_TalonSRX_GetLimitSwitchClosedRevDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetLimitSwitchClosedRevDelegate C_TalonSRX_GetLimitSwitchClosedRev;

        public delegate CTR_Code C_TalonSRX_GetSensorPositionDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetSensorPositionDelegate C_TalonSRX_GetSensorPosition;

        public delegate CTR_Code C_TalonSRX_GetSensorVelocityDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetSensorVelocityDelegate C_TalonSRX_GetSensorVelocity;

        public delegate CTR_Code C_TalonSRX_GetCurrentDelegate(CANTalonSafeHandle handle, ref double param);
        public static C_TalonSRX_GetCurrentDelegate C_TalonSRX_GetCurrent;

        public delegate CTR_Code C_TalonSRX_GetBrakeIsEnabledDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetBrakeIsEnabledDelegate C_TalonSRX_GetBrakeIsEnabled;

        public delegate CTR_Code C_TalonSRX_GetEncPositionDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetEncPositionDelegate C_TalonSRX_GetEncPosition;

        public delegate CTR_Code C_TalonSRX_GetEncVelDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetEncVelDelegate C_TalonSRX_GetEncVel;

        public delegate CTR_Code C_TalonSRX_GetEncIndexRiseEventsDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetEncIndexRiseEventsDelegate C_TalonSRX_GetEncIndexRiseEvents;

        public delegate CTR_Code C_TalonSRX_GetQuadApinDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetQuadApinDelegate C_TalonSRX_GetQuadApin;

        public delegate CTR_Code C_TalonSRX_GetQuadBpinDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetQuadBpinDelegate C_TalonSRX_GetQuadBpin;

        public delegate CTR_Code C_TalonSRX_GetQuadIdxpinDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetQuadIdxpinDelegate C_TalonSRX_GetQuadIdxpin;

        public delegate CTR_Code C_TalonSRX_GetAnalogInWithOvDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetAnalogInWithOvDelegate C_TalonSRX_GetAnalogInWithOv;

        public delegate CTR_Code C_TalonSRX_GetAnalogInVelDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetAnalogInVelDelegate C_TalonSRX_GetAnalogInVel;

        public delegate CTR_Code C_TalonSRX_GetTempDelegate(CANTalonSafeHandle handle, ref double param);
        public static C_TalonSRX_GetTempDelegate C_TalonSRX_GetTemp;

        public delegate CTR_Code C_TalonSRX_GetBatteryVDelegate(CANTalonSafeHandle handle, ref double param);
        public static C_TalonSRX_GetBatteryVDelegate C_TalonSRX_GetBatteryV;

        public delegate CTR_Code C_TalonSRX_GetResetCountDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetResetCountDelegate C_TalonSRX_GetResetCount;

        public delegate CTR_Code C_TalonSRX_GetResetFlagsDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetResetFlagsDelegate C_TalonSRX_GetResetFlags;

        public delegate CTR_Code C_TalonSRX_GetFirmVersDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetFirmVersDelegate C_TalonSRX_GetFirmVers;

        public delegate CTR_Code C_TalonSRX_GetPulseWidthPositionDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetPulseWidthPositionDelegate C_TalonSRX_GetPulseWidthPosition;

        public delegate CTR_Code C_TalonSRX_GetPulseWidthVelocityDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetPulseWidthVelocityDelegate C_TalonSRX_GetPulseWidthVelocity;

        public delegate CTR_Code C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate C_TalonSRX_GetPulseWidthRiseToRiseUs;

        public delegate CTR_Code C_TalonSRX_GetActTraj_IsValidDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetActTraj_IsValidDelegate C_TalonSRX_GetActTraj_IsValid;

        public delegate CTR_Code C_TalonSRX_GetActTraj_ProfileSlotSelectDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetActTraj_ProfileSlotSelectDelegate C_TalonSRX_GetActTraj_ProfileSlotSelect;

        public delegate CTR_Code C_TalonSRX_GetActTraj_VelOnlyDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetActTraj_VelOnlyDelegate C_TalonSRX_GetActTraj_VelOnly;

        public delegate CTR_Code C_TalonSRX_GetActTraj_IsLastDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetActTraj_IsLastDelegate C_TalonSRX_GetActTraj_IsLast;

        public delegate CTR_Code C_TalonSRX_GetOutputTypeDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetOutputTypeDelegate C_TalonSRX_GetOutputType;

        public delegate CTR_Code C_TalonSRX_GetHasUnderrunDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetHasUnderrunDelegate C_TalonSRX_GetHasUnderrun;

        public delegate CTR_Code C_TalonSRX_GetIsUnderrunDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetIsUnderrunDelegate C_TalonSRX_GetIsUnderrun;

        public delegate CTR_Code C_TalonSRX_GetNextIDDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetNextIDDelegate C_TalonSRX_GetNextID;

        public delegate CTR_Code C_TalonSRX_GetBufferIsFullDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetBufferIsFullDelegate C_TalonSRX_GetBufferIsFull;

        public delegate CTR_Code C_TalonSRX_GetCountDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetCountDelegate C_TalonSRX_GetCount;

        public delegate CTR_Code C_TalonSRX_GetActTraj_VelocityDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetActTraj_VelocityDelegate C_TalonSRX_GetActTraj_Velocity;

        public delegate CTR_Code C_TalonSRX_GetActTraj_PositionDelegate(CANTalonSafeHandle handle, ref int param);
        public static C_TalonSRX_GetActTraj_PositionDelegate C_TalonSRX_GetActTraj_Position;

        public delegate CTR_Code C_TalonSRX_SetDemandDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetDemandDelegate C_TalonSRX_SetDemand;

        public delegate CTR_Code C_TalonSRX_SetOverrideLimitSwitchEnDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetOverrideLimitSwitchEnDelegate C_TalonSRX_SetOverrideLimitSwitchEn;

        public delegate CTR_Code C_TalonSRX_SetFeedbackDeviceSelectDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetFeedbackDeviceSelectDelegate C_TalonSRX_SetFeedbackDeviceSelect;

        public delegate CTR_Code C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate C_TalonSRX_SetRevMotDuringCloseLoopEn;

        public delegate CTR_Code C_TalonSRX_SetOverrideBrakeTypeDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetOverrideBrakeTypeDelegate C_TalonSRX_SetOverrideBrakeType;

        public delegate CTR_Code C_TalonSRX_SetModeSelectDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetModeSelectDelegate C_TalonSRX_SetModeSelect;

        public delegate CTR_Code C_TalonSRX_SetProfileSlotSelectDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetProfileSlotSelectDelegate C_TalonSRX_SetProfileSlotSelect;

        public delegate CTR_Code C_TalonSRX_SetRampThrottleDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetRampThrottleDelegate C_TalonSRX_SetRampThrottle;

        public delegate CTR_Code C_TalonSRX_SetRevFeedbackSensorDelegate(CANTalonSafeHandle handle, int param);
        public static C_TalonSRX_SetRevFeedbackSensorDelegate C_TalonSRX_SetRevFeedbackSensor;


    }
}
