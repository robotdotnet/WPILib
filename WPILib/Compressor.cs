using System;
using HAL;
using NetworkTables.Tables;
using WPILib.LiveWindows;
using static HAL.Base.HALCompressor;
using static WPILib.Utility;
using HALCompressor = HAL.Base.HALCompressor;

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
        private IntPtr m_pcm; 

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
            if (!CheckCompressorModule((byte) module))
            {
                throw new ArgumentOutOfRangeException(nameof(module), "Compressor module out of range");
            }
            m_pcm = InitializeCompressor((byte) module);
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
            bool on = GetCompressor(m_pcm, ref status);
            CheckStatus(status);
            return on;
        }

        /// <summary>
        /// Gets the value of the pressure switch.
        /// </summary>
        public bool GetPressureSwitchValue()
        {
            int status = 0;
            bool on = GetPressureSwitch(m_pcm, ref status);
            CheckStatus(status);
            return on;
        }


        public float GetCompressorCurrent()
        {
            int status = 0;
            float current = HALCompressor.GetCompressorCurrent(m_pcm, ref status);
            CheckStatus(status);
            return current;
        }

        public bool ClosedLoopControl
        {
            set
            {
                int status = 0;
                SetClosedLoopControl(m_pcm, value, ref status);
                CheckStatus(status);
            }
            get
            {
                int status = 0;
                bool on = GetClosedLoopControl(m_pcm, ref status);
                CheckStatus(status);
                return on;
            }
        }

        public bool GetCompressorCurrentTooHighFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorCurrentTooHighFault(m_pcm, ref status);
            CheckStatus(status);
            return retVal;
        }

        public bool GetCompressorShortedStickyFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorShortedStickyFault(m_pcm, ref status);
            CheckStatus(status);
            return retVal;
        }

        public bool GetCompressorShortedFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorShortedFault(m_pcm, ref status);
            CheckStatus(status);
            return retVal;
        }

        public bool GetCompressorNotConnectedStickyFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorNotConnectedStickyFault(m_pcm, ref status);
            CheckStatus(status);
            return retVal;
        }

        public bool GetCompressorNotConnectedFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorNotConnectedFault(m_pcm, ref status);
            CheckStatus(status);
            return retVal;
        }

        public void ClearAllPCMStickyFaults()
        {
            int status = 0;
            HALCompressor.ClearAllPCMStickyFaults(m_pcm, ref status);
            CheckStatus(status);
        }

        ///<inheritdoc />
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        public ITable Table { get; private set; }
        public string SmartDashboardType => "Compressor";
        public void UpdateTable()
        {
            Table?.PutBoolean("Enabled", Enabled());
            Table?.PutBoolean("Pressure Switch", GetPressureSwitchValue());
        }

        public void StartLiveWindowMode()
        {
        }

        public void StopLiveWindowMode()
        {
        }
    }
}
