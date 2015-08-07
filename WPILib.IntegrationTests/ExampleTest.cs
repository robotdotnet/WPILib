using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture]
    class ExampleTest : AbstractComsSetup
    {
        [Test]
        public void PrintData([Range(0,4)] int port)
        {
            /*
            AnalogInput input = new AnalogInput(port);
            Console.WriteLine("Port" + port);
            Console.WriteLine(input.AverageBits);
            Console.WriteLine(input.LSBWeight);
            Console.WriteLine(input.Offset);
            Console.WriteLine(input.OversampleBits);
            Console.WriteLine(AnalogInput.GlobalSampleRate);
            */
        }
    }
}
