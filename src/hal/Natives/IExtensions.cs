using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IExtensions
    {
        int HAL_LoadExtensions();

        int HAL_LoadOneExtension(byte* library);

        void HAL_SetShowExtensionsNotFoundMessages(int showMessage);

    }
}
