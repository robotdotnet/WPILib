
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    /// Return Type: void
    ///param0: unsigned int
    ///param1: void*
    public delegate void NotifierDelegate(uint param0, System.IntPtr param1);

    public class HALNotifier
    {
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

        public delegate System.IntPtr InitializeNotifierDelegate(NotifierDelegate processQueue, ref int status);
        public static InitializeNotifierDelegate InitializeNotifier;

        public delegate void CleanNotifierDelegate(System.IntPtr notifierPointer, ref int status);
        public static CleanNotifierDelegate CleanNotifier;

        public delegate void UpdateNotifierAlarmDelegate(System.IntPtr notifierPointer, uint triggerTime, ref int status);
        public static UpdateNotifierAlarmDelegate UpdateNotifierAlarm;
    }
}
