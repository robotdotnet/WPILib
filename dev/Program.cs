using Hal;
using System;

namespace dev
{
    class Program
    {
        static void Main(string[] args)
        {


            // Hal.DesktopLibraries.libraries.windows.x86_64.wpiHalJni.dll 
            // Hal.DesktopLibraries.libraries.windows.x86_64.wpiHaljni.dll
            HalBase.HAL_Initialize();
            Console.WriteLine("Hello World!");
        }
    }
}
