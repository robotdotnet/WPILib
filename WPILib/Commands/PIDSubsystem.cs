using NetworkTablesDotNet.Tables;

namespace WPILib.Commands
{
    public abstract class IpidSubsystem : Subsystem, IPIDSource, IPIDOutput
    {
        public IpidSubsystem(string name, double p, double i, double d)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, this, this);
        }

        public IpidSubsystem(string name, double p, double i, double d, double f)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, f, this, this);
        }

        public IpidSubsystem(string name, double p, double i, double d, double f, double period)
            : base(name)
        {
            PIDController = new PIDController(p, i, d, f, this, this, period);
        }

        public IpidSubsystem(double p, double i, double d)
        {
            PIDController = new PIDController(p, i, d, this, this);
        }

        public IpidSubsystem(double p, double i, double d, double period, double f)
        {
            PIDController = new PIDController(p, i, d, f, this, this, period);
        }

        public IpidSubsystem(double p, double i, double d, double period)
        {
            PIDController = new PIDController(p, i, d, this, this, period);
        }

        public PIDController PIDController { get; }

        public double PositionRelative
        {
            set { Setpoint = Position + value; }
        }

        public double Setpoint
        {
            get { return PIDController.Setpoint; }
            set { PIDController.Setpoint = value; }
        }

        public double Position => PIDInput;

        public void SetInputRange(double minimumInput, double maximumInput)
        {
            PIDController.SetInputRange(minimumInput, maximumInput);
        }

        public void SetOutputRange(double minimumOutput, double maximumOutput)
        {
            PIDController.SetOutputRange(minimumOutput, maximumOutput);
        }

        public double AbsoluteTolerance
        {
            set { PIDController.SetAbsoluteTolerance(value); }
        }

        public double PercentTolerance
        {
            set { PIDController.SetPercentTolerance(value); }
        }

        public bool OnTarget => PIDController.OnTarget;

        protected abstract double PIDInput { get; }

        protected abstract double PIDOutput { set; }

        public void Enable() => PIDController.Enable();

        public void Disable() => PIDController.Disable();

        public double PidGet() => PIDInput;

        /// <summary>
        /// Set the output to the value calculated by PIDController
        /// </summary>
        /// <param name="value">Output the value calculated by PIDController</param>
        public void PidWrite(double value)
        {
            PIDOutput = value;
        }

        public new string SmartDashboardType => "PIDSubsystem";

        public new void InitTable(ITable table)
        {
            PIDController.InitTable(table);
            base.InitTable(table);
        }
    }
}
