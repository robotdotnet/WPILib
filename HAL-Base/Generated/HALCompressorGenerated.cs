//File automatically generated using robotdotnet-tools. Please do not modify.
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HALCompressor
    {
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

        public delegate System.IntPtr InitializeCompressorDelegate(byte module);
        public static InitializeCompressorDelegate InitializeCompressor;

        public delegate bool CheckCompressorModuleDelegate(byte module);
        public static CheckCompressorModuleDelegate CheckCompressorModule;

        public delegate bool GetCompressorDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetCompressorDelegate GetCompressor;

        public delegate void SetClosedLoopControlDelegate(System.IntPtr pcm_pointer, bool value, ref int status);
        public static SetClosedLoopControlDelegate SetClosedLoopControl;

        public delegate bool GetClosedLoopControlDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetClosedLoopControlDelegate GetClosedLoopControl;

        public delegate bool GetPressureSwitchDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetPressureSwitchDelegate GetPressureSwitch;

        public delegate float GetCompressorCurrentDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentDelegate GetCompressorCurrent;

        public delegate bool GetCompressorCurrentTooHighFaultDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentTooHighFaultDelegate GetCompressorCurrentTooHighFault;

        public delegate bool GetCompressorCurrentTooHighStickyFaultDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentTooHighStickyFaultDelegate GetCompressorCurrentTooHighStickyFault;

        public delegate bool GetCompressorShortedStickyFaultDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetCompressorShortedStickyFaultDelegate GetCompressorShortedStickyFault;

        public delegate bool GetCompressorShortedFaultDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetCompressorShortedFaultDelegate GetCompressorShortedFault;

        public delegate bool GetCompressorNotConnectedStickyFaultDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetCompressorNotConnectedStickyFaultDelegate GetCompressorNotConnectedStickyFault;

        public delegate bool GetCompressorNotConnectedFaultDelegate(System.IntPtr pcm_pointer, ref int status);
        public static GetCompressorNotConnectedFaultDelegate GetCompressorNotConnectedFault;

        public delegate void ClearAllPCMStickyFaultsDelegate(System.IntPtr pcm_pointer, ref int status);
        public static ClearAllPCMStickyFaultsDelegate ClearAllPCMStickyFaults;
    }
}
