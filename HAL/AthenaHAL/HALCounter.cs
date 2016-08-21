using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALCounter
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALCounter.HAL_InitializeCounter = (Base.HALCounter.HAL_InitializeCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeCounter"), typeof(Base.HALCounter.HAL_InitializeCounterDelegate));

Base.HALCounter.HAL_FreeCounter = (Base.HALCounter.HAL_FreeCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeCounter"), typeof(Base.HALCounter.HAL_FreeCounterDelegate));

Base.HALCounter.HAL_SetCounterAverageSize = (Base.HALCounter.HAL_SetCounterAverageSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterAverageSize"), typeof(Base.HALCounter.HAL_SetCounterAverageSizeDelegate));

Base.HALCounter.HAL_SetCounterUpSource = (Base.HALCounter.HAL_SetCounterUpSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterUpSource"), typeof(Base.HALCounter.HAL_SetCounterUpSourceDelegate));

Base.HALCounter.HAL_SetCounterUpSourceEdge = (Base.HALCounter.HAL_SetCounterUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterUpSourceEdge"), typeof(Base.HALCounter.HAL_SetCounterUpSourceEdgeDelegate));

Base.HALCounter.HAL_ClearCounterUpSource = (Base.HALCounter.HAL_ClearCounterUpSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ClearCounterUpSource"), typeof(Base.HALCounter.HAL_ClearCounterUpSourceDelegate));

Base.HALCounter.HAL_SetCounterDownSource = (Base.HALCounter.HAL_SetCounterDownSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterDownSource"), typeof(Base.HALCounter.HAL_SetCounterDownSourceDelegate));

Base.HALCounter.HAL_SetCounterDownSourceEdge = (Base.HALCounter.HAL_SetCounterDownSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterDownSourceEdge"), typeof(Base.HALCounter.HAL_SetCounterDownSourceEdgeDelegate));

Base.HALCounter.HAL_ClearCounterDownSource = (Base.HALCounter.HAL_ClearCounterDownSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ClearCounterDownSource"), typeof(Base.HALCounter.HAL_ClearCounterDownSourceDelegate));

Base.HALCounter.HAL_SetCounterUpDownMode = (Base.HALCounter.HAL_SetCounterUpDownModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterUpDownMode"), typeof(Base.HALCounter.HAL_SetCounterUpDownModeDelegate));

Base.HALCounter.HAL_SetCounterExternalDirectionMode = (Base.HALCounter.HAL_SetCounterExternalDirectionModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterExternalDirectionMode"), typeof(Base.HALCounter.HAL_SetCounterExternalDirectionModeDelegate));

Base.HALCounter.HAL_SetCounterSemiPeriodMode = (Base.HALCounter.HAL_SetCounterSemiPeriodModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterSemiPeriodMode"), typeof(Base.HALCounter.HAL_SetCounterSemiPeriodModeDelegate));

Base.HALCounter.HAL_SetCounterPulseLengthMode = (Base.HALCounter.HAL_SetCounterPulseLengthModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterPulseLengthMode"), typeof(Base.HALCounter.HAL_SetCounterPulseLengthModeDelegate));

Base.HALCounter.HAL_GetCounterSamplesToAverage = (Base.HALCounter.HAL_GetCounterSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCounterSamplesToAverage"), typeof(Base.HALCounter.HAL_GetCounterSamplesToAverageDelegate));

Base.HALCounter.HAL_SetCounterSamplesToAverage = (Base.HALCounter.HAL_SetCounterSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterSamplesToAverage"), typeof(Base.HALCounter.HAL_SetCounterSamplesToAverageDelegate));

Base.HALCounter.HAL_ResetCounter = (Base.HALCounter.HAL_ResetCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ResetCounter"), typeof(Base.HALCounter.HAL_ResetCounterDelegate));

Base.HALCounter.HAL_GetCounter = (Base.HALCounter.HAL_GetCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCounter"), typeof(Base.HALCounter.HAL_GetCounterDelegate));

Base.HALCounter.HAL_GetCounterPeriod = (Base.HALCounter.HAL_GetCounterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCounterPeriod"), typeof(Base.HALCounter.HAL_GetCounterPeriodDelegate));

Base.HALCounter.HAL_SetCounterMaxPeriod = (Base.HALCounter.HAL_SetCounterMaxPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterMaxPeriod"), typeof(Base.HALCounter.HAL_SetCounterMaxPeriodDelegate));

Base.HALCounter.HAL_SetCounterUpdateWhenEmpty = (Base.HALCounter.HAL_SetCounterUpdateWhenEmptyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterUpdateWhenEmpty"), typeof(Base.HALCounter.HAL_SetCounterUpdateWhenEmptyDelegate));

Base.HALCounter.HAL_GetCounterStopped = (Base.HALCounter.HAL_GetCounterStoppedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCounterStopped"), typeof(Base.HALCounter.HAL_GetCounterStoppedDelegate));

Base.HALCounter.HAL_GetCounterDirection = (Base.HALCounter.HAL_GetCounterDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCounterDirection"), typeof(Base.HALCounter.HAL_GetCounterDirectionDelegate));

Base.HALCounter.HAL_SetCounterReverseDirection = (Base.HALCounter.HAL_SetCounterReverseDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCounterReverseDirection"), typeof(Base.HALCounter.HAL_SetCounterReverseDirectionDelegate));
}
}
}

