//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HALUtilities
    {
        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            DelayTicks = (DelayTicksDelegate)Delegate.CreateDelegate(typeof(DelayTicksDelegate), type.GetMethod("delayTicks"));
            DelayMillis = (DelayMillisDelegate)Delegate.CreateDelegate(typeof(DelayMillisDelegate), type.GetMethod("delayMillis"));
            DelaySeconds = (DelaySecondsDelegate)Delegate.CreateDelegate(typeof(DelaySecondsDelegate), type.GetMethod("delaySeconds"));
        }

        public delegate void DelayTicksDelegate(int ticks);
        public static DelayTicksDelegate DelayTicks;

        public delegate void DelayMillisDelegate(double ms);
        public static DelayMillisDelegate DelayMillis;

        public delegate void DelaySecondsDelegate(double s);
        public static DelaySecondsDelegate DelaySeconds;
    }
}
