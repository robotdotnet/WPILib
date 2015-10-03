using System.Threading;

namespace WPILib.IntegrationTests.MockHardware
{
    public class FakeCounterSource
    {
        private Thread m_task;
        private int m_count;
        private int m_mSec;
        private DigitalOutput m_output;
        private bool m_allocated;

        internal void Run()
        {
            m_output.Set(false);
            try
            {
                for (int i = 0; i < m_count; i++)
                {
                    Thread.Sleep(m_mSec);
                    m_output.Set(true);
                    Thread.Sleep(m_mSec);
                    m_output.Set(false);
                }
            }
            catch (ThreadInterruptedException)
            {

            }
        }

        public FakeCounterSource(DigitalOutput output)
        {
            m_output = output;
            m_allocated = false;
            InitEncoder();
        }

        public FakeCounterSource(int port)
        {
            m_output = new DigitalOutput(port);
            m_allocated = false;
            InitEncoder();
        }

        public void Dispose()
        {
            m_task = null;
            if (m_allocated)
            {
                m_output.Dispose();
                m_output = null;
                m_allocated = false;
            }
        }

        public void InitEncoder()
        {
            m_mSec = 1;
            m_task = new Thread(Run);
            m_output.Set(false);
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
            m_count = count;
        }

        public void SetRate(int mSec)
        {
            m_mSec = mSec;
        }
    }
}
