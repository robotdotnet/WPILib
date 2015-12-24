using System;
using System.Collections.Generic;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.Interfaces;
using WPILib.LiveWindow;

namespace WPILib
{
    /// <summary>
    /// Implements a PID loop to be used with mechanisms.
    /// </summary>
    public class PIDController : IController, IPIDInterface, ILiveWindowSendable, ITableListener, IDisposable
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
        private double m_prevInput = 0.0; // the prior sensor input (used to compute velocity)
        private double m_totalError = 0.0; //the sum of the errors for use in the integral calc
        private double m_setpoint = 0.0;
        private double m_error = 0.0;
        private double m_result = 0.0;
        private double m_period;
        protected IPIDSource PIDInput;
        protected IPIDOutput PIDOutput;
        private readonly object m_lockObject = new object();

        private int m_bufLength = 0;
        private Queue<double> m_buf;
        private double m_bufTotal = 0.0;

        private double m_tolerance = 0.05;

        private Notifier m_controlLoop;


        public PIDController(double kp, double ki, double kd, double kf,
            IPIDSource source, IPIDOutput output,
            double period)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Null PIDSource was given");
            if (output == null)
                throw new ArgumentNullException(nameof(output), "Null PIDOutput was given");

            m_controlLoop = new Notifier(CallCalculate, this);

            m_P = kp;
            m_I = ki;
            m_D = kd;
            m_F = kf;

            PIDInput = source;
            PIDOutput = output;
            m_period = period;

            m_controlLoop.StartPeriodic(m_period);

            s_instances++;
            HLUsageReporting.ReportPIDController(s_instances);

            m_toleranceType = ToleranceType.NoTolerance;
            m_buf = new Queue<double>();
        }

        public PIDController(double kp, double ki, double kd,
            IPIDSource source, IPIDOutput output,
            double period)
            : this(kp, ki, kd, 0.0, source, output, period)
        {

        }

        public PIDController(double kp, double ki, double kd,
            IPIDSource source, IPIDOutput output)
            : this(kp, ki, kd, source, output, DefaultPeriod)
        {

        }

        public PIDController(double kp, double ki, double kd, double kf,
            IPIDSource source, IPIDOutput output)
            : this(kp, ki, kd, kf, source, output, DefaultPeriod)
        {

        }

        public void Dispose()
        {
            m_controlLoop.Stop();
            lock (this)
            {
                PIDOutput = null;
                PIDInput = null;
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

        protected virtual void Calculate()
        {
            bool enabled;
            IPIDSource pidInput;

            lock (m_lockObject)
            {
                if (PIDInput == null)
                {
                    return;
                }
                if (PIDOutput == null)
                {
                    return;
                }
                enabled = m_enabled; // take snapshot of these values...
                pidInput = PIDInput;
            }

            if (!enabled) return;
            double input;
            double result;
            IPIDOutput pidOutput = null;
            lock (this)
            {
                input = pidInput.PidGet();
            }
            lock (m_lockObject)
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


                if (pidInput.PIDSourceType == PIDSourceType.Rate)
                {
                    if (m_P != 0)
                    {
                        double potentialPGain = (m_totalError + m_error) * m_P;
                        if (potentialPGain < m_maximumOutput)
                        {
                            if (potentialPGain > m_minimumOutput)
                            {
                                m_totalError += m_error;
                            }
                            else
                            {
                                m_totalError = m_minimumOutput / m_P;
                            }
                        }
                        else
                        {
                            m_totalError = m_maximumOutput / m_P;
                        }
                    }
                    m_result = m_P * m_totalError + m_D * m_error + m_setpoint * m_F;
                }
                else
                {
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
                }

                m_result = m_P * m_error + m_I * m_totalError + m_D * (m_prevInput - input) + m_setpoint * m_F;

                m_prevInput = input;

                if (m_result > m_maximumOutput)
                {
                    m_result = m_maximumOutput;
                }
                else if (m_result < m_minimumOutput)
                {
                    m_result = m_minimumOutput;
                }
                pidOutput = PIDOutput;
                result = m_result;

                m_buf.Enqueue(m_error);
                m_bufTotal += m_error;
                //Remove old elements when the buffer is full
                if (m_buf.Count > m_bufLength)
                {
                    m_bufTotal -= m_buf.Dequeue();
                }
            }

            pidOutput.PidWrite(result);
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
                    m_buf.Clear();
                    Table?.PutNumber("setpoint", m_setpoint);
                }
            }
        }

        public double GetError()
        {
            lock (m_lockObject)
            {
                return Setpoint - PIDInput.PidGet();
            }
        }

        ///<inheritdoc/>
        internal PIDSourceType PIDSourceType {
            get { return PIDInput.PIDSourceType; }
            set { PIDInput.PIDSourceType = value; }
        }

        public double GetAvgError()
        {
            lock (m_lockObject)
            {
                double avgError = 0;
                if (m_buf.Count != 0) avgError = m_bufTotal / m_buf.Count;
                return avgError;
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

        public void SetToleranceBuffer(int bufLength)
        {
            m_bufLength = bufLength;
            while (m_buf.Count > bufLength)
            {
                m_bufTotal -= m_buf.Dequeue();
            }
        }

        public bool OnTarget()
        {
            lock (m_lockObject)
            {
                switch (m_toleranceType)
                {
                    case ToleranceType.PercentTolerance:
                        return Math.Abs(GetAvgError()) < (m_tolerance / 100 * (m_maximumInput - m_minimumInput));
                    case ToleranceType.AbsoluteTolerance:
                        return Math.Abs(GetAvgError()) < m_tolerance;
                    default:
                        throw new InvalidOperationException("Tolerance type must be set before calling OnTarget");
                }
            }
        }
        ///<inheritdoc/>
        public void Enable()
        {
            lock (m_lockObject)
                m_enabled = true;
        }
        ///<inheritdoc/>
        public void Disable()
        {
            lock (m_lockObject)
            {
                PIDOutput.PidWrite(0);
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
                m_prevInput = 0;
                m_totalError = 0;
                m_result = 0;
            }
        }
        ///<inheritdoc/>
        public void UpdateTable()
        {
        }
        ///<inheritdoc/>
        public void StartLiveWindowMode()
        {
            Disable();
        }
        ///<inheritdoc/>
        public void StopLiveWindowMode()
        {
        }
        ///<inheritdoc/>
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
        ///<inheritdoc/>
        public ITable Table { get; private set; }
        ///<inheritdoc/>
        public string SmartDashboardType => "PIDController";

        ///<inheritdoc/>
        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
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
