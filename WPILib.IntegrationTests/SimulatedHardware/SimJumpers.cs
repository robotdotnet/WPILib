using System;
using HAL.Simulator;
using HAL.Simulator.Data;

namespace WPILib.IntegrationTests.SimulatedHardware
{
    class SimJumpers
    {
        //Does a 2 way attachment for the jumpers
        public static void AttachDioPins(int input, int output)
        {
            NotifyCallback callback = (key, value) =>
            {
                if (!SimData.DIO[output].GetIsInput())
                    SimData.DIO[input].SetValue(value.GetBoolean());
            };
            SimData.DIO[output].RegisterValueCallback(callback);


            NotifyCallback callback2 = (key, value) =>
            {
                if (!SimData.DIO[input].GetIsInput())
                    SimData.DIO[output].SetValue(value.GetBoolean());
            };
            SimData.DIO[input].RegisterValueCallback(callback2);
        }


        public static void AttachRelay(int relay, int a, int b)
        {
            NotifyCallback fwdCallback = (key, value) =>
            {
                SimData.DIO[a].SetValue(value.GetBoolean());
            };

            NotifyCallback revCallback = (key, value) =>
            {
                SimData.DIO[b].SetValue(value.GetBoolean());
            };

            SimData.Relay[relay].RegisterForwardCallback(fwdCallback);
            SimData.Relay[relay].RegisterReverseCallback(revCallback);
        }

        public static void AttachAio(int input, int output)
        {
            NotifyCallback callback = (key, value) =>
            {
                SimData.AnalogIn[input].SetVoltage(value.GetDouble());
            };

            SimData.AnalogOut[output].RegisterVoltageCallback(callback);
        }
    }
}
