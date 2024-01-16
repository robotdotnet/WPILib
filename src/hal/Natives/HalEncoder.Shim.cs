using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalEncoder
{
    public static void FreeEncoder(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        FreeEncoderRefShim(encoderHandle, ref status);
    }
    public static int GetEncoder(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderRefShim(encoderHandle, ref status);
    }
    public static double GetEncoderDecodingScaleFactor(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderDecodingScaleFactorRefShim(encoderHandle, ref status);
    }
    public static int GetEncoderDirection(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderDirectionRefShim(encoderHandle, ref status);
    }
    public static double GetEncoderDistance(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderDistanceRefShim(encoderHandle, ref status);
    }
    public static double GetEncoderDistancePerPulse(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderDistancePerPulseRefShim(encoderHandle, ref status);
    }
    public static int GetEncoderEncodingScale(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderEncodingScaleRefShim(encoderHandle, ref status);
    }
    public static EncoderEncodingType GetEncoderEncodingType(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderEncodingTypeRefShim(encoderHandle, ref status);
    }
    public static int GetEncoderFPGAIndex(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderFPGAIndexRefShim(encoderHandle, ref status);
    }
    public static double GetEncoderPeriod(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderPeriodRefShim(encoderHandle, ref status);
    }
    public static double GetEncoderRate(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderRateRefShim(encoderHandle, ref status);
    }
    public static int GetEncoderRaw(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderRawRefShim(encoderHandle, ref status);
    }
    public static int GetEncoderSamplesToAverage(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderSamplesToAverageRefShim(encoderHandle, ref status);
    }
    public static int GetEncoderStopped(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetEncoderStoppedRefShim(encoderHandle, ref status);
    }
    public static HalEncoderHandle InitializeEncoder(HalAnalogTriggerHandle digitalSourceHandleA, AnalogTriggerType analogTriggerTypeA, HalAnalogTriggerHandle digitalSourceHandleB, AnalogTriggerType analogTriggerTypeB, int reverseDirection, EncoderEncodingType encodingType, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeEncoderRefShim(digitalSourceHandleA, analogTriggerTypeA, digitalSourceHandleB, analogTriggerTypeB, reverseDirection, encodingType, ref status);
    }
    public static void ResetEncoder(HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        ResetEncoderRefShim(encoderHandle, ref status);
    }
    public static void SetEncoderDistancePerPulse(HalEncoderHandle encoderHandle, double distancePerPulse, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetEncoderDistancePerPulseRefShim(encoderHandle, distancePerPulse, ref status);
    }
    internal static void SetEncoderIndexSource(HalEncoderHandle encoderHandle, HalDigitalHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetEncoderIndexSourceRefShim(encoderHandle, digitalSourceHandle, analogTriggerType, type, ref status);
    }
    public static void SetEncoderIndexSource(HalEncoderHandle encoderHandle, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetEncoderIndexSourceRefShim(encoderHandle, digitalSourceHandle, analogTriggerType, type, ref status);
    }
    public static void SetEncoderMaxPeriod(HalEncoderHandle encoderHandle, double maxPeriod, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetEncoderMaxPeriodRefShim(encoderHandle, maxPeriod, ref status);
    }
    public static void SetEncoderMinRate(HalEncoderHandle encoderHandle, double minRate, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetEncoderMinRateRefShim(encoderHandle, minRate, ref status);
    }
    public static void SetEncoderReverseDirection(HalEncoderHandle encoderHandle, int reverseDirection, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetEncoderReverseDirectionRefShim(encoderHandle, reverseDirection, ref status);
    }
    public static void SetEncoderSamplesToAverage(HalEncoderHandle encoderHandle, int samplesToAverage, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetEncoderSamplesToAverageRefShim(encoderHandle, samplesToAverage, ref status);
    }
}
