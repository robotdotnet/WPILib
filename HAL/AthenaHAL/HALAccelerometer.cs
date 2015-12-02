//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALAccelerometer
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAccelerometer.SetAccelerometerActive = (Base.HALAccelerometer.SetAccelerometerActiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccelerometerActive"), typeof(Base.HALAccelerometer.SetAccelerometerActiveDelegate));

            Base.HALAccelerometer.SetAccelerometerRange = (Base.HALAccelerometer.SetAccelerometerRangeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccelerometerRange"), typeof(Base.HALAccelerometer.SetAccelerometerRangeDelegate));

            Base.HALAccelerometer.GetAccelerometerX = (Base.HALAccelerometer.GetAccelerometerXDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerX"), typeof(Base.HALAccelerometer.GetAccelerometerXDelegate));

            Base.HALAccelerometer.GetAccelerometerY = (Base.HALAccelerometer.GetAccelerometerYDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerY"), typeof(Base.HALAccelerometer.GetAccelerometerYDelegate));

            Base.HALAccelerometer.GetAccelerometerZ = (Base.HALAccelerometer.GetAccelerometerZDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccelerometerZ"), typeof(Base.HALAccelerometer.GetAccelerometerZDelegate));

        }
    }
}
