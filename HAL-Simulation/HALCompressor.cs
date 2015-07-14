using System;
using System.Runtime.InteropServices;
using static HAL_Simulator.SimData;
// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming

namespace HAL_Simulator
{
    public class HALCompressor
    {
        public static IntPtr initializeCompressor(byte module)
        {
            if (module != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(module), "Must use module 0 for the compressor.");
            }
            halData["compressor"]["initialized"] = true;
            PCM pcm = new PCM {module = module};
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(pcm));
            Marshal.StructureToPtr(pcm, ptr, true);
            return ptr;
        }

        public static bool checkCompressorModule(byte module)
        {
            return module < 63;
        }

        public static bool getCompressor(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return halData["compressor"]["on"];
        }

        public static void setClosedLoopControl(IntPtr pcm_pointer, bool value, ref int status)
        {
            status = 0;
            halData["compressor"]["close_loop_enabled"] = value;
        }

        public static bool getClosedLoopControl(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return halData["compressor"]["close_loop_enabled"];
        }

        public static bool getPressureSwitch(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return halData["compressor"]["pressure_switch"];
        }

        public static float getCompressorCurrent(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return (float)halData["compressor"]["current"];
        }
        
        public static bool getCompressorCurrentTooHighFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }

        public static bool getCompressorCurrentTooHighStickyFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }

        public static bool getCompressorShortedStickyFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }


        public static bool getCompressorShortedFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }

        public static bool getCompressorNotConnectedStickyFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }

        public static bool getCompressorNotConnectedFault(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
            return false;
        }


        public static void clearAllPCMStickyFaults(IntPtr pcm_pointer, ref int status)
        {
            status = 0;
        }
    }
}
