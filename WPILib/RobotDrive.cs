using System;
using System.Collections.Generic;
using HAL.Base;
using WPILib.Interfaces;

namespace WPILib
{
    /// The location of a motor on the robot for the purpose of driving.
    public enum MotorType
    {
        /// <summary>
        /// The front left motor
        /// </summary>
        FrontLeft = 0,
        /// <summary>
        /// The front right motor
        /// </summary>
        FrontRight,
        /// <summary>
        /// The rear left motor
        /// </summary>
        RearLeft,
        /// <summary>
        /// The rear right motor.
        /// </summary>
        RearRight,
    }

    /// <summary>
    /// Utility class for handling robot drive based on a defined motor configuration.
    /// </summary>
    /// <remarks>
    /// The robot drive class handles basic driving for a robot. Currently, 2 and 4 motor tank
    /// and mecanum drive trains are supported. In the future other drive types like swerve might
    /// be implemented. Motor channel numbers are supplied on creation of the class. 
    /// </remarks>
    public class RobotDrive : IMotorSafety, IDisposable
    {
        /// The RobotDrive MotorSafetyHelper
        protected MotorSafetyHelper SafetyHelper;

        /// Default Expiration Time for MotorSafety
        public const double DefaultExpirationTime = 0.1;
        /// Default Sensitivity for the <see cref="Drive(double, double)"/> function
        public const double DefaultSensitivity = 0.5;
        /// Default Max Output for the motors
        public const double DefaultMaxOutput = 1.0;

        /// The Default max number of motors
        protected static int MaxNumberOfMotors = 4;

        /// The Front Left Motor
        protected readonly ISpeedController FrontLeftMotor;
        /// The Front Right Motor
        protected readonly ISpeedController FrontRightMotor;
        /// The Rear Left Motor
        protected readonly ISpeedController RearLeftMotor;
        /// The Rear Right Motor
        protected readonly ISpeedController RearRightMotor;
        /// Returns if this class allocated the speed controllers or was passed them.
        protected readonly bool AllocatedSpeedControllers;
        /// The SyncGroup for Jaguar Motor Controllers
        protected byte SyncGroup;

        /// Usage Reporters
        protected static bool ArcadeRatioCurveReported;
        /// <inheritdoc cref="ArcadeRatioCurveReported"/>
        protected static bool TankReported;
        /// <inheritdoc cref="ArcadeRatioCurveReported"/>
        protected static bool ArcadeStandardReported;
        /// <inheritdoc cref="ArcadeRatioCurveReported"/>
        protected static bool MecanumCartesianReported;
        /// <inheritdoc cref="ArcadeRatioCurveReported"/>
        protected static bool MecanumPolarReported;

        /// <summary>
        /// Constructor for RobotDrive with 2 motors specified with channel numbers.
        /// </summary>
        /// <remarks>Sets up parameters for a two motor drive system where the left
        /// and right motor channels are specified in the call. This call assumes Talons
        /// for controlling the motors.</remarks>
        /// <param name="leftMotorChannel">The PWM channel number that drives the left motor</param>
        /// <param name="rightMotorChannel">The PWM channel number that drives the right motor</param>
        public RobotDrive(int leftMotorChannel, int rightMotorChannel)
        {
            Sensitivity = DefaultSensitivity;
            MaxOutput = DefaultMaxOutput;
            FrontLeftMotor = null;
            RearLeftMotor = new Talon(leftMotorChannel);
            FrontRightMotor = null;
            RearRightMotor = new Talon(rightMotorChannel);
            AllocatedSpeedControllers = true;
            SetupMotorSafety();
            Drive(0, 0);
        }

