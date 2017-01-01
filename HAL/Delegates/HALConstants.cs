using System.Runtime.InteropServices;
using NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALConstants
    {
        static HALConstants()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALConstants>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetSystemClockTicksPerMicrosecondDelegate();
        [NativeDelegate] public static HAL_GetSystemClockTicksPerMicrosecondDelegate HAL_GetSystemClockTicksPerMicrosecond;
    }
}

