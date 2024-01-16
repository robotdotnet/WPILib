using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalInterrupts
{
    public static HalInterruptHandle InitializeInterrupts(out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeInterruptsRefShim(ref status);
    }
    public static long ReadInterruptFallingTimestamp(HalInterruptHandle interruptHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return ReadInterruptFallingTimestampRefShim(interruptHandle, ref status);
    }
    public static long ReadInterruptRisingTimestamp(HalInterruptHandle interruptHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return ReadInterruptRisingTimestampRefShim(interruptHandle, ref status);
    }
    public static void RequestInterrupts(HalInterruptHandle interruptHandle, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, out HalStatus status)
    {
        status = HalStatus.Ok;
        RequestInterruptsRefShim(interruptHandle, digitalSourceHandle, analogTriggerType, ref status);
    }
    public static void SetInterruptUpSourceEdge(HalInterruptHandle interruptHandle, int risingEdge, int fallingEdge, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetInterruptUpSourceEdgeRefShim(interruptHandle, risingEdge, fallingEdge, ref status);
    }
    public static long WaitForInterrupt(HalInterruptHandle interruptHandle, double timeout, int ignorePrevious, out HalStatus status)
    {
        status = HalStatus.Ok;
        return WaitForInterruptRefShim(interruptHandle, timeout, ignorePrevious, ref status);
    }
}
