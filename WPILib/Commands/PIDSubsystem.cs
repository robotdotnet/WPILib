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

        private PIDController m_controller;

        public PIDSubsystem(string name, double p, double i, double d)
            : base(name)
        {
            m_controller = new PIDController(p, i, d, this, this);
        }

        public PIDSubsystem(string name, double p, double i, double d, double f)
            : base(name)
        {
            m_controller = new PIDController(p, i, d, f, this, this);
        }

        public PIDSubsystem(string name, double p, double i, double d, double f, double period)
            : base(name)
        {
            m_controller = new PIDController(p, i, d, f, this, this, period);
        }

        public PIDSubsystem(double p, double i, double d)
        {
            m_controller = new PIDController(p, i, d, this, this);
        }

        public PIDSubsystem(double p, double i, double d, double period, double f)
        {
            m_controller = new PIDController(p, i, d, f, this, this, period);
        }

        public PIDSubsystem(double p, double i, double d, double period)
        {
            m_controller = new PIDController(p, i, d, this, this, period);
        }

        public PIDController GetPIDController()
        {
            return m_controller;
        }

        public void SetPositionRelative(double deltaSetpoint)
        {
            SetSetpoint(GetPosition() + deltaSetpoint);
        }

        public void SetSetpoint(double setpoint)
        {
            m_controller.SetSetpoint(setpoint);
        }

        public double GetSetpoint()
        {
            return m_controller.GetSetpoint();
        }

        public double GetPosition()
        {
            return ReturnPIDInput();
        }

        public void SetInputRange(double minimumInput, double maximumInput)
        {
            m_controller.SetInputRange(minimumInput, maximumInput);
        }

        public void SetOutputRange(double minimumOutput, double maximumOutput)
        {
            m_controller.SetOutputRange(minimumOutput, maximumOutput);
        }

        public void SetAbsoluteTolerance(double t)
        {
            m_controller.SetAbsoluteTolerance(t);
        }

        public void SetPercentTolerance(double p)
        {
            m_controller.SetPercentTolerance(p);
        }

        public bool OnTarget()
        {
            return m_controller.OnTarget();
        }

        protected abstract double ReturnPIDInput();

        protected abstract void UsePIDOutput(double output);

        public void Enable()
        {
            m_controller.Enable();
        }

        public void Disable()
        {
            m_controller.Disable();
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
            m_controller.InitTable(table);
            base.InitTable(table);
        }
    }
}
