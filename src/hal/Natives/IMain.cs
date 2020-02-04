using System;
using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IMain
    {
        void HAL_SetMain(void* param, IntPtr mainFunc, IntPtr exitFunc);

        int HAL_HasMain();

        void HAL_RunMain();

        void HAL_ExitMain();

    }
}
