using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace HAL_Base
{
    public partial class HAL
    {
        /// <summary>
        /// This contains all of the HAL Constants
        /// </summary>
        public class Constants
        {
            //public const uint dio_kNumSystems;//Find Value
            public const uint solenoid_kNumDO7_0Elements = 8;
            //public const uint interrupt_kNumSystems ;//Find value
            public const uint kSystemClockTicksPerMicrosecond = 40;
        }


        //These strings are the location of the HAL DLLs used for reflection.
        //Since these are provided by the NuGet package, they will usually be
        //located in the executable directory
        private const string HALSim = "HAL-Simulation.dll";
        private const string HALRIO = "/home/lvuser/mono/HAL-RoboRIO.dll";

        //This contains the HAL Assembly, if we need to reference it for values later.
        internal static Assembly HALAssembly;

        //We use this so we can call initialize multiple times, without it crashing with itself
        private static bool s_initialized = false;
        //Makes it so if we call initialize from different threads, its safe.
        private static object s_lockObject = new object();

        public enum HALTypes
        {
            RoboRIO,
            Simulation,
            Other,
            None,
        }

        public static HALTypes HALType = HALTypes.None;

        //These are used to get us a copy of the dictionary from the simulator.
        //This is so we can debug it, because we cannot look at private members
        //of a reflected assembly.
        public delegate void GetData(out Dictionary<dynamic, dynamic> a, out Dictionary<dynamic, dynamic> b);

        public static Dictionary<dynamic, dynamic> halData;
        public static Dictionary<dynamic, dynamic> halInData;



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

                //Check to see if we are on a RoboRIO. We do this by probing for a file we know is located
                //on the RIO.
                HALType = HALTypes.Simulation;
                if (File.Exists("/usr/local/frc/bin/frcRunRobot.sh"))
                {
                    HALType = HALTypes.RoboRIO;
                }

                string asm = "";
                switch (HALType)
                {
                    case (HALTypes.Simulation):
                        asm = HALSim;
                        break;
                    case HALTypes.None:
                    case HALTypes.RoboRIO:
                    case HALTypes.Other:
                    default:
                        asm = HALRIO;
                        break;
                }

                HALAssembly = Assembly.LoadFrom(asm);

                //Setup all of our delegates
                //This allows us to dynamically switch between simulator, RoboRIO
                //and potentially others later.
                //Using delegates speeds the method calls up directly over
                //invoking the method using reflection, by about 20x.
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
                if (HALType == HALTypes.Simulation)
                {
                    string className = "SimData";
                    var types = HAL.HALAssembly.GetTypes();
                    var q = from t in types where t.IsClass && t.Name == className select t;
                    Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);

                    GetData data = (GetData)Delegate.CreateDelegate(typeof(GetData), type.GetMethod("GetData"));

                    data(out halData, out halInData);

                    FindSimulators();

                    //

                }
                s_initialized = true;
            }
        }

        private static List<string> simulatorFiles = null;
        private static List<Simulator> simulators = new List<Simulator>(); 

        private static string[] ignoreList = { "WPILib.dll", "HAL-Base.dll", "HAL-RoboRIO.dll", "WPILib.Extras.dll", "HAL-Simulation.dll" };

        private struct Simulator
        {
            public string AssemblyName;
            public Type SimType;
            public string SimName;
            public string SearchName;
        }


        private static bool FindSimulators()
        {
            simulatorFiles =
                (Directory.GetFiles(Directory.GetCurrentDirectory()).Where(x => x.Contains(".dll"))).ToList();
            foreach (var s in ignoreList.Where(s => simulatorFiles.Contains(s)))
            {
                simulatorFiles.Remove(s);
            }

            if(simulatorFiles.Count == 0)
            {
                return false;
            }

            bool found = false;
            //Load each file still available.
            foreach (var asm in simulatorFiles.Where(File.Exists).Select(Assembly.LoadFile))
            {
                string assembly = asm.GetName().Name;
                var classes = (from t in asm.GetTypes() where typeof (ISimulator).IsAssignableFrom(t) select t).ToList();
                foreach (var c in classes)
                {
                    using (var s = (ISimulator) Activator.CreateInstance(assembly, c.FullName).Unwrap())
                    {
                        string sName = s.AddArguments();
                        found = true;
                        var sim = new Simulator()
                        {
                            AssemblyName = assembly,
                            SimType = c,
                            SimName = c.FullName,
                            SearchName = sName
                        };
                        simulators.Add(sim);
                    }
                }
            }
            return found;
        }


        public static string GetJoystickName(byte joystickNum)
        {
            return Marshal.PtrToStringAnsi(HALGetJoystickName(joystickNum));
        }

        /// <summary>
        /// Returns the string for the error message for the give code.
        /// </summary>
        /// <param name="code">The Error Code</param>
        /// <returns>The Error String</returns>
        public static string GetErrorMessage(int code)
        {
            return Marshal.PtrToStringAnsi(GetHALErrorMessage(code));
        }

        /// <summary>
        /// Writes Error Data to the driver station.
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="waitMs"></param>
        /// <returns></returns>
        public static int SetErrorData(string errors, int waitMs)
        {
            return HALSetErrorData(errors, errors.Length, waitMs);
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

        /// <summary>
        /// Gets the HAL Control Word
        /// </summary>
        /// <returns></returns>
        public static HALControlWord GetControlWord()
        {
            HALControlWord temp = new HALControlWord();
            HALGetControlWord(ref temp);
            return temp;
        }
    }
}
