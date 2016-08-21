using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALAnalogAccumulator
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALAnalogAccumulator.HAL_IsAccumulatorChannel = (Base.HALAnalogAccumulator.HAL_IsAccumulatorChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_IsAccumulatorChannel"), typeof(Base.HALAnalogAccumulator.HAL_IsAccumulatorChannelDelegate));

Base.HALAnalogAccumulator.HAL_InitAccumulator = (Base.HALAnalogAccumulator.HAL_InitAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitAccumulator"), typeof(Base.HALAnalogAccumulator.HAL_InitAccumulatorDelegate));

Base.HALAnalogAccumulator.HAL_ResetAccumulator = (Base.HALAnalogAccumulator.HAL_ResetAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ResetAccumulator"), typeof(Base.HALAnalogAccumulator.HAL_ResetAccumulatorDelegate));

Base.HALAnalogAccumulator.HAL_SetAccumulatorCenter = (Base.HALAnalogAccumulator.HAL_SetAccumulatorCenterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAccumulatorCenter"), typeof(Base.HALAnalogAccumulator.HAL_SetAccumulatorCenterDelegate));

Base.HALAnalogAccumulator.HAL_SetAccumulatorDeadband = (Base.HALAnalogAccumulator.HAL_SetAccumulatorDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAccumulatorDeadband"), typeof(Base.HALAnalogAccumulator.HAL_SetAccumulatorDeadbandDelegate));

Base.HALAnalogAccumulator.HAL_GetAccumulatorValue = (Base.HALAnalogAccumulator.HAL_GetAccumulatorValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAccumulatorValue"), typeof(Base.HALAnalogAccumulator.HAL_GetAccumulatorValueDelegate));

Base.HALAnalogAccumulator.HAL_GetAccumulatorCount = (Base.HALAnalogAccumulator.HAL_GetAccumulatorCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAccumulatorCount"), typeof(Base.HALAnalogAccumulator.HAL_GetAccumulatorCountDelegate));

Base.HALAnalogAccumulator.HAL_GetAccumulatorOutput = (Base.HALAnalogAccumulator.HAL_GetAccumulatorOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAccumulatorOutput"), typeof(Base.HALAnalogAccumulator.HAL_GetAccumulatorOutputDelegate));
}
}
}

