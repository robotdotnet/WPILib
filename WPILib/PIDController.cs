using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{

    public class PIDController : Controller
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

        private double m_P;     // factor for "proportional" control
        private double m_I;     // factor for "integral" control
        private double m_D;     // factor for "derivative" control
        private double m_F;                 // factor for feedforward term
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
            HAL.Report(ResourceType.kResourceType_PIDController, (byte)s_instances);

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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetPID(double p, double i, double d)
        {
            m_P = p;
            m_I = i;
            m_D = d;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetPID(double p, double i, double d, double f)
        {
            m_P = p;
            m_I = i;
            m_D = d;
            m_F = f;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetP()
        {
            return m_P;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetI()
        {
            return m_I;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetD()
        {
            return m_D;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetF()
        {
            return m_F;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double Get()
        {
            return m_result;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetContinouous(bool continuous = true)
        {
            m_continuous = continuous;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetInputRange(double minimumInput, double maximumInput)
        {
            if (minimumInput > maximumInput)
                throw new BoundaryException("Lower bound is greatter than upper bound");
            m_minimumInput = minimumInput;
            m_maximumInput = maximumInput;
            SetSetpoint(m_setpoint);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetOutputRange(double minimumOutput, double maximumOutput)
        {
            if (minimumOutput > maximumOutput)
                throw new BoundaryException("Lower bound is greatter than upper bound");
            m_minimumOutput = minimumOutput;
            m_maximumOutput = maximumOutput;
            SetSetpoint(m_setpoint);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetSetpoint(double setpoint)
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


        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetSetpoint()
        {
            return m_setpoint;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetError()
        {
            return GetSetpoint() - m_pidInput.PidGet();
        }

        [Obsolete]
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetTolerance(double percent)
        {
            m_toleranceType = ToleranceType.PercentTolerance;
            m_tolerance = percent;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetPercentTolerance(double percent)
        {
            m_toleranceType = ToleranceType.PercentTolerance;
            m_tolerance = percent;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetAbsoluteTolerance(double absTolerance)
        {
            m_toleranceType = ToleranceType.AbsoluteTolerance;
            m_tolerance = absTolerance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool OnTarget()
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Enable()
        {
            m_enabled = true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Disable()
        {
            m_pidOutput.PidWrite(0);
            m_enabled = false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsEnable()
        {
            return m_enabled;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Reset()
        {
            Disable();
            m_prevError = 0;
            m_totalError = 0;
            m_result = 0;
        }


    }


}
