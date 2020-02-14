
using Hal.Natives;

namespace Hal
{
    //[NativeInterface(typeof(IExtensions))]
    public static unsafe class ExtensionsLowLevel
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IExtensions lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
