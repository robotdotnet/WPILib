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
        private PIDController controller;
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
            controller = new PIDController(p, i, d, this, this);
        }

        public PIDCommand(double p, double i, double d)
        {
            controller = new PIDController(p, i, d, this, this);
        }

        public PIDCommand(double p, double i, double d, double period)
        {
            controller = new PIDController(p, i, d, this, this, period);
        }

        public PIDCommand(string name, double p, double i, double d, double period)
            : base(name)
        {
            controller = new PIDController(p, i, d, this, this, period);
        }

        protected PIDController GetPIDController()
        {
            return controller;
        }

        protected void _initialize()
        {
            controller.Enable();
        }

        protected void _end()
        {
            controller.Disable();
        }

        protected void _interrupted()
        {
            _end();
        }

        public void SetSetpointRelative(double deltaSetpoint)
        {
            SetSetpoint(GetSetpoint() + deltaSetpoint);
        }

        protected void SetSetpoint(double setpoint)
        {
            controller.SetSetpoint(setpoint);
        }

        protected double GetSetpoint()
        {
            return controller.GetSetpoint();
        }

        protected double GetPosition()
        {
            return ReturnPIDInput();
        }

        protected void SetInputRange(double minimumInput, double maximumInput)
        {
            controller.SetInputRange(minimumInput, maximumInput);
        }

        protected abstract double ReturnPIDInput();

        protected abstract void UsePIDOutput(double output);

        public new string GetSmartDashboardType()
        {
            return "PIDCommand";
        }
        public new void InitTable(ITable table)
        {
            controller.InitTable(table);
            base.InitTable(table);
        }
    }
}
