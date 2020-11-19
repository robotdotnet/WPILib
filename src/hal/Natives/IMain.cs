using System;
using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IMain
    {
        void HAL_SetMain(void* param, delegate* unmanaged[Cdecl]<void*, void> mainFunc, delegate* unmanaged[Cdecl]<void*, void> exitFunc);

        int HAL_HasMain();

        void HAL_RunMain();

        void HAL_ExitMain();

    }
}
