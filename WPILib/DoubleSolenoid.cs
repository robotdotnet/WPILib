using System;
using HAL_Base;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.LiveWindows;
using static HAL_Base.HAL;
using static HAL_Base.HALSolenoid;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Class for running 2 channels of High Voltage Digital Output from the PCM.
    /// </summary>
    public class DoubleSolenoid : SolenoidBase, ILiveWindowSendable, ITableListener
    {
        /// <summary>
        /// Values allowed for <see cref="DoubleSolenoid">Double Solenoids</see>.
        /// </summary>
        public enum Value
        {
            Off,
            Forward,
            Reverse
        }

        private readonly int m_forwardChannel;
        private readonly int m_reverseChannel;

        private IntPtr m_forwardSolenoid;
        private IntPtr m_reverseSolenoid;

        private readonly object m_lockObject = new object();

        private void InitSolenoid()
        {
            lock (m_lockObject)
            {
                CheckSolenoidModule(m_moduleNumber);
                CheckSolenoidChannel(m_forwardChannel);
                CheckSolenoidChannel(m_reverseChannel);

                s_allocated.Allocate(m_moduleNumber*SolenoidChannels + m_forwardChannel,
                    "Solenoid channel " + m_forwardChannel + " on module " + m_moduleNumber + " is already allocated");
                s_allocated.Allocate(m_moduleNumber*SolenoidChannels + m_reverseChannel,
                    "Solenoid channel " + m_reverseChannel + " on module " + m_moduleNumber + " is already allocated");

                int status = 0;
                IntPtr port = GetPortWithModule((byte)m_moduleNumber, (byte)m_forwardChannel);
                m_forwardSolenoid = InitializeSolenoidPort(port, ref status);
                CheckStatus(status);

                port = GetPortWithModule((byte)m_moduleNumber, (byte)m_reverseChannel);
                m_reverseSolenoid = InitializeSolenoidPort(port, ref status);
                CheckStatus(status);

                HAL.Report(ResourceType.kResourceType_Solenoid, (byte)m_forwardChannel, (byte)(m_moduleNumber));
                HAL.Report(ResourceType.kResourceType_Solenoid, (byte)m_reverseChannel, (byte)(m_moduleNumber));
                LiveWindow.AddActuator("DoubleSolenoid", m_moduleNumber, m_forwardChannel, this);
            }
        }

        public DoubleSolenoid(int forwardChannel, int reverseChannel)
            : this(DefaultSolenoidModule, forwardChannel, reverseChannel)
        {
        }

        public DoubleSolenoid(int moduleNumber, int forwardChannel, int reverseChannel)
            : base(moduleNumber)
        {
            m_forwardChannel = forwardChannel;
            m_reverseChannel = reverseChannel;
            InitSolenoid();
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            lock (m_lockObject)
            {
                s_allocated.Deallocate(m_moduleNumber * SolenoidChannels + m_forwardChannel);
                s_allocated.Deallocate(m_moduleNumber * SolenoidChannels + m_reverseChannel);
            }
        }

        public void Set(Value value)
        {
            bool forward = false;
            bool reverse = false;
            switch (value)
            {
                case Value.Forward:
                    forward = true;
                    break;
                case Value.Reverse:
                    reverse = true;
                    break;
            }
            int status = 0;
            SetSolenoid(m_forwardSolenoid, forward, ref status);
            CheckStatus(status);

            SetSolenoid(m_reverseSolenoid, reverse, ref status);
            CheckStatus(status);
        }

        public Value Get()
        {
            int status = 0;
            bool valueForward = GetSolenoid(m_forwardSolenoid, ref status);
            CheckStatus(status);
            bool valueReverse = GetSolenoid(m_reverseSolenoid, ref status);
            CheckStatus(status);

            if (valueForward)
                return Value.Forward;
            if (valueReverse)
                return Value.Reverse;
            return Value.Off;
        }

        public bool FwdSolenoidBlackListed
        {
            get
            {
                int blackList = GetPCMSolenoidBlackList();
                return ((blackList & (1 << m_forwardChannel)) != 0);
            }
        }

        public bool RevSolenoidBlackListed
        {
            get
            {
                int blackList = GetPCMSolenoidBlackList();
                return ((blackList & (1 << m_reverseChannel)) != 0);
            }
        }

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
        /// Returns the string representation of the named data type that will be used by the smart dashboard for this sendable
        /// </summary>
        public string SmartDashboardType => "Double Solenoid";

        /// <summary>
        /// Update the table for this sendable object with the latest
        /// values.
        /// </summary>
        public void UpdateTable()
        {
            Table?.PutString("Value", (Get() == Value.Forward ? "Forward" : (Get() == Value.Reverse ? "Reverse" : "Off")));
        }

        /// <summary>
        /// Start having this sendable object automatically respond to
        /// value changes reflect the value on the table.
        /// </summary>
        public void StartLiveWindowMode()
        {
            Set(Value.Off);
            Table.AddTableListener("Value", this, true);
        }

        /// <summary>
        /// Stop having this sendable object automatically respond to value changes.
        /// </summary>
        public void StopLiveWindowMode()
        {
            Table.RemoveTableListener(this);
        }

        /// <summary>
        /// Not called externally. Just needed because its an interface.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isNew"></param>
        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            if (value.ToString().Equals("Reverse"))
                Set(Value.Reverse);
            else if (value.ToString().Equals("Forward"))
                Set(Value.Forward);
            else
                Set(Value.Off);
        }
    }
}
