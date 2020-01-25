using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface IEncoder
    {
        [StatusCheckLastParameter]  void HAL_FreeEncoder(int encoderHandle);

        [StatusCheckLastParameter]  int HAL_GetEncoder(int encoderHandle);

        [StatusCheckLastParameter]  double HAL_GetEncoderDecodingScaleFactor(int encoderHandle);

        [StatusCheckLastParameter]  int HAL_GetEncoderDirection(int encoderHandle);

        [StatusCheckLastParameter]  double HAL_GetEncoderDistance(int encoderHandle);

        [StatusCheckLastParameter]  double HAL_GetEncoderDistancePerPulse(int encoderHandle);

        [StatusCheckLastParameter]  int HAL_GetEncoderEncodingScale(int encoderHandle);

        [StatusCheckLastParameter]  HAL_EncoderEncodingType HAL_GetEncoderEncodingType( int encoderHandle);

        [StatusCheckLastParameter]  int HAL_GetEncoderFPGAIndex(int encoderHandle);

        [StatusCheckLastParameter]  double HAL_GetEncoderPeriod(int encoderHandle);

        [StatusCheckLastParameter]  double HAL_GetEncoderRate(int encoderHandle);

        [StatusCheckLastParameter]  int HAL_GetEncoderRaw(int encoderHandle);

        [StatusCheckLastParameter]  int HAL_GetEncoderSamplesToAverage(int encoderHandle);

        [StatusCheckLastParameter]  int HAL_InitializeEncoder( int digitalSourceHandleA, HAL_AnalogTriggerType analogTriggerTypeA, int digitalSourceHandleB, HAL_AnalogTriggerType analogTriggerTypeB, int reverseDirection, HAL_EncoderEncodingType encodingType);

        [StatusCheckLastParameter]  void HAL_ResetEncoder(int encoderHandle);

        [StatusCheckLastParameter]  void HAL_SetEncoderIndexSource(int encoderHandle, int digitalSourceHandle, HAL_AnalogTriggerType analogTriggerType, HAL_EncoderIndexingType type);

        [StatusCheckLastParameter]  void HAL_SetEncoderMaxPeriod(int encoderHandle, double maxPeriod);

        [StatusCheckLastParameter]  void HAL_SetEncoderMinRate(int encoderHandle, double minRate);

        [StatusCheckLastParameter]  void HAL_SetEncoderReverseDirection(int encoderHandle, int reverseDirection);

        [StatusCheckLastParameter]  void HAL_SetEncoderSamplesToAverage(int encoderHandle, int samplesToAverage);

         void HAL_SetEncoderSimDevice(int handle, int device);

    }
}
