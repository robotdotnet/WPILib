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

            var cNotifier = new WPILib.Notifier(() =>
            {
                Console.WriteLine("Handler!");
            });

            cNotifier.Name = "Notifier Thread";
            cNotifier.StartPeriodic(TimeSpan.FromSeconds(1));
            Console.WriteLine("Hello World!");

            Console.ReadLine();

            cNotifier.Stop();
            cNotifier.Dispose();
        }
    }
}
