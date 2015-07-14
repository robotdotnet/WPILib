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

        private static Timer dsTimer;

        public static void StartDSLoop(int loopTime)
        {
            StartDSLoop(TimeSpan.FromMilliseconds(loopTime));
        }

        public static void StartDSLoop(TimeSpan loopTime)
        {
            if (dsTimer == null)
            {
                dsTimer = new Timer(s =>
                {
                    SimData.UpdateHalData(HalInData);
                    if (SimData.halNewDataSem != IntPtr.Zero)
                    {
                        HALSemaphore.giveMultiWait(SimData.halNewDataSem);
                    }
                }, null, loopTime, loopTime);
            }
        }

        public static void SetJoystickButton(int joystickNum, int buttonNum, bool state)
        {
            if (joystickNum < 0 || joystickNum >= HalInData["joysticks"].Length)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum), 
                    $"Joysticks must be between 0 and {HalInData["joysticks"].Length - 1}");
            }
            if (buttonNum < 1 || buttonNum >= HalInData["joysticks"][joystickNum]["buttons"].Length)
            {
                throw new ArgumentOutOfRangeException(nameof(buttonNum),
                    $"Button must be between 1 and {HalInData["joysticks"][joystickNum]["buttons"].Length - 1}");
            }
            HalInData["joysticks"][joystickNum]["buttons"][buttonNum] = state;
        }

        public static void SetJoystickAxis(int joystickNum, int axisNum, double value)
        {
            if (joystickNum < 0 || joystickNum >= HalInData["joysticks"].Length)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {HalInData["joysticks"].Length - 1}");
            }
            if (axisNum < 1 || axisNum >= HalInData["joysticks"][joystickNum]["axes"].Length)
            {
                throw new ArgumentOutOfRangeException(nameof(axisNum),
                    $"Axis must be between 1 and {HalInData["joysticks"][joystickNum]["axes"].Length - 1}");
            }
            HalInData["joysticks"][joystickNum]["axes"][axisNum] = (float)value;
        }

        
        public static void SetEnabledState(EnabledState state)
        {
            switch (state)
            {
                case EnabledState.Disabled:
                    HalInData["control"]["enabled"] = false;
                    HalInData["control"]["eStop"] = false;
                    break;
                case EnabledState.Enabled:
                    HalInData["control"]["enabled"] = true;
                    HalInData["control"]["eStop"] = false;
                    break;
                case EnabledState.EStopped:
                    HalInData["control"]["enabled"] = false;
                    HalInData["control"]["eStop"] = true;
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
                    HalInData["control"]["autonomous"] = true;
                    HalInData["control"]["test"] = false;
                    break;
                case RobotMode.Teleop:
                    if (prevState != RobotMode.Teleop)
                        SetEnabledState(EnabledState.Disabled);
                    HalInData["control"]["autonomous"] = false;
                    HalInData["control"]["test"] = false;
                    break;
                case RobotMode.Test:
                    if (prevState != RobotMode.Test)
                        SetEnabledState(EnabledState.Disabled);
                    HalInData["control"]["autonomous"] = false;
                    HalInData["control"]["test"] = true;
                    break;
                default:
                    SetEnabledState(EnabledState.Disabled);
                    HalInData["control"]["autonomous"] = false;
                    HalInData["control"]["test"] = false;
                    break;
            }
        }




    }
}
