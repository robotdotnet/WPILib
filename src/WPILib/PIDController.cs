using System;
using System.Collections.Generic;
using HAL.Base;
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
        /// <summary>
        /// Tolerance Types allowed for this PIDController.
        /// </summary>
        public enum ToleranceType
        {
            /// <summary>
            /// Absolute Tolerance
            /// </summary>
            AbsoluteTolerance,
            /// <summary>
            /// Percent Tolerance
            /// </summary>
            PercentTolerance,
            /// <summary>
            /// No Tolerance
            /// </summary>
            NoTolerance,
        }

        private ToleranceType m_toleranceType;

        /// <summary>
        /// The default period to run the controller at.
        /// </summary>
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
        private double m_prevError = 0.0; // the prior sensor error (used to compute velocity)
        private double m_totalError = 0.0; //the sum of the errors for use in the integral calc
        private double m_setpoint = 0.0;
        private double m_error = 0.0;
        private double m_result = 0.0;
        private readonly double m_period;
        /// <summary>
        /// The PIDInput for this controller.
        /// </summary>
        protected IPIDSource PIDInput;
        /// <summary>
        /// The PIDOutput for this controller.
        /// </summary>
        protected IPIDOutput PIDOutput;
        private readonly object m_lockObject = new object();

        private int m_bufLength = 1;
        private readonly Queue<double> m_buf;
        private double m_bufTotal = 0.0;

        private double m_prevSetpoint = 0.0;
        private readonly Timer m_setpointTimer;

        private double m_tolerance = 0.05;

        private Notifier m_controlLoop;

        private readonly Action CalculateCallback;

        /// <summary>
        /// Creates a new PID object with the given contants for P, I, D and F.
        /// </summary>
        /// <param name="kp">The proportional coefficient.</param>
        /// <param name="ki">The integral coefficient</param>
        /// <param name="kd">The derivative coefficient</param>
        /// <param name="kf">The feed forward term.</param>
        /// <param name="source">The PIDSource object that is used to get values.</param>
        /// <param name="output">The PIDOutput object that is set to the output percentage.</param>
        /// <param name="period">The loop time for doing calculations.</param>
        public PIDController(double kp, double ki, double kd, double kf,
            IPIDSource source, IPIDOutput output,
            double period)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Null PIDSource was given");
            if (output == null)
                throw new ArgumentNullException(nameof(output), "Null PIDOutput was given");

            CalculateCallback = Calculate;
            m_controlLoop = new Notifier(CalculateCallback);
            m_setpointTimer = new Timer();
            m_setpointTimer.Start();

            m_P = kp;
            m_I = ki;
            m_D = kd;
            m_F = kf;

            PIDInput = source;
            PIDOutput = output;
            m_period = period;

            m_controlLoop.StartPeriodic(m_period);

            s_instances++;
            HAL.Base.HAL.Report(ResourceType.kResourceType_PIDController, (byte)s_instances);

            m_toleranceType = ToleranceType.NoTolerance;
            m_buf = new Queue<double>(m_bufLength + 1);
        }

        /// <summary>
        /// Creates a new PID object with the given contants for P, I and D.
        /// </summary>
        /// <param name="kp">The proportional coefficient.</param>
        /// <param name="ki">The integral coefficient</param>
        /// <param name="kd">The derivative coefficient</param>
        /// <param name="source">The PIDSource object that is used to get values.</param>
        /// <param name="output">The PIDOutput object that is set to the output percentage.</param>
        /// <param name="period">The loop time for doing calculations.</param>
        public PIDController(double kp, double ki, double kd,
            IPIDSource source, IPIDOutput output,
            double period)
            : this(kp, ki, kd, 0.0, source, output, period)
        {

        }

        /// <summary>
        /// Creates a new PID object with the given contants for P, I and D using a 50ms period.
        /// </summary>
        /// <param name="kp">The proportional coefficient.</param>
        /// <param name="ki">The integral coefficient</param>
        /// <param name="kd">The derivative coefficient</param>
        /// <param name="source">The PIDSource object that is used to get values.</param>
        /// <param name="output">The PIDOutput object that is set to the output percentage.</param>
        public PIDController(double kp, double ki, double kd,
            IPIDSource source, IPIDOutput output)
            : this(kp, ki, kd, source, output, DefaultPeriod)
        {

        }

        /// <summary>
        /// Creates a new PID object with the given contants for P, I, D and F using a 50ms period
        /// </summary>
        /// <param name="kp">The proportional coefficient.</param>
        /// <param name="ki">The integral coefficient</param>
        /// <param name="kd">The derivative coefficient</param>
        /// <param name="kf">The feed forward term.</param>
        /// <param name="source">The PIDSource object that is used to get values.</param>
        /// <param name="output">The PIDOutput object that is set to the output percentage.</param>
        public PIDController(double kp, double ki, double kd, double kf,
            IPIDSource source, IPIDOutput output)
            : this(kp, ki, kd, kf, source, output, DefaultPeriod)
        {

        }

        /// <inheritdoc/>
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

        /// <summary>
        /// Read the inpout, calculate the output accordinglyu, and write to the input.
        /// This should only be called by the Notifier timer and is created during initialization.
        /// </summary>
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
                    m_result = m_P * m_totalError + m_D * m_error + CalculateFeedForward();
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
                    m_result = m_P * m_error + m_I * m_totalError + m_D * (m_error - m_prevError) + CalculateFeedForward();
                }



                m_prevError = m_error;

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

        /// <summary>
        /// Calculate the feed forward term.
        /// </summary>
        /// <remarks>
        ///Both of the provided feed forward calculations are velocity feed forwards.
        /// If a different feed forward calculation is desired, the user can override
        /// this function and provide his or her own.This function  does no
        /// synchronization because the PIDController class only calls it in
        /// synchronized code, so be careful if calling it oneself.
        ///	<para></para>
        /// If a velocity PID controller is being used, the F term should be set to 1
        /// over the maximum setpoint for the output.If a position PID controller is
        /// being used, the F term should be set to 1 over the maximum speed for the
        /// output measured in setpoint units per this controller's update period (see
        /// the default period in this class's constructor).
        /// </remarks>
        /// <returns>The calculated Feed Forward Value</returns>
        protected virtual double CalculateFeedForward()
        {
            if (PIDInput.PIDSourceType == PIDSourceType.Rate)
            {
                return m_F * Setpoint;
            }
            else
            {
                double temp = m_F * GetDeltaSetpoint();
                m_prevSetpoint = m_setpoint;
                m_setpointTimer.Reset();
                return temp;
            }
        }

        ///<inheritdoc/>
        public void SetPID(double p, double i, double d)
        {
            lock (m_lockObject)
            {
                m_P = p;
                m_I = i;
                m_D = d;
            }
        }

        /// <summary>
        /// Sets the PID Controller Gain Parameters.
        /// </summary>
        /// <param name="p">The proportional coefficient</param>
        /// <param name="i">The integral coefficient</param>
        /// <param name="d">The derivative coefficient</param>
        /// <param name="f">The feed forward coefficient</param>
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

        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <summary>
        /// Gets the current PID result.
        /// </summary>
        /// <remarks>
        /// This is always centered on zero and constrained by the max and min output values.
        /// </remarks>
        /// <returns>The latest calculated output.</returns>
        public double Get()
        {
            lock (m_lockObject)
                return m_result;
        }

        /// <summary>
        /// Gets or Sets whether the PID controller input is continouous.
        /// </summary>
        /// <remarks>
        /// Rather then using the max and min inputs as constraints, it considers them
        /// to be the same point and automatically calculates the shortest route to the
        /// setpoint.
        /// </remarks>
        public bool Continuous
        {
            set
            {
                lock (m_lockObject)
                    m_continuous = value;
            }
            get
            {
                lock (m_lockObject)
                    return m_continuous;
            }
        }

        /// <summary>
        /// Sets the minimum and maximum values expected from the input and setpoint.
        /// </summary>
        /// <param name="minimumInput">The minimum value expected from the input and setpoint.</param>
        /// <param name="maximumInput">The maximum value expected from the input and setpoint.</param>
        public void SetInputRange(double minimumInput, double maximumInput)
        {
            lock (m_lockObject)
            {
                if (minimumInput > maximumInput)
                    throw new BoundaryException("Lower bound is greater than upper bound");
                m_minimumInput = minimumInput;
                m_maximumInput = maximumInput;
                Setpoint = m_setpoint;
            }
        }

        /// <summary>
        /// Sets the maximum and minimum values to write.
        /// </summary>
        /// <param name="minimumOutput">The minimum value to write to the output.</param>
        /// <param name="maximumOutput">The maximum value to write to the output.</param>
        public void SetOutputRange(double minimumOutput, double maximumOutput)
        {
            lock (m_lockObject)
            {
                if (minimumOutput > maximumOutput)
                    throw new BoundaryException("Lower bound is greater than upper bound");
                m_minimumOutput = minimumOutput;
                m_maximumOutput = maximumOutput;
                Setpoint = m_setpoint;
            }
        }

        /// <summary>
        /// Gets or sets the setpoint for the PID Controller.
        /// </summary>
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

        /// <summary>
        /// Retunrs the change in setpoing over time of the PIDController.
        /// </summary>
        /// <returns>The change in setpoint over time.</returns>
        public double GetDeltaSetpoint()
        {
            return (m_setpoint - m_prevSetpoint) / m_setpointTimer.Get();
        }

        /// <summary>
        /// Returns the current difference of the input from the setpoint.
        /// </summary>
        /// <returns>The current error.</returns>
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

        /// <summary>
        /// Returns the current difference of the error over the past few iterations.
        /// </summary>
        /// <remarks>
        /// You can specify the number of iterations to average with <see cref="SetToleranceBuffer"/> (defaults to 1).
        /// <see cref="GetAvgError"/> is used for the <see cref="OnTarget"/> method.
        /// </remarks>
        /// <returns>The current average of the error.</returns>
        public double GetAvgError()
        {
            lock (m_lockObject)
            {
                double avgError = 0;
                if (m_buf.Count != 0) avgError = m_bufTotal / m_buf.Count;
                return avgError;
            }
        }

        /// <summary>
        /// Returns whether or not any values have been collected.
        /// </summary>
        /// <remarks>
        /// If no values have been collected, <see cref="GetAvgError"/> is 0, which
        /// is invalid.
        /// </remarks>
        /// <returns>True if <see cref="GetAvgError"/> is currently valid.</returns>
        public bool IsAvgErrorValid()
        {
            lock (m_lockObject)
            {
                return m_buf.Count != 0;
            }
        }

        /// <summary>
        /// Sets the percentage error which is considered tolerable for use
        /// with <see cref="OnTarget"/>.
        /// </summary>
        /// <remarks>Obsolete. Please use <see cref="SetPercentTolerance"/> or 
        /// <see cref="SetAbsoluteTolerance"/> instead.</remarks>
        /// <param name="percent">The percent error which is tolerable.</param>
        [Obsolete("Use SetPercentTolerance or SetAbsoluteTolerance instead.")]
        public void SetTolerance(double percent)
        {
            lock (m_lockObject)
            {
                m_toleranceType = ToleranceType.PercentTolerance;
                m_tolerance = percent;
            }
        }

        /// <summary>
        /// Set the percentage error which is considered tolerable for use with <see cref="OnTarget"/>.
        /// </summary>
        /// <param name="percent">The percent tolerance.</param>
        public void SetPercentTolerance(double percent)
        {
            lock (m_lockObject)
            {
                m_toleranceType = ToleranceType.PercentTolerance;
                m_tolerance = percent;
            }
        }
        /// <summary>
        /// Sets the absolute error which is considered tolerable for use with <see cref="OnTarget"/>.
        /// </summary>
        /// <remarks>The value is in the same range as the PIDInput values.</remarks>
        /// <param name="absTolerance">The absolute tolerance.</param>
        public void SetAbsoluteTolerance(double absTolerance)
        {
            lock (m_lockObject)
            {
                m_toleranceType = ToleranceType.AbsoluteTolerance;
                m_tolerance = absTolerance;
            }
        }

        /// <summary>
        /// Sets the number of previous error samples to average for tolerancing.
        /// </summary>
        /// <param name="bufLength">The number of previous cycles to average.</param>
        public void SetToleranceBuffer(int bufLength)
        {
            lock (m_lockObject)
            {
                m_bufLength = bufLength;
                while (m_buf.Count > bufLength)
                {
                    m_bufTotal -= m_buf.Dequeue();
                }
            }
        }

        /// <summary>
        /// Gets if the PID system is on target.
        /// </summary>
        /// <remarks>
        /// Returns true if the error is within the percentage of the total input range,
        /// determined by SetTolerance. This assumes theat the maximum and minimum input were
        /// set using <see cref="SetInputRange"/>.
        /// </remarks>
        /// <returns>True if the error is less then the tolerance.</returns>
        public bool OnTarget()
        {
            lock (m_lockObject)
            {
                switch (m_toleranceType)
                {
                    case ToleranceType.PercentTolerance:
                        return IsAvgErrorValid() && Math.Abs(GetAvgError()) < (m_tolerance / 100 * (m_maximumInput - m_minimumInput));
                    case ToleranceType.AbsoluteTolerance:
                        return IsAvgErrorValid() && Math.Abs(GetAvgError()) < m_tolerance;
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

        /// <inheritdoc/>
        public bool Enabled
        {
            get
            {
                lock (m_lockObject)
                    return m_enabled;
            }
        }
        /// <inheritdoc/>
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
        public void ValueChanged(ITable source, string key, Value value, NotifyFlags flags)
        {
            if (key == ("p") || key == ("i") || key == ("d") || key == ("f"))
            {
                if (P != Table.GetNumber("p", 0.0) || I != Table.GetNumber("i", 0.0) ||
                        D != Table.GetNumber("d", 0.0) || F != Table.GetNumber("f", 0.0))
                    SetPID(Table.GetNumber("p", 0.0), Table.GetNumber("i", 0.0), Table.GetNumber("d", 0.0), Table.GetNumber("f", 0.0));
            }
            else if (key == ("setpoint"))
            {
                double val = value.GetDouble();
                if (Setpoint != val)
                    Setpoint = val;
            }
            else if (key == ("enabled"))
            {
                bool val = value.GetBoolean();
                if (Enabled != val)
                {
                    if (val)
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
