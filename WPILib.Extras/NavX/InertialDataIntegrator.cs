using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    class InertialDataIntegrator
    {
        private float[] m_lastVelocity = new float[2];
        private float[] m_displacement = new float[2];

        public InertialDataIntegrator()
        {
            ResetDisplacement();
        }

        public void UpdateDisplacement(float accelXG, float accelYG,
                int updateRateHz, bool isMoving)
        {
            if (isMoving)
            {
                float[] accelG = new float[2];
                float[] accelMS2 = new float[2];
                float[] currVelocityMS = new float[2];
                float sampleTime = (1.0f / updateRateHz);
                accelG[0] = accelXG;
                accelG[1] = accelYG;
                for (int i = 0; i < 2; i++)
                {
                    accelMS2[i] = accelG[i] * 9.80665f;
                    currVelocityMS[i] = m_lastVelocity[i] + (accelMS2[i] * sampleTime);
                    m_displacement[i] += m_lastVelocity[i] + (0.5f * accelMS2[i] * sampleTime * sampleTime);
                    m_lastVelocity[i] = currVelocityMS[i];
                }
            }
            else
            {
                m_lastVelocity[0] = 0.0f;
                m_lastVelocity[1] = 0.0f;
            }
        }

        public void ResetDisplacement()
        {
            for (int i = 0; i < 2; i++)
            {
                m_lastVelocity[i] = 0.0f;
                m_displacement[i] = 0.0f;
            }
        }

        public float GetVelocityX()
        {
            return m_lastVelocity[0];
        }

        public float GetVelocityY()
        {
            return m_lastVelocity[1];
        }

        public float GetVelocityZ()
        {
            return 0;
        }

        public float GetDisplacementX()
        {
            return m_displacement[0];
        }

        public float GetDisplacementY()
        {
            return m_displacement[1];
        }

        public float GetDisplacementZ()
        {
            return 0;
        }
    }
}
