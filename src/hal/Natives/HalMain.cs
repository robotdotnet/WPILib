using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalMain
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetMain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SetMain(void* param, delegate* unmanaged[Cdecl]<void*, void> mainFunc, delegate* unmanaged[Cdecl]<void*, void> exitFunc);

    [LibraryImport("wpiHal", EntryPoint = "HAL_HasMain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool HasMain();

    [LibraryImport("wpiHal", EntryPoint = "HAL_RunMain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RunMain();

    [LibraryImport("wpiHal", EntryPoint = "HAL_ExitMain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ExitMain();
}
