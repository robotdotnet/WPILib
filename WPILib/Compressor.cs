using System;
using HAL_Base;
using static HAL_Base.HALCompressor;

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
    public class Compressor : SensorBase
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
        public bool Enabled
        {
            get
            {
                int status = 0;
                bool on = GetCompressor(m_pcm, ref status);
                return @on;
            }
        }

        /// <summary>
        /// Gets the value of the pressure switch.
        /// </summary>
        public bool PressureSwitchValue
        {
            get
            {
                int status = 0;
                bool on = GetPressureSwitch(m_pcm, ref status);
                return @on;
            }
        }


        public float CompressorCurrent
        {
            get
            {
                int status = 0;
                float current = GetCompressorCurrent(m_pcm, ref status);
                return current;
            }
        }

        public bool ClosedLoopControl
        {
            set
            {
                int status = 0;
                SetClosedLoopControl(m_pcm, value, ref status);
            }
            get
            {
                int status = 0;
                bool on = GetClosedLoopControl(m_pcm, ref status);
                return @on;
            }
        }

        public bool CompressorCurrentTooHighFault
        {
            get
            {
                int status = 0;
                bool retVal = GetCompressorCurrentTooHighFault(m_pcm, ref status);
                return retVal;
            }
        }

        public bool CompressorShortedStickyFault
        {
            get
            {
                int status = 0;
                bool retVal = GetCompressorShortedStickyFault(m_pcm, ref status);
                return retVal;
            }
        }

        public bool CompressorShortedFault
        {
            get
            {
                int status = 0;
                bool retVal = GetCompressorShortedFault(m_pcm, ref status);
                return retVal;
            }
        }

        public bool CompressorNotConnectedStickyFault
        {
            get
            {
                int status = 0;
                bool retVal = GetCompressorNotConnectedStickyFault(m_pcm, ref status);
                return retVal;
            }
        }

        public bool CompressorNotConnectedFault
        {
            get
            {
                int status = 0;
                bool retVal = GetCompressorNotConnectedFault(m_pcm, ref status);
                return retVal;
            }
        }

        public void ClearAllPCMStickyFaults()
        {
            int status = 0;
            HALCompressor.ClearAllPCMStickyFaults(m_pcm, ref status);
        }
    }
}
