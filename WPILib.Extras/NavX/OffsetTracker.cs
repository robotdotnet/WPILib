using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    class OffsetTracker
    {
        float[] m_valueHistory;
        int m_nextValueHistoryIndex;
        int m_historyLen;
        double m_valueOffset;

        public OffsetTracker(int historyLength)
        {
            m_historyLen = historyLength;
            m_valueHistory = new float[m_historyLen];
            for (int i = 0; i < m_historyLen; i++)
            {
                m_valueHistory[i] = 0;
            }
            m_nextValueHistoryIndex = 0;
            m_valueOffset = 0;
        }

        public void UpdateHistory(float currValue)
        {
            if (m_nextValueHistoryIndex >= m_historyLen)
            {
                m_nextValueHistoryIndex = 0;
            }
            m_valueHistory[m_nextValueHistoryIndex] = currValue;
            m_nextValueHistoryIndex++;
        }

        public double GetAverageFromHistory()
        {
            double valueHistorySum = 0.0;
            for (int i = 0; i < m_historyLen; i++)
            {
                valueHistorySum += m_valueHistory[i];
            }
            double valueHistoryAvg = valueHistorySum / m_historyLen;
            return valueHistoryAvg;
        }

        public void SetOffset()
        {
            m_valueOffset = GetAverageFromHistory();
        }

        public double GetOffset()
        {
            return m_valueOffset;
        }

        public double ApplyOffset(double value)
        {
            float offsetedValue = (float)(value - m_valueOffset);
            if (offsetedValue < -180)
            {
                offsetedValue += 360;
            }
            if (offsetedValue > 180)
            {
                offsetedValue -= 360;
            }
            return offsetedValue;
        }
    }
}
