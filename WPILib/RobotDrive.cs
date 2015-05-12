using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_RoboRIO;

namespace WPILib
{
    public enum MotorType
    {
        kFrontLeft = 0,
        kFrontRight,
        kRearLeft,
        kRearRight,
    }

    public class RobotDrive : MotorSafety
    {
        protected MotorSafetyHelper _safetyHelper;

        public static double kDefaultExpirationTime = 0.1;
        public static double kDefaultSensitivity = 0.5;
        public static double kDefaultMaxOutput = 1.0;

        protected static int kMaxNumberOfMotors = 4;
        protected int[] m_invertedMotors = new int[4];
        protected double m_sensitivity;
        protected double m_maxOutput;
        protected SpeedController m_frontLeftMotor;
        protected SpeedController m_frontRightMotor;
        protected SpeedController m_rearLeftMotor;
        protected SpeedController m_rearRightMotor;
        protected bool m_allocatedSpeedControllers;
        protected byte m_syncGroup = 0;
        protected static bool kArcadeRatioCurve_Reported = false;
        protected static bool kTank_Reported = false;
        protected static bool kArcadeStandard_Reported = false;
        protected static bool kMecanumCartesian_Reported = false;
        protected static bool kMecanumPolar_Reported = false;

        public RobotDrive(int leftMotorChannel, int rightMotorChannel)
        {
            m_sensitivity = kDefaultSensitivity;
            m_maxOutput = kDefaultMaxOutput;
            m_frontLeftMotor = null;
            m_rearLeftMotor = new Talon(leftMotorChannel);
            m_frontRightMotor = null;
            m_rearRightMotor = new Talon(rightMotorChannel);
            for (int i = 0; i < kMaxNumberOfMotors; i++)
            {
                m_invertedMotors[i] = 1;
            }
            m_allocatedSpeedControllers = true;
            SetupMotorSafety();
            drive(0, 0);
        }

        public RobotDrive(int frontLeftMotor, int rearLeftMotor,
                       int frontRightMotor, int rearRightMotor)
        {
            m_sensitivity = kDefaultSensitivity;
            m_maxOutput = kDefaultMaxOutput;
            m_rearLeftMotor = new Talon(rearLeftMotor);
            m_rearRightMotor = new Talon(rearRightMotor);
            m_frontLeftMotor = new Talon(frontLeftMotor);
            m_frontRightMotor = new Talon(frontRightMotor);
            for (int i = 0; i < kMaxNumberOfMotors; i++)
            {
                m_invertedMotors[i] = 1;
            }
            m_allocatedSpeedControllers = true;
            SetupMotorSafety();
            drive(0, 0);
        }

        public RobotDrive(SpeedController leftMotor, SpeedController rightMotor)
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
            m_sensitivity = kDefaultSensitivity;
            m_maxOutput = kDefaultMaxOutput;
            for (int i = 0; i < kMaxNumberOfMotors; i++)
            {
                m_invertedMotors[i] = 1;
            }
            m_allocatedSpeedControllers = false;
            SetupMotorSafety();
            drive(0, 0);
        }

        public RobotDrive(SpeedController frontLeftMotor, SpeedController rearLeftMotor,
                      SpeedController frontRightMotor, SpeedController rearRightMotor)
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
            m_sensitivity = kDefaultSensitivity;
            m_maxOutput = kDefaultMaxOutput;
            for (int i = 0; i < kMaxNumberOfMotors; i++)
            {
                m_invertedMotors[i] = 1;
            }
            m_allocatedSpeedControllers = false;
            SetupMotorSafety();
            drive(0, 0);
        }

        public void drive(double outputMagnitude, double curve)
        {
            double leftOutput, rightOutput;

            if (!kArcadeRatioCurve_Reported)
            {
                HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_ArcadeRatioCurve, (byte)GetNumMotors());
                kArcadeRatioCurve_Reported = true;
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

            if (!kTank_Reported)
            {
                HAL.Report(ResourceType.kResourceType_RobotDrive, Instances.kRobotDrive_Tank, (byte)GetNumMotors());
                kTank_Reported = true;
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

        public void SetLeftRightMotorOutputs(double leftOutput, double rightOutput)
        {
            if (m_rearLeftMotor == null || m_rearRightMotor == null)
            {
                throw new NullReferenceException("Null motor provided");
            }

            if (m_frontLeftMotor != null)
            {
                m_frontLeftMotor.Set(Limit(leftOutput) * m_invertedMotors[(int)MotorType.kFrontLeft] * m_maxOutput, m_syncGroup);
            }
            m_rearLeftMotor.Set(Limit(leftOutput) * m_invertedMotors[(int)MotorType.kRearLeft] * m_maxOutput, m_syncGroup);

            if (m_frontRightMotor != null)
            {
                m_frontRightMotor.Set(-Limit(rightOutput) * m_invertedMotors[(int)MotorType.kFrontRight] * m_maxOutput, m_syncGroup);
            }
            m_rearRightMotor.Set(-Limit(rightOutput) * m_invertedMotors[(int)MotorType.kRearRight] * m_maxOutput, m_syncGroup);

            if (this.m_syncGroup != 0)
            {
                //CANJaguar.updateSyncGroup(m_syncGroup);
            }

            if (_safetyHelper != null) _safetyHelper.Feed();
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

        public bool IsAlive()
        {
            return _safetyHelper.IsAlive();
        }

        public void SetExpiration(double timeout)
        {
            _safetyHelper.SetExpiration(timeout);
        }

        public double GetExpiration()
        {
            return _safetyHelper.GetExpiration();
        }

        public void StopMotor()
        {
            if (m_frontLeftMotor != null)
            {
                m_frontLeftMotor.Set(0.0);
            }
            if (m_frontRightMotor != null)
            {
                m_frontRightMotor.Set(0.0);
            }
            if (m_rearLeftMotor != null)
            {
                m_rearLeftMotor.Set(0.0);
            }
            if (m_rearRightMotor != null)
            {
                m_rearRightMotor.Set(0.0);
            }
            if (_safetyHelper != null) _safetyHelper.Feed();
        }

        public string GetDescription()
        {
            return "Robot Drive";
        }

        public bool IsSafetyEnabled()
        {
            return _safetyHelper.IsSafetyEnabled();
        }

        public void SetSafetyEnabled(bool enabled)
        {
            _safetyHelper.SetSafetyEnabled(enabled);
        }

        private void SetupMotorSafety()
        {
            _safetyHelper = new MotorSafetyHelper(this);
            _safetyHelper.SetExpiration(kDefaultExpirationTime);
            _safetyHelper.SetSafetyEnabled(true);
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
