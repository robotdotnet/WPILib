using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//These are all of the structures used by HAL-RoboRIO and HAL-Simulation. 
//Changes to these will always require a rebuild of the local HALs, which we want to avoid doing.
//So please do not change these without explicit reasoning.
namespace HAL_Base
{

#region HAL
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

        /// short[] hack
        public HALJoystickAxesArray axes;
    }
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickPOVs
    {
        /// unsigned short
        public ushort count;

        /// short[] hack
        public HALJoystickPOVArray povs;
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
                switch (i)
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

#endregion

#region Accelerometer

    public enum AccelerometerRange
    {
        /// kRange_2G -> 0
        Range_2G = 0,

        /// kRange_4G -> 1
        Range_4G = 1,

        /// kRange_8G -> 2
        Range_8G = 2,
    }

#endregion 

#region Analog
    public enum AnalogTriggerType
    {
        /// kInWindow -> 0
        InWindow = 0,

        /// kState -> 1
        State = 1,

        /// kRisingPulse -> 2
        RisingPulse = 2,

        /// kFallingPulse -> 3
        FallingPulse = 3,
    }
#endregion

#region CANTalonSRX
    public enum CTR_Code
    {
        CTR_OKAY,

        CTR_RxTimeout,

        CTR_TxTimeout,

        CTR_InvalidParamValue,

        CTR_UnexpectedArbId,

        CTR_TxFailed,

        CTR_SigNotUpdated,
    }
#endregion

#region Digital
    public enum Mode
    {
        /// kTwoPulse -> 0
        TwoPulse = 0,

        /// kSemiperiod -> 1
        Semiperiod = 1,

        /// kPulseLength -> 2
        PulseLength = 2,

        /// kExternalDirection -> 3
        ExternalDirection = 3,
    }
#endregion
}
