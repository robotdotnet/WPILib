namespace WPIHal.Natives;

public static partial class HalMain
{
    // [LibraryImport("wpiHal", EntryPoint = "@param exitFunc the function to be run when HAL_ExitMain")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial* @param exitFunc the function to be run when ExitMain() is called. */ void SetMain(void* param, void (* mainFunc)(void*), void (* exitFunc)(void*));

    // [LibraryImport("wpiHal", EntryPoint = "HAL_HasMain")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial int HasMain();

    // [LibraryImport("wpiHal", EntryPoint = "@param mainFunc the function to be run when HAL_RunMain")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial* @param mainFunc the function to be run when RunMain() is called. * @param exitFunc the function to be run when ExitMain() is called. */ void SetMain(void* param, void (* mainFunc)(void*), void (* exitFunc)(void*));

    // [LibraryImport("wpiHal", EntryPoint = "HAL_SetMain")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial void SetMain(void* param, void (* mainFunc)(void*), void (* exitFunc)(void*));


}
