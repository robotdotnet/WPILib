using System;
using HAL_Base;
using static HAL_Base.HALCompressor;

namespace WPILib
{
    public class Compressor : SensorBase
    {
        private IntPtr m_pcm; 

        public Compressor(int pcmId)
        {
            InitCompressor(pcmId);
        }

        public Compressor()
        {
            InitCompressor(DefaultSolenoidModule);
        }

        private void InitCompressor(int module)
        {
            m_pcm = InitializeCompressor((byte) module);
        }

        public void Start()
        {
            ClosedLoopControl = true;
        }

        public void Stop()
        {
            ClosedLoopControl = false;
        }

        public bool Enabled
        {
            get
            {
                int status = 0;
                bool on = GetCompressor(m_pcm, ref status);
                return @on;
            }
        }

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
