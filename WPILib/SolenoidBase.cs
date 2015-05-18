using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HAL_Base;

namespace WPILib
{
    public class SolenoidBase : SensorBase
    {
        private IntPtr[] m_ports;
        protected int m_moduleNumber;
        protected Resource m_allocated = new Resource(63*SolenoidChannels);

        public SolenoidBase(int moduleNumber)
        {
            m_moduleNumber = moduleNumber;
            m_ports = new IntPtr[SolenoidChannels];
            for (int i = 0; i < SolenoidChannels; i++)
            {
                IntPtr port = HAL.GetPortWithModule((byte) moduleNumber, (byte) i);
                int status = 0;
                m_ports[i] = HALSolenoid.InitializeSolenoidPort(port, ref status);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void Set(int value, int mask)
        {
            int status = 0;
            for (int i = 0; i < SolenoidChannels; i++)
            {
                int localMask = 1 << i;
                if ((mask & localMask) != 0)
                {
                    HALSolenoid.SetSolenoid(m_ports[i], ((value & localMask) != 0), ref status);
                }
            }
        }

        public byte GetAll()
        {
            byte value = 0;
            int status = 0;
            for (int i = 0; i < SolenoidChannels; i++)
            {
                value |= (byte)((HALSolenoid.GetSolenoid(m_ports[i], ref status) ? 1 : 0) << i);
            }
            return value;
        }

        public byte GetPCMSolenoidBlackList()
        {
            int status = 0;
            byte retVal = (byte)HALSolenoid.GetPCMSolenoidBlackList(m_ports[0], ref status);
            return retVal;
        }

        public bool GetPCMSolenoidVoltageStickyFault()
        {
            int status = 0;
            bool retVal = HALSolenoid.GetPCMSolenoidVoltageStickyFault(m_ports[0], ref status);
            return retVal;
        }

        public bool GetPCMSolenoidVoltageFault()
        {
            int status = 0;
            bool retVal = HALSolenoid.GetPCMSolenoidVoltageFault(m_ports[0], ref status);
            return retVal;
        }

        public void ClearAllPCMStickyFaults()
        {
            int status = 0;
            HALSolenoid.ClearAllPCMStickyFaults_sol(m_ports[0], ref status);
        }
    }
}
