using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.Simulator;
using HAL.SimulatorHAL.Handles;
using static HAL.Base.HALErrors;
using static HAL.Base.HAL;
using static HAL.SimulatorHAL.HALPorts;
using static HAL.SimulatorHAL.Handles.Handle;
using static HAL.Simulator.SimData;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALAnalogGyro
    {
        class AnalogGyro
        {
            public byte Accumulator { get; set; }
        }

        private static IndexedHandleResource<AnalogGyro> analogGyroHandles = new IndexedHandleResource<AnalogGyro>(kNumAccumulators, HALHandleEnum.AnalogGyro);


        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAnalogGyro.HAL_InitializeAnalogGyro = HAL_InitializeAnalogGyro;
            Base.HALAnalogGyro.HAL_SetupAnalogGyro = HAL_SetupAnalogGyro;
            Base.HALAnalogGyro.HAL_FreeAnalogGyro = HAL_FreeAnalogGyro;
            Base.HALAnalogGyro.HAL_SetAnalogGyroParameters = HAL_SetAnalogGyroParameters;
            Base.HALAnalogGyro.HAL_SetAnalogGyroVoltsPerDegreePerSecond = HAL_SetAnalogGyroVoltsPerDegreePerSecond;
            Base.HALAnalogGyro.HAL_ResetAnalogGyro = HAL_ResetAnalogGyro;
            Base.HALAnalogGyro.HAL_CalibrateAnalogGyro = HAL_CalibrateAnalogGyro;
            Base.HALAnalogGyro.HAL_SetAnalogGyroDeadband = HAL_SetAnalogGyroDeadband;
            Base.HALAnalogGyro.HAL_GetAnalogGyroAngle = HAL_GetAnalogGyroAngle;
            Base.HALAnalogGyro.HAL_GetAnalogGyroRate = HAL_GetAnalogGyroRate;
            Base.HALAnalogGyro.HAL_GetAnalogGyroOffset = HAL_GetAnalogGyroOffset;
            Base.HALAnalogGyro.HAL_GetAnalogGyroCenter = HAL_GetAnalogGyroCenter;
        }

        public static int HAL_InitializeAnalogGyro(int analogHandle, ref int status)
        {
            if (!HALAnalogAccumulator.HAL_IsAccumulatorChannel(analogHandle, ref status))
            {
                if (status == 0)
                {
                    status = HAL_INVALID_ACCUMULATOR_CHANNEL;
                }
                return HALInvalidHandle;
            }

            short channel = GetHandleIndex(analogHandle);

            var handle = analogGyroHandles.Allocate(channel, ref status);
            if (status != 0) return HALInvalidHandle;

            var gyro = analogGyroHandles.Get(handle);
            if (gyro == null)
            {
                status = HAL_HANDLE_ERROR;
                return HALInvalidHandle;
            }

            gyro.Accumulator = (byte)GetHandleIndex(analogHandle);
            return handle;
        }

        public static void HAL_SetupAnalogGyro(int handle, ref int status)
        {
            // No op
        }

        public static void HAL_FreeAnalogGyro(int handle)
        {
            analogGyroHandles.Free(handle);
        }

        public static void HAL_SetAnalogGyroParameters(int handle, double voltsPerDegreePerSecond, double offset, int center, ref int status)
        {
            // No op
        }

        public static void HAL_SetAnalogGyroVoltsPerDegreePerSecond(int handle, double voltsPerDegreePerSecond, ref int status)
        {
             // No op
        }

        public static void HAL_ResetAnalogGyro(int handle, ref int status)
        {
            var gyro = analogGyroHandles.Get(handle);
            if (gyro == null)
            {
                status = HAL_HANDLE_ERROR;
                return;
            }

            SimData.AnalogGyro[gyro.Accumulator].Angle = 0.0;
            SimData.AnalogGyro[gyro.Accumulator].Rate = 0.0;
        }

        public static void HAL_CalibrateAnalogGyro(int handle, ref int status)
        {
            // No op
        }

        public static void HAL_SetAnalogGyroDeadband(int handle, double volts, ref int status)
        {
            // No op
        }

        public static double HAL_GetAnalogGyroAngle(int handle, ref int status)
        {
            var gyro = analogGyroHandles.Get(handle);
            if (gyro == null)
            {
                status = HAL_HANDLE_ERROR;
                return 0.0;
            }

            return SimData.AnalogGyro[gyro.Accumulator].Angle;
        }

        public static double HAL_GetAnalogGyroRate(int handle, ref int status)
        {
            var gyro = analogGyroHandles.Get(handle);
            if (gyro == null)
            {
                status = HAL_HANDLE_ERROR;
                return 0.0;
            }

            return SimData.AnalogGyro[gyro.Accumulator].Rate;
        }

        public static double HAL_GetAnalogGyroOffset(int handle, ref int status)
        {
            return 0.0; // No op
        }

        public static int HAL_GetAnalogGyroCenter(int handle, ref int status)
        {
            return 0; // No op
        }
    }
}

