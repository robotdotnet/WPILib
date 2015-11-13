using System;

namespace HAL_Simulator.Data
{
    public class CanTalonData : NotifyDataBase
    {
        internal CanTalonData() { }

        private double m_eProfileParamSlot0_P = 0.0;
        private double m_eProfileParamSlot0_I = 0.0;
        private double m_eProfileParamSlot0_D = 0.0;
        private double m_eProfileParamSlot0_F = 0.0;
        private double m_eProfileParamSlot0_IZone = 0.0;
        private double m_eProfileParamSlot0_CloseLoopRampRate = 0.0;
        private double m_eProfileParamSlot1_P = 0.0;
        private double m_eProfileParamSlot1_I = 0.0;
        private double m_eProfileParamSlot1_D = 0.0;
        private double m_eProfileParamSlot1_F = 0.0;
        private double m_eProfileParamSlot1_IZone = 0.0;
        private double m_eProfileParamSlot1_CloseLoopRampRate = 0.0;
        private double m_eProfileParamSoftLimitForThreshold = 0.0;
        private double m_eProfileParamSoftLimitRevThreshold = 0.0;
        private double m_eProfileParamSoftLimitForEnable = 0.0;
        private double m_eProfileParamSoftLimitRevEnable = 0.0;
        private double m_eOnBoot_BrakeMode = 0.0;
        private double m_eOnBoot_LimitSwitch_Forward_NormallyClosed = 0.0;
        private double m_eOnBoot_LimitSwitch_Reverse_NormallyClosed = 0.0;
        private double m_eOnBoot_LimitSwitch_Forward_Disable = 0.0;
        private double m_eOnBoot_LimitSwitch_Reverse_Disable = 0.0;
        private double m_eFault_OverTemp = 0.0;
        private double m_eFault_UnderVoltage = 0.0;
        private double m_eFault_ForLim = 0.0;
        private double m_eFault_RevLim = 0.0;
        private double m_eFault_HardwareFailure = 0.0;
        private double m_eFault_ForSoftLim = 0.0;
        private double m_eFault_RevSoftLim = 0.0;
        private double m_eStckyFault_OverTemp = 0.0;
        private double m_eStckyFault_UnderVoltage = 0.0;
        private double m_eStckyFault_ForLim = 0.0;
        private double m_eStckyFault_RevLim = 0.0;
        private double m_eStckyFault_ForSoftLim = 0.0;
        private double m_eStckyFault_RevSoftLim = 0.0;
        private double m_eAppliedThrottle = 0.0;
        private double m_eCloseLoopErr = 0.0;
        private double m_eFeedbackDeviceSelect = 0.0;
        private bool m_eRevMotDuringCloseLoopEn = false;
        private double m_eModeSelect = 0.0;
        private double m_eProfileSlotSelect = 0.0;
        private double m_eRampThrottle = 0.0;
        private bool m_eRevFeedbackSensor = false;
        private double m_eLimitSwitchEn = 0.0;
        private bool m_eLimitSwitchClosedFor = false;
        private bool m_eLimitSwitchClosedRev = false;
        private double m_eSensorPosition = 0.0;
        private double m_eSensorVelocity = 0.0;
        private double m_eCurrent = 0.0;
        private bool m_eBrakeIsEnabled = false;
        private double m_eEncPosition = 0.0;
        private double m_eEncVel = 0.0;
        private double m_eEncIndexRiseEvents = 0.0;
        private double m_eQuadApin = 0.0;
        private double m_eQuadBpin = 0.0;
        private double m_eQuadIdxpin = 0.0;
        private double m_eAnalogInWithOv = 0.0;
        private double m_eAnalogInVel = 0.0;
        private double m_eTemp = 0.0;
        private double m_eBatteryV = 0.0;
        private double m_eResetCount = 0.0;
        private double m_eResetFlags = 0.0;
        private double m_eFirmVers = 0.0;
        private double m_eSettingsChanged = 0.0;
        private double m_eQuadFilterEn = 0.0;
        private double m_ePidIaccum = 0.0;
        private double m_eAinPosition = 0.0;
        private double m_eProfileParamSlot0_AllowableClosedLoopErr = 0.0;
        private double m_eProfileParamSlot1_AllowableClosedLoopErr = 0.0;
        private double m_eNumberEncoderCPR = 0.0;
        private double m_eNumberPotTurns = 0.0;

        private double m_overrideLimitSwitch = 0.0;
        private double m_overrideBrakeType = 0.0;

        private double m_demand = 0.0;

