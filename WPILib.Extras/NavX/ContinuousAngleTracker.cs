using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    class ContinuousAngleTracker
    {
        private float m_lastAngle;
        private double m_lastRate;
        private int m_zeroCrossingCount;

        public ContinuousAngleTracker()
        {
            m_lastAngle = 0.0f;
            m_zeroCrossingCount = 0;
            m_lastRate = 0;
        }

        public void NextAngle(float newAngle)
        {

            int angleLastDirection;
            float adjustedLastAngle = (m_lastAngle < 0.0f) ? m_lastAngle + 360.0f : m_lastAngle;
            float adjustedCurrAngle = (newAngle < 0.0f) ? newAngle + 360.0f : newAngle;
            float deltaAngle = adjustedCurrAngle - adjustedLastAngle;
            this.m_lastRate = deltaAngle;

            angleLastDirection = 0;
            if (adjustedCurrAngle < adjustedLastAngle)
            {
                if (deltaAngle < -180.0f)
                {
                    angleLastDirection = -1;
                }
                else {
                    angleLastDirection = 1;
                }
            }
            else if (adjustedCurrAngle > adjustedLastAngle)
            {
                if (deltaAngle > 180.0f)
                {
                    angleLastDirection = -1;
                }
                else {
                    angleLastDirection = 1;
                }
            }

            if (angleLastDirection < 0)
            {
                if ((adjustedCurrAngle < 0.0f) && (adjustedLastAngle >= 0.0f))
                {
                    m_zeroCrossingCount--;
                }
            }
            else if (angleLastDirection > 0)
            {
                if ((adjustedCurrAngle >= 0.0f) && (adjustedLastAngle < 0.0f))
                {
                    m_zeroCrossingCount++;
                }
            }
            m_lastAngle = newAngle;

        }

        public double GetAngle()
        {
            double accumulatedAngle = (double)m_zeroCrossingCount * 360.0f;
            double currAngle = (double)m_lastAngle;
            if (currAngle < 0.0f)
            {
                currAngle += 360.0f;
            }
            accumulatedAngle += currAngle;
            return accumulatedAngle;
        }

        public double GetRate()
        {
            return m_lastRate;
        }
    }
}
