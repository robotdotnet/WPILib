using System;
using System.Runtime.InteropServices;
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
        [CalledSimFunction]
        public static IntPtr initializeCompressor(byte module)
        {
            /*
            if (module != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(module), "Must use module 0 for the compressor.");
            }
            */
            InitializeNewPCM(module);
            halData["pcm"][(int)module]["compressor"]["initialized"] = true;
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
            
            return halData["pcm"][GetPCM(pcm_pointer)]["compressor"]["on"];
        }

        [CalledSimFunction]
        public static void setClosedLoopControl(IntPtr pcm_pointer, bool value, ref int status)
        {
            status = 0;
            halData["pcm"][GetPCM(pcm_pointer)]["compressor"]["close_loop_enabled"] = value;
        }

        [CalledSimFunction]
        public static bool getClosedLoopControl(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return halData["pcm"][GetPCM(pcm_pointer)]["compressor"]["close_loop_enabled"];
        }

        [CalledSimFunction]
        public static bool getPressureSwitch(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return halData["pcm"][GetPCM(pcm_pointer)]["compressor"]["pressure_switch"];
        }

        [CalledSimFunction]
        public static float getCompressorCurrent(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return (float)halData["pcm"][GetPCM(pcm_pointer)]["compressor"]["current"];
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
