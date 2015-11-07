using System;
using System.Threading;

namespace WPILib.IntegrationTests.MockHardware
{
    public class FakeEncoderSource
    {
        private Thread m_task;
        private int m_count;
        private int m_mSec;
        private bool m_forward;
        private readonly DigitalOutput m_outputA;
        private readonly DigitalOutput m_outputB;
        private readonly bool m_allocatedOutputs;

        internal void Run()
        {
            DigitalOutput lead, lag;

            m_outputA.Set(false);
            m_outputB.Set(false);

            if (IsForward())
            {
                lead = m_outputA;
                lag = m_outputB;
            }
            else
            {
                lead = m_outputB;
                lag = m_outputA;
            }

            try
            {
                for (int i = 0; i < m_count; i++)
                {
                    lead.Set(true);
                    Thread.Sleep(m_mSec);
                    lag.Set(true);
                    Thread.Sleep(m_mSec);
                    lead.Set(false);
                    Thread.Sleep(m_mSec);
                    lag.Set(false);
                    Thread.Sleep(m_mSec);
                }
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        public FakeEncoderSource(DigitalOutput iA, DigitalOutput iB)
        {
            m_outputA = iA;
            m_outputB = iB;
            m_allocatedOutputs = false;
            InitQuadEncoder();
        }

        public FakeEncoderSource(int portA, int portB)
        {
            m_outputA = new DigitalOutput(portA);
            m_outputB = new DigitalOutput(portB);
            m_allocatedOutputs = true;
            InitQuadEncoder();
        }

        public void Dispose()
        {
            m_task = null;
            if (m_allocatedOutputs)
            {
                m_outputA.Dispose();
                m_outputB.Dispose();
            }
        }

        public void InitQuadEncoder()
        {
            m_mSec = 1;
            m_forward = true;
            m_task = new Thread(Run);
            m_outputA.Set(false);
            m_outputB.Set(false);
        }

        public void Start()
        {
            m_task.Start();
        }

        public void Complete()
        {
            try
            {
                m_task.Join();
            }
            catch (ThreadInterruptedException)
            {

            }
            m_task = new Thread(Run);
            Timer.Delay(0.01);
        }

        public void Execute()
        {
            Start();
            Complete();
        }

        public void SetCount(int count)
        {
            m_count = Math.Abs(count);
        }

        public void SetRate(int mSec)
        {
            m_mSec = mSec;
        }

        public void SetForward(bool isForwared)
        {
            m_forward = isForwared;
        }

        public bool IsForward()
        {
            return m_forward;
        }
    }
}
