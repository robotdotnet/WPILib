using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.IntegrationTests.Fixtures
{
    public abstract class AnalogCrossConnectFixture : ITestFixture
    {
        private bool initialized = false;

        private AnalogInput input;
        private AnalogOutput output;

        internal abstract AnalogInput GiveAnalogInput();

        internal abstract AnalogOutput GiveAnalogOutput();

        private void Initialize()
        {
            lock (this)
            {
                if (!initialized)
                {
                    input = GiveAnalogInput();
                    output = GiveAnalogOutput();
                    initialized = true;
                }
            }
        }


        public bool Setup()
        {
            Initialize();
            output.SetVoltage(0);
            return true;
        }

        public bool Reset()
        {
            Initialize();
            return true;
        }

        public bool Teardown()
        {
            input.Dispose();
            output.Dispose();
            return true;
        }

        public AnalogOutput GetOutput()
        {
            Initialize();
            return output;
        }

        public AnalogInput GetInput()
        {
            Initialize();
            return input;
        }
    }
}
