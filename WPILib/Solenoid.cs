using System;
using System.Collections.Generic;

using HAL_Base;
using NetworkTablesDotNet.Tables;
using WPILib.livewindow;
using static HAL_Base.HAL;
using static HAL_Base.HALSolenoid;

namespace WPILib
{
    /// <summary>
    /// Solenoid class for running high voltage digital output.
    /// <para> </para>
    /// The Solenoid class is typically used for pneumatics solenoids, but could be used
    /// <para /> for any device within the current spec of the PCM. 
    /// </summary>
    public class Solenoid : SolenoidBase, LiveWindowSendable, ITableListener
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

                //TODO: Live Window Actuator
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
        /// <para> </para>
        /// <para />If a solenoid is shorted, it is added to the blacklist and
        /// <para />disabled until power cycle, or until faults are cleared.
        /// <para />See <see cref="SolenoidBase.ClearAllPCMStickyFaults()"/>
        /// </summary>
        /// <returns>IF solenoid is disabled due to short.</returns>
        public bool IsBlackListed()
        {
            int value = GetPCMSolenoidBlackList() & (1 << m_channel);
            return (value != 0);
        }

        /// <summary>
        /// Live Window code, only does anything if live window is activated.
        /// </summary>
        public string SmartDashboardType
        {
            get { return "Solenoid"; }
        }

        private ITable m_table;

        public void InitTable(ITable subtable)
        {
            m_table = subtable;
            UpdateTable();
        }

        public ITable Table
        {
            get { return m_table; }
        }

        public void UpdateTable()
        {
            m_table?.PutBoolean("Value", Get());
        }

        public void StartLiveWindowMode()
        {
            Set(false);
            m_table?.AddTableListener("Value", this, true);
        }

        public void StopLiveWindowMode()
        {
            Set(false);
            m_table.RemoveTableListener(this);
        }


        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            Set((bool)value);
        }
    }
}
