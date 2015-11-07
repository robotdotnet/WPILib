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
        public PIDSubsystem(string name, double p, double i, double d)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, this, this);
        }

        public PIDSubsystem(string name, double p, double i, double d, double f)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, f, this, this);
        }

        public PIDSubsystem(string name, double p, double i, double d, double f, double period)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, f, this, this, period);
        }

        public PIDSubsystem(double p, double i, double d)
        {
            PIDController = new PIDController(p, i, d, this, this);
        }

        public PIDSubsystem(double p, double i, double d, double period, double f)
        {
            PIDController = new PIDController(p, i, d, f, this, this, period);
        }

        public PIDSubsystem(double p, double i, double d, double period)
        {
            PIDController = new PIDController(p, i, d, this, this, period);
        }

        public PIDController PIDController { get; }

        public double PositionRelative
        {
            set { SetSetpoint(GetPosition() + value); }
        }

        public void SetSetpoint(double value)
        {
            PIDController.Setpoint = value;
        }

        public double GetSetpoint()
        {
            return PIDController.Setpoint;
        }

        public double GetPosition() => ReturnPIDInput();

        public void SetInputRange(double minimumInput, double maximumInput)
        {
            PIDController.SetInputRange(minimumInput, maximumInput);
        }

        public void SetOutputRange(double minimumOutput, double maximumOutput)
        {
            PIDController.SetOutputRange(minimumOutput, maximumOutput);
        }

        public void SetAbsoluteTolerance(double t)
        {
            PIDController.SetAbsoluteTolerance(t);
        }

        public void SetPercentTolerance(double p)
        {
            PIDController.SetPercentTolerance(p);
        }

        public bool OnTarget() => PIDController.OnTarget();

        protected abstract double ReturnPIDInput();


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
        public new string SmartDashboardType => "PIDSubsystem";
        ///<inheritdoc/>
        public new void InitTable(ITable table)
        {
            PIDController.InitTable(table);
            base.InitTable(table);
        }
    }
}
