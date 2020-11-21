using Hal.Natives;
using WPIUtil;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{
    public enum RuntimeType : int
    {
        Athena,
        Mock
    }



    public static class HALLowLevel
    {
        internal static HALLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static bool Initialize()
        {
            if (lowLevel == null)
            {
                // TODO Generate
            }
            return lowLevel!.HAL_Initialize(500, 0) != 0;
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
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
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
