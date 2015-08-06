using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator;

namespace WPILib.IntegrationTests.SimulatedHardware
{
    class SimJumpers
    {
        //Does a 2 way attachment for the jumpers
        public static void AttachDIOPins(int input, int output)
        {
            Action<dynamic, dynamic> callback = (key, value) =>
            {
                if (!SimData.HalData["dio"][output]["is_input"])
                    SimData.HalData["dio"][input][key] = value;
            };
            SimData.HalData["dio"][output].Register("value", callback);


            Action<dynamic, dynamic> callback2 = (key, value) =>
            {
                if (!SimData.HalData["dio"][input]["is_input"])
                    SimData.HalData["dio"][output][key] = value;
            };
            SimData.HalData["dio"][input].Register("value", callback2);
        }


        public static void AttachRelay(int relay, int a, int b)
        {
            Action<dynamic, dynamic> fwdCallback = (key, value) =>
            {
                SimData.HalData["dio"][a]["value"] = value;
            };

            Action<dynamic, dynamic> revCallback = (key, value) =>
            {
                SimData.HalData["dio"][b]["value"] = value;
            };

            SimData.HalData["relay"][relay].Register("fwd", fwdCallback);
            SimData.HalData["relay"][relay].Register("rev", revCallback);
        }

        public static void AttachAIO(int input, int output)
        {
            Action<dynamic, dynamic> callback = (key, value) =>
            {
                SimData.HalData["analog_in"][input]["voltage"] = value;
                SimData.HalData["analog_in"][input]["value"] = (int)(value * 204.8);
            };

            SimData.HalData["analog_out"][output].Register("voltage", callback);
        }
    }
}
