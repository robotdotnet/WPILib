using System;

namespace WPILib.CAN
{
    /// <summary>
    /// Exception indicating that a can message is not available from Network
    /// </summary>
    /// <remarks>Communications.This usually just means we already have the most recent
    /// value cached locally.</remarks>
    public class CANMessageNotFoundException : SystemException
    {
    }
}
