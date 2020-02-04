using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IConstants
    {
        int HAL_GetSystemClockTicksPerMicrosecond();

    }
}
