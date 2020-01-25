using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface IHALBase
    {
        int HAL_Initialize(int timeout, int mode);

        [StatusCheckLastParameter] ulong HAL_ExpandFPGATime(uint unexpanded_lower);

        [StatusCheckLastParameter] int HAL_GetBrownedOut();

        byte* HAL_GetErrorMessage(int code);

        [StatusCheckLastParameter] int HAL_GetFPGAButton();

        [StatusCheckLastParameter] long HAL_GetFPGARevision();

        [StatusCheckLastParameter] ulong HAL_GetFPGATime();

        [StatusCheckLastParameter] int HAL_GetFPGAVersion();

        int HAL_GetPort(int channel);

        int HAL_GetPortWithModule(int module, int channel);

        RuntimeType HAL_GetRuntimeType();

        [StatusCheckLastParameter] int HAL_GetSystemActive();

    }
}
