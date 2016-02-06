using System;
using HAL.Base;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.LiveWindow;
using static HAL.Base.HAL;
using static HAL.Base.HALSolenoid;
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

        private SolenoidPortSafeHandle m_forwardSolenoid;
        private SolenoidPortSafeHandle m_reverseSolenoid;

        private readonly object m_lockObject = new object();

        private void InitSolenoid()
        {
            lock (m_lockObject)
            {
                CheckSolenoidModule(ModuleNumber);
                CheckSolenoidChannel(m_forwardChannel);
                CheckSolenoidChannel(m_reverseChannel);

                Allocated.Allocate(ModuleNumber*SolenoidChannels + m_forwardChannel,
                    "Solenoid channel " + m_forwardChannel + " on module " + ModuleNumber + " is already allocated");
                Allocated.Allocate(ModuleNumber*SolenoidChannels + m_reverseChannel,
                    "Solenoid channel " + m_reverseChannel + " on module " + ModuleNumber + " is already allocated");

                int status = 0;
                HALPortSafeHandle port = GetPortWithModule((byte)ModuleNumber, (byte)m_forwardChannel);
                m_forwardSolenoid = InitializeSolenoidPort(port, ref status);
                CheckStatus(status);

                port = GetPortWithModule((byte)ModuleNumber, (byte)m_reverseChannel);
                m_reverseSolenoid = InitializeSolenoidPort(port, ref status);
                CheckStatus(status);

                HAL.Base.HAL.Report(ResourceType.kResourceType_Solenoid, (byte)m_forwardChannel, (byte)(ModuleNumber));
                HAL.Base.HAL.Report(ResourceType.kResourceType_Solenoid, (byte)m_reverseChannel, (byte)(ModuleNumber));
                LiveWindow.LiveWindow.AddActuator("DoubleSolenoid", ModuleNumber, m_forwardChannel, this);
            }
        }

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
            InitSolenoid();
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            lock (m_lockObject)
            {
                Allocated.Deallocate(ModuleNumber * SolenoidChannels + m_forwardChannel);
                Allocated.Deallocate(ModuleNumber * SolenoidChannels + m_reverseChannel);
                m_forwardSolenoid.Dispose();
                m_forwardSolenoid = null;
                m_reverseSolenoid.Dispose();
                m_reverseSolenoid = null;
                //(m_forwardSolenoid);
                //m_forwardSolenoid = IntPtr.Zero;
                //FreeSolenoidPort(m_reverseSolenoid);
                //m_reverseSolenoid = IntPtr.Zero;
                base.Dispose();
            }
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
            }
            int status = 0;
            SetSolenoid(m_forwardSolenoid, forward, ref status);
            CheckStatus(status);

            SetSolenoid(m_reverseSolenoid, reverse, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Gets the current value of the solenoid.
        /// </summary>
        /// <returns></returns>
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
        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
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
