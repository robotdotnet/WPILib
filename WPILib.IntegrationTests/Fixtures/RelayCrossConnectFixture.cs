using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.IntegrationTests.Fixtures
{
    public abstract class RelayCrossConnectFixture : ITestFixture
    {
        private DigitalInput inputOne;
        private DigitalInput inputTwo;
        private Relay relay;

        private bool initialized = false;
        private bool freed = false;



        protected abstract Relay GiveRelay();

        protected abstract DigitalInput GiveInputOne();

        protected abstract DigitalInput GiveInputTwo();

        private void Initialize()
        {
            lock (this)
            {
                if (!initialized)
                {
                    relay = GiveRelay();
                    inputOne = GiveInputOne();
                    inputTwo = GiveInputTwo();
                    initialized = true;
                }
            }
        }

        public Relay GetRelay()
        {
            Initialize();
            return relay;
        }

        public DigitalInput GetInputOne()
        {
            Initialize();
            return inputOne;
        }

        public DigitalInput GetInputTwo()
        {
            Initialize();
            return inputTwo;
        }

        public bool Setup()
        {
            Initialize();
            return true;
        }

        public bool Reset()
        {
            Initialize();
            return true;
        }

        public bool Teardown()
        {
            if (!freed)
            {
                relay.Dispose();
                inputOne.Dispose();
                inputTwo.Dispose();
                freed = true;
            }
            else
            {
                throw new SystemException($"You attempted to free the {nameof(RelayCrossConnectFixture)} multiple times");
            }
            return true;
        }
    }
}
