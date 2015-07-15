using System;
using System.Threading;
using static HAL_Simulator.SimData;

namespace HAL_Simulator
{
    /*
    public static class ModeHelpers
    {
        public static void NotifyNewDSData()
        {
            if (halNewDataSem != IntPtr.Zero)
            {
                HALSemaphore.giveMultiWait(halNewDataSem);
            }
        }

        private static Thread DSLoop;

        public static void StartDSLoop(int millisecondLoopTime = 20)
        {
            if (DSLoop == null)
            {
                DSLoop = new Thread(() =>
                {
                    while (true)
                    {
                        NotifyNewDSData();
                        Thread.Sleep(millisecondLoopTime);
                    }
                });
                DSLoop.Start();
            }
        }

        public static void KillDSLoop()
        {
            DSLoop?.Abort();
        }

        //TODO: These all need to be rewritten to make sense with how the 
        //FPGA actually works.


        private static string oldMode = "teleop";
        public static void SetMode(string mode, bool newEnabled)
        {
            if (mode != "auto" || mode != "test" || mode != "teleop")
            {
                throw new Exception("Bad Mode");//TODO : Fix This
            }

            var ctrl = halData["control"];
            bool enabled = ctrl["enabled"];
            if (ctrl["autonomous"])
                oldMode = "auto";
            else if (ctrl["test"])
                oldMode = "test";
            else
                oldMode = "teleop";

            if (mode != oldMode || enabled != newEnabled)
            {
                switch (mode)
                {
                    case "test":
                        SetTestMode(newEnabled);
                        break;
                    case "auto":
                        SetAutonomous(newEnabled);
                        break;
                    case "teleop":
                        SetTeleopMode(newEnabled);
                        break;
                }

            }
        }

        public static void SetTeleopMode(bool newEnabled)
        {
            halData["control"]["autonomous"] = false;
            halData["control"]["test"] = false;
            halData["control"]["enabled"] = newEnabled;
            halData["control"]["ds_attached"] = true;

            //halData["time"]["match_start"] = SimHooks.GetFPGATimestamp();

            if (newEnabled)
                halData["time"]["match_start"] = SimHooks.GetFPGATimestamp();

            NotifyNewDSData();
        }

        public static void SetAutonomous(bool newEnabled)
        {
            halData["control"]["autonomous"] = true;
            halData["control"]["test"] = false;
            halData["control"]["enabled"] = newEnabled;
            halData["control"]["ds_attached"] = true;

            if (newEnabled)
                halData["time"]["match_start"] = SimHooks.GetFPGATimestamp();
            NotifyNewDSData();
        }

        public static void SetTestMode(bool newEnabled)
        {
            halData["control"]["autonomous"] = false;
            halData["control"]["test"] = true;
            halData["control"]["enabled"] = newEnabled;
            halData["control"]["ds_attached"] = true;

            halData["time"]["match_start"] = SimHooks.GetFPGATimestamp();

            NotifyNewDSData();
        }

        public static void SetDisabled()
        {
            halData["control"]["autonomous"] = false;
            halData["control"]["test"] = false;
            halData["control"]["enabled"] = false;
            halData["control"]["ds_attached"] = true;

            halData["time"]["match_start"] = SimHooks.GetFPGATimestamp();

            NotifyNewDSData();
        }

        public static void SetEStop()
        {
            halData["control"]["autonomous"] = false;
            halData["control"]["test"] = false;
            halData["control"]["enabled"] = false;
            halData["control"]["ds_attached"] = true;
            halData["control"]["eStop"] = true;

            halData["time"]["match_start"] = SimHooks.GetFPGATimestamp();

            NotifyNewDSData();
        }

  

    }
*/
}
