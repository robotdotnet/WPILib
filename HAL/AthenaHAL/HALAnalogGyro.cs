using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALAnalogGyro
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALAnalogGyro.HAL_InitializeAnalogGyro = (Base.HALAnalogGyro.HAL_InitializeAnalogGyroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeAnalogGyro"), typeof(Base.HALAnalogGyro.HAL_InitializeAnalogGyroDelegate));

Base.HALAnalogGyro.HAL_SetupAnalogGyro = (Base.HALAnalogGyro.HAL_SetupAnalogGyroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetupAnalogGyro"), typeof(Base.HALAnalogGyro.HAL_SetupAnalogGyroDelegate));

Base.HALAnalogGyro.HAL_FreeAnalogGyro = (Base.HALAnalogGyro.HAL_FreeAnalogGyroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeAnalogGyro"), typeof(Base.HALAnalogGyro.HAL_FreeAnalogGyroDelegate));

Base.HALAnalogGyro.HAL_SetAnalogGyroParameters = (Base.HALAnalogGyro.HAL_SetAnalogGyroParametersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogGyroParameters"), typeof(Base.HALAnalogGyro.HAL_SetAnalogGyroParametersDelegate));

Base.HALAnalogGyro.HAL_SetAnalogGyroVoltsPerDegreePerSecond = (Base.HALAnalogGyro.HAL_SetAnalogGyroVoltsPerDegreePerSecondDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogGyroVoltsPerDegreePerSecond"), typeof(Base.HALAnalogGyro.HAL_SetAnalogGyroVoltsPerDegreePerSecondDelegate));

Base.HALAnalogGyro.HAL_ResetAnalogGyro = (Base.HALAnalogGyro.HAL_ResetAnalogGyroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ResetAnalogGyro"), typeof(Base.HALAnalogGyro.HAL_ResetAnalogGyroDelegate));

Base.HALAnalogGyro.HAL_CalibrateAnalogGyro = (Base.HALAnalogGyro.HAL_CalibrateAnalogGyroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CalibrateAnalogGyro"), typeof(Base.HALAnalogGyro.HAL_CalibrateAnalogGyroDelegate));

Base.HALAnalogGyro.HAL_SetAnalogGyroDeadband = (Base.HALAnalogGyro.HAL_SetAnalogGyroDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogGyroDeadband"), typeof(Base.HALAnalogGyro.HAL_SetAnalogGyroDeadbandDelegate));

Base.HALAnalogGyro.HAL_GetAnalogGyroAngle = (Base.HALAnalogGyro.HAL_GetAnalogGyroAngleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogGyroAngle"), typeof(Base.HALAnalogGyro.HAL_GetAnalogGyroAngleDelegate));

Base.HALAnalogGyro.HAL_GetAnalogGyroRate = (Base.HALAnalogGyro.HAL_GetAnalogGyroRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogGyroRate"), typeof(Base.HALAnalogGyro.HAL_GetAnalogGyroRateDelegate));

Base.HALAnalogGyro.HAL_GetAnalogGyroOffset = (Base.HALAnalogGyro.HAL_GetAnalogGyroOffsetDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogGyroOffset"), typeof(Base.HALAnalogGyro.HAL_GetAnalogGyroOffsetDelegate));

Base.HALAnalogGyro.HAL_GetAnalogGyroCenter = (Base.HALAnalogGyro.HAL_GetAnalogGyroCenterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogGyroCenter"), typeof(Base.HALAnalogGyro.HAL_GetAnalogGyroCenterDelegate));
}
}
}

