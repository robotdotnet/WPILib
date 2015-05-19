using System;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{
    public class Encoder : SensorBase, CounterBase, PIDSource
    {
        public enum IndexingType
        {
            ResetWhileHigh,
            ResetWhileLow,
            ResetOnFallingEdge,
            ResetOnRisingEdge,
        }

        protected DigitalSource m_aSource;
        protected DigitalSource m_bSource;
        protected DigitalSource m_indexSource = null;

        private IntPtr m_encoder;
        private int m_index;
        private double m_distancePerPulse;

        private Counter m_counter;
        private EncodingType m_encodingType = EncodingType.K4X;
        private int m_encodingScale;
        private bool m_allocatedA;
        private bool m_allocatedB;
        private bool m_allocatedI;
        private PIDSourceParameter m_pidSource;

        private void InitEncoder(bool reverseDirection)
        {
            switch (m_encodingType)
            {
                case EncodingType.K4X:
                    m_encodingScale = 4;
                    int status = 0;
                    m_encoder = HALDigital.InitializeEncoder(m_aSource.GetModuleForRouting(),
                        (uint)m_aSource.GetChannelForRouting(),
                        m_aSource.GetAnalogTriggerForRouting(), m_bSource.GetModuleForRouting(),
                        (uint)m_bSource.GetChannelForRouting(),
                        m_bSource.GetAnalogTriggerForRouting(), reverseDirection, ref m_index, ref status);
                    m_counter = null;
                    SetMaxPeriod(0.5);
                    break;
                case EncodingType.K2X:
                    m_encodingScale = 2;
                    m_counter = new Counter(m_encodingType, m_aSource, m_bSource, reverseDirection);
                    m_index = m_counter.GetFPGAIndex();
                    break;
                case EncodingType.K1X:
                    m_encodingScale = 1;
                    m_counter = new Counter(m_encodingType, m_aSource, m_bSource, reverseDirection);
                    m_index = m_counter.GetFPGAIndex();
                    break;
            }
            m_distancePerPulse = 1.0;

            m_pidSource = PIDSourceParameter.Distance;

            HAL.Report(ResourceType.kResourceType_Encoder, (byte)m_index, (byte)m_encodingType);
        }

        public Encoder(int aChannel, int bChannel, bool reverseDirection)
        {
            m_allocatedA = true;
            m_allocatedB = true;
            m_allocatedI = false;
            m_aSource = new DigitalInput(aChannel);
            m_bSource = new DigitalInput(bChannel);
            InitEncoder(reverseDirection);
        }

        public Encoder(int aChannel, int bChannel) :
            this(aChannel, bChannel, false)
        {
        }

        public Encoder(int aChannel, int bChannel, bool reverseDirection, EncodingType encodingType)
        {
            m_allocatedA = true;
            m_allocatedB = true;
            m_allocatedI = false;
            m_encodingType = encodingType;
            m_aSource = new DigitalInput(aChannel);
            m_bSource = new DigitalInput(bChannel);
            InitEncoder(reverseDirection);
        }

        public Encoder(int aChannel, int bChannel,
            int indexChannel, bool reverseDirection)
        {
            m_allocatedA = true;
            m_allocatedB = true;
            m_allocatedI = true;
            m_aSource = new DigitalInput(aChannel);
            m_bSource = new DigitalInput(bChannel);
            m_indexSource = new DigitalInput(indexChannel);
            InitEncoder(reverseDirection);
            SetIndexSource(indexChannel);
        }

        public Encoder(int aChannel, int bChannel, int indexChannel) :
            this(aChannel, bChannel, indexChannel, false)
        {

        }

        public Encoder(DigitalSource aSource, DigitalSource bSource, bool reverseDirection)
        {
            m_allocatedA = false;
            m_allocatedB = false;
            m_allocatedI = false;
            if (aSource == null)
                throw new NullReferenceException("Digital Source A was null");
            m_aSource = aSource;
            if (bSource == null)
                throw new NullReferenceException("Digital Source B was null");
            m_bSource = bSource;
            InitEncoder(reverseDirection);
        }

        public Encoder(DigitalSource aSource, DigitalSource bSource) :
            this(aSource, bSource, false)
        {

        }

        public Encoder(DigitalSource aSource, DigitalSource bSource,
            bool reverseDirection, EncodingType encodingType)
        {
            m_allocatedA = false;
            m_allocatedB = false;
            m_allocatedI = false;
            m_encodingType = encodingType;
            if (aSource == null)
                throw new NullReferenceException("Digital Source A was null");
            m_aSource = aSource;
            if (bSource == null)
                throw new NullReferenceException("Digital Source B was null");
            m_aSource = aSource;
            m_bSource = bSource;
            InitEncoder(reverseDirection);
        }

        public Encoder(DigitalSource aSource, DigitalSource bSource,
            DigitalSource indexSource, bool reverseDirection)
        {
            m_allocatedA = false;
            m_allocatedB = false;
            m_allocatedI = false;
            if (aSource == null)
                throw new NullReferenceException("Digital Source A was null");
            m_aSource = aSource;
            if (bSource == null)
                throw new NullReferenceException("Digital Source B was null");
            m_aSource = aSource;
            m_bSource = bSource;
            m_indexSource = indexSource;
            InitEncoder(reverseDirection);
            SetIndexSource(indexSource);
        }

        public Encoder(DigitalSource aSource, DigitalSource bSource, DigitalSource indexSource) :
            this(aSource, bSource, indexSource, false)
        {

        }

        public int GetFPGAIndex()
        {
            return m_index;
        }

        public int GetEncodingScale()
        {
            return m_encodingScale;
        }

        public override void Free()
        {
            if (m_aSource != null && m_allocatedA)
            {
                m_aSource.Free();
                m_allocatedA = false;
            }
            if (m_bSource != null && m_allocatedB)
            {
                m_bSource.Free();
                m_allocatedB = false;
            }
            if (m_indexSource != null && m_allocatedI)
            {
                m_indexSource.Free();
                m_allocatedI = false;
            }

            m_aSource = null;
            m_bSource = null;
            m_indexSource = null;
            if (m_counter != null)
            {
                m_counter.Free();
                m_counter = null;
            }
            else
            {
                int status = 0;
                HALDigital.FreeCounter(m_encoder, ref status);
            }
        }

        public int GetRaw()
        {
            int value;
            if (m_counter != null)
            {
                value = m_counter.Get();
            }
            else
            {
                int status = 0;
                value = HALDigital.GetEncoder(m_encoder, ref status);
            }
            return value;
        }

        public int Get()
        {
            return (int) (GetRaw()*DecodingScaleFactor());
        }

        public void Reset()
        {
            if (m_counter != null)
                m_counter.Reset();
            else
            {
                int status = 0;
                HALDigital.ResetEncoder(m_encoder, ref status);
            }
        }

        public double GetPeriod()
        {
            double measuredPeriod;
            if (m_counter != null)
            {
                measuredPeriod = m_counter.GetPeriod()/DecodingScaleFactor();
            }
            else
            {
                int status = 0;
                measuredPeriod = HALDigital.GetEncoderPeriod(m_encoder, ref status);
            }
            return measuredPeriod;
        }

        public void SetMaxPeriod(double maxPeriod)
        {
            if (m_counter != null)
            {
                m_counter.SetMaxPeriod(maxPeriod*DecodingScaleFactor());
            }
            else
            {
                int status = 0;
                HALDigital.SetEncoderMaxPeriod(m_encoder, maxPeriod, ref status);
            }
        }

        public bool GetStopped()
        {
            if (m_counter != null)
            {
                return m_counter.GetStopped();
            }
            else
            {
                int status = 0;
                bool value = HALDigital.GetEncoderStopped(m_encoder, ref status);
                return value;
            }
        }

        public bool GetDirection()
        {
            if (m_counter != null)
            {
                return m_counter.GetDirection();
            }
            else
            {
                int status = 0;
                bool value = HALDigital.GetEncoderDirection(m_encoder, ref status);
                return value;
            }
        }

        private double DecodingScaleFactor()
        {
            switch (m_encodingType)
            {
                case EncodingType.K4X:
                    return 0.25;
                case EncodingType.K2X:
                    return 0.5;
                case EncodingType.K1X:
                    return 1.0;
                default:
                    return 0.0;
            }
        }

        public double GetDistance()
        {
            return GetRaw()*DecodingScaleFactor()*m_distancePerPulse;
        }

        public double GetRate()
        {
            return m_distancePerPulse/GetPeriod();
        }

        public void SetMinRate(double minRate)
        {
            SetMaxPeriod(m_distancePerPulse/minRate);
        }

        public void SetDistancePerPulse(double distancePerPulse)
        {
            m_distancePerPulse = distancePerPulse;
        }

        public void SetReverseDirection(bool reverseDirection)
        {
            if (m_counter != null)
            {
                m_counter.SetReverseDirection(reverseDirection);
            }
            else
            {
                int status = 0;
                HALDigital.SetEncoderReverseDirection(m_encoder, reverseDirection, ref status);
            }
        }

        public void SetSamplesToAverage(int samplesToAverage)
        {
            switch (m_encodingType)
            {
                case EncodingType.K4X:
                    int status = 0;
                    HALDigital.SetEncoderSamplesToAverage(m_encoder, (uint) samplesToAverage, ref status);
                    if (status == HALErrors.PARAMETER_OUT_OF_RANGE)
                    {
                        throw new BoundaryException(BoundaryException.GetMessage(samplesToAverage, 1, 127));
                    }
                    break;
                case EncodingType.K2X:
                    m_counter.SetSamplesToAverage(samplesToAverage);
                    break;
                case EncodingType.K1X:
                    m_counter.SetSamplesToAverage(samplesToAverage);
                    break;
            }
        }

        public int GetSamplesToAverage()
        {
            switch (m_encodingType)
            {
                case EncodingType.K4X:
                    int status = 0;
                    int value = (int)HALDigital.GetEncoderSamplesToAverage(m_encoder, ref status);
                    return value;
                case EncodingType.K2X:
                    return m_counter.GetSamplesToAverage();
                case EncodingType.K1X:
                    return m_counter.GetSamplesToAverage();
            }
            return 1;
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

        public void SetIndexSource(int channel, IndexingType type)
        {
            int status = 0;

            bool activeHigh = (type == IndexingType.ResetWhileHigh) || (type == IndexingType.ResetOnRisingEdge);
            bool edgeSensitive = (type == IndexingType.ResetOnFallingEdge) || (type == IndexingType.ResetOnRisingEdge);

            HALDigital.SetEncoderIndexSource(m_encoder, (uint) channel, false, activeHigh, edgeSensitive, ref status);
        }

        public void SetIndexSource(int channel)
        {
            SetIndexSource(channel, IndexingType.ResetOnRisingEdge);
        }

        public void SetIndexSource(DigitalSource source, IndexingType type)
        {
            int status = 0;

            bool activeHigh = (type == IndexingType.ResetWhileHigh) || (type == IndexingType.ResetOnRisingEdge);
            bool edgeSensitive = (type == IndexingType.ResetOnFallingEdge) || (type == IndexingType.ResetOnRisingEdge);

            HALDigital.SetEncoderIndexSource(m_encoder, (uint) source.GetChannelForRouting(),
                source.GetAnalogTriggerForRouting(), activeHigh, edgeSensitive, ref status);
        }

        public void SetIndexSource(DigitalSource source)
        {
            SetIndexSource(source, IndexingType.ResetOnRisingEdge);
        }

    }
}
