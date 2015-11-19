using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using HAL_Base;
using static WPILib.Timer;
using static HAL_Base.HAL;
using static HAL_Base.HALSemaphore;
using static HAL_Base.HAL.DriverStationConstants;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Provides access to the network communication data to/from the Driver Station.
    /// </summary>
    public class DriverStation : RobotState.Interface
    {
        //Enums
        /// <summary>
        /// Alliance value enum.
        /// </summary>
        public enum Alliance
        {
            Red,
            Blue,
            Invalid
        };

        //Private Fields
        private HALJoystickAxes[] m_joystickAxes = new HALJoystickAxes[JoystickPorts];
        private HALJoystickPOVs[] m_joystickPOVs = new HALJoystickPOVs[JoystickPorts];
        private HALJoystickButtons[] m_joystickButtons = new HALJoystickButtons[JoystickPorts];

        //Pointers to the semaphores to the HAL and FPGA
        private readonly object m_mutex;

        private readonly IntPtr m_packetDataAvailableMutex;
        private readonly IntPtr m_packetDataAvailableSem;
        private bool m_newControlData = false;

        //Driver station thread keep alive
        private bool m_threadKeepAlive = true;

        //User mode states
        private bool m_userInDisabled;
        private bool m_userInAutonomous;
        private bool m_userInTeleop;
        private bool m_userInTest;

        //Thread lock objects
        private readonly object m_lockObject = new object();

        //Reporting interval time limiters
        private const double JoystickUnpluggedMessageInterval = 1.0;
        private double m_nextMessageTime;

        //The singleton instance of the driver station
        private static DriverStation s_instance;

        /// <summary>
        /// Gets the instance of the driver station.
        /// </summary>
        public static DriverStation Instance => s_instance ?? (s_instance = new DriverStation());

        /// <summary>
        /// Driver Station constructor
        /// </summary>
        /// <remarks>This is normally created statically in the singleton instance.</remarks>
        protected DriverStation()
        {
            //Force all joysticks to have no value.
            for (int i = 0; i < JoystickPorts; i++)
            {
                m_joystickButtons[i].count = 0;
                m_joystickAxes[i].count = 0;
                m_joystickPOVs[i].count = 0;
            }

            //Initializes the HAL semaphores
            m_mutex = new object();
            

            m_packetDataAvailableMutex = InitializeMutexNormal();
            m_packetDataAvailableSem = InitializeMultiWait();
            HALSetNewDataSem(m_packetDataAvailableSem);


            //Starts the driver station thread in the background.
            var thread = new Thread(Task)
            {
                Priority = ThreadPriority.AboveNormal,
                IsBackground = true,
                Name = "FRCDriverStation"
            };
            thread.Start();
        }

        /// <summary>
        /// Stops the driver station thread
        /// </summary>
        public void Release() => m_threadKeepAlive = false;

        //The DS loop thread
        private void Task()
        {
            //The safety counter is used in order to implement motor safety
            int safetyCounter = 0;
            while (m_threadKeepAlive)
            {
                //Wait for new DS data, grab the newest data, and return the semaphore.
                TakeMultiWait(m_packetDataAvailableSem, m_packetDataAvailableMutex);
                GetData();
                try
                {
                    Monitor.Enter(m_mutex);
                    Monitor.PulseAll(m_mutex);
                }
                finally 
                {
                    Monitor.Exit(m_mutex);
                }
                

                //Every 4 loops (80ms) check all of the motors to make sure they have been updated
                if (++safetyCounter >= 4)
                {
                    MotorSafetyHelper.CheckMotors();
                    safetyCounter = 0;
                }

                //Report our program state.
                if (m_userInDisabled)
                    HALNetworkCommunicationObserveUserProgramDisabled();
                if (m_userInAutonomous)
                    HALNetworkCommunicationObserveUserProgramAutonomous();
                if (m_userInTeleop)
                    HALNetworkCommunicationObserveUserProgramTeleop();
                if (m_userInTest)
                    HALNetworkCommunicationObserveUserProgramTest();
            }
        }

        /// <summary>
        /// Wait for new data from the driver station.
        /// </summary>
        /// <param name="timeout">The timeout in ms</param>
        public void WaitForData(int timeout = Timeout.Infinite)
        {
            try
            {
                Monitor.Enter(m_mutex);
                Monitor.Wait(m_mutex, timeout);
            }
            finally 
            {
                Monitor.Exit(m_mutex);
            }
        }

        /// <summary>
        /// Grabs the newest Joystick data and stores it
        /// </summary>
        protected void GetData()
        {
            try
            {
                Monitor.Enter(m_mutex);
                for (byte stick = 0; stick < JoystickPorts; stick++)
                {
                    HALGetJoystickAxes(stick, ref m_joystickAxes[stick]);
                    HALGetJoystickPOVs(stick, ref m_joystickPOVs[stick]);
                    HALGetJoystickButtons(stick, ref m_joystickButtons[stick]);
                }
                m_newControlData = true;
            }
            finally
            {
                Monitor.Exit(m_mutex);
            }
        }

        /// <summary>
        /// Reads the battery voltage
        /// </summary>
        /// <returns>The battery voltage in Volts.</returns>
        public double GetBatteryVoltage()
        {
            int status = 0;
            double voltage = HALPower.GetVinVoltage(ref status);
            CheckStatus(status);
            return voltage;
        }

        /// <summary>
        /// Reports errors related to unplugged joysticks.
        /// </summary>
        /// <param name="message">The message to send.</param>
        private void ReportJoystickUnpluggedError(string message)
        {
            double currentTime = GetFPGATimestamp();
            if (currentTime > m_nextMessageTime)
            {
                ReportError(message, false);
                m_nextMessageTime = currentTime + JoystickUnpluggedMessageInterval;
            }
        }

        /// <summary>
        /// Gets the value of an axis on the joystick.
        /// </summary>
        /// <param name="stick">The joystick to read</param>
        /// <param name="axis">The axis to read.</param>
        /// <returns>The value of the axis from -1.0 to 1.0</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick or axis are out of range.</exception>
        public double GetStickAxis(int stick, int axis)
        {
            lock (m_joystickAxes)
            {
                if (stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
                }

                if (axis > m_joystickAxes[stick].count)
                {
                    if (axis >= MaxJoystickAxes)
                        throw new ArgumentOutOfRangeException(nameof(axis),
                            $"Joystick axis is out of range, should be between 0 and {m_joystickAxes[stick].count}");
                    else
                    {
                        ReportJoystickUnpluggedError("WARNING: Joystick axis " + axis + " on port " + stick +
                                                     " not available, check if controller is plugged in\n");
                    }
                    return 0.0;
                }

                int value = m_joystickAxes[stick].axes[axis];

                if (value < 0)
                {
                    return value / 128.0d;
                }
                else
                {
                    return value / 127.0d;
                }
            }

        }

        /// <summary>
        /// Returns the number of axes on a given joystick port.
        /// </summary>
        /// <param name="stick">The joystick to check</param>
        /// <returns>The number of axes on the joystick.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public int GetStickAxisCount(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick index is out of range, should be 0-{JoystickPorts}");
                }

                return m_joystickAxes[stick].count;
            }
        }

        /// <summary>
        /// Returns the state of a POV on the joystick.
        /// </summary>
        /// <param name="stick">The joystick to read</param>
        /// <param name="pov">The POV to read</param>
        /// <returns>The angle of the POV in degrees, or -1 if the POV is not pressed.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick or povs are out of range.</exception>
        public int GetStickPOV(int stick, int pov)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
                }

                if (pov < 0 || pov >= MaxJoystickPOVs)
                {
                    throw new ArgumentOutOfRangeException(nameof(pov),
                        $"Joystick POV is out of range, should be between 0 and {MaxJoystickPOVs}");
                }


                if (pov >= m_joystickPOVs[stick].count)
                {
                    ReportJoystickUnpluggedError("WARNING: Joystick POV " + pov + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return -1;
                }

                return m_joystickPOVs[stick].povs[pov];
            }
        }

        /// <summary>
        /// Returns the number of POVs on a given joystick port
        /// </summary>
        /// <param name="stick">The joystick port number</param>
        /// <returns>The number of POVs on the indicated joystick.</returns>.
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public int GetStickPOVCount(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
                }

                return m_joystickPOVs[stick].count;
            }
        }

        /// <summary>
        /// The state of the buttons on the joystick.
        /// </summary>
        /// <param name="stick">The joystick to read</param>
        /// <returns>The state of the buttons</returns>
        /// /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public int GetStickButtons(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
                }
                return (int)m_joystickButtons[stick].buttons;
            }
        }

        /// <summary>
        /// Gets the state of one joystick button. Button indexes begin at 1.
        /// </summary>
        /// <param name="stick">The joystick to read</param>
        /// <param name="button">The button index</param>
        /// <returns>The state of the joystick button.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public bool GetStickButton(int stick, byte button)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
                }

                if (button > m_joystickButtons[stick].count)
                {
                    ReportJoystickUnpluggedError("WARNING: Joystick Button " + button + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return false;
                }

                if (button <= 0)
                {
                    ReportJoystickUnpluggedError("ERROR: Button indexes begin at 1 in WPILib for C#\n");
                    return false;
                }
                return ((0x1 << (button - 1)) & m_joystickButtons[stick].buttons) != 0;
            }
        }

        /// <summary>
        /// Gets the number of buttons on a joystick.
        /// </summary>
        /// <param name="stick">The joystick port number</param>
        /// <returns>The number of buttons on the joystick.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public int GetStickButtonCount(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
                }

                return m_joystickButtons[stick].count;
            }
        }

        /// <summary>
        /// Gets if the joystick is an xbox controller.
        /// </summary>
        /// <param name="stick">The joystick port number</param>
        /// <returns>True if the joystick is an xbox controller.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public bool GetJoystickIsXbox(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
                }
                //TODO: Remove this when calling for descriptor on empty stick no longer crashes
                if (1 > m_joystickButtons[stick].count && 1 > m_joystickAxes[stick].count)
                {
                    ReportJoystickUnpluggedError("WARNING: Joystick on port " + stick +
                        " not available, check if controller is plugged in\n");
                    return false;
                }
                return HALGetJoystickIsXbox((byte)stick) == 1;
            }
        }

        /// <summary>
        /// Gets the type of joystick
        /// </summary>
        /// <param name="stick">The joystick port number</param>
        /// <returns>The joystick type</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public int GetJoystickType(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
                }
                //TODO: Remove this when calling for descriptor on empty stick
                if (1 > m_joystickButtons[stick].count && 1 > m_joystickAxes[stick].count)
                {
                    ReportJoystickUnpluggedError("WARNING: Joystick on port " + stick +
                        " not available, check if controller is plugged in\n");
                    return -1;
                }
                return HALGetJoystickType((byte)stick);
            }
        }

        /// <summary>
        /// Gets the name of the joystick.
        /// </summary>
        /// <param name="stick">The joystick port number</param>
        /// <returns>The joystick name</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public string GetJoystickName(int stick)
        {
            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                        $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }

            //TODO: Remove this when calling for descriptor on empty stick
            if (1 > m_joystickButtons[stick].count && 1 > m_joystickAxes[stick].count)
            {
                ReportJoystickUnpluggedError("WARNING: Joystick on port " + stick +
                    " not available, check if controller is plugged in\n");
                return "";
            }
            return HAL.HALGetJoystickName((byte)stick);
        }

        /// <summary>
        /// Gets a value indicating whether the Driver Station requires the robot to be enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                HALControlWord controlWord = GetControlWord();
                return controlWord.GetEnabled() && controlWord.GetDSAttached();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Driver Station requires the robot to be disabled.
        /// </summary>
        public bool Disabled => !Enabled;

        /// <summary>
        /// Gets a value indicating whether the Driver Station requires the robot to be
        /// running in autonomous mode.
        /// </summary>
        public bool Autonomous
        {
            get
            {
                HALControlWord controlWord = GetControlWord();
                return controlWord.GetAutonomous();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Driver Station requires the robot to be
        /// running in test mode.
        /// </summary>
        public bool Test
        {
            get
            {
                HALControlWord controlWord = GetControlWord();
                return controlWord.GetTest();
            }
        }

        //The implementation here is different, since calls to GetControlWord
        //take over 0.3ms. Instead of having to call it twice if we are in teleop
        //we only call it once.
        /// <summary>
        /// Gets a value indicating whether the Driver Station requires the robot to be
        /// running in operator-controlled mode.
        /// </summary>
        public bool OperatorControl // => !(Autonomous || Test);//
        {
            get
            {
                HALControlWord controlWord = GetControlWord();
                return !(controlWord.GetAutonomous() || controlWord.GetTest());
            }
        }

        /// <summary>
        /// Gets a value indicating whether the FPGA outputs are enabled.
        /// </summary>
        /// <remarks>The outputs may be disabled if the robot is disabled or
        /// e-stopped, the watchdog has expired, or if the roboRIO browns out</remarks>
        public bool SysActive
        {
            get
            {
                int status = 0;
                bool retVal = HALGetSystemActive(ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        /// <summary>
        /// Gets if the system is browned out.
        /// </summary>
        public bool BrownedOut
        {
            get
            {
                int status = 0;
                bool retval = HALGetBrownedOut(ref status);
                CheckStatus(status);
                return retval;
            }
        }

        /// <summary>
        /// Gets whether a new control packet from the driver station has arrived since
        /// the last time this was called.
        /// </summary>
        public bool NewControlData
        {
            get
            {
                try
                {
                    Monitor.Enter(m_mutex);
                    bool result = m_newControlData;
                    m_newControlData = false;
                    return result;
                }
                finally
                {
                    Monitor.Exit(m_mutex);
                }
            }
        }

        /// <summary>
        /// Gets the current alliance and station from the FMS.
        /// </summary>
        /// <returns>The current alliance</returns>
        public Alliance GetAlliance()
        {
            HALAllianceStationID allianceStationID = new HALAllianceStationID();

            HALGetAllianceStation(ref allianceStationID);

            switch (allianceStationID)
            {
                case HALAllianceStationID.HALAllianceStationID_red1:
                case HALAllianceStationID.HALAllianceStationID_red2:
                case HALAllianceStationID.HALAllianceStationID_red3:
                    return Alliance.Red;

                case HALAllianceStationID.HALAllianceStationID_blue1:
                case HALAllianceStationID.HALAllianceStationID_blue2:
                case HALAllianceStationID.HALAllianceStationID_blue3:
                    return Alliance.Blue;

                default:
                    return Alliance.Invalid;
            }
        }

        /// <summary>
        /// Gets the driver station number.
        /// </summary>
        /// <returns>The driver station number (1, 2 or 3)</returns>
        public int GetLocation()
        {
            HALAllianceStationID allianceStationID = new HALAllianceStationID();
            HALGetAllianceStation(ref allianceStationID);

            switch (allianceStationID)
            {
                case HALAllianceStationID.HALAllianceStationID_red1:
                case HALAllianceStationID.HALAllianceStationID_blue1:
                    return 1;

                case HALAllianceStationID.HALAllianceStationID_red2:
                case HALAllianceStationID.HALAllianceStationID_blue2:
                    return 2;

                case HALAllianceStationID.HALAllianceStationID_red3:
                case HALAllianceStationID.HALAllianceStationID_blue3:
                    return 3;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets if the FMS is attached.
        /// </summary>
        public bool FMSAttached => GetControlWord().GetFMSAttached();

        /// <summary>
        /// Gets if the DS is attached.
        /// </summary>
        public bool DSAtached => GetControlWord().GetDSAttached();

        /// <summary>
        /// Get the approximate match time.
        /// </summary>
        /// <remarks>The FMS does not send an official match
        /// time to the robots, but does send an approximate match time. The value will
        /// count down the time remaining in the current period (auto or teleop).
        /// <para/>
        /// Warning: This is not an official time(so it cannot be used to dispute ref
        /// calls or guarantee that a function will trigger before the match ends) The
        /// Practice Match function of the DS approximates the behavior seen on the
        /// field.</remarks>
        /// <returns>The time remaining in the current match period in seconds.</returns>
        public double GetMatchTime()
        {
            float temp = 0;
            HALGetMatchTime(ref temp);
            return temp;
        }

        /// <summary>
        /// Report an error to the driver station.
        /// </summary>
        /// <param name="error">The error to send</param>
        /// <param name="printTrace">If true, append stack trace to error string</param>
        public static void ReportError(string error, bool printTrace)
        {
            string errorString = error;
            if (printTrace)
            {
                errorString += " at ";
                var stacktrace = new StackTrace();
                var traces = stacktrace.GetFrames();
                errorString = traces?.Aggregate(errorString, (current, s) => current + (s + "\n"));
            }
            TextWriter errorWriter = Console.Error;
            errorWriter.WriteLine(errorString);


            HALControlWord controlWord = GetControlWord();
            if (controlWord.GetDSAttached())
            {
                HALSetErrorData(errorString, 0);
            }
        }


        public void InDisabled(bool entering)
        {
            m_userInDisabled = entering;
        }


        public void InAutonomous(bool entering)
        {
            m_userInAutonomous = entering;
        }

        public void InOperatorControl(bool entering)
        {
            m_userInTeleop = entering;
        }

        public void InTest(bool entering)
        {
            m_userInTest = entering;
        }

    }
}
