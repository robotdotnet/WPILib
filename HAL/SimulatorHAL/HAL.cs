using System;
using System.Runtime.InteropServices;
using HAL.Base;

/*
// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HAL
    {
        //Time constants used for GetMatchTime.
        public const double AutonomousTime = 15.0;
        public const double TeleopTime = 135.0;

        private static bool s_libraryLoaded = false;
        private static IntPtr s_library;

        public static void InitializeImpl()
        {
            if (!s_libraryLoaded)
            {
                try
                {
                    /* //These can be ignored for now, since we dont have a native library.
                    OsType type = LoaderUtilities.GetOsType();
                    if (!LoaderUtilities.CheckOsValid(type))
                        throw new InvalidOperationException("OS Not Supported");
                    string loadedPath = LoaderUtilities.ExtractLibrary(type);
                    if (string.IsNullOrEmpty(loadedPath)) throw new FileNotFoundException("Stream not found");
                    library = LoaderUtilities.LoadLibrary(loadedPath, type);
                    if (library == IntPtr.Zero) throw new BadImageFormatException($"Library file {loadedPath} could not be loaded successfully.");
                    
                    s_library = IntPtr.Zero;
                    ILibraryLoader loader = null;
                    // ReSharper disable ExpressionIsAlwaysNull
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
                    // ReSharper restore ExpressionIsAlwaysNull
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
            Base.HAL.HAL_GetPort = HAL_GetPort;
            Base.HAL.HAL_GetPortWithModule = HAL_GetPortWithModule;
            Base.HAL.HAL_GetErrorMessage = HAL_GetErrorMessage;
            Base.HAL.HAL_GetFPGAVersion = HAL_GetFPGAVersion;
            Base.HAL.HAL_GetFPGARevision = HAL_GetFPGARevision;
            Base.HAL.HAL_GetFPGATime = HAL_GetFPGATime;
            Base.HAL.HAL_GetFPGAButton = HAL_GetFPGAButton;
            Base.HAL.HAL_GetSystemActive = HAL_GetSystemActive;
            Base.HAL.HAL_GetBrownedOut = HAL_GetBrownedOut;
            Base.HAL.HAL_Initialize = HAL_Initialize;
            Base.HAL.HAL_Report = HAL_Report;
        }

        public static int HAL_GetPort(int pin)
        {

        }

        public static int HAL_GetPortWithModule(int module, int pin)
        {
        }

        public static string HAL_GetErrorMessage(int code)
        {
        }

        public static int HAL_GetFPGAVersion(ref int status)
        {
        }

        public static long HAL_GetFPGARevision(ref int status)
        {
        }

        public static ulong HAL_GetFPGATime(ref int status)
        {
        }

        public static bool HAL_GetFPGAButton(ref int status)
        {
        }

        public static bool HAL_GetSystemActive(ref int status)
        {
        }

        public static bool HAL_GetBrownedOut(ref int status)
        {
        }

        public static int HAL_Initialize(int mode)
        {
        }

        public static long HAL_Report(int resource, int instanceNumber, int context, string feature)
        {
        }
    }
}

    */