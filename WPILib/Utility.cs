using System.Runtime.CompilerServices;
using HAL.Base;
using WPILib.Exceptions;
using static HAL.Base.HAL;
using System.Text;
using System;
using HAL = HAL.Base.HAL;

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
            int value = HAL.Base.HAL.HAL_GetFPGAVersion(ref status);
            CheckStatus(status);
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
            long value = HAL.Base.HAL.HAL_GetFPGARevision(ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Read the microsecond timer from the FPGA
        /// </summary>
        public static ulong GetFPGATime()
        {
            int status = 0;
            ulong value = HAL.Base.HAL.HAL_GetFPGATime(ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Get the state of the "USER" button on the RoboRIO
        /// </summary>
        /// <returns>True if the button is currently pressed down</returns>
        public static bool GetUserButton()
        {
            int status = 0;
            bool value = HAL_GetFPGAButton(ref status);
            CheckStatus(status);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckStatus(int status, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (status < 0)
            {
                throw new UncleanStatusException(status, $" Code : {status}. {HAL_GetErrorMessage(status)} \n at {memberName} path:{filePath} line:{lineNumber}");
            }
            else if (status > 0)
            {
                //Pass the caller members along.
                DriverStation.ReportError(HAL_GetErrorMessage(status), true, status, memberName, filePath, lineNumber);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CheckStatusRange(int status, int min, int max, int requested,
            [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (status != 0)
            {
                if (status == HALErrors.NO_AVAILABLE_RESOURCES ||
                    status == HALErrors.RESOURCE_IS_ALLOCATED ||
                    status == HALErrors.RESOURCE_OUT_OF_RANGE)
                {
                    throw new AllocationException(status, $" Code : {status}. {HAL_GetErrorMessage(status)} \n Minimum: {min}, Maximum: {max}, Requested {requested} \n at {memberName} path:{filePath} line:{lineNumber}");
                }
                if (status == HALErrors.HAL_HANDLE_ERROR)
                {
                    throw new HalHandleException(
                        $" Code : {status}. {HAL_GetErrorMessage(status)} \n at {memberName} path:{filePath} line:{lineNumber}");
                }
                throw new UncleanStatusException(status, $" Code : {status}. {HAL_GetErrorMessage(status)} \n at {memberName} path:{filePath} line:{lineNumber}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CheckStatusForceThrow(int status, [CallerMemberName] string memberName = "", 
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            CheckStatusRange(status, 0, 0, 0, memberName, filePath, lineNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool CheckCTRStatus(CTR_Code status, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (status != CTR_Code.CTR_OKAY)
            {
                //Pass the caller memebers along
                DriverStation.ReportError(HAL_GetErrorMessage((int)status), true, (int) status, memberName, filePath, lineNumber);
            }
            return status == CTR_Code.CTR_OKAY;
        }
    }
}
