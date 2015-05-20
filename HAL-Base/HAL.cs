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

        public HALJoystickAxesArray axes;
        /*
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
         * */
         
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
        public HALJoystickPOVArray povs;
        
            /*
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
             * */
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickAxesArray
    {
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

        public Int16 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return axes0;
                    case 1:
                        return axes1;
                    case 2:
                        return axes2;
                    case 3:
                        return axes3;
                    case 4:
                        return axes4;
                    case 5:
                        return axes5;
                    case 6:
                        return axes6;
                    case 7:
                        return axes7;
                    case 8:
                        return axes8;
                    case 9:
                        return axes9;
                    case 10:
                        return axes10;
                    case 11:
                        return axes11;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }

            set
            {
                switch(i)
                {
                    case 0:
                        axes0 = value;
                        return;
                    case 1:
                        axes1 = value;
                        return;
                    case 2:
                        axes2 = value;
                        return;
                    case 3:
                        axes3 = value;
                        return;
                    case 4:
                        axes4 = value;
                        return;
                    case 5:
                        axes5 = value;
                        return;
                    case 6:
                        axes6 = value;
                        return;
                    case 7:
                        axes7 = value;
                        return;
                    case 8:
                        axes8 = value;
                        return;
                    case 9:
                        axes9 = value;
                        return;
                    case 10:
                        axes10 = value;
                        return;
                    case 11:
                        axes11 = value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();

                }
            }
            
        }

    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickPOVArray
    {
        private Int16 pov0;
        private Int16 pov1;
        private Int16 pov2;
        private Int16 pov3;
        private Int16 pov4;
        private Int16 pov5;
        private Int16 pov6;
        private Int16 pov7;
        private Int16 pov8;
        private Int16 pov9;
        private Int16 pov10;
        private Int16 pov11;

        public Int16 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return pov0;
                    case 1:
                        return pov1;
                    case 2:
                        return pov2;
                    case 3:
                        return pov3;
                    case 4:
                        return pov4;
                    case 5:
                        return pov5;
                    case 6:
                        return pov6;
                    case 7:
                        return pov7;
                    case 8:
                        return pov8;
                    case 9:
                        return pov9;
                    case 10:
                        return pov10;
                    case 11:
                        return pov11;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }

            set
            {
                switch (i)
                {
                    case 0:
                        pov0 = value;
                        return;
                    case 1:
                        pov1 = value;
                        return;
                    case 2:
                        pov2 = value;
                        return;
                    case 3:
                        pov3 = value;
                        return;
                    case 4:
                        pov4 = value;
                        return;
                    case 5:
                        pov5 = value;
                        return;
                    case 6:
                        pov6 = value;
                        return;
                    case 7:
                        pov7 = value;
                        return;
                    case 8:
                        pov8 = value;
                        return;
                    case 9:
                        pov9 = value;
                        return;
                    case 10:
                        pov10 = value;
                        return;
                    case 11:
                        pov11 = value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();

                }
            }

        }

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
