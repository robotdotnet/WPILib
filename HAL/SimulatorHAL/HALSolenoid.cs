using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALSolenoid
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALSolenoid.HAL_InitializeSolenoidPort = HAL_InitializeSolenoidPort;
            Base.HALSolenoid.HAL_FreeSolenoidPort = HAL_FreeSolenoidPort;
            Base.HALSolenoid.HAL_CheckSolenoidModule = HAL_CheckSolenoidModule;
            Base.HALSolenoid.HAL_CheckSolenoidPin = HAL_CheckSolenoidPin;
            Base.HALSolenoid.HAL_GetSolenoid = HAL_GetSolenoid;
            Base.HALSolenoid.HAL_GetAllSolenoids = HAL_GetAllSolenoids;
            Base.HALSolenoid.HAL_SetSolenoid = HAL_SetSolenoid;
            Base.HALSolenoid.HAL_GetPCMSolenoidBlackList = HAL_GetPCMSolenoidBlackList;
            Base.HALSolenoid.HAL_GetPCMSolenoidVoltageStickyFault = HAL_GetPCMSolenoidVoltageStickyFault;
            Base.HALSolenoid.HAL_GetPCMSolenoidVoltageFault = HAL_GetPCMSolenoidVoltageFault;
            Base.HALSolenoid.HAL_ClearAllPCMStickyFaults = HAL_ClearAllPCMStickyFaults;
        }

        public static int HAL_InitializeSolenoidPort(int port_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_FreeSolenoidPort(int solenoid_port_handle)
        {
        }

        public static bool HAL_CheckSolenoidModule(int module)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_CheckSolenoidPin(int pin)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetSolenoid(int solenoid_port_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetAllSolenoids(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetSolenoid(int solenoid_port_handle, bool value, ref int status)
        {
        }

        public static int HAL_GetPCMSolenoidBlackList(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetPCMSolenoidVoltageStickyFault(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetPCMSolenoidVoltageFault(int module, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_ClearAllPCMStickyFaults(int module, ref int status)
        {
        }
    }
}

