using Hal;
using System;
using System.Linq;
using System.Threading;

namespace roboRIODev
{
    class Program
    {
        public interface DefaultInterfaceCheck
        {
            string GetThing(); 

            public int GetThingInt()
            {
                if (int.TryParse(GetThing(), out var r))
                {
                    return r;
                }
                return -1;
            }
        }

        public class Tester : DefaultInterfaceCheck
        {
            public string GetThing()
            {
                return "1234";
            }
        }

        static void Main(string[] args)
        {
            DefaultInterfaceCheck dic = new Tester();
            Console.WriteLine(dic.GetThingInt());
            //var a =typeof(System.Runtime.InteropServices.OSPlatform).Assembly.GetTypes().Where(x => x.Namespace!.StartsWith("System.Runtime.InteropServices")).ToArray();
            //foreach (var aa in a)
            //{
            //    Console.WriteLine(aa);
            //}
            //Console.WriteLine();
            HalBase.Initialize();

            DriverStation.ObserveUserProgramStarting();

            while (true)
            {
                DriverStation.WaitForDSData();
                var controlWord = DriverStation.GetControlWord();

                if (controlWord.Enabled)
                {
                    if (controlWord.Autonomous)
                    {
                        DriverStation.ObserveUserProgramAutonomous();
                    }
                    else
                    {
                        DriverStation.ObserveUserProgramTeleop();
                    }
                }
                else
                {
                    DriverStation.ObserveUserProgramDisabled();
                }
            }

            //Console.WriteLine("Hello World!");
        }
    }
}
