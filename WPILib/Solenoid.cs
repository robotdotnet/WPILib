using System;
using HAL_Base;
using NetworkTablesDotNet.Tables;
using WPILib.LiveWindows;
using static HAL_Base.HAL;
using static HAL_Base.HALSolenoid;

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
        private int m_channel;//The channel to control.
        private IntPtr m_solenoidPort;
        private object m_lockObject = new object();

        /// <summary>
        /// Common function to implement constructor behavior.
        /// </summary>
        private void InitSolenoid()
        {
            lock (m_lockObject)
            {
                CheckSolenoidModule(m_moduleNumber);
                CheckSolenoidChannel(m_channel);

                int status = 0;

                IntPtr port = GetPortWithModule((byte)m_moduleNumber, (byte)m_channel);
                m_solenoidPort = InitializeSolenoidPort(port, ref status);

                LiveWindow.AddActuator("Solenoid", m_moduleNumber, m_channel, this);
                Report(ResourceType.kResourceType_Solenoid, (byte)m_channel, (byte)m_moduleNumber);
            }
        }

        /// <summary>
        /// Constructor using the default PCM ID (0)
        /// </summary>
        /// <param name="channel">The channel on the PCM to control (0..7).</param>
        public Solenoid(int channel)
            : base(DefaultSolenoidModule)
        {
            m_channel = channel;
            InitSolenoid();
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

        /// <summary>
        /// Destructor
        /// </summary>
        public override void Dispose()
        {
        }

        /// <summary>
        /// Set the value of a solenoid
        /// </summary>
        /// <param name="on">Turn the solenoid output off or on.</param>
        public void Set(bool on)
        {
            byte value = (byte)(on ? 0xFF : 0x00);
            byte mask = (byte)(1 << m_channel);

            Set(value, mask);
        }

        /// <summary>
        /// Read the current value of the solenoid.
        /// </summary>
        /// <returns>The current value of the solenoid.</returns>
        public bool Get()
        {
            int value = GetAll() & (1 << m_channel);
            return (value != 0);
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

        /// <summary>
        /// Live Window code, only does anything if live window is activated.
        /// </summary>
        public string SmartDashboardType => "Solenoid";

        /// <summary>
        /// Initialize a table for this sendable object.
        /// </summary>
        /// <param name="subtable">The table to put the values in.</param>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        /// <summary>
        /// Returns the table that is currently associated with the sendable
        /// </summary>
        public ITable Table { get; private set; }

        /// <summary>
        /// Update the table for this sendable object with the latest
        /// values.
        /// </summary>
        public void UpdateTable()
        {
            Table?.PutBoolean("Value", Get());
        }

        /// <summary>
        /// Start having this sendable object automatically respond to
        /// value changes reflect the value on the table.
        /// </summary>
        public void StartLiveWindowMode()
        {
            Set(false);
            Table?.AddTableListener("Value", this, true);
        }

        /// <summary>
        /// Stop having this sendable object automatically respond to value changes.
        /// </summary>
        public void StopLiveWindowMode()
        {
            Set(false);
            Table?.RemoveTableListener(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isNew"></param>
        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            Set((bool)value);
        }
    }
}
