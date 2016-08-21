using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALRelay
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALRelay.HAL_InitializeRelayPort = (Base.HALRelay.HAL_InitializeRelayPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeRelayPort"), typeof(Base.HALRelay.HAL_InitializeRelayPortDelegate));

Base.HALRelay.HAL_FreeRelayPort = (Base.HALRelay.HAL_FreeRelayPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeRelayPort"), typeof(Base.HALRelay.HAL_FreeRelayPortDelegate));

Base.HALRelay.HAL_CheckRelayChannel = (Base.HALRelay.HAL_CheckRelayChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckRelayChannel"), typeof(Base.HALRelay.HAL_CheckRelayChannelDelegate));

Base.HALRelay.HAL_SetRelay = (Base.HALRelay.HAL_SetRelayDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetRelay"), typeof(Base.HALRelay.HAL_SetRelayDelegate));

Base.HALRelay.HAL_GetRelay = (Base.HALRelay.HAL_GetRelayDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetRelay"), typeof(Base.HALRelay.HAL_GetRelayDelegate));
}
}
}

