using System;
using System.Runtime.CompilerServices;
using static HAL_Base.HAL;

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
        internal static int FPGAVersion
        {
            get
            {
                int status = 0;
                int value = GetFPGAVersion(ref status);
                return value;
            }
        }

        /// <summary>
        /// Return the FPGA Revision number. The format of the revision is 3 numbers. </summary>
        /// <remarks>The 12 most significant bits are the Major Revision. the next 8 bits are
        /// <para /> the Minor Revision. The 12 least significant bits are the Build Number.
        /// </remarks>
        internal static long FPGARevision
        {
            get
            {
                int status = 0;
                uint value = GetFPGARevision(ref status);
                return value;
            }
        }

        /// <summary>
        /// Read the microsecond timer from the FPGA
        /// </summary>
        public static long FPGATime
        {
            get
            {
                int status = 0;
                long value = GetFPGATime(ref status);
                return value;
            }
        }

        /// <summary>
        /// Get the state of the "USER" button on the RoboRIO
        /// </summary>
        /// <returns>True if the button is currently pressed down</returns>
        public static bool UserButton
        {
            get
            {
                int status = 0;
                bool value = GetFPGAButton(ref status);
                //CheckStatus(status); //Not calling check status right now because these functions are returning errors. 
                return value;
            }
        }

        /// <summary>
        /// //TODO:Implement
        /// </summary>
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
                throw new SystemException($" Code : {status}. {GetErrorMessage(status)}");
            }
            else if (status > 0)
            {
                DriverStation.ReportError(GetErrorMessage(status), true);
            }
        }
    }
}
