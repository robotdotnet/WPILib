using System;
using System.IO;

namespace HAL.Base
{
    public partial class HAL
    {
        /// <summary>
        /// This contains all of the HAL Constants
        /// </summary>
        public class Constants
        {
            //public const uint dio_kNumSystems;//Find Value
            //public const uint solenoid_kNumDO7_0Elements = 8;
            //public const uint interrupt_kNumSystems ;//Find value
            public const uint SystemClockTicksPerMicrosecond = 40;
        }

        ///This contains constants used for the DriverStation.
        public class DriverStationConstants
        {
            ///The number of joystick ports
            public const int JoystickPorts = 6;
            ///The number of joystick axes
            public const int MaxJoystickAxes = 12;
            ///The number of joystick POVs
            public const int MaxJoystickPOVs = 12;
        }

        //We use this so we can call initialize multiple times, without it crashing with itself
        private static bool s_initialized = false;
        //Makes it so if we call initialize from different threads, its safe.
        private static readonly object s_lockObject = new object();

        public enum HALTypes
        {
            RoboRIO,
            Simulation,
            Other,
            None,
        }

        public static HALTypes HALType = HALTypes.None;


        /// <summary>
        /// HAL Initialization. Must be called before any other HAL functions.
        /// </summary>
        /// <param name="mode">Initialization Mode</param>
        public static void Initialize(int mode = 0)
        {
            //Lock this function, so that if accidentally called from multiple threads it doesn't
            //get m_initialized at the wrong value.
            lock (s_lockObject)
            {
                // We don't want to initialize more then once.
                if (s_initialized) return;

                s_initialized = true;

                //Check to see if we are on a RoboRIO. We do this by probing for a file we know is located
                //on the RIO.
                HALType = HALTypes.Simulation;
                if (File.Exists("/usr/local/frc/bin/frcRunRobot.sh"))
                {
                    HALType = HALTypes.RoboRIO;
                }

                try
                {
                    switch (HALType)
                    {
                        case HALTypes.RoboRIO:
                            AthenaHAL.HAL.InitializeImpl();
                            break;
                        case HALTypes.Simulation:
                        case HALTypes.Other:
                        case HALTypes.None:
                            SimulatorHAL.HAL.InitializeImpl();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception e)
                {
                    //If our loading ever causes an exception, print it, print the stack trace, and kill the program.
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Environment.Exit(1);
                }

                var rv = HALInitialize(mode);
                if (rv != 1)
                {
                    throw new Exception($"HAL Initialize Failed with return code {rv}");
                }

                //This next piece of code is to make sure that the Notifier and Interrupt libraries work.
                //Note that this leaks 6 bytes, but there is no way in the HAL to delete this structure,
                //And it is leaked on normal use as well. So this is a workaround, and we hope to fix it
                //When the HAL fixes it.
                int status = 0;
                HALDigital.InitializeDigitalPort(GetPort(0), ref status);
            }
        }


        public static uint Report(ResourceType resource, Instances instanceNumber, byte context = 0, string feature = null)
        {
            return HALReport((byte) resource, (byte) instanceNumber, context, feature);
        }

        public static uint Report(ResourceType resource, byte instanceNumber, byte context = 0, string feature = null)
        {
            return HALReport((byte) resource, instanceNumber, context, feature);
        }

        public static uint Report(byte resource, Instances instanceNumber, byte context = 0, string feature = null)
        {
            return HALReport(resource, (byte) instanceNumber, context, feature);
        }

        public static uint Report(byte resource, byte instanceNumber, byte context = 0, string feature = null)
        {
            return HALReport(resource, instanceNumber, context, feature);
        }
    }
}
