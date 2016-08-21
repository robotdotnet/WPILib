using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    /// <summary>
    /// This attributes are placed on strings we want to force be allowed in the impl test.
    /// </summary>
    public class HALAllowNonBlittable : Attribute { }
    public partial class HAL
    {
        static HAL()
        {
            HAL.Initialize();
        }

        public delegate int HAL_GetPortDelegate(int pin);
        public static HAL_GetPortDelegate HAL_GetPort;

        public delegate int HAL_GetPortWithModuleDelegate(int module, int pin);
        public static HAL_GetPortWithModuleDelegate HAL_GetPortWithModule;

        [HALAllowNonBlittable]
        public delegate string HAL_GetErrorMessageDelegate(int code);
        public static HAL_GetErrorMessageDelegate HAL_GetErrorMessage;

        public delegate int HAL_GetFPGAVersionDelegate(ref int status);
        public static HAL_GetFPGAVersionDelegate HAL_GetFPGAVersion;

        public delegate long HAL_GetFPGARevisionDelegate(ref int status);
        public static HAL_GetFPGARevisionDelegate HAL_GetFPGARevision;

        public delegate ulong HAL_GetFPGATimeDelegate(ref int status);
        public static HAL_GetFPGATimeDelegate HAL_GetFPGATime;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetFPGAButtonDelegate(ref int status);
        public static HAL_GetFPGAButtonDelegate HAL_GetFPGAButton;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetSystemActiveDelegate(ref int status);
        public static HAL_GetSystemActiveDelegate HAL_GetSystemActive;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetBrownedOutDelegate(ref int status);
        public static HAL_GetBrownedOutDelegate HAL_GetBrownedOut;

        public delegate int HAL_InitializeDelegate(int mode);
        public static HAL_InitializeDelegate HAL_Initialize;

        public delegate long HAL_ReportDelegate(int resource, int instanceNumber, int context, [HALAllowNonBlittable]string feature = null);
        public static HAL_ReportDelegate HAL_Report;
    }
}

