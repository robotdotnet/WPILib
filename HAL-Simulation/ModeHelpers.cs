using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HAL_Simulator.SimData;

namespace HAL_Simulator
{
    public static class ModeHelpers
    {
        public static void NotifyNewDSData()
        {
            if (halNewDataSem != IntPtr.Zero)
            {
                HALSemaphore.giveMultiWait(halNewDataSem);
            }
        }


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
                if (mode == "test")
                    SetTestMode(newEnabled);
                else if (mode == "auto")
                    SetAutonomous(newEnabled);
                else if (mode == "teleop")
                    SetTeleopMode(newEnabled);

            }
        }

        public static void SetTeleopMode(bool newEnabled)
        {
            halData["control"]["autonomous"] = false;
            halData["control"]["test"] = false;
            halData["control"]["enabled"] = newEnabled;
            halData["control"]["ds_attached"] = true;

            if (newEnabled)
                halData["time"]["match_start"] = Hooks.GetFPGATime() - 15000000;
            else
                halData["time"]["match_start"] = 0;

            NotifyNewDSData();
        }

        public static void SetAutonomous(bool newEnabled)
        {
            halData["control"]["autonomous"] = true;
            halData["control"]["test"] = false;
            halData["control"]["enabled"] = newEnabled;
            halData["control"]["ds_attached"] = true;

            if (newEnabled)
                halData["time"]["match_start"] = Hooks.GetFPGATime();
            else
                halData["time"]["match_start"] = 0;

            NotifyNewDSData();
        }

        public static void SetTestMode(bool newEnabled)
        {
            halData["control"]["autonomous"] = false;
            halData["control"]["test"] = true;
            halData["control"]["enabled"] = newEnabled;
            halData["control"]["ds_attached"] = true;

            halData["time"]["match_start"] = 0;

            NotifyNewDSData();
        }

        public static void SetDisabled()
        {
            halData["control"]["autonomous"] = false;
            halData["control"]["test"] = false;
            halData["control"]["enabled"] = false;
            halData["control"]["ds_attached"] = true;

            halData["time"]["match_start"] = 0;

            NotifyNewDSData();
        }

        public static void SetEStop()
        {
            halData["control"]["autonomous"] = false;
            halData["control"]["test"] = false;
            halData["control"]["enabled"] = false;
            halData["control"]["ds_attached"] = true;
            halData["control"]["eStop"] = true;

            halData["time"]["match_start"] = 0;

            NotifyNewDSData();
        }



    }
}
