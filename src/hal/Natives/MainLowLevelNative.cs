using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class MainLowLevelNative
    {
        
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
