using System;
using System.Runtime.InteropServices;
using HAL.Base;
using static HAL.Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
#pragma warning disable 1591


namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal class HALCanTalonSRX
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALCanTalonSRX.C_TalonSRX_Create3 = c_TalonSRX_Create;
            Base.HALCanTalonSRX.C_TalonSRX_Destroy = c_TalonSRX_Destroy;
            Base.HALCanTalonSRX.C_TalonSRX_SetParam = c_TalonSRX_SetParam;
            Base.HALCanTalonSRX.C_TalonSRX_RequestParam = c_TalonSRX_RequestParam;
            Base.HALCanTalonSRX.C_TalonSRX_GetParamResponse = c_TalonSRX_GetParamResponse;
            Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32 = c_TalonSRX_GetParamResponseInt32;
            Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRate = c_TalonSRX_SetStatusFrameRate;
            Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaults = c_TalonSRX_ClearStickyFaults;
            Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTemp = c_TalonSRX_GetFault_OverTemp;
            Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltage = c_TalonSRX_GetFault_UnderVoltage;
            Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLim = c_TalonSRX_GetFault_ForLim;
            Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLim = c_TalonSRX_GetFault_RevLim;
            Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailure = c_TalonSRX_GetFault_HardwareFailure;
            Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLim = c_TalonSRX_GetFault_ForSoftLim;
            Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLim = c_TalonSRX_GetFault_RevSoftLim;
            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTemp = c_TalonSRX_GetStckyFault_OverTemp;
            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltage = c_TalonSRX_GetStckyFault_UnderVoltage;
            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLim = c_TalonSRX_GetStckyFault_ForLim;
            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLim = c_TalonSRX_GetStckyFault_RevLim;
            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLim = c_TalonSRX_GetStckyFault_ForSoftLim;
            Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLim = c_TalonSRX_GetStckyFault_RevSoftLim;
            Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottle = c_TalonSRX_GetAppliedThrottle;
            Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErr = c_TalonSRX_GetCloseLoopErr;
            Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelect = c_TalonSRX_GetFeedbackDeviceSelect;
            Base.HALCanTalonSRX.C_TalonSRX_GetModeSelect = c_TalonSRX_GetModeSelect;
            Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEn = c_TalonSRX_GetLimitSwitchEn;
            Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedFor = c_TalonSRX_GetLimitSwitchClosedFor;
            Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRev = c_TalonSRX_GetLimitSwitchClosedRev;
            Base.HALCanTalonSRX.C_TalonSRX_GetSensorPosition = c_TalonSRX_GetSensorPosition;
            Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocity = c_TalonSRX_GetSensorVelocity;
            Base.HALCanTalonSRX.C_TalonSRX_GetCurrent = c_TalonSRX_GetCurrent;
            Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabled = c_TalonSRX_GetBrakeIsEnabled;
            Base.HALCanTalonSRX.C_TalonSRX_GetEncPosition = c_TalonSRX_GetEncPosition;
            Base.HALCanTalonSRX.C_TalonSRX_GetEncVel = c_TalonSRX_GetEncVel;
            Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEvents = c_TalonSRX_GetEncIndexRiseEvents;
            Base.HALCanTalonSRX.C_TalonSRX_GetQuadApin = c_TalonSRX_GetQuadApin;
            Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpin = c_TalonSRX_GetQuadBpin;
            Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpin = c_TalonSRX_GetQuadIdxpin;
            Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOv = c_TalonSRX_GetAnalogInWithOv;
            Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVel = c_TalonSRX_GetAnalogInVel;
            Base.HALCanTalonSRX.C_TalonSRX_GetTemp = c_TalonSRX_GetTemp;
            Base.HALCanTalonSRX.C_TalonSRX_GetBatteryV = c_TalonSRX_GetBatteryV;
            Base.HALCanTalonSRX.C_TalonSRX_GetResetCount = c_TalonSRX_GetResetCount;
            Base.HALCanTalonSRX.C_TalonSRX_GetResetFlags = c_TalonSRX_GetResetFlags;
            Base.HALCanTalonSRX.C_TalonSRX_GetFirmVers = c_TalonSRX_GetFirmVers;
            Base.HALCanTalonSRX.C_TalonSRX_SetDemand = c_TalonSRX_SetDemand;
            Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEn = c_TalonSRX_SetOverrideLimitSwitchEn;
            Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelect = c_TalonSRX_SetFeedbackDeviceSelect;
            Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEn = c_TalonSRX_SetRevMotDuringCloseLoopEn;
            Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeType = c_TalonSRX_SetOverrideBrakeType;
            Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect = c_TalonSRX_SetModeSelect;
            Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2 = c_TalonSRX_SetModeSelect2;
            Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelect = c_TalonSRX_SetProfileSlotSelect;
            Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottle = c_TalonSRX_SetRampThrottle;
            Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensor = c_TalonSRX_SetRevFeedbackSensor;
            Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPosition = CTalonSRXGetPulseWidthPosition;
            Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocity = CTalonSRXGetPulseWidthVelocity;
            Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUs = CTalonSRXGetPulseRiseToFallUs;
            Base.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUs = CTalonSRXGetPulseRiseToRiseUs;
            Base.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresent = CTalonSRXIsPulseWidthSensorPresent;
            Base.HALCanTalonSRX.C_TalonSRX_Set = c_TalonSRX_Set;
            Base.HALCanTalonSRX.C_TalonSRX_SetPgain = c_TalonSRX_SetPgain;
            Base.HALCanTalonSRX.C_TalonSRX_SetIgain = c_TalonSRX_SetIgain;
            Base.HALCanTalonSRX.C_TalonSRX_SetDgain = c_TalonSRX_SetDgain;
            Base.HALCanTalonSRX.C_TalonSRX_SetFgain = c_TalonSRX_SetFgain;
            Base.HALCanTalonSRX.C_TalonSRX_SetIzone = c_TalonSRX_SetIzone;
            Base.HALCanTalonSRX.C_TalonSRX_SetCloseLoopRampRate = c_TalonSRX_SetCloseLoopRampRate;
            Base.HALCanTalonSRX.C_TalonSRX_SetVoltageCompensationRate = c_TalonSRX_SetVoltageCompensationRate;
            Base.HALCanTalonSRX.C_TalonSRX_SetSensorPosition = c_TalonSRX_SetSensorPosition;
            Base.HALCanTalonSRX.C_TalonSRX_SetForwardSoftLimit = c_TalonSRX_SetForwardSoftLimit;
            Base.HALCanTalonSRX.C_TalonSRX_SetReverseSoftLimit = c_TalonSRX_SetReverseSoftLimit;
            Base.HALCanTalonSRX.C_TalonSRX_SetForwardSoftEnable = c_TalonSRX_SetForwardSoftEnable;
            Base.HALCanTalonSRX.C_TalonSRX_SetReverseSoftEnable = c_TalonSRX_SetReverseSoftEnable;
            Base.HALCanTalonSRX.C_TalonSRX_GetPgain = c_TalonSRX_GetPgain;
            Base.HALCanTalonSRX.C_TalonSRX_GetIgain = c_TalonSRX_GetIgain;
            Base.HALCanTalonSRX.C_TalonSRX_GetDgain = c_TalonSRX_GetDgain;
            Base.HALCanTalonSRX.C_TalonSRX_GetFgain = c_TalonSRX_GetFgain;
            Base.HALCanTalonSRX.C_TalonSRX_GetIzone = c_TalonSRX_GetIzone;
            Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopRampRate = c_TalonSRX_GetCloseLoopRampRate;
            Base.HALCanTalonSRX.C_TalonSRX_GetVoltageCompensationRate = c_TalonSRX_GetVoltageCompensationRate;
            Base.HALCanTalonSRX.C_TalonSRX_GetForwardSoftLimit = c_TalonSRX_GetForwardSoftLimit;
            Base.HALCanTalonSRX.C_TalonSRX_GetReverseSoftLimit = c_TalonSRX_GetReverseSoftLimit;
            Base.HALCanTalonSRX.C_TalonSRX_GetForwardSoftEnable = c_TalonSRX_GetForwardSoftEnable;
            Base.HALCanTalonSRX.C_TalonSRX_GetReverseSoftEnable = c_TalonSRX_GetReverseSoftEnable;
            Base.HALCanTalonSRX.C_TalonSRX_ChangeMotionControlFramePeriod = c_TalonSRX_ChangeMotionControlFramePeriod;
            Base.HALCanTalonSRX.C_TalonSRX_ClearMotionProfileTrajectories = c_TalonSRX_ClearMotionProfileTrajectories;
            Base.HALCanTalonSRX.C_TalonSRX_GetMotionProfileTopLevelBufferCount = c_TalonSRX_GetMotionProfileTopLevelBufferCount;
            Base.HALCanTalonSRX.C_TalonSRX_IsMotionProfileTopLevelBufferFull = c_TalonSRX_IsMotionProfileTopLevelBufferFull;
            Base.HALCanTalonSRX.C_TalonSRX_PushMotionProfileTrajectory = c_TalonSRX_PushMotionProfileTrajectory;
            Base.HALCanTalonSRX.C_TalonSRX_ProcessMotionProfileBuffer = c_TalonSRX_ProcessMotionProfileBuffer;
            Base.HALCanTalonSRX.C_TalonSRX_GetMotionProfileStatus = c_TalonSRX_GetMotionProfileStatus;
            Base.HALCanTalonSRX.C_TalonSRX_GetActTraj_IsValid = c_TalonSRX_GetActTraj_IsValid;
            Base.HALCanTalonSRX.C_TalonSRX_GetActTraj_ProfileSlotSelect = c_TalonSRX_GetActTraj_ProfileSlotSelect;
            Base.HALCanTalonSRX.C_TalonSRX_GetActTraj_VelOnly = c_TalonSRX_GetActTraj_VelOnly;
            Base.HALCanTalonSRX.C_TalonSRX_GetActTraj_IsLast = c_TalonSRX_GetActTraj_IsLast;
            Base.HALCanTalonSRX.C_TalonSRX_GetOutputType = c_TalonSRX_GetOutputType;
            Base.HALCanTalonSRX.C_TalonSRX_GetHasUnderrun = c_TalonSRX_GetHasUnderrun;
            Base.HALCanTalonSRX.C_TalonSRX_GetIsUnderrun = c_TalonSRX_GetIsUnderrun;
            Base.HALCanTalonSRX.C_TalonSRX_GetNextID = c_TalonSRX_GetNextID;
            Base.HALCanTalonSRX.C_TalonSRX_GetBufferIsFull = c_TalonSRX_GetBufferIsFull;
            Base.HALCanTalonSRX.C_TalonSRX_GetCount = c_TalonSRX_GetCount;
            Base.HALCanTalonSRX.C_TalonSRX_GetActTraj_Velocity = c_TalonSRX_GetActTraj_Velocity;
            Base.HALCanTalonSRX.C_TalonSRX_GetActTraj_Position = c_TalonSRX_GetActTraj_Position;
        }

        [CalledSimFunction]
        public static IntPtr c_TalonSRX_Create(int deviceNumber, int controlPeriodMs, int enablePeriodMs)
        {
            if (!InitializeCanTalon(deviceNumber))
            {
                throw new ArgumentOutOfRangeException(nameof(deviceNumber), "Device Already Allocated.");
            }


            TalonSRX srx = new TalonSRX { deviceNumber = deviceNumber };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(srx));
            Marshal.StructureToPtr(srx, ptr, true);
            GetCanTalon(deviceNumber).ResetData();
            return ptr;
        }

        [CalledSimFunction]
        public static void c_TalonSRX_Destroy(IntPtr handle)
        {
            RemoveCanTalon(PortConverters.GetTalonSRX(handle));
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetParam(IntPtr handle, int paramEnum, double value)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).SetParam((Base.HALCanTalonSRX.ParamID)paramEnum, value);
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_RequestParam(IntPtr handle, int paramEnum)
        {
            try
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).GetParam((Base.HALCanTalonSRX.ParamID)paramEnum);
                return CTR_Code.CTR_OKAY;
            }
            catch (ArgumentOutOfRangeException)
            {
                return CTR_Code.CTR_InvalidParamValue;
            }
        }



        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetParamResponse(IntPtr handle, int paramEnum, ref double value)
        {
            try
            {
                value = GetCanTalon(PortConverters.GetTalonSRX(handle)).GetParam((Base.HALCanTalonSRX.ParamID)paramEnum);
                return CTR_Code.CTR_OKAY;
            }
            catch (ArgumentOutOfRangeException)
            {
                value = 0.0;
                return CTR_Code.CTR_InvalidParamValue;
            }
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetParamResponseInt32(IntPtr handle, int paramEnum, ref int value)
        {
            try
            {
                value = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).GetParam((Base.HALCanTalonSRX.ParamID)paramEnum);
                return CTR_Code.CTR_OKAY;
            }
            catch (ArgumentOutOfRangeException)
            {
                value = 0;
                return CTR_Code.CTR_InvalidParamValue;
            }
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetStatusFrameRate(IntPtr handle, int frameEnum, int periodMs)
        {
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_ClearStickyFaults(IntPtr handle)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_OverTemp = 0;
            GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_UnderVoltage = 0;
            GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_ForLim = 0;
            GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_RevLim = 0;
            GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_ForSoftLim = 0;
            GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_ForSoftLim = 0;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_OverTemp(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).Fault_OverTemp;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_UnderVoltage(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).Fault_UnderVoltage;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_ForLim(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).Fault_ForLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_RevLim(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).Fault_RevLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_HardwareFailure(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).Fault_HardwareFailure;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_ForSoftLim(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).Fault_ForSoftLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_RevSoftLim(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).Fault_RevSoftLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_OverTemp(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_OverTemp;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_UnderVoltage(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_UnderVoltage;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_ForLim(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_ForLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_RevLim(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_RevLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_ForSoftLim(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_ForSoftLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_RevSoftLim(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).StckyFault_RevSoftLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAppliedThrottle(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).AppliedThrottle;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetCloseLoopErr(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).CloseLoopErr;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFeedbackDeviceSelect(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).FeedbackDeviceSelect;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetModeSelect(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).ModeSelect;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchEn(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).LimitSwitchEn;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchClosedFor(IntPtr handle, ref int param)
        {
            param = GetCanTalon(PortConverters.GetTalonSRX(handle)).LimitSwitchClosedFor ? 1 : 0;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchClosedRev(IntPtr handle, ref int param)
        {
            param = GetCanTalon(PortConverters.GetTalonSRX(handle)).LimitSwitchClosedRev ? 1 : 0;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetSensorPosition(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).SensorPosition;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetSensorVelocity(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).SensorVelocity;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetCurrent(IntPtr handle, ref double param)
        {
            param = GetCanTalon(PortConverters.GetTalonSRX(handle)).Current;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetBrakeIsEnabled(IntPtr handle, ref int param)
        {
            param = GetCanTalon(PortConverters.GetTalonSRX(handle)).BrakeIsEnabled ? 1 : 0;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncPosition(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).EncPosition;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncVel(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).EncVel;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncIndexRiseEvents(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).EncIndexRiseEvents;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadApin(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).QuadApin;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadBpin(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).QuadBpin;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadIdxpin(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).QuadIdxpin;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAnalogInWithOv(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).AnalogInWithOv;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAnalogInVel(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).AnalogInVel;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetTemp(IntPtr handle, ref double param)
        {
            param = GetCanTalon(PortConverters.GetTalonSRX(handle)).Temp;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetBatteryV(IntPtr handle, ref double param)
        {
            param = GetCanTalon(PortConverters.GetTalonSRX(handle)).BatteryV;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetResetCount(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).ResetCount;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetResetFlags(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).ResetFlags;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFirmVers(IntPtr handle, ref int param)
        {
            param = (int)GetCanTalon(PortConverters.GetTalonSRX(handle)).FirmVers;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetDemand(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).Demand = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetOverrideLimitSwitchEn(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).OverrideLimitSwitch = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetFeedbackDeviceSelect(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).FeedbackDeviceSelect = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRevMotDuringCloseLoopEn(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).RevMotDuringCloseLoopEn = param != 0;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetOverrideBrakeType(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).OverrideBrakeType = param;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetModeSelect(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).ModeSelect = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetModeSelect2(IntPtr handle, int modeSelect, int demand)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).ModeSelect = modeSelect;
            GetCanTalon(PortConverters.GetTalonSRX(handle)).Demand = demand;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetProfileSlotSelect(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileSlotSelect = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRampThrottle(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).RampThrottle = param;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRevFeedbackSensor(IntPtr handle, int param)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).RevFeedbackSensor = param != 0;
            return CTR_Code.CTR_OKAY;
        }

        public static void c_TalonSRX_Set(IntPtr handle, double value)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).PercentVBusValue = value;
        }

        public static CTR_Code c_TalonSRX_SetPgain(IntPtr handle, int slotIdx, double gain)
        {
            if (slotIdx == 0)
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_P = gain;
            }
            else
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_P = gain;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetIgain(IntPtr handle, int slotIdx, double gain)
        {
            if (slotIdx == 0)
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_I = gain;
            }
            else
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_I = gain;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetDgain(IntPtr handle, int slotIdx, double gain)
        {
            if (slotIdx == 0)
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_D = gain;
            }
            else
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_D = gain;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetFgain(IntPtr handle, int slotIdx, double gain)
        {
            if (slotIdx == 0)
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_F = gain;
            }
            else
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_F = gain;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetIzone(IntPtr handle, int slotIdx, int zone)
        {
            if (slotIdx == 0)
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_IZone = zone;
            }
            else
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_IZone = zone;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetCloseLoopRampRate(IntPtr handle, int slotIdx, int closeLoopRampRate)
        {
            if (slotIdx == 0)
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_CloseLoopRampRate = closeLoopRampRate;
            }
            else
            {
                GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_CloseLoopRampRate = closeLoopRampRate;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetVoltageCompensationRate(IntPtr handle, double voltagePerMs)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).VoltageCompensationRate = voltagePerMs;
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetSensorPosition(IntPtr handle, int pos)
        {
            GetCanTalon(PortConverters.GetTalonSRX(handle)).SensorPosition = pos;
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetForwardSoftLimit(IntPtr handle, int forwardLimit)
        {
            //TODO: Add This
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetReverseSoftLimit(IntPtr handle, int reverseLimit)
        {
            //TODO: Add This
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetForwardSoftEnable(IntPtr handle, int enable)
        {
            //TODO: Add This
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_SetReverseSoftEnable(IntPtr handle, int enable)
        {
            //TODO: Add This
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetPgain(IntPtr handle, int slotIdx, ref double gain)
        {
            if (slotIdx == 0)
            {
                gain = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_P;
            }
            else
            {
                gain = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_P;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetIgain(IntPtr handle, int slotIdx, ref double gain)
        {
            if (slotIdx == 0)
            {
                gain = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_I;
            }
            else
            {
                gain = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_I;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetDgain(IntPtr handle, int slotIdx, ref double gain)
        {
            if (slotIdx == 0)
            {
                gain = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_D;
            }
            else
            {
                gain = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_D;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetFgain(IntPtr handle, int slotIdx, ref double gain)
        {
            if (slotIdx == 0)
            {
                gain = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_F;
            }
            else
            {
                gain = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_F;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetIzone(IntPtr handle, int slotIdx, ref int zone)
        {
            if (slotIdx == 0)
            {
                zone = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_IZone;
            }
            else
            {
                zone = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_IZone;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetCloseLoopRampRate(IntPtr handle, int slotIdx, ref int closeLoopRampRate)
        {
            if (slotIdx == 0)
            {
                closeLoopRampRate = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot0_CloseLoopRampRate;
            }
            else
            {
                closeLoopRampRate = GetCanTalon(PortConverters.GetTalonSRX(handle)).ProfileParamSlot1_CloseLoopRampRate;
            }
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetVoltageCompensationRate(IntPtr handle, ref double voltagePerMs)
        {
            voltagePerMs = GetCanTalon(PortConverters.GetTalonSRX(handle)).VoltageCompensationRate;
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetForwardSoftLimit(IntPtr handle, ref int forwardLimit)
        {
            //TODO: Do This
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetReverseSoftLimit(IntPtr handle, ref int reverseLimit)
        {
            //TODO: Do This
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetForwardSoftEnable(IntPtr handle, ref int enable)
        {
            //TODO: Do This
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetReverseSoftEnable(IntPtr handle, ref int enable)
        {
            //TODO: Do This
            return CTR_Code.CTR_OKAY;
        }

        public static void c_TalonSRX_ChangeMotionControlFramePeriod(IntPtr handle, int periodMs)
        {
            //TODO: Do This
        }

        public static void c_TalonSRX_ClearMotionProfileTrajectories(IntPtr handle)
        {
            //TODO: Do This
        }

        public static int c_TalonSRX_GetMotionProfileTopLevelBufferCount(IntPtr handle)
        {
            return 0;
        }

        public static int c_TalonSRX_IsMotionProfileTopLevelBufferFull(IntPtr handle)
        {
            return 0;
        }

        public static CTR_Code c_TalonSRX_PushMotionProfileTrajectory(IntPtr handle, int targPos, int targVel,
            int profileSlotSelect, int timeDurMs, int velOnly, int isLastPoint, int zeroPos)
        {
            //TODO: Do This
            return CTR_Code.CTR_OKAY;
        }

        public static void c_TalonSRX_ProcessMotionProfileBuffer(IntPtr handle)
        {
            
        }

        public static CTR_Code c_TalonSRX_GetMotionProfileStatus(IntPtr handle, ref int flags, ref int profileSlotSelect,
            ref int targPos, ref int targVel, ref int topBufferRemaining, ref int topBufferCnt, ref int btmBufferCnt,
            ref int outputEnable)
        {
            //TODO: Do This
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetActTraj_IsValid(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetActTraj_ProfileSlotSelect(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetActTraj_VelOnly(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetActTraj_IsLast(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetOutputType(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetIsUnderrun(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetHasUnderrun(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetNextID(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetBufferIsFull(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetCount(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetActTraj_Velocity(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetActTraj_Position(IntPtr handle, ref int param)
        {
            return CTR_Code.CTR_OKAY;
        }

        private static CTR_Code CTalonSRXIsPulseWidthSensorPresent(IntPtr handle, ref int i)
        {
            return CTR_Code.CTR_OKAY;
        }

        private static CTR_Code CTalonSRXGetPulseRiseToRiseUs(IntPtr handle, ref int i)
        {
            return CTR_Code.CTR_OKAY;
        }

        private static CTR_Code CTalonSRXGetPulseRiseToFallUs(IntPtr handle, ref int i)
        {
            return CTR_Code.CTR_OKAY;
        }

        private static CTR_Code CTalonSRXGetPulseWidthVelocity(IntPtr handle, ref int i)
        {
            return CTR_Code.CTR_OKAY;
        }

        private static CTR_Code CTalonSRXGetPulseWidthPosition(IntPtr handle, ref int i)
        {
            return CTR_Code.CTR_OKAY;
        }

    }
}
