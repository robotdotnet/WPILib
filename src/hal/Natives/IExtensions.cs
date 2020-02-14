using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IExtensions
    {
        int HAL_LoadExtensions();

        int HAL_LoadOneExtension(byte* library);

        void HAL_SetShowExtensionsNotFoundMessages(int showMessage);

    }
}
