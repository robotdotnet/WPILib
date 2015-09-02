//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALNotifier
    {
        static HALNotifier()
        {
            HAL.Initialize();
        }

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            InitializeNotifier = (InitializeNotifierDelegate)Delegate.CreateDelegate(typeof(InitializeNotifierDelegate), type.GetMethod("initializeNotifier"));
            CleanNotifier = (CleanNotifierDelegate)Delegate.CreateDelegate(typeof(CleanNotifierDelegate), type.GetMethod("cleanNotifier"));
            UpdateNotifierAlarm = (UpdateNotifierAlarmDelegate)Delegate.CreateDelegate(typeof(UpdateNotifierAlarmDelegate), type.GetMethod("updateNotifierAlarm"));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NotifierDelegate(uint mask, IntPtr val);
        public delegate IntPtr InitializeNotifierDelegate(NotifierDelegate ProcessQueue, ref int status);
        public static InitializeNotifierDelegate InitializeNotifier;

        public delegate void CleanNotifierDelegate(IntPtr notifier_pointer, ref int status);
        public static CleanNotifierDelegate CleanNotifier;

        public delegate void UpdateNotifierAlarmDelegate(IntPtr notifier_pointer, uint triggerTime, ref int status);
        public static UpdateNotifierAlarmDelegate UpdateNotifierAlarm;
    }
}
