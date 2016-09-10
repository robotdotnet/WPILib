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
    /// Class for running 2 channels of High Voltage Digital Output from the PCM.
    /// </summary>
    public class DoubleSolenoid : SolenoidBase, ILiveWindowSendable, ITableListener
    {
        /// <summary>
        /// Values allowed for <see cref="DoubleSolenoid">Double Solenoids</see>.
        /// </summary>
        public enum Value
        {
            /// <summary>
            /// Sets the solenoid to not be set.
            /// </summary>
            Off,
            /// <summary>
            /// Sets the solenoid to the forward position.
            /// </summary>
            Forward,
            /// <summary>
            /// Sets the solenoid to the reverse position.
            /// </summary>
            Reverse
        }

        private readonly int m_forwardChannel;
        private readonly int m_reverseChannel;

        private byte m_forwardMask;
        private byte m_reverseMask;

        private int m_forwardHandle;
        private int m_reverseHandle;

        private readonly object m_lockObject = new object();

        /// <summary>
        /// Creates a new <see cref="DoubleSolenoid"/> using the default PCM Id of 0.
        /// </summary>
        /// <param name="forwardChannel">The forward channel number on the PCM [0..7]</param>
        /// <param name="reverseChannel">The reverse channel number on the PCM [0..7]</param>
        public DoubleSolenoid(int forwardChannel, int reverseChannel)
            : this(DefaultSolenoidModule, forwardChannel, reverseChannel)
        {
        }

        /// <summary>
        /// Creates a new <see cref="DoubleSolenoid"/> with the specified module number.
        /// </summary>
        /// <param name="moduleNumber">The module number of the PCM to use.</param>
        /// <param name="forwardChannel">The forward channel number on the PCM [0..7]</param>
        /// <param name="reverseChannel">The reverse channel number on the PCM [0..7]</param>
        public DoubleSolenoid(int moduleNumber, int forwardChannel, int reverseChannel)
            : base(moduleNumber)
        {
            m_forwardChannel = forwardChannel;
            m_reverseChannel = reverseChannel;
            
            CheckSolenoidModule(moduleNumber);
            CheckSolenoidChannel(m_forwardChannel);
            CheckSolenoidChannel(m_reverseChannel);

            int status = 0;
            m_forwardHandle = HAL_InitializeSolenoidPort(HAL_GetPortWithModule(moduleNumber, forwardChannel),
                ref status);
            CheckStatusRange(status, 0, HAL_GetNumSolenoidPins(), forwardChannel);

            m_reverseHandle = HAL_InitializeSolenoidPort(HAL_GetPortWithModule(moduleNumber, reverseChannel), ref status);

            if (status != 0)
            {
                HAL_FreeSolenoidPort(m_forwardHandle);
                m_forwardHandle = 0;
                m_reverseChannel = 0;

                CheckStatusRange(status, 0, HAL_GetNumSolenoidPins(), reverseChannel);
            }

            m_forwardMask = (byte)(1 << m_forwardChannel);
            m_reverseMask = (byte)(1 << m_reverseChannel);

            HAL.Base.HAL.Report(ResourceType.kResourceType_Solenoid, (byte)m_forwardChannel, (byte)(ModuleNumber));
            HAL.Base.HAL.Report(ResourceType.kResourceType_Solenoid, (byte)m_reverseChannel, (byte)(ModuleNumber));
            LiveWindow.LiveWindow.AddActuator("DoubleSolenoid", ModuleNumber, m_forwardChannel, this);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            HAL_FreeSolenoidPort(m_forwardHandle);
            HAL_FreeSolenoidPort(m_reverseHandle);
            m_forwardHandle = 0;
            m_reverseHandle = 0;
            base.Dispose();
        }
        /// <summary>
        /// Sets the value of the solenoid.
        /// </summary>
        /// <param name="value">The value to set (Off, Forward, Reverse).</param>
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
                case Value.Off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
            int status = 0;
            HAL_SetSolenoid(m_forwardHandle, forward, ref status);
            CheckStatus(status);

            HAL_SetSolenoid(m_reverseHandle, reverse, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Gets the current value of the solenoid.
        /// </summary>
        /// <returns></returns>
        public Value Get()
        {
            int status = 0;
            bool valueForward = HAL_GetSolenoid(m_forwardHandle, ref status);
            CheckStatus(status);
            bool valueReverse = HAL_GetSolenoid(m_reverseHandle, ref status);
            CheckStatus(status);

            if (valueForward)
                return Value.Forward;
            if (valueReverse)
                return Value.Reverse;
            return Value.Off;
        }

        /// <summary>
        /// Gets if the forward solenoid is black listed.
        /// </summary>
        /// <remarks>If the solenoid is shorted, it is added to the blacklist and disabled
        /// until power cycle, or until faults are cleared.</remarks>
        /// <seealso cref="SolenoidBase.ClearAllPCMStickyFaults()"/>
        public bool FwdSolenoidBlackListed
        {
            get
            {
                int blackList = GetPCMSolenoidBlackList();
                return ((blackList & (1 << m_forwardChannel)) != 0);
            }
        }

        /// <summary>
        /// Gets if the reverse solenoid is black listed.
        /// </summary>
        /// <remarks>If the solenoid is shorted, it is added to the blacklist and disabled
        /// until power cycle, or until faults are cleared.</remarks>
        /// <seealso cref="SolenoidBase.ClearAllPCMStickyFaults()"/>
        public bool RevSolenoidBlackListed
        {
            get
            {
                int blackList = GetPCMSolenoidBlackList();
                return ((blackList & (1 << m_reverseChannel)) != 0);
            }
        }

        /// <inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        /// <inheritdoc/>
        public ITable Table { get; private set; }

        /// <inheritdoc/>
        public string SmartDashboardType => "Double Solenoid";

        /// <inheritdoc/>
        public void UpdateTable()
        {
            Table?.PutString("Value", (Get() == Value.Forward ? "Forward" : (Get() == Value.Reverse ? "Reverse" : "Off")));
        }

        /// <inheritdoc/>
        public void StartLiveWindowMode()
        {
            Set(Value.Off);
            Table?.AddTableListener("Value", this, true);
        }

        /// <inheritdoc/>
        public void StopLiveWindowMode()
        {
            Table?.RemoveTableListener(this);
        }

        /// <inheritdoc/>
        public void ValueChanged(ITable source, string key, NetworkTables.Value value, NotifyFlags flags)
        {
            string valString = value.GetString();
            if (valString.Equals("Reverse"))
                Set(Value.Reverse);
            else if (valString.Equals("Forward"))
                Set(Value.Forward);
            else
                Set(Value.Off);
        }
    }
}
