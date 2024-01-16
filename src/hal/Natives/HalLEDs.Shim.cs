namespace WPIHal.Natives;

public static unsafe partial class HalLEDs
{
    public static void SetRadioLEDState(RadioLEDState state, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetRadioLEDStateRefShim(state, ref status);
    }
    public static RadioLEDState GetRadioLEDState(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetRadioLEDStateRefShim(ref status);
    }
}
