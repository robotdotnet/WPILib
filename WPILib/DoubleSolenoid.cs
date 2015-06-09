using HAL_Base;
using WPILib.Util;

namespace WPILib
{
    
    public class DoubleSolenoid : SolenoidBase
    {
        public enum Positions
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

        public Positions Value
        {
            set
            {
                byte rawValue = 0;

                switch (value)
                {
                    case Positions.Off:
                        rawValue = 0x00;
                        break;
                    case Positions.Forward:
                        rawValue = m_forwardMask;
                        break;
                    case Positions.Reverse:
                        rawValue = m_reverseMask;
                        break;
                }

                Set(rawValue, m_forwardMask | m_reverseMask);
            }
            get
            {
                byte value = GetAll();

                if ((value & m_forwardMask) != 0) return Positions.Forward;
                if ((value & m_reverseMask) != 0) return Positions.Reverse;
                return Positions.Off;
            }
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
    }
}
