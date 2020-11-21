using Hal.Natives;
using WPIUtil;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{
    public enum RuntimeType : int
    {
        Athena,
        Mock
    }



    public static class HALLowLevel
    {
        internal static HALLowLevelNative lowLevel = null!;
        private readonly static object lockObject = new();

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static bool Initialize()
        {
            lock (lockObject)
            {
                if (lowLevel == null)
                {
                    IFunctionPointerLoader loader = NativeLibraryLoader.LoadNativeLibrary("wpiHal")!;
                    AccelerometerLowLevel.InitializeNatives(loader);
                    AddressableLEDLowLevel.InitializeNatives(loader);
                    AnalogAccumulatorLowLevel.InitializeNatives(loader);
                    AnalogGyroLowLevel.InitializeNatives(loader);
                    AnalogInputLowLevel.InitializeNatives(loader);
                    AnalogOutputLowLevel.InitializeNatives(loader);
                    AnalogTriggerLowLevel.InitializeNatives(loader);
                    CANAPILowLevel.InitializeNatives(loader);
                    CANLowLevel.InitializeNatives(loader);
                    CompressorLowLevel.InitializeNatives(loader);
                    ConstantsLowLevel.InitializeNatives(loader);
                    CounterLowLevel.InitializeNatives(loader);
                    DIOLowLevel.InitializeNatives(loader);
                    DMALowLevel.InitializeNatives(loader);
                    DriverStationLowLevel.InitializeNatives(loader);
                    DutyCycleLowLevel.InitializeNatives(loader);
                    EncoderLowLevel.InitializeNatives(loader);
                    ExtensionsLowLevel.InitializeNatives(loader);
                    HALLowLevel.InitializeNatives(loader);
                    I2CLowLevel.InitializeNatives(loader);
                    InterruptsLowLevel.InitializeNatives(loader);
                    MainLowLevel.InitializeNatives(loader);
                    NotifierLowLevel.InitializeNatives(loader);
                    PDPLowLevel.InitializeNatives(loader);
                    PortsLowLevel.InitializeNatives(loader);
                    PowerLowLevel.InitializeNatives(loader);
                    PWMLowLevel.InitializeNatives(loader);
                    RelayLowLevel.InitializeNatives(loader);
                    SerialPortLowLevel.InitializeNatives(loader);
                    SimDeviceLowLevel.InitializeNatives(loader);
                    SolenoidLowLevel.InitializeNatives(loader);
                    SPILowLevel.InitializeNatives(loader);
                    ThreadsLowLevel.InitializeNatives(loader);
                    UsageReporting.InitializeNatives(loader);
                }
            }
            return lowLevel!.HAL_Initialize(500, 0) != 0;
        }

        public static int GetPort(int channel)
        {
            return lowLevel.HAL_GetPort(channel);
        }

        public static ulong ExpandFPGATime(uint unexpanded)
        {
            return lowLevel.HAL_ExpandFPGATime(unexpanded);
        }

        public static bool GetBrownedOut()
        {
            return lowLevel.HAL_GetBrownedOut() != 0;
        }

        public static unsafe string GetErrorMessage(int code)
        {
            try
            {
                return UTF8String.ReadUTF8String(lowLevel.HAL_GetErrorMessage(code));
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return "Error determining status";
            }
        }

        public static int GetPortWithModule(int module, int channel)
        {
            return lowLevel.HAL_GetPortWithModule(module, channel);
        }

        public static int GetFPGAVersion()
        {
            return lowLevel.HAL_GetFPGAVersion();
        }

        public static ulong GetFPGATimestamp()
        {
            return lowLevel.HAL_GetFPGATime();
        }

        public static RuntimeType GetRuntimeType()
        {
            return lowLevel.HAL_GetRuntimeType();
        }

        public static bool GetUserButton()
        {
            return lowLevel.HAL_GetFPGAButton() != 0;
        }

        public static bool GetSystemActive()
        {
            return lowLevel.HAL_GetSystemActive() != 0;
        }
    }
}
