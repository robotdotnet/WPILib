using System;
using HAL_Base;
using WPILib.Interfaces;
using System.Collections.Generic;

namespace WPILib
{
    /// The location of a motor on the robot for the purpose of driving.
    public enum MotorType
    {
        FrontLeft = 0,
        FrontRight,
        RearLeft,
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
        protected MotorSafetyHelper m_safetyHelper;

        /// Default Expiration Time for MotorSafety
        public const double DefaultExpirationTime = 0.1;
        /// Default Sensitivity for the <see cref="Drive(double, double)"/> function
        public const double DefaultSensitivity = 0.5;
        /// Default Max Output for the motors
        public const double DefaultMaxOutput = 1.0;

        /// The Default max number of motors
        protected static int s_maxNumberOfMotors = 4;
        /// The current motor sensitivity
        protected double m_sensitivity;
        /// The current max output
        protected double m_maxOutput;
        /// The Front Left Motor
        protected ISpeedController m_frontLeftMotor;
        /// The Front Right Motor
        protected ISpeedController m_frontRightMotor;
        /// The Rear Left Motor
        protected ISpeedController m_rearLeftMotor;
        /// The Rear Right Motor
        protected ISpeedController m_rearRightMotor;
        /// Returns if this class allocated the speed controllers or was passed them.
        protected bool m_allocatedSpeedControllers;
        /// The SyncGroup for Jaguar Motor Controllers
        protected byte m_syncGroup;

        /// Usage Reporters
        protected static bool s_arcadeRatioCurveReported;
        /// <inheritdoc cref="s_arcadeRatioCurveReported"/>
        protected static bool s_tankReported;
        /// <inheritdoc cref="s_arcadeRatioCurveReported"/>
        protected static bool s_arcadeStandardReported;
        /// <inheritdoc cref="s_arcadeRatioCurveReported"/>
        protected static bool s_mecanumCartesianReported;
        /// <inheritdoc cref="s_arcadeRatioCurveReported"/>
        protected static bool s_mecanumPolarReported;

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
            m_sensitivity = DefaultSensitivity;
            m_maxOutput = DefaultMaxOutput;
            m_frontLeftMotor = null;
            m_rearLeftMotor = new Talon(leftMotorChannel);
            m_frontRightMotor = null;
            m_rearRightMotor = new Talon(rightMotorChannel);
            m_allocatedSpeedControllers = true;
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
            m_sensitivity = DefaultSensitivity;
            m_maxOutput = DefaultMaxOutput;
            m_rearLeftMotor = new Talon(rearLeftMotor);
            m_rearRightMotor = new Talon(rearRightMotor);
            m_frontLeftMotor = new Talon(frontLeftMotor);
            m_frontRightMotor = new Talon(frontRightMotor);
            m_allocatedSpeedControllers = true;
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
            m_frontLeftMotor = null;
            m_rearLeftMotor = leftMotor;
            m_frontRightMotor = null;
            m_rearRightMotor = rightMotor;
            m_sensitivity = DefaultSensitivity;
            m_maxOutput = DefaultMaxOutput;
            m_allocatedSpeedControllers = false;
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
            m_frontLeftMotor = frontLeftMotor;
            m_rearLeftMotor = rearLeftMotor;
            m_frontRightMotor = frontRightMotor;
            m_rearRightMotor = rearRightMotor;
            m_sensitivity = DefaultSensitivity;
            m_maxOutput = DefaultMaxOutput;
            m_allocatedSpeedControllers = false;
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

            if (!s_arcadeRatioCurveReported)
            {
                HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_ArcadeRatioCurve, (byte)GetNumMotors());
                s_arcadeRatioCurveReported = true;
            }
            if (curve < 0)
            {
                double value = Math.Log(-curve);
                double ratio = (value - m_sensitivity) / (value + m_sensitivity);
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
                double ratio = (value - m_sensitivity) / (value + m_sensitivity);
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
            if (!s_tankReported)
            {
                HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_Tank, (byte)GetNumMotors());
                s_tankReported = true;
            }

            // square the inputs (while preserving the sign) to increase fine control while permitting full power
            leftValue = Limit(leftValue);
            rightValue = Limit(rightValue);
            if (squaredInputs)
            {
                if (leftValue >= 0.0)
                {
                    leftValue = (leftValue * leftValue);
                }
                else
                {
                    leftValue = -(leftValue * leftValue);
                }
                if (rightValue >= 0.0)
                {
                    rightValue = (rightValue * rightValue);
                }
                else
                {
                    rightValue = -(rightValue * rightValue);
                }
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
            if (!s_arcadeStandardReported)
            {
                HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_ArcadeStandard, (byte)GetNumMotors());
                s_arcadeStandardReported = true;
            }

