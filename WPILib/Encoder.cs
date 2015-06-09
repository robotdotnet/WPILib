using System;
using HAL_Base;
using WPILib.Util;
using static HAL_Base.HAL;
using static HAL_Base.HALDigital;

namespace WPILib
{
    public class Encoder : SensorBase, CounterBase, IPIDSource
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
                    m_encoder = InitializeEncoder(m_aSource.ModuleForRouting,
                        (uint)m_aSource.ChannelForRouting,
                        m_aSource.AnalogTriggerForRouting, m_bSource.ModuleForRouting,
                        (uint)m_bSource.ChannelForRouting,
                        m_bSource.AnalogTriggerForRouting, reverseDirection, ref m_index, ref status);
                    m_counter = null;
                    MaxPeriod = 0.5;
                    break;
                case EncodingType.K2X:
                    m_encodingScale = 2;
                    m_counter = new Counter(m_encodingType, m_aSource, m_bSource, reverseDirection);
                    m_index = m_counter.FPGAIndex;
                    break;
                case EncodingType.K1X:
                    m_encodingScale = 1;
                    m_counter = new Counter(m_encodingType, m_aSource, m_bSource, reverseDirection);
                    m_index = m_counter.FPGAIndex;
                    break;
            }
            DistancePerPulse = 1.0;

            m_pidSource = PIDSourceParameter.Distance;

            Report(ResourceType.kResourceType_Encoder, (byte)m_index, (byte)m_encodingType);
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

        public int FPGAIndex => m_index;

        public int EncodingScale => m_encodingScale;

        public override void Dispose()
        {
            if (m_aSource != null && m_allocatedA)
            {
                m_aSource.Dispose();
                m_allocatedA = false;
            }
            if (m_bSource != null && m_allocatedB)
            {
                m_bSource.Dispose();
                m_allocatedB = false;
            }
            if (m_indexSource != null && m_allocatedI)
            {
                m_indexSource.Dispose();
                m_allocatedI = false;
            }

            m_aSource = null;
            m_bSource = null;
            m_indexSource = null;
            if (m_counter != null)
            {
                m_counter.Dispose();
                m_counter = null;
            }
            else
            {
                int status = 0;
                FreeCounter(m_encoder, ref status);
            }
        }

        public int Raw
        {
            get
            {
                int value;
                if (m_counter != null)
                {
                    value = m_counter.Value;
                }
                else
                {
                    int status = 0;
                    value = GetEncoder(m_encoder, ref status);
                }
                return value;
            }
        }

        public int Value => (int) (Raw*DecodingScaleFactor);

        public void Reset()
        {
            if (m_counter != null)
                m_counter.Reset();
            else
            {
                int status = 0;
                ResetEncoder(m_encoder, ref status);
            }
        }

        public double Period
        {
            get
            {
                double measuredPeriod;
                if (m_counter != null)
                {
                    measuredPeriod = m_counter.Period/DecodingScaleFactor;
                }
                else
                {
                    int status = 0;
                    measuredPeriod = GetEncoderPeriod(m_encoder, ref status);
                }
                return measuredPeriod;
            }
        }

        public double MaxPeriod
        {
            set
            {
                if (m_counter != null)
                {
                    m_counter.MaxPeriod = value*DecodingScaleFactor;
                }
                else
                {
                    int status = 0;
                    SetEncoderMaxPeriod(m_encoder, value, ref status);
                }
            }
        }

        public bool Stopped
        {
            get
            {
                if (m_counter != null)
                {
                    return m_counter.Stopped;
                }
                else
                {
                    int status = 0;
                    bool value = GetEncoderStopped(m_encoder, ref status);
                    return value;
                }
            }
        }

        public bool Direction
        {
            get
            {
                if (m_counter != null)
                {
                    return m_counter.Direction;
                }
                else
                {
                    int status = 0;
                    bool value = GetEncoderDirection(m_encoder, ref status);
                    return value;
                }
            }
        }

        private double DecodingScaleFactor
        {
            get
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
        }

        public double Distance => Raw*DecodingScaleFactor*DistancePerPulse;

        public double Rate => DistancePerPulse/Period;

        public double MinRate
        {
            set { MaxPeriod = DistancePerPulse/value; }
        }

        public double DistancePerPulse { get; set; }

        public bool ReverseDirection
        {
            set
            {
                if (m_counter != null)
                {
                    m_counter.Direction = value;
                }
                else
                {
                    int status = 0;
                    SetEncoderReverseDirection(m_encoder, value, ref status);
                }
            }
        }

        public int SamplesToAverage
        {
            set
            {
                switch (m_encodingType)
                {
                    case EncodingType.K4X:
                        int status = 0;
                        SetEncoderSamplesToAverage(m_encoder, (uint) value, ref status);
                        if (status == HALErrors.PARAMETER_OUT_OF_RANGE)
                        {
                            throw new BoundaryException(BoundaryException.GetMessage(value, 1, 127));
                        }
                        break;
                    case EncodingType.K2X:
                        m_counter.SamplesToAverage = value;
                        break;
                    case EncodingType.K1X:
                        m_counter.SamplesToAverage = value;
                        break;
                }
            }
            get
            {
                switch (m_encodingType)
                {
                    case EncodingType.K4X:
                        int status = 0;
                        int value = (int) GetEncoderSamplesToAverage(m_encoder, ref status);
                        return value;
                    case EncodingType.K2X:
                        return m_counter.SamplesToAverage;
                    case EncodingType.K1X:
                        return m_counter.SamplesToAverage;
                }
                return 1;
            }
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

        public void SetIndexSource(int channel, IndexingType type)
        {
            int status = 0;

            bool activeHigh = (type == IndexingType.ResetWhileHigh) || (type == IndexingType.ResetOnRisingEdge);
            bool edgeSensitive = (type == IndexingType.ResetOnFallingEdge) || (type == IndexingType.ResetOnRisingEdge);

            SetEncoderIndexSource(m_encoder, (uint) channel, false, activeHigh, edgeSensitive, ref status);
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

            SetEncoderIndexSource(m_encoder, (uint) source.ChannelForRouting,
                source.AnalogTriggerForRouting, activeHigh, edgeSensitive, ref status);
        }

        public void SetIndexSource(DigitalSource source)
        {
            SetIndexSource(source, IndexingType.ResetOnRisingEdge);
        }

    }
}
