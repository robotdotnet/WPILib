using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalPorts
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumAccumulators")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumAccumulators();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumAddressableLEDs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumAddressableLEDs();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumAnalogInputs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumAnalogInputs();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumAnalogOutputs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumAnalogOutputs();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumAnalogTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumAnalogTriggers();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumCounters")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumCounters();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumDigitalChannels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumDigitalChannels();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumDigitalHeaders")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumDigitalHeaders();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumDigitalPWMOutputs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumDigitalPWMOutputs();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumDutyCycles")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumDutyCycles();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumEncoders")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumEncoders();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumInterrupts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumInterrupts();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumPWMChannels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumPWMChannels();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumPWMHeaders")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumPWMHeaders();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumRelayChannels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumRelayChannels();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetNumRelayHeaders")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumRelayHeaders();


}
