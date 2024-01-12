using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalAnalogAccumulator
{
    public static long GetAccumulatorCount(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAccumulatorCountRefShim(analogPortHandle, ref status);
    }
    public static void GetAccumulatorOutput(HalAnalogInputHandle analogPortHandle, out long value, out long count, out HalStatus status)
    {
        status = HalStatus.Ok;
        GetAccumulatorOutputRefShim(analogPortHandle, out value, out count, ref status);
    }
    public static long GetAccumulatorValue(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAccumulatorValueRefShim(analogPortHandle, ref status);
    }
    public static void InitAccumulator(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        InitAccumulatorRefShim(analogPortHandle, ref status);
    }
    public static int IsAccumulatorChannel(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return IsAccumulatorChannelRefShim(analogPortHandle, ref status);
    }
    public static void ResetAccumulator(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        ResetAccumulatorRefShim(analogPortHandle, ref status);
    }
    public static void SetAccumulatorCenter(HalAnalogInputHandle analogPortHandle, int center, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAccumulatorCenterRefShim(analogPortHandle, center, ref status);
    }
    public static void SetAccumulatorDeadband(HalAnalogInputHandle analogPortHandle, int deadband, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAccumulatorDeadbandRefShim(analogPortHandle, deadband, ref status);
    }
}