        /// <summary>
        /// Constructor for RobotDrive with 4 motors specified with channel numbers.
        /// </summary>
        /// <remarks>Sets up parameters for a two motor drive system where the left
        /// and right motor channels are specified in the call. This call assumes Talons
        /// for controlling the motors.</remarks>
        /// <param name="frontLeftMotor">The PWM Channel that drives the Front Left Motor</param>
        /// <param name="rearLeftMotor">The PWM Channel that drives the Rear Left Motor</param>
        /// <param name="frontRightMotor">The PWM Channel that drives the Front Right Motor</param>
        /// <param name="rearRightMotor">The PWM Channel that drives the Rear Right Motor</param>
        public RobotDrive(int frontLeftMotor, int rearLeftMotor,
                       int frontRightMotor, int rearRightMotor)
        {
            Sensitivity = DefaultSensitivity;
            MaxOutput = DefaultMaxOutput;
            RearLeftMotor = new Talon(rearLeftMotor);
            RearRightMotor = new Talon(rearRightMotor);
            FrontLeftMotor = new Talon(frontLeftMotor);
            FrontRightMotor = new Talon(frontRightMotor);
            AllocatedSpeedControllers = true;
            SetupMotorSafety();
            Drive(0, 0);
        }

        /// <summary>
        /// Constructor for RobotDrive with 2 motors specified as <see cref="ISpeedController"/> objects.
        /// </summary>
        /// <remarks>The <see cref="ISpeedController"/> version of the constructor enables programs to use
        /// RobotDrive classes with subclasses of the <see cref="ISpeedController"/> objects.</remarks>
        /// <param name="leftMotor">The <see cref="ISpeedController"/> controlling the Left Motor</param>
        /// <param name="rightMotor">The <see cref="ISpeedController"/> controlling the Right Motor</param>
        public RobotDrive(ISpeedController leftMotor, ISpeedController rightMotor)
        {
            if (leftMotor == null)
            {
                throw new ArgumentNullException(nameof(leftMotor), "Null Motor Controller Provided");
            }
            if (rightMotor == null)
            {
                throw new ArgumentNullException(nameof(rightMotor), "Null Motor Controller Provided");
            }
            FrontLeftMotor = null;
            RearLeftMotor = leftMotor;
            FrontRightMotor = null;
            RearRightMotor = rightMotor;
            Sensitivity = DefaultSensitivity;
            MaxOutput = DefaultMaxOutput;
            AllocatedSpeedControllers = false;
            SetupMotorSafety();
            Drive(0, 0);
        }

        /// <summary>
        /// Constructor for RobotDrive with 4 motors specified as <see cref="ISpeedController"/> objects.
        /// </summary>
        /// <remarks>The <see cref="ISpeedController"/> version of the constructor enables programs to use
        /// RobotDrive classes with subclasses of the <see cref="ISpeedController"/> objects.</remarks>
        /// <param name="frontLeftMotor">The <see cref="ISpeedController"/> controlling the Front Left Motor</param>
        /// <param name="rearLeftMotor">The <see cref="ISpeedController"/> controlling the Rear Left Motor</param>
        /// <param name="frontRightMotor">The <see cref="ISpeedController"/> controlling the Front Right Motor</param>
        /// <param name="rearRightMotor">The <see cref="ISpeedController"/> controlling the Rear Right Motor</param>
        public RobotDrive(ISpeedController frontLeftMotor, ISpeedController rearLeftMotor,
                      ISpeedController frontRightMotor, ISpeedController rearRightMotor)
        {
            if (frontLeftMotor == null)
            {
                throw new ArgumentNullException(nameof(frontLeftMotor), "Null Motor Controller Provided");
            }
            if (frontRightMotor == null)
            {
                throw new ArgumentNullException(nameof(frontRightMotor), "Null Motor Controller Provided");
            }
            if (rearLeftMotor == null)
            {
                throw new ArgumentNullException(nameof(rearLeftMotor), "Null Motor Controller Provided");
            }
            if (rearRightMotor == null)
            {
                throw new ArgumentNullException(nameof(rearRightMotor), "Null Motor Controller Provided");
            }
            FrontLeftMotor = frontLeftMotor;
            RearLeftMotor = rearLeftMotor;
            FrontRightMotor = frontRightMotor;
            RearRightMotor = rearRightMotor;
            Sensitivity = DefaultSensitivity;
            MaxOutput = DefaultMaxOutput;
            AllocatedSpeedControllers = false;
            SetupMotorSafety();
            Drive(0, 0);
        }

