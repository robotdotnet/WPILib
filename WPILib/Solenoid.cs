using System;
using HAL.Base;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.LiveWindow;
using static HAL.Base.HAL;
using static HAL.Base.HALSolenoid;
using static HAL.Base.HALPorts;
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
        private int m_solenoidHandle = 0;
        private readonly object m_lockObject = new object();

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

            CheckSolenoidChannel(channel);
            CheckSolenoidModule(moduleNumber);

            int status = 0;
            m_solenoidHandle = HAL_InitializeSolenoidPort(HAL_GetPortWithModule(moduleNumber, channel), ref status);

            CheckStatusRange(status, 0, HAL_GetNumSolenoidPins(), channel);

            LiveWindow.LiveWindow.AddActuator("Solenoid", moduleNumber, channel, this);
            Report(ResourceType.kResourceType_Solenoid, channel, moduleNumber);
        }

        ///<inheritdoc/>
        public override void Dispose()
        {
            HAL_FreeSolenoidPort(m_solenoidHandle);
            m_solenoidHandle = 0;
            Table?.RemoveTableListener(this);
            base.Dispose();
        }

        /// <summary>
        /// Set the value of a solenoid
        /// </summary>
        /// <param name="on">Turn the solenoid output off or on.</param>
        public virtual void Set(bool on)
        {
            int status = 0;
            HAL_SetSolenoid(m_solenoidHandle, on, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Read the current value of the solenoid.
        /// </summary>
        /// <returns>The current value of the solenoid.</returns>
        public virtual bool Get()
        {
            int status = 0;
            bool value = HAL_GetSolenoid(m_solenoidHandle, ref status);
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
