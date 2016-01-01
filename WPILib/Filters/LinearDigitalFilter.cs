using System;
using WPILib.Interfaces;

namespace WPILib.Filters
{
    /// <summary>
    /// This class implements a linear, digital filter.
    /// </summary>
    /// <remarks>
    /// All types of FIR and IIR filters are supported.Static factory methods are provided to create commonly used types of filters.	
    ///	<para></para>
    /// Filters are of the form:	
    ///  y[n] = (b0*x[n] + b1*x[n-1] + ... + bP*x[n-P) - (a0*y[n-1] + a2*y[n-2] + ... + aQ*y[n-Q])
    /// <para></para>
    /// Where:	
    ///  y[n] is the output at time "n"	
    ///  x[n] is the input at time "n"	
    ///  y[n - 1] is the output from the LAST time step ("n-1")    
    /// x[n - 1] is the input from the LAST time step("n-1") 
    /// b0...bP are the "feedforward" (FIR) gains	
    /// a0...aQ are the "feedback" (IIR) gains	
    /// IMPORTANT! Note the "-" sign in front of the feedback term! This is a common 
    /// convention in signal processing.  
    ///	<para></para>
    /// What can linear filters do? Basically, they can filter, or diminish, the 
    /// effects of undesirable input frequencies.High frequencies, or rapid changes,    
    /// can be indicative of sensor noise or be otherwise undesirable. A "low pass"	
    /// filter smooths out the signal, reducing the impact of these high frequency   
    /// components.Likewise, a "high pass" filter gets rid of slow-moving signal	
    /// components, letting you detect large changes more easily.    
    ///	3<para></para>
    /// Example FRC applications of filters:	
    ///  - Getting rid of noise from an analog sensor input (note: the roboRIO's FPGA	
    ///    can do this faster in hardware)   
    ///  - Smoothing out joystick input to prevent the wheels from slipping or the   
    /// robot from tipping    
    ///  - Smoothing motor commands so that unnecessary strain isn't put on	
    /// electrical or mechanical components	
    ///  - If you use clever gains, you can make a PID controller out of this class!	
    ///	<para></para>
    /// For more on filters, I highly recommend the following articles:	
    /// http://en.wikipedia.org/wiki/Linear_filter	
    /// http://en.wikipedia.org/wiki/Iir_filter	
    /// http://en.wikipedia.org/wiki/Fir_filter 
    ///	<para></para>
    /// Note 1: PIDGet() should be called by the user on a known, regular period.    
    /// You can set up a Notifier to do this (look at the WPILib PIDController   
    /// class), or do it "inline" with code in a periodic function.  
    ///	<para></para>
    /// Note 2: For ALL filters, gains are necessarily a function of frequency.If	
    /// you make a filter that works well for you at, say, 100Hz, you will most	
    /// definitely need to adjust the gains if you then want to run it at 200Hz!	
    /// Combining this with Note 1 - the impetus is on YOU as a developer to make	
    /// sure PIDGet() gets called at the desired, constant frequency!
    /// </remarks>
    public class LinearDigitalFilter : Filter
    {
        private CircularBuffer<double> m_inputs;
        private CircularBuffer<double> m_outputs;
        private double[] m_inputGains;
        private double[] m_outputGains;

        /// <summary>
        /// Creates a linear FIR or IIR filter.
        /// </summary>
        /// <param name="source">The <see cref="IPIDSource"/> object that is used to get values.</param>
        /// <param name="ffGains">The "feed forward" or FIR gains.</param>
        /// <param name="fbGains">The "feed back" or IIR gains.</param>
        public LinearDigitalFilter(IPIDSource source, double[] ffGains, double[] fbGains) : base(source)
        {
            m_inputs = new CircularBuffer<double>(ffGains.Length);
            m_outputs = new CircularBuffer<double>(fbGains.Length);
            m_inputGains = ffGains;
            m_outputGains = fbGains;
        }

