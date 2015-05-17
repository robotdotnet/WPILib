using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace HAL_Base
{
    public enum AccelerometerRange
    {
        /// kRange_2G -> 0
        Range_2G = 0,

        /// kRange_4G -> 1
        Range_4G = 1,

        /// kRange_8G -> 2
        Range_8G = 2,
    }

    public class HALAccelerometer
    {

        internal static void SetupDelegate()
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

        public delegate void SetAccelerometerActiveDelegate([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool param0);
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
