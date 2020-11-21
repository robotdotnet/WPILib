using Hal.Natives;
using System.Collections.Generic;
using WPIUtil;
using WPIUtil.NativeUtilities;

namespace Hal
{
    public enum ResourceType
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
        Controller = 0,
        Module = 1,
        Language = 2,
        CANPlugin = 3,
        Accelerometer = 4,
        ADXL345 = 5,
        AnalogChannel = 6,
        AnalogTrigger = 7,
        AnalogTriggerOutput = 8,
        CANJaguar = 9,
        Compressor = 10,
        Counter = 11,
        Dashboard = 12,
        DigitalInput = 13,
        DigitalOutput = 14,
        DriverStationCIO = 15,
        DriverStationEIO = 16,
        DriverStationLCD = 17,
        Encoder = 18,
        GearTooth = 19,
        Gyro = 20,
        I2C = 21,
        Framework = 22,
        Jaguar = 23,
        Joystick = 24,
        Kinect = 25,
        KinectStick = 26,
        PIDController = 27,
        Preferences = 28,
        PWM = 29,
        Relay = 30,
        RobotDrive = 31,
        SerialPort = 32,
        Servo = 33,
        Solenoid = 34,
        SPI = 35,
        Task = 36,
        Ultrasonic = 37,
        Victor = 38,
        Button = 39,
        Command = 40,
        AxisCamera = 41,
        PCVideoServer = 42,
        SmartDashboard = 43,
        Talon = 44,
        HiTechnicColorSensor = 45,
        HiTechnicAccel = 46,
        HiTechnicCompass = 47,
        SRF08 = 48,
        AnalogOutput = 49,
        VictorSP = 50,
        PWMTalonSRX = 51,
        CANTalonSRX = 52,
        ADXL362 = 53,
        ADXRS450 = 54,
        RevSPARK = 55,
        MindsensorsSD540 = 56,
        DigitalGlitchFilter = 57,
        ADIS16448 = 58,
        PDP = 59,
        PCM = 60,
        PigeonIMU = 61,
        NidecBrushless = 62,
        CANifier = 63,
        TalonFX = 64,
        CTRE_future1 = 65,
        CTRE_future2 = 66,
        CTRE_future3 = 67,
        CTRE_future4 = 68,
        CTRE_future5 = 69,
        CTRE_future6 = 70,
        LinearFilter = 71,
        XboxController = 72,
        UsbCamera = 73,
        NavX = 74,
        Pixy = 75,
        Pixy2 = 76,
        ScanseSweep = 77,
        Shuffleboard = 78,
        CAN = 79,
        DigilentDMC60 = 80,
        PWMVictorSPX = 81,
        RevSparkMaxPWM = 82,
        RevSparkMaxCAN = 83,
        ADIS16470 = 84,
        PIDController2 = 85,
        ProfiledPIDController = 86,
        Kinematics = 87,
        Odometry = 88,
        Units = 89,
        TrapezoidProfile = 90,
        DutyCycle = 91,
        AddressableLEDs = 92,
        FusionVenom = 93,
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }

#pragma warning disable CA1717 // Only FlagsAttribute enums should have plural names
    public enum Instances
