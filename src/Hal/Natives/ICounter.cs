using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface ICounter
    {
        [StatusCheckLastParameter] int HAL_GetCounter(int counterHandle);

        [StatusCheckLastParameter]  void HAL_ClearCounterDownSource(int counterHandle);

        [StatusCheckLastParameter]  void HAL_ClearCounterUpSource(int counterHandle);

        [StatusCheckLastParameter]  void HAL_FreeCounter(int counterHandle);

        [StatusCheckLastParameter]  int HAL_GetCounterSamplesToAverage(int counterHandle);

        [StatusCheckLastParameter]  int HAL_GetCounterDirection(int counterHandle);

        [StatusCheckLastParameter]  double HAL_GetCounterPeriod(int counterHandle);

        [StatusCheckLastParameter]  int HAL_InitializeCounter(CounterMode mode, int* index);

        [StatusCheckLastParameter]  void HAL_ResetCounter(int counterHandle);

        [StatusCheckLastParameter]  void HAL_SetCounterAverageSize(int counterHandle, int size);

        [StatusCheckLastParameter]  void HAL_SetCounterDownSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType);

        [StatusCheckLastParameter]  void HAL_SetCounterDownSourceEdge(int counterHandle, int risingEdge, int fallingEdge);

        [StatusCheckLastParameter]  void HAL_SetCounterExternalDirectionMode(int counterHandle);

        [StatusCheckLastParameter]  void HAL_SetCounterMaxPeriod(int counterHandle, double maxPeriod);

        [StatusCheckLastParameter]  void HAL_SetCounterPulseLengthMode(int counterHandle, double threshold);

        [StatusCheckLastParameter]  void HAL_SetCounterReverseDirection(int counterHandle, int reverseDirection);

        [StatusCheckLastParameter]  void HAL_SetCounterSamplesToAverage(int counterHandle, int samplesToAverage);

        [StatusCheckLastParameter]  void HAL_SetCounterSemiPeriodMode(int counterHandle, int highSemiPeriod);

        [StatusCheckLastParameter]  void HAL_SetCounterUpDownMode(int counterHandle);

        [StatusCheckLastParameter]  void HAL_SetCounterUpSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType);

        [StatusCheckLastParameter]  void HAL_SetCounterUpSourceEdge(int counterHandle, int risingEdge, int fallingEdge);

        [StatusCheckLastParameter]  void HAL_SetCounterUpdateWhenEmpty(int counterHandle, int enabled);

        [StatusCheckLastParameter] int HAL_GetCounterStopped(int counterHandle);

    }
}
