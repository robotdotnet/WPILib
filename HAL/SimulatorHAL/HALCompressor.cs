using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using HAL.Simulator.Data;
using HAL.SimulatorHAL.Handles;
using static HAL.Base.HALErrors;
using static HAL.Base.HAL;
using static HAL.SimulatorHAL.HALPorts;
using static HAL.SimulatorHAL.Handles.Handle;
using static HAL.Simulator.SimData;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALCompressor
    {



        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALCompressor.HAL_InitializeCompressor = HAL_InitializeCompressor;
            Base.HALCompressor.HAL_CheckCompressorModule = HAL_CheckCompressorModule;
            Base.HALCompressor.HAL_GetCompressor = HAL_GetCompressor;
            Base.HALCompressor.HAL_SetCompressorClosedLoopControl = HAL_SetCompressorClosedLoopControl;
            Base.HALCompressor.HAL_GetCompressorClosedLoopControl = HAL_GetCompressorClosedLoopControl;
            Base.HALCompressor.HAL_GetCompressorPressureSwitch = HAL_GetCompressorPressureSwitch;
            Base.HALCompressor.HAL_GetCompressorCurrent = HAL_GetCompressorCurrent;
            Base.HALCompressor.HAL_GetCompressorCurrentTooHighFault = HAL_GetCompressorCurrentTooHighFault;
            Base.HALCompressor.HAL_GetCompressorCurrentTooHighStickyFault = HAL_GetCompressorCurrentTooHighStickyFault;
            Base.HALCompressor.HAL_GetCompressorShortedStickyFault = HAL_GetCompressorShortedStickyFault;
            Base.HALCompressor.HAL_GetCompressorShortedFault = HAL_GetCompressorShortedFault;
            Base.HALCompressor.HAL_GetCompressorNotConnectedStickyFault = HAL_GetCompressorNotConnectedStickyFault;
            Base.HALCompressor.HAL_GetCompressorNotConnectedFault = HAL_GetCompressorNotConnectedFault;
        }

        public static int HAL_InitializeCompressor(int module, ref int status)
        {
            bool initialized = InitializePCM(module);
            if (!initialized)
            {
                status = PARAMETER_OUT_OF_RANGE;
                return HALInvalidHandle;
            }

            GetPCM(module).Compressor.Initialized = true;

            return CreateHandle((short) module, HALHandleEnum.Compressor);
        }

        public static bool HAL_CheckCompressorModule(int module)
        {
            return module < kNumPCMModules && module >= 0;
        }

        public static bool HAL_GetCompressor(int compressor_handle, ref int status)
        {
            short index = GetHandleTypedIndex(compressor_handle, HALHandleEnum.Compressor);
            if (index == InvalidHandleIndex)
            {
                status = HAL_HANDLE_ERROR;
                return false;
            }

            return GetPCM(index).Compressor.On;
        }

        public static void HAL_SetCompressorClosedLoopControl(int compressor_handle, bool value, ref int status)
        {
            short index = GetHandleTypedIndex(compressor_handle, HALHandleEnum.Compressor);
            if (index == InvalidHandleIndex)
            {
                status = HAL_HANDLE_ERROR;
                return;
            }

            GetPCM(index).Compressor.CloseLoopEnabled = value;
        }

        public static bool HAL_GetCompressorClosedLoopControl(int compressor_handle, ref int status)
        {
            short index = GetHandleTypedIndex(compressor_handle, HALHandleEnum.Compressor);
            if (index == InvalidHandleIndex)
            {
                status = HAL_HANDLE_ERROR;
                return false;
            }

            return GetPCM(index).Compressor.CloseLoopEnabled;
        }

        public static bool HAL_GetCompressorPressureSwitch(int compressor_handle, ref int status)
        {
            short index = GetHandleTypedIndex(compressor_handle, HALHandleEnum.Compressor);
            if (index == InvalidHandleIndex)
            {
                status = HAL_HANDLE_ERROR;
                return false;
            }

            return GetPCM(index).Compressor.PressureSwitch;
        }

        public static double HAL_GetCompressorCurrent(int compressor_handle, ref int status)
        {
            short index = GetHandleTypedIndex(compressor_handle, HALHandleEnum.Compressor);
            if (index == InvalidHandleIndex)
            {
                status = HAL_HANDLE_ERROR;
                return  0.0;
            }

            return GetPCM(index).Compressor.Current;
        }

        public static bool HAL_GetCompressorCurrentTooHighFault(int compressor_handle, ref int status)
        {
            return false;
        }

        public static bool HAL_GetCompressorCurrentTooHighStickyFault(int compressor_handle, ref int status)
        {
            return false;
        }

        public static bool HAL_GetCompressorShortedStickyFault(int compressor_handle, ref int status)
        {
            return false;
        }

        public static bool HAL_GetCompressorShortedFault(int compressor_handle, ref int status)
        {
            return false;
        }

        public static bool HAL_GetCompressorNotConnectedStickyFault(int compressor_handle, ref int status)
        {
            return false;
        }

        public static bool HAL_GetCompressorNotConnectedFault(int compressor_handle, ref int status)
        {
            return false;
        }
    }
}

