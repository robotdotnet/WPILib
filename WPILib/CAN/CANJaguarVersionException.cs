using System;

namespace WPILib.CAN
{
    /// <summary>
    /// Exception indicating that the CAN Jaguar does not have the correct version
    /// </summary>
    public class CANJaguarVersionException : Exception
    {
        /// <summary>
        /// The Minimum FIRST Legal version of the firmware
        /// </summary>
        public const int MinLegalFIRSTFirmwareVersion = 101;
        /// <summary>
        /// The Minimum RDK Firmware Version
        /// </summary>
        public const int MinRdkFirmwareVersion = 3330;

        /// <summary>
        /// Initializes a new instance of the <see cref="CANJaguarVersionException"/> class
        /// </summary>
        /// <param name="deviceNumber">The Device Number</param>
        /// <param name="fwVersion">The FW Version</param>
        public CANJaguarVersionException(int deviceNumber, int fwVersion) : 
            base(GetString(deviceNumber, fwVersion))
        {
            Console.WriteLine($"fwVersion[{deviceNumber}]: {fwVersion}");
        }

        private static string GetString(int deviceNumber, int fwVersion)
        {
            string msg;
            msg = fwVersion < MinRdkFirmwareVersion ? 
                $"Jaguar {deviceNumber} firmware is too old. It must be updated to at least version {MinLegalFIRSTFirmwareVersion} of the FIRST approved firmware!" 
                : $"Jaguar {deviceNumber} firmware is not FIRST approved. It must be updated to at least version {MinLegalFIRSTFirmwareVersion} of the FIRST approved firmware!";
            return msg;
        }
    }
}
