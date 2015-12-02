//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace HAL.AthenaHAL
{
    internal class HAL
    {
        private static string ExtractLibrary()
        {
            string inputName = "";
            string outputName = "";
            inputName = "HAL.AthenaHAL.Native.libHALAthena.so";
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
                    Athena.HALAccelerometer.Initialize(library, loader);
                    Athena.HALAnalog.Initialize(library, loader);
                    Athena.HALCAN.Initialize(library, loader);
                    Athena.HALCanTalonSRX.Initialize(library, loader);
                    Athena.HALCompressor.Initialize(library, loader);
                    Athena.HALDigital.Initialize(library, loader);
                    Athena.HALInterrupts.Initialize(library, loader);
                    Athena.HALNotifier.Initialize(library, loader);
                    Athena.HALPDP.Initialize(library, loader);
                    Athena.HALPower.Initialize(library, loader);
                    Athena.HALSemaphore.Initialize(library, loader);
                    Athena.HALSerialPort.Initialize(library, loader);
                    Athena.HALSolenoid.Initialize(library, loader);
                    Athena.HALUtilities.Initialize(library, loader);
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
            global::HAL.HAL.GetPort = (global::HAL.HAL.GetPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPort"), typeof(global::HAL.HAL.GetPortDelegate));

            global::HAL.HAL.GetPortWithModule = (global::HAL.HAL.GetPortWithModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPortWithModule"), typeof(global::HAL.HAL.GetPortWithModuleDelegate));

            global::HAL.HAL.FreePort = (global::HAL.HAL.FreePortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freePort"), typeof(global::HAL.HAL.FreePortDelegate));

            global::HAL.HAL.GetHALErrorMessage = GetHALErrorMessage;

            NativeGetHALErrorMessage = (NativeGetHALErrorMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getHALErrorMessage"), typeof(NativeGetHALErrorMessageDelegate));

            global::HAL.HAL.GetFPGAVersion = (global::HAL.HAL.GetFPGAVersionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGAVersion"), typeof(global::HAL.HAL.GetFPGAVersionDelegate));

            global::HAL.HAL.GetFPGARevision = (global::HAL.HAL.GetFPGARevisionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGARevision"), typeof(global::HAL.HAL.GetFPGARevisionDelegate));

            global::HAL.HAL.GetFPGATime = (global::HAL.HAL.GetFPGATimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGATime"), typeof(global::HAL.HAL.GetFPGATimeDelegate));

            global::HAL.HAL.GetFPGAButton = (global::HAL.HAL.GetFPGAButtonDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFPGAButton"), typeof(global::HAL.HAL.GetFPGAButtonDelegate));

            global::HAL.HAL.HALSetErrorData = HALSetErrorData;
                
            NativeHALSetErrorData = (NativeHALSetErrorDataDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetErrorData"), typeof(NativeHALSetErrorDataDelegate));

            global::HAL.HAL.GetControlWord = HALGetControlWord;

            NativeHALGetControlWord = (NativeHALGetControlWordDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetControlWord"), typeof(NativeHALGetControlWordDelegate));

            global::HAL.HAL.HALGetAllianceStation = (global::HAL.HAL.HALGetAllianceStationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetAllianceStation"), typeof(global::HAL.HAL.HALGetAllianceStationDelegate));

            global::HAL.HAL.HALGetJoystickAxes = (global::HAL.HAL.HALGetJoystickAxesDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickAxes"), typeof(global::HAL.HAL.HALGetJoystickAxesDelegate));

            global::HAL.HAL.HALGetJoystickPOVs = (global::HAL.HAL.HALGetJoystickPOVsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickPOVs"), typeof(global::HAL.HAL.HALGetJoystickPOVsDelegate));

            global::HAL.HAL.HALGetJoystickButtons = (global::HAL.HAL.HALGetJoystickButtonsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickButtons"), typeof(global::HAL.HAL.HALGetJoystickButtonsDelegate));

            global::HAL.HAL.HALGetJoystickDescriptor = (global::HAL.HAL.HALGetJoystickDescriptorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickDescriptor"), typeof(global::HAL.HAL.HALGetJoystickDescriptorDelegate));

            global::HAL.HAL.HALGetJoystickIsXbox = (global::HAL.HAL.HALGetJoystickIsXboxDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickIsXbox"), typeof(global::HAL.HAL.HALGetJoystickIsXboxDelegate));

            global::HAL.HAL.HALGetJoystickType = (global::HAL.HAL.HALGetJoystickTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickType"), typeof(global::HAL.HAL.HALGetJoystickTypeDelegate));

            global::HAL.HAL.HALGetJoystickAxisType = (global::HAL.HAL.HALGetJoystickAxisTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetJoystickAxisType"), typeof(global::HAL.HAL.HALGetJoystickAxisTypeDelegate));

            global::HAL.HAL.HALSetJoystickOutputs = (global::HAL.HAL.HALSetJoystickOutputsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetJoystickOutputs"), typeof(global::HAL.HAL.HALSetJoystickOutputsDelegate));

            global::HAL.HAL.HALGetMatchTime = (global::HAL.HAL.HALGetMatchTimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetMatchTime"), typeof(global::HAL.HAL.HALGetMatchTimeDelegate));

            global::HAL.HAL.HALSetNewDataSem = (global::HAL.HAL.HALSetNewDataSemDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALSetNewDataSem"), typeof(global::HAL.HAL.HALSetNewDataSemDelegate));

            global::HAL.HAL.HALGetSystemActive = (global::HAL.HAL.HALGetSystemActiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetSystemActive"), typeof(global::HAL.HAL.HALGetSystemActiveDelegate));

            global::HAL.HAL.HALGetBrownedOut = (global::HAL.HAL.HALGetBrownedOutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALGetBrownedOut"), typeof(global::HAL.HAL.HALGetBrownedOutDelegate));

            global::HAL.HAL.HALInitialize = (global::HAL.HAL.HALInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALInitialize"), typeof(global::HAL.HAL.HALInitializeDelegate));

            global::HAL.HAL.HALNetworkCommunicationObserveUserProgramStarting = (global::HAL.HAL.HALNetworkCommunicationObserveUserProgramStartingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramStarting"), typeof(global::HAL.HAL.HALNetworkCommunicationObserveUserProgramStartingDelegate));

            global::HAL.HAL.HALNetworkCommunicationObserveUserProgramDisabled = (global::HAL.HAL.HALNetworkCommunicationObserveUserProgramDisabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramDisabled"), typeof(global::HAL.HAL.HALNetworkCommunicationObserveUserProgramDisabledDelegate));

            global::HAL.HAL.HALNetworkCommunicationObserveUserProgramAutonomous = (global::HAL.HAL.HALNetworkCommunicationObserveUserProgramAutonomousDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramAutonomous"), typeof(global::HAL.HAL.HALNetworkCommunicationObserveUserProgramAutonomousDelegate));

            global::HAL.HAL.HALNetworkCommunicationObserveUserProgramTeleop = (global::HAL.HAL.HALNetworkCommunicationObserveUserProgramTeleopDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramTeleop"), typeof(global::HAL.HAL.HALNetworkCommunicationObserveUserProgramTeleopDelegate));

            global::HAL.HAL.HALNetworkCommunicationObserveUserProgramTest = (global::HAL.HAL.HALNetworkCommunicationObserveUserProgramTestDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HALNetworkCommunicationObserveUserProgramTest"), typeof(global::HAL.HAL.HALNetworkCommunicationObserveUserProgramTestDelegate));

            global::HAL.HAL.HALReport = HALReport;
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
