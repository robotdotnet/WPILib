using REV.SparkMax.Natives;

namespace REV.SparkMax
{
    public struct APIVersion
    {
        public byte Major;
        public byte Minor;
        public byte Build;
        public uint Version;
    }

    public enum LimitDirection
    {
        kForward = 0,
        kReverse = 1
    }

    public enum LimitPolarity
    {
        kNormallyOpen,
        kNormallyClosed
    }

    public enum EncoderType
    {
        kNoSensor,
        kHallSensor,
        kQuadrature,
        kSensorless,
        kAlternateQuadrature
    }

    public enum IdleMode
    {
        kCoast,
        kBrake
    }

    public enum InputMode
    {
        kPWM,
        kCAN
    }

    public enum FaultID
    {
        kBrownout = 0,
        kOvercurrent = 1,
        kIWDTReset = 2,
        kMotorFault = 3,
        kSensorFault = 4,
        kStall = 5,
        kEEPROMCRC = 6,
        kCANTX = 7,
        kCANRX = 8,
        kHasReset = 9,
        kDRVFault = 10,
        kOtherFault = 11,
        kSoftLimitFwd = 12,
        kSoftLimitRev = 13,
        kHardLimitFwd = 14,
        kHardLimitRev = 15
    }

    public enum MotorType
    {
        kBrushed,
        kBrushless
    }

    public enum ParameterStatus
    {
        kParamOK = 0,
        kInvalidID = 1,
        kMismatchType = 2,
        kAccessMode = 3,
        kInvalid = 4,
        kNotImplementedDeprecated = 5
    }

    public enum ConfigParameter
    {
        kCanID = 0,
        kInputMode = 1,
        kMotorType = 2,
        kCommAdvance = 3,
        kSensorType = 4,
        kCtrlType = 5,
        kIdleMode = 6,
        kInputDeadband = 7,
        kFeedbackSensorPID0 = 8,
        kFeedbackSensorPID1 = 9,
        kPolePairs = 10,
        kCurrentChop = 11,
        kCurrentChopCycles = 12,
        kP_0 = 13,
        kI_0 = 14,
        kD_0 = 15,
        kF_0 = 16,
        kIZone_0 = 17,
        kDFilter_0 = 18,
        kOutputMin_0 = 19,
        kOutputMax_0 = 20,
        kP_1 = 21,
        kI_1 = 22,
        kD_1 = 23,
        kF_1 = 24,
        kIZone_1 = 25,
        kDFilter_1 = 26,
        kOutputMin_1 = 27,
        kOutputMax_1 = 28,
        kP_2 = 29,
        kI_2 = 30,
        kD_2 = 31,
        kF_2 = 32,
        kIZone_2 = 33,
        kDFilter_2 = 34,
        kOutputMin_2 = 35,
        kOutputMax_2 = 36,
        kP_3 = 37,
        kI_3 = 38,
        kD_3 = 39,
        kF_3 = 40,
        kIZone_3 = 41,
        kDFilter_3 = 42,
        kOutputMin_3 = 43,
        kOutputMax_3 = 44,
        kInverted = 45,
        kOutputRatio = 46,
        kSerialNumberLow = 47,
        kSerialNumberMid = 48,
        kSerialNumberHigh = 49,
        kLimitSwitchFwdPolarity = 50,
        kLimitSwitchRevPolarity = 51,
        kHardLimitFwdEn = 52,
        kHardLimitRevEn = 53,
        kSoftLimitFwdEn = 54,
        kSoftLimitRevEn = 55,
        kRampRate = 56,
        kFollowerID = 57,
        kFollowerConfig = 58,
        kSmartCurrentStallLimit = 59,
        kSmartCurrentFreeLimit = 60,
        kSmartCurrentConfig = 61,
        kSmartCurrentReserved = 62,
        kMotorKv = 63,
        kMotorR = 64,
        kMotorL = 65,
        kMotorRsvd1 = 66,
        kMotorRsvd2 = 67,
        kMotorRsvd3 = 68,
        kEncoderCountsPerRev = 69,
        kEncoderAverageDepth = 70,
        kEncoderSampleDelta = 71,
        kEncoderInverted = 72,
        kEncoderRsvd1 = 73,
        kVoltageCompMode = 74,
        kCompensatedNominalVoltage = 75,
        kSmartMotionMaxVelocity_0 = 76,
        kSmartMotionMaxAccel_0 = 77,
        kSmartMotionMinVelOutput_0 = 78,
        kSmartMotionAllowedClosedLoopError_0 = 79,
        kSmartMotionAccelStrategy_0 = 80,
        kSmartMotionMaxVelocity_1 = 81,
        kSmartMotionMaxAccel_1 = 82,
        kSmartMotionMinVelOutput_1 = 83,
        kSmartMotionAllowedClosedLoopError_1 = 84,
        kSmartMotionAccelStrategy_1 = 85,
        kSmartMotionMaxVelocity_2 = 86,
        kSmartMotionMaxAccel_2 = 87,
        kSmartMotionMinVelOutput_2 = 88,
        kSmartMotionAllowedClosedLoopError_2 = 89,
        kSmartMotionAccelStrategy_2 = 90,
        kSmartMotionMaxVelocity_3 = 91,
        kSmartMotionMaxAccel_3 = 92,
        kSmartMotionMinVelOutput_3 = 93,
        kSmartMotionAllowedClosedLoopError_3 = 94,
        kSmartMotionAccelStrategy_3 = 95,
        kIMaxAccum_0 = 96,
        kSlot3Placeholder1_0 = 97,
        kSlot3Placeholder2_0 = 98,
        kSlot3Placeholder3_0 = 99,
        kIMaxAccum_1 = 100,
        kSlot3Placeholder1_1 = 101,
        kSlot3Placeholder2_1 = 102,
        kSlot3Placeholder3_1 = 103,
        kIMaxAccum_2 = 104,
        kSlot3Placeholder1_2 = 105,
        kSlot3Placeholder2_2 = 106,
        kSlot3Placeholder3_2 = 107,
        kIMaxAccum_3 = 108,
        kSlot3Placeholder1_3 = 109,
        kSlot3Placeholder2_3 = 110,
        kSlot3Placeholder3_3 = 111,
        kPositionConversionFactor = 112,
        kVelocityConversionFactor = 113,
        kClosedLoopRampRate = 114,
        kSoftLimitFwd = 115,
        kSoftLimitRev = 116,
        kSoftLimitRsvd0 = 117,
        kSoftLimitRsvd1 = 118,
        kAnalogRevPerVolt = 119,
        kAnalogRotationsPerVoltSec = 120,
        kAnalogAverageDepth = 121,
        kAnalogSensorMode = 122,
        kAnalogInverted = 123,
        kAnalogSampleDelta = 124,
        kAnalogRsvd0 = 125,
        kAnalogRsvd1 = 126,
        kDataPortConfig = 127,
        kAltEncoderCountsPerRev = 128,
        kAltEncoderAverageDepth = 129,
        kAltEncoderSampleDelta = 130,
        kAltEncoderInverted = 131,
        kAltEncodePositionFactor = 132,
        kAltEncoderVelocityFactor = 133
    }

