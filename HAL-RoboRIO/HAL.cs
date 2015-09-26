//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    internal class HAL
    {
        private static string ExtractLibrary()
        {
            string inputName = "";
            string outputName = "";
            inputName = "HAL_RoboRIO.libHALAthena_shared.so";
            outputName = "/home/lvuser/libHALAthena_shared2.so";
            byte[] bytes = null;
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(inputName))
            {
                if (s == null || s.Length == 0)
                    return null;
                bytes = new byte[(int)s.Length];
                s.Read(bytes, 0, (int)s.Length);

                if (File.Exists(outputName))
                    File.Delete(outputName);
                File.WriteAllBytes(outputName, bytes);
            }
            return outputName;

        }

        private static bool libraryLoaded = false;
        private static IntPtr library;

        public static void InitializeImpl()
        {
            if (!libraryLoaded)
            {
                try
                {
                    //Force OS, since we know we are on Linux and the RIO if this is getting called
                    ILibraryLoader loader = new RoboRIOLibraryLoader();

                    //Extract our library
                    string loadedPath = ExtractLibrary();
                    if (string.IsNullOrEmpty(loadedPath))
                    {
                        //If fail to load, throw exception
                        throw new FileNotFoundException($"Library file could not be found in the resorces. Please contact RobotDotNet for support for this issue");
                    }
                    library = loader.LoadLibrary(loadedPath);
                    if (library == IntPtr.Zero)
                    {
                        //If library could not be loaded
                        throw new BadImageFormatException($"Library file {loadedPath} could not be loaded successfully.");
                    }
                    Initialize(library, loader);
                    HALAccelerometer.Initialize(library, loader);
                    HALAnalog.Initialize(library, loader);
                    HALCAN.Initialize(library, loader);
                    HALCANTalonSRX.Initialize(library, loader);
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
            HAL_Base.HAL.GetPort = (HAL_Base.HAL.GetPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPort"), typeof(HAL_Base.HAL.GetPortDelegate));

            HAL_Base.HAL.GetPortWithModule = (HAL_Base.HAL.GetPortWithModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPortWithModule"), typeof(HAL_Base.HAL.GetPortWithModuleDelegate));

            HAL_Base.HAL.GetHALErrorMessage = (HAL_Base.HAL.GetHALErrorMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getHALErrorMessage"), typeof(HAL_Base.HAL.GetHALErrorMessageDelegate));

            HAL_Base.HAL.GetFPGAVersion = (HAL_Base.HAL.GetFPGAVersionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGAVersion"), typeof(HAL_Base.HAL.GetFPGAVersionDelegate));

            HAL_Base.HAL.GetFPGARevision = (HAL_Base.HAL.GetFPGARevisionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGARevision"), typeof(HAL_Base.HAL.GetFPGARevisionDelegate));

            HAL_Base.HAL.GetFPGATime = (HAL_Base.HAL.GetFPGATimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGATime"), typeof(HAL_Base.HAL.GetFPGATimeDelegate));

            HAL_Base.HAL.GetFPGAButton = (HAL_Base.HAL.GetFPGAButtonDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGAButton"), typeof(HAL_Base.HAL.GetFPGAButtonDelegate));

            HAL_Base.HAL.HALSetErrorData = (HAL_Base.HAL.HALSetErrorDataDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetErrorData"), typeof(HAL_Base.HAL.HALSetErrorDataDelegate));

            HAL_Base.HAL.GetControlWord = HALGetControlWord;

            NativeHALGetControlWord = (NativeHALGetControlWordDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetControlWord"), typeof(NativeHALGetControlWordDelegate));

            HAL_Base.HAL.HALGetAllianceStation = (HAL_Base.HAL.HALGetAllianceStationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetAllianceStation"), typeof(HAL_Base.HAL.HALGetAllianceStationDelegate));

            HAL_Base.HAL.HALGetJoystickAxes = (HAL_Base.HAL.HALGetJoystickAxesDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickAxes"), typeof(HAL_Base.HAL.HALGetJoystickAxesDelegate));

            HAL_Base.HAL.HALGetJoystickPOVs = (HAL_Base.HAL.HALGetJoystickPOVsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickPOVs"), typeof(HAL_Base.HAL.HALGetJoystickPOVsDelegate));

            HAL_Base.HAL.HALGetJoystickButtons = (HAL_Base.HAL.HALGetJoystickButtonsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickButtons"), typeof(HAL_Base.HAL.HALGetJoystickButtonsDelegate));

            HAL_Base.HAL.HALGetJoystickDescriptor = (HAL_Base.HAL.HALGetJoystickDescriptorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickDescriptor"), typeof(HAL_Base.HAL.HALGetJoystickDescriptorDelegate));
            //TODO: FIX THIS
            //HAL_Base.HAL.HALGetJoystickIsXbox = (HAL_Base.HAL.HALGetJoystickIsXboxDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickIsXbox"), typeof(HAL_Base.HAL.HALGetJoystickIsXboxDelegate));

            //HAL_Base.HAL.HALGetJoystickType = (HAL_Base.HAL.HALGetJoystickTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickType"), typeof(HAL_Base.HAL.HALGetJoystickTypeDelegate));

            //HAL_Base.HAL.HALGetJoystickName = (HAL_Base.HAL.HALGetJoystickNameDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickName"), typeof(HAL_Base.HAL.HALGetJoystickNameDelegate));

            //HAL_Base.HAL.HALGetJoystickAxisType = (HAL_Base.HAL.HALGetJoystickAxisTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickAxisType"), typeof(HAL_Base.HAL.HALGetJoystickAxisTypeDelegate));

            HAL_Base.HAL.HALSetJoystickOutputs = (HAL_Base.HAL.HALSetJoystickOutputsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetJoystickOutputs"), typeof(HAL_Base.HAL.HALSetJoystickOutputsDelegate));

            HAL_Base.HAL.HALGetMatchTime = (HAL_Base.HAL.HALGetMatchTimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetMatchTime"), typeof(HAL_Base.HAL.HALGetMatchTimeDelegate));

            HAL_Base.HAL.HALSetNewDataSem = (HAL_Base.HAL.HALSetNewDataSemDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetNewDataSem"), typeof(HAL_Base.HAL.HALSetNewDataSemDelegate));

            HAL_Base.HAL.HALGetSystemActive = (HAL_Base.HAL.HALGetSystemActiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetSystemActive"), typeof(HAL_Base.HAL.HALGetSystemActiveDelegate));

            HAL_Base.HAL.HALGetBrownedOut = (HAL_Base.HAL.HALGetBrownedOutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetBrownedOut"), typeof(HAL_Base.HAL.HALGetBrownedOutDelegate));

            HAL_Base.HAL.HALInitialize = (HAL_Base.HAL.HALInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALInitialize"), typeof(HAL_Base.HAL.HALInitializeDelegate));

            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramStarting = (HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramStartingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramStarting"), typeof(HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramStartingDelegate));

            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramDisabled = (HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramDisabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramDisabled"), typeof(HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramDisabledDelegate));

            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramAutonomous = (HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramAutonomousDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramAutonomous"), typeof(HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramAutonomousDelegate));

            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramTeleop = (HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramTeleopDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramTeleop"), typeof(HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramTeleopDelegate));

            HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramTest = (HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramTestDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramTest"), typeof(HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramTestDelegate));

            HAL_Base.HAL.HALReport = (HAL_Base.HAL.HALReportDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALReport"), typeof(HAL_Base.HAL.HALReportDelegate));

            //HAL_Base.HAL.HALGetControlWord = (HAL_Base.HAL.HALGetControlWordDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, ""), typeof(HAL_Base.HAL.HALGetControlWordDelegate));

        }

        private delegate int NativeHALGetControlWordDelegate(ref uint data);

        private static NativeHALGetControlWordDelegate NativeHALGetControlWord;

        internal const string LibhalathenaSharedSo = "libHALAthena_shared.so";
        /*
        [DllImport(LibhalathenaSharedSo, EntryPoint = "getPort")]
        public static extern IntPtr getPort(byte pin);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getPortWithModule")]
        public static extern IntPtr getPortWithModule(byte module, byte pin);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getHALErrorMessage")]
        public static extern IntPtr getHALErrorMessage(int code);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getFPGAVersion")]
        public static extern ushort getFPGAVersion(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getFPGARevision")]
        public static extern uint getFPGARevision(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getFPGATime")]
        public static extern uint getFPGATime(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "getFPGAButton")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getFPGAButton(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALSetErrorData")]
        public static extern int HALSetErrorData(string errors, int errorsLength, int wait_ms);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetControlWord")]
        private static extern int NativeHALGetControlWord(ref uint data);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetAllianceStation")]
        public static extern int HALGetAllianceStation(ref HALAllianceStationID allianceStation);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickAxes")]
        public static extern int HALGetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickPOVs")]
        public static extern int HALGetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickButtons")]
        public static extern int HALGetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickDescriptor")]
        public static extern int HALGetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickIsXbox")]
        public static extern int HALGetJoystickIsXbox(byte joystickNum);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickType")]
        public static extern int HALGetJoystickType(byte joystickNum);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickName")]
        public static extern IntPtr HALGetJoystickName(byte joystickNum);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetJoystickAxisType")]
        public static extern int HALGetJoystickAxisType(byte joystickNum, byte axis);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALSetJoystickOutputs")]
        public static extern int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetMatchTime")]
        public static extern int HALGetMatchTime(ref float matchTime);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALSetNewDataSem")]
        public static extern void HALSetNewDataSem(IntPtr sem);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetSystemActive")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool HALGetSystemActive(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALGetBrownedOut")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool HALGetBrownedOut(ref int status);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALInitialize")]
        public static extern int HALInitialize(int mode);

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramStarting")]
        public static extern void HALNetworkCommunicationObserveUserProgramStarting();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramDisabled")]
        public static extern void HALNetworkCommunicationObserveUserProgramDisabled();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramAutonomous")]
        public static extern void HALNetworkCommunicationObserveUserProgramAutonomous();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramTeleop")]
        public static extern void HALNetworkCommunicationObserveUserProgramTeleop();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALNetworkCommunicationObserveUserProgramTest")]
        public static extern void HALNetworkCommunicationObserveUserProgramTest();

        [DllImport(LibhalathenaSharedSo, EntryPoint = "HALReport")]
        public static extern uint HALReport(byte resource, byte instanceNumber, byte context, string feature = null);
        */

        /// <summary>
        /// Gets the HAL Control Word
        /// </summary>
        /// <returns></returns>
        public static HALControlWord HALGetControlWord()
        {
            //HALControlWord temp = new HALControlWord();
            uint word = 0;
            NativeHALGetControlWord(ref word);
            return new HALControlWord((word & 1) != 0, ((word >> 1) & 1) != 0, ((word >> 2) & 1) != 0,
                ((word >> 3) & 1) != 0, ((word >> 4) & 1) != 0, ((word >> 5) & 1) != 0);
        }
    }
}
