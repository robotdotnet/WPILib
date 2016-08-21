using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALNotifier
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALNotifier.HAL_InitializeNotifier = HAL_InitializeNotifier;
            Base.HALNotifier.HAL_CleanNotifier = HAL_CleanNotifier;
            Base.HALNotifier.HAL_GetNotifierParam = HAL_GetNotifierParam;
            Base.HALNotifier.HAL_UpdateNotifierAlarm = HAL_UpdateNotifierAlarm;
            Base.HALNotifier.HAL_StopNotifierAlarm = HAL_StopNotifierAlarm;
        }

        public static int HAL_InitializeNotifier(Action<ulong, IntPtr> process, IntPtr param, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_CleanNotifier(int notifier_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static IntPtr HAL_GetNotifierParam(int notifier_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_UpdateNotifierAlarm(int notifier_handle, ulong triggerTime, ref int status)
        {
        }

        public static void HAL_StopNotifierAlarm(int notifier_handle, ref int status)
        {
        }
    }
}

