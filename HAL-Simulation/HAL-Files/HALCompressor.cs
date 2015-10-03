using System;
using System.Runtime.InteropServices;
using HAL_Base;
using static HAL_Simulator.SimData;
using static HAL_Simulator.PortConverters;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALCompressor
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALCompressor.InitializeCompressor = initializeCompressor;
            HAL_Base.HALCompressor.CheckCompressorModule = checkCompressorModule;
            HAL_Base.HALCompressor.GetCompressor = getCompressor;
            HAL_Base.HALCompressor.SetClosedLoopControl = setClosedLoopControl;
            HAL_Base.HALCompressor.GetClosedLoopControl = getClosedLoopControl;
            HAL_Base.HALCompressor.GetPressureSwitch = getPressureSwitch;
            HAL_Base.HALCompressor.GetCompressorCurrent = getCompressorCurrent;
            HAL_Base.HALCompressor.GetCompressorCurrentTooHighFault = getCompressorCurrentTooHighFault;
            HAL_Base.HALCompressor.GetCompressorCurrentTooHighStickyFault = getCompressorCurrentTooHighStickyFault;
            HAL_Base.HALCompressor.GetCompressorShortedStickyFault = getCompressorShortedStickyFault;
            HAL_Base.HALCompressor.GetCompressorShortedFault = getCompressorShortedFault;
            HAL_Base.HALCompressor.GetCompressorNotConnectedStickyFault = getCompressorNotConnectedStickyFault;
            HAL_Base.HALCompressor.GetCompressorNotConnectedFault = getCompressorNotConnectedFault;
            HAL_Base.HALCompressor.ClearAllPCMStickyFaults = clearAllPCMStickyFaults;
        }

        [CalledSimFunction]
        public static IntPtr initializeCompressor(byte module)
        {
           
            InitializePCM(module);
            GetPCM(module).Compressor.Initialized = true;
            PCM pcm = new PCM { module = module };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(pcm));
            Marshal.StructureToPtr(pcm, ptr, true);
            return ptr;
        }

        [CalledSimFunction]
        public static bool checkCompressorModule(byte module)
        {
            return module < 63;
        }

        [CalledSimFunction]
        public static bool getCompressor(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            
            return GetPCM(GetPCM(pcm_pointer)).Compressor.On;
        }

        [CalledSimFunction]
        public static void setClosedLoopControl(IntPtr pcm_pointer, bool value, ref int status)
        {
            status = 0;
            GetPCM(GetPCM(pcm_pointer)).Compressor.CloseLoopEnabled = value;
        }

        [CalledSimFunction]
        public static bool getClosedLoopControl(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return GetPCM(GetPCM(pcm_pointer)).Compressor.CloseLoopEnabled;
        }

        [CalledSimFunction]
        public static bool getPressureSwitch(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return GetPCM(GetPCM(pcm_pointer)).Compressor.PressureSwitch;
        }

        [CalledSimFunction]
        public static float getCompressorCurrent(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return GetPCM(GetPCM(pcm_pointer)).Compressor.Current;
        }

        [CalledSimFunction]
        public static bool getCompressorCurrentTooHighFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }

        [CalledSimFunction]
        public static bool getCompressorCurrentTooHighStickyFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }

        [CalledSimFunction]
        public static bool getCompressorShortedStickyFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }


        [CalledSimFunction]
        public static bool getCompressorShortedFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }

        [CalledSimFunction]
        public static bool getCompressorNotConnectedStickyFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }

        [CalledSimFunction]
        public static bool getCompressorNotConnectedFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }


        [CalledSimFunction]
        public static void clearAllPCMStickyFaults(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
        }
    }
}
