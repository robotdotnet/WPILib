using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static HAL_Simulator.SimData;

namespace HAL_Simulator
{
    public static class DriverStationHelper
    {
        public enum RobotMode
        {
            Autonomous,
            Teleop,
            Test,
        }

        public enum EnabledState
        {
            Enabled,
            Disabled,
            EStopped,
        }

        private static Timer s_dsTimer;

        public static Dictionary<dynamic, dynamic> DSData; 

        public static void StartDSLoop()
        {
            StartDSLoop(TimeSpan.FromMilliseconds(20));
        }

        public static void StartDSLoop(int loopTime)
        {
            StartDSLoop(TimeSpan.FromMilliseconds(loopTime));
        }

        public static void StartDSLoop(TimeSpan loopTime)
        {
            DSData = HalDSData;
            if (s_dsTimer == null)
            {
                s_dsTimer = new Timer(s =>
                {
                    UpdateHalData(HalDSData);
                    if (halNewDataSem != IntPtr.Zero)
                    {
                        HALSemaphore.giveMultiWait(halNewDataSem);
                    }
                }, null, loopTime, loopTime);
            }
        }

        public static void SetJoystickButton(int joystickNum, int buttonNum, bool state)
        {
            if (joystickNum < 0 || joystickNum >= HalDSData["joysticks"].Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {HalDSData["joysticks"].Count - 1}");
            }
            if (buttonNum < 1 || buttonNum >= HalDSData["joysticks"][joystickNum]["buttons"].Length)
            {
                throw new ArgumentOutOfRangeException(nameof(buttonNum),
                    $"Button must be between 1 and {HalDSData["joysticks"][joystickNum]["buttons"].Length - 1}");
            }
            HalDSData["joysticks"][joystickNum]["buttons"][buttonNum] = state;
        }

        public static void SetJoystickAxis(int joystickNum, int axisNum, double value)
        {
            if (joystickNum < 0 || joystickNum >= HalDSData["joysticks"].Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {HalDSData["joysticks"].Count - 1}");
            }
            if (axisNum < 0 || axisNum >= HalDSData["joysticks"][joystickNum]["axes"].Length)
            {
                throw new ArgumentOutOfRangeException(nameof(axisNum),
                    $"Axis must be between 0 and {HalDSData["joysticks"][joystickNum]["axes"].Length - 1}");
            }
            HalDSData["joysticks"][joystickNum]["axes"][axisNum] = (float)value;
        }

        public static void SetJoystickPOV(int joystickNum, int povNum, int povValue)
        {
            if (joystickNum < 0 || joystickNum >= HalDSData["joysticks"].Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {HalDSData["joysticks"].Count - 1}");
            }
            if (povNum < 1 || povNum >= HalDSData["joysticks"][joystickNum]["povs"].Length)
            {
                throw new ArgumentOutOfRangeException(nameof(povNum),
                    $"POV must be between 0 and {HalDSData["joysticks"][joystickNum]["povs"].Length - 1}");
            }
            HalDSData["joysticks"][joystickNum]["povs"][povNum] = (short)povValue;
        }


        public static void SetEnabledState(EnabledState state)
        {
            switch (state)
            {
                case EnabledState.Disabled:
                    HalDSData["control"]["enabled"] = false;
                    HalDSData["control"]["eStop"] = false;
                    HalDSData["control"]["ds_attached"] = true;
                    break;
                case EnabledState.Enabled:
                    HalDSData["control"]["enabled"] = true;
                    HalDSData["control"]["eStop"] = false;
                    HalDSData["control"]["ds_attached"] = true;
                    break;
                case EnabledState.EStopped:
                    HalDSData["control"]["enabled"] = false;
                    HalDSData["control"]["eStop"] = true;
                    HalDSData["control"]["ds_attached"] = true;
                    break;
            }
        }

        public static void SetRobotMode(RobotMode mode)
        {
            RobotMode prevState = RobotMode.Teleop;
            if (HalData["control"]["autonomous"])
            {
                prevState = RobotMode.Autonomous;
            }
            else if (HalData["control"]["test"])
            {
                prevState = RobotMode.Test;
            }
            switch (mode)
            {
                case RobotMode.Autonomous:
                    if (prevState != RobotMode.Autonomous)
                        SetEnabledState(EnabledState.Disabled);
                    HalDSData["control"]["autonomous"] = true;
                    HalDSData["control"]["test"] = false;
                    break;
                case RobotMode.Teleop:
                    if (prevState != RobotMode.Teleop)
                        SetEnabledState(EnabledState.Disabled);
                    HalDSData["control"]["autonomous"] = false;
                    HalDSData["control"]["test"] = false;
                    break;
                case RobotMode.Test:
                    if (prevState != RobotMode.Test)
                        SetEnabledState(EnabledState.Disabled);
                    HalDSData["control"]["autonomous"] = false;
                    HalDSData["control"]["test"] = true;
                    break;
                default:
                    SetEnabledState(EnabledState.Disabled);
                    HalDSData["control"]["autonomous"] = false;
                    HalDSData["control"]["test"] = false;
                    break;
            }
        }




    }
}
