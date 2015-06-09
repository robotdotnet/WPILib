

using System;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_Base;
using static HAL_Base.HAL;
using static HAL_Base.HALDigital;

namespace WPILib
{
    public class Counter : SensorBase, CounterBase, IPIDSource
    {
        private DigitalSource m_upSource;
        private DigitalSource m_downSource;
        private bool m_allocatedUpSource;
        private bool m_allocatedDownSource;
        private IntPtr m_counter;
        private uint m_index;
        private PIDSourceParameter m_pidSource;
        private double m_distancePerPulse;

        private void InitCounter(Mode mode)
        {
            int status = 0;
            m_counter = InitializeCounter(mode, ref m_index, ref status);

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

        public Counter(int channel)
        {
            InitCounter(Mode.TwoPulse);
            SetUpSource(channel);
        }

        public Counter(EncodingType encodingType, DigitalSource upSource, DigitalSource downSource, bool inverted)
        {
            InitCounter(Mode.ExternalDirection);
            if (encodingType != EncodingType.K2X && encodingType != EncodingType.K1X)
            {
                throw new SystemException("Counters only support 1X and 2X decoding!");
            }
            if (upSource == null)
                throw new NullReferenceException("Up Source given was null");
            SetUpSource(upSource);
            if (downSource == null)
                throw new NullReferenceException("Down Source given was null");
            SetDownSource(downSource);
            int status = 0;
            if (encodingType == EncodingType.K1X)
            {
                SetUpSourceEdge(true, false);
                SetCounterAverageSize(m_counter, 1, ref status);
            }
            else
            {
                SetDownSourceEdge(true, true);
                SetCounterAverageSize(m_counter, 2, ref status);
            }

            SetDownSourceEdge(inverted, true);
        }

        public Counter(AnalogTrigger trigger)
        {
            if (trigger == null)
            {
                throw new NullReferenceException("The Analog Trigger given was null");
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

            m_upSource = null;
            m_downSource = null;
            m_counter = IntPtr.Zero;
        }

        public int FPGAIndex => (int) m_index;

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
        }

        public void SetUpDownCounterMode()
        {
            int status = 0;
            SetCounterUpDownMode(m_counter, ref status);
        }

        public void SetExternalDirectionMode()
        {
            int status = 0;
            SetCounterExternalDirectionMode(m_counter, ref status);
        }

        public void SetSemiPeriodMode(bool highSemiPeriod)
        {
            int status = 0;
            SetCounterSemiPeriodMode(m_counter, highSemiPeriod, ref status);
        }

        public void SetPulseLengthMode(double threshold)
        {
            int status = 0;
            SetCounterPulseLengthMode(m_counter, threshold, ref status);
        }

        public int Value
        {
            get
            {
                int status = 0;
                int value = GetCounter(m_counter, ref status);
                return value;
            }
        }

        public double Distance => Value*m_distancePerPulse;

        public void Reset()
        {
            int status = 0;
            ResetCounter(m_counter, ref status);
        }

        public double MaxPeriod
        {
            set
            {
                int status = 0;
                SetCounterMaxPeriod(m_counter, value, ref status);
            }
        }

        public bool UpdateWhenEmpty
        {
            set
            {
                int status = 0;
                SetCounterUpdateWhenEmpty(m_counter, value, ref status);
            }
        }

        public bool Stopped
        {
            get
            {
                int status = 0;
                bool value = GetCounterStopped(m_counter, ref status);
                return value;
            }
        }

        public bool Direction
        {
            get
            {
                int status = 0;
                bool value = GetCounterDirection(m_counter, ref status);
                return value;
            }
            set
            {
                int status = 0;
                SetCounterReverseDirection(m_counter, value, ref status);
            }
        }

        public double Period
        {
            get
            {
                int status = 0;
                double value = GetCounterPeriod(m_counter, ref status);
                return value;
            }
        }

        public double Rate => m_distancePerPulse/Period;

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
            }
            get
            {
                int status = 0;
                int value = GetCounterSamplesToAverage(m_counter, ref status);
                return value;
            }
        }

        public double DistancePerPulse
        {
            set { m_distancePerPulse = value; }
        }

        public PIDSourceParameter PIDSourceParameter
        {
            set
            {
                BoundaryException.AssertWithinBounds((int) value, 0, 1);
                m_pidSource = value;
            }
        }

        public double PidGet
        {
            get
            {
                switch (m_pidSource)
                {
                    case PIDSourceParameter.Distance:
                        return Distance;
                    case PIDSourceParameter.Rate:
                        return Rate;
                    default:
                        return 0.0;
                }
            }
        }
    }
}
