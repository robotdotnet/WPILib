using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    public class DriveWheelMechanism : FeedbackMechanismBase
    {
        public bool Slipping { get; protected set; }
        public double XPosition { get; protected set; }
        public double YPosition { get; protected set; }
        public double RPosition { get; protected set; }
        public double StaticCoef { get; protected set; }
        public double DynamicCoef { get; protected set; }
        public double WheelDiameter { get; protected set; }
        public string Type { get; set; }

        public double StaticFriction { get; protected set; }
        public double DynamicFriction { get; protected set; }

        public double MassLoad { get; protected set; }
        public double ForceApplied { get; protected set; }

        public double MotorPower { get; protected set; }
        public double Acceleration { get; protected set; }

        public DriveWheelMechanism(DCMotor motor, ISimSpeedController input, double wheelbaseXM, double wheelDiameterM,
            double wheelStaticCoef = 1.07, double wheelDynamicCoef = 0.1, double staticFriction = 0.1, double dynamicFriction = 0.01)
        {
            m_model = motor;
            m_output = null;
            m_input = input;
            XPosition = -wheelbaseXM;
            StaticCoef = wheelStaticCoef;
            DynamicCoef = wheelDynamicCoef;
            WheelDiameter = wheelDiameterM;
            StaticFriction = staticFriction;
            DynamicFriction = dynamicFriction;
        }

        public DriveWheelMechanism(DCMotor motor, ISimSpeedController input, double wheelbaseXM, double wheelDiameterM, IServoFeedback output,
            double wheelStaticCoef = 1.07, double wheelDynamicCoef = 0.1, double staticFriction = 0.1, double dynamicFriction = 0.01) : 
            this(motor, input, wheelbaseXM, wheelDiameterM, wheelStaticCoef, wheelDynamicCoef, 
                staticFriction, dynamicFriction)
        {
            m_output = output;
        }

        public void Update(double seconds, double[] driveAcceleration, double massKg, int numberOfWheels, double[] botVel)
        {
            double mechAdvantage = 1 / WheelDiameter;
            double massShare = massKg / numberOfWheels;
            double normalForce = massShare * 9.806; // Multiplied by gravity
            double metersPerSecondPerRadianPerSecond = (WheelDiameter/2);

            double ySpeed = botVel[1] + botVel[2] / XPosition;
            double xSpeed = botVel[0];
            double groundMetersPerSecond = Math.Sqrt(ySpeed * ySpeed + xSpeed * xSpeed);

            double wheelMetersPerSecond = CurrentRadiansPerSecond * metersPerSecondPerRadianPerSecond;

            if (Math.Abs(groundMetersPerSecond - wheelMetersPerSecond) < 0.1)
            {
                //No Slip
                Slipping = false;
                MassLoad = massShare * mechAdvantage;
                ForceApplied = -ySpeed * DynamicFriction;
            }
            else
            {
                Slipping = true;
                MassLoad = 1;
                ForceApplied = (ySpeed - wheelMetersPerSecond) * DynamicCoef * normalForce / mechAdvantage;
            }

            MotorPower = (1 - CurrentRadiansPerSecond / m_model.MaxSpeed) * m_model.MaxTorque *
                         Math.Max(Math.Min(m_input.Get(), 1), -1);
            Acceleration = MotorPower / MassLoad + ForceApplied;

            double newRps = CurrentRadiansPerSecond + Acceleration * seconds;

            CurrentRadians += 0.5*(newRps * CurrentRadiansPerSecond) * seconds;

            CurrentRadiansPerSecond = newRps;

            double newMetersPerSecond = CurrentRadiansPerSecond * metersPerSecondPerRadianPerSecond;
            double wheelForce = (newMetersPerSecond - wheelMetersPerSecond) / seconds * massShare;

            if (wheelForce > StaticCoef * normalForce)
            {
                Slipping = true;
                wheelForce = (newMetersPerSecond - wheelMetersPerSecond) / seconds * DynamicCoef * normalForce;
            }

            driveAcceleration[1] += wheelForce / massKg;
            driveAcceleration[2] += wheelForce / massKg / XPosition;



        }


        public override void Update(double seconds)
        {
            //We need more, so do nothing if this is called.
        }
    }
}
