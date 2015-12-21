using System;
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
    /// Class to read quadrature encoders.
    /// </summary>
    /// <remarks>
    /// Quadrature encoders are devices that count shaft rotation and can sense direction. 
    /// The output of the QuadEncoder class is an integer that can count either up or down, 
    /// and can go negative for reverse direction counting. When creating Quad Encoders, a direction 
    /// is supplied that changes the sense of the output to make code more readable if the encoder 
    /// is mounter such that forwrd movement generates negative values. Quadrature encoders have 
    /// two digital outputs, a A channel and a B channel that are out of phase with each other to 
    /// allow the FPGA to do direction sensing.
    /// <para/>All encoders will immediately start counting - <see cref="Reset()"/> them if you need 
    /// them to be zeroed before use.
    /// </remarks>
    public class Encoder : SensorBase, ICounterBase, IPIDSource, ILiveWindowSendable
    {
        /// <summary>
        /// Encoder Indexing Type Enum
        /// </summary>
        public enum IndexingType
        {
            /// <summary>
            /// Reset indexing while the index pin is High
            /// </summary>
            ResetWhileHigh,
            /// <summary>
            /// Reset indexing while the index pin is low.
            /// </summary>
            ResetWhileLow,
            /// <summary>
            /// Reset indexing on the falling edge of the index pin.
            /// </summary>
            ResetOnFallingEdge,
            /// <summary>
            /// Reset indexing on the rising edge of the index pin.
            /// </summary>
            ResetOnRisingEdge,
        }

        /// <summary>
        /// The A Source
        /// </summary>
        protected internal DigitalSource m_aSource;
        /// <summary>
        /// The B Source
        /// </summary>
        protected internal DigitalSource m_bSource;
        /// <summary>
        /// The Index Source
        /// </summary>
        protected DigitalSource m_indexSource = null;

        private IntPtr m_encoder;
        private int m_index;

        private Counter m_counter;
        private readonly EncodingType m_encodingType = EncodingType.K4X;
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

        /// <summary>
        /// Construct an Encoder given A and B Channels.
        /// </summary>
        /// <remarks>The encoder will start counting immediately.</remarks>
        /// <param name="aChannel">The A channel DIO channel. 0-9 are on-board, 10-25 are on the MXP port.</param>
        /// <param name="bChannel">The B channel DIO channel. 0-9 are on-board, 10-25 are on the MXP port.</param>
        /// <param name="reverseDirection">True if to reverse the output, otherwise false</param>
        public Encoder(int aChannel, int bChannel, bool reverseDirection = false)
        {
            m_allocatedA = true;
            m_allocatedB = true;
            m_allocatedI = false;
            m_aSource = new DigitalInput(aChannel);
            m_bSource = new DigitalInput(bChannel);
            InitEncoder(reverseDirection);
        }

        /// <summary>
        /// Construct an Encoder given A and B Channels.
        /// </summary>
        /// <remarks>The encoder will start counting immediately.
        /// <para/>
        /// For encoding type, if 4X is selected, then an encoder FPGA object is used and the returned counts
        /// will be 4X the encoder spec'd value since all rising and falling edges are counted. If 1X or 2X
        /// are slected then a counter object will be used and the returned value will either exactly match the
        /// spec'd count or be double (2x) the spec'd count.
        /// </remarks>
        /// <param name="aChannel">The A channel DIO channel. 0-9 are on-board, 10-25 are on the MXP port.</param>
        /// <param name="bChannel">The B channel DIO channel. 0-9 are on-board, 10-25 are on the MXP port.</param>
        /// <param name="reverseDirection">True if to reverse the output, otherwise false</param>
        /// <param name="encodingType">Either 1X, 2X or 4X to indicate decoding scale.</param>
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

        /// <summary>
        /// Construct an Encoder given A and B Channels, and an Index pulse channel.
        /// </summary>
        /// <remarks>The encoder will start counting immediately.</remarks>
        /// <param name="aChannel">The A channel DIO channel. 0-9 are on-board, 10-25 are on the MXP port.</param>
        /// <param name="bChannel">The B channel DIO channel. 0-9 are on-board, 10-25 are on the MXP port.</param>
        /// <param name="indexChannel">The Index channel DIO channel. 0-9 are on-board, 10-25 are on the MXP port.</param>
        /// <param name="reverseDirection">True if to reverse the output, otherwise false</param>
        public Encoder(int aChannel, int bChannel,
            int indexChannel, bool reverseDirection = false)
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

        /// <summary>
        /// Construct an Encoder given precreated A and B Channels as <see cref="DigitalSource">DigitalSources</see>.
        /// </summary>
        /// <remarks>The encoder will start counting immediately.</remarks>
        /// <param name="aSource">The A channel <see cref="DigitalSource"/></param>
        /// <param name="bSource">The B channel <see cref="DigitalSource"/></param>
        /// <param name="reverseDirection">True if to reverse the output, otherwise false</param>
        public Encoder(DigitalSource aSource, DigitalSource bSource, bool reverseDirection = false)
        {
            m_allocatedA = false;
            m_allocatedB = false;
            m_allocatedI = false;
            if (aSource == null)
                throw new ArgumentNullException(nameof(aSource),"Digital Source A was null");
            m_aSource = aSource;
            if (bSource == null)
                throw new ArgumentNullException(nameof(bSource), "Digital Source B was null");
            m_bSource = bSource;
            InitEncoder(reverseDirection);
        }

        /// <summary>
        /// Construct an Encoder given precreated A and B Channels as <see cref="DigitalSource">DigitalSources</see>.
        /// </summary>
        /// <remarks>The encoder will start counting immediately.
        /// <para/>
        /// For encoding type, if 4X is selected, then an encoder FPGA object is used and the returned counts
        /// will be 4X the encoder spec'd value since all rising and falling edges are counted. If 1X or 2X
        /// are slected then a counter object will be used and the returned value will either exactly match the
        /// spec'd count or be double (2x) the spec'd count.
        /// </remarks>
        /// <param name="aSource">The A channel <see cref="DigitalSource"/></param>
        /// <param name="bSource">The B channel <see cref="DigitalSource"/></param>
        /// <param name="reverseDirection">True if to reverse the output, otherwise false</param>
        /// <param name="encodingType">Either 1X, 2X or 4X to indicate decoding scale.</param>
        public Encoder(DigitalSource aSource, DigitalSource bSource,
            bool reverseDirection, EncodingType encodingType)
        {
            m_allocatedA = false;
            m_allocatedB = false;
            m_allocatedI = false;
            m_encodingType = encodingType;
            if (aSource == null)
                throw new ArgumentNullException(nameof(aSource), "Digital Source A was null");
            m_aSource = aSource;
            if (bSource == null)
                throw new ArgumentNullException(nameof(bSource),"Digital Source B was null");
            m_aSource = aSource;
            m_bSource = bSource;
            InitEncoder(reverseDirection);
        }

        /// <summary>
        /// Construct an Encoder given precreated A, B, and Index Channels as <see cref="DigitalSource">DigitalSources</see>.
        /// </summary>
        /// <remarks>The encoder will start counting immediately.</remarks>
        /// <param name="aSource">The A channel <see cref="DigitalSource"/></param>
        /// <param name="bSource">The B channel <see cref="DigitalSource"/></param>
        /// <param name="indexSource">The Index channel <see cref="DigitalSource"/></param>
        /// <param name="reverseDirection">True if to reverse the output, otherwise false</param>
        public Encoder(DigitalSource aSource, DigitalSource bSource,
            DigitalSource indexSource, bool reverseDirection = false)
        {
            m_allocatedA = false;
            m_allocatedB = false;
            m_allocatedI = false;
            if (aSource == null)
                throw new ArgumentNullException(nameof(aSource), "Digital Source A was null");
            m_aSource = aSource;
            if (bSource == null)
                throw new ArgumentNullException(nameof(bSource), "Digital Source B was null");
            m_aSource = aSource;
            m_bSource = bSource;
            m_indexSource = indexSource;
            InitEncoder(reverseDirection);
            SetIndexSource(indexSource);
        }

        /// <summary>
        /// Gets the encoder's FPGA Index.
        /// </summary>
        public int FPGAIndex => m_index;

        /// <summary>
        /// Gets the encoder's Encoding Scale, which is used to divide raw edge counts to spec'd counts.
        /// </summary>
        public int EncodingScale => m_encodingScale;

        /// <inheritdoc/>
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

        /// <summary>
        /// Gets the raw value from the encoder.
        /// </summary>
        /// <remarks>The value is the actual count, not scaled by the scale factor.</remarks>
        /// <returns>The raw count from the encoder</returns>
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

        /// <inheritdoc/>
        public virtual int Get() => (int)(GetRaw() * DecodingScaleFactor);

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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


        /// <inheritdoc/>
        public double MaxPeriod
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

        /// <inheritdoc/>
        public bool GetStopped()
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

        /// <inheritdoc/>
        public bool GetDirection()
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

        /// <summary>
        /// Gets the distance the robot has driven since the last reset.
        /// </summary>
        /// <returns>Distance driven since the last reset scaled by the <see cref="DistancePerPulse"/></returns>
        public virtual double GetDistance() => GetRaw() * DecodingScaleFactor * DistancePerPulse;

        /// <summary>
        /// Gets the current rate of the encoder in distance per second.
        /// </summary>
        /// <returns>The current rate of the encoder scaled by the <see cref="DistancePerPulse"/></returns>
        public virtual double GetRate() => DistancePerPulse / GetPeriod();

        /// <summary>
        /// Sets the minimum rate of the device before the hardware reports it stopped.
        /// </summary>
        public double MinRate
        {
            set { MaxPeriod = DistancePerPulse / value; }
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

        /// <summary>
        /// Sets the direction sensing for this encoder.
        /// </summary>
        /// <param name="direction">True if direction should be reversed, otherwise false.</param>
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

        /// <summary>
        /// Gets or Sets the number of samples to average when caluclating the period.
        /// </summary>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <summary>
        /// Sets the index source for the encoder. Resets based on the <see cref="IndexingType"/> passed.
        /// </summary>
        /// <param name="channel">The DIO channel to set as the encoder index.</param>
        /// <param name="type">The state that will cause the encoder to reset.</param>
        public void SetIndexSource(int channel, IndexingType type = IndexingType.ResetOnRisingEdge)
        {
            int status = 0;

            bool activeHigh = (type == IndexingType.ResetWhileHigh) || (type == IndexingType.ResetOnRisingEdge);
            bool edgeSensitive = (type == IndexingType.ResetOnFallingEdge) || (type == IndexingType.ResetOnRisingEdge);

            SetEncoderIndexSource(m_encoder, (uint)channel, false, activeHigh, edgeSensitive, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Sets the index source for the encoder. Resets based on the <see cref="IndexingType"/> passed.
        /// </summary>
        /// <param name="source">The <see cref="DigitalSource"/> to set as the encoder index.</param>
        /// <param name="type">The state that will cause the encoder to reset.</param>
        public void SetIndexSource(DigitalSource source, IndexingType type = IndexingType.ResetOnRisingEdge)
        {
            int status = 0;

            bool activeHigh = (type == IndexingType.ResetWhileHigh) || (type == IndexingType.ResetOnRisingEdge);
            bool edgeSensitive = (type == IndexingType.ResetOnFallingEdge) || (type == IndexingType.ResetOnRisingEdge);

            SetEncoderIndexSource(m_encoder, (uint)source.ChannelForRouting,
                source.AnalogTriggerForRouting, activeHigh, edgeSensitive, ref status);
            CheckStatus(status);
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
