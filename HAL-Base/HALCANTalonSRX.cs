
namespace HAL_Base
{

    public partial class HALCanTalonSRX
    {
        public class Constants
        {

            public const int kDefaultControlPeriodMs = 10; //!< default control update rate is 10ms.
                                                           /* mode select enumerations */
            public const int kMode_DutyCycle = 0; //!< Demand is 11bit signed duty cycle [-1023,1023].
            public const int kMode_PositionCloseLoop = 1; //!< Position PIDF.
            public const int kMode_VelocityCloseLoop = 2; //!< Velocity PIDF.
            public const int kMode_CurrentCloseLoop = 3; //!< Current close loop - not done.
            public const int kMode_VoltCompen = 4; //!< Voltage Compensation Mode - not done.  Demand is fixed pt target 8.8 volts.
            public const int kMode_SlaveFollower = 5; //!< Demand is the 6 bit Device ID of the 'master' TALON SRX.
            public const int kMode_NoDrive = 15; //!< Zero the output (honors brake/coast) regardless of demand.  Might be useful if we need to change modes but can't atomically change all the signals we want in between.
                                                 /* limit switch enumerations */
            public const int kLimitSwitchOverride_UseDefaultsFromFlash = 1;
            public const int kLimitSwitchOverride_DisableFwd_DisableRev = 4;
            public const int kLimitSwitchOverride_DisableFwd_EnableRev = 5;
            public const int kLimitSwitchOverride_EnableFwd_DisableRev = 6;
            public const int kLimitSwitchOverride_EnableFwd_EnableRev = 7;
            /* brake override enumerations */
            public const int kBrakeOverride_UseDefaultsFromFlash = 0;
            public const int kBrakeOverride_OverrideCoast = 1;
            public const int kBrakeOverride_OverrideBrake = 2;
            /* feedback device enumerations */
            public const int kFeedbackDev_DigitalQuadEnc = 0;
            public const int kFeedbackDev_AnalogPot = 2;
            public const int kFeedbackDev_AnalogEncoder = 3;
            public const int kFeedbackDev_CountEveryRisingEdge = 4;
            public const int kFeedbackDev_CountEveryFallingEdge = 5;
            public const int kFeedbackDev_PosIsPulseWidth = 8;
            /* ProfileSlotSelect enumerations*/
            public const int kProfileSlotSelect_Slot0 = 0;
            public const int kProfileSlotSelect_Slot1 = 1;
            /* status frame rate types */
            public const int kStatusFrame_General = 0;
            public const int kStatusFrame_Feedback = 1;
            public const int kStatusFrame_Encoder = 2;
            public const int kStatusFrame_AnalogTempVbat = 3;
        }

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
