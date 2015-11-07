using System;
using HAL_Simulator;

namespace WPILib.IntegrationTests.SimulatedHardware
{
    class SimJumpers
    {
        //Does a 2 way attachment for the jumpers
        public static void AttachDioPins(int input, int output)
        {
            Action<string, dynamic> callback = (key, value) =>
            {
                if (!SimData.DIO[output].IsInput)
                    SimData.DIO[input].Value = value;
            };
            SimData.DIO[output].Register("Value", callback);


            Action<string, dynamic> callback2 = (key, value) =>
            {
                if (!SimData.DIO[input].IsInput)
                    SimData.DIO[output].Value= value;
            };
            SimData.DIO[input].Register("Value", callback2);
        }


        public static void AttachRelay(int relay, int a, int b)
        {
            Action<string, dynamic> fwdCallback = (key, value) =>
            {
                SimData.DIO[a].Value = value;
            };

            Action<string, dynamic> revCallback = (key, value) =>
            {
                SimData.DIO[b].Value = value;
            };

            SimData.Relay[relay].Register("Forward", fwdCallback);
            SimData.Relay[relay].Register("Reverse", revCallback);
        }

        public static void AttachAio(int input, int output)
        {
            Action<string, dynamic> callback = (key, value) =>
            {
                SimData.AnalogIn[input].Voltage = value;
            };

            SimData.AnalogOut[output].Register("Voltage", callback);
        }
    }
}
