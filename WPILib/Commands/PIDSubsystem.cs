using NetworkTables.Tables;
using WPILib.Interfaces;

namespace WPILib.Commands
{
    /// <summary>
    /// This class is designed to handle the case where there is a <see cref="Subsystem"/>
    /// which uses a single <see cref="WPILib.PIDController">PIDController</see> almost constantly.
    /// </summary>
    public abstract class PIDSubsystem : Subsystem, IPIDSource, IPIDOutput
    {
        /// <summary>
        /// Instantiates a <see cref="PIDSubsystem"/> that will use the give p, i and d values.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="p">The proportional value</param>
        /// <param name="i">The integral value</param>
        /// <param name="d">The derivative value</param>
        protected PIDSubsystem(string name, double p, double i, double d)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, this, this);
        }

        /// <summary>
        /// Instantiates a <see cref="PIDSubsystem"/> that will use the give p, i, d and f values.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="p">The proportional value</param>
        /// <param name="i">The integral value</param>
        /// <param name="d">The derivative value</param>
        /// <param name="f">The feed forward value</param>
        protected PIDSubsystem(string name, double p, double i, double d, double f)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, f, this, this);
        }
        /// <summary>
        /// Instantiates a <see cref="PIDSubsystem"/> that will use the give p, i, d and f values.
        /// It will also space the time between PID loop calcuulations to be equal to the given period.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="p">The proportional value</param>
        /// <param name="i">The integral value</param>
        /// <param name="d">The derivative value</param>
        /// <param name="f">The feed forward value</param>
        /// <param name="period">The time (in seconds) between calculations</param>
        protected PIDSubsystem(string name, double p, double i, double d, double f, double period)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, f, this, this, period);
        }
        /// <summary>
        /// Instantiates a <see cref="PIDSubsystem"/> that will use the give p, i and d values.
        /// /// It will use the class name as its name.
        /// </summary>
        /// <param name="p">The proportional value</param>
        /// <param name="i">The integral value</param>
        /// <param name="d">The derivative value</param>
        protected PIDSubsystem(double p, double i, double d)
        {
            PIDController = new PIDController(p, i, d, this, this);
        }
        /// <summary>
        /// Instantiates a <see cref="PIDSubsystem"/> that will use the give p, i, d and f values.
        /// It will also space the time between PID loop calcuulations to be equal to the given period.
        /// It will use the class name as its name.
        /// </summary>
        /// <param name="p">The proportional value</param>
        /// <param name="i">The integral value</param>
        /// <param name="d">The derivative value</param>
        /// <param name="f">The feed forward value</param>
        /// <param name="period">The time (in seconds) between calculations</param>
        protected PIDSubsystem(double p, double i, double d, double f, double period)
        {
            PIDController = new PIDController(p, i, d, f, this, this, period);
        }
        /// <summary>
        /// Instantiates a <see cref="PIDSubsystem"/> that will use the give p, i and d values.
        /// It will also space the time between PID loop calcuulations to be equal to the given period.
        /// It will use the class name as its name.
        /// </summary>
        /// <param name="p">The proportional value</param>
        /// <param name="i">The integral value</param>
        /// <param name="d">The derivative value</param>
        /// <param name="period">The time (in seconds) between calculations</param>
        protected PIDSubsystem(double p, double i, double d, double period)
        {
            PIDController = new PIDController(p, i, d, this, this, period);
        }

        /// <summary>
        /// Gets the <see cref="PIDController"/> used by this <see cref="PIDSubsystem"/>.
        /// </summary>
        public PIDController PIDController { get; }

        /// <summary>
        /// Adds the given value to the setpoint.
        /// </summary>
        /// <remarks>
        /// If <see cref="SetInputRange"/> was used, then the 
        /// bounds will still be honored by this method.
        /// </remarks>
        /// <param name="deltaSetpoint">The change in the setpoint.</param>
        public void SetPositionRelative(double deltaSetpoint)
        {
            Setpoint = GetPosition() + deltaSetpoint;
        }

        /// <summary>
        /// Gets or sets the setpoint.
        /// </summary>
        /// <remarks>
        /// If <see cref="SetInputRange"/> was used, then the 
        /// bounds will still be honored when setting this method.
        /// </remarks>
        public double Setpoint
        {
            set { PIDController.Setpoint = value; }
            get { return PIDController.Setpoint; }
        }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <returns>The current position.</returns>
        public double GetPosition() => ReturnPIDInput();

        /// <summary>
        /// Sets the minimum and maximum values expected from the input and setpoint.
        /// </summary>
        /// <param name="minimumInput">The minimum value expected from the input and setpoint.</param>
        /// <param name="maximumInput">The maximum value expected from the input and setpoint.</param>
        public void SetInputRange(double minimumInput, double maximumInput)
        {
            PIDController.SetInputRange(minimumInput, maximumInput);
        }

        /// <summary>
        /// Sets the maximum and minimum values to write.
        /// </summary>
        /// <param name="minimumOutput">The minimum value to write to the output.</param>
        /// <param name="maximumOutput">The maximum value to write to the output.</param>
        public void SetOutputRange(double minimumOutput, double maximumOutput)
        {
            PIDController.SetOutputRange(minimumOutput, maximumOutput);
        }

        /// <summary>
        /// Sets the absolute error which is considered tolerable for use with <see cref="OnTarget"/>.
        /// </summary>
        /// <remarks>The value is in the same range as the PIDInput values.</remarks>
        /// <param name="t">The absolute tolerance.</param>
        public void SetAbsoluteTolerance(double t)
        {
            PIDController.SetAbsoluteTolerance(t);
        }

        /// <summary>
        /// Set the percentage error which is considered tolerable for use with <see cref="OnTarget"/>.
        /// </summary>
        /// <param name="p">The percent tolerance.</param>
        public void SetPercentTolerance(double p)
        {
            PIDController.SetPercentTolerance(p);
        }

        /// <summary>
        /// Gets if the PID system is on target.
        /// </summary>
        /// <remarks>
        /// Returns true if the error is within the percentage of the total input range,
        /// determined by SetTolerance. This assumes theat the maximum and minimum input were
        /// set using <see cref="SetInputRange"/>.
        /// </remarks>
        /// <returns>True if the error is less then the tolerance.</returns>
        public bool OnTarget() => PIDController.OnTarget();

        /// <summary>
        /// Returns the input for the PID Loop.
        /// </summary>
        /// <remarks>
        /// It returns the input for the PID loop, so if this command was based off of a gyro,
        /// then it shoudl return the angle of the gyro.
        /// <para/>
        /// This method will be called in a different thread then the <see cref="Scheduler"/> thread.
        /// </remarks>
        /// <returns>The value the PID Loop should use as input.</returns>
        protected abstract double ReturnPIDInput();

        /// <summary>
        /// Uses the value that the PID Loop Calculated.
        /// </summary>
        /// <remarks>
        /// The calculated value is the "output" parameter. This method is a good time to set motor values,
        /// maybe along the lines of <code>driveline.TankDrive(output, -output)</code>.
        /// <para/>
        /// This method will be called in a different thread then the <see cref="Scheduler"/> thread.
        /// </remarks>
        /// <param name="output">The value the PID loop calculated.</param>
        protected abstract void UsePIDOutput(double output);

        /// <summary>
        /// Enables the internal <see cref="WPILib.PIDController">PIDController</see>
        /// </summary>
        public void Enable() => PIDController.Enable();

        /// <summary>
        /// Disables the internal <see cref="WPILib.PIDController">PIDController</see>
        /// </summary>
        public void Disable() => PIDController.Disable();


        ///<inheritdoc/>
        public double PidGet() => ReturnPIDInput();

        ///<inheritdoc/>
        public PIDSourceType PIDSourceType { get { return PIDSourceType.Displacement; } set { } }

        ///<inheritdoc/>
        public void PidWrite(double value)
        {
            UsePIDOutput(value);
        }

        ///<inheritdoc/>
        public override string SmartDashboardType => "PIDSubsystem";
        ///<inheritdoc/>
        public override void InitTable(ITable table)
        {
            PIDController.InitTable(table);
            base.InitTable(table);
        }
    }
}
