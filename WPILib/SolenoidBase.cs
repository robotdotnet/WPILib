using System;
using static WPILib.Utility;
using static HAL.Base.HAL;
using static HAL.Base.HALSolenoid;
using HALSolenoid = HAL.Base.HALSolenoid;
using HAL.Base;

namespace WPILib
{
    /// <summary>
    /// The SolenoidBase class is the common base class for the <see cref="Solenoid"/>
    /// and <see cref="DoubleSolenoid"/> classes.
    /// </summary>
    public abstract class SolenoidBase : SensorBase
    {
        private SolenoidPortSafeHandle m_port;
        /// <summary>
        /// The module number for the solenoid.
        /// </summary>
        protected readonly int ModuleNumber;
        /// <summary>
        /// The allocated resources for the solenoid.
        /// </summary>
        protected static readonly Resource Allocated = new Resource(63 * SolenoidChannels);
        private readonly object m_lockObject = new object();

        /// <summary>
        /// Creates a new <see cref="SolenoidBase"/>.
        /// </summary>
        /// <param name="moduleNumber">The PCM CAN ID</param>
        protected SolenoidBase(int moduleNumber)
        {
            lock (m_lockObject)
            {
                ModuleNumber = moduleNumber;
                int status = 0;
                m_port = InitializeSolenoidPort(GetPortWithModule((byte) ModuleNumber, 0), ref status);
                CheckStatus(status);
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            m_port.Dispose();
            m_port = null;
            //FreeSolenoidPort(m_port);
            //m_port = IntPtr.Zero;
            base.Dispose();
        }

        /// <summary>
        /// Read all 8 solenoids from the module used byt this solenoid as a single byte
        /// </summary>
        /// <returns>The current value of all 8 solenoids on thsis module.</returns>
        public byte GetAll()
        {
            int status = 0;
            byte retVal = HALSolenoid.GetAllSolenoids(m_port, ref status);
            CheckStatus(status);
            return retVal;
        }
        
        /// <summary>
        /// Reads complete solenoid blacklist for all 8 solenoids as a single byte.
        /// </summary>
        /// <remarks>If a solenoid is shorted, it is added to the blacklist and
        /// disabled until power cycle, or until faults are cleared.
        /// </remarks>
        /// <seealso cref="ClearAllPCMStickyFaults()"/>
        /// <returns>The solenoid blacklist of all 8 solenoids on the module.</returns>
        public byte GetPCMSolenoidBlackList()
        {
            int status = 0;
            byte retVal = (byte)HALSolenoid.GetPCMSolenoidBlackList(m_port, ref status);
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
            bool retVal = HALSolenoid.GetPCMSolenoidVoltageStickyFault(m_port, ref status);
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
            bool retVal = HALSolenoid.GetPCMSolenoidVoltageFault(m_port, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Clear all sticky faults inside PCM that Compressor is wired to. </summary>
        /// <remarks>
        /// If a sticky fault is set, then it will be persistently cleared.  Compressor drive
        /// maybe momentarily disable while flags are being cleared. Care should be 
        /// taken to not call this too frequently, otherwise normal compressor 
        /// functionality may be prevented.
        /// <para> </para>
        /// If no sticky faults are set then this call will have no effect.
        /// </remarks>
        public void ClearAllPCMStickyFaults()
        {
            int status = 0;
            ClearAllPCMStickyFaults_sol(m_port, ref status);
            CheckStatus(status);
        }
    }
}
