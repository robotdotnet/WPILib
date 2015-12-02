//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALAccelerometer
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALAccelerometer.SetAccelerometerActive = (global::HAL.HALAccelerometer.SetAccelerometerActiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccelerometerActive"), typeof(global::HAL.HALAccelerometer.SetAccelerometerActiveDelegate));

            global::HAL.HALAccelerometer.SetAccelerometerRange = (global::HAL.HALAccelerometer.SetAccelerometerRangeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccelerometerRange"), typeof(global::HAL.HALAccelerometer.SetAccelerometerRangeDelegate));

            global::HAL.HALAccelerometer.GetAccelerometerX = (global::HAL.HALAccelerometer.GetAccelerometerXDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerX"), typeof(global::HAL.HALAccelerometer.GetAccelerometerXDelegate));

            global::HAL.HALAccelerometer.GetAccelerometerY = (global::HAL.HALAccelerometer.GetAccelerometerYDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerY"), typeof(global::HAL.HALAccelerometer.GetAccelerometerYDelegate));

            global::HAL.HALAccelerometer.GetAccelerometerZ = (global::HAL.HALAccelerometer.GetAccelerometerZDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerZ"), typeof(global::HAL.HALAccelerometer.GetAccelerometerZDelegate));

        }
    }
}
