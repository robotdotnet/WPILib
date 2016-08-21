using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
    internal class HALAccelerometer
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {

            Base.HALAccelerometer.HAL_SetAccelerometerActive = (Base.HALAccelerometer.HAL_SetAccelerometerActiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAccelerometerActive"), typeof(Base.HALAccelerometer.HAL_SetAccelerometerActiveDelegate));

            Base.HALAccelerometer.HAL_SetAccelerometerRange = (Base.HALAccelerometer.HAL_SetAccelerometerRangeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAccelerometerRange"), typeof(Base.HALAccelerometer.HAL_SetAccelerometerRangeDelegate));

            Base.HALAccelerometer.HAL_GetAccelerometerX = (Base.HALAccelerometer.HAL_GetAccelerometerXDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAccelerometerX"), typeof(Base.HALAccelerometer.HAL_GetAccelerometerXDelegate));

            Base.HALAccelerometer.HAL_GetAccelerometerY = (Base.HALAccelerometer.HAL_GetAccelerometerYDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAccelerometerY"), typeof(Base.HALAccelerometer.HAL_GetAccelerometerYDelegate));

            Base.HALAccelerometer.HAL_GetAccelerometerZ = (Base.HALAccelerometer.HAL_GetAccelerometerZDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAccelerometerZ"), typeof(Base.HALAccelerometer.HAL_GetAccelerometerZDelegate));
        }
    }
}

