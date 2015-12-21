using System;
using HAL;
using HAL.Base;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.Interfaces;
using WPILib.LiveWindow;
using static HAL.Base.HAL;
using static HAL.Base.HALDigital;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Class for counting the number of ticks on a digital input channel.
    /// </summary>
    /// <remarks>This is a general purpose class for counting repetitive events.
    /// It can return the number of counts, the period of the most recent cycle,
    /// and detect when the signal being counted has stopped by supplying a 
    /// maximum cycle time.
    /// <para>All counters will immediately start counting - <see cref="Reset()"/> them if you need
    /// them to be zeroes before use.</para></remarks>
    public class Counter : SensorBase, ICounterBase, IPIDSource, ILiveWindowSendable
    {
        internal DigitalSource m_upSource;
        internal DigitalSource m_downSource;
        private bool m_allocatedUpSource;
        private bool m_allocatedDownSource;
        private IntPtr m_counter;
        private uint m_index;
        private PIDSourceType m_pidSource;

        private void InitCounter(Mode mode)
        {
            int status = 0;
            m_counter = InitializeCounter(mode, ref m_index, ref status);
            CheckStatus(status);

            m_allocatedUpSource = false;
            m_allocatedDownSource = false;
            m_upSource = null;
            m_downSource = null;

            MaxPeriod = 0.5;
            DistancePerPulse = 1;

            Report(ResourceType.kResourceType_Counter, (byte)m_index, (byte)mode);
        }

        /// <summary>
        /// Creates an instance of a counter where no sources are selected.
        /// </summary>
        /// <remarks>
        /// If this constructor is used, all sources must then be specified by calling
        /// functions to specify the upsource, and the downsource independently.
        /// <para>The counter will start counting immediately.</para>
        /// </remarks>
        public Counter()
        {
            InitCounter(Mode.TwoPulse);
        }

        /// <summary>
        /// Creates an instacne of a counter from a <see cref="DigitalSource"/>. 
        /// </summary>
        /// <remarks>
        /// This constructor should be used if an existing <see cref="DigitalSource"/> is to
        /// be shared by multiple other objects such as <see cref="Encoder">Encoders</see> or if
        /// the <see cref="DigitalSource"/> is not a DIO Channel (for instance a <see cref="AnalogTrigger"/>.
        /// <para>The counter will start couting immediately.</para>
        /// </remarks>
        /// <param name="source">The <see cref="DigitalSource"/> to count.</param>
        public Counter(DigitalSource source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Digital Source given was null");
            InitCounter(Mode.TwoPulse);
            SetUpSource(source);
        }
        
        /// <summary>
        /// Create an instance of a Counter on the specified digital input, as an up counter.
        /// </summary>
        /// <remarks>
        /// The counter will start counting immediately.
        /// </remarks>
        /// <param name="channel">The DIO channel to use as the up source. [0..9] on RIO, [10..25] on MXP.</param>
        public Counter(int channel)
        {
            InitCounter(Mode.TwoPulse);
            SetUpSource(channel);
        }

        /// <summary>
        /// Creates an instance of a Counter object from specified <see cref="DigitalSource">
        /// Digital Sources</see> for up and down counts.
        /// </summary>
        /// <remarks>
        /// The counter will start counting immediately.
        /// </remarks>
        /// <param name="encodingType">The EncodingType for the counter. <see cref="EncodingType.K4X"/> is not supported.</param>
        /// <param name="upSource">The <see cref="DigitalSource"/> to use for up counting.</param>
        /// <param name="downSource">The <see cref="DigitalSource"/> to use for down counting.</param>
        /// <param name="inverted">True to invert the direction of counting.</param>
        public Counter(EncodingType encodingType, DigitalSource upSource, DigitalSource downSource, bool inverted)
        {
            if (encodingType != EncodingType.K2X && encodingType != EncodingType.K1X)
            {
                throw new ArgumentOutOfRangeException(nameof(encodingType), "Counters only support 1X and 2X decoding!");
            }
            if (upSource == null)
                throw new ArgumentNullException(nameof(upSource), "Up Source given was null");
            if (downSource == null)
                throw new ArgumentNullException(nameof(downSource), "Down Source given was null");
            InitCounter(Mode.ExternalDirection);
            SetUpSource(upSource);
            SetDownSource(downSource);
            int status = 0;
            if (encodingType == EncodingType.K1X)
            {
                SetUpSourceEdge(true, false);
                SetCounterAverageSize(m_counter, 1, ref status);
            }
            else
            {
                SetUpSourceEdge(true, true);
                SetCounterAverageSize(m_counter, 2, ref status);
            }
            CheckStatus(status);
            SetDownSourceEdge(inverted, true);
        }

        /// <summary>
        /// Create an instance of a Counter object, using an AnalogTrigger.
        /// </summary>
        /// <remarks>
        /// Uses the trigger state output from the analog trigger.
        /// <para>The counter will start counting immediately.</para>
        /// </remarks>
        /// <param name="trigger">The <see cref="AnalogTrigger"/> to count.</param>
        public Counter(AnalogTrigger trigger)
        {
            if (trigger == null)
            {
                throw new ArgumentNullException(nameof(trigger), "The Analog Trigger given was null");
            }
            InitCounter(Mode.TwoPulse);
            SetUpSource(trigger.CreateOutput(AnalogTriggerType.State));
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            UpdateWhenEmpty = true;

            ClearUpSource();
            ClearDownSource();

            int status = 0;
            FreeCounter(m_counter, ref status);
            CheckStatus(status);

            m_upSource = null;
            m_downSource = null;
            m_counter = IntPtr.Zero;
        }

        /// <summary>
        /// Gets the counters FPGA Index.
        /// </summary>
        public int FPGAIndex => (int)m_index;

        /// <summary>
        /// Sets the up source for the counter as a digital input.
        /// </summary>
        /// <param name="channel">The DIO channel to count up. [0..9] on RIO, [10..25] are on the MXP.</param>
        public void SetUpSource(int channel)
        {
            SetUpSource(new DigitalInput(channel));
            m_allocatedUpSource = true;
        }

        /// <summary>
        /// Sets the up source object for the counter.
        /// </summary>
        /// <param name="source">The DigitalSource to use for counting up.</param>
        public void SetUpSource(DigitalSource source)
        {
            if (m_upSource != null && m_allocatedUpSource)
            {
                m_upSource.Dispose();
                m_allocatedUpSource = false;
            }
            m_upSource = source;
            int status = 0;
            SetCounterUpSource(m_counter, (uint)source.ChannelForRouting, source.AnalogTriggerForRouting, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Sets the up source for the counter as a <see cref="AnalogTrigger"/>.
        /// </summary>
        /// <param name="analogTrigger">The AnalogTrigger object that is used for the up source.</param>
        /// <param name="triggerType">The anlog trigger output that will trigger the counter.</param>
        public void SetUpSource(AnalogTrigger analogTrigger, AnalogTriggerType triggerType)
        {
            if (analogTrigger == null)
                throw new ArgumentNullException(nameof(analogTrigger), "Analog Trigger given was null");
            SetUpSource(analogTrigger.CreateOutput(triggerType));
            m_allocatedUpSource = true;
        }

        /// <summary>
        /// Set the edge sensitivity on an up counting source.
        /// </summary>
        /// <param name="risingEdge">True to count on rising edge.</param>
        /// <param name="fallingEdge">True to count on falling edge.</param>
        public void SetUpSourceEdge(bool risingEdge, bool fallingEdge)
        {
            if (m_upSource == null)
                throw new SystemException("Up Source must be set before setting the edge!");
            int status = 0;
            SetCounterUpSourceEdge(m_counter, risingEdge, fallingEdge, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Disable the up counting source of the counter.
        /// </summary>
        public void ClearUpSource()
        {
            if (m_upSource != null && m_allocatedUpSource)
            {
                m_upSource.Dispose();
                m_allocatedUpSource = false;
            }
            m_upSource = null;

            int status = 0;
            ClearCounterUpSource(m_counter, ref status);
            CheckStatus(status);
        }


        /// <summary>
        /// Sets the down source for the counter as a digital input.
        /// </summary>
        /// <param name="channel">The DIO channel to count down. [0..9] on RIO, [10..25] are on the MXP.</param>
        public void SetDownSource(int channel)
        {
            SetDownSource(new DigitalInput(channel));
            m_allocatedDownSource = true;
        }

        /// <summary>
        /// Sets the down source object for the counter.
        /// </summary>
        /// <param name="source">The DigitalSource to use for counting down.</param>
        public void SetDownSource(DigitalSource source)
        {
            if (m_downSource != null && m_allocatedDownSource)
            {
                m_downSource.Dispose();
                m_allocatedDownSource = false;
            }
            m_downSource = source;
            int status = 0;
            SetCounterDownSource(m_counter, (uint)source.ChannelForRouting, source.AnalogTriggerForRouting, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Sets the down source for the counter as a <see cref="AnalogTrigger"/>.
        /// </summary>
        /// <param name="analogTrigger">The AnalogTrigger object that is used for the down source.</param>
        /// <param name="triggerType">The anlog trigger output that will trigger the counter.</param>
        public void SetDownSource(AnalogTrigger analogTrigger, AnalogTriggerType triggerType)
        {
            if (analogTrigger == null)
                throw new ArgumentNullException(nameof(analogTrigger), "Analog Trigger given was null");
            SetDownSource(analogTrigger.CreateOutput(triggerType));
            m_allocatedDownSource = true;
        }

        /// <summary>
        /// Set the edge sensitivity on a down counting source.
        /// </summary>
        /// <param name="risingEdge">True to count on rising edge.</param>
        /// <param name="fallingEdge">True to count on falling edge.</param>
        public void SetDownSourceEdge(bool risingEdge, bool fallingEdge)
        {
            if (m_downSource == null)
                throw new SystemException("Up Source must be set before setting the edge!");
            int status = 0;
            SetCounterDownSourceEdge(m_counter, risingEdge, fallingEdge, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Disable the down counting source of the counter.
        /// </summary>
        public void ClearDownSource()
        {
            if (m_downSource != null && m_allocatedDownSource)
            {
                m_downSource.Dispose();
                m_allocatedDownSource = false;
            }
            m_downSource = null;

            int status = 0;
            ClearCounterDownSource(m_counter, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Set standard up/down counting mode on this counter.
        /// </summary>
        /// <remarks>Up and down counts are sourced independently from two outputs.</remarks>
        public void SetUpDownCounterMode()
        {
            int status = 0;
            SetCounterUpDownMode(m_counter, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Set external direction mode on this counter.
        /// </summary>
        /// <remarks>Counts are sourced on the up counter input.
        /// The down counter input represents the direction to count.</remarks>
        public void SetExternalDirectionMode()
        {
            int status = 0;
            SetCounterExternalDirectionMode(m_counter, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Set Semi-period mode on this counter.
        /// </summary>
        /// <remarks>Counts up on both rising and falling edges.</remarks>
        /// <param name="highSemiPeriod">True to count up on both rising and falling.</param>
        public void SetSemiPeriodMode(bool highSemiPeriod)
        {
            int status = 0;
            SetCounterSemiPeriodMode(m_counter, highSemiPeriod, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Configure the counter to count in up or down based on the length of the input pulse.
        /// </summary>
        /// <remarks>This mode is most useful for direction sensitive gear sensors.</remarks>
        /// <param name="threshold">The pulse length beyond which the counter counts in the opposite direction (seconds).</param>
        public void SetPulseLengthMode(double threshold)
        {
            int status = 0;
            SetCounterPulseLengthMode(m_counter, threshold, ref status);
            CheckStatus(status);
        }

        /// <inheritdoc/>
        public virtual int Get()
        {
            int status = 0;
            int value = GetCounter(m_counter, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Gets the distance the robot has driven since the last reset.
        /// </summary>
        /// <returns>Distance driven since the last reset scaled by the <see cref="DistancePerPulse"/></returns>
        public virtual double GetDistance() => Get() * DistancePerPulse;

        /// <inheritdoc/>
        public virtual void Reset()
        {
            int status = 0;
            ResetCounter(m_counter, ref status);
            CheckStatus(status);
        }

        /// <inheritdoc/>
        public double MaxPeriod
        {
            set
            {
                int status = 0;
                SetCounterMaxPeriod(m_counter, value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Sets whether you want to continue updating the event timer output
        /// when there are no samples captured.
        /// </summary>
        /// <remarks>The output of the event timer has a buffer of periods that are averaged
        /// and posted to a register on the FPGA. When the timer detects that the event
        /// source has stopped (based on the <see cref="MaxPeriod"/>) the buffer of samples
        /// to be averaged is emptied. If you enable the update when empty, you will be notfied 
        /// of the stopped source and the event time will report 0 samples. If you disable update
        /// when empty, the most recent average will remain on the output until a new sample
        /// is acquired. You will never see 0 samples output (except when there have been no
        /// events since an FPGA reset) and you will likely not se the stopped bit become true
        /// (since it is updated at the end of an average and there are no samples to average.)</remarks>
        public bool UpdateWhenEmpty
        {
            set
            {
                int status = 0;
                SetCounterUpdateWhenEmpty(m_counter, value, ref status);
                CheckStatus(status);
            }
        }

        /// <inheritdoc/>
        public bool GetStopped()
        {
            int status = 0;
            bool value = GetCounterStopped(m_counter, ref status);
            CheckStatus(status);
            return value;
        }

        /// <inheritdoc/>
        public bool GetDirection()
        {
            int status = 0;
            bool value = GetCounterDirection(m_counter, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Sets the direction sensing for this encoder.
        /// </summary>
        /// <param name="direction">True if direction should be reversed, otherwise false.</param>
        public void SetReverseDirection(bool direction)
        {
            int status = 0;
            SetCounterReverseDirection(m_counter, direction, ref status);
            CheckStatus(status);
        }

        /// <inheritdoc/>
        public virtual double GetPeriod()
        {
            int status = 0;
            double value = GetCounterPeriod(m_counter, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Gets the current rate of the encoder in distance per second.
        /// </summary>
        /// <returns>The current rate of the encoder scaled by the <see cref="DistancePerPulse"/></returns>
        public virtual double GetRate() => DistancePerPulse / GetPeriod();

        /// <summary>
        /// Gets or Sets the number of samples to average when caluclating the period.
        /// </summary>
        public int SamplesToAverage
        {
            set
            {
                int status = 0;
                SetCounterSamplesToAverage(m_counter, value, ref status);
                if (status == HALErrors.PARAMETER_OUT_OF_RANGE)
                {
                    throw new BoundaryException(BoundaryException.GetMessage(value, 1, 127));
                }
                CheckStatus(status);
            }
            get
            {
                int status = 0;
                int value = GetCounterSamplesToAverage(m_counter, ref status);
                CheckStatus(status);
                return value;
            }
        }

        /// <summary>
        /// Sets the distance per pulse for this encoder.
        /// </summary>
        /// <remarks>
        /// This sets the multiplier used to determine the distance driven based on the count value 
        /// from the encoder. Do not include the decoding type in the scale. The library arleady compensates 
        /// for the decoding type. Set this value based on the encoders rated Pulses Per Revolution and factor 
        /// in gearing reductions following the encoder shaft.
        /// </remarks>
        public double DistancePerPulse { get; set; }

        /// <inheritdoc/>
        public PIDSourceType PIDSourceType
        {
            get { return m_pidSource; }
            set
            {
                BoundaryException.AssertWithinBounds((int)value, 0, 1);
                m_pidSource = value;
            }
        }

        /// <inheritdoc/>
        public double PidGet()
        {
            switch (m_pidSource)
            {
                case PIDSourceType.Displacement:
                    return GetDistance();
                case PIDSourceType.Rate:
                    return GetRate();
                default:
                    return 0.0;
            }
        }

        ///<inheritdoc />
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }
        ///<inheritdoc />
        public ITable Table { get; private set; }

        ///<inheritdoc />
        public string SmartDashboardType => "Counter";
        ///<inheritdoc />
        public void UpdateTable()
        {
            Table?.PutNumber("Value", Get());
        }
        ///<inheritdoc />
        public void StartLiveWindowMode()
        {
        }

        ///<inheritdoc />
        public void StopLiveWindowMode()
        {
        }
    }
}
