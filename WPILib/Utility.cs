using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;

namespace WPILib
{
    public class Utility
    {
        private Utility()
        {
            
        }

        private int GetFPGAVersion()
        {
            int status = 0;
            int value = HAL.GetFPGAVersion(ref status);
            return value;
        }

        private long GetFPGARevision()
        {
            int status = 0;
            int value = (int)HAL.GetFPGARevision(ref status);
            return (long) value;
        }

        public static long GetFPGATime()
        {
            int status = 0;
            long value = HAL.GetFPGATime(ref status);
            return value;
        }

        public static bool GetUserButton()
        {
            int status = 0;
            bool value = HAL.GetFPGAButton(ref status);
            return value;
        }

        public static void CheckStatus(int status)
        {
            if (status < 0)
            {
                string message = HAL.GetHALErrorMessage(status);
                throw new SystemException(" Code: " + status + ". " + message);
            }
            else if (status > 0)
            {
                string message = HAL.GetHALErrorMessage(status);
                DriverStation.ReportError(message, true);
            }
        }
    }
}
