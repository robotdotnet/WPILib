using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator;

namespace LoadTester
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            try
            {
                HAL.Base.HAL.PingAll();

                SimData.PingAll();

                Console.WriteLine("Found All Symbols");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
            
        }
    }
}
