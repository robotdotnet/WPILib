using System;
using System.Linq;
using System.Reflection;


namespace HAL_Base
{
    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// Joystick Axes Struct
    /// </summary>
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickAxes
    {
        /// unsigned short
        public ushort count;

        //For some reason, the code works with 12 Int16s, but not an array of 12 Int16s. Weird...
        /// short[12]
        //[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12)]
        //public Int16[] axes;

        
        public Int16 axes0;
        public Int16 axes1;
        public Int16 axes2;
        public Int16 axes3;
        public Int16 axes4;
        public Int16 axes5;
        public Int16 axes6;
        public Int16 axes7;
        public Int16 axes8;
        public Int16 axes9;
        public Int16 axes10;
        public Int16 axes11;
        
    }
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickPOVs
    {
        /// unsigned short
        public ushort count;

        //For some reason, the code works with 12 Int16s, but not an array of 12 Int16s. Weird...
        /// short[12]
        //[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12)]
        //public Int16[] povs;

        
        public Int16 povs0;
        public Int16 povs1;
        public Int16 povs2;
        public Int16 povs3;
        public Int16 povs4;
        public Int16 povs5;
        public Int16 povs6;
        public Int16 povs7;
        public Int16 povs8;
        public Int16 povs9;
        public Int16 povs10;
        public Int16 povs11;
        
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

    public partial class HAL
    {
        //public const uint dio_kNumSystems;//Find Value
        public const uint solenoid_kNumDO7_0Elements = 8;
        //public const uint interrupt_kNumSystems ;//Find value
        public const uint kSystemClockTicksPerMicrosecond = 40;


        internal static Assembly HALAssembly;

        private static bool s_isSimulation = false;

        /// <summary>
        /// Gets or Sets if the code is in simulation mode
        /// </summary>
        public static bool IsSimulation
        {
            get
            {
                return s_isSimulation;
            }
            set { s_isSimulation = value; }
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
            if (IsSimulation)
            {
                HALAssembly = Assembly.LoadFrom("/home/lvuser/mono/HAL-Simulation.dll");
            }
            else
            {
                HALAssembly = Assembly.LoadFrom("/home/lvuser/mono/HAL-RoboRIO.dll");
            }

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
            HALControlWord temp = new HALControlWord();
            HALGetControlWord(ref temp);
            return temp;
        }

        public static short GetAxesData(int axis, ref HALJoystickAxes axes)
        {
            switch (axis)
            {
                case 0:
                    return axes.axes0;
                case 1:
                    return axes.axes1;
                case 2:
                    return axes.axes2;
                case 3:
                    return axes.axes3;
                case 4:
                    return axes.axes4;
                case 5:
                    return axes.axes5;
                case 6:
                    return axes.axes6;
                case 7:
                    return axes.axes7;
                case 8:
                    return axes.axes8;
                case 9:
                    return axes.axes9;
                case 10:
                    return axes.axes10;
                case 11:
                    return axes.axes11;
                default:
                    return 0;
            }
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
