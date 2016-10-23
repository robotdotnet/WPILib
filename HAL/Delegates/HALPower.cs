using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALPower
    {
        static HALPower()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALPower>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetVinVoltageDelegate(ref int status);
        [NativeDelegate] public static HAL_GetVinVoltageDelegate HAL_GetVinVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetVinCurrentDelegate(ref int status);
        [NativeDelegate] public static HAL_GetVinCurrentDelegate HAL_GetVinCurrent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetUserVoltage6VDelegate(ref int status);
        [NativeDelegate] public static HAL_GetUserVoltage6VDelegate HAL_GetUserVoltage6V;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetUserCurrent6VDelegate(ref int status);
        [NativeDelegate] public static HAL_GetUserCurrent6VDelegate HAL_GetUserCurrent6V;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetUserActive6VDelegate(ref int status);
        [NativeDelegate] public static HAL_GetUserActive6VDelegate HAL_GetUserActive6V;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetUserCurrentFaults6VDelegate(ref int status);
        [NativeDelegate] public static HAL_GetUserCurrentFaults6VDelegate HAL_GetUserCurrentFaults6V;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetUserVoltage5VDelegate(ref int status);
        [NativeDelegate] public static HAL_GetUserVoltage5VDelegate HAL_GetUserVoltage5V;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetUserCurrent5VDelegate(ref int status);
        [NativeDelegate] public static HAL_GetUserCurrent5VDelegate HAL_GetUserCurrent5V;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetUserActive5VDelegate(ref int status);
        [NativeDelegate] public static HAL_GetUserActive5VDelegate HAL_GetUserActive5V;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetUserCurrentFaults5VDelegate(ref int status);
        [NativeDelegate] public static HAL_GetUserCurrentFaults5VDelegate HAL_GetUserCurrentFaults5V;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetUserVoltage3V3Delegate(ref int status);
        [NativeDelegate] public static HAL_GetUserVoltage3V3Delegate HAL_GetUserVoltage3V3;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetUserCurrent3V3Delegate(ref int status);
        [NativeDelegate] public static HAL_GetUserCurrent3V3Delegate HAL_GetUserCurrent3V3;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetUserActive3V3Delegate(ref int status);
        [NativeDelegate] public static HAL_GetUserActive3V3Delegate HAL_GetUserActive3V3;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetUserCurrentFaults3V3Delegate(ref int status);
        [NativeDelegate] public static HAL_GetUserCurrentFaults3V3Delegate HAL_GetUserCurrentFaults3V3;
    }
}

