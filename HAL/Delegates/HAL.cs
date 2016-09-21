using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using HAL.NativeLoader;

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
            NativeDelegateInitializer.SetupNativeDelegates<HAL>(LibraryLoaderHolder.NativeLoader);
        }

        public static void PingAll()
        {
            HALAccelerometer.Ping();
            HALAnalogAccumulator.Ping();
            HALAnalogGyro.Ping();
            HALAnalogInput.Ping();
            HALAnalogOutput.Ping();
            HALAnalogTrigger.Ping();
            HALCompressor.Ping();
            HALConstants.Ping();
            HALDIO.Ping();
            HALEncoder.Ping();
            HALI2C.Ping();
            HALInterrupts.Ping();
            HALNotifier.Ping();
            HALPDP.Ping();
            HALPorts.Ping();
            HALPower.Ping();
            HALPWM.Ping();
            HALRelay.Ping();
            HALSerialPort.Ping();
            HALSolenoid.Ping();
            HALSPI.Ping();
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        /*
        [HALAllowNonBlittable]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate long HAL_ReportDelegate(int resource, int instanceNumber, int context, [HALAllowNonBlittable]string feature = null);
        public static HAL_ReportDelegate HAL_Report;
        */
        /*
        [HALAllowNonBlittable]
        private delegate string HAL_GetErrorMessageDelegate(int code);
        private static HAL_GetErrorMessageDelegate HAL_GetErrorMessageNative;
        */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetPortDelegate(int pin);
        [NativeDelegate] public static HAL_GetPortDelegate HAL_GetPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetPortWithModuleDelegate(int module, int pin);
        [NativeDelegate]
        public static HAL_GetPortWithModuleDelegate HAL_GetPortWithModule;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetFPGAVersionDelegate(ref int status);
        [NativeDelegate]
        public static HAL_GetFPGAVersionDelegate HAL_GetFPGAVersion;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate long HAL_GetFPGARevisionDelegate(ref int status);
        [NativeDelegate]
        public static HAL_GetFPGARevisionDelegate HAL_GetFPGARevision;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate ulong HAL_GetFPGATimeDelegate(ref int status);
        [NativeDelegate]
        public static HAL_GetFPGATimeDelegate HAL_GetFPGATime;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetFPGAButtonDelegate(ref int status);
        [NativeDelegate]
        public static HAL_GetFPGAButtonDelegate HAL_GetFPGAButton;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetSystemActiveDelegate(ref int status);
        [NativeDelegate]
        public static HAL_GetSystemActiveDelegate HAL_GetSystemActive;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetBrownedOutDelegate(ref int status);
        [NativeDelegate]
        public static HAL_GetBrownedOutDelegate HAL_GetBrownedOut;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeDelegate(int mode);
        [NativeDelegate]
        public static HAL_InitializeDelegate HAL_Initialize;

        private delegate long NativeHALReportDelegate(int resource, int instanceNumber, int context, byte[] feature);
        [NativeDelegate("HAL_Report")]
        private static NativeHALReportDelegate NativeHALReport;

        private delegate IntPtr NativeGetHALErrorMessageDelegate(int code);
        [NativeDelegate("HAL_GetErrorMessage")]
        private static NativeGetHALErrorMessageDelegate NativeHALGetErrorMessage;

        public static long HAL_Report(int resource, int instanceNumber, int context, string feature = null)
        {
            int len;
            return NativeHALReport(resource, instanceNumber, context, CreateUTF8String(feature, out len));
        }

        /// <summary>
        /// Gets an Error Message from the HAL
        /// </summary>
        /// <param name="code">The Error Code</param>
        /// <returns>The Error Message</returns>
        public static string HAL_GetErrorMessage(int code)
        {
            return ReadUTF8String(NativeHALGetErrorMessage(code));
        }

        internal static string ReadUTF8String(IntPtr ptr)
        {
            var data = new List<byte>();
            var off = 0;
            while (true)
            {
                var ch = Marshal.ReadByte(ptr, off++);
                if (ch == 0)
                {
                    break;
                }
                data.Add(ch);
            }
            return Encoding.UTF8.GetString(data.ToArray());
        }

        internal static byte[] CreateUTF8String(string str, out int size)
        {
            if (str == null)
            {
                str = "";
            }

            var bytes = Encoding.UTF8.GetByteCount(str);

            var buffer = new byte[bytes + 1];
            size = bytes;
            Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 0);
            buffer[bytes] = 0;
            return buffer;
        }
    }
}

