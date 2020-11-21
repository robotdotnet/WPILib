
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{
    public static unsafe class ExtensionsLowLevel
    {
        internal static ExtensionsLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static int Load()
        {
            return lowLevel.HAL_LoadExtensions();
        }

        public static int LoadOneExtension(byte* library)
        {
            return lowLevel.HAL_LoadOneExtension(library);
        }

        public static void SetShowNotFoundMessages(int showMessage)
        {
            lowLevel.HAL_SetShowExtensionsNotFoundMessages(showMessage);
        }

    }
}
