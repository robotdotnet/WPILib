//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HALAccelerometer
    {
        static HALAccelerometer()
        {
            HAL.Initialize();
        }

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            SetAccelerometerActive = (SetAccelerometerActiveDelegate)Delegate.CreateDelegate(typeof(SetAccelerometerActiveDelegate), type.GetMethod("setAccelerometerActive"));
            SetAccelerometerRange = (SetAccelerometerRangeDelegate)Delegate.CreateDelegate(typeof(SetAccelerometerRangeDelegate), type.GetMethod("setAccelerometerRange"));
            GetAccelerometerX = (GetAccelerometerXDelegate)Delegate.CreateDelegate(typeof(GetAccelerometerXDelegate), type.GetMethod("getAccelerometerX"));
            GetAccelerometerY = (GetAccelerometerYDelegate)Delegate.CreateDelegate(typeof(GetAccelerometerYDelegate), type.GetMethod("getAccelerometerY"));
            GetAccelerometerZ = (GetAccelerometerZDelegate)Delegate.CreateDelegate(typeof(GetAccelerometerZDelegate), type.GetMethod("getAccelerometerZ"));
        }

        public delegate void SetAccelerometerActiveDelegate(bool param0);
        public static SetAccelerometerActiveDelegate SetAccelerometerActive;

        public delegate void SetAccelerometerRangeDelegate(AccelerometerRange param0);
        public static SetAccelerometerRangeDelegate SetAccelerometerRange;

        public delegate double GetAccelerometerXDelegate();
        public static GetAccelerometerXDelegate GetAccelerometerX;

        public delegate double GetAccelerometerYDelegate();
        public static GetAccelerometerYDelegate GetAccelerometerY;

        public delegate double GetAccelerometerZDelegate();
        public static GetAccelerometerZDelegate GetAccelerometerZ;
    }
}
