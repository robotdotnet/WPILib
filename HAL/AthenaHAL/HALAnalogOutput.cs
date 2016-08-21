using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALAnalogOutput
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALAnalogOutput.HAL_InitializeAnalogOutputPort = (Base.HALAnalogOutput.HAL_InitializeAnalogOutputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeAnalogOutputPort"), typeof(Base.HALAnalogOutput.HAL_InitializeAnalogOutputPortDelegate));

Base.HALAnalogOutput.HAL_FreeAnalogOutputPort = (Base.HALAnalogOutput.HAL_FreeAnalogOutputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeAnalogOutputPort"), typeof(Base.HALAnalogOutput.HAL_FreeAnalogOutputPortDelegate));

Base.HALAnalogOutput.HAL_SetAnalogOutput = (Base.HALAnalogOutput.HAL_SetAnalogOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogOutput"), typeof(Base.HALAnalogOutput.HAL_SetAnalogOutputDelegate));

Base.HALAnalogOutput.HAL_GetAnalogOutput = (Base.HALAnalogOutput.HAL_GetAnalogOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogOutput"), typeof(Base.HALAnalogOutput.HAL_GetAnalogOutputDelegate));

Base.HALAnalogOutput.HAL_CheckAnalogOutputChannel = (Base.HALAnalogOutput.HAL_CheckAnalogOutputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckAnalogOutputChannel"), typeof(Base.HALAnalogOutput.HAL_CheckAnalogOutputChannelDelegate));
}
}
}

