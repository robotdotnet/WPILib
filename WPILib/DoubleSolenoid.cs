using HAL_Base;
using NetworkTablesDotNet.Tables;
using WPILib.Exceptions;
using WPILib.LiveWindows;

namespace WPILib
{

    public class DoubleSolenoid : SolenoidBase, ILiveWindowSendable, ITableListener
    {
        public enum Value
        {
            Off,
            Forward,
            Reverse
        }

        private int m_forwardChannel;
        private int m_reverseChannel;
        private byte m_forwardMask;
        private byte m_reverseMask;
        private object m_lockObject = new object();

        private void InitSolenoid()
        {
            lock (m_lockObject)
            {
                CheckSolenoidModule(m_moduleNumber);
                CheckSolenoidChannel(m_forwardChannel);
                CheckSolenoidChannel(m_reverseChannel);

                try
                {
                    m_allocated.Allocate(m_moduleNumber * SolenoidChannels + m_forwardChannel);
                }
                catch (CheckedAllocationException)
                {
                    throw new AllocationException("Solenoid channel " + m_forwardChannel + " on module " + m_moduleNumber + " is already allocated");
                }
                try
                {
                    m_allocated.Allocate(m_moduleNumber * SolenoidChannels + m_reverseChannel);
                }
                catch (CheckedAllocationException)
                {
                    throw new AllocationException("Solenoid channel " + m_reverseChannel + " on module " + m_moduleNumber + " is already allocated");
                }

                m_forwardMask = (byte)(1 << m_forwardChannel);
                m_reverseMask = (byte)(1 << m_reverseChannel);

                HAL.Report(ResourceType.kResourceType_Solenoid, (byte)m_forwardChannel, (byte)(m_moduleNumber));
                HAL.Report(ResourceType.kResourceType_Solenoid, (byte)m_reverseChannel, (byte)(m_moduleNumber));
            }
        }

        public DoubleSolenoid(int forwardChannel, int reverseChannel)
            : base(DefaultSolenoidModule)
        {
            m_forwardChannel = forwardChannel;
            m_reverseChannel = reverseChannel;
            InitSolenoid();
        }

        public DoubleSolenoid(int moduleNumber, int forwardChannel, int reverseChannel)
            : base(moduleNumber)
        {
            m_forwardChannel = forwardChannel;
            m_reverseChannel = reverseChannel;
            InitSolenoid();
        }

        public override void Dispose()
        {
            lock (m_lockObject)
            {
                m_allocated.Dispose(m_moduleNumber * SolenoidChannels + m_forwardChannel);
                m_allocated.Dispose(m_moduleNumber * SolenoidChannels + m_reverseChannel);
            }
        }

        public void Set(Value value)
        {
            byte rawValue = 0;

            switch (value)
            {
                case Value.Off:
                    rawValue = 0x00;
                    break;
                case Value.Forward:
                    rawValue = m_forwardMask;
                    break;
                case Value.Reverse:
                    rawValue = m_reverseMask;
                    break;
            }

            Set(rawValue, m_forwardMask | m_reverseMask);
        }

        public Value Get()
        {
            byte value = GetAll();

            if ((value & m_forwardMask) != 0) return Value.Forward;
            if ((value & m_reverseMask) != 0) return Value.Reverse;
            return Value.Off;
        }

        public bool FwdSolenoidBlackListed
        {
            get
            {
                int blackList = GetPCMSolenoidBlackList();
                return ((blackList & m_forwardMask) != 0);
            }
        }

        public bool RevSolenoidBlackListed
        {
            get
            {
                int blackList = GetPCMSolenoidBlackList();
                return ((blackList & m_reverseMask) != 0);
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
