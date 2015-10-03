//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALAccelerometer
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALAccelerometer.SetAccelerometerActive = (HAL_Base.HALAccelerometer.SetAccelerometerActiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccelerometerActive"), typeof(HAL_Base.HALAccelerometer.SetAccelerometerActiveDelegate));

            HAL_Base.HALAccelerometer.SetAccelerometerRange = (HAL_Base.HALAccelerometer.SetAccelerometerRangeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccelerometerRange"), typeof(HAL_Base.HALAccelerometer.SetAccelerometerRangeDelegate));

            HAL_Base.HALAccelerometer.GetAccelerometerX = (HAL_Base.HALAccelerometer.GetAccelerometerXDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerX"), typeof(HAL_Base.HALAccelerometer.GetAccelerometerXDelegate));

            HAL_Base.HALAccelerometer.GetAccelerometerY = (HAL_Base.HALAccelerometer.GetAccelerometerYDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerY"), typeof(HAL_Base.HALAccelerometer.GetAccelerometerYDelegate));

            HAL_Base.HALAccelerometer.GetAccelerometerZ = (HAL_Base.HALAccelerometer.GetAccelerometerZDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerZ"), typeof(HAL_Base.HALAccelerometer.GetAccelerometerZDelegate));

        }
    }
}
