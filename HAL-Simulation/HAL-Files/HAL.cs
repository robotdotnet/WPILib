using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HAL_Base;
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
        /// <returns>IntPtr containing the Error message</returns>
        [CalledSimFunction]
        public static IntPtr getHALErrorMessage(int code)
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


            return Marshal.StringToHGlobalAnsi(retVal);
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
            return halData["fpga_button"];
        }

        /// <summary>
        /// Sets the HAL error data
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="errorsLength"></param>
        /// <param name="wait_ms"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static int HALSetErrorData(string errors, int errorsLength, int wait_ms)
        {
            //TODO: Logger 
            halData["error_data"] = errors;
            return 0;
        }

        /// <summary>
        /// Returns a control word containing the DS states.
        /// </summary>
        /// <returns></returns>
        [CalledSimFunction]
        public static HALControlWord HALGetControlWord()
        {
            var h = halData["control"];
            return new HALControlWord(h["enabled"], h["autonomous"], h["test"], h["eStop"], h["fms_attached"], h["ds_attached"]);
        }

        /// <summary>
        /// Gets the Alliance Station
        /// </summary>
        /// <param name="allianceStation"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static int HALGetAllianceStation(ref HALAllianceStationID allianceStation)
        {
            int data = (int) HalData["alliance_station"];
            if (data < 6 && data >= 0)
            {
                allianceStation = (HALAllianceStationID) data;
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
            var joyData = halData["joysticks"][joystickNum]["axes"];
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
            var povData = halData["joysticks"][joystickNum]["povs"];
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
            var b = halData["joysticks"][joystickNum]["buttons"];
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
            var stick = halData["joysticks"][joystickNum];
            desc.isXbox = (byte)(stick["isXbox"]);
            desc.type = stick["type"];
            desc.name = stick["name"];
            desc.axisCount = stick["axisCount"];
            desc.buttonCount = stick["buttonCount"];
            return 0;
        }

        [CalledSimFunction]
        public static int HALGetJoystickIsXbox(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return (int)stick["isXbox"];
        }

        [CalledSimFunction]
        public static int HALGetJoystickType(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return (int)stick["type"];
        }

        [CalledSimFunction]
        public static IntPtr HALGetJoystickName(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return Marshal.StringToHGlobalAnsi(stick["name"]);
        }

        [CalledSimFunction]
        public static int HALGetJoystickAxisType(byte joystickNum, byte axis)
        {
            var stick = halData["joysticks"][joystickNum];
            return 0;
        }

        [CalledSimFunction]
        public static int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble,
            ushort rightRumble)
        {
            halData["joysticks"][joystickNum]["leftRumble"] = leftRumble;
            halData["joysticks"][joystickNum]["rightRumble"] = rightRumble;
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

            var matchStart = halData["time"]["match_start"];
            //If Enabled
            if (halData["control"]["enabled"])
            {
                if (halData["control"]["autonomous"])
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
            halData["user_program_state"] = "starting";
            halData["program_started"] = true;
        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramDisabled()
        {
            halData["user_program_state"] = "disabled";
        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramAutonomous()
        {
            halData["user_program_state"] = "autonomous";
        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramTeleop()
        {
            halData["user_program_state"] = "teleop";
        }

        [CalledSimFunction]
        public static void HALNetworkCommunicationObserveUserProgramTest()
        {
            halData["user_program_state"] = "test";
        }

        [CalledSimFunction]
        public static uint HALReport(byte resource, byte instanceNumber, byte context = 0, string feature = null)
        {
            switch (resource)
            {
                case (byte)ResourceType.kResourceType_Jaguar:
                    halData["pwm"][instanceNumber]["type"] = "jaguar";
                    break;
                case (byte)ResourceType.kResourceType_Talon:
                    halData["pwm"][instanceNumber]["type"] = "talon";
                    break;
                case (byte)ResourceType.kResourceType_TalonSRX:
                    halData["pwm"][instanceNumber]["type"] = "talonsrx";
                    break;
                case (byte)ResourceType.kResourceType_Victor:
                    halData["pwm"][instanceNumber]["type"] = "victor";
                    break;
                case (byte)ResourceType.kResourceType_VictorSP:
                    halData["pwm"][instanceNumber]["type"] = "victorsp";
                    break;
                case (byte)ResourceType.kResourceType_Servo:
                    halData["pwm"][instanceNumber]["type"] = "servo";
                    break;
                case (byte)ResourceType.kResourceType_Solenoid:
                    halData["pcm"][(int)context]["solenoid"][instanceNumber]["initialized"] = true;
                    break;
            }
            if (!halData["reports"].ContainsKey(resource))
            {
                halData["reports"].Add(resource, new List<dynamic>());
            }
            halData["reports"][resource].Add(instanceNumber);

            return 0;
        }
    }
}
