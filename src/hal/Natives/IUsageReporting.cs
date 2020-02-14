using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IUsageReporting
    {
        int HAL_Report(int resource, int instanceNumber, int context, byte* feature);
    }
}
