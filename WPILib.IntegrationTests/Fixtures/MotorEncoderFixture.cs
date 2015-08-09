using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.IntegrationTests.Test;
using WPILib.Interfaces;

namespace WPILib.IntegrationTests.Fixtures
{
    public abstract class MotorEncoderFixture : ITestFixture 
    {
        private bool initialized = false;
        private bool tornDown = false;
        private ISpeedController motor;
        private Encoder encoder;
        private readonly Counter[] counters = new Counter[2];
        protected DigitalInput aSource;
        protected DigitalInput bSource;

        public abstract int GetPDPChannel();

        protected abstract ISpeedController GiveSpeedController();

        protected abstract DigitalInput GiveDigitalInputA();

        protected abstract DigitalInput GiveDigitalInputB();

        private void Initialize()
        {
            lock (this)
            {
                if (!initialized)
                {
                    initialized = true;

                    aSource = GiveDigitalInputA();
                    bSource = GiveDigitalInputB();

                    encoder = new Encoder(aSource, bSource);

                    counters[0] = new Counter(aSource);
                    counters[1] = new Counter(bSource);

                    motor = GiveSpeedController();
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
            return motor;
        }

        public Encoder GetEncoder()
        {
            Initialize();
            return encoder;
        }

        public Counter[] GetCounters()
        {
            Initialize();
            return counters;
        }

        public string GetCustomType()
        {
            Initialize();
            return motor.GetType().Name;
        }

        public bool IsMotorSpeedWithinRange(double value, double accuracy)
        {
            Initialize();
            return Math.Abs((Math.Abs(motor.Get()) - Math.Abs(value))) < Math.Abs(accuracy);
        }

        public bool Reset()
        {
            Initialize();
            bool wasReset = true;
            motor.Inverted = false;
            motor.Set(0);
            Timer.Delay(TestBench.MOTOR_STOP_TIME);
            encoder.Reset();
            foreach (var c in counters)
            {
                c.Reset();
            }
            wasReset = wasReset && motor.Get() == 0;

            wasReset = wasReset && encoder.Get() == 0;

            foreach (var c in counters)
            {
                wasReset = wasReset && c.Get() == 0;
            }

            return wasReset;;
        }

        public bool Teardown()
        {
            var type = motor != null ? GetCustomType() : "null";
            if (!tornDown)
            {
                bool wasNull = false;
                var pwm = motor as PWM;
                if (pwm != null && motor != null) {
                    pwm.Dispose();
                    motor = null;
                } else if (motor == null)
                    wasNull = true;
                if (encoder != null)
                {
                    encoder.Dispose();
                    encoder = null;
                }
                else
                    wasNull = true;
                if (counters[0] != null)
                {
                    counters[0].Dispose();
                    counters[0] = null;
                }
                else
                    wasNull = true;
                if (counters[1] != null)
                {
                    counters[1].Dispose();
                    counters[1] = null;
                }
                else
                    wasNull = true;
                if (aSource != null)
                {
                    aSource.Dispose();
                    aSource = null;
                }
                else
                    wasNull = true;
                if (bSource != null)
                {
                    bSource.Dispose();
                    bSource = null;
                }
                else
                    wasNull = true;

                tornDown = true;

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
