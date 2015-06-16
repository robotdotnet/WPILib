using System;

namespace WPILib.CAN
{
    /// <summary>
    /// Exception indicating that the Jaguar CAN Driver layer refused to send a
    /// restricted message ID to the CAN bus.
    /// </summary>
    public class CANMessageNotAllowedException : SystemException
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="CANMessageNotAllowedException"/> class
        /// </summary>
        /// <param name="msg"></param>
        public CANMessageNotAllowedException(string msg)
            : base(msg)
        {
        }
    }
}