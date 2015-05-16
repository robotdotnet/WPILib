using System;
using System.Reflection;


namespace HAL_FRC
{
    public enum HALAllianceStationID
    {
// ReSharper disable InconsistentNaming
        HALAllianceStationID_red1,

        HALAllianceStationID_red2,

        HALAllianceStationID_red3,

        HALAllianceStationID_blue1,

        HALAllianceStationID_blue2,


        HALAllianceStationID_blue3,
// ReSharper restore InconsistentNaming
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickAxes
    {
        /// unsigned short
        public ushort count;

        /// short[12]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I2)]
        public short[] axes;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickPOVs
    {
        /// unsigned short
        public ushort count;

        /// short[12]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I2)]
        public short[] povs;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickButtons
    {
        /// unsigned int
        public uint buttons;

        /// byte
        public byte count;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct HALJoystickDescriptor
    {
        /// byte
        public byte isXbox;

        /// byte
        public byte type;

        /// char[256]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 256)]
        public string name;

        /// byte
        public byte axisCount;

        /// byte
        public byte axisTypes;

        /// byte
        public byte buttonCount;

        /// byte
        public byte povCount;
    }

    public class HAL
    {
        //public const string "libHALAthena_shared.so" = "libHALAthena_shared.so"; 

        /// Return Type: void*
        ///pin: byte
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPort")]
        public static extern System.IntPtr GetPort(byte pin);


        /// Return Type: void*
        ///module: byte
        ///pin: byte
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPortWithModule")]
        public static extern System.IntPtr GetPortWithModule(byte module, byte pin);


        /// Return Type: char*
        ///code: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getHALErrorMessage")]
        public static extern string GetHALErrorMessage(int code);


        /// Return Type: unsigned short
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getFPGAVersion")]
        public static extern ushort GetFPGAVersion(ref int status);


        /// Return Type: unsigned int
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getFPGARevision")]
        public static extern uint GetFPGARevision(ref int status);


        /// Return Type: unsigned int
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getFPGATime")]
        public static extern uint GetFPGATime(ref int status);


        /// Return Type: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getFPGAButton")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool GetFPGAButton(ref int status);


        /// Return Type: int
        ///errors: char*
        ///errorsLength: int
        ///wait_ms: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALSetErrorData")]
        public static extern int SetErrorData([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string errors, int errorsLength, int waitMs);


        /// Return Type: int
        ///data: HALControlWord*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetControlWord")]
        private static extern int GetControlWord(ref uint data);


        /// Return Type: int
        ///allianceStation: HALAllianceStationID*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetAllianceStation")]
        public static extern int GetAllianceStation(ref HALAllianceStationID allianceStation);


        /// Return Type: int
        ///joystickNum: byte
        ///axes: HALJoystickAxes*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetJoystickAxes")]
        public static extern int GetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes);


        /// Return Type: int
        ///joystickNum: byte
        ///povs: HALJoystickPOVs*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetJoystickPOVs")]
        public static extern int GetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs);


        /// Return Type: int
        ///joystickNum: byte
        ///buttons: HALJoystickButtons*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetJoystickButtons")]
        public static extern int GetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons);


        /// Return Type: int
        ///joystickNum: byte
        ///desc: HALJoystickDescriptor*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetJoystickDescriptor")]
        public static extern int GetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc);


        /// Return Type: int
        ///joystickNum: byte
        ///outputs: unsigned int
        ///leftRumble: unsigned short
        ///rightRumble: unsigned short
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALSetJoystickOutputs")]
        public static extern int SetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);


        /// Return Type: int
        ///matchTime: float*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetMatchTime")]
        public static extern int GetMatchTime(ref float matchTime);


        /// Return Type: void
        ///sem: void*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALSetNewDataSem")]
        public static extern void SetNewDataSem(System.IntPtr sem);


        /// Return Type: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetSystemActive")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool GetSystemActive(ref int status);


        /// Return Type: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALGetBrownedOut")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool GetBrownedOut(ref int status);


