using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HAL_Base;

namespace WPILib
{
    public class Solenoid : SolenoidBase
    {
        private int m_channel;
        private IntPtr m_solenoidPort;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void InitSolenoid()
        {
            CheckSolenoidModule(m_moduleNumber);
            CheckSolenoidChannel(m_channel);

            int status = 0;

            IntPtr port = HAL.GetPortWithModule((byte) m_moduleNumber, (byte) m_channel);
            m_solenoidPort = HALSolenoid.InitializeSolenoidPort(port, ref status);

            HAL.Report(ResourceType.kResourceType_Solenoid, (byte) m_channel, (byte) m_moduleNumber);
        }

        public Solenoid(int channel) : base(GetDefaultSolenoidModule())
        {
            m_channel = channel;
            InitSolenoid();
        }

        public Solenoid(int moduleNumber, int channel) : base(moduleNumber)
        {
            m_channel = channel;
            InitSolenoid();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Free()
        {
        }

        public void Set(bool on)
        {
            byte value = (byte)(on ? 0xFF : 0x00);
            byte mask = (byte)(1 << m_channel);

            Set(value, mask);
        }

        public bool Get()
        {
            int value = GetAll() & (1 << m_channel);
            return (value != 0);
        }

        public bool IsBlackListed()
        {
            int value = GetPCMSolenoidBlackList() & (1 << m_channel);
            return (value != 0);
        }
    }
}
