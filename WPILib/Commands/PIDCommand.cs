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
        /// <param name="p"></param>
        /// <param name="i"></param>
        /// <param name="d"></param>
        protected PIDCommand(double p, double i, double d)
        {
            m_controller = new PIDController(p, i, d, this, this);
        }

        protected PIDCommand(double p, double i, double d, double period)
        {
            m_controller = new PIDController(p, i, d, this, this, period);
        }

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
        protected new virtual void _Initialize()
        {
            m_controller.Enable();
        }
        ///<inheritdoc/>
        protected new virtual void _End()
        {
            m_controller.Disable();
        }
        ///<inheritdoc/>
        protected new virtual void _Interrupted()
        {
            _End();
        }

        public void SetSetpointRelative(double deltaSetpoint)
        {
            SetSetpoint(GetSetpoint() + deltaSetpoint);
        }

        protected void SetSetpoint(double setpoint)
        {
            m_controller.Setpoint = setpoint;
        }

        protected double GetSetpoint()
        {
            return m_controller.Setpoint;
        }

        protected double GetPosition()
        {
            return ReturnPIDInput();
        }

        protected void SetInputRange(double minimumInput, double maximumInput)
        {
            m_controller.SetInputRange(minimumInput, maximumInput);
        }

        protected abstract double ReturnPIDInput();

        protected abstract void UsePIDOutput(double output);
        ///<inheritdoc/>
        public new string SmartDashboardType => "PIDCommand";

        ///<inheritdoc/>
        public new void InitTable(ITable table)
        {
            m_controller.InitTable(table);
            base.InitTable(table);
        }
    }
}
