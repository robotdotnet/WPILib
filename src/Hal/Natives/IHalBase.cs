using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public interface IHalBase
    {
        int HAL_Initialize(int timeout, int mode);

        unsafe byte* HAL_GetErrorMessage(int code);

        [StatusCheckLastParameter]
        int HAL_GetFPGAVersion();

        [StatusCheckLastParameter]
        long HAL_GetFPGARevision();

        RuntimeType HAL_GetRuntimeType();

        [StatusCheckLastParameter]
        int HAL_GetFPGAButton();

        [StatusCheckLastParameter]
        int HAL_GetSystemActive();

        [StatusCheckLastParameter]
        int HAL_GetBrownedOut();

        int HAL_GetPort(int channel);

        int HAL_GetPortWithModule(int module, int channel);

        [StatusCheckLastParameter]
        ulong HAL_GetFPGATime();

        [StatusCheckLastParameter]
        ulong HAL_ExpandFPGATime(uint unexpandedLower);
    }
}

