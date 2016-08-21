using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALNotifier
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALNotifier.HAL_InitializeNotifier = (Base.HALNotifier.HAL_InitializeNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeNotifierShim"), typeof(Base.HALNotifier.HAL_InitializeNotifierDelegate));

Base.HALNotifier.HAL_CleanNotifier = (Base.HALNotifier.HAL_CleanNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CleanNotifierShim"), typeof(Base.HALNotifier.HAL_CleanNotifierDelegate));

Base.HALNotifier.HAL_GetNotifierParam = (Base.HALNotifier.HAL_GetNotifierParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNotifierParam"), typeof(Base.HALNotifier.HAL_GetNotifierParamDelegate));

Base.HALNotifier.HAL_UpdateNotifierAlarm = (Base.HALNotifier.HAL_UpdateNotifierAlarmDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_UpdateNotifierAlarm"), typeof(Base.HALNotifier.HAL_UpdateNotifierAlarmDelegate));

Base.HALNotifier.HAL_StopNotifierAlarm = (Base.HALNotifier.HAL_StopNotifierAlarmDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_StopNotifierAlarm"), typeof(Base.HALNotifier.HAL_StopNotifierAlarmDelegate));
}
}
}

