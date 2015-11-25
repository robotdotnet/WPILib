//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Collections.Generic;
using System.Text;
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
            inputName = "HAL_RoboRIO.Native.libHALAthena.so";
            outputName = "libHALAthena.so";
            outputName = Path.GetTempPath() + outputName;
            byte[] bytes = null;
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(inputName))
            {
                if (s == null || s.Length == 0)
                    return null;
                bytes = new byte[(int)s.Length];
                s.Read(bytes, 0, (int)s.Length);
            }
            bool isFileSame = true;

            //If file exists
            if (File.Exists(outputName))
            {
                //Load existing file into memory
                byte[] existingFile = File.ReadAllBytes(outputName);
                //If files are different length they are different,
                //and we can automatically assume they are different.
                if (existingFile.Length != bytes.Length)
                {
                    isFileSame = false;
                }
                else
                {
                    //Otherwise directly compare the files
                    //I first tried hashing, but that took 1.5-2.0 seconds,
                    //wheras this took 0.3 seconds.
                    for (int i = 0; i < existingFile.Length; i++)
                    {
                        if (bytes[i] != existingFile[i])
                        {
                            isFileSame = false;
                        }
                    }
                }
            }
            else
            {
                isFileSame = false;
            }

            //If file is different write the new file
            if (!isFileSame)
            {
                if (File.Exists(outputName))
                    File.Delete(outputName);
                File.WriteAllBytes(outputName, bytes);
            }
            //Force a garbage collection, since we just wasted about 12 MB of RAM.
            GC.Collect();

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
                    ILibraryLoader loader = new RoboRioLibraryLoader();

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
                    HALCanTalonSRX.Initialize(library, loader);
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

            HAL_Base.HAL.GetHALErrorMessage = GetHALErrorMessage;

            NativeGetHALErrorMessage = (NativeGetHALErrorMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getHALErrorMessage"), typeof(NativeGetHALErrorMessageDelegate));

            HAL_Base.HAL.GetFPGAVersion = (HAL_Base.HAL.GetFPGAVersionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGAVersion"), typeof(HAL_Base.HAL.GetFPGAVersionDelegate));

            HAL_Base.HAL.GetFPGARevision = (HAL_Base.HAL.GetFPGARevisionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGARevision"), typeof(HAL_Base.HAL.GetFPGARevisionDelegate));

            HAL_Base.HAL.GetFPGATime = (HAL_Base.HAL.GetFPGATimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGATime"), typeof(HAL_Base.HAL.GetFPGATimeDelegate));

            HAL_Base.HAL.GetFPGAButton = (HAL_Base.HAL.GetFPGAButtonDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGAButton"), typeof(HAL_Base.HAL.GetFPGAButtonDelegate));

            HAL_Base.HAL.HALSetErrorData = HALSetErrorData;
                
            NativeHALSetErrorData = (NativeHALSetErrorDataDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetErrorData"), typeof(NativeHALSetErrorDataDelegate));

            HAL_Base.HAL.GetControlWord = HALGetControlWord;

            NativeHALGetControlWord = (NativeHALGetControlWordDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetControlWord"), typeof(NativeHALGetControlWordDelegate));

            HAL_Base.HAL.HALGetAllianceStation = (HAL_Base.HAL.HALGetAllianceStationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetAllianceStation"), typeof(HAL_Base.HAL.HALGetAllianceStationDelegate));

            HAL_Base.HAL.HALGetJoystickAxes = (HAL_Base.HAL.HALGetJoystickAxesDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickAxes"), typeof(HAL_Base.HAL.HALGetJoystickAxesDelegate));

            HAL_Base.HAL.HALGetJoystickPOVs = (HAL_Base.HAL.HALGetJoystickPOVsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickPOVs"), typeof(HAL_Base.HAL.HALGetJoystickPOVsDelegate));

            HAL_Base.HAL.HALGetJoystickButtons = (HAL_Base.HAL.HALGetJoystickButtonsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickButtons"), typeof(HAL_Base.HAL.HALGetJoystickButtonsDelegate));

            HAL_Base.HAL.HALGetJoystickDescriptor = (HAL_Base.HAL.HALGetJoystickDescriptorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickDescriptor"), typeof(HAL_Base.HAL.HALGetJoystickDescriptorDelegate));

            HAL_Base.HAL.HALGetJoystickIsXbox = (HAL_Base.HAL.HALGetJoystickIsXboxDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickIsXbox"), typeof(HAL_Base.HAL.HALGetJoystickIsXboxDelegate));

            HAL_Base.HAL.HALGetJoystickType = (HAL_Base.HAL.HALGetJoystickTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickType"), typeof(HAL_Base.HAL.HALGetJoystickTypeDelegate));
            //TODO: FIX THIS
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

            HAL_Base.HAL.HALReport = HALReport;
            NativeHALReport = (NativeHALReportDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALReport"), typeof(NativeHALReportDelegate));

        }

        private delegate uint NativeHALReportDelegate(byte resource, byte instanceNumber, byte context, byte[] feature);

        private static NativeHALReportDelegate NativeHALReport;

        public static uint HALReport(byte resource, byte instanceNumber, byte context, string feature)
        {
            int len;
            return NativeHALReport(resource, instanceNumber, context, CreateUTF8String(feature, out len));
        }

        private delegate int NativeHALSetErrorDataDelegate(byte[] errors, int errorsLength, int waitMs);

        private static NativeHALSetErrorDataDelegate NativeHALSetErrorData;

        public static int HALSetErrorData(string errors, int waitMs)
        {
            int len;
            byte[] errorB = CreateUTF8String(errors, out len);
            return NativeHALSetErrorData(errorB, len, waitMs); 
        }

        private delegate IntPtr NativeGetHALErrorMessageDelegate(int code);

        private static NativeGetHALErrorMessageDelegate NativeGetHALErrorMessage;

        /// <summary>
        /// Gets an Error Message from the HAL
        /// </summary>
        /// <param name="code">The Error Code</param>
        /// <returns>The Error Message</returns>
        public static string GetHALErrorMessage(int code)
        {
            return ReadUTF8String(NativeGetHALErrorMessage(code));
        }

        internal static string ReadUTF8String(IntPtr ptr)
        {
            var data = new List<byte>();
            var off = 0;
            while (true)
            {
                var ch = Marshal.ReadByte(ptr, off++);
                if (ch == 0)
                {
                    break;
                }
                data.Add(ch);
            }
            return Encoding.UTF8.GetString(data.ToArray());
        }

        internal static byte[] CreateUTF8String(string str, out int size)
        {
            if (str == null)
            {
                str = "";
            }

            var bytes = Encoding.UTF8.GetByteCount(str);

            var buffer = new byte[bytes + 1];
            size = bytes;
            Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 0);
            buffer[bytes] = 0;
            return buffer;
        }

        private delegate int NativeHALGetControlWordDelegate(ref uint data);

        private static NativeHALGetControlWordDelegate NativeHALGetControlWord;

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
