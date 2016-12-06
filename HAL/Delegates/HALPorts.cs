using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALPorts
    {
        static HALPorts()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALPorts>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumAccumulatorsDelegate();
        [NativeDelegate] public static HAL_GetNumAccumulatorsDelegate HAL_GetNumAccumulators;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumAnalogTriggersDelegate();
        [NativeDelegate] public static HAL_GetNumAnalogTriggersDelegate HAL_GetNumAnalogTriggers;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumAnalogInputsDelegate();
        [NativeDelegate] public static HAL_GetNumAnalogInputsDelegate HAL_GetNumAnalogInputs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumAnalogOutputsDelegate();
        [NativeDelegate] public static HAL_GetNumAnalogOutputsDelegate HAL_GetNumAnalogOutputs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumCountersDelegate();
        [NativeDelegate] public static HAL_GetNumCountersDelegate HAL_GetNumCounters;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumDigitalHeadersDelegate();
        [NativeDelegate] public static HAL_GetNumDigitalHeadersDelegate HAL_GetNumDigitalHeaders;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumPWMHeadersDelegate();
        [NativeDelegate] public static HAL_GetNumPWMHeadersDelegate HAL_GetNumPWMHeaders;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumDigitalChannelsDelegate();
        [NativeDelegate] public static HAL_GetNumDigitalChannelsDelegate HAL_GetNumDigitalChannels;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumPWMChannelsDelegate();
        [NativeDelegate] public static HAL_GetNumPWMChannelsDelegate HAL_GetNumPWMChannels;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumDigitalPWMOutputsDelegate();
        [NativeDelegate] public static HAL_GetNumDigitalPWMOutputsDelegate HAL_GetNumDigitalPWMOutputs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumEncodersDelegate();
        [NativeDelegate] public static HAL_GetNumEncodersDelegate HAL_GetNumEncoders;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumInterruptsDelegate();
        [NativeDelegate] public static HAL_GetNumInterruptsDelegate HAL_GetNumInterrupts;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumRelayChannelsDelegate();
        [NativeDelegate] public static HAL_GetNumRelayChannelsDelegate HAL_GetNumRelayChannels;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumRelayHeadersDelegate();
        [NativeDelegate] public static HAL_GetNumRelayHeadersDelegate HAL_GetNumRelayHeaders;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumPCMModulesDelegate();
        [NativeDelegate] public static HAL_GetNumPCMModulesDelegate HAL_GetNumPCMModules;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumSolenoidChannelsDelegate();
        [NativeDelegate] public static HAL_GetNumSolenoidChannelsDelegate HAL_GetNumSolenoidChannels;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumPDPModulesDelegate();
        [NativeDelegate] public static HAL_GetNumPDPModulesDelegate HAL_GetNumPDPModules;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetNumPDPChannelsDelegate();
        [NativeDelegate] public static HAL_GetNumPDPChannelsDelegate HAL_GetNumPDPChannels;
    }
}

