using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALNotifier
    {
        static HALNotifier()
        {
            HAL.Initialize();
        }

        public delegate int HAL_InitializeNotifierDelegate(Action<ulong, IntPtr> process, IntPtr param, ref int status);
        public static HAL_InitializeNotifierDelegate HAL_InitializeNotifier;

        public delegate void HAL_CleanNotifierDelegate(int notifier_handle, ref int status);
        public static HAL_CleanNotifierDelegate HAL_CleanNotifier;

        public delegate IntPtr HAL_GetNotifierParamDelegate(int notifier_handle, ref int status);
        public static HAL_GetNotifierParamDelegate HAL_GetNotifierParam;

        public delegate void HAL_UpdateNotifierAlarmDelegate(int notifier_handle, ulong triggerTime, ref int status);
        public static HAL_UpdateNotifierAlarmDelegate HAL_UpdateNotifierAlarm;

        public delegate void HAL_StopNotifierAlarmDelegate(int notifier_handle, ref int status);
        public static HAL_StopNotifierAlarmDelegate HAL_StopNotifierAlarm;
    }
}

