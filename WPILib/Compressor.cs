using System;
using HAL_Base;

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
            InitCompressor(GetDefaultSolenoidModule());
        }

        private void InitCompressor(int module)
        {
            m_pcm = HALCompressor.InitializeCompressor((byte) module);
        }

        public void Start()
        {
            SetClosedLoopControl(true);
        }

        public void Stop()
        {
            SetClosedLoopControl(false);
        }

        public bool Enabled()
        {
            int status = 0;
            bool on = HALCompressor.GetCompressor(m_pcm, ref status);
            return on;
        }

        public bool GetPressureSwitchValue()
        {
            int status = 0;
            bool on = HALCompressor.GetPressureSwitch(m_pcm, ref status);
            return on;
        }

        public float GetCompressorCurrent()
        {
            int status = 0;
            float current = HALCompressor.GetCompressorCurrent(m_pcm, ref status);
            return current;
        }

        public void SetClosedLoopControl(bool on)
        {
            int status = 0;
            HALCompressor.SetClosedLoopControl(m_pcm, on, ref status);
        }

        public bool GetClosedLoopControl()
        {
            int status = 0;
            bool on = HALCompressor.GetClosedLoopControl(m_pcm, ref status);
            return on;
        }

        public bool GetCompressorCurrentTooHighFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorCurrentTooHighFault(m_pcm, ref status);
            return retVal;
        }

        public bool GetCompressorShortedStickyFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorShortedStickyFault(m_pcm, ref status);
            return retVal;
        }

        public bool GetCompressorShortedFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorShortedFault(m_pcm, ref status);
            return retVal;
        }

        public bool GetCompressorNotConnectedStickyFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorNotConnectedStickyFault(m_pcm, ref status);
            return retVal;
        }

        public bool GetCompressorNotConnectedFault()
        {
            int status = 0;
            bool retVal = HALCompressor.GetCompressorNotConnectedFault(m_pcm, ref status);
            return retVal;
        }

        public void ClearAllPCMStickyFaults()
        {
            int status = 0;
            HALCompressor.ClearAllPCMStickyFaults(m_pcm, ref status);
        }
    }
}
