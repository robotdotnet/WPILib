using System;
using HAL_Base;
using static WPILib.Utility;
using static HAL_Base.HAL;
using static HAL_Base.HALSolenoid;

namespace WPILib
{
    /// <summary>
    /// SolenoidBase class is the common base class for the Solenoid and DoubleSolenoid classes
    /// </summary>
    public class SolenoidBase : SensorBase
    {
        private IntPtr[] m_ports;
        protected int m_moduleNumber;//The number of the solenoid module being used
        protected Resource m_allocated = new Resource(63 * SolenoidChannels);
        private object m_lockObject = new object();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="moduleNumber">The PCM CAN ID</param>
        public SolenoidBase(int moduleNumber)
        {
            lock (m_lockObject)
            {
                m_moduleNumber = moduleNumber;
                m_ports = new IntPtr[SolenoidChannels];
                for (int i = 0; i < SolenoidChannels; i++)
                {
                    IntPtr port = GetPortWithModule((byte)moduleNumber, (byte)i);
                    int status = 0;
                    m_ports[i] = InitializeSolenoidPort(port, ref status);
                    CheckStatus(status);
                }
            }
        }

        /// <summary>
        /// Set the value of a solenoid.
        /// </summary>
        /// <param name="value">The value you want to set on the module</param>
        /// <param name="mask">The channels you want to be affected</param>
        protected void Set(int value, int mask)
        {
            lock (m_lockObject)
            {
                int status = 0;
                for (int i = 0; i < SolenoidChannels; i++)
                {
                    int localMask = 1 << i;
                    if ((mask & localMask) != 0)
                    {
                        SetSolenoid(m_ports[i], ((value & localMask) != 0), ref status);
                    }
                }
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Read all 8 solenoids from the module used by this solenoid as a single byte
        /// </summary>
        /// <returns>The current value of all 8 solenoids on this module.</returns>
        public byte GetAll()
        {
            byte value = 0;
            int status = 0;
            for (int i = 0; i < SolenoidChannels; i++)
            {
                value |= (byte)((GetSolenoid(m_ports[i], ref status) ? 1 : 0) << i);
            }
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Reads complete solenoid blacklist for all 8 solenoids as a single byte.
        /// <para> </para>
        /// <para />If a solenoid is shorted, it is added to the blacklist and
        /// <para />disabled until power cycle, or until faults are cleared.
        /// <para />See <see cref="ClearAllPCMStickyFaults()"/>
        /// </summary>
        /// <returns></returns>
        public byte GetPCMSolenoidBlackList()
        {
            int status = 0;
            byte retVal = (byte)HALSolenoid.GetPCMSolenoidBlackList(m_ports[0], ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Returns if a PCM sticky fault is set.
        /// </summary>
        /// <returns>True if PCM sticky fault is set : The common highside solenoid voltage rail is too low, most likely a solenoid channel is shorted.</returns>
        public bool GetPCMSolenoidVoltageStickyFault()
        {
            int status = 0;
            bool retVal = HALSolenoid.GetPCMSolenoidVoltageStickyFault(m_ports[0], ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Returns if PCM is in a fault state
        /// </summary>
        /// <returns>true if PCM is in fault state : The common highside solenoid voltage rail is too low, most likely a solenoid channel is shorted.</returns>
        public bool GetPCMSolenoidVoltageFault()
        {
            int status = 0;
            bool retVal = HALSolenoid.GetPCMSolenoidVoltageFault(m_ports[0], ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Clear all sticky faults inside PCM that Compressor is wired to.
        /// <para> </para>
        /// If a sticky fault is set, then it will be persistently cleared.  Compressor drive
        /// <para />		maybe momentarily disable while flags are being cleared. Care should be 
        /// <para />		taken to not call this too frequently, otherwise normal compressor 
        /// <para />		functionality may be prevented.
        /// <para> </para>
        /// If no sticky faults are set then this call will have no effect.
        /// </summary>
        public void ClearAllPCMStickyFaults()
        {
            int status = 0;
            ClearAllPCMStickyFaults_sol(m_ports[0], ref status);
            CheckStatus(status);
        }
    }
}