#pragma warning restore CA1717 // Only FlagsAttribute enums should have plural names
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
        Language_LabVIEW = 1,
        Language_CPlusPlus = 2,
        Language_Java = 3,
        Language_Python = 4,
        Language_DotNet = 5,
        Language_Kotlin = 6,
        CANPlugin_BlackJagBridge = 1,
        CANPlugin_2CAN = 2,
        Framework_Iterative = 1,
        Framework_Simple = 2,
        Framework_CommandControl = 3,
        Framework_Timed = 4,
        Framework_ROS = 5,
        Framework_RobotBuilder = 6,
        RobotDrive_ArcadeStandard = 1,
        RobotDrive_ArcadeButtonSpin = 2,
        RobotDrive_ArcadeRatioCurve = 3,
        RobotDrive_Tank = 4,
        RobotDrive_MecanumPolar = 5,
        RobotDrive_MecanumCartesian = 6,
        RobotDrive2_DifferentialArcade = 7,
        RobotDrive2_DifferentialTank = 8,
        RobotDrive2_DifferentialCurvature = 9,
        RobotDrive2_MecanumCartesian = 10,
        RobotDrive2_MecanumPolar = 11,
        RobotDrive2_KilloughCartesian = 12,
        RobotDrive2_KilloughPolar = 13,
        DriverStationCIO_Analog = 1,
        DriverStationCIO_DigitalIn = 2,
        DriverStationCIO_DigitalOut = 3,
        DriverStationEIO_Acceleration = 1,
        DriverStationEIO_AnalogIn = 2,
        DriverStationEIO_AnalogOut = 3,
        DriverStationEIO_Button = 4,
        DriverStationEIO_LED = 5,
        DriverStationEIO_DigitalIn = 6,
        DriverStationEIO_DigitalOut = 7,
        DriverStationEIO_FixedDigitalOut = 8,
        DriverStationEIO_PWM = 9,
        DriverStationEIO_Encoder = 10,
        DriverStationEIO_TouchSlider = 11,
        ADXL345_SPI = 1,
        ADXL345_I2C = 2,
        Command_Scheduler = 1,
        Command2_Scheduler = 2,
        SmartDashboard_Instance = 1,
        Kinematics_DifferentialDrive = 1,
        Kinematics_MecanumDrive = 2,
        Kinematics_SwerveDrive = 3,
        Odometry_DifferentialDrive = 1,
        Odometry_MecanumDrive = 2,
        Odometry_SwerveDrive = 3,
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }


    public static class UsageReporting
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        internal static UsageReportingLowLevelNative lowLevel = null!;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        private readonly struct ReportStore
        {
            public ResourceType Resource { get; }
            public int InstanceNumber { get; }
            public int Context { get; }
            public string Feature { get; }

            public ReportStore(ResourceType resource, int instanceNumber, int context, string feature)
            {
                this.Resource = resource;
                this.InstanceNumber = instanceNumber;
                this.Context = context;
                this.Feature = feature;
            }
        }

        private static readonly object storeLock = new object();
        private static readonly List<ReportStore> storeList = new List<ReportStore>();
        private static bool allowDirectWrite = false;

        public unsafe static void WriteReports()
        {
            lock (storeLock)
            {
                if (allowDirectWrite)
                {
                    return;
                }
                allowDirectWrite = true;
            }
            foreach (var r in storeList)
            {
                if (string.IsNullOrEmpty(r.Feature))
                {
                    byte empty = 0;
                    lowLevel.HAL_Report((int)r.Resource, r.InstanceNumber, r.Context, &empty);
                }
                else
                {
                    var str = new UTF8String(r.Feature);
                    fixed (byte* buf = str.Buffer)
                    {
                        lowLevel.HAL_Report((int)r.Resource, r.InstanceNumber, r.Context, buf);
                    }
                }
            }
        }

        public unsafe static int Report(ResourceType resourceType, Instances instanceNumber, int context = 0, string feature = "")
        {
            lock (storeLock)
            {
                if (!allowDirectWrite)
                {
                    storeList.Add(new ReportStore(resourceType, (int)instanceNumber, context, feature));
                    return 0;
                }
            }
            if (string.IsNullOrEmpty(feature))
            {
                byte empty = 0;
                return lowLevel.HAL_Report((int)resourceType, (int)instanceNumber, context, &empty);
            }
            else
            {
                var str = new UTF8String(feature);
                fixed (byte* buf = str.Buffer)
                {
                    return lowLevel.HAL_Report((int)resourceType, (int)instanceNumber, context, buf);
                }
            }
        }

        public unsafe static int Report(ResourceType resourceType, int instanceNumber, int context = 0, string feature = "")
        {
            lock (storeLock)
            {
                if (!allowDirectWrite)
                {
                    storeList.Add(new ReportStore(resourceType, instanceNumber, context, feature));
                    return 0;
                }
            }
            if (string.IsNullOrEmpty(feature))
            {
                byte empty = 0;
                return lowLevel.HAL_Report((int)resourceType, (int)instanceNumber, context, &empty);
            }
            else
            {
                var str = new UTF8String(feature);
                fixed (byte* buf = str.Buffer)
                {
                    return lowLevel.HAL_Report((int)resourceType, (int)instanceNumber, context, buf);
                }
            }
        }
    }
}
