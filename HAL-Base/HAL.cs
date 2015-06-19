using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        public const string HALSim = "HAL-Simulation.dll";
        public const string HALRIO = "/home/lvuser/mono/HAL-RoboRIO.dll";


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

        private static bool m_initialized = false;
        private static object m_lockObject = new object();

        /// <summary>
        /// HAL Initialization. Must be called before any other HAL functions.
        /// </summary>
        /// <param name="mode">Initialization Mode</param>
        public static void Initialize(int mode = 0)
        {
            //Lock this function, so that if accidentally called from multiple threads it doesnt
            //get m_initialized at the wrong value.
            lock (m_lockObject)
            {
                // We don't want to initialize more then once.
                if (m_initialized) return;

                HALAssembly = Assembly.LoadFrom(IsSimulation ? HALSim : HALRIO);

                SetupDelegates();
                HALAccelerometer.SetupDelegates();
                HALAnalog.SetupDelegates();
                HALCAN.SetupDelegates();
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

                //If we are simulator, grab a local copy of the dictionary so we can debug its values.
                if (IsSimulation)
                {
                    string className = "SimData";
                    var types = HAL.HALAssembly.GetTypes();
                    var q = from t in types where t.IsClass && t.Name == className select t;
                    Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);

                    GetData data = (GetData)Delegate.CreateDelegate(typeof(GetData), type.GetMethod("GetData"));

                    data(out halData, out halInData);

                }
                m_initialized = true;
            }
        }

        public delegate void GetData(out Dictionary<dynamic, dynamic> a, out Dictionary<dynamic, dynamic> b);

        public static Dictionary<dynamic, dynamic> halData;
        public static Dictionary<dynamic, dynamic> halInData;

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
