using System;
using System.CodeDom;
using System.Threading;
using HAL.Base;

namespace HAL.Simulator
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
            JoystickCallback?.Invoke();
            if (SimData.s_halNewDataSem != IntPtr.Zero)
            {
                throw new ArgumentOutOfRangeException();
                // TODO:: FIX
                //SimulatorHAL.HALSemaphore.giveMultiWait(SimData.s_halNewDataSem);
            }
        }

        /// <summary>
        /// Assign to this callback to set the joysticks right before updating the robot data.
        /// </summary>
        public static Action JoystickCallback { get; set; } = null;

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
        /// Sets the number of buttons on the specific joystick.
        /// </summary>
        /// <param name="joystickNum">The joystick number.</param>
        /// <param name="buttonCount">The number of buttons to attach to the controller.</param>
        public static void SetJoystickButtonCount(int joystickNum, int buttonCount)
        {
            if (joystickNum < 0 || joystickNum >= SimData.DriverStation.Joysticks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {SimData.DriverStation.Joysticks.Count - 1}");
            }
            if (buttonCount < 0 || buttonCount > 32)
            {
                throw new ArgumentOutOfRangeException(nameof(buttonCount), "Must be between 0 and 32 buttons.");
            }
            SimData.DriverStation.Joysticks[joystickNum].NumButtons = buttonCount;
        }

        /// <summary>
        /// Sets the number of axes on the specific joystick.
        /// </summary>
        /// <param name="joystickNum">The joystick number.</param>
        /// <param name="axesCount">The number of axes to attach to the controller.</param>
        public static void SetJoystickAxesCount(int joystickNum, int axesCount)
        {
            if (joystickNum < 0 || joystickNum >= SimData.DriverStation.Joysticks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {SimData.DriverStation.Joysticks.Count - 1}");
            }
            if (axesCount < 0 || axesCount > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(axesCount), "Must be between 0 and 6 axes.");
            }
            SimData.DriverStation.Joysticks[joystickNum].NumAxes = axesCount;
        }

        /// <summary>
        /// Sets the number of povs on the specific joystick.
        /// </summary>
        /// <param name="joystickNum">The joystick number.</param>
        /// <param name="povCount">The number of povs to attach to the controller.</param>
        public static void SetJoystickPovCount(int joystickNum, int povCount)
        {
            if (joystickNum < 0 || joystickNum >= SimData.DriverStation.Joysticks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {SimData.DriverStation.Joysticks.Count - 1}");
            }
            if (povCount < 0 || povCount > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(povCount), "Must be between 0 and 12 Povs.");
            }
            SimData.DriverStation.Joysticks[joystickNum].NumPovs = povCount;
        }

        /// <summary>
        /// Set the value for a specific joystick button
        /// </summary>
        /// <param name="joystickNum">The joystick index [0..5]</param>
        /// <param name="buttonNum">The button number [1..n], set n using <see cref="SetJoystickButtonCount"/>.</param>
        /// <param name="value">The button value</param>
        public static void SetJoystickButton(int joystickNum, int buttonNum, bool value)
        {
            if (joystickNum < 0 || joystickNum >= SimData.DriverStation.Joysticks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {SimData.DriverStation.Joysticks.Count - 1}");
            }
            if (buttonNum < 1 || buttonNum > SimData.DriverStation.Joysticks[joystickNum].NumButtons)
            {
                throw new ArgumentOutOfRangeException(nameof(buttonNum),
                    $"Button must be between 1 and {SimData.DriverStation.Joysticks[joystickNum].NumButtons}");
            }
            SimData.DriverStation.Joysticks[joystickNum].Buttons[buttonNum] = value;
        }

        /// <summary>
        /// Set the value for a specific joystick axis
        /// </summary>
        /// <param name="joystickNum">The joystick index [0..5]</param>
        /// <param name="axisNum">The axis number [0..n], set n using <see cref="SetJoystickAxesCount"/>.</param>
        /// <param name="value">The joystick value from -1.0 to 1.0</param>
        public static void SetJoystickAxis(int joystickNum, int axisNum, double value)
        {
            if (joystickNum < 0 || joystickNum >= SimData.DriverStation.Joysticks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {SimData.DriverStation.Joysticks.Count - 1}");
            }
            if (axisNum < 0 || axisNum >= SimData.DriverStation.Joysticks[joystickNum].Axes.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(axisNum),
                    $"Axis must be between 0 and {SimData.DriverStation.Joysticks[joystickNum].NumAxes - 1}");
            }
            if (value > 1.0)
                value = 1.0;
            if (value < -1.0)
                value = -1.0;
            SimData.DriverStation.Joysticks[joystickNum].Axes[axisNum] = (float)value;
        }

        /// <summary>
        /// Sets the value for a specific joystick POV, in degrees
        /// </summary>
        /// <param name="joystickNum">The joystick index [0..6].</param>
        /// <param name="povNum">The pov number [0..n], set n using <see cref="SetJoystickPovCount"/>.</param>
        /// <param name="povValue">The pov value (-1 if not pressed, degrees otherwise)</param>
        public static void SetJoystickPov(int joystickNum, int povNum, int povValue)
        {
            if (joystickNum < 0 || joystickNum >= SimData.DriverStation.Joysticks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {SimData.DriverStation.Joysticks.Count - 1}");
            }
            if (povNum < 1 || povNum >= SimData.DriverStation.Joysticks[joystickNum].Povs.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(povNum),
                    $"POV must be between 0 and {SimData.DriverStation.Joysticks[joystickNum].Povs.Length - 1}");
            }
            if (povValue < -1)
                povValue = -1;
            if (povValue > 360)
                povValue = povValue % 360;
            SimData.DriverStation.Joysticks[joystickNum].Povs[povNum] = (short)povValue;
        }

        /// <summary>
        /// Sets the joystick name.
        /// </summary>
        /// <param name="joystickNum">The joystick number</param>
        /// <param name="name">The name to attach to the joystick.</param>
        public static void SetJoystickName(int joystickNum, string name)
        {
            if (joystickNum < 0 || joystickNum >= SimData.DriverStation.Joysticks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(joystickNum),
                    $"Joysticks must be between 0 and {SimData.DriverStation.Joysticks.Count - 1}");
            }
            SimData.DriverStation.Joysticks[joystickNum].Name = name;
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
                    SimData.DriverStation.ControlData.Enabled = false;
                    SimData.DriverStation.ControlData.EStop = false;
                    SimData.DriverStation.ControlData.DsAttached = true;
                    break;
                case EnabledState.Enabled:
                    SimData.DriverStation.ControlData.Enabled = true;
                    SimData.DriverStation.ControlData.EStop = false;
                    SimData.DriverStation.ControlData.DsAttached = true;
                    break;
                case EnabledState.EStopped:
                    SimData.DriverStation.ControlData.Enabled = false;
                    SimData.DriverStation.ControlData.EStop = true;
                    SimData.DriverStation.ControlData.DsAttached = true;
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
            if (SimData.DriverStation.ControlData.DsAttached)
            {
                prevState = RobotMode.Autonomous;
            }
            else if (SimData.DriverStation.ControlData.Test)
            {
                prevState = RobotMode.Test;
            }
            switch (mode)
            {
                case RobotMode.Autonomous:
                    if (prevState != RobotMode.Autonomous)
                        SetEnabledState(EnabledState.Disabled);
                    SimData.DriverStation.ControlData.Autonomous = true;
                    SimData.DriverStation.ControlData.Test = false;
                    break;
                case RobotMode.Teleop:
                    if (prevState != RobotMode.Teleop)
                        SetEnabledState(EnabledState.Disabled);
                    SimData.DriverStation.ControlData.Autonomous = false;
                    SimData.DriverStation.ControlData.Test = false;
                    break;
                case RobotMode.Test:
                    if (prevState != RobotMode.Test)
                        SetEnabledState(EnabledState.Disabled);
                    SimData.DriverStation.ControlData.Autonomous = false;
                    SimData.DriverStation.ControlData.Test = true;
                    break;
                default:
                    SetEnabledState(EnabledState.Disabled);
                    SimData.DriverStation.ControlData.Autonomous = false;
                    SimData.DriverStation.ControlData.Test = false;
                    break;
            }
        }

        public static void SetAllianceStation(HALAllianceStationID station)
        {
            SimData.DriverStation.AllianceStation = station;
        }

        public static void SetFMSAttached(bool attached)
        {
            SimData.DriverStation.ControlData.FmsAttached = attached;
        }

        public static void SetDSAttached(bool attached)
        {
            SimData.DriverStation.ControlData.DsAttached = attached;
        }

    }

}
