
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class MainLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static MainLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

        public static void Set(void* param, delegate* unmanaged[Cdecl]<void*, void> mainFunc, delegate* unmanaged[Cdecl]<void*, void> exitFunc)
        {
            lowLevel.HAL_SetMain(param, mainFunc, exitFunc);
        }

    public static bool HasMain()
    {
        return lowLevel.HAL_HasMain() != 0;
    }

    public static void RunMain()
    {
        lowLevel.HAL_RunMain();
    }

    public static void ExitMain()
    {
        lowLevel.HAL_ExitMain();
    }

}
}