        /// <summary>
        /// Drive the motors at a specified "speed" and "curve".
        /// </summary>
        /// <remarks>The speed and curve are -1.0 to +1.0 values where 0.0 represents stopped
        /// and not turning. The algorithm for adding in the direction attempts to provide a constant
        /// turn radius for differing speeds.
        /// <para/>This function will most likely be used in an autonomous routine.</remarks>
        /// <param name="outputMagnitude">The forward component of the magnitude to send to the motors.</param>
        /// <param name="curve">The rate of turn, constant for different forward speeds.c</param>
        public void Drive(double outputMagnitude, double curve)
        {
            double leftOutput, rightOutput;

            if (!ArcadeRatioCurveReported)
            {
                HAL.Base.HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_ArcadeRatioCurve, (byte)NumMotors);
                ArcadeRatioCurveReported = true;
            }
            if (curve < 0)
            {
                double value = Math.Log(-curve);
                double ratio = (value - Sensitivity) / (value + Sensitivity);
                if (ratio == 0)
                {
                    ratio = .0000000001;
                }
                leftOutput = outputMagnitude / ratio;
                rightOutput = outputMagnitude;
            }
            else if (curve > 0)
            {
                double value = Math.Log(curve);
                double ratio = (value - Sensitivity) / (value + Sensitivity);
                if (ratio == 0)
                {
                    ratio = .0000000001;
                }
                leftOutput = outputMagnitude;
                rightOutput = outputMagnitude / ratio;
            }
            else
            {
                leftOutput = outputMagnitude;
                rightOutput = outputMagnitude;
            }
            SetLeftRightMotorOutputs(leftOutput, rightOutput);
        }

        /// <summary>
        /// Provide tank style driving for the robot.
        /// </summary>
        /// <remarks>Drives the robot using the 2 joystick inputs. The Y-axis will be selected
        /// from each joystick object.</remarks>
        /// <param name="leftStick">The joystick to control the left side of the robot.</param>
        /// <param name="rightStick">The joystick to control the right side of the robot.</param>
        /// <param name="squaredInputs">If this setting is true, it decreases the sensitvity at lower speeds.</param>
        public void TankDrive(GenericHID leftStick, GenericHID rightStick, bool squaredInputs = true)
        {
            if (leftStick == null)
                throw new ArgumentNullException(nameof(leftStick), "Joystick provided was null");
            if (rightStick == null)
                throw new ArgumentNullException(nameof(rightStick), "Joystick provided was null");
            TankDrive(leftStick.GetY(), rightStick.GetY(), squaredInputs);
        }

        /// <summary>
        /// Provide tank style driving for the robot.
        /// </summary>
        /// <remarks>This function lets you pick the axis to be used on each joystick object for
        /// each side of the robot.</remarks>
        /// <param name="leftStick">The joystick to use for the left side.</param>
        /// <param name="leftAxis">The axis to select on the left joystick.</param>
        /// <param name="rightStick">The joystick to use for the right side.</param>
        /// <param name="rightAxis">The axis to select on the right joystick</param>
        /// <param name="squaredInputs">If this setting is true, it decreases the sensitvity at lower speeds.</param>
        public void TankDrive(GenericHID leftStick, int leftAxis,
                          GenericHID rightStick, int rightAxis, bool squaredInputs = true)
        {
            if (leftStick == null)
                throw new ArgumentNullException(nameof(leftStick), "Joystick provided was null");
            if (rightStick == null)
                throw new ArgumentNullException(nameof(rightStick), "Joystick provided was null");
            TankDrive(leftStick.GetRawAxis(leftAxis), rightStick.GetRawAxis(rightAxis), squaredInputs);
        }

