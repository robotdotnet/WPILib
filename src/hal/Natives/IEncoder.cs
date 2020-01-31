using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface IEncoder
    {
        [StatusCheckLastParameter] void HAL_FreeEncoder(int encoderHandle);

        [StatusCheckLastParameter] int HAL_GetEncoder(int encoderHandle);

        [StatusCheckLastParameter] double HAL_GetEncoderDecodingScaleFactor(int encoderHandle);

        [StatusCheckLastParameter] int HAL_GetEncoderDirection(int encoderHandle);

        [StatusCheckLastParameter] double HAL_GetEncoderDistance(int encoderHandle);

        [StatusCheckLastParameter] double HAL_GetEncoderDistancePerPulse(int encoderHandle);

        [StatusCheckLastParameter] int HAL_GetEncoderEncodingScale(int encoderHandle);

        [StatusCheckLastParameter] EncoderEncodingType HAL_GetEncoderEncodingType(int encoderHandle);

        [StatusCheckLastParameter] int HAL_GetEncoderFPGAIndex(int encoderHandle);

        [StatusCheckLastParameter] double HAL_GetEncoderPeriod(int encoderHandle);

        [StatusCheckLastParameter] double HAL_GetEncoderRate(int encoderHandle);

        [StatusCheckLastParameter] int HAL_GetEncoderRaw(int encoderHandle);

        [StatusCheckLastParameter] int HAL_GetEncoderSamplesToAverage(int encoderHandle);

        [StatusCheckLastParameter] int HAL_InitializeEncoder(int digitalSourceHandleA, AnalogTriggerType analogTriggerTypeA, int digitalSourceHandleB, AnalogTriggerType analogTriggerTypeB, int reverseDirection, EncoderEncodingType encodingType);

        [StatusCheckLastParameter] void HAL_ResetEncoder(int encoderHandle);

        [StatusCheckLastParameter] void HAL_SetEncoderIndexSource(int encoderHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type);

        [StatusCheckLastParameter] void HAL_SetEncoderMaxPeriod(int encoderHandle, double maxPeriod);

        [StatusCheckLastParameter] void HAL_SetEncoderMinRate(int encoderHandle, double minRate);

        [StatusCheckLastParameter] void HAL_SetEncoderReverseDirection(int encoderHandle, int reverseDirection);

        [StatusCheckLastParameter] void HAL_SetEncoderSamplesToAverage(int encoderHandle, int samplesToAverage);

        void HAL_SetEncoderSimDevice(int handle, int device);

    }
}
