using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALSPI
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALSPI.HAL_InitializeSPI = (Base.HALSPI.HAL_InitializeSPIDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeSPI"), typeof(Base.HALSPI.HAL_InitializeSPIDelegate));

Base.HALSPI.HAL_TransactionSPI = (Base.HALSPI.HAL_TransactionSPIDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_TransactionSPI"), typeof(Base.HALSPI.HAL_TransactionSPIDelegate));

Base.HALSPI.HAL_WriteSPI = (Base.HALSPI.HAL_WriteSPIDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_WriteSPI"), typeof(Base.HALSPI.HAL_WriteSPIDelegate));

Base.HALSPI.HAL_ReadSPI = (Base.HALSPI.HAL_ReadSPIDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ReadSPI"), typeof(Base.HALSPI.HAL_ReadSPIDelegate));

Base.HALSPI.HAL_CloseSPI = (Base.HALSPI.HAL_CloseSPIDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CloseSPI"), typeof(Base.HALSPI.HAL_CloseSPIDelegate));

Base.HALSPI.HAL_SetSPISpeed = (Base.HALSPI.HAL_SetSPISpeedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSPISpeed"), typeof(Base.HALSPI.HAL_SetSPISpeedDelegate));

Base.HALSPI.HAL_SetSPIOpts = (Base.HALSPI.HAL_SetSPIOptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSPIOpts"), typeof(Base.HALSPI.HAL_SetSPIOptsDelegate));

Base.HALSPI.HAL_SetSPIChipSelectActiveHigh = (Base.HALSPI.HAL_SetSPIChipSelectActiveHighDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSPIChipSelectActiveHigh"), typeof(Base.HALSPI.HAL_SetSPIChipSelectActiveHighDelegate));

Base.HALSPI.HAL_SetSPIChipSelectActiveLow = (Base.HALSPI.HAL_SetSPIChipSelectActiveLowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSPIChipSelectActiveLow"), typeof(Base.HALSPI.HAL_SetSPIChipSelectActiveLowDelegate));

Base.HALSPI.HAL_GetSPIHandle = (Base.HALSPI.HAL_GetSPIHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSPIHandle"), typeof(Base.HALSPI.HAL_GetSPIHandleDelegate));

Base.HALSPI.HAL_SetSPIHandle = (Base.HALSPI.HAL_SetSPIHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSPIHandle"), typeof(Base.HALSPI.HAL_SetSPIHandleDelegate));

Base.HALSPI.HAL_InitSPIAccumulator = (Base.HALSPI.HAL_InitSPIAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitSPIAccumulator"), typeof(Base.HALSPI.HAL_InitSPIAccumulatorDelegate));

Base.HALSPI.HAL_FreeSPIAccumulator = (Base.HALSPI.HAL_FreeSPIAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeSPIAccumulator"), typeof(Base.HALSPI.HAL_FreeSPIAccumulatorDelegate));

Base.HALSPI.HAL_ResetSPIAccumulator = (Base.HALSPI.HAL_ResetSPIAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ResetSPIAccumulator"), typeof(Base.HALSPI.HAL_ResetSPIAccumulatorDelegate));

Base.HALSPI.HAL_SetSPIAccumulatorCenter = (Base.HALSPI.HAL_SetSPIAccumulatorCenterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSPIAccumulatorCenter"), typeof(Base.HALSPI.HAL_SetSPIAccumulatorCenterDelegate));

Base.HALSPI.HAL_SetSPIAccumulatorDeadband = (Base.HALSPI.HAL_SetSPIAccumulatorDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSPIAccumulatorDeadband"), typeof(Base.HALSPI.HAL_SetSPIAccumulatorDeadbandDelegate));

Base.HALSPI.HAL_GetSPIAccumulatorLastValue = (Base.HALSPI.HAL_GetSPIAccumulatorLastValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSPIAccumulatorLastValue"), typeof(Base.HALSPI.HAL_GetSPIAccumulatorLastValueDelegate));

Base.HALSPI.HAL_GetSPIAccumulatorValue = (Base.HALSPI.HAL_GetSPIAccumulatorValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSPIAccumulatorValue"), typeof(Base.HALSPI.HAL_GetSPIAccumulatorValueDelegate));

Base.HALSPI.HAL_GetSPIAccumulatorCount = (Base.HALSPI.HAL_GetSPIAccumulatorCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSPIAccumulatorCount"), typeof(Base.HALSPI.HAL_GetSPIAccumulatorCountDelegate));

Base.HALSPI.HAL_GetSPIAccumulatorAverage = (Base.HALSPI.HAL_GetSPIAccumulatorAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSPIAccumulatorAverage"), typeof(Base.HALSPI.HAL_GetSPIAccumulatorAverageDelegate));

Base.HALSPI.HAL_GetSPIAccumulatorOutput = (Base.HALSPI.HAL_GetSPIAccumulatorOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSPIAccumulatorOutput"), typeof(Base.HALSPI.HAL_GetSPIAccumulatorOutputDelegate));
}
}
}

