using static  HAL.HALPower;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// This Class is used to get the voltages and currents from the power rails
    /// on the RoboRIO. See <see cref="PowerDistributionPanel"/> for PDP rails.
    /// </summary>
    public static class ControllerPower
    {

        public static double GetInputVoltage()
        {
            int status = 0;
            float retVal = GetVinVoltage(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static double GetInputCurrrent()
        {
            int status = 0;
            double retVal = GetVinCurrent(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static double GetVoltage3V3()
        {
            int status = 0;
            double retVal = GetUserVoltage3V3(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static double GetCurrent3V3()
        {
            int status = 0;
            double retVal = GetUserCurrent3V3(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static bool GetEnabled3V3()
        {
            int status = 0;
            bool retVal = GetUserActive3V3(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static int GetFaultCount3V3()
        {
            int status = 0;
            int retVal = GetUserCurrentFaults3V3(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static double GetVoltage5V()
        {
            int status = 0;
            double retVal = GetUserVoltage5V(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static double GetCurrent5V()
        {
            int status = 0;
            double retVal = GetUserCurrent5V(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static bool GetEnabled5V()
        {
            int status = 0;
            bool retVal = GetUserActive5V(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static int GetFaultCount5V()
        {
            int status = 0;
            int retVal = GetUserCurrentFaults5V(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static double GetVoltage6V()
        {
            int status = 0;
            double retVal = GetUserVoltage6V(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static double GetCurrent6V()
        {
            int status = 0;
            double retVal = GetUserCurrent6V(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static bool GetEnabled6V()
        {
            int status = 0;
            bool retVal = GetUserActive6V(ref status);
            CheckStatus(status);
            return retVal;
        }

        public static int GetFaultCount6V()
        {
            int status = 0;
            int retVal = GetUserCurrentFaults6V(ref status);
            CheckStatus(status);
            return retVal;
        }
    }
}
