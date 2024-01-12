using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalI2C
{
    public static void InitializeI2C(I2CPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        InitializeI2CRefShim(port, ref status);
    }
}
