using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IConstants
    {
        int HAL_GetSystemClockTicksPerMicrosecond();

    }
}
