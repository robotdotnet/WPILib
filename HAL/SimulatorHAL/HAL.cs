using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using HAL.Base;
using HAL.Simulator;
using HAL.Simulator.Data;
using static HAL.Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    /// <summary>
    /// This attribute is added to any Sim functions that are called from base.
    /// It helps make sure that our delegates will work.
    /// </summary>
    internal class CalledSimFunction : Attribute { }

    /// <summary>
    /// This class is used by HAL-Base, and is used to emulate the HAL. 
    /// Please do not call functions in this class directly. 
    /// </summary>
    internal class HAL
    {
        //Time constants used for GetMatchTime.
        public const double AutonomousTime = 15.0;
        public const double TeleopTime = 135.0;

        private static bool s_libraryLoaded = false;
        private static IntPtr s_library;

        public static void InitializeImpl()
        {
            if (!s_libraryLoaded)
            {
                try
                {
                    /* //These can be ignored for now, since we dont have a native library.
                    OsType type = LoaderUtilities.GetOsType();
                    if (!LoaderUtilities.CheckOsValid(type))
                        throw new InvalidOperationException("OS Not Supported");

                    string loadedPath = LoaderUtilities.ExtractLibrary(type);
                    if (string.IsNullOrEmpty(loadedPath)) throw new FileNotFoundException("Stream not found");

                    library = LoaderUtilities.LoadLibrary(loadedPath, type);

                    if (library == IntPtr.Zero) throw new BadImageFormatException($"Library file {loadedPath} could not be loaded successfully.");
                    */
                    s_library = IntPtr.Zero;
                    ILibraryLoader loader = null;
                    // ReSharper disable ExpressionIsAlwaysNull
                    Initialize(s_library, loader);
                    HALAccelerometer.Initialize(s_library, loader);
                    HALAnalog.Initialize(s_library, loader);
                    HALCAN.Initialize(s_library, loader);
                    HALCanTalonSRX.Initialize(s_library, loader);
                    HALCompressor.Initialize(s_library, loader);
                    HALDigital.Initialize(s_library, loader);
                    HALInterrupts.Initialize(s_library, loader);
                    HALNotifier.Initialize(s_library, loader);
                    HALPDP.Initialize(s_library, loader);
                    HALPower.Initialize(s_library, loader);
                    HALSemaphore.Initialize(s_library, loader);
                    HALSerialPort.Initialize(s_library, loader);
                    HALSolenoid.Initialize(s_library, loader);
                    HALUtilities.Initialize(s_library, loader);
                    // ReSharper restore ExpressionIsAlwaysNull
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Environment.Exit(1);
                }
                s_libraryLoaded = true;
            }
        }

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HAL.GetPort = getPort;
            Base.HAL.GetPortWithModule = getPortWithModule;
            Base.HAL.FreePort = freePort;
            Base.HAL.GetHALErrorMessage = getHALErrorMessage;
            Base.HAL.GetFPGAVersion = getFPGAVersion;
            Base.HAL.GetFPGARevision = getFPGARevision;
            Base.HAL.GetFPGATime = getFPGATime;
            Base.HAL.GetFPGAButton = getFPGAButton;
            Base.HAL.HALSetErrorData = HALSetErrorData;
            Base.HAL.HALSendError = HALSendError;
            Base.HAL.GetControlWord = HALGetControlWord;
            Base.HAL.HALGetAllianceStation = HALGetAllianceStation;
            Base.HAL.HALGetJoystickAxes = HALGetJoystickAxes;
            Base.HAL.HALGetJoystickPOVs = HALGetJoystickPOVs;
            Base.HAL.HALGetJoystickButtons = HALGetJoystickButtons;
            Base.HAL.HALGetJoystickDescriptor = HALGetJoystickDescriptor;
            Base.HAL.HALGetJoystickIsXbox = HALGetJoystickIsXbox;
            Base.HAL.HALGetJoystickType = HALGetJoystickType;
            Base.HAL.HALGetJoystickAxisType = HALGetJoystickAxisType;
            Base.HAL.HALSetJoystickOutputs = HALSetJoystickOutputs;
            Base.HAL.HALGetMatchTime = HALGetMatchTime;
            Base.HAL.HALSetNewDataSem = HALSetNewDataSem;
            Base.HAL.HALGetSystemActive = HALGetSystemActive;
            Base.HAL.HALGetBrownedOut = HALGetBrownedOut;
            Base.HAL.HALInitialize = HALInitialize;
            Base.HAL.HALNetworkCommunicationObserveUserProgramStarting = HALNetworkCommunicationObserveUserProgramStarting;
            Base.HAL.HALNetworkCommunicationObserveUserProgramDisabled = HALNetworkCommunicationObserveUserProgramDisabled;
            Base.HAL.HALNetworkCommunicationObserveUserProgramAutonomous = HALNetworkCommunicationObserveUserProgramAutonomous;
            Base.HAL.HALNetworkCommunicationObserveUserProgramTeleop = HALNetworkCommunicationObserveUserProgramTeleop;
            Base.HAL.HALNetworkCommunicationObserveUserProgramTest = HALNetworkCommunicationObserveUserProgramTest;
            Base.HAL.HALReport = HALReport;
        }

        private static void StartSimulator(string loadDirectory)
        {
            //Get a list of all files
            string[] dllFileNames = null;
            if (Directory.Exists(loadDirectory))
            {
                dllFileNames = Directory.GetFiles(loadDirectory);
            }

            //If files not found, just return
            if (dllFileNames == null)
                return;

            //Load all assemblies in folder
            var assemblies = new List<Assembly>(dllFileNames.Length);

            foreach (var s in dllFileNames)
            {
                //Only do anything if its a DLL or EXE.
                string ext = Path.GetExtension(s).ToLower();
                if (ext.Contains("dll") || ext.Contains("exe"))
                {
                    try
                    {
                        //Try to load it
                        var asmbly = AssemblyName.GetAssemblyName(s);
                        var asm = Assembly.Load(asmbly);
                        if (asm != null)
                        {
                            assemblies.Add(asm);
                        }
                    }
                    catch (Exception)
                    {
                        //If loading fails, its probably native. Just skip it.
                    }

                }
            }

            //assemblies.AddRange(dllFileNames.Where(name => new FileInfo(name).Extension == ".dll").Select(AssemblyName.GetAssemblyName).Select(Assembly.Load));

            //Find all types inheriting from ISimulator
            Type simulatorType = typeof(ISimulator);
            ICollection<Type> simulatorTypes = new List<Type>();
            try
            {
                foreach (var type in from assembly in assemblies where assembly != null select assembly.GetTypes() into types from type in types select type)
                {
                    try
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                        }
                        else
                        {
                            if (type.GetInterface(simulatorType.FullName) != null)
                            {
                                simulatorTypes.Add(type);
                            }
                        }
                    }
                    catch
                    {
                    }

                }
            }
            catch
            {
                return;
            }
            

            //If none were found, just return
            if (simulatorTypes.Count == 0)
                return;

            //Create an instance of all found ISimulators
            List<ISimulator> simulators;
            try
            {
                simulators = simulatorTypes.Select(type => (ISimulator)Activator.CreateInstance(type)).ToList();
            }
            catch (MissingMethodException)
            {
                Console.WriteLine("Could not properly open one of the ISimulators. Make sure they all have parameterless constructors.");
                return;
            }


            //If only 1 was found, start it.
            if (simulatorTypes.Count == 1)
            {
                StartSimulator(simulators[0]);
                return;
            }

            //Otherwise list all simulators, and select one.
            int input = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Please select a simulator:\n");
                for (int i = 0; i < simulators.Count; i++)
                {
                    Console.WriteLine($"{i}. {simulators[i].Name}");
                }
                Console.WriteLine($"{simulators.Count}. Skip simulator");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    input = -1;
                }
                if (input == simulators.Count)
                {
                    return;
                }
            } while ((input < 0) || (input >= simulators.Count));

            StartSimulator(simulators[input]);
        }

        private static void StartSimulator(ISimulator simulator)
        {
            Console.WriteLine($"Starting Simulator: {simulator.Name}");
            if (s_simThread != null)
            {
                s_simThread.Abort();
                s_simThread.Join();
            }
            simulator.Initialize();
            s_simThread = new Thread(simulator.Start);
            s_simThread.Start();
        }

        private static Thread s_simThread;

        public static void KillSimulator()
        {
            s_simThread?.Abort();
        }


        /// <summary>
        /// Gets a RoboRIO Port.
        /// </summary>
        /// <param name="pin">The hardware pin of the port</param>
        /// <returns>IntPtr containing the port</returns>
        [CalledSimFunction]
        public static IntPtr getPort(byte pin)
        {
            return getPortWithModule(0, pin);
        }

        /// <summary>
        /// Gets a RoboRIO Port with Module
        /// </summary>
        /// <param name="module">Hardware Module</param>
        /// <param name="pin">Hardware Pin</param>
        /// <returns>IntPtr containing the port</returns>
        [CalledSimFunction]
        public static IntPtr getPortWithModule(byte module, byte pin)
        {
            Port port = new Port
            {
                pin = pin,
                module = module
            };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(port));
            Marshal.StructureToPtr(port, ptr, true);
            return ptr;
        }

        [CalledSimFunction]
        public static void freePort(IntPtr port_pointer)
        {
            if (port_pointer == IntPtr.Zero) return;
            Marshal.FreeHGlobal(port_pointer);
        }


        /// <summary>
        /// Sets the NewDataSem used to indicate new DS ports.
        /// </summary>
        /// <param name="sem"></param>
        [CalledSimFunction]
        public static void HALSetNewDataSem(IntPtr sem)
        {
            s_halNewDataSem = sem;
        }

        /// <summary>
        /// Returns a HAL Error Message
        /// </summary>
        /// <param name="code">The Error Code</param>
        /// <returns>The Error Message</returns>
        [CalledSimFunction]
        public static string getHALErrorMessage(int code)
        {
            string retVal = "";

            if (code == 0)
                retVal = "";
            else if (code == HALErrorConstants.CTR_RxTimeout)
                retVal = "CTRE CAN Recieve Timeout";
            else if (code == HALErrorConstants.CTR_InvalidParamValue)
                retVal = "CTRE CAN Invalid Parameter";
            else if (code == HALErrorConstants.CTR_UnexpectedArbId)
                retVal = "CTRE Unexpected Arbitration ID (CAN Node ID)";
            else if (code == HALErrorConstants.CTR_TxFailed)
                retVal = "CTRE CAN Transmit Error";
            else if (code == HALErrorConstants.CTR_SigNotUpdated)
                retVal = "CTRE CAN Signal Not Updated";
            else if (code == HALErrorConstants.NiFpga_Status_FifoTimeout)
                retVal = "NIFPGA: FIFO timeout error";
            else if (code == HALErrorConstants.NiFpga_Status_TransferAborted)
                retVal = "NIFPGA: Transfer aborted error";
            else if (code == HALErrorConstants.NiFpga_Status_MemoryFull)
                retVal = "NIFPGA: Memory Allocation failed, memory full";
            else if (code == HALErrorConstants.NiFpga_Status_SoftwareFault)
                retVal = "NIFPGA: Unexepected software error";
            else if (code == HALErrorConstants.NiFpga_Status_InvalidParameter)
                retVal = "NIFPGA: Invalid Parameter";
            else if (code == HALErrorConstants.NiFpga_Status_ResourceNotFound)
                retVal = "NIFPGA: Resource not found";
            else if (code == HALErrorConstants.NiFpga_Status_ResourceNotInitialized)
                retVal = "NIFPGA: Resource not initialized";
            else if (code == HALErrorConstants.NiFpga_Status_HardwareFault)
                retVal = "NIFPGA: Hardware Fault";
            else if (code == HALErrorConstants.NiFpga_Status_IrqTimeout)
                retVal = "NIFPGA: Interrupt timeout";

            else if (code == HALErrorConstants.ERR_CANSessionMux_InvalidBuffer)
                retVal = "CAN: Invalid Buffer";
            else if (code == HALErrorConstants.ERR_CANSessionMux_MessageNotFound)
                retVal = "CAN: Message not found";
            else if (code == HALErrorConstants.WARN_CANSessionMux_NoToken)
                retVal = "CAN: No token";
            else if (code == HALErrorConstants.ERR_CANSessionMux_NotAllowed)
                retVal = "CAN: Not allowed";
            else if (code == HALErrorConstants.ERR_CANSessionMux_NotInitialized)
                retVal = "CAN: Not initialized";

            else if (code == HALErrorConstants.SAMPLE_RATE_TOO_HIGH)
                retVal = "HAL: Analog module sample rate is too high";
            else if (code == HALErrorConstants.VOLTAGE_OUT_OF_RANGE)
                retVal = "HAL: Voltage to convert to raw value is out of range [0; 5]";
            else if (code == HALErrorConstants.LOOP_TIMING_ERROR)
                retVal = "HAL: Digital module loop timing is not the expected value";
            else if (code == HALErrorConstants.SPI_WRITE_NO_MOSI)
                retVal = "HAL: Cannot write to SPI port with no MOSI output";
            else if (code == HALErrorConstants.SPI_READ_NO_MISO)
                retVal = "HAL: Cannot read from SPI port with no MISO input";
            else if (code == HALErrorConstants.SPI_READ_NO_DATA)
                retVal = "HAL: No Data available to read from SPI";
            else if (code == HALErrorConstants.INCOMPATIBLE_STATE)
                retVal = "HAL: Incompatible State: The operation cannot be completed";
            else if (code == HALErrorConstants.NO_AVAILABLE_RESOURCES)
                retVal = "HAL: No available resources to allocate";
            else if (code == HALErrorConstants.NULL_PARAMETER)
                retVal = "HAL: A pointer parameter to a method is NULL";
            else if (code == HALErrorConstants.ANALOG_TRIGGER_LIMIT_ORDER_ERROR)
                retVal = "HAL: AnalogTrigger limits error.  Lower limit > Upper Limit";
            else if (code == HALErrorConstants.ANALOG_TRIGGER_PULSE_OUTPUT_ERROR)
                retVal = "HAL: Attempted to read AnalogTrigger pulse output.";
            else if (code == HALErrorConstants.PARAMETER_OUT_OF_RANGE)
                retVal = "HAL: A parameter is out of range.";
            else if (code == HALErrorConstants.RESOURCE_IS_ALLOCATED)
                retVal = "HAL: A resource is already allocated.";
            else
                retVal = "Unknown error status";


            return retVal;
        }

        /// <summary>
        /// Gets the FPGA Version
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static ushort getFPGAVersion(ref int status)
        {
            status = 0;
            return 2015;
        }

        /// <summary>
        /// Gets the FPGA Revision
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static uint getFPGARevision(ref int status)
        {
            status = 0;
            return 0;
        }

        /// <summary>
        /// Gets the FPGA Time
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static ulong getFPGATime(ref int status)
        {
            status = 0;
            return (ulong)SimHooks.GetFPGATime();
        }

        /// <summary>
        /// Gets the FPGA Time
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static bool getFPGAButton(ref int status)
        {
            status = 0;
            return SimData.RoboRioData.FPGAButton;
        }

        /// <summary>
        /// Sets the HAL error Data
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="wait_ms"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static int HALSetErrorData(string errors, int wait_ms)
        {
            //TODO: Logger 
            //ErrorData = errors;
            HALSendError(true, 1, false, errors, "", "", true);
            return 0;
        }

        public static int HALSendError(bool isError, int errorCode, bool isLVCode, string details,
            string location, string callStack, bool printMsg)
        {
            //No need to rate limit sim
            //Log Error
            ErrorList.Add(new ErrorData(isError, errorCode, isLVCode, details, location, callStack));

            if (printMsg)
            {
                if (location != null && location[0] != '\0')
                {
                    Console.Error.WriteLine($"{(isError ? "Error" : "Warning")} at {location}");
                }
                if (callStack != null && callStack[0] != '\0')
                {
                    Console.Error.WriteLine(callStack);
                }
            }
            return 0;
        }

        /// <summary>
        /// Returns a control word containing the DS states.
        /// </summary>
        /// <returns></returns>
        [CalledSimFunction]
        public static HALControlWord HALGetControlWord()
        {
            return new HALControlWord(DriverStation.ControlData.Enabled,
                DriverStation.ControlData.Autonomous,
                DriverStation.ControlData.Test,
                DriverStation.ControlData.EStop,
                DriverStation.ControlData.FmsAttached,
                DriverStation.ControlData.DsAttached);
        }

        /// <summary>
        /// Gets the Alliance Station
        /// </summary>
        /// <param name="allianceStation"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static int HALGetAllianceStation(ref HALAllianceStationID allianceStation)
        {
            int data = (int)DriverStation.AllianceStation;
            if (data < 6 && data >= 0)
            {
                allianceStation = DriverStation.AllianceStation;
            }
            else
            {
                allianceStation = HALAllianceStationID.HALAllianceStationID_red1;
            }
            return 0;
        }

        [CalledSimFunction]
        public static int HALGetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes)
        {
            axes.axes = new HALJoystickAxesArray();
            var joyData = DriverStation.Joysticks[joystickNum].Axes;
            var count = DriverStation.Joysticks[joystickNum].NumAxes;
            for (short i = 0; i < count; i++)
            {
                int tmp = 0;
                if (joyData[i] < 0)
                    tmp = (int)(joyData[i] * 128);
                else
                    tmp = (int)(joyData[i] * 127);
                axes.axes[i] = (short)tmp;
            }
            axes.count = (ushort)count;
            return 0;
        }

        [CalledSimFunction]
        public static int HALGetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs)
        {
            povs.povs = new HALJoystickPOVArray();
            var povData = DriverStation.Joysticks[joystickNum].Povs;
            for (int i = 0; i < povData.Length; i++)
            {
                povs.povs[i] = (short)povData[i];
            }
            povs.count = (ushort)povData.Length;
            return 0;
        }

        [CalledSimFunction]
        public static int HALGetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons)
        {
            var b = DriverStation.Joysticks[joystickNum].Buttons;
            var count = DriverStation.Joysticks[joystickNum].NumButtons;
            uint total = 0;
            for (int i = 1; i < count + 1; i++)
            {
                total = total + (uint)((b[i] ? 1 : 0) << i - 1);
            }

            buttons.buttons = total;
            buttons.count = (byte)(count);
            return 0;
        }

        [CalledSimFunction]
        public static int HALGetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc)
        {
            var stick = DriverStation.Joysticks[joystickNum];
            desc.isXbox = (byte)stick.IsXbox;
            desc.type = stick.Type;
            CreateUTF8String(stick.Name, ref desc.name);//stick.Name;
            desc.axisCount = (byte)stick.Axes.Length;
            desc.buttonCount = (byte)(stick.Buttons.Length - 1);
            return 0;
        }

        internal static void CreateUTF8String(string str, ref HALJoystickNameArray array)
        {
            if (str == null)
            {
                str = "";
            }
            var bytes = Encoding.UTF8.GetByteCount(str);

            var buffer = new byte[bytes + 1];
            Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 0);
            buffer[bytes] = 0;
            int i = 0;
            for (; i < buffer.Length; i++)
            {
                array[i] = buffer[i];
            }
        }

        [CalledSimFunction]
        public static int HALGetJoystickIsXbox(byte joystickNum)
        {
            var stick = DriverStation.Joysticks[joystickNum];
            return stick.IsXbox;
        }

        [CalledSimFunction]
        public static int HALGetJoystickType(byte joystickNum)
        {
            var stick = DriverStation.Joysticks[joystickNum];
            return stick.Type;
        }

        [CalledSimFunction]
        public static int HALGetJoystickAxisType(byte joystickNum, byte axis)
        {
            var stick = DriverStation.Joysticks[joystickNum];
            return 0;
        }

        [CalledSimFunction]
        public static int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble,
            ushort rightRumble)
        {
            DriverStation.Joysticks[joystickNum].LeftRumble = leftRumble;
            DriverStation.Joysticks[joystickNum].RightRumble = rightRumble;
            //halData[]TODO:Outputs
            return 0;
        }

        /// <summary>
        /// Gets the Current match time.
        /// </summary>
        /// <param name="matchTime"></param>
        /// <returns></returns>
        /// <remarks>Returns -1.0 if the robot is disabled, in test mode, or enabled, but not field connected
        /// or practice mode.</remarks>
        [CalledSimFunction]
        public static int HALGetMatchTime(ref float matchTime)
        {

            var matchStart = 0.0;
            //If Enabled
            if (DriverStation.ControlData.Enabled)
            {
                if (DriverStation.ControlData.Autonomous)
                {
                    matchTime = (float)(AutonomousTime - (SimHooks.GetFPGATimestamp() - matchStart));
                }
                else
                {
                    matchTime = (float)(TeleopTime - (SimHooks.GetFPGATimestamp() - matchStart));
                }
            }
            else
            {
                matchTime = -1.0f;
            }
            return 0;
        }

        [CalledSimFunction]
        public static bool HALGetSystemActive(ref int status)
        {
            status = 0;
            return true;
        }

        [CalledSimFunction]
        public static bool HALGetBrownedOut(ref int status)
        {
            status = 0;
            return false;
        }

        [CalledSimFunction]
        public static int HALInitialize(int mode)
        {
            ResetHALData(true);

            //Check to see if we selected an alternate directory.
            string[] commandLineArgs = Environment.GetCommandLineArgs();

            string loadDirectory = Directory.GetCurrentDirectory();

            //Check for alternate directory
            if (commandLineArgs.Length > 1)
            {
                foreach (var arg in commandLineArgs)
                {
                    if (arg.Contains("--simdir"))
                    {
                        //Use this as the simulator directory.
                        int firstColonIndex = arg.IndexOf(':');
                        if (firstColonIndex == -1) break;
                        string path = arg.Substring(firstColonIndex + 1);
                        if (Directory.Exists(path))
                        {
                            loadDirectory = path;
                            break;
                        }
                    }
                }
            }

            StartSimulator(loadDirectory);
            return 1;

        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramStarting()
        {
            SimData.GlobalData.UserProgramState = ProgramState.Starting;
            SimData.GlobalData.ProgramStarted = true;
        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramDisabled()
        {
            SimData.GlobalData.UserProgramState = ProgramState.Disabled;
        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramAutonomous()
        {
            SimData.GlobalData.UserProgramState = ProgramState.Autonomous;
        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramTeleop()
        {
            SimData.GlobalData.UserProgramState = ProgramState.Teleop;
        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramTest()
        {
            SimData.GlobalData.UserProgramState = ProgramState.Test;
        }

        [CalledSimFunction]
        public static uint HALReport(byte resource, byte instanceNumber, byte context = 0, string feature = null)
        {
            switch (resource)
            {
                case (byte)ResourceType.kResourceType_Jaguar:
                    PWM[instanceNumber].Type = ControllerType.Jaguar;
                    break;
                case (byte)ResourceType.kResourceType_Talon:
                    PWM[instanceNumber].Type = ControllerType.Talon;
                    break;
                case (byte)ResourceType.kResourceType_TalonSRX:
                    PWM[instanceNumber].Type = ControllerType.TalonSRX;
                    break;
                case (byte)ResourceType.kResourceType_Victor:
                    PWM[instanceNumber].Type = ControllerType.Victor;
                    break;
                case (byte)ResourceType.kResourceType_VictorSP:
                    PWM[instanceNumber].Type = ControllerType.VictorSP;
                    break;
                case (byte)ResourceType.kResourceType_RevSPARK:
                    PWM[instanceNumber].Type = ControllerType.Spark;
                    break;
                case (byte)ResourceType.kResourceType_MindsensorsSD540:
                    PWM[instanceNumber].Type = ControllerType.SD540;
                    break;
                case (byte)ResourceType.kResourceType_Servo:
                    PWM[instanceNumber].Type = ControllerType.Servo;
                    break;
                case (byte)ResourceType.kResourceType_Solenoid:
                    GetPCM(context).Solenoids[instanceNumber].Initialized = true;
                    break;
            }
            if (!Reports.ContainsKey(resource))
            {
                Reports.Add(resource, new List<dynamic>());
            }
            Reports[resource].Add(instanceNumber);

            return 0;
        }
    }
}
