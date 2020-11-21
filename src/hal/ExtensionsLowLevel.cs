
using Hal.Natives;

namespace Hal
{
    public static unsafe class ExtensionsLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static ExtensionsLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

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
