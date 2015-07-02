using static  HAL_Base.HALPower;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// This Class is used to get the voltages and currents from the power rails
    /// on the RoboRIO. See <see cref="PowerDistributionPanel"/> for PDP rails.
    /// </summary>
    public static class ControllerPower
    {
        //TODO: Make these not properties.
        public static double InputVoltage
        {
            get
            {
                int status = 0;
                double retVal = GetVinVoltage(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static double InputCurrrent
        {
            get
            {
                int status = 0;
                double retVal = GetVinCurrent(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static double Voltage3V3
        {
            get
            {
                int status = 0;
                double retVal = GetUserVoltage3V3(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static double Current3V3
        {
            get
            {
                int status = 0;
                double retVal = GetUserCurrent3V3(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static bool Enabled3V3
        {
            get
            {
                int status = 0;
                bool retVal = GetUserActive3V3(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static int FaultCount3V3
        {
            get
            {
                int status = 0;
                int retVal = GetUserCurrentFaults3V3(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static double Voltage5V
        {
            get
            {
                int status = 0;
                double retVal = GetUserVoltage5V(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static double Current5V
        {
            get
            {
                int status = 0;
                double retVal = GetUserCurrent5V(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static bool Enabled5V
        {
            get
            {
                int status = 0;
                bool retVal = GetUserActive5V(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static int FaultCount5V
        {
            get
            {
                int status = 0;
                int retVal = GetUserCurrentFaults5V(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static double Voltage6V
        {
            get
            {
                int status = 0;
                double retVal = GetUserVoltage6V(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static double Current6V
        {
            get
            {
                int status = 0;
                double retVal = GetUserCurrent6V(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static bool Enabled6V
        {
            get
            {
                int status = 0;
                bool retVal = GetUserActive6V(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        public static int FaultCount6V
        {
            get
            {
                int status = 0;
                int retVal = GetUserCurrentFaults6V(ref status);
                CheckStatus(status);
                return retVal;
            }
        }
    }
}
