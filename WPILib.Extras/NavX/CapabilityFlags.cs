using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    public enum CapabilityFlags : byte
    {
        OMNIMOUNT = 4,
        VEL_AND_DISP = 64,
        YAW_RESET = 128,
        OMNIMOUNT_CONFIG_MASK = 56
    }

    public enum OpStatus : byte
    {
        INITIALIZING = 0,
        SELFTEST_IN_PROGRESS = 1,
        ERROR = 2,
        IMU_AUTOCAL_IN_PROGRESS =3,
        NORMAL = 4
    }

    [Flags]
    public enum CalStatus : byte
    {
        IMU_CAL_STATE_MASK = 3,
        IMU_CAL_INPROGRESS = 0,
        IMU_CAL_ACCUMULATE = 1,
        IMU_CAL_COMPLETE = 2,
        MAG_CAL_COMPLETE = 4,
        BARO_CAL_COMPLETE = 8
    }

    [Flags]
    public enum SelfTestStatus : byte
    {
        COMPLETE = 80,
        GYRO_PASSED = 1,
        ACCEL_PASSED = 2,
        MAG_PASSED = 4,
        BARO_PASSED = 8,
    }
}
