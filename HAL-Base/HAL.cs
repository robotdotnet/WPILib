using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

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

                string asm;
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

                try
                {
                    //Force load from current directory first. Note will fail if directory has spaces. Need to find a way to work around this
                    string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
                    assemblyFile = Path.GetDirectoryName(assemblyFile);
                    assemblyFile += Path.DirectorySeparatorChar + asm;

                    HALAssembly = Assembly.LoadFrom(assemblyFile);
                }
                catch (FileNotFoundException)
                {
                    // If that fails load it directly
                    try
                    {
                        //Load our HAL assembly
                        HALAssembly = Assembly.LoadFrom(asm);
                    }
                    catch (Exception e)
                    {
                        //If our loading ever causes an exception, print it, print the stack trace, and kill the program.
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                        Environment.Exit(1);
                    }
                    
                }


                //Find our initialize function
                string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
                var types = HALAssembly.GetTypes();
                var q = from t in types where t.IsClass && t.Name == className select t;
                Type type = HALAssembly.GetType(q.ToList()[0].FullName);
                //Initialize our delegates. The InitializeImpl code need to assign all delegates.
                try
                {
                    type.GetMethod("InitializeImpl").Invoke(null, null);
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

    }
}
