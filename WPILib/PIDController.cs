using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NetworkTablesDotNet.Tables;
using WPILib.Interfaces;
using WPILib.livewindow;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{

    public class PIDController : Controller, LiveWindowSendable, ITableListener
    {
        public enum ToleranceType
        {
            AbsoluteTolerance,
            PercentTolerance,
            NoTolerance,
        }

        private ToleranceType m_toleranceType;


        public const double DefaultPeriod = 0.05;

        private static int s_instances = 0;

// ReSharper disable InconsistentNaming
        private double m_P;     // factor for "proportional" control
        private double m_I;     // factor for "integral" control
        private double m_D;     // factor for "derivative" control
        private double m_F;                 // factor for feedforward term
// ReSharper restore InconsistentNaming
        private double m_maximumOutput = 1.0; // |maximum output|
        private double m_minimumOutput = -1.0;  // |minimum output|
        private double m_maximumInput = 0.0;    // maximum input - limit setpoint to this
        private double m_minimumInput = 0.0;    // minimum input - limit setpoint to this
        private bool m_continuous = false; // do the endpoints wrap around? eg. Absolute encoder
        private bool m_enabled = false;    //is the pid controller enabled
        private double m_prevError = 0.0; // the prior sensor input (used to compute velocity)
        private double m_totalError = 0.0; //the sum of the errors for use in the integral calc
        private double m_setpoint = 0.0;
        private double m_error = 0.0;
        private double m_result = 0.0;
        private double m_period = DefaultPeriod;
        PIDSource m_pidInput;
        PIDOutput m_pidOutput;
        private object m_lockObject = new object();

        private double m_tolerance = 0.05;

        private Notifier m_controlLoop;


        public PIDController(double Kp, double Ki, double Kd, double Kf,
            PIDSource source, PIDOutput output,
            double period)
        {
            if (source == null)
                throw new NullReferenceException("Null PIDSource was given");
            if (output == null)
                throw new NullReferenceException("Null PIDOutput was given");

            m_controlLoop = new Notifier(CallCalculate, this);

            m_P = Kp;
            m_I = Ki;
            m_D = Kd;
            m_F = Kf;

            m_pidInput = source;
            m_pidOutput = output;
            m_period = period;

            m_controlLoop.StartPeriodic(m_period);

            s_instances++;
            HLUsageReporting.ReportPIDController(s_instances);

            m_toleranceType = ToleranceType.NoTolerance;
        }

        public PIDController(double Kp, double Ki, double Kd,
            PIDSource source, PIDOutput output,
            double period)
            : this(Kp, Ki, Kd, 0.0, source, output, period)
        {

        }

        public PIDController(double Kp, double Ki, double Kd,
            PIDSource source, PIDOutput output)
            : this(Kp, Ki, Kd, source, output, DefaultPeriod)
        {

        }

        public PIDController(double Kp, double Ki, double Kd, double Kf,
            PIDSource source, PIDOutput output)
            : this(Kp, Ki, Kd, Kf, source, output, DefaultPeriod)
        {

        }

        public void Free()
        {
            m_controlLoop.Stop();
            lock (this)
            {
                m_pidOutput = null;
                m_pidInput = null;
                m_controlLoop = null;
            }
        }


        public static void CallCalculate(object o)
        {
            PIDController controller = (PIDController)o;
            controller.Calculate();
        }

        private void Calculate()
        {
            bool enabled;
            PIDSource pidInput;

            lock (this)
            {
                if (m_pidInput == null)
                {
                    return;
                }
                if (m_pidOutput == null)
                {
                    return;
                }
                enabled = m_enabled; // take snapshot of these values...
                pidInput = m_pidInput;
            }

            if (enabled)
            {
                double input;
                double result;
                PIDOutput pidOutput = null;
                lock (this)
                {
                    input = pidInput.PidGet();
                }
                lock (this)
                {
                    m_error = m_setpoint - input;
                    if (m_continuous)
                    {
                        if (Math.Abs(m_error) > (m_maximumInput - m_minimumInput) / 2)
                        {
                            if (m_error > 0)
                            {
                                m_error = m_error - m_maximumInput + m_minimumInput;
                            }
                            else
                            {
                                m_error = m_error + m_maximumInput - m_minimumInput;
                            }
                        }
                    }

                    if (m_I != 0.0)
                    {
                        double potentialIGain = (m_totalError + m_error) * m_I;
                        if (potentialIGain < m_maximumOutput)
                        {
                            if (potentialIGain > m_minimumInput)
                            {
                                m_totalError += m_error;
                            }
                            else
                            {
                                m_totalError = m_minimumOutput / m_I;
                            }
                        }
                        else
                        {
                            m_totalError = m_maximumOutput / m_I;
                        }
                    }

                    m_result = m_P * m_error + m_I * m_totalError + m_D * (m_error - m_prevError) + m_setpoint * m_F;
                    m_prevError = m_error;

                    if (m_result > m_maximumOutput)
                    {
                        m_result = m_maximumOutput;
                    }
                    else if (m_result < m_minimumOutput)
                    {
                        m_result = m_minimumOutput;
                    }
                    pidOutput = m_pidOutput;
                    result = m_result;
                }
                pidOutput.PidWrite(result);
            }
        }

        public void SetPID(double p, double i, double d)
        {
            lock (m_lockObject)
            {
                m_P = p;
                m_I = i;
                m_D = d;
            }
        }

        public void SetPID(double p, double i, double d, double f)
        {
            lock (m_lockObject)
            {
                m_P = p;
                m_I = i;
                m_D = d;
                m_F = f;
            }
        }

        public double GetP()
        {
            lock (m_lockObject)
                return m_P;
        }

        public double GetI()
        {
            lock (m_lockObject)
                return m_I;
        }

        public double GetD()
        {
            lock (m_lockObject)
                return m_D;
        }

        public double GetF()
        {
            lock (m_lockObject)
                return m_F;
        }

        public double Get()
        {
            lock (m_lockObject)
                return m_result;
        }

        public void SetContinouous(bool continuous = true)
        {
            lock (m_lockObject)
                m_continuous = continuous;
        }

        public void SetInputRange(double minimumInput, double maximumInput)
        {
            lock (m_lockObject)
            {
                if (minimumInput > maximumInput)
                    throw new BoundaryException("Lower bound is greatter than upper bound");
                m_minimumInput = minimumInput;
                m_maximumInput = maximumInput;
                SetSetpoint(m_setpoint);
            }
        }

        public void SetOutputRange(double minimumOutput, double maximumOutput)
        {
            lock (m_lockObject)
            {
                if (minimumOutput > maximumOutput)
                    throw new BoundaryException("Lower bound is greatter than upper bound");
                m_minimumOutput = minimumOutput;
                m_maximumOutput = maximumOutput;
                SetSetpoint(m_setpoint);
            }
        }

        public void SetSetpoint(double setpoint)
        {
            lock (m_lockObject)
            {
                if (m_maximumInput > m_minimumInput)
                {
                    if (setpoint > m_maximumInput)
                    {
                        m_setpoint = m_maximumInput;
                    }
                    else if (setpoint < m_minimumInput)
                    {
                        m_setpoint = m_minimumInput;
                    }
                    else
                    {
                        m_setpoint = setpoint;
                    }
                }
                else
                {
                    m_setpoint = setpoint;
                }
            }
        }

        public double GetSetpoint()
        {
            lock (m_lockObject)
                return m_setpoint;
        }

        public double GetError()
        {
            lock (m_lockObject)
            {
                return GetSetpoint() - m_pidInput.PidGet();
            }
        }

        [Obsolete]
        public void SetTolerance(double percent)
        {
            lock (m_lockObject)
            {
                m_toleranceType = ToleranceType.PercentTolerance;
                m_tolerance = percent;
            }
        }

        public void SetPercentTolerance(double percent)
        {
            lock (m_lockObject)
            {
                m_toleranceType = ToleranceType.PercentTolerance;
                m_tolerance = percent;
            }
        }

        public void SetAbsoluteTolerance(double absTolerance)
        {
            lock (m_lockObject)
            {
                m_toleranceType = ToleranceType.AbsoluteTolerance;
                m_tolerance = absTolerance;
            }
        }

        public bool OnTarget()
        {
            lock (m_lockObject)
            {
                switch (m_toleranceType)
                {
                    case ToleranceType.PercentTolerance:
                        return Math.Abs(m_error) < (m_tolerance / 100 * (m_maximumInput - m_minimumInput));
                    case ToleranceType.AbsoluteTolerance:
                        return Math.Abs(m_error) < m_tolerance;
                    default:
                        return false;
                }
            }
        }

        public void Enable()
        {
            lock (m_lockObject)
                m_enabled = true;
        }

        public void Disable()
        {
            lock (m_lockObject)
            {
                m_pidOutput.PidWrite(0);
                m_enabled = false;
            }
        }

        public bool IsEnable()
        {
            lock (m_lockObject)
                return m_enabled;
        }

        public void Reset()
        {
            lock (m_lockObject)
            {
                Disable();
                m_prevError = 0;
                m_totalError = 0;
                m_result = 0;
            }
        }

        private ITable m_table;

        public void UpdateTable()
        {
        }

        public void StartLiveWindowMode()
        {
        }

        public void StopLiveWindowMode()
        {
            
        }

        public void InitTable(ITable subtable)
        {
            if (m_table != null)
            {
                m_table.RemoveTableListener(this);
            }
            m_table = subtable;
            if (m_table != null)
            {
                m_table.PutNumber("p", GetP());
                m_table.PutNumber("i", GetI());
                m_table.PutNumber("d", GetD());
                m_table.PutNumber("f", GetF());
                m_table.PutNumber("setpoint", GetSetpoint());
                m_table.PutBoolean("enabled", IsEnable());
                m_table.AddTableListener(this, false);
            }
        }

        public ITable GetTable()
        {
            return m_table;
        }

        public string GetSmartDashboardType()
        {
            return "PIDController";
        }

        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            if (key == ("p") || key == ("i") || key == ("d") || key == ("f"))
            {
                if (GetP() != m_table.GetNumber("p", 0.0) || GetI() != m_table.GetNumber("i", 0.0) ||
                        GetD() != m_table.GetNumber("d", 0.0) || GetF() != m_table.GetNumber("f", 0.0))
                    SetPID(m_table.GetNumber("p", 0.0), m_table.GetNumber("i", 0.0), m_table.GetNumber("d", 0.0), m_table.GetNumber("f", 0.0));
            }
            else if (key == ("setpoint"))
            {
                if (GetSetpoint() != (double)value)
                    SetSetpoint((double)value);
            }
            else if (key == ("enabled"))
            {
                if (IsEnable() != (bool)value)
                {
                    if ((bool)value)
                    {
                        Enable();
                    }
                    else
                    {
                        Disable();
                    }
                }
            }
        }
    }


}