        /// <summary>
        /// Ceates a one-pole IIR low-passs filter.
        /// </summary>
        /// <remarks>
        /// The filter is in the form 
        /// y[n] = gain*x[n] + (1-gain)*y[n-1].
        /// <para></para>
        /// This filter is stable for gains in the range [0, 1]
        /// </remarks>
        /// <param name="source">The <see cref="IPIDSource"/> object that is used to get values.</param>
        /// <param name="timeConstant">The filter's feedforward gain factor (lower = smoother but slower).</param>
        /// <param name="period"></param>
        /// <returns>A new Single Pole IIR <see cref="LinearDigitalFilter"/></returns>
        public static LinearDigitalFilter SinglePoleIIR(IPIDSource source, double timeConstant,
            double period)
        {
            double gain = Math.Exp(-period / timeConstant);
            double[] ffGains = { 1.0 - gain };
            double[] fbGains = { -gain };

            return new LinearDigitalFilter(source, ffGains, fbGains);
        }

        /// <summary>
        /// Creates a first-order high-pass filter.
        /// </summary>
        /// <remarks>
        /// The filter is in the form
        /// y[n] = gain*x[n] + (-gain)*x[n-1] + gain*y[n-1]
        /// where gain = e^(-dt / T), T is the time constant in seconds.
        /// <para>This filter is stable for time constants greater then zero.</para>
        /// </remarks>
        /// <param name="source">The <see cref="IPIDSource"/> object that is used to get values.</param>
        /// <param name="timeConstant">The discrete-time time constant in seconds.</param>
        /// <param name="period">The period in seconds between samples taken by the user.</param>
        /// <returns></returns>
        public static LinearDigitalFilter HighPass(IPIDSource source, double timeConstant,
            double period)
        {
            double gain = Math.Exp(-period / timeConstant);
            double[] ffGains = { gain, -gain };
            double[] fbGains = { -gain };

            return new LinearDigitalFilter(source, ffGains, fbGains);
        }


        /// <summary>
        /// Creates a K-tap FIR moving average filter.
        /// </summary>
        /// <remarks>
        /// The filter is in the form 
        /// y[n] = 1/k * (x[k] + x[k-1] + ... + x[0]).
        /// <para></para>
        /// This filter is always stable
        /// </remarks>
        /// <param name="source">The <see cref="IPIDSource"/> object that is used to get values.</param>
        /// <param name="taps">The number of samples to average over. Higher = smoother but slower</param>
        /// <returns>A new Moving Average <see cref="LinearDigitalFilter"/></returns>
        public static LinearDigitalFilter MovingAverage(IPIDSource source, int taps)
        {
            if (taps <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(taps), "Number of taps was not at least 1.");
            }

            double[] ffGains = new double[taps];
            for (int i = 0; i < ffGains.Length; i++)
            {
                ffGains[i] = 1.0 / taps;
            }

            double[] fbGains = new double[0];

            return new LinearDigitalFilter(source, ffGains, fbGains);
        }

        /// <inheritdoc/>
        public override double Get()
        {
            double retVal = 0.0;
            for (int i = 0; i < m_inputGains.Length; i++)
            {
                retVal += m_inputs[i] * m_inputGains[i];
            }
            for (int i = 0; i < m_outputGains.Length; i++)
            {
                retVal -= m_outputs[i] * m_outputGains[i];
            }
            return retVal;
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            m_inputs.Reset();
            m_outputs.Reset();
        }
        /// <inheritdoc/>
        public override double PidGet()
        {
            double retVal = 0.0;

            // Rotate the inputs 	
            m_inputs.PushFront(PidGetSource());

            // Calculate the new value 	
            for (int i = 0; i < m_inputGains.Length; i++)
            {
                retVal += m_inputs.Get(i) * m_inputGains[i];
            }

            for (int i = 0; i < m_outputGains.Length; i++)
            {
                retVal -= m_outputs.Get(i) * m_outputGains[i];
            }


            m_outputs.PushFront(retVal);

            return retVal;
        }

    }
}
