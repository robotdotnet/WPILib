using NetworkTables.Tables;
using WPILib.Interfaces;

namespace WPILib.Commands
{
    /// <summary>
    /// This class defines a <see cref="Command"/> which interacts heavily with a PID loop.
    /// </summary>
    /// <remarks>It provides some conveniance methods to run an internal <see cref="PIDController"/>.
    /// It will also start and stop said <see cref="PIDController"/> when the <see cref="PIDCommand"/>
    /// is first initialized and ended/interrupted.</remarks>
    public abstract class PIDCommand : Command, IPIDSource, IPIDOutput
    {
        private readonly PIDController m_controller;

        ///<inheritdoc/>
        public double PidGet() => ReturnPIDInput();

        ///<inheritdoc/>
        public PIDSourceType PIDSourceType { get { return PIDSourceType.Displacement; } set { } }

        ///<inheritdoc/>
        public void PidWrite(double value)
        {
            UsePIDOutput(value);
        }

        /// <summary>
        /// Instantiates a <see cref="PIDCommand"/> that will use the given p, i and d values.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="p">The proportional value.</param>
        /// <param name="i">The integral value.</param>
        /// <param name="d">The derivative value.</param>
        protected PIDCommand(string name, double p, double i, double d)
            : base(name)
        {
            m_controller = new PIDController(p, i, d, this, this);
        }

        /// <summary>
        /// Instantiates a <see cref="PIDCommand"/> that will use the given p, i and d values,
        /// and use the class name as its name
        /// </summary>
        /// <param name="p">The proportional value.</param>
        /// <param name="i">The integral value.</param>
        /// <param name="d">The derivative value.</param>
        protected PIDCommand(double p, double i, double d)
        {
            m_controller = new PIDController(p, i, d, this, this);
        }

        /// <summary>
        /// Instantiates a <see cref="PIDCommand"/> that will use the given p, i, and d values,
        /// using the class name as the name, and runs during the specified period.
        /// </summary>
        /// <param name="p">The proportional value.</param>
        /// <param name="i">The integral value.</param>
        /// <param name="d">The derivative value.</param>
        /// <param name="period">The period to run the controller at. (seconds)</param>
        protected PIDCommand(double p, double i, double d, double period)
        {
            m_controller = new PIDController(p, i, d, this, this, period);
        }

        /// <summary>
        /// Instantiates a <see cref="PIDCommand"/> that will use the given p, i and d values,
        /// running using the specifed period.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="p">The proportional value.</param>
        /// <param name="i">The integral value.</param>
        /// <param name="d">The derivative value.</param>
        /// <param name="period">The period to run the controller at. (seconds)</param>
        protected PIDCommand(string name, double p, double i, double d, double period)
            : base(name)
        {
            m_controller = new PIDController(p, i, d, this, this, period);
        }

        /// <summary>
        /// Returns the <see cref="PIDController"/> used by this <see cref="PIDCommand"/>.
        /// </summary>
        /// <remarks>Use this if you would like to fine tune the PID loop.</remarks>
        protected PIDController PIDController => m_controller;

        ///<inheritdoc/>
        protected internal override void _Initialize()
        {
            m_controller.Enable();
        }
        ///<inheritdoc/>
        protected internal override void _End()
        {
            m_controller.Disable();
        }
        ///<inheritdoc/>
        protected internal override void _Interrupted()
        {
            _End();
        }

        /// <summary>
        /// Adds the given value to the setpoint.
        /// </summary>
        /// <remarks>
        /// If <see cref="SetInputRange"/> was used, then the 
        /// bounds will still be honored by this method.
        /// </remarks>
        /// <param name="deltaSetpoint">The change in the setpoint.</param>
        public void SetSetpointRelative(double deltaSetpoint)
        {
            Setpoint = Setpoint + deltaSetpoint;
        }

        /// <summary>
        /// Gets or sets the setpoint.
        /// </summary>
        /// <remarks>
        /// If <see cref="SetInputRange"/> was used, then the 
        /// bounds will still be honored when setting this method.
        /// </remarks>
        protected double Setpoint
        {
            set { m_controller.Setpoint = value; }
            get { return m_controller.Setpoint; }
        }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <returns>The current position.</returns>
        protected double GetPosition()
        {
            return ReturnPIDInput();
        }

        /// <summary>
        /// Sets the minimum and maximum values expected from the input and setpoint.
        /// </summary>
        /// <param name="minimumInput">The minimum value expected from the input and setpoint.</param>
        /// <param name="maximumInput">The maximum value expected from the input and setpoint.</param>
        protected void SetInputRange(double minimumInput, double maximumInput)
        {
            m_controller.SetInputRange(minimumInput, maximumInput);
        }

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

        ///<inheritdoc/>
        public override string SmartDashboardType => "PIDCommand";

        ///<inheritdoc/>
        public override void InitTable(ITable table)
        {
            m_controller.InitTable(table);
            base.InitTable(table);
        }
    }
}
