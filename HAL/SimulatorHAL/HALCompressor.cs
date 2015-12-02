using System;
using System.Runtime.InteropServices;
using HAL.Base;
using static HAL.Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal class HALCompressor
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALCompressor.InitializeCompressor = initializeCompressor;
            Base.HALCompressor.CheckCompressorModule = checkCompressorModule;
            Base.HALCompressor.GetCompressor = getCompressor;
            Base.HALCompressor.SetClosedLoopControl = setClosedLoopControl;
            Base.HALCompressor.GetClosedLoopControl = getClosedLoopControl;
            Base.HALCompressor.GetPressureSwitch = getPressureSwitch;
            Base.HALCompressor.GetCompressorCurrent = getCompressorCurrent;
            Base.HALCompressor.GetCompressorCurrentTooHighFault = getCompressorCurrentTooHighFault;
            Base.HALCompressor.GetCompressorCurrentTooHighStickyFault = getCompressorCurrentTooHighStickyFault;
            Base.HALCompressor.GetCompressorShortedStickyFault = getCompressorShortedStickyFault;
            Base.HALCompressor.GetCompressorShortedFault = getCompressorShortedFault;
            Base.HALCompressor.GetCompressorNotConnectedStickyFault = getCompressorNotConnectedStickyFault;
            Base.HALCompressor.GetCompressorNotConnectedFault = getCompressorNotConnectedFault;
            Base.HALCompressor.ClearAllPCMStickyFaults = clearAllPCMStickyFaults;
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
            
            return GetPCM(PortConverters.GetPCM(pcm_pointer)).Compressor.On;
        }

        [CalledSimFunction]
        public static void setClosedLoopControl(IntPtr pcm_pointer, bool value, ref int status)
        {
            status = 0;
            GetPCM(PortConverters.GetPCM(pcm_pointer)).Compressor.CloseLoopEnabled = value;
        }

        [CalledSimFunction]
        public static bool getClosedLoopControl(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return GetPCM(PortConverters.GetPCM(pcm_pointer)).Compressor.CloseLoopEnabled;
        }

        [CalledSimFunction]
        public static bool getPressureSwitch(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return GetPCM(PortConverters.GetPCM(pcm_pointer)).Compressor.PressureSwitch;
        }

        [CalledSimFunction]
        public static float getCompressorCurrent(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return GetPCM(PortConverters.GetPCM(pcm_pointer)).Compressor.Current;
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
