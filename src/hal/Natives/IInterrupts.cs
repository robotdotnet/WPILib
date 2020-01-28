using System;
using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface IInterrupts
    {
        [StatusCheckLastParameter]  void HAL_AttachInterruptHandler(int interruptHandle, IntPtr handler, void* param);

        [StatusCheckLastParameter]  void HAL_AttachInterruptHandlerThreaded(int interruptHandle, IntPtr handler, void* param);

        [StatusCheckLastParameter]  void* HAL_CleanInterrupts(int interruptHandle);

        [StatusCheckLastParameter]  void HAL_DisableInterrupts(int interruptHandle);

        [StatusCheckLastParameter]  void HAL_EnableInterrupts(int interruptHandle);

        [StatusCheckLastParameter]  int HAL_InitializeInterrupts(int watcher);

        [StatusCheckLastParameter]  long HAL_ReadInterruptFallingTimestamp(int interruptHandle);

        [StatusCheckLastParameter]  long HAL_ReadInterruptRisingTimestamp(int interruptHandle);

        [StatusCheckLastParameter]  void HAL_RequestInterrupts(int interruptHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType);

        [StatusCheckLastParameter]  void HAL_SetInterruptUpSourceEdge(int interruptHandle, int risingEdge, int fallingEdge);

        [StatusCheckLastParameter]  long HAL_WaitForInterrupt(int interruptHandle, double timeout, int ignorePrevious);

    }
}
