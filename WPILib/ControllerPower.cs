using static HAL.Base.HALPower;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// This Class is used to get the voltages and currents from the power rails
    /// on the RoboRIO. See <see cref="PowerDistributionPanel"/> for PDP rails.
    /// </summary>
    public static class ControllerPower
    {
        /// <summary>
        /// Gets the input voltage of the RoboRIO.
        /// </summary>
        /// <returns>The input voltage in Volts.</returns>
        public static double GetInputVoltage()
        {
            int status = 0;
            double retVal = HAL_GetVinVoltage(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the input current of the RoboRIO.
        /// </summary>
        /// <returns>The input current in Amps.</returns>
        public static double GetInputCurrrent()
        {
            int status = 0;
            double retVal = HAL_GetVinCurrent(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the voltage of the 3.3V rail.
        /// </summary>
        /// <returns>The 3.3v rail voltage in Volts.</returns>
        public static double GetVoltage3V3()
        {
            int status = 0;
            double retVal = HAL_GetUserVoltage3V3(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the current of the 3.3V rail.
        /// </summary>
        /// <returns>The 3.3v rail current in Amps.</returns>
        public static double GetCurrent3V3()
        {
            int status = 0;
            double retVal = HAL_GetUserCurrent3V3(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets if the 3.3v rail is enabled.
        /// </summary>
        /// <returns>True if the 3.3v rail is enabled.</returns>
        public static bool GetEnabled3V3()
        {
            int status = 0;
            bool retVal = HAL_GetUserActive3V3(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the count of current faults on the 3.3V rail since the controller has booted.
        /// </summary>
        /// <returns>The number of faults.</returns>
        public static int GetFaultCount3V3()
        {
            int status = 0;
            int retVal = HAL_GetUserCurrentFaults3V3(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the voltage of the 5V rail.
        /// </summary>
        /// <returns>The 5v rail voltage in Volts.</returns>
        public static double GetVoltage5V()
        {
            int status = 0;
            double retVal = HAL_GetUserVoltage5V(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the current of the 5V rail.
        /// </summary>
        /// <returns>The 5v rail current in Amps.</returns>
        public static double GetCurrent5V()
        {
            int status = 0;
            double retVal = HAL_GetUserCurrent5V(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets if the 5v rail is enabled.
        /// </summary>
        /// <returns>True if the 5v rail is enabled.</returns>
        public static bool GetEnabled5V()
        {
            int status = 0;
            bool retVal = HAL_GetUserActive5V(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the count of current faults on the 5V rail since the controller has booted.
        /// </summary>
        /// <returns>The number of faults.</returns>
        public static int GetFaultCount5V()
        {
            int status = 0;
            int retVal = HAL_GetUserCurrentFaults5V(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the voltage of the 6V rail.
        /// </summary>
        /// <returns>The 6v rail voltage in Volts.</returns>
        public static double GetVoltage6V()
        {
            int status = 0;
            double retVal = HAL_GetUserVoltage6V(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the current of the 6V rail.
        /// </summary>
        /// <returns>The 6v rail current in Amps.</returns>
        public static double GetCurrent6V()
        {
            int status = 0;
            double retVal = HAL_GetUserCurrent6V(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets if the 5v rail is enabled.
        /// </summary>
        /// <returns>True if the 5v rail is enabled.</returns>
        public static bool GetEnabled6V()
        {
            int status = 0;
            bool retVal = HAL_GetUserActive6V(ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets the count of current faults on the 6V rail since the controller has booted.
        /// </summary>
        /// <returns>The number of faults.</returns>
        public static int GetFaultCount6V()
        {
            int status = 0;
            int retVal = HAL_GetUserCurrentFaults6V(ref status);
            CheckStatus(status);
            return retVal;
        }
    }
}
