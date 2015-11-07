using System;
using System.Linq;
using System.Text;
using WPILib.IntegrationTests.Test;
using WPILib.Interfaces;

namespace WPILib.IntegrationTests.Fixtures
{
    public abstract class MotorEncoderFixture : ITestFixture 
    {
        private bool m_initialized;
        private bool m_tornDown;
        private ISpeedController m_motor;
        private Encoder m_encoder;
        private readonly Counter[] m_counters = new Counter[2];
        protected DigitalInput m_aSource;
        protected DigitalInput m_bSource;

        public abstract int GetPdpChannel();

        protected abstract ISpeedController GiveSpeedController();

        protected abstract DigitalInput GiveDigitalInputA();

        protected abstract DigitalInput GiveDigitalInputB();

        private void Initialize()
        {
            lock (this)
            {
                if (!m_initialized)
                {
                    m_initialized = true;

                    m_aSource = GiveDigitalInputA();
                    m_bSource = GiveDigitalInputB();

                    m_encoder = new Encoder(m_aSource, m_bSource);

                    m_counters[0] = new Counter(m_aSource);
                    m_counters[1] = new Counter(m_bSource);

                    m_motor = GiveSpeedController();
                }
            }
        }


        public bool Setup()
        {
            Initialize();
            return true;
        }

        public ISpeedController GetMotor()
        {
            Initialize();
            return m_motor;
        }

        public Encoder GetEncoder()
        {
            Initialize();
            return m_encoder;
        }

        public Counter[] GetCounters()
        {
            Initialize();
            return m_counters;
        }

        public string GetCustomType()
        {
            Initialize();
            return m_motor.GetType().Name;
        }

        public bool IsMotorSpeedWithinRange(double value, double accuracy)
        {
            Initialize();
            return Math.Abs(Math.Abs(m_motor.Get()) - Math.Abs(value)) < Math.Abs(accuracy);
        }

        public bool Reset()
        {
            Initialize();
            bool wasReset = true;
            m_motor.Inverted = false;
            m_motor.Set(0);
            Timer.Delay(TestBench.MotorStopTime);
            m_encoder.Reset();
            foreach (var c in m_counters)
            {
                c.Reset();
            }
            wasReset = wasReset && m_motor.Get() == 0;

            wasReset = wasReset && m_encoder.Get() == 0;

            return m_counters.Aggregate(wasReset, (current, c) => current && c.Get() == 0);
        }

        public bool Teardown()
        {
            var type = m_motor != null ? GetCustomType() : "null";
            if (!m_tornDown)
            {
                bool wasNull = false;
                var pwm = m_motor as PWM;
                if (pwm != null && m_motor != null) {
                    pwm.Dispose();
                    m_motor = null;
                } else if (m_motor == null)
                    wasNull = true;
                if (m_encoder != null)
                {
                    m_encoder.Dispose();
                    m_encoder = null;
                }
                else
                    wasNull = true;
                if (m_counters[0] != null)
                {
                    m_counters[0].Dispose();
                    m_counters[0] = null;
                }
                else
                    wasNull = true;
                if (m_counters[1] != null)
                {
                    m_counters[1].Dispose();
                    m_counters[1] = null;
                }
                else
                    wasNull = true;
                if (m_aSource != null)
                {
                    m_aSource.Dispose();
                    m_aSource = null;
                }
                else
                    wasNull = true;
                if (m_bSource != null)
                {
                    m_bSource.Dispose();
                    m_bSource = null;
                }
                else
                    wasNull = true;

                m_tornDown = true;

                if (wasNull)
                {
                    throw new NullReferenceException("MotorEncoderFixture had null params at teardown");
                }
            }
            else
            {
                throw new SystemException(type + " Motor Encoder torn down multiple times");
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sendString = new StringBuilder("MotorEncoderFixture<");
            string class1 = GetType().Name;
            sendString.Append(class1);
            sendString.Append(">");
            return sendString.ToString();
        }
    }
}
