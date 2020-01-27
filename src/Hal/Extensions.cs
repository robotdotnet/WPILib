
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IExtensions))]
    public unsafe static class Extensions
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IExtensions lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static int LoadExtensions()
{
return lowLevel.HAL_LoadExtensions();
}

public static int LoadOneExtension(byte* library)
{
return lowLevel.HAL_LoadOneExtension(library);
}

public static void SetShowExtensionsNotFoundMessages(int showMessage)
{
lowLevel.HAL_SetShowExtensionsNotFoundMessages(showMessage);
}

}
}
