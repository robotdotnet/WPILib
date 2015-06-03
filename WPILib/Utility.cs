using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;

namespace WPILib
{
    /// <summary>
    /// Contains global utility functions
    /// </summary>
    public class Utility
    {
        private Utility()
        {

        }

        /// <summary>
        /// Return the FPGA Version number. For now, expect this to be 2015.
        /// </summary>
        /// <returns>FPGA Version Number</returns>
        internal int GetFPGAVersion()
        {
            int status = 0;
            int value = HAL.GetFPGAVersion(ref status);
            return value;
        }

        /// <summary>
        /// Return the FPGA Revision number. The format of the revision is 3 numbers.
        /// <para /> The 12 most significant bits are the Major Revision. the next 8 bits are
        /// <para /> the Minor Revision. The 12 least significant bits are the Build Number.
        /// </summary>
        /// <returns>FPGA Revision Number</returns>
        internal long GetFPGARevision()
        {
            int status = 0;
            int value = (int)HAL.GetFPGARevision(ref status);
            return (long)value;
        }

        /// <summary>
        /// Read the microsecond timer from the FPGA
        /// </summary>
        /// <returns>The current time in microseconds according to the FPGA.</returns>
        public static long GetFPGATime()
        {
            int status = 0;
            long value = HAL.GetFPGATime(ref status);
            return value;
        }

        /// <summary>
        /// Get the state of the "USER" button on the RoboRIO
        /// </summary>
        /// <returns>True if the button is currently pressed down</returns>
        public static bool GetUserButton()
        {
            int status = 0;
            bool value = HAL.GetFPGAButton(ref status);
            //CheckStatus(status); //Not calling check status right now because these functions are returning errors. 
            return value;
        }

        internal static void CheckStatus(int status)
        {
            if (status < 0)
            {
                string message = HAL.GetErrorMessage(status);
                throw new SystemException(" Code: " + status + ". " + message);
            }
            else if (status > 0)
            {
                string message = HAL.GetErrorMessage(status);
                DriverStation.ReportError(message, true);
            }
        }
    }
}
