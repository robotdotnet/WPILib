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

        /// <summary>
        /// This function is called in order to setup the delegates for the HAL functions.
        /// </summary>
        internal static void SetupDelegates()
        {
            if (HALAssembly == null)
            {
                throw new Exception(
                    "HAL Assembly was not set. This is probalby an error in the WPILib. If you see contact the robotdotnet team.");
            }
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;

            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);

            GetPort = (GetPortDelegate)Delegate.CreateDelegate(typeof(GetPortDelegate), type.GetMethod("GetPort"));

            GetPortWithModule = (GetPortWithModuleDelegate)Delegate.CreateDelegate(typeof(GetPortWithModuleDelegate), type.GetMethod("GetPortWithModule"));

            GetHALErrorMessage = (GetHALErrorMessageDelegate)Delegate.CreateDelegate(typeof(GetHALErrorMessageDelegate), type.GetMethod("GetHALErrorMessage"));

            GetFPGAVersion = (GetFPGAVersionDelegate)Delegate.CreateDelegate(typeof(GetFPGAVersionDelegate), type.GetMethod("GetFPGAVersion"));

            GetFPGARevision = (GetFPGARevisionDelegate)Delegate.CreateDelegate(typeof(GetFPGARevisionDelegate), type.GetMethod("GetFPGARevision"));

            GetFPGATime = (GetFPGATimeDelegate)Delegate.CreateDelegate(typeof(GetFPGATimeDelegate), type.GetMethod("GetFPGATime"));

            GetFPGAButton = (GetFPGAButtonDelegate)Delegate.CreateDelegate(typeof(GetFPGAButtonDelegate), type.GetMethod("GetFPGAButton"));

            SetErrorDataHAL = (SetErrorDataDelegate)Delegate.CreateDelegate(typeof(SetErrorDataDelegate), type.GetMethod("SetErrorData"));

            GetControlWordHAL = (GetControlWordDelegate)Delegate.CreateDelegate(typeof(GetControlWordDelegate), type.GetMethod("GetControlWord"));

            GetAllianceStation = (GetAllianceStationDelegate)Delegate.CreateDelegate(typeof(GetAllianceStationDelegate), type.GetMethod("GetAllianceStation"));

            GetJoystickAxes = (GetJoystickAxesDelegate)Delegate.CreateDelegate(typeof(GetJoystickAxesDelegate), type.GetMethod("GetJoystickAxes"));

            GetJoystickPOVs = (GetJoystickPOVsDelegate)Delegate.CreateDelegate(typeof(GetJoystickPOVsDelegate), type.GetMethod("GetJoystickPOVs"));

            GetJoystickButtons = (GetJoystickButtonsDelegate)Delegate.CreateDelegate(typeof(GetJoystickButtonsDelegate), type.GetMethod("GetJoystickButtons"));

            GetJoystickDescriptor = (GetJoystickDescriptorDelegate)Delegate.CreateDelegate(typeof(GetJoystickDescriptorDelegate), type.GetMethod("GetJoystickDescriptor"));

            SetJoystickOutputs = (SetJoystickOutputsDelegate)Delegate.CreateDelegate(typeof(SetJoystickOutputsDelegate), type.GetMethod("SetJoystickOutputs"));

            GetMatchTime = (GetMatchTimeDelegate)Delegate.CreateDelegate(typeof(GetMatchTimeDelegate), type.GetMethod("GetMatchTime"));

            SetNewDataSem = (SetNewDataSemDelegate)Delegate.CreateDelegate(typeof(SetNewDataSemDelegate), type.GetMethod("SetNewDataSem"));

            GetSystemActive = (GetSystemActiveDelegate)Delegate.CreateDelegate(typeof(GetSystemActiveDelegate), type.GetMethod("GetSystemActive"));

            GetBrownedOut = (GetBrownedOutDelegate)Delegate.CreateDelegate(typeof(GetBrownedOutDelegate), type.GetMethod("GetBrownedOut"));

            HALInitialize = (HALInitializeDelegate)Delegate.CreateDelegate(typeof(HALInitializeDelegate), type.GetMethod("HALInitialize"));

            NetworkCommunicationObserveUserProgramStarting = (NetworkCommunicationObserveUserProgramStartingDelegate)Delegate.CreateDelegate(typeof(NetworkCommunicationObserveUserProgramStartingDelegate), type.GetMethod("NetworkCommunicationObserveUserProgramStarting"));

            NetworkCommunicationObserveUserProgramDisabled = (NetworkCommunicationObserveUserProgramDisabledDelegate)Delegate.CreateDelegate(typeof(NetworkCommunicationObserveUserProgramDisabledDelegate), type.GetMethod("NetworkCommunicationObserveUserProgramDisabled"));

            NetworkCommunicationObserveUserProgramAutonomous = (NetworkCommunicationObserveUserProgramAutonomousDelegate)Delegate.CreateDelegate(typeof(NetworkCommunicationObserveUserProgramAutonomousDelegate), type.GetMethod("NetworkCommunicationObserveUserProgramAutonomous"));

            NetworkCommunicationObserveUserProgramTeleop = (NetworkCommunicationObserveUserProgramTeleopDelegate)Delegate.CreateDelegate(typeof(NetworkCommunicationObserveUserProgramTeleopDelegate), type.GetMethod("NetworkCommunicationObserveUserProgramTeleop"));

            NetworkCommunicationObserveUserProgramTest = (NetworkCommunicationObserveUserProgramTestDelegate)Delegate.CreateDelegate(typeof(NetworkCommunicationObserveUserProgramTestDelegate), type.GetMethod("NetworkCommunicationObserveUserProgramTest"));

            HALReport = (HALReportDelegate)Delegate.CreateDelegate(typeof(HALReportDelegate), type.GetMethod("HALReport"));

            NumericArrayResize = (NumericArrayResizeDelegate)Delegate.CreateDelegate(typeof(NumericArrayResizeDelegate), type.GetMethod("NumericArrayResize"));

            RTSetCleanupProc = (RTSetCleanupProcDelegate)Delegate.CreateDelegate(typeof(RTSetCleanupProcDelegate), type.GetMethod("RTSetCleanupProc"));

            EDVR_CreateReference = (EDVR_CreateReferenceDelegate)Delegate.CreateDelegate(typeof(EDVR_CreateReferenceDelegate), type.GetMethod("EDVR_CreateReference"));

            Occur = (OccurDelegate)Delegate.CreateDelegate(typeof(OccurDelegate), type.GetMethod("Occur"));
        }

        public delegate System.IntPtr GetPortDelegate(byte pin);
        public static GetPortDelegate GetPort;

        public delegate System.IntPtr GetPortWithModuleDelegate(byte module, byte pin);
        public static GetPortWithModuleDelegate GetPortWithModule;

        public delegate string GetHALErrorMessageDelegate(int code);
        public static GetHALErrorMessageDelegate GetHALErrorMessage;

        public delegate ushort GetFPGAVersionDelegate(ref int status);
        public static GetFPGAVersionDelegate GetFPGAVersion;

        public delegate uint GetFPGARevisionDelegate(ref int status);
        public static GetFPGARevisionDelegate GetFPGARevision;

        public delegate uint GetFPGATimeDelegate(ref int status);
        public static GetFPGATimeDelegate GetFPGATime;

        public delegate bool GetFPGAButtonDelegate(ref int status);
        public static GetFPGAButtonDelegate GetFPGAButton;

        public delegate int SetErrorDataDelegate([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string errors, int errorsLength, int waitMs);
        private static SetErrorDataDelegate SetErrorDataHAL;

        public delegate int GetControlWordDelegate(ref uint data);
        private static GetControlWordDelegate GetControlWordHAL;

        public delegate int GetAllianceStationDelegate(ref HALAllianceStationID allianceStation);
        public static GetAllianceStationDelegate GetAllianceStation;

        public delegate int GetJoystickAxesDelegate(byte joystickNum, ref HALJoystickAxes axes);
        public static GetJoystickAxesDelegate GetJoystickAxes;

        public delegate int GetJoystickPOVsDelegate(byte joystickNum, ref HALJoystickPOVs povs);
        public static GetJoystickPOVsDelegate GetJoystickPOVs;

        public delegate int GetJoystickButtonsDelegate(byte joystickNum, ref HALJoystickButtons buttons);
        public static GetJoystickButtonsDelegate GetJoystickButtons;

        public delegate int GetJoystickDescriptorDelegate(byte joystickNum, ref HALJoystickDescriptor desc);
        public static GetJoystickDescriptorDelegate GetJoystickDescriptor;

        public delegate int SetJoystickOutputsDelegate(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);
        public static SetJoystickOutputsDelegate SetJoystickOutputs;

        public delegate int GetMatchTimeDelegate(ref float matchTime);
        public static GetMatchTimeDelegate GetMatchTime;

        public delegate void SetNewDataSemDelegate(System.IntPtr sem);
        public static SetNewDataSemDelegate SetNewDataSem;

        public delegate bool GetSystemActiveDelegate(ref int status);
        public static GetSystemActiveDelegate GetSystemActive;

        public delegate bool GetBrownedOutDelegate(ref int status);
        public static GetBrownedOutDelegate GetBrownedOut;

        public delegate int HALInitializeDelegate(int mode = 0);
        public static HALInitializeDelegate HALInitialize;

        public delegate void NetworkCommunicationObserveUserProgramStartingDelegate();
        public static NetworkCommunicationObserveUserProgramStartingDelegate NetworkCommunicationObserveUserProgramStarting;

        public delegate void NetworkCommunicationObserveUserProgramDisabledDelegate();
        public static NetworkCommunicationObserveUserProgramDisabledDelegate NetworkCommunicationObserveUserProgramDisabled;

        public delegate void NetworkCommunicationObserveUserProgramAutonomousDelegate();
        public static NetworkCommunicationObserveUserProgramAutonomousDelegate NetworkCommunicationObserveUserProgramAutonomous;

        public delegate void NetworkCommunicationObserveUserProgramTeleopDelegate();
        public static NetworkCommunicationObserveUserProgramTeleopDelegate NetworkCommunicationObserveUserProgramTeleop;

        public delegate void NetworkCommunicationObserveUserProgramTestDelegate();
        public static NetworkCommunicationObserveUserProgramTestDelegate NetworkCommunicationObserveUserProgramTest;

        public delegate uint HALReportDelegate(byte resource, byte instanceNumber, byte context = 0, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string feature = null);
        public static HALReportDelegate HALReport;

        public delegate void NumericArrayResizeDelegate();
        public static NumericArrayResizeDelegate NumericArrayResize;

        public delegate void RTSetCleanupProcDelegate();
        public static RTSetCleanupProcDelegate RTSetCleanupProc;

        public delegate void EDVR_CreateReferenceDelegate();
        public static EDVR_CreateReferenceDelegate EDVR_CreateReference;

        public delegate void OccurDelegate();
        public static OccurDelegate Occur;


        public static int SetErrorData(string errors, int waitMs)
        {
            return SetErrorDataHAL(errors, errors.Length, waitMs);
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
                HALAssembly = Assembly.LoadFrom("/home/lvuser/robotdotnet/HAL-RoboRIO.dll");
            }

            SetupDelegates();
            HALAccelerometer.SetupDelegate();
            HALAnalog.SetupDelegate();
            HALCANTalon.SetupDelegate();
            HALCompressor.SetupDelegate();
            HALDigital.SetupDelegates();
            HALInterrupts.SetupDelegates();
            HALNotifier.SetupDelegates();
            HALPDP.SetupDelegates();
            HALPower.SetupDelegates();
            HALSemaphore.SetupDelegates();
            HALSerialPort.SetupDelegates();
            HALSolenoid.SetupDelegates();
            HALUtilities.SetupDelegates();


            var rv = HALInitialize();
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
            uint temp = 0;
            GetControlWordHAL(ref temp);
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