    public enum ParameterType
    {
        kInt32,
        kUint32,
        kFloat32,
        kBool
    }

    public enum PeriodicFrame
    {
        kStatus0,
        kStatus1,
        kStatus2,
        kStatus3,
        kStatus4,
    }

    public enum ControlType
    {
        kDutyCycle = 0,
        kVelocity = 1,
        kVoltage = 2,
        kPosition = 3,
        kSmartMotion = 4,
        kCurrent = 5,
        kSmartVelocity = 6
    }

    public enum DataPortConfig
    {
        kDefault,
        kAltEncoder
    }

    public readonly struct PeriodicStatus0
    {
        public float AppliedOutput { get; }
        public ushort Faults { get; }
        public ushort StickyFaults { get; }
        public MotorType MotorType { get; }
        public bool IsFollower { get; }
        public bool IsInverted { get; }
        public bool Lock { get; }
        public bool RoboRIO { get; }
        public ulong Timestamp { get; }

        public PeriodicStatus0(PeriodicStatus0LowLevel lowLevel)
        {
            AppliedOutput = lowLevel.AppliedOutput;
            Faults = lowLevel.Faults;
            StickyFaults = lowLevel.StickyFaults;
            MotorType = lowLevel.MotorType;
            IsFollower = lowLevel.IsFollower != 0;
            IsInverted = lowLevel.IsInverted != 0;
            Lock = lowLevel.Lock != 0;
            RoboRIO = lowLevel.RoboRIO != 0;
            Timestamp = lowLevel.Timestamp;
        }
    }

    public struct PeriodicStatus1
    {
        public float SensorVelocity;
        public byte MotorTemperature;
        public float BusVoltage;
        public float OutputCurrrent;
        public ulong Timestamp;
    }

    public struct PeriodicStatus2
    {
        public float SensorPostion;
        public float IAccum;
        public ulong Timestamp;
    }

    public struct PeriodicStatus3
    {
        public float AnalogVoltage;
        public float AnalogVelocity;
        public float AnalogPosition;
        public ulong Timestamp;
    }

    public struct PeriodicStatus4
    {
        public float AltEncoderPosition;
        public float AltEncoderVelocity;
        public ulong Timestamp;
    }

    public struct FirmwareVersion
    {
        public byte Major;
        public byte Minor;
        public ushort Build;
        public bool IsDebug;
        public uint VersionRaw;
    }

    public enum AccelStrategy
    {
        kStrategyTrapezoidal = 0,
        kStrategySCurve = 1
    }

    public enum AnalogMode
    {
        kAbsolute,
        kRelative
    }

    public struct DRVStatus
    {
        public ushort DRVStat0;
        public ushort DRVStat1;
        public ushort Faults;
        public ushort StickyFaults;
    }

    public enum ErrorCode
    {
        ErrorNone = 0,
        ErrorGeneral,
        ErrorCANTimeout,
        ErrorNotImplmented,
        ErrorHAL,
        ErrorCantFindFirmware,
        ErrorFirmwareTooOld,
        ErrorFirmwareTooNew,
        ErrorParamInvalidID,
        ErrorParamMismatchType,
        ErrorParamAccessMode,
        ErrorParamInvalid,
        ErrorParamNotImplementedDeprecated,
        ErrorFollowConfigMismatch,
        ErrorInvalid,
        ErrorSetpointOutOfRange,
        NumErrorCodes
    }
}
