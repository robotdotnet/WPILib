

using System;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{
    public class Counter : SensorBase, CounterBase, PIDSource
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
            m_counter = HALDigital.InitializeCounter(mode, ref m_index, ref status);

            m_allocatedUpSource = false;
            m_allocatedDownSource = false;
            m_upSource = null;
            m_downSource = null;

            SetMaxPeriod(0.5);
            m_distancePerPulse = 1;

            HAL.Report(ResourceType.kResourceType_Counter, (byte)m_index, (byte)mode);
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
                HALDigital.SetCounterAverageSize(m_counter, 1, ref status);
            }
            else
            {
                SetDownSourceEdge(true, true);
                HALDigital.SetCounterAverageSize(m_counter, 2, ref status);
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

        public override void Free()
        {
            SetUpdateWhenEmpty(true);

            ClearUpSource();
            ClearDownSource();

            int status = 0;
            HALDigital.FreeCounter(m_counter, ref status);

            m_upSource = null;
            m_downSource = null;
            m_counter = IntPtr.Zero;
        }

        public int GetFPGAIndex()
        {
            return (int)m_index;
        }

        public void SetUpSource(int channel)
        {
            SetUpSource(new DigitalInput(channel));
            m_allocatedUpSource = true;
        }
        public void SetUpSource(DigitalSource source)
        {
            if (m_upSource != null && m_allocatedUpSource)
            {
                m_upSource.Free();
                m_allocatedUpSource = false;
            }
            m_upSource = source;
            int status = 0;
            HALDigital.SetCounterUpSource(m_counter, (uint)source.GetChannelForRouting(), source.GetAnalogTriggerForRouting(), ref status);
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
            HALDigital.SetCounterUpSourceEdge(m_counter, risingEdge, fallingEdge, ref status);
        }

        public void ClearUpSource()
        {
            if (m_upSource != null && m_allocatedUpSource)
            {
                m_upSource.Free();
                m_allocatedUpSource = false;
            }
            m_upSource = null;

            int status = 0;
            HALDigital.ClearCounterUpSource(m_counter, ref status);
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
                m_downSource.Free();
                m_allocatedDownSource = false;
            }
            m_downSource = source;
            int status = 0;
            HALDigital.SetCounterDownSource(m_counter, (uint)source.GetChannelForRouting(), source.GetAnalogTriggerForRouting(), ref status);
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
            HALDigital.SetCounterDownSourceEdge(m_counter, risingEdge, fallingEdge, ref status);
        }

        public void ClearDownSource()
        {
            if (m_downSource != null && m_allocatedDownSource)
            {
                m_downSource.Free();
                m_allocatedDownSource = false;
            }
            m_downSource = null;

            int status = 0;
            HALDigital.ClearCounterDownSource(m_counter, ref status);
        }

        public void SetUpDownCounterMode()
        {
            int status = 0;
            HALDigital.SetCounterUpDownMode(m_counter, ref status);
        }

        public void SetExternalDirectionMode()
        {
            int status = 0;
            HALDigital.SetCounterExternalDirectionMode(m_counter, ref status);
        }

        public void SetSemiPeriodMode(bool highSemiPeriod)
        {
            int status = 0;
            HALDigital.SetCounterSemiPeriodMode(m_counter, highSemiPeriod, ref status);
        }

        public void SetPulseLengthMode(double threshold)
        {
            int status = 0;
            HALDigital.SetCounterPulseLengthMode(m_counter, threshold, ref status);
        }

        public int Get()
        {
            int status = 0;
            int value = HALDigital.GetCounter(m_counter, ref status);
            return value;
        }

        public double GetDistance()
        {
            return Get()*m_distancePerPulse;
        }

        public void Reset()
        {
            int status = 0;
            HALDigital.ResetCounter(m_counter, ref status);
        }

        public void SetMaxPeriod(double maxPeriod)
        {
            int status = 0;
            HALDigital.SetCounterMaxPeriod(m_counter, maxPeriod, ref status);
        }

        public void SetUpdateWhenEmpty(bool enabled)
        {
            int status = 0;
            HALDigital.SetCounterUpdateWhenEmpty(m_counter, enabled, ref status);
        }

        public bool GetStopped()
        {
            int status = 0;
            bool value = HALDigital.GetCounterStopped(m_counter, ref status);
            return value;
        }

        public bool GetDirection()
        {
            int status = 0;
            bool value = HALDigital.GetCounterDirection(m_counter, ref status);
            return value;
        }

        public void SetReverseDirection(bool reverseDirection)
        {
            int status = 0;
            HALDigital.SetCounterReverseDirection(m_counter, reverseDirection, ref status);
        }

        public double GetPeriod()
        {
            int status = 0;
            double value = HALDigital.GetCounterPeriod(m_counter, ref status);
            return value;
        }

        public double GetRate()
        {
            return m_distancePerPulse/GetPeriod();
        }

        public void SetSamplesToAverage(int samplesToAverage)
        {
            int status = 0;
            HALDigital.SetCounterSamplesToAverage(m_counter, samplesToAverage, ref status);
            if (status == HALErrors.PARAMETER_OUT_OF_RANGE)
            {
                throw new BoundaryException(BoundaryException.GetMessage(samplesToAverage, 1, 127));
            }
        }

        public int GetSamplesToAverage()
        {
            int status = 0;
            int value = HALDigital.GetCounterSamplesToAverage(m_counter, ref status);
            return value;
        }

        public void SetDistancePerPulse(double distancePerPulse)
        {
            m_distancePerPulse = distancePerPulse;
        }

        public void SetPIDSourceParameter(PIDSourceParameter pidSource)
        {
            BoundaryException.AssertWithinBounds((int) pidSource, 0, 1);
            m_pidSource = pidSource;
        }

        public double PidGet()
        {
            switch (m_pidSource)
            {
                case PIDSourceParameter.Distance:
                    return GetDistance();
                case PIDSourceParameter.Rate:
                    return GetRate();
                default:
                    return 0.0;
            }
        }
    }
}
