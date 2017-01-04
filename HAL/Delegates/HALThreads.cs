using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HAL.Base;
using NativeLibraryUtilities;

namespace HAL.Base
{
    public class HALThreads
    {
        static HALThreads()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALThreads>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int HAL_GetCurrentThreadPriorityDelegate([MarshalAs(UnmanagedType.Bool)]ref bool isRealTime, ref int status);
        [NativeDelegate]
        public static HAL_GetCurrentThreadPriorityDelegate HAL_GetCurrentThreadPriority;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public delegate bool HAL_SetCurrentThreadPriorityDelegate([MarshalAs(UnmanagedType.Bool)]bool realTime, int priority, ref int status);
        [NativeDelegate]
        public static HAL_SetCurrentThreadPriorityDelegate HAL_SetCurrentThreadPriority;
    }
}
