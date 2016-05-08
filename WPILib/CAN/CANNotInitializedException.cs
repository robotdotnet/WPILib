using System;

namespace WPILib.CAN
{
    /// <summary>
    /// Exception indicating that the CAN driver layer has not been initialized.
    /// </summary>
    /// <remarks>This happens when an entry-point is called before a CAN driver plugin
    /// has been installed.</remarks>
    public class CANNotInitializedException : Exception
    {
    }
}