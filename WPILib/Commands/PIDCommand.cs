using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkTablesDotNet.Tables;
using WPILib.Interfaces;

namespace WPILib.Commands
{
    public abstract class PIDCommand : Command, PIDSource, PIDOutput
    {
        private PIDController m_controller;
        public double PidGet()
        {
            return ReturnPIDInput();
        }

        public void PidWrite(double output)
        {
            UsePIDOutput(output);
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

        protected PIDController GetPIDController()
        {
            return m_controller;
        }

        protected new virtual void _Initialize()
        {
            m_controller.Enable();
        }

        protected new virtual void _End()
        {
            m_controller.Disable();
        }

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
            m_controller.SetSetpoint(setpoint);
        }

        protected double GetSetpoint()
        {
            return m_controller.GetSetpoint();
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

        public new string GetSmartDashboardType()
        {
            return "PIDCommand";
        }
        public new void InitTable(ITable table)
        {
            m_controller.InitTable(table);
            base.InitTable(table);
        }
    }
}