        public override void ResetData()
        {
            m_eProfileParamSlot0_P = 0.0;
            m_eProfileParamSlot0_I = 0.0;
            m_eProfileParamSlot0_D = 0.0;
            m_eProfileParamSlot0_F = 0.0;
            m_eProfileParamSlot0_IZone = 0.0;
            m_eProfileParamSlot0_CloseLoopRampRate = 0.0;
            m_eProfileParamSlot1_P = 0.0;
            m_eProfileParamSlot1_I = 0.0;
            m_eProfileParamSlot1_D = 0.0;
            m_eProfileParamSlot1_F = 0.0;
            m_eProfileParamSlot1_IZone = 0.0;
            m_eProfileParamSlot1_CloseLoopRampRate = 0.0;
            m_eProfileParamSoftLimitForThreshold = 0.0;
            m_eProfileParamSoftLimitRevThreshold = 0.0;
            m_eProfileParamSoftLimitForEnable = 0.0;
            m_eProfileParamSoftLimitRevEnable = 0.0;
            m_eOnBoot_BrakeMode = 0.0;
            m_eOnBoot_LimitSwitch_Forward_NormallyClosed = 0.0;
            m_eOnBoot_LimitSwitch_Reverse_NormallyClosed = 0.0;
            m_eOnBoot_LimitSwitch_Forward_Disable = 0.0;
            m_eOnBoot_LimitSwitch_Reverse_Disable = 0.0;
            m_eFault_OverTemp = 0.0;
            m_eFault_UnderVoltage = 0.0;
            m_eFault_ForLim = 0.0;
            m_eFault_RevLim = 0.0;
            m_eFault_HardwareFailure = 0.0;
            m_eFault_ForSoftLim = 0.0;
            m_eFault_RevSoftLim = 0.0;
            m_eStckyFault_OverTemp = 0.0;
            m_eStckyFault_UnderVoltage = 0.0;
            m_eStckyFault_ForLim = 0.0;
            m_eStckyFault_RevLim = 0.0;
            m_eStckyFault_ForSoftLim = 0.0;
            m_eStckyFault_RevSoftLim = 0.0;
            m_eAppliedThrottle = 0.0;
            m_eCloseLoopErr = 0.0;
            m_eFeedbackDeviceSelect = 0.0;
            m_eRevMotDuringCloseLoopEn = false;
            m_eModeSelect = 0.0;
            m_eProfileSlotSelect = 0.0;
            m_eRampThrottle = 0.0;
            m_eRevFeedbackSensor = false;
            m_eLimitSwitchEn = 0.0;
            m_eLimitSwitchClosedFor = false;
            m_eLimitSwitchClosedRev = false;
            m_eSensorPosition = 0.0;
            m_eSensorVelocity = 0.0;
            m_eCurrent = 0.0;
            m_eBrakeIsEnabled = false;
            m_eEncPosition = 0.0;
            m_eEncVel = 0.0;
            m_eEncIndexRiseEvents = 0.0;
            m_eQuadApin = 0.0;
            m_eQuadBpin = 0.0;
            m_eQuadIdxpin = 0.0;
            m_eAnalogInWithOv = 0.0;
            m_eAnalogInVel = 0.0;
            m_eTemp = 0.0;
            m_eBatteryV = 0.0;
            m_eResetCount = 0.0;
            m_eResetFlags = 0.0;
            m_eFirmVers = 0.0;
            m_eSettingsChanged = 0.0;
            m_eQuadFilterEn = 0.0;
            m_ePidIaccum = 0.0;
            m_eAinPosition = 0.0;
            m_eProfileParamSlot0_AllowableClosedLoopErr = 0.0;
            m_eProfileParamSlot1_AllowableClosedLoopErr = 0.0;
            m_eNumberEncoderCPR = 0.0;
            m_eNumberPotTurns = 0.0;

            m_overrideLimitSwitch = 0.0;
            m_overrideBrakeType = 0.0;

            m_demand = 0.0;
        }



