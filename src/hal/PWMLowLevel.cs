
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class PWMLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static PWMLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

        public static int CheckChannel(int channel)
        {
            return lowLevel.HAL_CheckPWMChannel(channel);
        }

        public static void FreePort(int pwmPortHandle)
        {
            lowLevel.HAL_FreePWMPort(pwmPortHandle);
        }

        public static ulong GetCycleStartTime()
        {
            return lowLevel.HAL_GetPWMCycleStartTime();
        }

        public static int GetEliminateDeadband(int pwmPortHandle)
        {
            return lowLevel.HAL_GetPWMEliminateDeadband(pwmPortHandle);
        }

        public static int GetLoopTiming()
        {
            return lowLevel.HAL_GetPWMLoopTiming();
        }

        public static double GetPosition(int pwmPortHandle)
        {
            return lowLevel.HAL_GetPWMPosition(pwmPortHandle);
        }

        public static int GetRaw(int pwmPortHandle)
        {
            return lowLevel.HAL_GetPWMRaw(pwmPortHandle);
        }

        public static double GetSpeed(int pwmPortHandle)
        {
            return lowLevel.HAL_GetPWMSpeed(pwmPortHandle);
        }

        public static int InitializePort(int portHandle)
        {
            return lowLevel.HAL_InitializePWMPort(portHandle);
        }

        public static void LatchZero(int pwmPortHandle)
        {
            lowLevel.HAL_LatchPWMZero(pwmPortHandle);
        }

        public static void SetConfig(int pwmPortHandle, double maxPwm, double deadbandMaxPwm, double centerPwm, double deadbandMinPwm, double minPwm)
        {
            lowLevel.HAL_SetPWMConfig(pwmPortHandle, maxPwm, deadbandMaxPwm, centerPwm, deadbandMinPwm, minPwm);
        }

        public static void SetConfigRaw(int pwmPortHandle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm)
        {
            lowLevel.HAL_SetPWMConfigRaw(pwmPortHandle, maxPwm, deadbandMaxPwm, centerPwm, deadbandMinPwm, minPwm);
        }

        public static void SetDisabled(int pwmPortHandle)
        {
            lowLevel.HAL_SetPWMDisabled(pwmPortHandle);
        }

        public static void SetEliminateDeadband(int pwmPortHandle, bool eliminateDeadband)
        {
            lowLevel.HAL_SetPWMEliminateDeadband(pwmPortHandle, eliminateDeadband ? 1 : 0);
        }

        public static void SetPeriodScale(int pwmPortHandle, int squelchMask)
        {
            lowLevel.HAL_SetPWMPeriodScale(pwmPortHandle, squelchMask);
        }

        public static void SetPosition(int pwmPortHandle, double position)
        {
            lowLevel.HAL_SetPWMPosition(pwmPortHandle, position);
        }

        public static void SetRaw(int pwmPortHandle, int value)
        {
            lowLevel.HAL_SetPWMRaw(pwmPortHandle, value);
        }

        public static void SetSpeed(int pwmPortHandle, double speed)
        {
            lowLevel.HAL_SetPWMSpeed(pwmPortHandle, speed);
        }

    }
}
