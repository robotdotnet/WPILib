//File automatically generated using robotdotnet-tools. Please do not modify.
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HAL
    {
        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            GetPort = (GetPortDelegate)Delegate.CreateDelegate(typeof(GetPortDelegate), type.GetMethod("getPort"));
            GetPortWithModule = (GetPortWithModuleDelegate)Delegate.CreateDelegate(typeof(GetPortWithModuleDelegate), type.GetMethod("getPortWithModule"));
            GetHALErrorMessage = (GetHALErrorMessageDelegate)Delegate.CreateDelegate(typeof(GetHALErrorMessageDelegate), type.GetMethod("getHALErrorMessage"));
            GetFPGAVersion = (GetFPGAVersionDelegate)Delegate.CreateDelegate(typeof(GetFPGAVersionDelegate), type.GetMethod("getFPGAVersion"));
            GetFPGARevision = (GetFPGARevisionDelegate)Delegate.CreateDelegate(typeof(GetFPGARevisionDelegate), type.GetMethod("getFPGARevision"));
            GetFPGATime = (GetFPGATimeDelegate)Delegate.CreateDelegate(typeof(GetFPGATimeDelegate), type.GetMethod("getFPGATime"));
            GetFPGAButton = (GetFPGAButtonDelegate)Delegate.CreateDelegate(typeof(GetFPGAButtonDelegate), type.GetMethod("getFPGAButton"));
            HALSetErrorData = (HALSetErrorDataDelegate)Delegate.CreateDelegate(typeof(HALSetErrorDataDelegate), type.GetMethod("HALSetErrorData"));
            HALGetControlWord = (HALGetControlWordDelegate)Delegate.CreateDelegate(typeof(HALGetControlWordDelegate), type.GetMethod("HALGetControlWord"));
            HALGetAllianceStation = (HALGetAllianceStationDelegate)Delegate.CreateDelegate(typeof(HALGetAllianceStationDelegate), type.GetMethod("HALGetAllianceStation"));
            HALGetJoystickAxes = (HALGetJoystickAxesDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickAxesDelegate), type.GetMethod("HALGetJoystickAxes"));
            HALGetJoystickPOVs = (HALGetJoystickPOVsDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickPOVsDelegate), type.GetMethod("HALGetJoystickPOVs"));
            HALGetJoystickButtons = (HALGetJoystickButtonsDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickButtonsDelegate), type.GetMethod("HALGetJoystickButtons"));
            HALGetJoystickDescriptor = (HALGetJoystickDescriptorDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickDescriptorDelegate), type.GetMethod("HALGetJoystickDescriptor"));
            HALSetJoystickOutputs = (HALSetJoystickOutputsDelegate)Delegate.CreateDelegate(typeof(HALSetJoystickOutputsDelegate), type.GetMethod("HALSetJoystickOutputs"));
            HALGetMatchTime = (HALGetMatchTimeDelegate)Delegate.CreateDelegate(typeof(HALGetMatchTimeDelegate), type.GetMethod("HALGetMatchTime"));
            HALSetNewDataSem = (HALSetNewDataSemDelegate)Delegate.CreateDelegate(typeof(HALSetNewDataSemDelegate), type.GetMethod("HALSetNewDataSem"));
            HALGetSystemActive = (HALGetSystemActiveDelegate)Delegate.CreateDelegate(typeof(HALGetSystemActiveDelegate), type.GetMethod("HALGetSystemActive"));
            HALGetBrownedOut = (HALGetBrownedOutDelegate)Delegate.CreateDelegate(typeof(HALGetBrownedOutDelegate), type.GetMethod("HALGetBrownedOut"));
            HALInitialize = (HALInitializeDelegate)Delegate.CreateDelegate(typeof(HALInitializeDelegate), type.GetMethod("HALInitialize"));
            HALNetworkCommunicationObserveUserProgramStarting = (HALNetworkCommunicationObserveUserProgramStartingDelegate)Delegate.CreateDelegate(typeof(HALNetworkCommunicationObserveUserProgramStartingDelegate), type.GetMethod("HALNetworkCommunicationObserveUserProgramStarting"));
            HALNetworkCommunicationObserveUserProgramDisabled = (HALNetworkCommunicationObserveUserProgramDisabledDelegate)Delegate.CreateDelegate(typeof(HALNetworkCommunicationObserveUserProgramDisabledDelegate), type.GetMethod("HALNetworkCommunicationObserveUserProgramDisabled"));
            HALNetworkCommunicationObserveUserProgramAutonomous = (HALNetworkCommunicationObserveUserProgramAutonomousDelegate)Delegate.CreateDelegate(typeof(HALNetworkCommunicationObserveUserProgramAutonomousDelegate), type.GetMethod("HALNetworkCommunicationObserveUserProgramAutonomous"));
            HALNetworkCommunicationObserveUserProgramTeleop = (HALNetworkCommunicationObserveUserProgramTeleopDelegate)Delegate.CreateDelegate(typeof(HALNetworkCommunicationObserveUserProgramTeleopDelegate), type.GetMethod("HALNetworkCommunicationObserveUserProgramTeleop"));
            HALNetworkCommunicationObserveUserProgramTest = (HALNetworkCommunicationObserveUserProgramTestDelegate)Delegate.CreateDelegate(typeof(HALNetworkCommunicationObserveUserProgramTestDelegate), type.GetMethod("HALNetworkCommunicationObserveUserProgramTest"));
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

        public delegate System.IntPtr GetHALErrorMessageDelegate(int code);
        public static GetHALErrorMessageDelegate GetHALErrorMessage;

        public delegate ushort GetFPGAVersionDelegate(ref int status);
        public static GetFPGAVersionDelegate GetFPGAVersion;

        public delegate uint GetFPGARevisionDelegate(ref int status);
        public static GetFPGARevisionDelegate GetFPGARevision;

        public delegate uint GetFPGATimeDelegate(ref int status);
        public static GetFPGATimeDelegate GetFPGATime;

        public delegate bool GetFPGAButtonDelegate(ref int status);
        public static GetFPGAButtonDelegate GetFPGAButton;

        public delegate int HALSetErrorDataDelegate(string errors, int errorsLength, int wait_ms);
        public static HALSetErrorDataDelegate HALSetErrorData;

        public delegate int HALGetControlWordDelegate(ref HALControlWord data);
        public static HALGetControlWordDelegate HALGetControlWord;

        public delegate int HALGetAllianceStationDelegate(ref HALAllianceStationID allianceStation);
        public static HALGetAllianceStationDelegate HALGetAllianceStation;

        public delegate int HALGetJoystickAxesDelegate(byte joystickNum, ref HALJoystickAxes axes);
        public static HALGetJoystickAxesDelegate HALGetJoystickAxes;

        public delegate int HALGetJoystickPOVsDelegate(byte joystickNum, ref HALJoystickPOVs povs);
        public static HALGetJoystickPOVsDelegate HALGetJoystickPOVs;

        public delegate int HALGetJoystickButtonsDelegate(byte joystickNum, ref HALJoystickButtons buttons);
        public static HALGetJoystickButtonsDelegate HALGetJoystickButtons;

        public delegate int HALGetJoystickDescriptorDelegate(byte joystickNum, ref HALJoystickDescriptor desc);
        public static HALGetJoystickDescriptorDelegate HALGetJoystickDescriptor;

        public delegate int HALSetJoystickOutputsDelegate(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);
        public static HALSetJoystickOutputsDelegate HALSetJoystickOutputs;

        public delegate int HALGetMatchTimeDelegate(ref float matchTime);
        public static HALGetMatchTimeDelegate HALGetMatchTime;

        public delegate void HALSetNewDataSemDelegate(System.IntPtr sem);
        public static HALSetNewDataSemDelegate HALSetNewDataSem;

        public delegate bool HALGetSystemActiveDelegate(ref int status);
        public static HALGetSystemActiveDelegate HALGetSystemActive;

        public delegate bool HALGetBrownedOutDelegate(ref int status);
        public static HALGetBrownedOutDelegate HALGetBrownedOut;

        public delegate int HALInitializeDelegate(int mode);
        public static HALInitializeDelegate HALInitialize;

        public delegate void HALNetworkCommunicationObserveUserProgramStartingDelegate();
        public static HALNetworkCommunicationObserveUserProgramStartingDelegate HALNetworkCommunicationObserveUserProgramStarting;

        public delegate void HALNetworkCommunicationObserveUserProgramDisabledDelegate();
        public static HALNetworkCommunicationObserveUserProgramDisabledDelegate HALNetworkCommunicationObserveUserProgramDisabled;

        public delegate void HALNetworkCommunicationObserveUserProgramAutonomousDelegate();
        public static HALNetworkCommunicationObserveUserProgramAutonomousDelegate HALNetworkCommunicationObserveUserProgramAutonomous;

        public delegate void HALNetworkCommunicationObserveUserProgramTeleopDelegate();
        public static HALNetworkCommunicationObserveUserProgramTeleopDelegate HALNetworkCommunicationObserveUserProgramTeleop;

        public delegate void HALNetworkCommunicationObserveUserProgramTestDelegate();
        public static HALNetworkCommunicationObserveUserProgramTestDelegate HALNetworkCommunicationObserveUserProgramTest;

        public delegate uint HALReportDelegate(byte resource, byte instanceNumber, byte context, string feature = null);
        public static HALReportDelegate HALReport;

        public delegate void NumericArrayResizeDelegate();
        public static NumericArrayResizeDelegate NumericArrayResize;

        public delegate void RTSetCleanupProcDelegate();
        public static RTSetCleanupProcDelegate RTSetCleanupProc;

        public delegate void EDVR_CreateReferenceDelegate();
        public static EDVR_CreateReferenceDelegate EDVR_CreateReference;

        public delegate void OccurDelegate();
        public static OccurDelegate Occur;
    }
}
