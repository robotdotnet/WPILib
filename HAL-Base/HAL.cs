using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace HAL_Base
{
    

    public partial class HAL
    {
        //public const uint dio_kNumSystems;//Find Value
        public const uint solenoid_kNumDO7_0Elements = 8;
        //public const uint interrupt_kNumSystems ;//Find value
        public const uint kSystemClockTicksPerMicrosecond = 40;


        internal static Assembly HALAssembly;

        /// <summary>
        /// Gets or Sets if the code is in simulation mode
        /// </summary>
        public static bool IsSimulation { get; set; } = false;

        public static string GetErrorMessage(int code)
        {
            return Marshal.PtrToStringAnsi(GetHALErrorMessage(code));
        }

        public static int SetErrorData(string errors, int waitMs)
        {
            return HALSetErrorData(errors, errors.Length, waitMs);
        }

        /// <summary>
        /// HAL Initalization. Must be called before any other HAL functions.
        /// </summary>
        /// <param name="mode">Initialization Mode</param>
        public static void Initialize(int mode = 0)
        {
            HALAssembly = Assembly.LoadFrom(IsSimulation ? "HAL-Simulation.dll" : "/home/lvuser/mono/HAL-RoboRIO.dll");

            SetupDelegates();
            HALAccelerometer.SetupDelegates();
            HALAnalog.SetupDelegates();
            HALCanTalonSRX.SetupDelegates();
            HALCompressor.SetupDelegates();
            HALDigital.SetupDelegates();
            HALInterrupts.SetupDelegates();
            HALNotifier.SetupDelegates();
            HALPDP.SetupDelegates();
            HALPower.SetupDelegates();
            HALSemaphore.SetupDelegates();
            HALSerialPort.SetupDelegates();
            HALSolenoid.SetupDelegates();
            HALUtilities.SetupDelegates();


            var rv = HALInitialize(mode);
            if (rv == 0)
            {
                throw new Exception("HAL Initialize Failed");
            }
        }

        public static uint Report(ResourceType resource, Instances instanceNumber, byte context = 0,
            string feature = null)
        {
            return HALReport((byte)resource, (byte)instanceNumber, context, feature);
        }

        public static uint Report(ResourceType resource, byte instanceNumber, byte context = 0,
            string feature = null)
        {
            return HALReport((byte)resource, instanceNumber, context, feature);
        }

        public static uint Report(byte resource, Instances instanceNumber, byte context = 0,
            string feature = null)
        {
            return HALReport(resource, (byte)instanceNumber, context, feature);
        }

        public static uint Report(byte resource, byte instanceNumber, byte context = 0,
            string feature = null)
        {
            return HALReport(resource, instanceNumber, context, feature);
        }
        
        public static HALControlWord GetControlWord()
        {
            HALControlWord temp = new HALControlWord();
            HALGetControlWord(ref temp);
            return temp;
        }
    }
}
