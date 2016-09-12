using System;
using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALCanTalonSRX
    {
        static HALCanTalonSRX()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALCanTalonSRX>(LibraryLoaderHolder.NativeLoader);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate IntPtr C_TalonSRX_Create3Delegate(int deviceNumber, int controlPeriodMs, int enablePeriodMs);
        [NativeDelegate] public static C_TalonSRX_Create3Delegate C_TalonSRX_Create3;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void C_TalonSRX_DestroyDelegate(IntPtr handle);
        [NativeDelegate] public static C_TalonSRX_DestroyDelegate C_TalonSRX_Destroy;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void C_TalonSRX_SetDelegate(IntPtr handle, double value);
        [NativeDelegate] public static C_TalonSRX_SetDelegate C_TalonSRX_Set;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetParamDelegate(IntPtr handle, int paramEnum, double value);
        [NativeDelegate] public static C_TalonSRX_SetParamDelegate C_TalonSRX_SetParam;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_RequestParamDelegate(IntPtr handle, int paramEnum);
        [NativeDelegate] public static C_TalonSRX_RequestParamDelegate C_TalonSRX_RequestParam;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetParamResponseDelegate(IntPtr handle, int paramEnum, ref double value);
        [NativeDelegate] public static C_TalonSRX_GetParamResponseDelegate C_TalonSRX_GetParamResponse;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetParamResponseInt32Delegate(IntPtr handle, int paramEnum, ref int value);
        [NativeDelegate] public static C_TalonSRX_GetParamResponseInt32Delegate C_TalonSRX_GetParamResponseInt32;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetPgainDelegate(IntPtr handle, int slotIdx, double gain);
        [NativeDelegate] public static C_TalonSRX_SetPgainDelegate C_TalonSRX_SetPgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetIgainDelegate(IntPtr handle, int slotIdx, double gain);
        [NativeDelegate] public static C_TalonSRX_SetIgainDelegate C_TalonSRX_SetIgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetDgainDelegate(IntPtr handle, int slotIdx, double gain);
        [NativeDelegate] public static C_TalonSRX_SetDgainDelegate C_TalonSRX_SetDgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetFgainDelegate(IntPtr handle, int slotIdx, double gain);
        [NativeDelegate] public static C_TalonSRX_SetFgainDelegate C_TalonSRX_SetFgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetIzoneDelegate(IntPtr handle, int slotIdx, int zone);
        [NativeDelegate] public static C_TalonSRX_SetIzoneDelegate C_TalonSRX_SetIzone;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetCloseLoopRampRateDelegate(IntPtr handle, int slotIdx, int closeLoopRampRate);
        [NativeDelegate] public static C_TalonSRX_SetCloseLoopRampRateDelegate C_TalonSRX_SetCloseLoopRampRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetVoltageCompensationRateDelegate(IntPtr handle, double voltagePerMs);
        [NativeDelegate] public static C_TalonSRX_SetVoltageCompensationRateDelegate C_TalonSRX_SetVoltageCompensationRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetSensorPositionDelegate(IntPtr handle, int pos);
        [NativeDelegate] public static C_TalonSRX_SetSensorPositionDelegate C_TalonSRX_SetSensorPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetForwardSoftLimitDelegate(IntPtr handle, int forwardLimit);
        [NativeDelegate] public static C_TalonSRX_SetForwardSoftLimitDelegate C_TalonSRX_SetForwardSoftLimit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetReverseSoftLimitDelegate(IntPtr handle, int reverseLimit);
        [NativeDelegate] public static C_TalonSRX_SetReverseSoftLimitDelegate C_TalonSRX_SetReverseSoftLimit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetForwardSoftEnableDelegate(IntPtr handle, int enable);
        [NativeDelegate] public static C_TalonSRX_SetForwardSoftEnableDelegate C_TalonSRX_SetForwardSoftEnable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetReverseSoftEnableDelegate(IntPtr handle, int enable);
        [NativeDelegate] public static C_TalonSRX_SetReverseSoftEnableDelegate C_TalonSRX_SetReverseSoftEnable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetPgainDelegate(IntPtr handle, int slotIdx, ref double gain);
        [NativeDelegate] public static C_TalonSRX_GetPgainDelegate C_TalonSRX_GetPgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetIgainDelegate(IntPtr handle, int slotIdx, ref double gain);
        [NativeDelegate] public static C_TalonSRX_GetIgainDelegate C_TalonSRX_GetIgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetDgainDelegate(IntPtr handle, int slotIdx, ref double gain);
        [NativeDelegate] public static C_TalonSRX_GetDgainDelegate C_TalonSRX_GetDgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFgainDelegate(IntPtr handle, int slotIdx, ref double gain);
        [NativeDelegate] public static C_TalonSRX_GetFgainDelegate C_TalonSRX_GetFgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetIzoneDelegate(IntPtr handle, int slotIdx, ref int zone);
        [NativeDelegate] public static C_TalonSRX_GetIzoneDelegate C_TalonSRX_GetIzone;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetCloseLoopRampRateDelegate(IntPtr handle, int slotIdx, ref int closeLoopRampRate);
        [NativeDelegate] public static C_TalonSRX_GetCloseLoopRampRateDelegate C_TalonSRX_GetCloseLoopRampRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetVoltageCompensationRateDelegate(IntPtr handle, ref double voltagePerMs);
        [NativeDelegate] public static C_TalonSRX_GetVoltageCompensationRateDelegate C_TalonSRX_GetVoltageCompensationRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetForwardSoftLimitDelegate(IntPtr handle, ref int forwardLimit);
        [NativeDelegate] public static C_TalonSRX_GetForwardSoftLimitDelegate C_TalonSRX_GetForwardSoftLimit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetReverseSoftLimitDelegate(IntPtr handle, ref int reverseLimit);
        [NativeDelegate] public static C_TalonSRX_GetReverseSoftLimitDelegate C_TalonSRX_GetReverseSoftLimit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetForwardSoftEnableDelegate(IntPtr handle, ref int enable);
        [NativeDelegate] public static C_TalonSRX_GetForwardSoftEnableDelegate C_TalonSRX_GetForwardSoftEnable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetReverseSoftEnableDelegate(IntPtr handle, ref int enable);
        [NativeDelegate] public static C_TalonSRX_GetReverseSoftEnableDelegate C_TalonSRX_GetReverseSoftEnable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetPulseWidthRiseToFallUsDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetPulseWidthRiseToFallUsDelegate C_TalonSRX_GetPulseWidthRiseToFallUs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_IsPulseWidthSensorPresentDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_IsPulseWidthSensorPresentDelegate C_TalonSRX_IsPulseWidthSensorPresent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetModeSelect2Delegate(IntPtr handle, int modeSelect, int demand);
        [NativeDelegate] public static C_TalonSRX_SetModeSelect2Delegate C_TalonSRX_SetModeSelect2;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetStatusFrameRateDelegate(IntPtr handle, int frameEnum, int periodMs);
        [NativeDelegate] public static C_TalonSRX_SetStatusFrameRateDelegate C_TalonSRX_SetStatusFrameRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_ClearStickyFaultsDelegate(IntPtr handle);
        [NativeDelegate] public static C_TalonSRX_ClearStickyFaultsDelegate C_TalonSRX_ClearStickyFaults;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void C_TalonSRX_ChangeMotionControlFramePeriodDelegate(IntPtr handle, int periodMs);
        [NativeDelegate] public static C_TalonSRX_ChangeMotionControlFramePeriodDelegate C_TalonSRX_ChangeMotionControlFramePeriod;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void C_TalonSRX_ClearMotionProfileTrajectoriesDelegate(IntPtr handle);
        [NativeDelegate] public static C_TalonSRX_ClearMotionProfileTrajectoriesDelegate C_TalonSRX_ClearMotionProfileTrajectories;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int C_TalonSRX_GetMotionProfileTopLevelBufferCountDelegate(IntPtr handle);
        [NativeDelegate] public static C_TalonSRX_GetMotionProfileTopLevelBufferCountDelegate C_TalonSRX_GetMotionProfileTopLevelBufferCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int C_TalonSRX_IsMotionProfileTopLevelBufferFullDelegate(IntPtr handle);
        [NativeDelegate] public static C_TalonSRX_IsMotionProfileTopLevelBufferFullDelegate C_TalonSRX_IsMotionProfileTopLevelBufferFull;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_PushMotionProfileTrajectoryDelegate(IntPtr handle, int targPos, int targVel, int profileSlotSelect, int timeDurMs, int velOnly, int isLastPoint, int zeroPos);
        [NativeDelegate] public static C_TalonSRX_PushMotionProfileTrajectoryDelegate C_TalonSRX_PushMotionProfileTrajectory;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void C_TalonSRX_ProcessMotionProfileBufferDelegate(IntPtr handle);
        [NativeDelegate] public static C_TalonSRX_ProcessMotionProfileBufferDelegate C_TalonSRX_ProcessMotionProfileBuffer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetMotionProfileStatusDelegate(IntPtr handle, ref int flags, ref int profileSlotSelect, ref int targPos, ref int targVel, ref int topBufferRemaining, ref int topBufferCnt, ref int btmBufferCnt, ref int outputEnable);
        [NativeDelegate] public static C_TalonSRX_GetMotionProfileStatusDelegate C_TalonSRX_GetMotionProfileStatus;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFault_OverTempDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFault_OverTempDelegate C_TalonSRX_GetFault_OverTemp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFault_UnderVoltageDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFault_UnderVoltageDelegate C_TalonSRX_GetFault_UnderVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFault_ForLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFault_ForLimDelegate C_TalonSRX_GetFault_ForLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFault_RevLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFault_RevLimDelegate C_TalonSRX_GetFault_RevLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFault_HardwareFailureDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFault_HardwareFailureDelegate C_TalonSRX_GetFault_HardwareFailure;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFault_ForSoftLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFault_ForSoftLimDelegate C_TalonSRX_GetFault_ForSoftLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFault_RevSoftLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFault_RevSoftLimDelegate C_TalonSRX_GetFault_RevSoftLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetStckyFault_OverTempDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetStckyFault_OverTempDelegate C_TalonSRX_GetStckyFault_OverTemp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetStckyFault_UnderVoltageDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetStckyFault_UnderVoltageDelegate C_TalonSRX_GetStckyFault_UnderVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetStckyFault_ForLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetStckyFault_ForLimDelegate C_TalonSRX_GetStckyFault_ForLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetStckyFault_RevLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetStckyFault_RevLimDelegate C_TalonSRX_GetStckyFault_RevLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetStckyFault_ForSoftLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetStckyFault_ForSoftLimDelegate C_TalonSRX_GetStckyFault_ForSoftLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetStckyFault_RevSoftLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetStckyFault_RevSoftLimDelegate C_TalonSRX_GetStckyFault_RevSoftLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetAppliedThrottleDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetAppliedThrottleDelegate C_TalonSRX_GetAppliedThrottle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetCloseLoopErrDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetCloseLoopErrDelegate C_TalonSRX_GetCloseLoopErr;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFeedbackDeviceSelectDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFeedbackDeviceSelectDelegate C_TalonSRX_GetFeedbackDeviceSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetModeSelectDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetModeSelectDelegate C_TalonSRX_GetModeSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetLimitSwitchEnDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetLimitSwitchEnDelegate C_TalonSRX_GetLimitSwitchEn;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetLimitSwitchClosedForDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetLimitSwitchClosedForDelegate C_TalonSRX_GetLimitSwitchClosedFor;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetLimitSwitchClosedRevDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetLimitSwitchClosedRevDelegate C_TalonSRX_GetLimitSwitchClosedRev;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetSensorPositionDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetSensorPositionDelegate C_TalonSRX_GetSensorPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetSensorVelocityDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetSensorVelocityDelegate C_TalonSRX_GetSensorVelocity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetCurrentDelegate(IntPtr handle, ref double param);
        [NativeDelegate] public static C_TalonSRX_GetCurrentDelegate C_TalonSRX_GetCurrent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetBrakeIsEnabledDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetBrakeIsEnabledDelegate C_TalonSRX_GetBrakeIsEnabled;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetEncPositionDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetEncPositionDelegate C_TalonSRX_GetEncPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetEncVelDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetEncVelDelegate C_TalonSRX_GetEncVel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetEncIndexRiseEventsDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetEncIndexRiseEventsDelegate C_TalonSRX_GetEncIndexRiseEvents;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetQuadApinDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetQuadApinDelegate C_TalonSRX_GetQuadApin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetQuadBpinDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetQuadBpinDelegate C_TalonSRX_GetQuadBpin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetQuadIdxpinDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetQuadIdxpinDelegate C_TalonSRX_GetQuadIdxpin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetAnalogInWithOvDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetAnalogInWithOvDelegate C_TalonSRX_GetAnalogInWithOv;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetAnalogInVelDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetAnalogInVelDelegate C_TalonSRX_GetAnalogInVel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetTempDelegate(IntPtr handle, ref double param);
        [NativeDelegate] public static C_TalonSRX_GetTempDelegate C_TalonSRX_GetTemp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetBatteryVDelegate(IntPtr handle, ref double param);
        [NativeDelegate] public static C_TalonSRX_GetBatteryVDelegate C_TalonSRX_GetBatteryV;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetResetCountDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetResetCountDelegate C_TalonSRX_GetResetCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetResetFlagsDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetResetFlagsDelegate C_TalonSRX_GetResetFlags;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetFirmVersDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetFirmVersDelegate C_TalonSRX_GetFirmVers;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetPulseWidthPositionDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetPulseWidthPositionDelegate C_TalonSRX_GetPulseWidthPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetPulseWidthVelocityDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetPulseWidthVelocityDelegate C_TalonSRX_GetPulseWidthVelocity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetPulseWidthRiseToRiseUsDelegate C_TalonSRX_GetPulseWidthRiseToRiseUs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetActTraj_IsValidDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetActTraj_IsValidDelegate C_TalonSRX_GetActTraj_IsValid;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetActTraj_ProfileSlotSelectDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetActTraj_ProfileSlotSelectDelegate C_TalonSRX_GetActTraj_ProfileSlotSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetActTraj_VelOnlyDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetActTraj_VelOnlyDelegate C_TalonSRX_GetActTraj_VelOnly;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetActTraj_IsLastDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetActTraj_IsLastDelegate C_TalonSRX_GetActTraj_IsLast;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetOutputTypeDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetOutputTypeDelegate C_TalonSRX_GetOutputType;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetHasUnderrunDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetHasUnderrunDelegate C_TalonSRX_GetHasUnderrun;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetIsUnderrunDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetIsUnderrunDelegate C_TalonSRX_GetIsUnderrun;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetNextIDDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetNextIDDelegate C_TalonSRX_GetNextID;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetBufferIsFullDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetBufferIsFullDelegate C_TalonSRX_GetBufferIsFull;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetCountDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetCountDelegate C_TalonSRX_GetCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetActTraj_VelocityDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetActTraj_VelocityDelegate C_TalonSRX_GetActTraj_Velocity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_GetActTraj_PositionDelegate(IntPtr handle, ref int param);
        [NativeDelegate] public static C_TalonSRX_GetActTraj_PositionDelegate C_TalonSRX_GetActTraj_Position;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetDemandDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetDemandDelegate C_TalonSRX_SetDemand;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetOverrideLimitSwitchEnDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetOverrideLimitSwitchEnDelegate C_TalonSRX_SetOverrideLimitSwitchEn;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetFeedbackDeviceSelectDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetFeedbackDeviceSelectDelegate C_TalonSRX_SetFeedbackDeviceSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate C_TalonSRX_SetRevMotDuringCloseLoopEn;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetOverrideBrakeTypeDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetOverrideBrakeTypeDelegate C_TalonSRX_SetOverrideBrakeType;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetModeSelectDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetModeSelectDelegate C_TalonSRX_SetModeSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetProfileSlotSelectDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetProfileSlotSelectDelegate C_TalonSRX_SetProfileSlotSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetRampThrottleDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetRampThrottleDelegate C_TalonSRX_SetRampThrottle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate CTR_Code C_TalonSRX_SetRevFeedbackSensorDelegate(IntPtr handle, int param);
        [NativeDelegate] public static C_TalonSRX_SetRevFeedbackSensorDelegate C_TalonSRX_SetRevFeedbackSensor;


    }
}