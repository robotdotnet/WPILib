//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;
using HAL;

// ReSharper disable CheckNamespace

namespace HAL
{
    public partial class HALAccelerometer
    {
        static HALAccelerometer()
        {
            HAL.Initialize();
        }

        public delegate void SetAccelerometerActiveDelegate([MarshalAs(UnmanagedType.I1)]bool param0);
        public static SetAccelerometerActiveDelegate SetAccelerometerActive;

        public delegate void SetAccelerometerRangeDelegate(HALAccelerometerRange param0);
        public static SetAccelerometerRangeDelegate SetAccelerometerRange;

        public delegate double GetAccelerometerXDelegate();
        public static GetAccelerometerXDelegate GetAccelerometerX;

        public delegate double GetAccelerometerYDelegate();
        public static GetAccelerometerYDelegate GetAccelerometerY;

        public delegate double GetAccelerometerZDelegate();
        public static GetAccelerometerZDelegate GetAccelerometerZ;
    }
}
