using Hal;
using System;

namespace dev
{
    class Program
    {
        static void Main(string[] args)
        {
            HalBase.HAL_Initialize();
            var v = HalBase.GetFPGAVersion();

            var pwm = PWM.InitializePort(HalBase.GetPort(0));

            var notifier = Notifier.Initialize();
            Console.WriteLine("Hello World!");
        }
    }
}
