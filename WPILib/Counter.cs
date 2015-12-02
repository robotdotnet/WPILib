using System;
using HAL;
using HAL.Base;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.Interfaces;
using WPILib.LiveWindows;
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
        private double m_distancePerPulse;

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
            m_distancePerPulse = 1;

            Report(ResourceType.kResourceType_Counter, (byte)m_index, (byte)mode);
        }

        public Counter()
        {
            InitCounter(Mode.TwoPulse);
        }

        public Counter(DigitalSource source)
        {
            if (source == null)
                throw new NullReferenceException("Digital Source given was null");
            InitCounter(Mode.TwoPulse);
            SetUpSource(source);
        }
        //TwoPulse
        public Counter(int channel)
        {
            InitCounter(Mode.TwoPulse);
            SetUpSource(channel);
        }

        //External direction is encoder style.
        public Counter(EncodingType encodingType, DigitalSource upSource, DigitalSource downSource, bool inverted)
        {
            InitCounter(Mode.ExternalDirection);
            if (encodingType != EncodingType.K2X && encodingType != EncodingType.K1X)
            {
                throw new ArgumentOutOfRangeException(nameof(encodingType), "Counters only support 1X and 2X decoding!");
            }
            if (upSource == null)
                throw new ArgumentNullException(nameof(upSource), "Up Source given was null");
            if (downSource == null)
                throw new ArgumentNullException(nameof(downSource), "Down Source given was null");
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

        public Counter(AnalogTrigger trigger)
        {
            if (trigger == null)
            {
                throw new ArgumentNullException(nameof(trigger), "The Analog Trigger given was null");
            }
            InitCounter(Mode.TwoPulse);
            SetUpSource(trigger.CreateOutput(AnalogTriggerType.State));
        }

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

        public int FPGAIndex => (int)m_index;

        public void SetUpSource(int channel)
        {
            SetUpSource(new DigitalInput(channel));
            m_allocatedUpSource = true;
        }
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

        public void SetUpSource(AnalogTrigger analogTrigger, AnalogTriggerType triggerType)
        {
            if (analogTrigger == null)
                throw new NullReferenceException("Analog Trigger given was null");
            SetUpSource(analogTrigger.CreateOutput(triggerType));
            m_allocatedUpSource = true;
        }

        public void SetUpSourceEdge(bool risingEdge, bool fallingEdge)
        {
            if (m_upSource == null)
                throw new SystemException("Up Source must be set before setting the edge!");
            int status = 0;
            SetCounterUpSourceEdge(m_counter, risingEdge, fallingEdge, ref status);
            CheckStatus(status);
        }

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


        //Down Source
        public void SetDownSource(int channel)
        {
            SetDownSource(new DigitalInput(channel));
            m_allocatedDownSource = true;
        }
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

        public void SetDownSource(AnalogTrigger analogTrigger, AnalogTriggerType triggerType)
        {
            if (analogTrigger == null)
                throw new NullReferenceException("Analog Trigger given was null");
            SetDownSource(analogTrigger.CreateOutput(triggerType));
            m_allocatedDownSource = true;
        }

        public void SetDownSourceEdge(bool risingEdge, bool fallingEdge)
        {
            if (m_downSource == null)
                throw new SystemException("Up Source must be set before setting the edge!");
            int status = 0;
            SetCounterDownSourceEdge(m_counter, risingEdge, fallingEdge, ref status);
            CheckStatus(status);
        }

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

        public void SetUpDownCounterMode()
        {
            int status = 0;
            SetCounterUpDownMode(m_counter, ref status);
            CheckStatus(status);
        }

        public void SetExternalDirectionMode()
        {
            int status = 0;
            SetCounterExternalDirectionMode(m_counter, ref status);
            CheckStatus(status);
        }

        public void SetSemiPeriodMode(bool highSemiPeriod)
        {
            int status = 0;
            SetCounterSemiPeriodMode(m_counter, highSemiPeriod, ref status);
            CheckStatus(status);
        }

        public void SetPulseLengthMode(double threshold)
        {
            int status = 0;
            SetCounterPulseLengthMode(m_counter, threshold, ref status);
            CheckStatus(status);
        }

        public virtual int Get()
        {
            int status = 0;
            int value = GetCounter(m_counter, ref status);
            CheckStatus(status);
            return value;
        }

        public virtual double GetDistance() => Get() * m_distancePerPulse;

        public virtual void Reset()
        {
            int status = 0;
            ResetCounter(m_counter, ref status);
            CheckStatus(status);
        }

        public double MaxPeriod
        {
            set
            {
                int status = 0;
                SetCounterMaxPeriod(m_counter, value, ref status);
                CheckStatus(status);
            }
        }

        public bool UpdateWhenEmpty
        {
            set
            {
                int status = 0;
                SetCounterUpdateWhenEmpty(m_counter, value, ref status);
                CheckStatus(status);
            }
        }

        public virtual bool GetStopped()
        {
            int status = 0;
            bool value = GetCounterStopped(m_counter, ref status);
            CheckStatus(status);
            return value;
        }

        public bool GetDirection()
        {
            int status = 0;
            bool value = GetCounterDirection(m_counter, ref status);
            CheckStatus(status);
            return value;
        }

        public void SetReverseDirection(bool direction)
        {
            int status = 0;
            SetCounterReverseDirection(m_counter, direction, ref status);
            CheckStatus(status);
        }

        public virtual double GetPeriod()
        {
            int status = 0;
            double value = GetCounterPeriod(m_counter, ref status);
            CheckStatus(status);
            return value;
        }

        public virtual double GetRate() => m_distancePerPulse / GetPeriod();

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

        public double DistancePerPulse
        {
            get { return m_distancePerPulse; }
            set { m_distancePerPulse = value; }
        }

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
