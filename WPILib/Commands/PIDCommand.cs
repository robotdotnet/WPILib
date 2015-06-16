using NetworkTablesDotNet.Tables;

namespace WPILib.Commands
{
    public abstract class PIDCommand : Command, IPIDSource, IPIDOutput
    {
        private PIDController m_controller;

        ///<inheritdoc/>
        public double PidGet() => ReturnPIDInput();
        ///<inheritdoc/>
        public void PidWrite(double value)
        {
            UsePIDOutput(value);
        }

        public PIDCommand(string name, double p, double i, double d)
            : base(name)
        {
            m_controller = new PIDController(p, i, d, this, this);
        }

        public PIDCommand(double p, double i, double d)
        {
            m_controller = new PIDController(p, i, d, this, this);
        }

        public PIDCommand(double p, double i, double d, double period)
        {
            m_controller = new PIDController(p, i, d, this, this, period);
        }

        public PIDCommand(string name, double p, double i, double d, double period)
            : base(name)
        {
            m_controller = new PIDController(p, i, d, this, this, period);
        }


        protected PIDController GetPIDController() => m_controller;
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