            double leftMotorSpeed;
            double rightMotorSpeed;

            moveValue = Limit(moveValue);
            rotateValue = Limit(rotateValue);

            if (squaredInputs)
            {
                // square the inputs (while preserving the sign) to increase fine control while permitting full power
                if (moveValue >= 0.0)
                {
                    moveValue = (moveValue * moveValue);
                }
                else
                {
                    moveValue = -(moveValue * moveValue);
                }
                if (rotateValue >= 0.0)
                {
                    rotateValue = (rotateValue * rotateValue);
                }
                else
                {
                    rotateValue = -(rotateValue * rotateValue);
                }
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
            if (!s_mecanumCartesianReported)
            {
                HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_MecanumCartesian, (byte)GetNumMotors());
                s_mecanumCartesianReported = true;
            }
            double xIn = x;
            double yIn = y;
            // Negate y for the joystick.
            yIn = -yIn;
            // Compenstate for gyro angle.
            double[] rotated = RotateVector(xIn, yIn, gyroAngle);
            xIn = rotated[0];
            yIn = rotated[1];

            double[] wheelSpeeds = new double[s_maxNumberOfMotors];
            wheelSpeeds[(int)MotorType.FrontLeft] = xIn + yIn + rotation;
            wheelSpeeds[(int)MotorType.FrontRight] = -xIn + yIn - rotation;
            wheelSpeeds[(int)MotorType.RearLeft] = -xIn + yIn + rotation;
            wheelSpeeds[(int)MotorType.RearRight] = xIn + yIn - rotation;

            Normalize(wheelSpeeds);
            m_frontLeftMotor.Set(wheelSpeeds[(int)MotorType.FrontLeft] * m_maxOutput, m_syncGroup);
            m_frontRightMotor.Set(wheelSpeeds[(int)MotorType.FrontRight] * m_maxOutput, m_syncGroup);
            m_rearLeftMotor.Set(wheelSpeeds[(int)MotorType.RearLeft] * m_maxOutput, m_syncGroup);
            m_rearRightMotor.Set(wheelSpeeds[(int)MotorType.RearRight] * m_maxOutput, m_syncGroup);

            if (m_syncGroup != 0)
            {
                CANJaguar.UpdateSyncGroup(m_syncGroup);
            }

