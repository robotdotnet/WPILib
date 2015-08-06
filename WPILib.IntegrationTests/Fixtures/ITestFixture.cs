using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.IntegrationTests.Fixtures
{
    interface ITestFixture
    {
        bool Setup();

        bool Reset();

        bool Teardown();
    }
}
