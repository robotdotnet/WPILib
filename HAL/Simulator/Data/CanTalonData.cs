using System;
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable InconsistentNaming
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace HAL.Simulator.Data
{
    /// <summary>
    /// Talon SRX CAN Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
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

        /// <inheritdoc/>
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

        internal double GetParam(Base.HALCanTalonSRX.ParamID id)
        {
            switch (id)
            {
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_P:
                    return ProfileParamSlot0_P;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_I:
                    return ProfileParamSlot0_I;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_D:
                    return ProfileParamSlot0_D;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_F:
                    return ProfileParamSlot0_F;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_IZone:
                    return ProfileParamSlot0_IZone;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_CloseLoopRampRate:
                    return ProfileParamSlot0_CloseLoopRampRate;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_P:
                    return ProfileParamSlot1_P;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_I:
                    return ProfileParamSlot1_I;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_D:
                    return ProfileParamSlot1_D;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_F:
                    return ProfileParamSlot1_F;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_IZone:
                    return ProfileParamSlot1_IZone;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_CloseLoopRampRate:
                    return ProfileParamSlot1_CloseLoopRampRate;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForThreshold:
                    return ProfileParamSoftLimitForThreshold;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevThreshold:
                    return ProfileParamSoftLimitRevThreshold;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForEnable:
                    return ProfileParamSoftLimitForEnable;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevEnable:
                    return ProfileParamSoftLimitRevEnable;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_BrakeMode:
                    return OnBoot_BrakeMode;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed:
                    return OnBoot_LimitSwitch_Forward_NormallyClosed;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed:
                    return OnBoot_LimitSwitch_Reverse_NormallyClosed;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_Disable:
                    return OnBoot_LimitSwitch_Forward_Disable;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_Disable:
                    return OnBoot_LimitSwitch_Reverse_Disable;
                case Base.HALCanTalonSRX.ParamID.eFault_OverTemp:
                    return Fault_OverTemp;
                case Base.HALCanTalonSRX.ParamID.eFault_UnderVoltage:
                    return Fault_UnderVoltage;
                case Base.HALCanTalonSRX.ParamID.eFault_ForLim:
                    return Fault_ForLim;
                case Base.HALCanTalonSRX.ParamID.eFault_RevLim:
                    return Fault_RevLim;
                case Base.HALCanTalonSRX.ParamID.eFault_HardwareFailure:
                    return Fault_HardwareFailure;
                case Base.HALCanTalonSRX.ParamID.eFault_ForSoftLim:
                    return Fault_ForSoftLim;
                case Base.HALCanTalonSRX.ParamID.eFault_RevSoftLim:
                    return Fault_RevSoftLim;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_OverTemp:
                    return StckyFault_OverTemp;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_UnderVoltage:
                    return StckyFault_UnderVoltage;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_ForLim:
                    return StckyFault_ForLim;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_RevLim:
                    return StckyFault_RevLim;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_ForSoftLim:
                    return StckyFault_ForSoftLim;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_RevSoftLim:
                    return StckyFault_RevSoftLim;
                case Base.HALCanTalonSRX.ParamID.eAppliedThrottle:
                    return AppliedThrottle;
                case Base.HALCanTalonSRX.ParamID.eCloseLoopErr:
                    return CloseLoopErr;
                case Base.HALCanTalonSRX.ParamID.eFeedbackDeviceSelect:
                    return FeedbackDeviceSelect;
                case Base.HALCanTalonSRX.ParamID.eRevMotDuringCloseLoopEn:
                    return RevMotDuringCloseLoopEn ? 1 : 0;
                case Base.HALCanTalonSRX.ParamID.eModeSelect:
                    return ModeSelect;
                case Base.HALCanTalonSRX.ParamID.eProfileSlotSelect:
                    return ProfileSlotSelect;
                case Base.HALCanTalonSRX.ParamID.eRampThrottle:
                    return RampThrottle;
                case Base.HALCanTalonSRX.ParamID.eRevFeedbackSensor:
                    return RevFeedbackSensor ? 1 : 0;
                case Base.HALCanTalonSRX.ParamID.eLimitSwitchEn:
                    return LimitSwitchEn;
                case Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedFor:
                    return LimitSwitchClosedFor ? 1 : 0;
                case Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedRev:
                    return LimitSwitchClosedRev ? 1 : 0;
                case Base.HALCanTalonSRX.ParamID.eSensorPosition:
                    return SensorPosition;
                case Base.HALCanTalonSRX.ParamID.eSensorVelocity:
                    return SensorVelocity;
                case Base.HALCanTalonSRX.ParamID.eCurrent:
                    return Current;
                case Base.HALCanTalonSRX.ParamID.eBrakeIsEnabled:
                    return BrakeIsEnabled ? 1 : 0;
                case Base.HALCanTalonSRX.ParamID.eEncPosition:
                    return EncPosition;
                case Base.HALCanTalonSRX.ParamID.eEncVel:
                    return EncVel;
                case Base.HALCanTalonSRX.ParamID.eEncIndexRiseEvents:
                    return EncIndexRiseEvents;
                case Base.HALCanTalonSRX.ParamID.eQuadApin:
                    return QuadApin;
                case Base.HALCanTalonSRX.ParamID.eQuadBpin:
                    return QuadBpin;
                case Base.HALCanTalonSRX.ParamID.eQuadIdxpin:
                    return QuadIdxpin;
                case Base.HALCanTalonSRX.ParamID.eAnalogInWithOv:
                    return AnalogInWithOv;
                case Base.HALCanTalonSRX.ParamID.eAnalogInVel:
                    return AnalogInVel;
                case Base.HALCanTalonSRX.ParamID.eTemp:
                    return Temp;
                case Base.HALCanTalonSRX.ParamID.eBatteryV:
                    return BatteryV;
                case Base.HALCanTalonSRX.ParamID.eResetCount:
                    return ResetCount;
                case Base.HALCanTalonSRX.ParamID.eResetFlags:
                    return ResetFlags;
                case Base.HALCanTalonSRX.ParamID.eFirmVers:
                    return FirmVers;
                case Base.HALCanTalonSRX.ParamID.eSettingsChanged:
                    return SettingsChanged;
                case Base.HALCanTalonSRX.ParamID.eQuadFilterEn:
                    return QuadFilterEn;
                case Base.HALCanTalonSRX.ParamID.ePidIaccum:
                    return PidIaccum;
                case Base.HALCanTalonSRX.ParamID.eAinPosition:
                    return AinPosition;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_AllowableClosedLoopErr:
                    return ProfileParamSlot0_AllowableClosedLoopErr;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_AllowableClosedLoopErr:
                    return ProfileParamSlot1_AllowableClosedLoopErr;
                case Base.HALCanTalonSRX.ParamID.eNumberEncoderCPR:
                    return NumberEncoderCPR;
                case Base.HALCanTalonSRX.ParamID.eNumberPotTurns:
                    return NumberPotTurns;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }


        internal void SetParam(Base.HALCanTalonSRX.ParamID id, double value)
        {
            switch (id)
            {
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_P:
                    ProfileParamSlot0_P = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_I:
                    ProfileParamSlot0_I = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_D:
                    ProfileParamSlot0_D = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_F:
                    ProfileParamSlot0_F = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_IZone:
                    ProfileParamSlot0_IZone = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_CloseLoopRampRate:
                    ProfileParamSlot0_CloseLoopRampRate = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_P:
                    ProfileParamSlot1_P = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_I:
                    ProfileParamSlot1_I = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_D:
                    ProfileParamSlot1_D = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_F:
                    ProfileParamSlot1_F = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_IZone:
                    ProfileParamSlot1_IZone = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_CloseLoopRampRate:
                    ProfileParamSlot1_CloseLoopRampRate = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForThreshold:
                    ProfileParamSoftLimitForThreshold = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevThreshold:
                    ProfileParamSoftLimitRevThreshold = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForEnable:
                    ProfileParamSoftLimitForEnable = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevEnable:
                    ProfileParamSoftLimitRevEnable = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_BrakeMode:
                    OnBoot_BrakeMode = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed:
                    OnBoot_LimitSwitch_Forward_NormallyClosed = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed:
                    OnBoot_LimitSwitch_Reverse_NormallyClosed = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_Disable:
                    OnBoot_LimitSwitch_Forward_Disable = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_Disable:
                    OnBoot_LimitSwitch_Reverse_Disable = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFault_OverTemp:
                    Fault_OverTemp = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFault_UnderVoltage:
                    Fault_UnderVoltage = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFault_ForLim:
                    Fault_ForLim = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFault_RevLim:
                    Fault_RevLim = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFault_HardwareFailure:
                    Fault_HardwareFailure = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFault_ForSoftLim:
                    Fault_ForSoftLim = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFault_RevSoftLim:
                    Fault_RevSoftLim = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_OverTemp:
                    StckyFault_OverTemp = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_UnderVoltage:
                    StckyFault_UnderVoltage = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_ForLim:
                    StckyFault_ForLim = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_RevLim:
                    StckyFault_RevLim = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_ForSoftLim:
                    StckyFault_ForSoftLim = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eStckyFault_RevSoftLim:
                    StckyFault_RevSoftLim = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eAppliedThrottle:
                    AppliedThrottle = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eCloseLoopErr:
                    CloseLoopErr = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFeedbackDeviceSelect:
                    FeedbackDeviceSelect = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eRevMotDuringCloseLoopEn:
                    RevMotDuringCloseLoopEn = value != 0;
                    break;
                case Base.HALCanTalonSRX.ParamID.eModeSelect:
                    ModeSelect = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileSlotSelect:
                    ProfileSlotSelect = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eRampThrottle:
                    RampThrottle = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eRevFeedbackSensor:
                    RevFeedbackSensor = value != 0;
                    break;
                case Base.HALCanTalonSRX.ParamID.eLimitSwitchEn:
                    LimitSwitchEn = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedFor:
                    LimitSwitchClosedFor = value != 0;
                    break;
                case Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedRev:
                    LimitSwitchClosedRev = value != 0;
                    break;
                case Base.HALCanTalonSRX.ParamID.eSensorPosition:
                    SensorPosition = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eSensorVelocity:
                    SensorVelocity = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eCurrent:
                    Current = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eBrakeIsEnabled:
                    BrakeIsEnabled = value != 0;
                    break;
                case Base.HALCanTalonSRX.ParamID.eEncPosition:
                    EncPosition = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eEncVel:
                    EncVel = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eEncIndexRiseEvents:
                    EncIndexRiseEvents = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eQuadApin:
                    QuadApin = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eQuadBpin:
                    QuadBpin = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eQuadIdxpin:
                    QuadIdxpin = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eAnalogInWithOv:
                    AnalogInWithOv = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eAnalogInVel:
                    AnalogInVel = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eTemp:
                    Temp = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eBatteryV:
                    BatteryV = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eResetCount:
                    ResetCount = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eResetFlags:
                    ResetFlags = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eFirmVers:
                    FirmVers = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eSettingsChanged:
                    SettingsChanged = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eQuadFilterEn:
                    QuadFilterEn = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.ePidIaccum:
                    PidIaccum = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eAinPosition:
                    AinPosition = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_AllowableClosedLoopErr:
                    ProfileParamSlot0_AllowableClosedLoopErr = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_AllowableClosedLoopErr:
                    ProfileParamSlot1_AllowableClosedLoopErr = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eNumberEncoderCPR:
                    NumberEncoderCPR = value;
                    break;
                case Base.HALCanTalonSRX.ParamID.eNumberPotTurns:
                    NumberPotTurns = value;
                    break;
            }
        }


        /// <summary>
        /// Gets or sets the profile parameter slot0_ p.
        /// </summary>
        /// <value>
        /// The profile parameter slot0_ p.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot0_ i.
        /// </summary>
        /// <value>
        /// The profile parameter slot0_ i.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot0_ d.
        /// </summary>
        /// <value>
        /// The profile parameter slot0_ d.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot0_ f.
        /// </summary>
        /// <value>
        /// The profile parameter slot0_ f.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot0_ i zone.
        /// </summary>
        /// <value>
        /// The profile parameter slot0_ i zone.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot0_ close loop ramp rate.
        /// </summary>
        /// <value>
        /// The profile parameter slot0_ close loop ramp rate.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot1_ p.
        /// </summary>
        /// <value>
        /// The profile parameter slot1_ p.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot1_ i.
        /// </summary>
        /// <value>
        /// The profile parameter slot1_ i.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot1_ d.
        /// </summary>
        /// <value>
        /// The profile parameter slot1_ d.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot1_ f.
        /// </summary>
        /// <value>
        /// The profile parameter slot1_ f.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot1_ i zone.
        /// </summary>
        /// <value>
        /// The profile parameter slot1_ i zone.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter slot1_ close loop ramp rate.
        /// </summary>
        /// <value>
        /// The profile parameter slot1_ close loop ramp rate.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter soft limit for threshold.
        /// </summary>
        /// <value>
        /// The profile parameter soft limit for threshold.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter soft limit rev threshold.
        /// </summary>
        /// <value>
        /// The profile parameter soft limit rev threshold.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter soft limit for enable.
        /// </summary>
        /// <value>
        /// The profile parameter soft limit for enable.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile parameter soft limit rev enable.
        /// </summary>
        /// <value>
        /// The profile parameter soft limit rev enable.
        /// </value>
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
        /// <summary>
        /// Gets or sets the on boot_ brake mode.
        /// </summary>
        /// <value>
        /// The on boot_ brake mode.
        /// </value>
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
        /// <summary>
        /// Gets or sets the on boot_ limit switch_ forward_ normally closed.
        /// </summary>
        /// <value>
        /// The on boot_ limit switch_ forward_ normally closed.
        /// </value>
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
        /// <summary>
        /// Gets or sets the on boot_ limit switch_ reverse_ normally closed.
        /// </summary>
        /// <value>
        /// The on boot_ limit switch_ reverse_ normally closed.
        /// </value>
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
        /// <summary>
        /// Gets or sets the on boot_ limit switch_ forward_ disable.
        /// </summary>
        /// <value>
        /// The on boot_ limit switch_ forward_ disable.
        /// </value>
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
        /// <summary>
        /// Gets or sets the on boot_ limit switch_ reverse_ disable.
        /// </summary>
        /// <value>
        /// The on boot_ limit switch_ reverse_ disable.
        /// </value>
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
        /// <summary>
        /// Gets or sets the fault_ over temporary.
        /// </summary>
        /// <value>
        /// The fault_ over temporary.
        /// </value>
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
        /// <summary>
        /// Gets or sets the fault_ under voltage.
        /// </summary>
        /// <value>
        /// The fault_ under voltage.
        /// </value>
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
        /// <summary>
        /// Gets or sets the fault_ for lim.
        /// </summary>
        /// <value>
        /// The fault_ for lim.
        /// </value>
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
        /// <summary>
        /// Gets or sets the fault_ rev lim.
        /// </summary>
        /// <value>
        /// The fault_ rev lim.
        /// </value>
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
        /// <summary>
        /// Gets or sets the fault_ hardware failure.
        /// </summary>
        /// <value>
        /// The fault_ hardware failure.
        /// </value>
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
        /// <summary>
        /// Gets or sets the fault_ for soft lim.
        /// </summary>
        /// <value>
        /// The fault_ for soft lim.
        /// </value>
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
        /// <summary>
        /// Gets or sets the fault_ rev soft lim.
        /// </summary>
        /// <value>
        /// The fault_ rev soft lim.
        /// </value>
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
        /// <summary>
        /// Gets or sets the stcky fault_ over temporary.
        /// </summary>
        /// <value>
        /// The stcky fault_ over temporary.
        /// </value>
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
        /// <summary>
        /// Gets or sets the stcky fault_ under voltage.
        /// </summary>
        /// <value>
        /// The stcky fault_ under voltage.
        /// </value>
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
        /// <summary>
        /// Gets or sets the stcky fault_ for lim.
        /// </summary>
        /// <value>
        /// The stcky fault_ for lim.
        /// </value>
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
        /// <summary>
        /// Gets or sets the stcky fault_ rev lim.
        /// </summary>
        /// <value>
        /// The stcky fault_ rev lim.
        /// </value>
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
        /// <summary>
        /// Gets or sets the stcky fault_ for soft lim.
        /// </summary>
        /// <value>
        /// The stcky fault_ for soft lim.
        /// </value>
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
        /// <summary>
        /// Gets or sets the stcky fault_ rev soft lim.
        /// </summary>
        /// <value>
        /// The stcky fault_ rev soft lim.
        /// </value>
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
        /// <summary>
        /// Gets or sets the applied throttle.
        /// </summary>
        /// <value>
        /// The applied throttle.
        /// </value>
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
        /// <summary>
        /// Gets or sets the close loop error.
        /// </summary>
        /// <value>
        /// The close loop error.
        /// </value>
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
        /// <summary>
        /// Gets or sets the feedback device select.
        /// </summary>
        /// <value>
        /// The feedback device select.
        /// </value>
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
        /// <summary>
        /// Gets or sets a value indicating whether [rev mot during close loop en].
        /// </summary>
        /// <value>
        /// <c>true</c> if [rev mot during close loop en]; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets or sets the mode select.
        /// </summary>
        /// <value>
        /// The mode select.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profile slot select.
        /// </summary>
        /// <value>
        /// The profile slot select.
        /// </value>
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
        /// <summary>
        /// Gets or sets the ramp throttle.
        /// </summary>
        /// <value>
        /// The ramp throttle.
        /// </value>
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
        /// <summary>
        /// Gets or sets a value indicating whether [rev feedback sensor].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [rev feedback sensor]; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets or sets the limit switch en.
        /// </summary>
        /// <value>
        /// The limit switch en.
        /// </value>
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
        /// <summary>
        /// Gets or sets a value indicating whether [limit switch closed for].
        /// </summary>
        /// <value>
        /// <c>true</c> if [limit switch closed for]; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets or sets a value indicating whether [limit switch closed rev].
        /// </summary>
        /// <value>
        /// <c>true</c> if [limit switch closed rev]; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets or sets the sensor position.
        /// </summary>
        /// <value>
        /// The sensor position.
        /// </value>
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
        /// <summary>
        /// Gets or sets the sensor velocity.
        /// </summary>
        /// <value>
        /// The sensor velocity.
        /// </value>
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
        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
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
        /// <summary>
        /// Gets or sets a value indicating whether [brake is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [brake is enabled]; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets or sets the enc position.
        /// </summary>
        /// <value>
        /// The enc position.
        /// </value>
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
        /// <summary>
        /// Gets or sets the enc vel.
        /// </summary>
        /// <value>
        /// The enc vel.
        /// </value>
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
        /// <summary>
        /// Gets or sets the enc index rise events.
        /// </summary>
        /// <value>
        /// The enc index rise events.
        /// </value>
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
        /// <summary>
        /// Gets or sets the quad apin.
        /// </summary>
        /// <value>
        /// The quad apin.
        /// </value>
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
        /// <summary>
        /// Gets or sets the quad bpin.
        /// </summary>
        /// <value>
        /// The quad bpin.
        /// </value>
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
        /// <summary>
        /// Gets or sets the quad idxpin.
        /// </summary>
        /// <value>
        /// The quad idxpin.
        /// </value>
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
        /// <summary>
        /// Gets or sets the analog in with ov.
        /// </summary>
        /// <value>
        /// The analog in with ov.
        /// </value>
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
        /// <summary>
        /// Gets or sets the analog in vel.
        /// </summary>
        /// <value>
        /// The analog in vel.
        /// </value>
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
        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>
        /// The temporary.
        /// </value>
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
        /// <summary>
        /// Gets or sets the battery v.
        /// </summary>
        /// <value>
        /// The battery v.
        /// </value>
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
        /// <summary>
        /// Gets or sets the reset count.
        /// </summary>
        /// <value>
        /// The reset count.
        /// </value>
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
        /// <summary>
        /// Gets or sets the reset flags.
        /// </summary>
        /// <value>
        /// The reset flags.
        /// </value>
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
        /// <summary>
        /// Gets or sets the firm vers.
        /// </summary>
        /// <value>
        /// The firm vers.
        /// </value>
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
        /// <summary>
        /// Gets or sets the settings changed.
        /// </summary>
        /// <value>
        /// The settings changed.
        /// </value>
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
        /// <summary>
        /// Gets or sets the quad filter en.
        /// </summary>
        /// <value>
        /// The quad filter en.
        /// </value>
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
        /// <summary>
        /// Gets or sets the pid iaccum.
        /// </summary>
        /// <value>
        /// The pid iaccum.
        /// </value>
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
        /// <summary>
        /// Gets or sets the ain position.
        /// </summary>
        /// <value>
        /// The ain position.
        /// </value>
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

        /// <summary>
        /// Gets or sets the profile parameter slot0_ allowable closed loop error.
        /// </summary>
        /// <value>
        /// The profile parameter slot0_ allowable closed loop error.
        /// </value>
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

        /// <summary>
        /// Gets or sets the profile parameter slot1_ allowable closed loop error.
        /// </summary>
        /// <value>
        /// The profile parameter slot1_ allowable closed loop error.
        /// </value>
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

        /// <summary>
        /// Gets or sets the number encoder CPR.
        /// </summary>
        /// <value>
        /// The number encoder CPR.
        /// </value>
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

        /// <summary>
        /// Gets or sets the number pot turns.
        /// </summary>
        /// <value>
        /// The number pot turns.
        /// </value>
        public double NumberPotTurns
        {
            get { return m_eNumberPotTurns; }
            set
            {
                if (value == m_eNumberPotTurns) return;
                m_eNumberPotTurns = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the override limit switch.
        /// </summary>
        /// <value>
        /// The override limit switch.
        /// </value>
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

        /// <summary>
        /// Gets or sets the type of the override brake.
        /// </summary>
        /// <value>
        /// The type of the override brake.
        /// </value>
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

        /// <summary>
        /// Gets or sets the demand.
        /// </summary>
        /// <value>
        /// The demand.
        /// </value>
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
