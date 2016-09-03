using System;
using NetworkTables.Tables;
using WPILib.LiveWindow;
using static HAL.Base.HALCompressor;
using static WPILib.Utility;
using static HAL.Base.HALSolenoid;
using static HAL.Base.HALPorts;

namespace WPILib
{
    /// <summary>
    /// Class for operating the PCM Compressor. The PWM will automatically run in close-loop mode
    /// by default whenever a <see cref="Solenoid"/> object is created.
    /// </summary>
    /// <remarks>For most cases the <see cref="Compressor"/> object does not
    /// need to be instantiated or used in a robot program.
    /// <para/>This class is only This class is only required in cases where more detailed 
    /// status or to enable/disable closed loop control. Note: you cannot operate the compressor 
    /// directly from this class as doing so would circumvent the safety provided in using the 
    /// pressure switch and closed loop control. You can only turn off closed loop control, 
    /// thereby stopping the compressor from operating.</remarks>
    public class Compressor : SensorBase, ILiveWindowSendable
    {
        private int m_compressorHandle;
        private int m_module;

        /// <summary>
        /// Create an instance of the <see cref="Compressor"/> class
        /// </summary>
        /// <param name="pcmId">The PCM CAN device ID.</param>
        public Compressor(int pcmId)
        {
            InitCompressor(pcmId);
        }

        /// <summary>
        /// Create an instance of the <see cref="Compressor"/> class using the default module.
        /// </summary>
        public Compressor()
        {
            InitCompressor(DefaultSolenoidModule);
        }

        private void InitCompressor(int module)
        {
            m_module = module;
            int status = 0;
            m_compressorHandle = HAL_InitializeCompressor(module, ref status);
            if (status != 0)
            {
                CheckStatusRange(status, 0, HAL_GetNumPCMModules(), module);
                return;
            }
            ClosedLoopControl = true;
        }

        /// <summary>
        /// Start the compressor running in closed loop control mode.
        /// </summary>
        /// <remarks>Use this method in cases where you would like to manually
        /// stop and start the compressor for applications such as conserving
        /// battery or making sure that the compressor motor doesn't start
        /// during critical operations.</remarks>
        public void Start()
        {
            ClosedLoopControl = true;
        }

        /// <summary>
        /// Stop the compressor from running in closed loop control mode.
        /// </summary>
        /// <remarks>Use this method in cases where you would like to manually
        /// stop and start the compressor for applications such as conserving
        /// battery or making sure that the compressor motor doesn't start
        /// during critical operations.</remarks>
        public void Stop()
        {
            ClosedLoopControl = false;
        }

        /// <summary>
        /// Gets whether the compressor is enabled.
        /// </summary>
        public bool Enabled()
        {
            int status = 0;
            bool on = HAL_GetCompressor(m_compressorHandle, ref status);
            CheckStatus(status);
            return on;
        }

        /// <summary>
        /// Gets the value of the pressure switch.
        /// </summary>
        public bool GetPressureSwitchValue()
        {
            int status = 0;
            bool on = HAL_GetCompressorPressureSwitch(m_compressorHandle, ref status);
            CheckStatus(status);
            return on;
        }

        /// <summary>
        /// Gets the Current being drawed by the compressor.
        /// </summary>
        /// <returns></returns>
        public double GetCompressorCurrent()
        {
            int status = 0;
            double current = HAL_GetCompressorCurrent(m_compressorHandle, ref status);
            CheckStatus(status);
            return current;
        }

        /// <summary>
        /// Gets or sets whether closed loop control is enabled on the compressor.
        /// </summary>
        public bool ClosedLoopControl
        {
            set
            {
                int status = 0;
                HAL_SetCompressorClosedLoopControl(m_compressorHandle, value, ref status);
                CheckStatus(status);
            }
            get
            {
                int status = 0;
                bool on = HAL_GetCompressorClosedLoopControl(m_compressorHandle, ref status);
                CheckStatus(status);
                return on;
            }
        }

        /// <summary>
        /// Gets if PCM has a sticky fault for the compressor current draw being too high.
        /// </summary>
        /// <returns>True if sticky fault is set for the current draw being too high.</returns>
        public bool GetCompressorCurrentTooHighStickyFault()
        {
            int status = 0;
            bool retVal = HAL_GetCompressorCurrentTooHighStickyFault(m_compressorHandle, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets if the compressor is disabled due to the current draw being too high.
        /// </summary>
        /// <returns>True if the compressor is disabled due to current being too high.</returns>
        public bool GetCompressorCurrentTooHighFault()
        {
            int status = 0;
            bool retVal = HAL_GetCompressorCurrentTooHighFault(m_compressorHandle, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets if PCM has a sticky fault for the compressor output being shorted.
        /// </summary>
        /// <returns>True if sticky fault is set for the compressor output being shorted.</returns>
        public bool GetCompressorShortedStickyFault()
        {
            int status = 0;
            bool retVal = HAL_GetCompressorShortedStickyFault(m_compressorHandle, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets if the compressor is disabled due to an apparent short in the compressor output.
        /// </summary>
        /// <returns>True if the compressor is shorted.</returns>
        public bool GetCompressorShortedFault()
        {
            int status = 0;
            bool retVal = HAL_GetCompressorShortedFault(m_compressorHandle, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets if PCM has a sticky fault for the compressor output not being connected.
        /// </summary>
        /// <returns>True if sticky fault is set for the compressor output not being connected.</returns>
        public bool GetCompressorNotConnectedStickyFault()
        {
            int status = 0;
            bool retVal = HAL_GetCompressorNotConnectedStickyFault(m_compressorHandle, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Gets if the compressor is disabled due to the compressor output not being connected.
        /// </summary>
        /// <returns>True if the compressor is not connected.</returns>
        public bool GetCompressorNotConnectedFault()
        {
            int status = 0;
            bool retVal = HAL_GetCompressorNotConnectedFault(m_compressorHandle, ref status);
            CheckStatus(status);
            return retVal;
        }

        /// <summary>
        /// Clears ALL sticky faults inside the PCM that the compressor is wired to.
        /// </summary>
        /// <remarks>
        /// If a sticky fault is set, then it will be persistently cleared. Compressor drive
        /// may be momentarily disabled while flages are being cleard. Care should be 
        /// taken to not call this too frequently, otherwise normal compressor functionality
        /// may be prevented.
        /// <para>If no sticky faults are set then this call will have no effect.</para>
        /// </remarks>
        public void ClearAllPCMStickyFaults()
        {
            int status = 0;
            HAL_ClearAllPCMStickyFaults(m_module, ref status);
            CheckStatus(status);
        }

        ///<inheritdoc />
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }
        ///<inheritdoc />
        public ITable Table { get; private set; }
        ///<inheritdoc />
        public string SmartDashboardType => "Compressor";
        ///<inheritdoc />
        public void UpdateTable()
        {
            Table?.PutBoolean("Enabled", Enabled());
            Table?.PutBoolean("Pressure Switch", GetPressureSwitchValue());
        }
        ///<inheritdoc />
        public void StartLiveWindowMode()
        {
        }
        ///<inheritdoc />
        public void StopLiveWindowMode()
        {
        }
    }
}
