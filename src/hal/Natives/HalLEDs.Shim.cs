using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

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
