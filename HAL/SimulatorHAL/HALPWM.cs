using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALPWM
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALPWM.HAL_InitializePWMPort = HAL_InitializePWMPort;
            Base.HALPWM.HAL_FreePWMPort = HAL_FreePWMPort;
            Base.HALPWM.HAL_CheckPWMChannel = HAL_CheckPWMChannel;
            Base.HALPWM.HAL_SetPWMConfig = HAL_SetPWMConfig;
            Base.HALPWM.HAL_SetPWMConfigRaw = HAL_SetPWMConfigRaw;
            Base.HALPWM.HAL_GetPWMConfigRaw = HAL_GetPWMConfigRaw;
            Base.HALPWM.HAL_SetPWMEliminateDeadband = HAL_SetPWMEliminateDeadband;
            Base.HALPWM.HAL_GetPWMEliminateDeadband = HAL_GetPWMEliminateDeadband;
            Base.HALPWM.HAL_SetPWMRaw = HAL_SetPWMRaw;
            Base.HALPWM.HAL_SetPWMSpeed = HAL_SetPWMSpeed;
            Base.HALPWM.HAL_SetPWMPosition = HAL_SetPWMPosition;
            Base.HALPWM.HAL_SetPWMDisabled = HAL_SetPWMDisabled;
            Base.HALPWM.HAL_GetPWMRaw = HAL_GetPWMRaw;
            Base.HALPWM.HAL_GetPWMSpeed = HAL_GetPWMSpeed;
            Base.HALPWM.HAL_GetPWMPosition = HAL_GetPWMPosition;
            Base.HALPWM.HAL_LatchPWMZero = HAL_LatchPWMZero;
            Base.HALPWM.HAL_SetPWMPeriodScale = HAL_SetPWMPeriodScale;
            Base.HALPWM.HAL_GetLoopTiming = HAL_GetLoopTiming;
        }

        public static int HAL_InitializePWMPort(int port_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_FreePWMPort(int pwm_port_handle, ref int status)
        {
        }

        public static bool HAL_CheckPWMChannel(int pin)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetPWMConfig(int pwm_port_handle, double maxPwm, double deadbandMaxPwm, double centerPwm, double deadbandMinPwm, double minPwm, ref int status)
        {
        }

        public static void HAL_SetPWMConfigRaw(int pwm_port_handle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm, ref int status)
        {
        }

        public static void HAL_GetPWMConfigRaw(int pwm_port_handle, ref int maxPwm, ref int deadbandMaxPwm, ref int centerPwm, ref int deadbandMinPwm, ref int minPwm, ref int status)
        {
        }

        public static void HAL_SetPWMEliminateDeadband(int pwm_port_handle, bool eliminateDeadband, ref int status)
        {
        }

        public static bool HAL_GetPWMEliminateDeadband(int pwm_port_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetPWMRaw(int pwm_port_handle, int value, ref int status)
        {
        }

        public static void HAL_SetPWMSpeed(int pwm_port_handle, double speed, ref int status)
        {
        }

        public static void HAL_SetPWMPosition(int pwm_port_handle, double position, ref int status)
        {
        }

        public static void HAL_SetPWMDisabled(int pwm_port_handle, ref int status)
        {
        }

        public static int HAL_GetPWMRaw(int pwm_port_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetPWMSpeed(int pwm_port_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetPWMPosition(int pwm_port_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_LatchPWMZero(int pwm_port_handle, ref int status)
        {
        }

        public static void HAL_SetPWMPeriodScale(int pwm_port_handle, int squelchMask, ref int status)
        {
        }

        public static int HAL_GetLoopTiming(ref int status)
        {
            throw new NotImplementedException();
        }
    }
}

