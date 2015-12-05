using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    [Flags]
    public enum DeviceRegisters : byte
    {
        WHOAMI = 0x00,
        HW_REV = 0x01,
        FW_VER_MAJOR = 0x02,
        FW_VER_MINOR = 0x03,
        UPDATE_RATE_HZ = 0x04,
        SENSOR_STATUS = 0x10,
        //TODO: Add the rest of these.
        CAPATABILITY_FLAGS_L = 0x0B,
        INTEGRATION_CONTROL = 0x56,
        YAW = 0x18,
        FUSED_HEADING = 0x1E,

    }
}
