using System;
using HAL_Base;
using WPILib.Interfaces;

namespace WPILib
{
    /// <summary>The location of a motor on the robot for the purpose of driving.
    /// </summary>
    public enum MotorType
    {
        FrontLeft = 0,
        FrontRight,
        RearLeft,
        RearRight,
    }

    /// <summary>
    /// Simple abstraction for handing robot drivetrains.
    /// </summary>
    public class RobotDrive : IMotorSafety, IDisposable
    {
        protected MotorSafetyHelper m_safetyHelper;

        public static double DefaultExpirationTime = 0.1;
        public static double DefaultSensitivity = 0.5;
        public static double DefaultMaxOutput = 1.0;

        protected static int s_maxNumberOfMotors = 4;
        protected double m_sensitivity;
        protected double m_maxOutput;
        protected ISpeedController m_frontLeftMotor;
        protected ISpeedController m_frontRightMotor;
        protected ISpeedController m_rearLeftMotor;
        protected ISpeedController m_rearRightMotor;
        protected bool m_allocatedSpeedControllers;
        protected byte m_syncGroup;
        protected static bool s_arcadeRatioCurveReported;
        protected static bool s_tankReported;
        protected static bool s_arcadeStandardReported;
        protected static bool s_mecanumCartesianReported;
        protected static bool s_mecanumPolarReported;

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

        public RobotDrive(ISpeedController leftMotor, ISpeedController rightMotor)
        {
            if (leftMotor == null || rightMotor == null)
            {
                m_rearLeftMotor = m_rearRightMotor = null;
                throw new NullReferenceException("Null motor provided");
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

        public RobotDrive(ISpeedController frontLeftMotor, ISpeedController rearLeftMotor,
                      ISpeedController frontRightMotor, ISpeedController rearRightMotor)
        {
            if (frontLeftMotor == null || rearLeftMotor == null || frontRightMotor == null || rearRightMotor == null)
            {
                m_frontLeftMotor = m_rearLeftMotor = m_frontRightMotor = m_rearRightMotor = null;
                throw new NullReferenceException("Null motor provided");
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

        public void TankDrive(GenericHID leftStick, GenericHID rightStick)
        {
            if (leftStick == null || rightStick == null)
            {
                throw new NullReferenceException("Null HID provided");
            }
            TankDrive(leftStick.GetY(), rightStick.GetY(), true);
        }

        public void TankDrive(GenericHID leftStick, GenericHID rightStick, bool squaredInputs)
        {
            if (leftStick == null || rightStick == null)
            {
                throw new NullReferenceException("Null HID provided");
            }
            TankDrive(leftStick.GetY(), rightStick.GetY(), squaredInputs);
        }

        public void TankDrive(GenericHID leftStick, int leftAxis,
                          GenericHID rightStick, int rightAxis)
        {
            if (leftStick == null || rightStick == null)
            {
                throw new NullReferenceException("Null HID provided");
            }
            TankDrive(leftStick.GetRawAxis(leftAxis), rightStick.GetRawAxis(rightAxis), true);
        }

        public void TankDrive(GenericHID leftStick, int leftAxis,
                          GenericHID rightStick, int rightAxis, bool squaredInputs)
        {
            if (leftStick == null || rightStick == null)
            {
                throw new NullReferenceException("Null HID provided");
            }
            TankDrive(leftStick.GetRawAxis(leftAxis), rightStick.GetRawAxis(rightAxis), squaredInputs);
        }

        public void TankDrive(double leftValue, double rightValue, bool squaredInputs)
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

        public void TankDrive(double leftValue, double rightValue)
        {
            TankDrive(leftValue, rightValue, true);
        }

        public void ArcadeDrive(GenericHID stick, bool squaredInputs)
        {
            ArcadeDrive(stick.GetY(), stick.GetX(), squaredInputs);
        }

        public void ArcadeDrive(GenericHID stick)
        {
            ArcadeDrive(stick, true);
        }

        public void ArcadeDrive(GenericHID moveStick, int moveAxis, GenericHID rotateStick, int rotateAxis,
            bool squaredInputs)
        {
            double moveValue = moveStick.GetRawAxis(moveAxis);
            double rotateValue = rotateStick.GetRawAxis(rotateAxis);

            ArcadeDrive(moveValue, rotateValue, squaredInputs);
        }

        public void ArcadeDrive(GenericHID moveStick, int moveAxis, GenericHID rotateStick, int rotateAxis)
        {
            double moveValue = moveStick.GetRawAxis(moveAxis);
            double rotateValue = rotateStick.GetRawAxis(rotateAxis);

            ArcadeDrive(moveValue, rotateValue, true);
        }

        public void ArcadeDrive(double moveValue, double rotateValue, bool squaredInputs)
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

        public void ArcadeDrive(double moveValue, double rotateValue)
        {
            ArcadeDrive(moveValue, rotateValue, true);
        }

        public void MecanumDrive_Cartesian(double x, double y, double rotation, double gyroAngle)
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

        public void SetLeftRightMotorOutputs(double leftOutput, double rightOutput)
        {
            if (m_rearLeftMotor == null || m_rearRightMotor == null)
            {
                throw new NullReferenceException("Null motor provided");
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

        protected static void Normalize(double[] wheelSpeeds)
        {
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