        public double GetParam(HAL_Base.HALCanTalonSRX.ParamID id)
        {
            switch (id)
            {
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_P:
                    return ProfileParamSlot0_P;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_I:
                    return ProfileParamSlot0_I;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_D:
                    return ProfileParamSlot0_D;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_F:
                    return ProfileParamSlot0_F;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_IZone:
                    return ProfileParamSlot0_IZone;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_CloseLoopRampRate:
                    return ProfileParamSlot0_CloseLoopRampRate;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_P:
                    return ProfileParamSlot1_P;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_I:
                    return ProfileParamSlot1_I;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_D:
                    return ProfileParamSlot1_D;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_F:
                    return ProfileParamSlot1_F;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_IZone:
                    return ProfileParamSlot1_IZone;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_CloseLoopRampRate:
                    return ProfileParamSlot1_CloseLoopRampRate;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForThreshold:
                    return ProfileParamSoftLimitForThreshold;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevThreshold:
                    return ProfileParamSoftLimitRevThreshold;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForEnable:
                    return ProfileParamSoftLimitForEnable;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevEnable:
                    return ProfileParamSoftLimitRevEnable;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_BrakeMode:
                    return OnBoot_BrakeMode;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed:
                    return OnBoot_LimitSwitch_Forward_NormallyClosed;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed:
                    return OnBoot_LimitSwitch_Reverse_NormallyClosed;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_Disable:
                    return OnBoot_LimitSwitch_Forward_Disable;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_Disable:
                    return OnBoot_LimitSwitch_Reverse_Disable;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_OverTemp:
                    return Fault_OverTemp;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_UnderVoltage:
                    return Fault_UnderVoltage;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_ForLim:
                    return Fault_ForLim;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_RevLim:
                    return Fault_RevLim;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_HardwareFailure:
                    return Fault_HardwareFailure;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_ForSoftLim:
                    return Fault_ForSoftLim;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_RevSoftLim:
                    return Fault_RevSoftLim;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_OverTemp:
                    return StckyFault_OverTemp;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_UnderVoltage:
                    return StckyFault_UnderVoltage;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_ForLim:
                    return StckyFault_ForLim;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_RevLim:
                    return StckyFault_RevLim;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_ForSoftLim:
                    return StckyFault_ForSoftLim;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_RevSoftLim:
                    return StckyFault_RevSoftLim;
                case HAL_Base.HALCanTalonSRX.ParamID.eAppliedThrottle:
                    return AppliedThrottle;
                case HAL_Base.HALCanTalonSRX.ParamID.eCloseLoopErr:
                    return CloseLoopErr;
                case HAL_Base.HALCanTalonSRX.ParamID.eFeedbackDeviceSelect:
                    return FeedbackDeviceSelect;
                case HAL_Base.HALCanTalonSRX.ParamID.eRevMotDuringCloseLoopEn:
                    return RevMotDuringCloseLoopEn ? 1 : 0;
                case HAL_Base.HALCanTalonSRX.ParamID.eModeSelect:
                    return ModeSelect;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileSlotSelect:
                    return ProfileSlotSelect;
                case HAL_Base.HALCanTalonSRX.ParamID.eRampThrottle:
                    return RampThrottle;
                case HAL_Base.HALCanTalonSRX.ParamID.eRevFeedbackSensor:
                    return RevFeedbackSensor ? 1 : 0;
                case HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchEn:
                    return LimitSwitchEn;
                case HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedFor:
                    return LimitSwitchClosedFor ? 1 : 0;
                case HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedRev:
                    return LimitSwitchClosedRev ? 1 : 0;
                case HAL_Base.HALCanTalonSRX.ParamID.eSensorPosition:
                    return SensorPosition;
                case HAL_Base.HALCanTalonSRX.ParamID.eSensorVelocity:
                    return SensorVelocity;
                case HAL_Base.HALCanTalonSRX.ParamID.eCurrent:
                    return Current;
                case HAL_Base.HALCanTalonSRX.ParamID.eBrakeIsEnabled:
                    return BrakeIsEnabled ? 1 : 0;
                case HAL_Base.HALCanTalonSRX.ParamID.eEncPosition:
                    return EncPosition;
                case HAL_Base.HALCanTalonSRX.ParamID.eEncVel:
                    return EncVel;
                case HAL_Base.HALCanTalonSRX.ParamID.eEncIndexRiseEvents:
                    return EncIndexRiseEvents;
                case HAL_Base.HALCanTalonSRX.ParamID.eQuadApin:
                    return QuadApin;
                case HAL_Base.HALCanTalonSRX.ParamID.eQuadBpin:
                    return QuadBpin;
                case HAL_Base.HALCanTalonSRX.ParamID.eQuadIdxpin:
                    return QuadIdxpin;
                case HAL_Base.HALCanTalonSRX.ParamID.eAnalogInWithOv:
                    return AnalogInWithOv;
                case HAL_Base.HALCanTalonSRX.ParamID.eAnalogInVel:
                    return AnalogInVel;
                case HAL_Base.HALCanTalonSRX.ParamID.eTemp:
                    return Temp;
                case HAL_Base.HALCanTalonSRX.ParamID.eBatteryV:
                    return BatteryV;
                case HAL_Base.HALCanTalonSRX.ParamID.eResetCount:
                    return ResetCount;
                case HAL_Base.HALCanTalonSRX.ParamID.eResetFlags:
                    return ResetFlags;
                case HAL_Base.HALCanTalonSRX.ParamID.eFirmVers:
                    return FirmVers;
                case HAL_Base.HALCanTalonSRX.ParamID.eSettingsChanged:
                    return SettingsChanged;
                case HAL_Base.HALCanTalonSRX.ParamID.eQuadFilterEn:
                    return QuadFilterEn;
                case HAL_Base.HALCanTalonSRX.ParamID.ePidIaccum:
                    return PidIaccum;
                case HAL_Base.HALCanTalonSRX.ParamID.eAinPosition:
                    return AinPosition;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_AllowableClosedLoopErr:
                    return ProfileParamSlot0_AllowableClosedLoopErr;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_AllowableClosedLoopErr:
                    return ProfileParamSlot1_AllowableClosedLoopErr;
                case HAL_Base.HALCanTalonSRX.ParamID.eNumberEncoderCPR:
                    return NumberEncoderCPR;
                case HAL_Base.HALCanTalonSRX.ParamID.eNumberPotTurns:
                    return NumberPotTurns;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }


        public void SetParam(HAL_Base.HALCanTalonSRX.ParamID id, double value)
        {
            switch (id)
            {
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_P:
                    ProfileParamSlot0_P = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_I:
                    ProfileParamSlot0_I = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_D:
                    ProfileParamSlot0_D = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_F:
                    ProfileParamSlot0_F = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_IZone:
                    ProfileParamSlot0_IZone = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_CloseLoopRampRate:
                    ProfileParamSlot0_CloseLoopRampRate = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_P:
                    ProfileParamSlot1_P = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_I:
                    ProfileParamSlot1_I = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_D:
                    ProfileParamSlot1_D = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_F:
                    ProfileParamSlot1_F = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_IZone:
                    ProfileParamSlot1_IZone = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_CloseLoopRampRate:
                    ProfileParamSlot1_CloseLoopRampRate = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForThreshold:
                    ProfileParamSoftLimitForThreshold = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevThreshold:
                    ProfileParamSoftLimitRevThreshold = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForEnable:
                    ProfileParamSoftLimitForEnable = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevEnable:
                    ProfileParamSoftLimitRevEnable = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_BrakeMode:
                    OnBoot_BrakeMode = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed:
                    OnBoot_LimitSwitch_Forward_NormallyClosed = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed:
                    OnBoot_LimitSwitch_Reverse_NormallyClosed = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_Disable:
                    OnBoot_LimitSwitch_Forward_Disable = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_Disable:
                    OnBoot_LimitSwitch_Reverse_Disable = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_OverTemp:
                    Fault_OverTemp = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_UnderVoltage:
                    Fault_UnderVoltage = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_ForLim:
                    Fault_ForLim = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_RevLim:
                    Fault_RevLim = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_HardwareFailure:
                    Fault_HardwareFailure = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_ForSoftLim:
                    Fault_ForSoftLim = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFault_RevSoftLim:
                    Fault_RevSoftLim = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_OverTemp:
                    StckyFault_OverTemp = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_UnderVoltage:
                    StckyFault_UnderVoltage = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_ForLim:
                    StckyFault_ForLim = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_RevLim:
                    StckyFault_RevLim = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_ForSoftLim:
                    StckyFault_ForSoftLim = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_RevSoftLim:
                    StckyFault_RevSoftLim = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eAppliedThrottle:
                    AppliedThrottle = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eCloseLoopErr:
                    CloseLoopErr = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFeedbackDeviceSelect:
                    FeedbackDeviceSelect = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eRevMotDuringCloseLoopEn:
                    RevMotDuringCloseLoopEn = value != 0;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eModeSelect:
                    ModeSelect = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileSlotSelect:
                    ProfileSlotSelect = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eRampThrottle:
                    RampThrottle = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eRevFeedbackSensor:
                    RevFeedbackSensor = value != 0;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchEn:
                    LimitSwitchEn = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedFor:
                    LimitSwitchClosedFor = value != 0;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedRev:
                    LimitSwitchClosedRev = value != 0;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eSensorPosition:
                    SensorPosition = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eSensorVelocity:
                    SensorVelocity = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eCurrent:
                    Current = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eBrakeIsEnabled:
                    BrakeIsEnabled = value != 0;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eEncPosition:
                    EncPosition = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eEncVel:
                    EncVel = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eEncIndexRiseEvents:
                    EncIndexRiseEvents = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eQuadApin:
                    QuadApin = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eQuadBpin:
                    QuadBpin = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eQuadIdxpin:
                    QuadIdxpin = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eAnalogInWithOv:
                    AnalogInWithOv = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eAnalogInVel:
                    AnalogInVel = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eTemp:
                    Temp = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eBatteryV:
                    BatteryV = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eResetCount:
                    ResetCount = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eResetFlags:
                    ResetFlags = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eFirmVers:
                    FirmVers = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eSettingsChanged:
                    SettingsChanged = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eQuadFilterEn:
                    QuadFilterEn = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.ePidIaccum:
                    PidIaccum = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eAinPosition:
                    AinPosition = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_AllowableClosedLoopErr:
                    ProfileParamSlot0_AllowableClosedLoopErr = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_AllowableClosedLoopErr:
                    ProfileParamSlot1_AllowableClosedLoopErr = value;
                    break;
                case HAL_Base.HALCanTalonSRX.ParamID.eNumberEncoderCPR:
                    NumberEncoderCPR = value;
                case HAL_Base.HALCanTalonSRX.ParamID.eNumberPotTurns:
                    NumberPotTurns = value;
            }
        }


        public double ProfileParamSlot0_P
        {
            get { return m_eProfileParamSlot0_P; }
            set
            {
                if (value == m_eProfileParamSlot0_P) return;
                m_eProfileParamSlot0_P = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot0_I
        {
            get { return m_eProfileParamSlot0_I; }
            set
            {
                if (value == m_eProfileParamSlot0_I) return;
                m_eProfileParamSlot0_I = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot0_D
        {
            get { return m_eProfileParamSlot0_D; }
            set
            {
                if (value == m_eProfileParamSlot0_D) return;
                m_eProfileParamSlot0_D = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot0_F
        {
            get { return m_eProfileParamSlot0_F; }
            set
            {
                if (value == m_eProfileParamSlot0_F) return;
                m_eProfileParamSlot0_F = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot0_IZone
        {
            get { return m_eProfileParamSlot0_IZone; }
            set
            {
                if (value == m_eProfileParamSlot0_IZone) return;
                m_eProfileParamSlot0_IZone = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot0_CloseLoopRampRate
        {
            get { return m_eProfileParamSlot0_CloseLoopRampRate; }
            set
            {
                if (value == m_eProfileParamSlot0_CloseLoopRampRate) return;
                m_eProfileParamSlot0_CloseLoopRampRate = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot1_P
        {
            get { return m_eProfileParamSlot1_P; }
            set
            {
                if (value == m_eProfileParamSlot1_P) return;
                m_eProfileParamSlot1_P = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot1_I
        {
            get { return m_eProfileParamSlot1_I; }
            set
            {
                if (value == m_eProfileParamSlot1_I) return;
                m_eProfileParamSlot1_I = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot1_D
        {
            get { return m_eProfileParamSlot1_D; }
            set
            {
                if (value == m_eProfileParamSlot1_D) return;
                m_eProfileParamSlot1_D = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot1_F
        {
            get { return m_eProfileParamSlot1_F; }
            set
            {
                if (value == m_eProfileParamSlot1_F) return;
                m_eProfileParamSlot1_F = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot1_IZone
        {
            get { return m_eProfileParamSlot1_IZone; }
            set
            {
                if (value == m_eProfileParamSlot1_IZone) return;
                m_eProfileParamSlot1_IZone = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSlot1_CloseLoopRampRate
        {
            get { return m_eProfileParamSlot1_CloseLoopRampRate; }
            set
            {
                if (value == m_eProfileParamSlot1_CloseLoopRampRate) return;
                m_eProfileParamSlot1_CloseLoopRampRate = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSoftLimitForThreshold
        {
            get { return m_eProfileParamSoftLimitForThreshold; }
            set
            {
                if (value == m_eProfileParamSoftLimitForThreshold) return;
                m_eProfileParamSoftLimitForThreshold = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSoftLimitRevThreshold
        {
            get { return m_eProfileParamSoftLimitRevThreshold; }
            set
            {
                if (value == m_eProfileParamSoftLimitRevThreshold) return;
                m_eProfileParamSoftLimitRevThreshold = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSoftLimitForEnable
        {
            get { return m_eProfileParamSoftLimitForEnable; }
            set
            {
                if (value == m_eProfileParamSoftLimitForEnable) return;
                m_eProfileParamSoftLimitForEnable = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileParamSoftLimitRevEnable
        {
            get { return m_eProfileParamSoftLimitRevEnable; }
            set
            {
                if (value == m_eProfileParamSoftLimitRevEnable) return;
                m_eProfileParamSoftLimitRevEnable = value;
                OnPropertyChanged(value);
            }
        }
        public double OnBoot_BrakeMode
        {
            get { return m_eOnBoot_BrakeMode; }
            set
            {
                if (value == m_eOnBoot_BrakeMode) return;
                m_eOnBoot_BrakeMode = value;
                OnPropertyChanged(value);
            }
        }
        public double OnBoot_LimitSwitch_Forward_NormallyClosed
        {
            get { return m_eOnBoot_LimitSwitch_Forward_NormallyClosed; }
            set
            {
                if (value == m_eOnBoot_LimitSwitch_Forward_NormallyClosed) return;
                m_eOnBoot_LimitSwitch_Forward_NormallyClosed = value;
                OnPropertyChanged(value);
            }
        }
        public double OnBoot_LimitSwitch_Reverse_NormallyClosed
        {
            get { return m_eOnBoot_LimitSwitch_Reverse_NormallyClosed; }
            set
            {
                if (value == m_eOnBoot_LimitSwitch_Reverse_NormallyClosed) return;
                m_eOnBoot_LimitSwitch_Reverse_NormallyClosed = value;
                OnPropertyChanged(value);
            }
        }
        public double OnBoot_LimitSwitch_Forward_Disable
        {
            get { return m_eOnBoot_LimitSwitch_Forward_Disable; }
            set
            {
                if (value == m_eOnBoot_LimitSwitch_Forward_Disable) return;
                m_eOnBoot_LimitSwitch_Forward_Disable = value;
                OnPropertyChanged(value);
            }
        }
        public double OnBoot_LimitSwitch_Reverse_Disable
        {
            get { return m_eOnBoot_LimitSwitch_Reverse_Disable; }
            set
            {
                if (value == m_eOnBoot_LimitSwitch_Reverse_Disable) return;
                m_eOnBoot_LimitSwitch_Reverse_Disable = value;
                OnPropertyChanged(value);
            }
        }
        public double Fault_OverTemp
        {
            get { return m_eFault_OverTemp; }
            set
            {
                if (value == m_eFault_OverTemp) return;
                m_eFault_OverTemp = value;
                OnPropertyChanged(value);
            }
        }
        public double Fault_UnderVoltage
        {
            get { return m_eFault_UnderVoltage; }
            set
            {
                if (value == m_eFault_UnderVoltage) return;
                m_eFault_UnderVoltage = value;
                OnPropertyChanged(value);
            }
        }
        public double Fault_ForLim
        {
            get { return m_eFault_ForLim; }
            set
            {
                if (value == m_eFault_ForLim) return;
                m_eFault_ForLim = value;
                OnPropertyChanged(value);
            }
        }
        public double Fault_RevLim
        {
            get { return m_eFault_RevLim; }
            set
            {
                if (value == m_eFault_RevLim) return;
                m_eFault_RevLim = value;
                OnPropertyChanged(value);
            }
        }
        public double Fault_HardwareFailure
        {
            get { return m_eFault_HardwareFailure; }
            set
            {
                if (value == m_eFault_HardwareFailure) return;
                m_eFault_HardwareFailure = value;
                OnPropertyChanged(value);
            }
        }
        public double Fault_ForSoftLim
        {
            get { return m_eFault_ForSoftLim; }
            set
            {
                if (value == m_eFault_ForSoftLim) return;
                m_eFault_ForSoftLim = value;
                OnPropertyChanged(value);
            }
        }
        public double Fault_RevSoftLim
        {
            get { return m_eFault_RevSoftLim; }
            set
            {
                if (value == m_eFault_RevSoftLim) return;
                m_eFault_RevSoftLim = value;
                OnPropertyChanged(value);
            }
        }
        public double StckyFault_OverTemp
        {
            get { return m_eStckyFault_OverTemp; }
            set
            {
                if (value == m_eStckyFault_OverTemp) return;
                m_eStckyFault_OverTemp = value;
                OnPropertyChanged(value);
            }
        }
        public double StckyFault_UnderVoltage
        {
            get { return m_eStckyFault_UnderVoltage; }
            set
            {
                if (value == m_eStckyFault_UnderVoltage) return;
                m_eStckyFault_UnderVoltage = value;
                OnPropertyChanged(value);
            }
        }
        public double StckyFault_ForLim
        {
            get { return m_eStckyFault_ForLim; }
            set
            {
                if (value == m_eStckyFault_ForLim) return;
                m_eStckyFault_ForLim = value;
                OnPropertyChanged(value);
            }
        }
        public double StckyFault_RevLim
        {
            get { return m_eStckyFault_RevLim; }
            set
            {
                if (value == m_eStckyFault_RevLim) return;
                m_eStckyFault_RevLim = value;
                OnPropertyChanged(value);
            }
        }
        public double StckyFault_ForSoftLim
        {
            get { return m_eStckyFault_ForSoftLim; }
            set
            {
                if (value == m_eStckyFault_ForSoftLim) return;
                m_eStckyFault_ForSoftLim = value;
                OnPropertyChanged(value);
            }
        }
        public double StckyFault_RevSoftLim
        {
            get { return m_eStckyFault_RevSoftLim; }
            set
            {
                if (value == m_eStckyFault_RevSoftLim) return;
                m_eStckyFault_RevSoftLim = value;
                OnPropertyChanged(value);
            }
        }
        public double AppliedThrottle
        {
            get { return m_eAppliedThrottle; }
            set
            {
                if (value == m_eAppliedThrottle) return;
                m_eAppliedThrottle = value;
                OnPropertyChanged(value);
            }
        }
        public double CloseLoopErr
        {
            get { return m_eCloseLoopErr; }
            set
            {
                if (value == m_eCloseLoopErr) return;
                m_eCloseLoopErr = value;
                OnPropertyChanged(value);
            }
        }
        public double FeedbackDeviceSelect
        {
            get { return m_eFeedbackDeviceSelect; }
            set
            {
                if (value == m_eFeedbackDeviceSelect) return;
                m_eFeedbackDeviceSelect = value;
                OnPropertyChanged(value);
            }
        }
        public bool RevMotDuringCloseLoopEn
        {
            get { return m_eRevMotDuringCloseLoopEn; }
            set
            {
                if (value == m_eRevMotDuringCloseLoopEn) return;
                m_eRevMotDuringCloseLoopEn = value;
                OnPropertyChanged(value);
            }
        }
        public double ModeSelect
        {
            get { return m_eModeSelect; }
            set
            {
                if (value == m_eModeSelect) return;
                m_eModeSelect = value;
                OnPropertyChanged(value);
            }
        }
        public double ProfileSlotSelect
        {
            get { return m_eProfileSlotSelect; }
            set
            {
                if (value == m_eProfileSlotSelect) return;
                m_eProfileSlotSelect = value;
                OnPropertyChanged(value);
            }
        }
        public double RampThrottle
        {
            get { return m_eRampThrottle; }
            set
            {
                if (value == m_eRampThrottle) return;
                m_eRampThrottle = value;
                OnPropertyChanged(value);
            }
        }
        public bool RevFeedbackSensor
        {
            get { return m_eRevFeedbackSensor; }
            set
            {
                if (value == m_eRevFeedbackSensor) return;
                m_eRevFeedbackSensor = value;
                OnPropertyChanged(value);
            }
        }
        public double LimitSwitchEn
        {
            get { return m_eLimitSwitchEn; }
            set
            {
                if (value == m_eLimitSwitchEn) return;
                m_eLimitSwitchEn = value;
                OnPropertyChanged(value);
            }
        }
        public bool LimitSwitchClosedFor
        {
            get { return m_eLimitSwitchClosedFor; }
            set
            {
                if (value == m_eLimitSwitchClosedFor) return;
                m_eLimitSwitchClosedFor = value;
                OnPropertyChanged(value);
            }
        }
        public bool LimitSwitchClosedRev
        {
            get { return m_eLimitSwitchClosedRev; }
            set
            {
                if (value == m_eLimitSwitchClosedRev) return;
                m_eLimitSwitchClosedRev = value;
                OnPropertyChanged(value);
            }
        }
        public double SensorPosition
        {
            get { return m_eSensorPosition; }
            set
            {
                if (value == m_eSensorPosition) return;
                m_eSensorPosition = value;
                OnPropertyChanged(value);
            }
        }
        public double SensorVelocity
        {
            get { return m_eSensorVelocity; }
            set
            {
                if (value == m_eSensorVelocity) return;
                m_eSensorVelocity = value;
                OnPropertyChanged(value);
            }
        }
        public double Current
        {
            get { return m_eCurrent; }
            set
            {
                if (value == m_eCurrent) return;
                m_eCurrent = value;
                OnPropertyChanged(value);
            }
        }
        public bool BrakeIsEnabled
        {
            get { return m_eBrakeIsEnabled; }
            set
            {
                if (value == m_eBrakeIsEnabled) return;
                m_eBrakeIsEnabled = value;
                OnPropertyChanged(value);
            }
        }
        public double EncPosition
        {
            get { return m_eEncPosition; }
            set
            {
                if (value == m_eEncPosition) return;
                m_eEncPosition = value;
                OnPropertyChanged(value);
            }
        }
        public double EncVel
        {
            get { return m_eEncVel; }
            set
            {
                if (value == m_eEncVel) return;
                m_eEncVel = value;
                OnPropertyChanged(value);
            }
        }
        public double EncIndexRiseEvents
        {
            get { return m_eEncIndexRiseEvents; }
            set
            {
                if (value == m_eEncIndexRiseEvents) return;
                m_eEncIndexRiseEvents = value;
                OnPropertyChanged(value);
            }
        }
        public double QuadApin
        {
            get { return m_eQuadApin; }
            set
            {
                if (value == m_eQuadApin) return;
                m_eQuadApin = value;
                OnPropertyChanged(value);
            }
        }
        public double QuadBpin
        {
            get { return m_eQuadBpin; }
            set
            {
                if (value == m_eQuadBpin) return;
                m_eQuadBpin = value;
                OnPropertyChanged(value);
            }
        }
        public double QuadIdxpin
        {
            get { return m_eQuadIdxpin; }
            set
            {
                if (value == m_eQuadIdxpin) return;
                m_eQuadIdxpin = value;
                OnPropertyChanged(value);
            }
        }
        public double AnalogInWithOv
        {
            get { return m_eAnalogInWithOv; }
            set
            {
                if (value == m_eAnalogInWithOv) return;
                m_eAnalogInWithOv = value;
                OnPropertyChanged(value);
            }
        }
        public double AnalogInVel
        {
            get { return m_eAnalogInVel; }
            set
            {
                if (value == m_eAnalogInVel) return;
                m_eAnalogInVel = value;
                OnPropertyChanged(value);
            }
        }
        public double Temp
        {
            get { return m_eTemp; }
            set
            {
                if (value == m_eTemp) return;
                m_eTemp = value;
                OnPropertyChanged(value);
            }
        }
        public double BatteryV
        {
            get { return m_eBatteryV; }
            set
            {
                if (value == m_eBatteryV) return;
                m_eBatteryV = value;
                OnPropertyChanged(value);
            }
        }
        public double ResetCount
        {
            get { return m_eResetCount; }
            set
            {
                if (value == m_eResetCount) return;
                m_eResetCount = value;
                OnPropertyChanged(value);
            }
        }
        public double ResetFlags
        {
            get { return m_eResetFlags; }
            set
            {
                if (value == m_eResetFlags) return;
                m_eResetFlags = value;
                OnPropertyChanged(value);
            }
        }
        public double FirmVers
        {
            get { return m_eFirmVers; }
            set
            {
                if (value == m_eFirmVers) return;
                m_eFirmVers = value;
                OnPropertyChanged(value);
            }
        }
        public double SettingsChanged
        {
            get { return m_eSettingsChanged; }
            set
            {
                if (value == m_eSettingsChanged) return;
                m_eSettingsChanged = value;
                OnPropertyChanged(value);
            }
        }
        public double QuadFilterEn
        {
            get { return m_eQuadFilterEn; }
            set
            {
                if (value == m_eQuadFilterEn) return;
                m_eQuadFilterEn = value;
                OnPropertyChanged(value);
            }
        }
        public double PidIaccum
        {
            get { return m_ePidIaccum; }
            set
            {
                if (value == m_ePidIaccum) return;
                m_ePidIaccum = value;
                OnPropertyChanged(value);
            }
        }
        public double AinPosition
        {
            get { return m_eAinPosition; }
            set
            {
                if (value == m_eAinPosition) return;
                m_eAinPosition = value;
                OnPropertyChanged(value);
            }
        }

        public double ProfileParamSlot0_AllowableClosedLoopErr
        {
            get { return m_eProfileParamSlot0_AllowableClosedLoopErr; }
            set
            {
                if (value == m_eProfileParamSlot0_AllowableClosedLoopErr) return;
                m_eProfileParamSlot0_AllowableClosedLoopErr = value;
                OnPropertyChanged(value);
            }
        }

        public double ProfileParamSlot1_AllowableClosedLoopErr
        {
            get { return m_eProfileParamSlot1_AllowableClosedLoopErr; }
            set
            {
                if (value == m_eProfileParamSlot1_AllowableClosedLoopErr) return;
                m_eProfileParamSlot1_AllowableClosedLoopErr = value;
                OnPropertyChanged(value);
            }
        }

        public double NumberEncoderCPR
        {
            get { return m_eNumberEncoderCPR; }
            set
            {
                if (value == m_eNumberEncoderCPR) return;
                m_eNumberEncoderCPR = value;
                OnPropertyChanged(value);
            }
        }

        public double NumberPotTurns
        {
            get { return NumberPotTurns; }
            set
            {
                if (value == NumberPotTurns) return;
                NumberPotTurns = value;
                OnPropertyChanged(value);
            }
        }

        public double OverrideLimitSwitch
        {
            get { return m_overrideLimitSwitch; }
            set
            {
                if (value == m_overrideLimitSwitch) return;
                m_overrideLimitSwitch = value;
                OnPropertyChanged(value);
            }
        }

        public double OverrideBrakeType
        {
            get { return m_overrideBrakeType; }
            set
            {
                if (value == m_overrideBrakeType) return;
                m_overrideBrakeType = value;
                OnPropertyChanged(value);
            }
        }

        public double Demand
        {
            get { return m_demand; }
            set
            {
                if (value == m_demand) return;
                m_demand = value;
                OnPropertyChanged(value);
            }
        }
    }
}
