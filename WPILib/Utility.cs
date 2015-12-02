using System.Runtime.CompilerServices;
using WPILib.Exceptions;
using static HAL.Base.HAL;

namespace WPILib
{
    /// <summary>
    /// Contains global utility functions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Return the FPGA Version number. For now, expect this to be 2015.
        /// </summary>
        public static int GetFPGAVersion()
        {
            int status = 0;
            int value = HAL.Base.HAL.GetFPGAVersion(ref status);
            return value;
        }

        /// <summary>
        /// Return the FPGA Revision number. The format of the revision is 3 numbers. </summary>
        /// <remarks>The 12 most significant bits are the Major Revision. the next 8 bits are
        /// <para /> the Minor Revision. The 12 least significant bits are the Build Number.
        /// </remarks>
        public static long GetFPGARevision()
        {
            int status = 0;
            uint value = HAL.Base.HAL.GetFPGARevision(ref status);
            return value;
        }

        /// <summary>
        /// Read the microsecond timer from the FPGA
        /// </summary>
        public static uint GetFPGATime()
        {
            int status = 0;
            uint value = HAL.Base.HAL.GetFPGATime(ref status);
            return value;
        }

        /// <summary>
        /// Get the state of the "USER" button on the RoboRIO
        /// </summary>
        /// <returns>True if the button is currently pressed down</returns>
        public static bool GetUserButton()
        {
            int status = 0;
            bool value = GetFPGAButton(ref status);
            //CheckStatus(status); //Not calling check status right now because these functions are returning errors. 
            return value;
        }

        /// <summary>
        /// This is used to check the status's returned from the HAL functions.
        /// </summary>
        /// <remarks>This should not be needed, unless you are calling a HAL function yourself.
        /// However we would recommend against this unless absolutely necessary.</remarks>
        /// <param name="status"></param>
        /// <param name="memberName"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        public static void CheckStatus(int status, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            //TODO: Use Caller attributes
            if (status < 0)
            {
                throw new UncleanStatusException(status, $" Code : {status}. {GetHALErrorMessage(status)}");
            }
            else if (status > 0)
            {
                DriverStation.ReportError(GetHALErrorMessage(status), true);
            }
        }
    }
}
