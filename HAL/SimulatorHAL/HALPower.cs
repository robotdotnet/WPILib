using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALPower
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALPower.HAL_GetVinVoltage = HAL_GetVinVoltage;
            Base.HALPower.HAL_GetVinCurrent = HAL_GetVinCurrent;
            Base.HALPower.HAL_GetUserVoltage6V = HAL_GetUserVoltage6V;
            Base.HALPower.HAL_GetUserCurrent6V = HAL_GetUserCurrent6V;
            Base.HALPower.HAL_GetUserActive6V = HAL_GetUserActive6V;
            Base.HALPower.HAL_GetUserCurrentFaults6V = HAL_GetUserCurrentFaults6V;
            Base.HALPower.HAL_GetUserVoltage5V = HAL_GetUserVoltage5V;
            Base.HALPower.HAL_GetUserCurrent5V = HAL_GetUserCurrent5V;
            Base.HALPower.HAL_GetUserActive5V = HAL_GetUserActive5V;
            Base.HALPower.HAL_GetUserCurrentFaults5V = HAL_GetUserCurrentFaults5V;
            Base.HALPower.HAL_GetUserVoltage3V3 = HAL_GetUserVoltage3V3;
            Base.HALPower.HAL_GetUserCurrent3V3 = HAL_GetUserCurrent3V3;
            Base.HALPower.HAL_GetUserActive3V3 = HAL_GetUserActive3V3;
            Base.HALPower.HAL_GetUserCurrentFaults3V3 = HAL_GetUserCurrentFaults3V3;
        }

        public static double HAL_GetVinVoltage(ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetVinCurrent(ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetUserVoltage6V(ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetUserCurrent6V(ref int status)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetUserActive6V(ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetUserCurrentFaults6V(ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetUserVoltage5V(ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetUserCurrent5V(ref int status)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetUserActive5V(ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetUserCurrentFaults5V(ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetUserVoltage3V3(ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetUserCurrent3V3(ref int status)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetUserActive3V3(ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetUserCurrentFaults3V3(ref int status)
        {
            throw new NotImplementedException();
        }
    }
}

