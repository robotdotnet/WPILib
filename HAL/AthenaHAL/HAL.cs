using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
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
                    HALAnalogAccumulator.Initialize(s_library, loader);
                    HALAnalogGyro.Initialize(s_library, loader);
                    HALAnalogInput.Initialize(s_library, loader);
                    HALAnalogOutput.Initialize(s_library, loader);
                    HALAnalogTrigger.Initialize(s_library, loader);
                    HALCAN.Initialize(s_library, loader);
                    HALCanTalonSRX.Initialize(s_library, loader);
                    HALCompressor.Initialize(s_library, loader);
                    HALConstants.Initialize(s_library, loader);
                    HALCounter.Initialize(s_library, loader);
                    HALDIO.Initialize(s_library, loader);
                    HALDriverStation.Initialize(s_library, loader);
                    HALEncoder.Initialize(s_library, loader);
                    HALI2C.Initialize(s_library, loader);
                    HALInterrupts.Initialize(s_library, loader);
                    HALNotifier.Initialize(s_library, loader);
                    HALPDP.Initialize(s_library, loader);
                    HALPorts.Initialize(s_library, loader);
                    HALPower.Initialize(s_library, loader);
                    HALPWM.Initialize(s_library, loader);
                    HALRelay.Initialize(s_library, loader);
                    HALSerialPort.Initialize(s_library, loader);
                    HALSolenoid.Initialize(s_library, loader);
                    HALSPI.Initialize(s_library, loader);
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

            Base.HAL.HAL_GetPort = (Base.HAL.HAL_GetPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPort"), typeof(Base.HAL.HAL_GetPortDelegate));

            Base.HAL.HAL_GetPortWithModule = (Base.HAL.HAL_GetPortWithModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPortWithModule"), typeof(Base.HAL.HAL_GetPortWithModuleDelegate));

            Base.HAL.HAL_GetErrorMessage = HAL_GetErrorMessage;

            NativeHALGetErrorMessage = (NativeGetHALErrorMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetErrorMessage"), typeof(NativeGetHALErrorMessageDelegate));

            Base.HAL.HAL_GetFPGAVersion = (Base.HAL.HAL_GetFPGAVersionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetFPGAVersion"), typeof(Base.HAL.HAL_GetFPGAVersionDelegate));

            Base.HAL.HAL_GetFPGARevision = (Base.HAL.HAL_GetFPGARevisionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetFPGARevision"), typeof(Base.HAL.HAL_GetFPGARevisionDelegate));

            Base.HAL.HAL_GetFPGATime = (Base.HAL.HAL_GetFPGATimeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetFPGATime"), typeof(Base.HAL.HAL_GetFPGATimeDelegate));

            Base.HAL.HAL_GetFPGAButton = (Base.HAL.HAL_GetFPGAButtonDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetFPGAButton"), typeof(Base.HAL.HAL_GetFPGAButtonDelegate));

            Base.HAL.HAL_GetSystemActive = (Base.HAL.HAL_GetSystemActiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSystemActive"), typeof(Base.HAL.HAL_GetSystemActiveDelegate));

            Base.HAL.HAL_GetBrownedOut = (Base.HAL.HAL_GetBrownedOutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetBrownedOut"), typeof(Base.HAL.HAL_GetBrownedOutDelegate));

            Base.HAL.HAL_Initialize = (Base.HAL.HAL_InitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_Initialize"), typeof(Base.HAL.HAL_InitializeDelegate));

            Base.HAL.HAL_Report = HALReport;
            NativeHALReport = (NativeHALReportDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_Report"), typeof(NativeHALReportDelegate));

        }

        private delegate long NativeHALReportDelegate(int resource, int instanceNumber, int context, byte[] feature);

        private static NativeHALReportDelegate NativeHALReport;

        public static long HALReport(int resource, int instanceNumber, int context, string feature)
        {
            int len;
            return NativeHALReport(resource, instanceNumber, context, CreateUTF8String(feature, out len));
        }

        private delegate IntPtr NativeGetHALErrorMessageDelegate(int code);

        private static NativeGetHALErrorMessageDelegate NativeHALGetErrorMessage;

        /// <summary>
        /// Gets an Error Message from the HAL
        /// </summary>
        /// <param name="code">The Error Code</param>
        /// <returns>The Error Message</returns>
        public static string HAL_GetErrorMessage(int code)
        {
            return ReadUTF8String(NativeHALGetErrorMessage(code));
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
    }
}

