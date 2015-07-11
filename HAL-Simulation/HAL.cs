

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using HAL_Base;
using static HAL_Simulator.SimData;
using static HAL_Simulator.HALErrorConstants;

namespace HAL_Simulator
{
    public class HAL
    {
        public const double AutonomousTime = 15.0;
        public const double TeleopTime = 135.0;

        public static IntPtr getPort(byte pin)
        {
            return getPortWithModule(0, pin);
        }

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

        public static void HALSetNewDataSem(IntPtr sem)
        {
            halNewDataSem = sem;
        }

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

        public static ushort getFPGAVersion(ref int status)
        {
            status = 0;
            return 2015;
        }

        public static uint getFPGARevision(ref int status)
        {
            status = 0;
            return 0;
        }

        public static uint getFPGATime(ref int status)
        {
            status = 0;
            return (uint)SimHooks.GetFPGATime();
        }

        public static bool getFPGAButton(ref int status)
        {
            status = 0;
            return halData["fpga_button"];
        }

        public static int HALSetErrorData(string errors, int errorsLength, int wait_ms)
        {
            //TODO: Logger 
            halData["error_data"] = errors;
            return 0;
        }

        public static HALControlWord HALGetControlWord()
        {
            var h = halData["control"];
            return new HALControlWord(h["enabled"], h["autonomous"], h["test"], h["eStop"], h["fms_attached"], h["ds_attached"]);
        }

        public static int HALGetAllianceStation(ref HALAllianceStationID allianceStation)
        {
            return (int)halData["alliance_station"];
        }

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
            axes.count = 12; //Need to make this netter.
            return 0;
        }

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

        public static int HALGetJoystickIsXbox(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return (int)stick["isXbox"];
        }

        public static int HALGetJoystickType(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return (int)stick["type"];
        }

        public static IntPtr HALGetJoystickName(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return Marshal.StringToHGlobalAnsi(stick["name"]);
        }

        public static int HALGetJoystickAxisType(byte joystickNum, byte axis)
        {
            var stick = halData["joysticks"][joystickNum];
            return 0;//stick["NotImplemented"];
        }

        public static int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble,
            ushort rightRumble)
        {
            halData["joysticks"][joystickNum]["leftRumble"] = leftRumble;
            halData["joysticks"][joystickNum]["rightRumble"] = rightRumble;
            //halData[]TODO:Outputs
            return 0;
        }

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
                matchStart = -1.0f;
            }
            return 0;
        }

        public static bool HALGetSystemActive(ref int status)
        {
            status = 0;
            return true;
        }

        public static bool HALGetBrownedOut(ref int status)
        {
            status = 0;
            return false;
        }

        public static int HALInitialize(int mode)
        {
            ResetHALData();
            //Uncomment this if we want a constant running driver station thread.
            //This should be done by the Sim implementation though. But it might be needed for testing.
            
            //ModeHelpers.StartDSLoop();
            

            return 1;
        }

        public static void HALNetworkCommunicationObserveUserProgramStarting()
        {
            halData["user_program_state"] = "starting";
        }

        public static void HALNetworkCommunicationObserveUserProgramDisabled()
        {
            halData["user_program_state"] = "disabled";
        }

        public static void HALNetworkCommunicationObserveUserProgramAutonomous()
        {
            halData["user_program_state"] = "autonomous";
        }

        public static void HALNetworkCommunicationObserveUserProgramTeleop()
        {
            halData["user_program_state"] = "teleop";
        }

        public static void HALNetworkCommunicationObserveUserProgramTest()
        {
            halData["user_program_state"] = "test";
        }

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
                    halData["solenoid"][instanceNumber]["initialized"] = true;
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
