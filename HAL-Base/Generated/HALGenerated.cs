//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HAL
    {
        static HAL()
        {
            Initialize();
        }

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HALAssembly.GetType(q.ToList()[0].FullName);
            GetPort = (GetPortDelegate)Delegate.CreateDelegate(typeof(GetPortDelegate), type.GetMethod("getPort"));
            GetPortWithModule = (GetPortWithModuleDelegate)Delegate.CreateDelegate(typeof(GetPortWithModuleDelegate), type.GetMethod("getPortWithModule"));
            GetHALErrorMessage = (GetHALErrorMessageDelegate)Delegate.CreateDelegate(typeof(GetHALErrorMessageDelegate), type.GetMethod("getHALErrorMessage"));
            GetFPGAVersion = (GetFPGAVersionDelegate)Delegate.CreateDelegate(typeof(GetFPGAVersionDelegate), type.GetMethod("getFPGAVersion"));
            GetFPGARevision = (GetFPGARevisionDelegate)Delegate.CreateDelegate(typeof(GetFPGARevisionDelegate), type.GetMethod("getFPGARevision"));
            GetFPGATime = (GetFPGATimeDelegate)Delegate.CreateDelegate(typeof(GetFPGATimeDelegate), type.GetMethod("getFPGATime"));
            GetFPGAButton = (GetFPGAButtonDelegate)Delegate.CreateDelegate(typeof(GetFPGAButtonDelegate), type.GetMethod("getFPGAButton"));
            HALSetErrorData = (HALSetErrorDataDelegate)Delegate.CreateDelegate(typeof(HALSetErrorDataDelegate), type.GetMethod("HALSetErrorData"));
            GetControlWord = (HALGetControlWordDelegate)Delegate.CreateDelegate(typeof(HALGetControlWordDelegate), type.GetMethod("HALGetControlWord"));
            HALGetAllianceStation = (HALGetAllianceStationDelegate)Delegate.CreateDelegate(typeof(HALGetAllianceStationDelegate), type.GetMethod("HALGetAllianceStation"));
            HALGetJoystickAxes = (HALGetJoystickAxesDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickAxesDelegate), type.GetMethod("HALGetJoystickAxes"));
            HALGetJoystickPOVs = (HALGetJoystickPOVsDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickPOVsDelegate), type.GetMethod("HALGetJoystickPOVs"));
            HALGetJoystickButtons = (HALGetJoystickButtonsDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickButtonsDelegate), type.GetMethod("HALGetJoystickButtons"));
            HALGetJoystickDescriptor = (HALGetJoystickDescriptorDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickDescriptorDelegate), type.GetMethod("HALGetJoystickDescriptor"));

            HALGetJoystickIsXbox = (HALGetJoystickIsXboxDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickIsXboxDelegate), type.GetMethod("HALGetJoystickIsXbox"));
            HALGetJoystickType = (HALGetJoystickTypeDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickTypeDelegate), type.GetMethod("HALGetJoystickType"));
            HALGetJoystickName = (HALGetJoystickNameDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickNameDelegate), type.GetMethod("HALGetJoystickName"));
            HALGetJoystickAxisType = (HALGetJoystickAxisTypeDelegate)Delegate.CreateDelegate(typeof(HALGetJoystickAxisTypeDelegate), type.GetMethod("HALGetJoystickAxisType"));

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
        }

        public delegate IntPtr GetPortDelegate(byte pin);
        public static GetPortDelegate GetPort;

        public delegate IntPtr GetPortWithModuleDelegate(byte module, byte pin);
        public static GetPortWithModuleDelegate GetPortWithModule;

        private delegate IntPtr GetHALErrorMessageDelegate(int code);
        private static GetHALErrorMessageDelegate GetHALErrorMessage;

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

        public delegate HALControlWord HALGetControlWordDelegate();
        public static HALGetControlWordDelegate GetControlWord;

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

        public delegate int HALGetJoystickIsXboxDelegate(byte joystickNum);
        public static HALGetJoystickIsXboxDelegate HALGetJoystickIsXbox;

        public delegate int HALGetJoystickTypeDelegate(byte joystickNum);
        public static HALGetJoystickTypeDelegate HALGetJoystickType;

        private delegate IntPtr HALGetJoystickNameDelegate(byte joystickNum);
        private static HALGetJoystickNameDelegate HALGetJoystickName;

        public delegate int HALGetJoystickAxisTypeDelegate(byte joystickNum, byte axis);
        public static HALGetJoystickAxisTypeDelegate HALGetJoystickAxisType;

        public delegate int HALSetJoystickOutputsDelegate(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);
        public static HALSetJoystickOutputsDelegate HALSetJoystickOutputs;

        public delegate int HALGetMatchTimeDelegate(ref float matchTime);
        public static HALGetMatchTimeDelegate HALGetMatchTime;

        public delegate void HALSetNewDataSemDelegate(IntPtr sem);
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
    }
}
