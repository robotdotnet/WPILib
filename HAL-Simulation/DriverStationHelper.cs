using System;
using System.Threading;
using HAL_Base;
using static HAL_Simulator.SimData;

namespace HAL_Simulator
{
    /// <summary>
    /// This class is useful to emulate a driver station correctly in the simulator.
    /// </summary>
    public static class DriverStationHelper
    {
        
        /// The Robot Modes
        public enum RobotMode
        {
            /// Autonomous
            Autonomous,
            /// Teleoperated
            Teleop,
            /// Test
            Test,
        }

        /// <summary>
        /// The robot enabled state
        /// </summary>
        public enum EnabledState
        {
            /// Enabled
            Enabled,
            /// Disabled
            Disabled,
            /// Estopped
            EStopped,
        }

        private static Timer s_dsTimer;

        internal static void UpdateData()
        {
            //UpdateHalData(HalDSData);
            if (HALNewDataSem != IntPtr.Zero)
            {
                HALSemaphore.giveMultiWait(HALNewDataSem);
            }
        }

        /// <summary>
        /// Start the driver station loop at the default 20ms interval
        /// </summary>
        public static void StartDSLoop()
        {
            StartDSLoop(TimeSpan.FromMilliseconds(20));
        }

        /// <summary>
        /// Start the driver station loop at the requested interval
        /// </summary>
        /// <param name="loopTime">The loop interval in ms</param>
        public static void StartDSLoop(int loopTime)
        {
            StartDSLoop(TimeSpan.FromMilliseconds(loopTime));
        }

        /// <summary>
        /// Start the driver station loop at the requested interval
        /// </summary>
        /// <param name="loopTime">The loop interval</param>
        public static void StartDSLoop(TimeSpan loopTime)
        {
            if (s_dsTimer == null)
            {
                s_dsTimer = new Timer(s =>
                {
                    UpdateData();
                }, null, loopTime, loopTime);
            }
        }

        /// <summary>
        /// Stop the driver station loop
        /// </summary>
        public static void StopDSLoop()
        {
            s_dsTimer?.Dispose();
            s_dsTimer = null;
        }

        /// <summary>
        /// Set the value for a specific joystick button
        /// </summary>
        /// <param name="joystickNum">The joystick index</param>
        /// <param name="buttonNum">The button number</param>
        /// <param name="value">The button value</param>
        public static void SetJoystickButton(int joystickNum, int buttonNum, bool value)
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
            HalDSData["joysticks"][joystickNum]["buttons"][buttonNum] = value;
        }

        /// <summary>
        /// Set the value for a specific joystick axis
        /// </summary>
        /// <param name="joystickNum">The joystick index</param>
        /// <param name="axisNum">The axis number</param>
        /// <param name="value">The joystick value from -1.0 to 1.0</param>
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
            if (value > 1.0)
                value = 1.0;
            if (value < -1.0)
                value = -1.0;
            HalDSData["joysticks"][joystickNum]["axes"][axisNum] = (float)value;
        }

        /// <summary>
        /// Sets the value for a specific joystick POV, in degrees
        /// </summary>
        /// <param name="joystickNum">The joystick index</param>
        /// <param name="povNum">The pov number</param>
        /// <param name="povValue">The pov value (-1 if not pressed, degrees otherwise)</param>
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
            if (povValue < -1)
                povValue = -1;
            if (povValue > 360)
                povValue = povValue % 360;
            HalDSData["joysticks"][joystickNum]["povs"][povNum] = (short)povValue;
        }

        public static void SetJoystickName(int joystickNum, string name)
        {
            if (joystickNum < 0 || joystickNum >= HalDSData["joysticks"].Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {HalDSData["joysticks"].Count - 1}");
            }
            HalDSData["joysticks"][joystickNum]["name"] = name;
        }



        /// <summary>
        /// Sets the robot enabled state
        /// </summary>
        /// <param name="state">The state to set</param>
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

        /// <summary>
        /// Sets the robot mode
        /// </summary>
        /// <param name="mode">The robot mode to set</param>
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

        public static void SetAllianceStation(HALAllianceStationID station)
        {
            HalDSData["alliance_station"] = station;
        }

        public static void SetFMSAttached(bool attached)
        {
            HalDSData["control"]["fms_attached"] = attached;
        }

        public static void SetDSAttached(bool attached)
        {
            HalDSData["control"]["ds_attached"] = attached;
        }
        
    }
   
}
