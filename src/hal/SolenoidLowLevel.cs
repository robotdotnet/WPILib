
using Hal.Natives;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class SolenoidLowLevel
    {
        internal static SolenoidLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static int CheckChannel(int channel)
        {
            return lowLevel.HAL_CheckSolenoidChannel(channel);
        }

        public static int CheckModule(int module)
        {
            return lowLevel.HAL_CheckSolenoidModule(module);
        }

        public static void ClearAllPCMStickyFaults(int module)
        {
            lowLevel.HAL_ClearAllPCMStickyFaults(module);
        }

#pragma warning disable CA1030 // Use events where appropriate
        public static void FireOneShot(int solenoidPortHandle)
#pragma warning restore CA1030 // Use events where appropriate
        {
            lowLevel.HAL_FireOneShot(solenoidPortHandle);
        }

        public static void FreePort(int solenoidPortHandle)
        {
            lowLevel.HAL_FreeSolenoidPort(solenoidPortHandle);
        }

        public static int GetAlls(int module)
        {
            return lowLevel.HAL_GetAllSolenoids(module);
        }

        public static int GetPCMBlackList(int module)
        {
            return lowLevel.HAL_GetPCMSolenoidBlackList(module);
        }

        public static int GetPCMVoltageFault(int module)
        {
            return lowLevel.HAL_GetPCMSolenoidVoltageFault(module);
        }

        public static int GetPCMVoltageStickyFault(int module)
        {
            return lowLevel.HAL_GetPCMSolenoidVoltageStickyFault(module);
        }

        public static int Get(int solenoidPortHandle)
        {
            return lowLevel.HAL_GetSolenoid(solenoidPortHandle);
        }

        public static int InitializePort(int portHandle)
        {
            return lowLevel.HAL_InitializeSolenoidPort(portHandle);
        }

        public static void SetAlls(int module, int state)
        {
            lowLevel.HAL_SetAllSolenoids(module, state);
        }

        public static void SetOneShotDuration(int solenoidPortHandle, int durMS)
        {
            lowLevel.HAL_SetOneShotDuration(solenoidPortHandle, durMS);
        }

        public static void Set(int solenoidPortHandle, int value)
        {
            lowLevel.HAL_SetSolenoid(solenoidPortHandle, value);
        }

    }
}
