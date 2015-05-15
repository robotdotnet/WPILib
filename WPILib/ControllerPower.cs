using HAL_FRC;

namespace WPILib
{
    public class ControllerPower
    {
        public static double GetInputVoltage()
        {
            int status = 0;
            double retVal = HALPower.getVinVoltage(ref status);
            return retVal;
        }

        public static double GetInputCurrrent()
        {
            int status = 0;
            double retVal = HALPower.getVinCurrent(ref status);
            return retVal;
        }

        public static double GetVoltage3V3()
        {
            int status = 0;
            double retVal = HALPower.getUserVoltage3V3(ref status);
            return retVal;
        }

        public static double GetCurrent3V3()
        {
            int status = 0;
            double retVal = HALPower.getUserCurrent3V3(ref status);
            return retVal;
        }

        public static bool GetEnabled3V3()
        {
            int status = 0;
            bool retVal = HALPower.getUserActive3V3(ref status);
            return retVal;
        }

        public static int GetFaultCount3V3()
        {
            int status = 0;
            int retVal = HALPower.getUserCurrentFaults3V3(ref status);
            return retVal;
        }

        public static double GetVoltage5V()
        {
            int status = 0;
            double retVal = HALPower.getUserVoltage5V(ref status);
            return retVal;
        }

        public static double GetCurrent5V()
        {
            int status = 0;
            double retVal = HALPower.getUserCurrent5V(ref status);
            return retVal;
        }

        public static bool GetEnabled5V()
        {
            int status = 0;
            bool retVal = HALPower.getUserActive5V(ref status);
            return retVal;
        }

        public static int GetFaultCount5V()
        {
            int status = 0;
            int retVal = HALPower.getUserCurrentFaults5V(ref status);
            return retVal;
        }

        public static double GetVoltage6V()
        {
            int status = 0;
            double retVal = HALPower.getUserVoltage6V(ref status);
            return retVal;
        }

        public static double GetCurrent6V()
        {
            int status = 0;
            double retVal = HALPower.getUserCurrent6V(ref status);
            return retVal;
        }

        public static bool GetEnabled6V()
        {
            int status = 0;
            bool retVal = HALPower.getUserActive6V(ref status);
            return retVal;
        }

        public static int GetFaultCount6V()
        {
            int status = 0;
            int retVal = HALPower.getUserCurrentFaults6V(ref status);
            return retVal;
        }

    }
}
