using Hal.Natives;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IHalBase))]
    public static class HalBase
    {
        public static void StatusCheck(int status)
        {
            ;
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IHalBase halBase;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static bool HAL_Initialize()
        {
            if (!NativeInterfaceInitializer.LoadAndInitializeNativeTypes(typeof(HalBase).Assembly, 
                "wpiHal", typeof(HalBase).GetMethod(nameof(StatusCheck), BindingFlags.Public | BindingFlags.Static), 
                out var generator))
            {
                return false;
            }
            return halBase.HAL_Initialize(500, 0) != 0;
        }

        public static int GetPort(int channel)
        {
            return halBase.HAL_GetPort(channel);
        }

        public static int GetFPGAVersion()
        {
            return halBase.HAL_GetFPGAVersion();
        }
    }
}
