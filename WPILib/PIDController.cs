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

    public class PIDController : IController, LiveWindowSendable, ITableListener, IDisposable
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
        IPIDSource m_ipidInput;
        IPIDOutput m_ipidOutput;
        private object m_lockObject = new object();

        private double m_tolerance = 0.05;

        private Notifier m_controlLoop;


        public PIDController(double Kp, double Ki, double Kd, double Kf,
            IPIDSource source, IPIDOutput output,
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

            m_ipidInput = source;
            m_ipidOutput = output;
            m_period = period;

            m_controlLoop.StartPeriodic(m_period);

            s_instances++;
            HLUsageReporting.ReportPIDController(s_instances);

            m_toleranceType = ToleranceType.NoTolerance;
        }

        public PIDController(double Kp, double Ki, double Kd,
            IPIDSource source, IPIDOutput output,
            double period)
            : this(Kp, Ki, Kd, 0.0, source, output, period)
        {

        }

        public PIDController(double Kp, double Ki, double Kd,
            IPIDSource source, IPIDOutput output)
            : this(Kp, Ki, Kd, source, output, DefaultPeriod)
        {

        }

        public PIDController(double Kp, double Ki, double Kd, double Kf,
            IPIDSource source, IPIDOutput output)
            : this(Kp, Ki, Kd, Kf, source, output, DefaultPeriod)
        {

        }

        public void Dispose()
        {
            m_controlLoop.Stop();
            lock (this)
            {
                m_ipidOutput = null;
                m_ipidInput = null;
                m_controlLoop.Dispose();
                m_controlLoop = null;
            }
            Table?.RemoveTableListener(this);
        }


        public static void CallCalculate(object o)
        {
            PIDController controller = (PIDController)o;
            controller.Calculate();
        }

        private void Calculate()
        {
            bool enabled;
            IPIDSource ipidInput;

            lock (this)
            {
                if (m_ipidInput == null)
                {
                    return;
                }
                if (m_ipidOutput == null)
                {
                    return;
                }
                enabled = m_enabled; // take snapshot of these values...
                ipidInput = m_ipidInput;
            }

            if (!enabled) return;
            double input;
            double result;
            IPIDOutput ipidOutput = null;
            lock (this)
            {
                input = ipidInput.PidGet;
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
                ipidOutput = m_ipidOutput;
                result = m_result;
            }
            ipidOutput.PidWrite = result;
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

        public double P
        {
            get
            {
                lock (m_lockObject)
                    return m_P;
            }
            set
            {
                lock (m_lockObject)
                    m_P = value;
            }
        }

        public double I
        {
            get
            {
                lock (m_lockObject)
                    return m_I;
            }
            set
            {
                lock (m_lockObject)
                    m_I = value;
            }
        }

        public double D
        {
            get
            {
                lock (m_lockObject)
                    return m_D;
            }
            set
            {
                lock (m_lockObject)
                    m_D = value;
            }
        }

        public double F
        {
            get
            {
                lock (m_lockObject)
                    return m_F;
            }
            set
            {
                lock (m_lockObject)
                    m_F = value;
            }
        }

        public double Get()
        {
            lock (m_lockObject)
                return m_result;
        }

        public bool Continouous
        {
            set
            {
                lock (m_lockObject)
                    m_continuous = value;
            }
        }

        public void SetInputRange(double minimumInput, double maximumInput)
        {
            lock (m_lockObject)
            {
                if (minimumInput > maximumInput)
                    throw new BoundaryException("Lower bound is greatter than upper bound");
                m_minimumInput = minimumInput;
                m_maximumInput = maximumInput;
                Setpoint = m_setpoint;
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
                Setpoint = m_setpoint;
            }
        }

        public double Setpoint
        {
            get
            {
                lock (m_lockObject)
                    return m_setpoint;
            }
            set
            {
                lock (m_lockObject)
                {
                    if (m_maximumInput > m_minimumInput)
                    {
                        if (value > m_maximumInput)
                        {
                            m_setpoint = m_maximumInput;
                        }
                        else if (value < m_minimumInput)
                        {
                            m_setpoint = m_minimumInput;
                        }
                        else
                        {
                            m_setpoint = value;
                        }
                    }
                    else
                    {
                        m_setpoint = value;
                    }
                }
            }
        }

        public double Error
        {
            get
            {
                lock (m_lockObject)
                {
                    return Setpoint - m_ipidInput.PidGet;
                }
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

        public bool OnTarget
        {
            get
            {
                lock (m_lockObject)
                {
                    switch (m_toleranceType)
                    {
                        case ToleranceType.PercentTolerance:
                            return Math.Abs(m_error) < (m_tolerance/100*(m_maximumInput - m_minimumInput));
                        case ToleranceType.AbsoluteTolerance:
                            return Math.Abs(m_error) < m_tolerance;
                        default:
                            return false;
                    }
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
                m_ipidOutput.PidWrite = 0;
                m_enabled = false;
            }
        }

        public bool Enabled
        {
            get
            {
                lock (m_lockObject)
                    return m_enabled;
            }
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

        public void UpdateTable()
        {
        }

        public void StartLiveWindowMode()
        {
            Disable();
        }

        public void StopLiveWindowMode()
        {
        }

        public void InitTable(ITable subtable)
        {
            Table?.RemoveTableListener(this);
            Table = subtable;
            if (Table != null)
            {
                Table.PutNumber("p", P);
                Table.PutNumber("i", I);
                Table.PutNumber("d", D);
                Table.PutNumber("f", F);
                Table.PutNumber("setpoint", Setpoint);
                Table.PutBoolean("enabled", Enabled);
                Table.AddTableListener(this, false);
            }
        }

        public ITable Table { get; private set; }

        public string SmartDashboardType => "PIDController";

        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            if (key == ("p") || key == ("i") || key == ("d") || key == ("f"))
            {
                if (P != Table.GetNumber("p", 0.0) || I != Table.GetNumber("i", 0.0) ||
                        D != Table.GetNumber("d", 0.0) || F != Table.GetNumber("f", 0.0))
                    SetPID(Table.GetNumber("p", 0.0), Table.GetNumber("i", 0.0), Table.GetNumber("d", 0.0), Table.GetNumber("f", 0.0));
            }
            else if (key == ("setpoint"))
            {
                if (Setpoint != (double)value)
                    Setpoint = (double)value;
            }
            else if (key == ("enabled"))
            {
                if (Enabled != (bool)value)
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
