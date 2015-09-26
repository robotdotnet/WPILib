//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALCompressor
    {
        static HALCompressor()
        {
            HAL.Initialize();
        }

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            InitializeCompressor = (InitializeCompressorDelegate)Delegate.CreateDelegate(typeof(InitializeCompressorDelegate), type.GetMethod("initializeCompressor"));
            CheckCompressorModule = (CheckCompressorModuleDelegate)Delegate.CreateDelegate(typeof(CheckCompressorModuleDelegate), type.GetMethod("checkCompressorModule"));
            GetCompressor = (GetCompressorDelegate)Delegate.CreateDelegate(typeof(GetCompressorDelegate), type.GetMethod("getCompressor"));
            SetClosedLoopControl = (SetClosedLoopControlDelegate)Delegate.CreateDelegate(typeof(SetClosedLoopControlDelegate), type.GetMethod("setClosedLoopControl"));
            GetClosedLoopControl = (GetClosedLoopControlDelegate)Delegate.CreateDelegate(typeof(GetClosedLoopControlDelegate), type.GetMethod("getClosedLoopControl"));
            GetPressureSwitch = (GetPressureSwitchDelegate)Delegate.CreateDelegate(typeof(GetPressureSwitchDelegate), type.GetMethod("getPressureSwitch"));
            GetCompressorCurrent = (GetCompressorCurrentDelegate)Delegate.CreateDelegate(typeof(GetCompressorCurrentDelegate), type.GetMethod("getCompressorCurrent"));
            GetCompressorCurrentTooHighFault = (GetCompressorCurrentTooHighFaultDelegate)Delegate.CreateDelegate(typeof(GetCompressorCurrentTooHighFaultDelegate), type.GetMethod("getCompressorCurrentTooHighFault"));
            GetCompressorCurrentTooHighStickyFault = (GetCompressorCurrentTooHighStickyFaultDelegate)Delegate.CreateDelegate(typeof(GetCompressorCurrentTooHighStickyFaultDelegate), type.GetMethod("getCompressorCurrentTooHighStickyFault"));
            GetCompressorShortedStickyFault = (GetCompressorShortedStickyFaultDelegate)Delegate.CreateDelegate(typeof(GetCompressorShortedStickyFaultDelegate), type.GetMethod("getCompressorShortedStickyFault"));
            GetCompressorShortedFault = (GetCompressorShortedFaultDelegate)Delegate.CreateDelegate(typeof(GetCompressorShortedFaultDelegate), type.GetMethod("getCompressorShortedFault"));
            GetCompressorNotConnectedStickyFault = (GetCompressorNotConnectedStickyFaultDelegate)Delegate.CreateDelegate(typeof(GetCompressorNotConnectedStickyFaultDelegate), type.GetMethod("getCompressorNotConnectedStickyFault"));
            GetCompressorNotConnectedFault = (GetCompressorNotConnectedFaultDelegate)Delegate.CreateDelegate(typeof(GetCompressorNotConnectedFaultDelegate), type.GetMethod("getCompressorNotConnectedFault"));
            ClearAllPCMStickyFaults = (ClearAllPCMStickyFaultsDelegate)Delegate.CreateDelegate(typeof(ClearAllPCMStickyFaultsDelegate), type.GetMethod("clearAllPCMStickyFaults"));
        }

        public delegate IntPtr InitializeCompressorDelegate(byte module);
        public static InitializeCompressorDelegate InitializeCompressor;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool CheckCompressorModuleDelegate(byte module);
        public static CheckCompressorModuleDelegate CheckCompressorModule;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorDelegate GetCompressor;

        public delegate void SetClosedLoopControlDelegate(IntPtr pcm_pointer, [MarshalAs(UnmanagedType.I1)]bool value, ref int status);
        public static SetClosedLoopControlDelegate SetClosedLoopControl;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetClosedLoopControlDelegate(IntPtr pcm_pointer, ref int status);
        public static GetClosedLoopControlDelegate GetClosedLoopControl;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetPressureSwitchDelegate(IntPtr pcm_pointer, ref int status);
        public static GetPressureSwitchDelegate GetPressureSwitch;

        public delegate float GetCompressorCurrentDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentDelegate GetCompressorCurrent;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorCurrentTooHighFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentTooHighFaultDelegate GetCompressorCurrentTooHighFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorCurrentTooHighStickyFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentTooHighStickyFaultDelegate GetCompressorCurrentTooHighStickyFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorShortedStickyFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorShortedStickyFaultDelegate GetCompressorShortedStickyFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorShortedFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorShortedFaultDelegate GetCompressorShortedFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorNotConnectedStickyFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorNotConnectedStickyFaultDelegate GetCompressorNotConnectedStickyFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorNotConnectedFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorNotConnectedFaultDelegate GetCompressorNotConnectedFault;

        public delegate void ClearAllPCMStickyFaultsDelegate(IntPtr pcm_pointer, ref int status);
        public static ClearAllPCMStickyFaultsDelegate ClearAllPCMStickyFaults;
    }
}
