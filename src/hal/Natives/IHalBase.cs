using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IHAL
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
        int HAL_Initialize(int timeout, int mode);

        [StatusCheckLastParameter] ulong HAL_ExpandFPGATime(uint unexpanded_lower);

        [StatusCheckLastParameter] int HAL_GetBrownedOut();

        byte* HAL_GetErrorMessage(int code);

        [StatusCheckLastParameter] int HAL_GetFPGAButton();

        [StatusCheckLastParameter] long HAL_GetFPGARevision();

        [StatusCheckLastParameter] ulong HAL_GetFPGATime();

        [StatusCheckLastParameter] int HAL_GetFPGAVersion();

        int HAL_GetPort(int channel);

#pragma warning disable CA1716 // Identifiers should not match keywords
        int HAL_GetPortWithModule(int module, int channel);
#pragma warning restore CA1716 // Identifiers should not match keywords

        RuntimeType HAL_GetRuntimeType();

        [StatusCheckLastParameter] int HAL_GetSystemActive();
#pragma warning restore CA1707 // Identifiers should not contain underscores

    }
}
