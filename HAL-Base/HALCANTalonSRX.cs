
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public enum CTR_Code
    {
        CTR_OKAY,

        CTR_RxTimeout,

        CTR_TxTimeout,

        CTR_InvalidParamValue,

        CTR_UnexpectedArbId,

        CTR_TxFailed,

        CTR_SigNotUpdated,
    }
    public partial class HALCanTalonSRX
    {
        public enum ParamID
        {
            eProfileParamSlot0_P = 1,
            eProfileParamSlot0_I = 2,
            eProfileParamSlot0_D = 3,
            eProfileParamSlot0_F = 4,
            eProfileParamSlot0_IZone = 5,
            eProfileParamSlot0_CloseLoopRampRate = 6,
            eProfileParamSlot1_P = 11,
            eProfileParamSlot1_I = 12,
            eProfileParamSlot1_D = 13,
            eProfileParamSlot1_F = 14,
            eProfileParamSlot1_IZone = 15,
            eProfileParamSlot1_CloseLoopRampRate = 16,
            eProfileParamSoftLimitForThreshold = 21,
            eProfileParamSoftLimitRevThreshold = 22,
            eProfileParamSoftLimitForEnable = 23,
            eProfileParamSoftLimitRevEnable = 24,
            eOnBoot_BrakeMode = 31,
            eOnBoot_LimitSwitch_Forward_NormallyClosed = 32,
            eOnBoot_LimitSwitch_Reverse_NormallyClosed = 33,
            eOnBoot_LimitSwitch_Forward_Disable = 34,
            eOnBoot_LimitSwitch_Reverse_Disable = 35,
            eFault_OverTemp = 41,
            eFault_UnderVoltage = 42,
            eFault_ForLim = 43,
            eFault_RevLim = 44,
            eFault_HardwareFailure = 45,
            eFault_ForSoftLim = 46,
            eFault_RevSoftLim = 47,
            eStckyFault_OverTemp = 48,
            eStckyFault_UnderVoltage = 49,
            eStckyFault_ForLim = 50,
            eStckyFault_RevLim = 51,
            eStckyFault_ForSoftLim = 52,
            eStckyFault_RevSoftLim = 53,
            eAppliedThrottle = 61,
            eCloseLoopErr = 62,
            eFeedbackDeviceSelect = 63,
            eRevMotDuringCloseLoopEn = 64,
            eModeSelect = 65,
            eProfileSlotSelect = 66,
            eRampThrottle = 67,
            eRevFeedbackSensor = 68,
            eLimitSwitchEn = 69,
            eLimitSwitchClosedFor = 70,
            eLimitSwitchClosedRev = 71,
            eSensorPosition = 73,
            eSensorVelocity = 74,
            eCurrent = 75,
            eBrakeIsEnabled = 76,
            eEncPosition = 77,
            eEncVel = 78,
            eEncIndexRiseEvents = 79,
            eQuadApin = 80,
            eQuadBpin = 81,
            eQuadIdxpin = 82,
            eAnalogInWithOv = 83,
            eAnalogInVel = 84,
            eTemp = 85,
            eBatteryV = 86,
            eResetCount = 87,
            eResetFlags = 88,
            eFirmVers = 89,
            eSettingsChanged = 90,
            eQuadFilterEn = 91,
            ePidIaccum = 93,
        }
    }
}
