//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using HAL.Base;

namespace HAL.AthenaHAL
{
    internal class HAL
    {
        private static string ExtractLibrary()
        {
            string[] commandArgs = Environment.GetCommandLineArgs();
            foreach (var commandArg in commandArgs)
            {
                //search for a line with the prefix "-athena:"
                if (commandArg.ToLower().Contains("-athena:"))
                {
                    //Split line to get the library.
                    int splitLoc = commandArg.IndexOf(':');
                    string file = commandArg.Substring(splitLoc + 1);

                    //If the file exists, just return it so dlopen can load it.
                    if (File.Exists(file))
                    {
                        return file;
                    }
                }
            }

            var inputName = "HAL.AthenaHAL.Native.libHALAthena.so";
            var outputName = "libHALAthena.so";

            outputName = Path.GetTempPath() + outputName;
            byte[] bytes;
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

        private static bool s_libraryLoaded;
        private static IntPtr s_library;

        public static void InitializeImpl()
        {
            if (!s_libraryLoaded)
            {
                try
                {
                    //Force OS, since we know we are on Linux and the RIO if this is getting called
                    ILibraryLoader loader = new RoboRioLibraryLoader();

                    //Extract our s_library
                    string loadedPath = ExtractLibrary();
                    if (string.IsNullOrEmpty(loadedPath))
                    {
                        //If fail to load, throw exception
                        throw new FileNotFoundException("Library file could not be found in the resorces. Please contact RobotDotNet for support for this issue");
                    }
                    s_library = loader.LoadLibrary(loadedPath);
                    if (s_library == IntPtr.Zero)
                    {
                        //If s_library could not be loaded
                        throw new BadImageFormatException($"Library file {loadedPath} could not be loaded successfully.");
                    }
                    Initialize(s_library, loader);
                    HALAccelerometer.Initialize(s_library, loader);
                    HALAnalog.Initialize(s_library, loader);
                    HALCAN.Initialize(s_library, loader);
                    HALCanTalonSRX.Initialize(s_library, loader);
                    HALCompressor.Initialize(s_library, loader);
                    HALDigital.Initialize(s_library, loader);
                    HALInterrupts.Initialize(s_library, loader);
                    HALNotifier.Initialize(s_library, loader);
                    HALPDP.Initialize(s_library, loader);
                    HALPower.Initialize(s_library, loader);
                    HALSemaphore.Initialize(s_library, loader);
                    HALSerialPort.Initialize(s_library, loader);
                    HALSolenoid.Initialize(s_library, loader);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Environment.Exit(1);
                }
                s_libraryLoaded = true;
            }
        }

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HAL.GetPort = (Base.HAL.GetPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPort"), typeof(Base.HAL.GetPortDelegate));

            Base.HAL.GetPortWithModule = (Base.HAL.GetPortWithModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPortWithModule"), typeof(Base.HAL.GetPortWithModuleDelegate));

            Base.HAL.FreePort = (Base.HAL.FreePortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freePort"), typeof(Base.HAL.FreePortDelegate));

            Base.HAL.GetHALErrorMessage = GetHALErrorMessage;

            NativeGetHALErrorMessage = (NativeGetHALErrorMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getHALErrorMessage"), typeof(NativeGetHALErrorMessageDelegate));

            Base.HAL.GetFPGAVersion = (Base.HAL.GetFPGAVersionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGAVersion"), typeof(Base.HAL.GetFPGAVersionDelegate));

            Base.HAL.GetFPGARevision = (Base.HAL.GetFPGARevisionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGARevision"), typeof(Base.HAL.GetFPGARevisionDelegate));

            Base.HAL.GetFPGATime = (Base.HAL.GetFPGATimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGATime"), typeof(Base.HAL.GetFPGATimeDelegate));

            Base.HAL.GetFPGAButton = (Base.HAL.GetFPGAButtonDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGAButton"), typeof(Base.HAL.GetFPGAButtonDelegate));

            Base.HAL.HALSetErrorData = HALSetErrorData;
                
            NativeHALSetErrorData = (NativeHALSetErrorDataDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetErrorData"), typeof(NativeHALSetErrorDataDelegate));

            Base.HAL.HALSendError = HALSendError;

            NativeHALSendError = (NativeHALSendErrorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSendError"), typeof(NativeHALSendErrorDelegate));

            Base.HAL.GetControlWord = HALGetControlWord;

            NativeHALGetControlWord = (NativeHALGetControlWordDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetControlWord"), typeof(NativeHALGetControlWordDelegate));

            Base.HAL.HALGetAllianceStation = (Base.HAL.HALGetAllianceStationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetAllianceStation"), typeof(Base.HAL.HALGetAllianceStationDelegate));

            Base.HAL.HALGetJoystickAxes = (Base.HAL.HALGetJoystickAxesDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickAxes"), typeof(Base.HAL.HALGetJoystickAxesDelegate));

            Base.HAL.HALGetJoystickPOVs = (Base.HAL.HALGetJoystickPOVsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickPOVs"), typeof(Base.HAL.HALGetJoystickPOVsDelegate));

            Base.HAL.HALGetJoystickButtons = (Base.HAL.HALGetJoystickButtonsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickButtons"), typeof(Base.HAL.HALGetJoystickButtonsDelegate));

            Base.HAL.HALGetJoystickDescriptor = (Base.HAL.HALGetJoystickDescriptorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickDescriptor"), typeof(Base.HAL.HALGetJoystickDescriptorDelegate));

            Base.HAL.HALGetJoystickIsXbox = (Base.HAL.HALGetJoystickIsXboxDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickIsXbox"), typeof(Base.HAL.HALGetJoystickIsXboxDelegate));

            Base.HAL.HALGetJoystickType = (Base.HAL.HALGetJoystickTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickType"), typeof(Base.HAL.HALGetJoystickTypeDelegate));

            Base.HAL.HALGetJoystickAxisType = (Base.HAL.HALGetJoystickAxisTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickAxisType"), typeof(Base.HAL.HALGetJoystickAxisTypeDelegate));

            Base.HAL.HALSetJoystickOutputs = (Base.HAL.HALSetJoystickOutputsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetJoystickOutputs"), typeof(Base.HAL.HALSetJoystickOutputsDelegate));

            Base.HAL.HALGetMatchTime = (Base.HAL.HALGetMatchTimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetMatchTime"), typeof(Base.HAL.HALGetMatchTimeDelegate));

            Base.HAL.HALSetNewDataSem = (Base.HAL.HALSetNewDataSemDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetNewDataSem"), typeof(Base.HAL.HALSetNewDataSemDelegate));

            Base.HAL.HALGetSystemActive = (Base.HAL.HALGetSystemActiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetSystemActive"), typeof(Base.HAL.HALGetSystemActiveDelegate));

            Base.HAL.HALGetBrownedOut = (Base.HAL.HALGetBrownedOutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetBrownedOut"), typeof(Base.HAL.HALGetBrownedOutDelegate));

            Base.HAL.HALInitialize = (Base.HAL.HALInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALInitialize"), typeof(Base.HAL.HALInitializeDelegate));

            Base.HAL.HALNetworkCommunicationObserveUserProgramStarting = (Base.HAL.HALNetworkCommunicationObserveUserProgramStartingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramStarting"), typeof(Base.HAL.HALNetworkCommunicationObserveUserProgramStartingDelegate));

            Base.HAL.HALNetworkCommunicationObserveUserProgramDisabled = (Base.HAL.HALNetworkCommunicationObserveUserProgramDisabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramDisabled"), typeof(Base.HAL.HALNetworkCommunicationObserveUserProgramDisabledDelegate));

            Base.HAL.HALNetworkCommunicationObserveUserProgramAutonomous = (Base.HAL.HALNetworkCommunicationObserveUserProgramAutonomousDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramAutonomous"), typeof(Base.HAL.HALNetworkCommunicationObserveUserProgramAutonomousDelegate));

            Base.HAL.HALNetworkCommunicationObserveUserProgramTeleop = (Base.HAL.HALNetworkCommunicationObserveUserProgramTeleopDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramTeleop"), typeof(Base.HAL.HALNetworkCommunicationObserveUserProgramTeleopDelegate));

            Base.HAL.HALNetworkCommunicationObserveUserProgramTest = (Base.HAL.HALNetworkCommunicationObserveUserProgramTestDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramTest"), typeof(Base.HAL.HALNetworkCommunicationObserveUserProgramTestDelegate));

            Base.HAL.HALReport = HALReport;
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

        private delegate int NativeHALSendErrorDelegate(int isError, int errorCode, int isLVCode, 
            byte[] details, byte[] location, byte[] callStack, int printMsg);

        private static NativeHALSendErrorDelegate NativeHALSendError;

        public static int HALSendError(bool isError, int errorCode, bool isLVCode, string details,
            string location, string callStack, bool printMsg)
        {
            int len;
            byte[] loc = CreateUTF8String(location, out len);
            byte[] det = CreateUTF8String(details, out len);
            byte[] stack = CreateUTF8String(callStack, out len);
            return NativeHALSendError(isError ? 1 : 0, errorCode, isLVCode ? 1 : 0, det, loc, stack, printMsg ? 1 : 0);
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
