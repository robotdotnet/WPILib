using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class MainLowLevelNative
    {
        public MainLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_SetMainFunc = (delegate* unmanaged[Cdecl] < void *, delegate* unmanaged[Cdecl] < void *, void >, delegate* unmanaged[Cdecl] < void *, void >, void >)loader.GetProcAddress("HAL_SetMain");
            HAL_HasMainFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_HasMain");
            HAL_RunMainFunc = (delegate* unmanaged[Cdecl] < void >)loader.GetProcAddress("HAL_RunMain");
            HAL_ExitMainFunc = (delegate* unmanaged[Cdecl] < void >)loader.GetProcAddress("HAL_ExitMain");
        }

        private readonly delegate* unmanaged[Cdecl]<void*, delegate* unmanaged[Cdecl]<void*, void>, delegate* unmanaged[Cdecl]<void*, void>, void> HAL_SetMainFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetMain(void* param, delegate* unmanaged[Cdecl]<void*, void> mainFunc, delegate* unmanaged[Cdecl]<void*, void> exitFunc)
        {
            HAL_SetMainFunc(param, mainFunc, exitFunc);
    }



    private readonly delegate* unmanaged[Cdecl]<int> HAL_HasMainFunc;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int HAL_HasMain()
    {
        return HAL_HasMainFunc();
    }



    private readonly delegate* unmanaged[Cdecl]<void> HAL_RunMainFunc;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HAL_RunMain()
    {
        HAL_RunMainFunc();
    }



    private readonly delegate* unmanaged[Cdecl]<void> HAL_ExitMainFunc;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HAL_ExitMain()
    {
        HAL_ExitMainFunc();
    }



}
}
