using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HAL_Base;
using HAL_Simulator.Data;
using System.Text;
using static HAL_Simulator.SimData;
using static HAL_Simulator.HALErrorConstants;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    /// <summary>
    /// This attribute is added to any Sim functions that are called from base.
    /// It helps make sure that our delegates will work.
    /// </summary>
    public class CalledSimFunction : Attribute { }

    /// <summary>
    /// This class is used by HAL-Base, and is used to emulate the HAL. 
    /// Please do not call functions in this class directly. 
    /// </summary>
    internal class HAL
    {
        //Time constants used for GetMatchTime.
        public const double AutonomousTime = 15.0;
        public const double TeleopTime = 135.0;

        private static bool libraryLoaded = false;
        private static IntPtr library;

        public static void InitializeImpl()
        {
            if (!libraryLoaded)
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
                    ILibraryLoader loader = null;
                    Initialize(library, loader);
                    HALAccelerometer.Initialize(library, loader);
                    HALAnalog.Initialize(library, loader);
                    HALCAN.Initialize(library, loader);
                    HALCanTalonSRX.Initialize(library, loader);
                    HALCompressor.Initialize(library, loader);
                    HALDigital.Initialize(library, loader);
                    HALInterrupts.Initialize(library, loader);
                    HALNotifier.Initialize(library, loader);
                    HALPDP.Initialize(library, loader);
                    HALPower.Initialize(library, loader);
                    HALSemaphore.Initialize(library, loader);
                    HALSerialPort.Initialize(library, loader);
                    HALSolenoid.Initialize(library, loader);
                    HALUtilities.Initialize(library, loader);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Environment.Exit(1);
                }
                libraryLoaded = true;
            }
        }

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HAL.GetPort = getPort;
            HAL_Base.HAL.GetPortWithModule = getPortWithModule;
            HAL_Base.HAL.GetHALErrorMessage = getHALErrorMessage;
            HAL_Base.HAL.GetFPGAVersion = getFPGAVersion;
            HAL_Base.HAL.GetFPGARevision = getFPGARevision;
            HAL_Base.HAL.GetFPGATime = getFPGATime;
            HAL_Base.HAL.GetFPGAButton = getFPGAButton;
            HAL_Base.HAL.HALSetErrorData = HALSetErrorData;
            HAL_Base.HAL.GetControlWord = HALGetControlWord;
            HAL_Base.HAL.HALGetAllianceStation = HALGetAllianceStation;
            HAL_Base.HAL.HALGetJoystickAxes = HALGetJoystickAxes;
            HAL_Base.HAL.HALGetJoystickPOVs = HALGetJoystickPOVs;
            HAL_Base.HAL.HALGetJoystickButtons = HALGetJoystickButtons;
            HAL_Base.HAL.HALGetJoystickDescriptor = HALGetJoystickDescriptor;
            HAL_Base.HAL.HALGetJoystickIsXbox = HALGetJoystickIsXbox;
            HAL_Base.HAL.HALGetJoystickType = HALGetJoystickType;
            HAL_Base.HAL.HALGetJoystickName = HALGetJoystickName;
            HAL_Base.HAL.HALGetJoystickAxisType = HALGetJoystickAxisType;
            HAL_Base.HAL.HALSetJoystickOutputs = HALSetJoystickOutputs;
            HAL_Base.HAL.HALGetMatchTime = HALGetMatchTime;
            HAL_Base.HAL.HALSetNewDataSem = HALSetNewDataSem;
            HAL_Base.HAL.HALGetSystemActive = HALGetSystemActive;
            HAL_Base.HAL.HALGetBrownedOut = HALGetBrownedOut;
            HAL_Base.HAL.HALInitialize = HALInitialize;
            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramStarting = HALNetworkCommunicationObserveUserProgramStarting;
            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramDisabled = HALNetworkCommunicationObserveUserProgramDisabled;
            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramAutonomous = HALNetworkCommunicationObserveUserProgramAutonomous;
            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramTeleop = HALNetworkCommunicationObserveUserProgramTeleop;
            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramTest = HALNetworkCommunicationObserveUserProgramTest;
            HAL_Base.HAL.HALReport = HALReport;
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


        /// <summary>
        /// Sets the NewDataSem used to indicate new DS ports.
        /// </summary>
        /// <param name="sem"></param>
        [CalledSimFunction]
        public static void HALSetNewDataSem(IntPtr sem)
        {
            HALNewDataSem = sem;
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
            else if (code == CTR_RxTimeout)
                retVal = "CTRE CAN Recieve Timeout";
            else if (code == CTR_InvalidParamValue)
                retVal = "CTRE CAN Invalid Parameter";
            else if (code == CTR_UnexpectedArbId)
                retVal = "CTRE Unexpected Arbitration ID (CAN Node ID)";
            else if (code == CTR_TxFailed)
                retVal = "CTRE CAN Transmit Error";
            else if (code == CTR_SigNotUpdated)
                retVal = "CTRE CAN Signal Not Updated";
            else if (code == NiFpga_Status_FifoTimeout)
                retVal = "NIFPGA: FIFO timeout error";
            else if (code == NiFpga_Status_TransferAborted)
                retVal = "NIFPGA: Transfer aborted error";
            else if (code == NiFpga_Status_MemoryFull)
                retVal = "NIFPGA: Memory Allocation failed, memory full";
            else if (code == NiFpga_Status_SoftwareFault)
                retVal = "NIFPGA: Unexepected software error";
            else if (code == NiFpga_Status_InvalidParameter)
                retVal = "NIFPGA: Invalid Parameter";
            else if (code == NiFpga_Status_ResourceNotFound)
                retVal = "NIFPGA: Resource not found";
            else if (code == NiFpga_Status_ResourceNotInitialized)
                retVal = "NIFPGA: Resource not initialized";
            else if (code == NiFpga_Status_HardwareFault)
                retVal = "NIFPGA: Hardware Fault";
            else if (code == NiFpga_Status_IrqTimeout)
                retVal = "NIFPGA: Interrupt timeout";

            else if (code == ERR_CANSessionMux_InvalidBuffer)
                retVal = "CAN: Invalid Buffer";
            else if (code == ERR_CANSessionMux_MessageNotFound)
                retVal = "CAN: Message not found";
            else if (code == WARN_CANSessionMux_NoToken)
                retVal = "CAN: No token";
            else if (code == ERR_CANSessionMux_NotAllowed)
                retVal = "CAN: Not allowed";
            else if (code == ERR_CANSessionMux_NotInitialized)
                retVal = "CAN: Not initialized";

            else if (code == SAMPLE_RATE_TOO_HIGH)
                retVal = "HAL: Analog module sample rate is too high";
            else if (code == VOLTAGE_OUT_OF_RANGE)
                retVal = "HAL: Voltage to convert to raw value is out of range [0; 5]";
            else if (code == LOOP_TIMING_ERROR)
                retVal = "HAL: Digital module loop timing is not the expected value";
            else if (code == SPI_WRITE_NO_MOSI)
                retVal = "HAL: Cannot write to SPI port with no MOSI output";
            else if (code == SPI_READ_NO_MISO)
                retVal = "HAL: Cannot read from SPI port with no MISO input";
            else if (code == SPI_READ_NO_DATA)
                retVal = "HAL: No data available to read from SPI";
            else if (code == INCOMPATIBLE_STATE)
                retVal = "HAL: Incompatible State: The operation cannot be completed";
            else if (code == NO_AVAILABLE_RESOURCES)
                retVal = "HAL: No available resources to allocate";
            else if (code == NULL_PARAMETER)
                retVal = "HAL: A pointer parameter to a method is NULL";
            else if (code == ANALOG_TRIGGER_LIMIT_ORDER_ERROR)
                retVal = "HAL: AnalogTrigger limits error.  Lower limit > Upper Limit";
            else if (code == ANALOG_TRIGGER_PULSE_OUTPUT_ERROR)
                retVal = "HAL: Attempted to read AnalogTrigger pulse output.";
            else if (code == PARAMETER_OUT_OF_RANGE)
                retVal = "HAL: A parameter is out of range.";
            else if (code == RESOURCE_IS_ALLOCATED)
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
        public static uint getFPGATime(ref int status)
        {
            status = 0;
            return (uint)SimHooks.GetFPGATime();
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
        /// Sets the HAL error data
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="wait_ms"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static int HALSetErrorData(string errors, int wait_ms)
        {
            //TODO: Logger 
            ErrorData = errors;
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
            for (short i = 0; i < joyData.Length; i++)
            {
                int tmp = 0;
                if (joyData[i] < 0)
                    tmp = (int)(joyData[i] * 128);
                else
                    tmp = (int)(joyData[i] * 127);
                axes.axes[i] = (short)tmp;
            }
            axes.count = (ushort)joyData.Length;
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
            uint total = 0;
            for (int i = 1; i < b.Length; i++)
            {
                total = total + (uint)((b[i] ? 1 : 0) << i - 1);
            }

            buttons.buttons = total;
            buttons.count = (byte)(b.Length - 1);
            return 0;
        }

        [CalledSimFunction]
        public static int HALGetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc)
        {
            int len;
            var stick = DriverStation.Joysticks[joystickNum];
            desc.isXbox = (byte)(stick.IsXbox);
            desc.type = stick.Type;
            desc.name = CreateUTF8String(stick.Name, out len);//stick.Name;
            desc.axisCount = (byte)stick.Axes.Length;
            desc.buttonCount = (byte)(stick.Buttons.Length - 1);
            return 0;
        }

        internal static byte[] CreateUTF8String(string str, out int size)
        {
            if (str == null)
            {
                str = "";
            }

            var bytes = Encoding.UTF8.GetByteCount(str);

            var buffer = new byte[bytes + 1];
            size = bytes;
            Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 0);
            buffer[bytes] = 0;
            return buffer;
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
        public static string HALGetJoystickName(byte joystickNum)
        {
            var stick = DriverStation.Joysticks[joystickNum];
            return stick.Name;
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
