
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class MainLowLevel
    {
        internal static MainLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

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