        /// Return Type: int
        ///mode: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALInitialize")]
        private static extern int HALInitialize(int mode = 0);


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramStarting")]
        public static extern void NetworkCommunicationObserveUserProgramStarting();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramDisabled")]
        public static extern void NetworkCommunicationObserveUserProgramDisabled();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramAutonomous")]
        public static extern void NetworkCommunicationObserveUserProgramAutonomous();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTeleop")]
        public static extern void NetworkCommunicationObserveUserProgramTeleop();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTest")]
        public static extern void NetworkCommunicationObserveUserProgramTest();


        /// Return Type: unsigned int
        ///resource: byte
        ///instanceNumber: byte
        ///context: byte
        ///feature: char*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALReport")]
        private static extern uint HALReport(byte resource, byte instanceNumber, byte context = 0, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string feature = null);


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "NumericArrayResize")]
        public static extern void NumericArrayResize();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "RTSetCleanupProc")]
        public static extern void RTSetCleanupProc();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "EDVR_CreateReference")]
        public static extern void EDVR_CreateReference();


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "Occur")]
        public static extern void Occur();


        //Getting Joystick Axes
        public static short[] GetJoystickAxes(byte joystickNum)
        {
            var axes = new HALJoystickAxes();
            GetJoystickAxes(joystickNum, ref axes);
            return axes.axes;
        }

        public static uint GetJoystickButtons(byte joystickNum, ref int count)
        {
            HALJoystickButtons joystickButtons = new HALJoystickButtons();
            GetJoystickButtons(joystickNum, ref joystickButtons);
            count = joystickButtons.count;
            return joystickButtons.buttons;
        }

        /*
        public static HALJoystickButtons GetJoystickButtons(byte joystickNum)
        {

            Console.WriteLine("Getting Joystick Axes");
            var buttons = new HALJoystickButtons();
            GetJoystickButtons(joystickNum, ref buttons);
            return buttons;
        }
        */
        public static HALJoystickDescriptor GetJoystickDescriptor(byte joystickNum)
        {
            var descriptor = new HALJoystickDescriptor();
            GetJoystickDescriptor(joystickNum, ref descriptor);
            return descriptor;
        }

        //GettingJoystickPOVs
        public static short[] GetJoystickPOVs(byte joystickNum)
        {

            var povs = new HALJoystickPOVs();
            GetJoystickPOVs(joystickNum, ref povs);
            return povs.povs;
        }

        public static int SetErrorData(string errors, int waitMs)
        {
            return SetErrorData(errors, errors.Length, waitMs);
        }

        public static void Initialize(int mode = 0)
        {
            var rv = HALInitialize();

            //HALDigital.SetupHAL(Assembly.LoadFrom("/home/lvuser/mono/HAL-RoboRIO.dll"));
            if (rv == 0)
            {
                //Throw exception saying HAL not initialized
                throw new Exception("HAL Initialize Failed");
            }
        }


        //Move to WPILib

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

        /*
        public static void CheckStatus(int status)
        {
            if (status < 0)
            {
                string message = GetHALErrorMessage(status);
                throw new SystemException(" Code: " + status + ". " + message);
            }
            else if (status > 0)
            {
                string message = GetHALErrorMessage(status);
                DriverStation.ReportError(message, true);
            }
        }
         * */

        public static HALControlWord GetControlWord()
        {
            //HALControlWord temp = new HALControlWord();
            uint temp = 0;
            GetControlWord(ref temp);
            return new HALControlWord(temp);
        }
    }

    public struct HALControlWord
    {
        private uint _wordData;

        public HALControlWord(uint data)
        {
            _wordData = data;
        }

        public bool GetEnabled()
        {
            return (_wordData & (1 << 1 - 1)) != 0;
        }

        public bool GetAutonomous()
        {
            return (_wordData & (1 << 2 - 1)) != 0;
        }

        public bool GetTest()
        {
            return (_wordData & (1 << 3 - 1)) != 0;
        }

        public bool GetEStop()
        {
            return (_wordData & (1 << 4 - 1)) != 0;
        }

        public bool GetFMSAttached()
        {
            return (_wordData & (1 << 5 - 1)) != 0;
        }

        public bool GetDSAttached()
        {
            return (_wordData & (1 << 6 - 1)) != 0;
        }
    }
}
