using HAL_Base;

namespace WPILib
{
    public class ControllerPower
    {
        public static double GetInputVoltage()
        {
            int status = 0;
            double retVal = HALPower.GetVinVoltage(ref status);
            return retVal;
        }

        public static double GetInputCurrrent()
        {
            int status = 0;
            double retVal = HALPower.GetVinCurrent(ref status);
            return retVal;
        }

        public static double GetVoltage3V3()
        {
            int status = 0;
            double retVal = HALPower.GetUserVoltage3V3(ref status);
            return retVal;
        }

        public static double GetCurrent3V3()
        {
            int status = 0;
            double retVal = HALPower.GetUserCurrent3V3(ref status);
            return retVal;
        }

        public static bool GetEnabled3V3()
        {
            int status = 0;
            bool retVal = HALPower.GetUserActive3V3(ref status);
            return retVal;
        }

        public static int GetFaultCount3V3()
        {
            int status = 0;
            int retVal = HALPower.GetUserCurrentFaults3V3(ref status);
            return retVal;
        }

        public static double GetVoltage5V()
        {
            int status = 0;
            double retVal = HALPower.GetUserVoltage5V(ref status);
            return retVal;
        }

        public static double GetCurrent5V()
        {
            int status = 0;
            double retVal = HALPower.GetUserCurrent5V(ref status);
            return retVal;
        }

        public static bool GetEnabled5V()
        {
            int status = 0;
            bool retVal = HALPower.GetUserActive5V(ref status);
            return retVal;
        }

        public static int GetFaultCount5V()
        {
            int status = 0;
            int retVal = HALPower.GetUserCurrentFaults5V(ref status);
            return retVal;
        }

        public static double GetVoltage6V()
        {
            int status = 0;
            double retVal = HALPower.GetUserVoltage6V(ref status);
            return retVal;
        }

        public static double GetCurrent6V()
        {
            int status = 0;
            double retVal = HALPower.GetUserCurrent6V(ref status);
            return retVal;
        }

        public static bool GetEnabled6V()
        {
            int status = 0;
            bool retVal = HALPower.GetUserActive6V(ref status);
            return retVal;
        }

        public static int GetFaultCount6V()
        {
            int status = 0;
            int retVal = HALPower.GetUserCurrentFaults6V(ref status);
            return retVal;
        }

    }
}
