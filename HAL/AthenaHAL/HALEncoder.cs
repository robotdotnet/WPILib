using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALEncoder
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALEncoder.HAL_InitializeEncoder = (Base.HALEncoder.HAL_InitializeEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeEncoder"), typeof(Base.HALEncoder.HAL_InitializeEncoderDelegate));

Base.HALEncoder.HAL_FreeEncoder = (Base.HALEncoder.HAL_FreeEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeEncoder"), typeof(Base.HALEncoder.HAL_FreeEncoderDelegate));

Base.HALEncoder.HAL_GetEncoder = (Base.HALEncoder.HAL_GetEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoder"), typeof(Base.HALEncoder.HAL_GetEncoderDelegate));

Base.HALEncoder.HAL_GetEncoderRaw = (Base.HALEncoder.HAL_GetEncoderRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderRaw"), typeof(Base.HALEncoder.HAL_GetEncoderRawDelegate));

Base.HALEncoder.HAL_GetEncoderEncodingScale = (Base.HALEncoder.HAL_GetEncoderEncodingScaleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderEncodingScale"), typeof(Base.HALEncoder.HAL_GetEncoderEncodingScaleDelegate));

Base.HALEncoder.HAL_ResetEncoder = (Base.HALEncoder.HAL_ResetEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ResetEncoder"), typeof(Base.HALEncoder.HAL_ResetEncoderDelegate));

Base.HALEncoder.HAL_GetEncoderPeriod = (Base.HALEncoder.HAL_GetEncoderPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderPeriod"), typeof(Base.HALEncoder.HAL_GetEncoderPeriodDelegate));

Base.HALEncoder.HAL_SetEncoderMaxPeriod = (Base.HALEncoder.HAL_SetEncoderMaxPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetEncoderMaxPeriod"), typeof(Base.HALEncoder.HAL_SetEncoderMaxPeriodDelegate));

Base.HALEncoder.HAL_GetEncoderStopped = (Base.HALEncoder.HAL_GetEncoderStoppedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderStopped"), typeof(Base.HALEncoder.HAL_GetEncoderStoppedDelegate));

Base.HALEncoder.HAL_GetEncoderDirection = (Base.HALEncoder.HAL_GetEncoderDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderDirection"), typeof(Base.HALEncoder.HAL_GetEncoderDirectionDelegate));

Base.HALEncoder.HAL_GetEncoderDistance = (Base.HALEncoder.HAL_GetEncoderDistanceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderDistance"), typeof(Base.HALEncoder.HAL_GetEncoderDistanceDelegate));

Base.HALEncoder.HAL_GetEncoderRate = (Base.HALEncoder.HAL_GetEncoderRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderRate"), typeof(Base.HALEncoder.HAL_GetEncoderRateDelegate));

Base.HALEncoder.HAL_SetEncoderMinRate = (Base.HALEncoder.HAL_SetEncoderMinRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetEncoderMinRate"), typeof(Base.HALEncoder.HAL_SetEncoderMinRateDelegate));

Base.HALEncoder.HAL_SetEncoderDistancePerPulse = (Base.HALEncoder.HAL_SetEncoderDistancePerPulseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetEncoderDistancePerPulse"), typeof(Base.HALEncoder.HAL_SetEncoderDistancePerPulseDelegate));

Base.HALEncoder.HAL_SetEncoderReverseDirection = (Base.HALEncoder.HAL_SetEncoderReverseDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetEncoderReverseDirection"), typeof(Base.HALEncoder.HAL_SetEncoderReverseDirectionDelegate));

Base.HALEncoder.HAL_SetEncoderSamplesToAverage = (Base.HALEncoder.HAL_SetEncoderSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetEncoderSamplesToAverage"), typeof(Base.HALEncoder.HAL_SetEncoderSamplesToAverageDelegate));

Base.HALEncoder.HAL_GetEncoderSamplesToAverage = (Base.HALEncoder.HAL_GetEncoderSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderSamplesToAverage"), typeof(Base.HALEncoder.HAL_GetEncoderSamplesToAverageDelegate));

Base.HALEncoder.HAL_SetEncoderIndexSource = (Base.HALEncoder.HAL_SetEncoderIndexSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetEncoderIndexSource"), typeof(Base.HALEncoder.HAL_SetEncoderIndexSourceDelegate));

Base.HALEncoder.HAL_GetEncoderFPGAIndex = (Base.HALEncoder.HAL_GetEncoderFPGAIndexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderFPGAIndex"), typeof(Base.HALEncoder.HAL_GetEncoderFPGAIndexDelegate));

Base.HALEncoder.HAL_GetEncoderDecodingScaleFactor = (Base.HALEncoder.HAL_GetEncoderDecodingScaleFactorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderDecodingScaleFactor"), typeof(Base.HALEncoder.HAL_GetEncoderDecodingScaleFactorDelegate));

Base.HALEncoder.HAL_GetEncoderDistancePerPulse = (Base.HALEncoder.HAL_GetEncoderDistancePerPulseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderDistancePerPulse"), typeof(Base.HALEncoder.HAL_GetEncoderDistancePerPulseDelegate));

Base.HALEncoder.HAL_GetEncoderEncodingType = (Base.HALEncoder.HAL_GetEncoderEncodingTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetEncoderEncodingType"), typeof(Base.HALEncoder.HAL_GetEncoderEncodingTypeDelegate));
}
}
}

