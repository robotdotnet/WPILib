using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalAnalogOutput
{
    public static double GetAnalogOutput(HalAnalogOutputHandle analogOutputHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogOutputRefShim(analogOutputHandle, ref status);
    }
    public static HalAnalogOutputHandle InitializeAnalogOutputPort(HalPortHandle portHandle, string allocationLocation, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeAnalogOutputPortRefShim(portHandle, allocationLocation, ref status);
    }
    public static void SetAnalogOutput(HalAnalogOutputHandle analogOutputHandle, double voltage, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogOutputRefShim(analogOutputHandle, voltage, ref status);
    }
}
