using System;
using HAL;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.Interfaces;
using WPILib.LiveWindows;
using static HAL.HAL;
using static HAL.HALDigital;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Class to read quadrature encoders.
    /// </summary>
    public class Encoder : SensorBase, ICounterBase, IPIDSource, ILiveWindowSendable
    {
        /// <summary>
        /// Encoder Indexing Type Enum
        /// </summary>
        public enum IndexingType
        {
            ResetWhileHigh,
            ResetWhileLow,
            ResetOnFallingEdge,
            ResetOnRisingEdge,
        }

        protected internal DigitalSource m_aSource;
        protected internal DigitalSource m_bSource;
        protected DigitalSource m_indexSource = null;

        private IntPtr m_encoder;
        private int m_index;

        private Counter m_counter;
        private EncodingType m_encodingType = EncodingType.K4X;
        private int m_encodingScale;
        private bool m_allocatedA;
        private bool m_allocatedB;
        private bool m_allocatedI;
        private PIDSourceType m_pidSource;

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
                    CheckStatus(status);
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

            m_pidSource = PIDSourceType.Displacement;

            LiveWindow.LiveWindow.AddSensor("Encoder", m_aSource.ChannelForRouting, this);
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
                FreeEncoder(m_encoder, ref status);
                CheckStatus(status);
            }
        }

        public virtual int GetRaw()
        {
            int value;
            if (m_counter != null)
            {
                value = m_counter.Get();
            }
            else
            {
                int status = 0;
                value = GetEncoder(m_encoder, ref status);
                CheckStatus(status);
            }
            return value;
        }

        public virtual int Get() => (int)(GetRaw() * DecodingScaleFactor);

        public virtual void Reset()
        {
            if (m_counter != null)
                m_counter.Reset();
            else
            {
                int status = 0;
                ResetEncoder(m_encoder, ref status);
                CheckStatus(status);
            }
        }

        public virtual double GetPeriod()
        {
            double measuredPeriod;
            if (m_counter != null)
            {
                measuredPeriod = m_counter.GetPeriod() / DecodingScaleFactor;
            }
            else
            {
                int status = 0;
                measuredPeriod = GetEncoderPeriod(m_encoder, ref status);
                CheckStatus(status);
            }
            return measuredPeriod;
        }

        public virtual double MaxPeriod
        {
            set
            {
                if (m_counter != null)
                {
                    m_counter.MaxPeriod = value * DecodingScaleFactor;
                }
                else
                {
                    int status = 0;
                    SetEncoderMaxPeriod(m_encoder, value, ref status);
                    CheckStatus(status);
                }
            }
        }

        public virtual bool GetStopped()
        {
            if (m_counter != null)
            {
                return m_counter.GetStopped();
            }
            else
            {
                int status = 0;
                bool value = GetEncoderStopped(m_encoder, ref status);
                CheckStatus(status);
                return value;
            }
        }

        public virtual bool GetDirection()
        {
            if (m_counter != null)
            {
                return m_counter.GetDirection();
            }
            else
            {
                int status = 0;
                bool value = GetEncoderDirection(m_encoder, ref status);
                CheckStatus(status);
                return value;
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

        public virtual double GetDistance() => GetRaw() * DecodingScaleFactor * DistancePerPulse;

        public virtual double GetRate() => DistancePerPulse / GetPeriod();

        public double MinRate
        {
            set { MaxPeriod = DistancePerPulse / value; }
        }

        public double DistancePerPulse { get; set; }

        public void SetReverseDirection(bool direction)
        {
            if (m_counter != null)
            {
                m_counter.SetReverseDirection(direction);
            }
            else
            {
                int status = 0;
                SetEncoderReverseDirection(m_encoder, direction, ref status);
                CheckStatus(status);
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
                        SetEncoderSamplesToAverage(m_encoder, (uint)value, ref status);
                        if (status == HALErrors.PARAMETER_OUT_OF_RANGE)
                        {
                            throw new BoundaryException(BoundaryException.GetMessage(value, 1, 127));
                        }
                        CheckStatus(status);
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
                        int value = (int)GetEncoderSamplesToAverage(m_encoder, ref status);
                        CheckStatus(status);
                        return value;
                    case EncodingType.K2X:
                        return m_counter.SamplesToAverage;
                    case EncodingType.K1X:
                        return m_counter.SamplesToAverage;
                }
                return 1;
            }
        }

        public PIDSourceType PIDSourceType
        {
            get
            {
                return m_pidSource;
            }
            set
            {
                BoundaryException.AssertWithinBounds((int)value, 0, 1);
                m_pidSource = value;
            }
        }

        public virtual double PidGet()
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

        public void SetPIDSourceType(PIDSourceType pidSource)
        {
            PIDSourceType = pidSource;
        }

        public PIDSourceType GetPIDSourceType()
        {
            return PIDSourceType;
        }

        public void SetIndexSource(int channel, IndexingType type)
        {
            int status = 0;

            bool activeHigh = (type == IndexingType.ResetWhileHigh) || (type == IndexingType.ResetOnRisingEdge);
            bool edgeSensitive = (type == IndexingType.ResetOnFallingEdge) || (type == IndexingType.ResetOnRisingEdge);

            SetEncoderIndexSource(m_encoder, (uint)channel, false, activeHigh, edgeSensitive, ref status);
            CheckStatus(status);
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

            SetEncoderIndexSource(m_encoder, (uint)source.ChannelForRouting,
                source.AnalogTriggerForRouting, activeHigh, edgeSensitive, ref status);
            CheckStatus(status);
        }

        public void SetIndexSource(DigitalSource source)
        {
            SetIndexSource(source, IndexingType.ResetOnRisingEdge);
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
        public string SmartDashboardType => "Encoder";
        ///<inheritdoc />
        public void UpdateTable()
        {
            if (Table != null)
            {
                Table.PutNumber("Speed", GetRate());
                Table.PutNumber("Distance", GetDistance());
                Table.PutNumber("Distance per Tick", DistancePerPulse);
            }
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
