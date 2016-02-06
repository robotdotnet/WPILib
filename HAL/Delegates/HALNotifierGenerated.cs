//File automatically generated using robotdotnet-tools. Please do not modify.

using System;

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALNotifier
    {
        static HALNotifier()
        {
            HAL.Initialize();
        }

        public delegate NotifierSafeHandle InitializeNotifierDelegate(Action<ulong, IntPtr> Process, IntPtr param, ref int status);
        public static InitializeNotifierDelegate InitializeNotifier;

        public delegate IntPtr GetNotifierParamDelegate(NotifierSafeHandle notifier_pointer, ref int status);
        public static GetNotifierParamDelegate GetNotifierParam;

        public delegate void CleanNotifierDelegate(NotifierSafeHandle notifier_pointer, ref int status);
        public static CleanNotifierDelegate CleanNotifier;

        public delegate void UpdateNotifierAlarmDelegate(NotifierSafeHandle notifier_pointer, ulong triggerTime, ref int status);
        public static UpdateNotifierAlarmDelegate UpdateNotifierAlarm;

        public delegate void StopNotifierAlarmDelegate(NotifierSafeHandle notifier_pointer, ref int status);
        public static StopNotifierAlarmDelegate StopNotifierAlarm;
    }
}
