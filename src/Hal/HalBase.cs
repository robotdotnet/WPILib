﻿using Hal.Natives;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
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
                throw new InvalidOperationException("TODO, Do Something About This Message");
            }
            ;
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IHALBase halBase;
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

        public static ulong GetFPGATimestamp()
        {
            return halBase.HAL_GetFPGATime();
        }

        public static bool HasMain()
        {
            return false;
        }

        public static void ExitMain()
        {

        }

        public static void RunMain()
        {

        }


        public static RuntimeType GetRuntimeType()
        {
            return halBase.HAL_GetRuntimeType();
        }

        public static bool GetUserButton()
        {
            return halBase.HAL_GetFPGAButton() != 0;
        }
    }
}