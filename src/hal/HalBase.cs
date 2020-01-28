using Hal.Natives;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using WPIUtil;
using WPIUtil.NativeUtilities;

namespace Hal
{
    public enum RuntimeType : int
    {
        Athena,
        Mock
    }


    [NativeInterface(typeof(IHALBase))]
    public static class HalBase
    {
        public static void StatusCheck(int status)
        {
            if (status != 0)
            {
                throw new UncleanStatusException(status);
            }
            ;
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IHALBase lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static bool Initialize()
        {
            if (!NativeInterfaceInitializer.LoadAndInitializeNativeTypes(typeof(HalBase).Assembly, 
                "wpiHal", typeof(HalBase).GetMethod(nameof(StatusCheck), BindingFlags.Public | BindingFlags.Static), 
                out var generator))
            {
                return false;
            }
            return lowLevel.HAL_Initialize(500, 0) != 0;
        }

        public static int GetPort(int channel)
        {
            return lowLevel.HAL_GetPort(channel);
        }

        public static ulong ExpandFPGATime(uint unexpanded)
        {
            return lowLevel.HAL_ExpandFPGATime(unexpanded);
        }

        public static bool GetBrownedOut()
        {
            return lowLevel.HAL_GetBrownedOut() != 0;
        }

        public static unsafe string GetErrorMessage(int code)
        {
            try
            {
                return UTF8String.ReadUTF8String(lowLevel.HAL_GetErrorMessage(code));
            }
            catch
            {
                return "Error determining status";
            }
        }

        public static int GetPortWithModule(int module, int channel)
        {
            return lowLevel.HAL_GetPortWithModule(module, channel);
        }

        public static int GetFPGAVersion()
        {
            return lowLevel.HAL_GetFPGAVersion();
        }

        public static ulong GetFPGATimestamp()
        {
            return lowLevel.HAL_GetFPGATime();
        }

        public static RuntimeType GetRuntimeType()
        {
            return lowLevel.HAL_GetRuntimeType();
        }

        public static bool GetUserButton()
        {
            return lowLevel.HAL_GetFPGAButton() != 0;
        }

        public static bool GetSystemActive()
        {
            return lowLevel.HAL_GetSystemActive() != 0;
        }
    }
}
