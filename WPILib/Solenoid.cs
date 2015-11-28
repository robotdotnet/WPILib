using System;
using HAL_Base;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.LiveWindows;
using static HAL_Base.HAL;
using static HAL_Base.HALSolenoid;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Solenoid class for running high voltage digital output.
    /// </summary><remarks>
    /// The Solenoid class is typically used for pneumatics solenoids, but could be used
    /// for any device within the current spec of the PCM. 
    /// </remarks>
    public class Solenoid : SolenoidBase, ILiveWindowSendable, ITableListener
    {
        private readonly int m_channel;//The channel to control.
        private IntPtr m_solenoidPort;
        private readonly object m_lockObject = new object();

        /// <summary>
        /// Common function to implement constructor behavior.
        /// </summary>
        private void InitSolenoid()
        {
            lock (m_lockObject)
            {
                CheckSolenoidModule(m_moduleNumber);
                CheckSolenoidChannel(m_channel);

                //Check to see if it is already allocated
                s_allocated.Allocate(m_moduleNumber*SolenoidChannels + m_channel,
                    "Solenoid channel " + m_channel + " on module " + m_moduleNumber + " is already allocated");

                int status = 0;

                IntPtr port = GetPortWithModule((byte)m_moduleNumber, (byte)m_channel);
                m_solenoidPort = InitializeSolenoidPort(port, ref status);
                CheckStatus(status);
                LiveWindow.AddActuator("Solenoid", m_moduleNumber, m_channel, this);
                Report(ResourceType.kResourceType_Solenoid, (byte)m_channel, (byte)m_moduleNumber);
            }
        }

        /// <summary>
        /// Constructor using the default PCM ID (0)
        /// </summary>
        /// <param name="channel">The channel on the PCM to control (0..7).</param>
        public Solenoid(int channel)
            : this(DefaultSolenoidModule, channel)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="moduleNumber">The CAN ID of the PCM the solenoid is attached to.</param>
        /// <param name="channel">The channel on the PCM to control (0..7).</param>
        public Solenoid(int moduleNumber, int channel)
            : base(moduleNumber)
        {
            m_channel = channel;
            InitSolenoid();
        }

        ///<inheritdoc/>
        public override void Dispose()
        {
            lock (m_lockObject)
            {
                s_allocated.Deallocate(m_moduleNumber * SolenoidChannels + m_channel);
                FreeSolenoidPort(m_solenoidPort);
                m_solenoidPort = IntPtr.Zero;
                base.Dispose();
            }
        }

        /// <summary>
        /// Set the value of a solenoid
        /// </summary>
        /// <param name="on">Turn the solenoid output off or on.</param>
        public void Set(bool on)
        {
            int status = 0;
            SetSolenoid(m_solenoidPort, on, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Read the current value of the solenoid.
        /// </summary>
        /// <returns>The current value of the solenoid.</returns>
        public bool Get()
        {
            int status = 0;
            bool value = GetSolenoid(m_solenoidPort, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Check if solenoid is blacklisted.
        /// </summary>
        /// <remarks>If a solenoid is shorted, it is added to the blacklist and
        /// disabled until power cycle, or until faults are cleared.
        /// See <see cref="SolenoidBase.ClearAllPCMStickyFaults()"/>
        /// </remarks>
        /// <returns>If solenoid is disabled due to short.</returns>
        public bool IsBlackListed()
        {
            int value = GetPCMSolenoidBlackList() & (1 << m_channel);
            return (value != 0);
        }

        ///<inheritdoc/>
        public string SmartDashboardType => "Solenoid";

        ///<inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        ///<inheritdoc/>
        public ITable Table { get; private set; }

        ///<inheritdoc/>
        public void UpdateTable()
        {
            Table?.PutBoolean("Value", Get());
        }

        ///<inheritdoc/>
        public void StartLiveWindowMode()
        {
            Set(false);
            Table?.AddTableListener("Value", this, true);
        }

        ///<inheritdoc/>
        public void StopLiveWindowMode()
        {
            Set(false);
            Table?.RemoveTableListener(this);
        }

        ///<inheritdoc/>
        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
        {
            Set((bool)value);
        }
    }
}
