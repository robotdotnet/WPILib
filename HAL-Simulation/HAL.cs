

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using HAL_Base;
using static HAL_Simulator.SimData;

namespace HAL_Simulator
{
    public class HAL
    {
        public static IntPtr getPort(byte pin)
        {
            return getPortWithModule(1, pin);
        }

        public static IntPtr getPortWithModule(byte module, byte pin)
        {
            Port port = new Port
            {
                pin = pin,
                module = module
            };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(port));
            Marshal.StructureToPtr(port, ptr, true);
            return ptr;
        }

        public static void HALSetNewDataSem(IntPtr sem)
        {
            halNewDataSem = sem;
        }

        public static IntPtr getHALErrorMessage(int code)
        {
            string retVal = "";

            return Marshal.StringToHGlobalAnsi(retVal);
        }

        public static ushort getFPGAVersion(ref int status)
        {
            status = 0;
            return 2015;
        }

        public static uint getFPGARevision(ref int status)
        {
            status = 0;
            return 0;
        }

        public static uint getFPGATime(ref int status)
        {
            status = 0;
            return Hooks.GetFPGATime();
        }

        public static bool getFPGAButton(ref int status)
        {
            status = 0;
            return halData["fpga_button"];
        }

        public static int HALSetErrorData(string errors, int errorsLength, int wait_ms)
        {
            //TODO: Logger 
            halData["error_data"] = errors;
            return 0;
        }

        //[System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetControlWord")]
        public static int HALGetControlWord(ref HALControlWord data)
        {
            var h = halData["control"];
            bool[] d = new bool[32];
            d[0] = h["enabled"];
            d[1] = h["autonomous"];
            d[2] = h["test"];
            d[3] = h["eStop"];
            d[4] = h["fms_attached"];
            d[5] = h["ds_attached"];

            int r = 0;
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i])
                {
                    r |= 1 << (d.Length - i);
                }
            }



            data = new HALControlWord((uint) r);
            return 0;
        }

        public static int HALGetAllianceStation(ref HALAllianceStationID allianceStation)
        {
            return halData["alliance_station"];
        }

        public static int HALGetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes)
        {
            axes.axes = new HALJoystickAxesArray();
            var joyData = halData["joysticks"][joystickNum]["axes"];
            for (short i = 0; i < joyData.Length; i++)
            {
                int tmp = 0;
                if (joyData[i] < 0)
                    tmp = joyData[i]*128;
                else
                    tmp = joyData[i]*127;
                axes.axes[i] = (short) tmp;
            }
            axes.count = 12; //Need to make this netter.
            return 0;
        }

        public static int HALGetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs)
        {
            //TODO: Fix
            povs.povs = new HALJoystickPOVArray();
            povs.count = 12;
            return 0;
        }

        public static int HALGetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons)
        {
            //TODO: Fix
            buttons.buttons = 123;
            buttons.count = 12;
            return 0;
        }

        public static int HALGetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc)
        {
            var stick = halData["joysticks"][joystickNum];
            desc.isXbox = stick["isXbox"];
            desc.type = stick["type"];
            desc.name = stick["name"];
            desc.axisCount = stick["axisCount"];
            desc.buttonCount = stick["buttonCount"];
            return 0;
        }

        public static int HALGetJoystickIsXbox(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return stick["isXbox"];
        }

        public static int HALGetJoystickType(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return stick["type"];
        }

        public static IntPtr HALGetJoystickName(byte joystickNum)
        {
            var stick = halData["joysticks"][joystickNum];
            return Marshal.StringToHGlobalAnsi(stick["name"]);
        }

        public static int HALGetJoystickAxisType(byte joystickNum, byte axis)
        {
            var stick = halData["joysticks"][joystickNum];
            return stick["NotImplemented"];
        }

        public static int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble,
            ushort rightRumble)
        {
            halData["joysticks"][joystickNum]["leftRumble"] = leftRumble;
            halData["joysticks"][joystickNum]["rightRumble"] = rightRumble;
            //halData[]TODO:Outputs
            return 0;
        }

        public static int HALGetMatchTime(ref float matchTime)
        {
            var matchStart = halData["time"]["match_start"];
            if (matchStart == null)
            {
                return 0;
            }
            else
            {
                return ((Hooks.GetFPGATime() - matchStart)/
                        1000000.0);
            }
        }

        //[System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALSetNewDataSem")]
        //public static extern void HALSetNewDataSem(System.IntPtr sem);

        public static bool HALGetSystemActive(ref int status)
        {
            status = 0;
            return true;
        }

        public static bool HALGetBrownedOut(ref int status)
        {
            status = 0;
            return false;
        }

        //[System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALInitialize")]
        public static int HALInitialize(int mode)
        {
            ResetHALData();
            /*
            timer = new Timer((sender) =>
            {
                if (SimData.halNewDataSem != IntPtr.Zero)
                {
                    //HALSemaphore.giveMultiWait(SimData.halNewDataSem);
                }
            },null, 10, 10);
            */
            timer = new Thread(() =>
            {
                while (true)
                {
                    if (halNewDataSem != IntPtr.Zero)
                    {
                        HALSemaphore.giveMultiWait(halNewDataSem);
                    }
                    Thread.Sleep(20);
                }
            });

            timer.Start();

            return 1;
        }

        private static Thread timer;

        public static void HALNetworkCommunicationObserveUserProgramStarting()
        {
            halData["user_program_state"] = "starting";
        }

        public static void HALNetworkCommunicationObserveUserProgramDisabled()
        {
            halData["user_program_state"] = "disabled";
        }

        public static void HALNetworkCommunicationObserveUserProgramAutonomous()
        {
            halData["user_program_state"] = "autonomous";
        }

        public static void HALNetworkCommunicationObserveUserProgramTeleop()
        {
            halData["user_program_state"] = "teleop";
        }

        public static void HALNetworkCommunicationObserveUserProgramTest()
        {
            halData["user_program_state"] = "test";
        }

        public static void NumericArrayResize()
        {

        }

        public static void RTSetCleanupProc()
        {

        }

        public static void EDVR_CreateReference()
        {

        }

        public static void Occur()
        {

        }


        public static uint HALReport(byte resource, byte instanceNumber, byte context = 0, string feature = null)
        {
            return 0;
        }
    }
}
