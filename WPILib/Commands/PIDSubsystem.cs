using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkTablesDotNet.Tables;
using WPILib.Interfaces;

namespace WPILib.Commands
{
    public abstract class PIDSubsystem : Subsystem, PIDSource, PIDOutput
    {

        private PIDController controller;

        public PIDSubsystem(string name, double p, double i, double d)
            : base(name)
        {
            controller = new PIDController(p, i, d, this, this);
        }

        public PIDSubsystem(string name, double p, double i, double d, double f)
            : base(name)
        {
            controller = new PIDController(p, i, d, f, this, this);
        }

        public PIDSubsystem(string name, double p, double i, double d, double f, double period)
            : base(name)
        {
            controller = new PIDController(p, i, d, f, this, this, period);
        }

        public PIDSubsystem(double p, double i, double d)
        {
            controller = new PIDController(p, i, d, this, this);
        }

        public PIDSubsystem(double p, double i, double d, double period, double f)
        {
            controller = new PIDController(p, i, d, f, this, this, period);
        }

        public PIDSubsystem(double p, double i, double d, double period)
        {
            controller = new PIDController(p, i, d, this, this, period);
        }

        public PIDController GetPIDController()
        {
            return controller;
        }

        public void SetPositionRelative(double deltaSetpoint)
        {
            SetSetpoint(GetPosition() + deltaSetpoint);
        }

        public void SetSetpoint(double setpoint)
        {
            controller.SetSetpoint(setpoint);
        }

        public double GetSetpoint()
        {
            return controller.GetSetpoint();
        }

        public double GetPosition()
        {
            return ReturnPIDInput();
        }

        public void SetInputRange(double minimumInput, double maximumInput)
        {
            controller.SetInputRange(minimumInput, maximumInput);
        }

        public void SetOutputRange(double minimumOutput, double maximumOutput)
        {
            controller.SetOutputRange(minimumOutput, maximumOutput);
        }

        public void SetAbsoluteTolerance(double t)
        {
            controller.SetAbsoluteTolerance(t);
        }

        public void SetPercentTolerance(double p)
        {
            controller.SetPercentTolerance(p);
        }

        public bool OnTarget()
        {
            return controller.OnTarget();
        }

        protected abstract double ReturnPIDInput();

        protected abstract void UsePIDOutput(double output);

        public void Enable()
        {
            controller.Enable();
        }

        public void Disable()
        {
            controller.Disable();
        }

        public double PidGet()
        {
            return ReturnPIDInput();
        }

        public void PidWrite(double output)
        {
            UsePIDOutput(output);
        }

        public new string GetSmartDashboardType()
        {
            return "PIDSubsystem";
        }

        public new void InitTable(ITable table)
        {
            controller.InitTable(table);
            base.InitTable(table);
        }
    }
}