            m_safetyHelper?.Feed();
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
            if (!s_mecanumPolarReported)
            {
                HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_MecanumPolar, (byte)GetNumMotors());
                s_mecanumPolarReported = true;
            }
            // Normalized for full power along the Cartesian axes.
            magnitude = Limit(magnitude) * Math.Sqrt(2.0);
            // The rollers are at 45 degree angles.
            double dirInRad = (direction + 45.0) * 3.14159 / 180.0;
            double cosD = Math.Cos(dirInRad);
            double sinD = Math.Sin(dirInRad);

            double[] wheelSpeeds = new double[s_maxNumberOfMotors];
            wheelSpeeds[(int)MotorType.FrontLeft] = (sinD * magnitude + rotation);
            wheelSpeeds[(int)MotorType.FrontRight] = (cosD * magnitude - rotation);
            wheelSpeeds[(int)MotorType.RearLeft] = (cosD * magnitude + rotation);
            wheelSpeeds[(int)MotorType.RearRight] = (sinD * magnitude - rotation);

            Normalize(wheelSpeeds);

            m_frontLeftMotor.Set(wheelSpeeds[(int)MotorType.FrontLeft] * m_maxOutput, m_syncGroup);
            m_frontRightMotor.Set(wheelSpeeds[(int)MotorType.FrontRight] * m_maxOutput, m_syncGroup);
            m_rearLeftMotor.Set(wheelSpeeds[(int)MotorType.RearLeft] * m_maxOutput, m_syncGroup);
            m_rearRightMotor.Set(wheelSpeeds[(int)MotorType.RearRight] * m_maxOutput, m_syncGroup);

            if (m_syncGroup != 0)
            {
                CANJaguar.UpdateSyncGroup(m_syncGroup);
            }

            m_safetyHelper?.Feed();
        }

        /// <summary>
        /// Sets the speed of the left and right drive motors
        /// </summary>
        /// <param name="leftOutput">The speed to send to the left side.</param>
        /// <param name="rightOutput">The speed to send to the right side.</param>
        public void SetLeftRightMotorOutputs(double leftOutput, double rightOutput)
        {             
            if (m_rearLeftMotor == null || m_rearRightMotor == null)
            {
                throw new NullReferenceException("The motor controllers have been set to null.");
            }

            m_frontLeftMotor?.Set(Limit(leftOutput) * m_maxOutput, m_syncGroup);
            m_rearLeftMotor.Set(Limit(leftOutput) * m_maxOutput, m_syncGroup);

            m_frontRightMotor?.Set(-Limit(rightOutput) * m_maxOutput, m_syncGroup);
            m_rearRightMotor.Set(-Limit(rightOutput) * m_maxOutput, m_syncGroup);

            if (m_syncGroup != 0)
            {
                CANJaguar.UpdateSyncGroup(m_syncGroup);
            }

            m_safetyHelper?.Feed();
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
            if (wheelSpeeds.Count > s_maxNumberOfMotors)
                throw new ArgumentOutOfRangeException(nameof(wheelSpeeds), "Not enough motors to normalize provided.");
            double maxMagnitude = Math.Abs(wheelSpeeds[0]);
            int i;
            for (i = 1; i < s_maxNumberOfMotors; i++)
            {
                double temp = Math.Abs(wheelSpeeds[i]);
                if (maxMagnitude < temp) maxMagnitude = temp;
            }
            if (maxMagnitude > 1.0)
            {
                for (i = 0; i < s_maxNumberOfMotors; i++)
                {
                    wheelSpeeds[i] = wheelSpeeds[i] / maxMagnitude;
                }
            }
        }
        protected static double[] RotateVector(double x, double y, double angle)
        {
            double cosA = Math.Cos(angle * (3.14159 / 180.0));
            double sinA = Math.Sin(angle * (3.14159 / 180.0));
            double[] output = new double[2];
            output[0] = x * cosA - y * sinA;
            output[1] = x * sinA + y * cosA;
            return output;
        }

        public void SetInvertedMotor(MotorType motor, bool isInverted)
        {
            switch (motor)
            {
                case MotorType.FrontLeft:
                    m_frontLeftMotor.Inverted = isInverted;
                    break;
                case MotorType.FrontRight:
                    m_frontRightMotor.Inverted = isInverted;
                    break;
                case MotorType.RearLeft:
                    m_rearLeftMotor.Inverted = isInverted;
                    break;
                case MotorType.RearRight:
                    m_rearRightMotor.Inverted = isInverted;
                    break;
            }
        }

        public double Sensitivity
        {
            set { m_sensitivity = value; }
        }

        public double MaxOutput
        {
            set { m_maxOutput = value; }
        }

        public byte CANJaguarSyncGroup
        {
            set { m_syncGroup = value; }
        }

        public void Dispose()
        {
            if (m_allocatedSpeedControllers)
            {
                if (m_frontLeftMotor != null)
                {
                    ((PWM)m_frontLeftMotor).Dispose();
                }
                if (m_frontRightMotor != null)
                {
                    ((PWM)m_frontRightMotor).Dispose();
                }
                if (m_rearLeftMotor != null)
                {
                    ((PWM)m_rearLeftMotor).Dispose();
                }
                if (m_rearRightMotor != null)
                {
                    ((PWM)m_rearRightMotor).Dispose();
                }
            }
        }

        public bool Alive => m_safetyHelper.Alive;

        public double Expiration
        {
            set { m_safetyHelper.Expiration = value; }
            get { return m_safetyHelper.Expiration; }
        }

        public void StopMotor()
        {
            m_frontLeftMotor.Set(0.0);
            m_frontRightMotor.Set(0.0);
            m_rearLeftMotor.Set(0.0);
            m_rearRightMotor.Set(0.0);
            m_safetyHelper?.Feed();
        }

        public string Description => "Robot Drive";

        public bool SafetyEnabled
        {
            set { m_safetyHelper.SafetyEnabled = value; }
            get { return m_safetyHelper.SafetyEnabled; }
        }

        private void SetupMotorSafety()
        {
            m_safetyHelper = new MotorSafetyHelper(this)
            {
                Expiration = DefaultExpirationTime,
                SafetyEnabled = true
            };
        }

        protected int GetNumMotors()
        {
            int motors = 0;
            if (m_frontLeftMotor != null) motors++;
            if (m_frontRightMotor != null) motors++;
            if (m_rearLeftMotor != null) motors++;
            if (m_rearRightMotor != null) motors++;
            return motors;
        }
    }
}
