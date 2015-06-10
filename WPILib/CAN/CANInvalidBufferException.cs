using System;

namespace WPILib.CAN
{
    /// <summary>
    /// Exception indicating that a CAN driver library entry-point
    /// was passed an invalid buffer. </summary>
    /// <remarks>
    /// Typically, this is due to a buffer being too small to include the 
    /// needed safety token.</remarks>
    public class CANInvalidBufferException : SystemException
    {
    }
}