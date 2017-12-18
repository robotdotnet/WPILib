using System;
using System.Runtime.InteropServices;
using NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALNotifier
    {
        static HALNotifier()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALNotifier>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeNotifierDelegate(ref int status);
        [NativeDelegate] public static HAL_InitializeNotifierDelegate HAL_InitializeNotifier;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_StopNotifierDelegate(int notifier_handle, ref int status);
        [NativeDelegate] public static HAL_StopNotifierDelegate HAL_StopNotifier;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_CleanNotifierDelegate(int notifier_handle, ref int status);
        [NativeDelegate] public static HAL_CleanNotifierDelegate HAL_CleanNotifier;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_UpdateNotifierAlarmDelegate(int notifier_handle, ulong triggerTime, ref int status);
        [NativeDelegate] public static HAL_UpdateNotifierAlarmDelegate HAL_UpdateNotifierAlarm;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_CancelNotifierAlarmDelegate(int notifier_handle, ref int status);
        [NativeDelegate] public static HAL_CancelNotifierAlarmDelegate HAL_CancelNotifierAlarm;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate ulong HAL_WaitForNotifierAlarmDelegate(int notifier_handle, ref int status);
        [NativeDelegate] public static HAL_WaitForNotifierAlarmDelegate HAL_WaitForNotifierAlarm;
    }
}

