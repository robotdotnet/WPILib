using System;
using HAL.Simulator.Inputs;
using HAL.Simulator.Outputs;

namespace HAL.Simulator.Mechanisms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="FeedbackMechanismBase" />
    public class DriveWheelMechanism : FeedbackMechanismBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DriveWheelMechanism"/> is slipping.
        /// </summary>
        /// <value>
        ///   <c>true</c> if slipping; otherwise, <c>false</c>.
        /// </value>
        public bool Slipping { get; protected set; }
        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        /// <value>
        /// The x position.
        /// </value>
        public double XPosition { get; protected set; }
        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        /// <value>
        /// The y position.
        /// </value>
        public double YPosition { get; protected set; }
        /// <summary>
        /// Gets or sets the r position.
        /// </summary>
        /// <value>
        /// The r position.
        /// </value>
        public double RPosition { get; protected set; }
        /// <summary>
        /// Gets or sets the static coef.
        /// </summary>
        /// <value>
        /// The static coef.
        /// </value>
        public double StaticCoef { get; protected set; }
        /// <summary>
        /// Gets or sets the dynamic coef.
        /// </summary>
        /// <value>
        /// The dynamic coef.
        /// </value>
        public double DynamicCoef { get; protected set; }
        /// <summary>
        /// Gets or sets the wheel diameter.
        /// </summary>
        /// <value>
        /// The wheel diameter.
        /// </value>
        public double WheelDiameter { get; protected set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the static friction.
        /// </summary>
        /// <value>
        /// The static friction.
        /// </value>
        public double StaticFriction { get; protected set; }
        /// <summary>
        /// Gets or sets the dynamic friction.
        /// </summary>
        /// <value>
        /// The dynamic friction.
        /// </value>
        public double DynamicFriction { get; protected set; }

        /// <summary>
        /// Gets or sets the mass load.
        /// </summary>
        /// <value>
        /// The mass load.
        /// </value>
        public double MassLoad { get; protected set; }
        /// <summary>
        /// Gets or sets the force applied.
        /// </summary>
        /// <value>
        /// The force applied.
        /// </value>
        public double ForceApplied { get; protected set; }

        /// <summary>
        /// Gets or sets the motor power.
        /// </summary>
        /// <value>
        /// The motor power.
        /// </value>
        public double MotorPower { get; protected set; }
        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        /// <value>
        /// The acceleration.
        /// </value>
        public double Acceleration { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DriveWheelMechanism"/> class.
        /// </summary>
        /// <param name="motor">The motor.</param>
        /// <param name="input">The input.</param>
        /// <param name="wheelbaseXM">The wheelbase xm.</param>
        /// <param name="wheelDiameterM">The wheel diameter m.</param>
        /// <param name="wheelStaticCoef">The wheel static coef.</param>
        /// <param name="wheelDynamicCoef">The wheel dynamic coef.</param>
        /// <param name="staticFriction">The static friction.</param>
        /// <param name="dynamicFriction">The dynamic friction.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DriveWheelMechanism"/> class.
        /// </summary>
        /// <param name="motor">The motor.</param>
        /// <param name="input">The input.</param>
        /// <param name="wheelbaseXM">The wheelbase xm.</param>
        /// <param name="wheelDiameterM">The wheel diameter m.</param>
        /// <param name="output">The output.</param>
        /// <param name="wheelStaticCoef">The wheel static coef.</param>
        /// <param name="wheelDynamicCoef">The wheel dynamic coef.</param>
        /// <param name="staticFriction">The static friction.</param>
        /// <param name="dynamicFriction">The dynamic friction.</param>
        public DriveWheelMechanism(DCMotor motor, ISimSpeedController input, double wheelbaseXM, double wheelDiameterM, IServoFeedback output,
            double wheelStaticCoef = 1.07, double wheelDynamicCoef = 0.1, double staticFriction = 0.1, double dynamicFriction = 0.01) :
            this(motor, input, wheelbaseXM, wheelDiameterM, wheelStaticCoef, wheelDynamicCoef,
                staticFriction, dynamicFriction)
        {
            m_output = output;
        }

        /// <summary>
        /// UUpdates the mechanism with the specified delta time
        /// </summary>
        /// <param name="seconds">The delta time in seconds.</param>
        /// <param name="driveAcceleration">The drive acceleration.</param>
        /// <param name="massKg">The mass kg.</param>
        /// <param name="numberOfWheels">The number of wheels.</param>
        /// <param name="botVel">The bot vel.</param>
        public void Update(double seconds, double[] driveAcceleration, double massKg, int numberOfWheels, double[] botVel)
        {
            double mechAdvantage = 1 / WheelDiameter;
            double massShare = massKg / numberOfWheels;
            double normalForce = massShare * 9.806; // Multiplied by gravity
            double metersPerSecondPerRadianPerSecond = WheelDiameter / 2;

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

            CurrentRadians += 0.5 * (newRps * CurrentRadiansPerSecond) * seconds;

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
