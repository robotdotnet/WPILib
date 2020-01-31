using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface IExtensions
    {
        int HAL_LoadExtensions();

        int HAL_LoadOneExtension(byte* library);

        void HAL_SetShowExtensionsNotFoundMessages(int showMessage);

    }
}