        /// <summary>
        /// Provide tank style driving for the robot.
        /// </summary>
        /// <remarks>This function lets you directly provide joystick values from any source.</remarks>
        /// <param name="leftValue">The value for the left side</param>
        /// <param name="rightValue">The value for the right side</param>
        /// <param name="squaredInputs">If this setting is true, it decreases the sensitvity at lower speeds.</param>
        public void TankDrive(double leftValue, double rightValue, bool squaredInputs = true)
        {
            if (!TankReported)
            {
                HAL.Base.HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_Tank, (byte)NumMotors);
                TankReported = true;
            }

            // square the inputs (while preserving the sign) to increase fine control while permitting full power
            leftValue = Limit(leftValue);
            rightValue = Limit(rightValue);
            if (squaredInputs)
            {
                leftValue *= Math.Abs(leftValue);
                rightValue *= Math.Abs(rightValue);
            }
            SetLeftRightMotorOutputs(leftValue, rightValue);
        }

        /// <summary>
        /// Provides arcade style driving for the robot.
        /// </summary>
        /// <remarks>Given a single joystick, the class assumes the Y axis for the move value and the X
        /// axis for the rotate value.</remarks>
        /// <param name="stick">The joystick to use for drving. Y-axis will be forward/backwards, and X-axis
        /// will be rotation.</param>
        /// <param name="squaredInputs">If this setting is true, it decreases the sensitvity at lower speeds.</param>
        public void ArcadeDrive(GenericHID stick, bool squaredInputs = true)
        {
            if (stick == null)
                throw new ArgumentNullException(nameof(stick), "Joystick provided was null");
            ArcadeDrive(stick.GetY(), stick.GetX(), squaredInputs);
        }

        /// <summary>
        /// Provides arcade style driving for the robot.
        /// </summary>
        /// <remarks>Given 2 joysticks and 2 axis numbers, computes the values to send to the drive.</remarks>
        /// <param name="moveStick">The Joystick object that represents the forward/backward direction</param>
        /// <param name="moveAxis">The axis on the moveStick to use.</param>
        /// <param name="rotateStick">The Joystick object that represents the rotation value</param>
        /// <param name="rotateAxis">The axis on the rotationStick to use.</param>
        /// <param name="squaredInputs">If this setting is true, it decreases the sensitvity at lower speeds.</param>
        public void ArcadeDrive(GenericHID moveStick, int moveAxis, GenericHID rotateStick, int rotateAxis,
            bool squaredInputs = true)
        {
            if (moveStick == null)
                throw new ArgumentNullException(nameof(moveStick), "Joystick provided was null");
            if (rotateStick == null)
                throw new ArgumentNullException(nameof(rotateStick), "Joystick provided was null");

            double moveValue = moveStick.GetRawAxis(moveAxis);
            double rotateValue = rotateStick.GetRawAxis(rotateAxis);

            ArcadeDrive(moveValue, rotateValue, squaredInputs);
        }

        /// <summary>
        /// Provides arcade style driving for the robot.
        /// </summary>
        /// <remarks>This function lets you directly provide joystick values from any source.</remarks>
        /// <param name="moveValue">The value to use for forward/backwards</param>
        /// <param name="rotateValue">The value to use to rotate left/right</param>
        /// <param name="squaredInputs">If this setting is true, it decreases the sensitvity at lower speeds.</param>
        public void ArcadeDrive(double moveValue, double rotateValue, bool squaredInputs = true)
        {
            if (!ArcadeStandardReported)
            {
                HAL.Base.HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_ArcadeStandard, (byte)NumMotors);
                ArcadeStandardReported = true;
            }

            double leftMotorSpeed;
            double rightMotorSpeed;

            moveValue = Limit(moveValue);
            rotateValue = Limit(rotateValue);

            if (squaredInputs)
            {
                // square the inputs (while preserving the sign) to increase fine control while permitting full power
                moveValue *= Math.Abs(moveValue);
                rotateValue *= Math.Abs(rotateValue);
            }

            if (moveValue > 0.0)
            {
                if (rotateValue > 0.0)
                {
                    leftMotorSpeed = moveValue - rotateValue;
                    rightMotorSpeed = Math.Max(moveValue, rotateValue);
                }
                else
                {
                    leftMotorSpeed = Math.Max(moveValue, -rotateValue);
                    rightMotorSpeed = moveValue + rotateValue;
                }
            }
            else
            {
                if (rotateValue > 0.0)
                {
                    leftMotorSpeed = -Math.Max(-moveValue, rotateValue);
                    rightMotorSpeed = moveValue + rotateValue;
                }
                else
                {
                    leftMotorSpeed = moveValue - rotateValue;
                    rightMotorSpeed = -Math.Max(-moveValue, -rotateValue);
                }
            }

            SetLeftRightMotorOutputs(leftMotorSpeed, rightMotorSpeed);
        }

        /// <summary>
        /// Cartesian drive method for Mecanum wheeled robots
        /// </summary>
        /// <remarks>A method for driving Mecanum wheeled robots in the cartesian plane.
        /// There are 4 wheels on the robot, arranged so that the front and back wheels are 
        /// toed in 45 degrees. When looking at the wheels from the top, the roller axles should
        /// form an X across the robot.
        /// <para/>This is designed to be directly driven by joystick axes.</remarks>
        /// <param name="x">The speed that the robot should drive in the X direction.</param>
        /// <param name="y">The speed that the robbot should drive in the Y direction.</param>
        /// <param name="rotation">The rate of rotation for the robot that is independed of translation.</param>
        /// <param name="gyroAngle">The current angle reading from the gyro. Use this to implement field-oriented controls.</param>
        public void MecanumDrive_Cartesian(double x, double y, double rotation, double gyroAngle = 0)
        {
            if (!MecanumCartesianReported)
            {
                HAL.Base.HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_MecanumCartesian, (byte)NumMotors);
                MecanumCartesianReported = true;
            }
            double xIn = x;
            double yIn = y;
            // Negate y for the joystick.
            yIn = -yIn;
            // Compenstate for gyro angle.
            RotateVector(ref xIn, ref yIn, gyroAngle);

            double[] wheelSpeeds = new double[MaxNumberOfMotors];
            wheelSpeeds[(int)MotorType.FrontLeft] = xIn + yIn + rotation;
            wheelSpeeds[(int)MotorType.FrontRight] = -xIn + yIn - rotation;
            wheelSpeeds[(int)MotorType.RearLeft] = -xIn + yIn + rotation;
            wheelSpeeds[(int)MotorType.RearRight] = xIn + yIn - rotation;

            Normalize(wheelSpeeds);
            FrontLeftMotor.Set(wheelSpeeds[(int)MotorType.FrontLeft] * MaxOutput, SyncGroup);
            FrontRightMotor.Set(wheelSpeeds[(int)MotorType.FrontRight] * MaxOutput, SyncGroup);
            RearLeftMotor.Set(wheelSpeeds[(int)MotorType.RearLeft] * MaxOutput, SyncGroup);
            RearRightMotor.Set(wheelSpeeds[(int)MotorType.RearRight] * MaxOutput, SyncGroup);

            if (SyncGroup != 0)
            {
                CANJaguar.UpdateSyncGroup(SyncGroup);
            }

            SafetyHelper?.Feed();
        }

        /// <summary>
        /// Polar drive method for Mecanum wheeled robots.
        /// </summary>
        /// <remarks>A method for driving Mecanum wheeled robots in the polar plane.
        /// There are 4 wheels on the robot, arranged so that the front and back wheels are 
        /// toed in 45 degrees. When looking at the wheels from the top, the roller axles should
        /// form an X across the robot.
        /// <para/>This is designed to be directly driven by raw vectors.</remarks>
        /// <param name="magnitude">The magnitude vector</param>
        /// <param name="direction">The direction vector to drive in.</param>
        /// <param name="rotation">The rate of rotation. This is independant from translation.</param>
        public void mecanumDrive_Polar(double magnitude, double direction, double rotation)
        {
            if (!MecanumPolarReported)
            {
                HAL.Base.HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_MecanumPolar, (byte)NumMotors);
                MecanumPolarReported = true;
            }
            // Normalized for full power along the Cartesian axes.
            magnitude = Limit(magnitude) * Math.Sqrt(2.0);
            // The rollers are at 45 degree angles.
            double dirInRad = (direction + 45.0) * 3.14159 / 180.0;
            double cosD = Math.Cos(dirInRad);
            double sinD = Math.Sin(dirInRad);

            double[] wheelSpeeds = new double[MaxNumberOfMotors];
            wheelSpeeds[(int)MotorType.FrontLeft] = (sinD * magnitude + rotation);
            wheelSpeeds[(int)MotorType.FrontRight] = (cosD * magnitude - rotation);
            wheelSpeeds[(int)MotorType.RearLeft] = (cosD * magnitude + rotation);
            wheelSpeeds[(int)MotorType.RearRight] = (sinD * magnitude - rotation);

            Normalize(wheelSpeeds);

            FrontLeftMotor.Set(wheelSpeeds[(int)MotorType.FrontLeft] * MaxOutput, SyncGroup);
            FrontRightMotor.Set(wheelSpeeds[(int)MotorType.FrontRight] * MaxOutput, SyncGroup);
            RearLeftMotor.Set(wheelSpeeds[(int)MotorType.RearLeft] * MaxOutput, SyncGroup);
            RearRightMotor.Set(wheelSpeeds[(int)MotorType.RearRight] * MaxOutput, SyncGroup);

            if (SyncGroup != 0)
            {
                CANJaguar.UpdateSyncGroup(SyncGroup);
            }

            SafetyHelper?.Feed();
        }

        /// <summary>
        /// Sets the speed of the left and right drive motors
        /// </summary>
        /// <param name="leftOutput">The speed to send to the left side.</param>
        /// <param name="rightOutput">The speed to send to the right side.</param>
        public void SetLeftRightMotorOutputs(double leftOutput, double rightOutput)
        {             
            if (RearLeftMotor == null || RearRightMotor == null)
            {
                throw new NullReferenceException("The motor controllers have been set to null.");
            }

            FrontLeftMotor?.Set(Limit(leftOutput) * MaxOutput, SyncGroup);
            RearLeftMotor.Set(Limit(leftOutput) * MaxOutput, SyncGroup);

            FrontRightMotor?.Set(-Limit(rightOutput) * MaxOutput, SyncGroup);
            RearRightMotor.Set(-Limit(rightOutput) * MaxOutput, SyncGroup);

            if (SyncGroup != 0)
            {
                CANJaguar.UpdateSyncGroup(SyncGroup);
            }

            SafetyHelper?.Feed();
        }

        /// <summary>
        /// Limit motor values to the -1.0 to +1.0 ranges
        /// </summary>
        /// <param name="num">The number to limit</param>
        /// <returns>The limited value</returns>
        protected static double Limit(double num)
        {
            if (num > 1.0)
            {
                return 1.0;
            }
            if (num < -1.0)
            {
                return -1.0;
            }
            return num;
        }

        /// <summary>
        /// Normalized all wheel speeds if the magnitude of any wheel is greater the 1.0.
        /// </summary>
        /// <param name="wheelSpeeds">The wheel speeds to normalize</param>
        protected static void Normalize(IList<double> wheelSpeeds)
        {
            if (wheelSpeeds.Count > MaxNumberOfMotors)
                throw new ArgumentOutOfRangeException(nameof(wheelSpeeds), "Not enough motors to normalize provided.");
            double maxMagnitude = Math.Abs(wheelSpeeds[0]);
            int i;
            for (i = 1; i < MaxNumberOfMotors; i++)
            {
                double temp = Math.Abs(wheelSpeeds[i]);
                if (maxMagnitude < temp) maxMagnitude = temp;
            }
            if (maxMagnitude > 1.0)
            {
                for (i = 0; i < MaxNumberOfMotors; i++)
                {
                    wheelSpeeds[i] = wheelSpeeds[i] / maxMagnitude;
                }
            }
        }

        /// <summary>
        /// Rotates a vector in Cartesian Space
        /// </summary>
        /// <param name="x">The X vector</param>
        /// <param name="y">The Y vector</param>
        /// <param name="angle">The angle to rotate in degrees</param>
        protected static void RotateVector(ref double x, ref double y, double angle)
        {
            double cosA = Math.Cos(angle * (3.14159 / 180.0));
            double sinA = Math.Sin(angle * (3.14159 / 180.0));
            double xOut = x * cosA - y * sinA;
            double yOut = x * sinA + y * cosA;
            x = xOut;
            y = yOut;
        }

        /// <summary>
        /// Invert a motor direction
        /// </summary>
        /// <remarks>This is used when a motor should run in the opposite direction
        /// as the drive code would normally run it. Motors that are direct drive would be inverted,
        /// the drive code assumes that the motors are geared with 1 reversal.</remarks>
        /// <param name="motor">The motor index to invert</param>
        /// <param name="isInverted">True if the motor should be inverted.</param>
        public void SetInvertedMotor(MotorType motor, bool isInverted)
        {
            switch (motor)
            {
                case MotorType.FrontLeft:
                    FrontLeftMotor.Inverted = isInverted;
                    break;
                case MotorType.FrontRight:
                    FrontRightMotor.Inverted = isInverted;
                    break;
                case MotorType.RearLeft:
                    RearLeftMotor.Inverted = isInverted;
                    break;
                case MotorType.RearRight:
                    RearRightMotor.Inverted = isInverted;
                    break;
            }
        }

        /// <summary>
        /// Sets the turning sensitivity for the <see cref="Drive(double, double)"/> function.
        /// </summary>
        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
        public double Sensitivity { get; protected set; }

        /// <summary>
        /// Sets the maximum output allowed to be outputed by the drive.
        /// </summary>
        public double MaxOutput { set; protected get; }
        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Global

        /// <summary>
        /// Sets the sync group for the motor controllers if they are <see cref="CANJaguar">CANJaguars</see>
        /// </summary>
        public byte CANJaguarSyncGroup
        {
            set { SyncGroup = value; }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (AllocatedSpeedControllers)
            {
                FrontLeftMotor?.Dispose();
                FrontRightMotor?.Dispose();
                RearLeftMotor?.Dispose();
                RearRightMotor?.Dispose();
            }
        }

        /// <inheritdoc/>
        public bool Alive => SafetyHelper.Alive;

        /// <inheritdoc/>
        public double Expiration
        {
            set { SafetyHelper.Expiration = value; }
            get { return SafetyHelper.Expiration; }
        }

        /// <inheritdoc/>
        public void StopMotor()
        {
            FrontLeftMotor?.StopMotor();
            FrontRightMotor?.StopMotor();
            RearLeftMotor?.StopMotor();
            RearRightMotor?.StopMotor();
            SafetyHelper?.Feed();
        }

        /// <inheritdoc/>
        public string Description => "Robot Drive";

        /// <inheritdoc/>
        public bool SafetyEnabled
        {
            set { SafetyHelper.SafetyEnabled = value; }
            get { return SafetyHelper.SafetyEnabled; }
        }

        private void SetupMotorSafety()
        {
            SafetyHelper = new MotorSafetyHelper(this)
            {
                Expiration = DefaultExpirationTime,
                SafetyEnabled = true
            };
        }

        /// <summary>
        /// Gets the number of motors in the drive.
        /// </summary>
        protected int NumMotors
        {
            get
            {
                int motors = 0;
                if (FrontLeftMotor != null) motors++;
                if (FrontRightMotor != null) motors++;
                if (RearLeftMotor != null) motors++;
                if (RearRightMotor != null) motors++;
                return motors;
            }
        }
    }
}
