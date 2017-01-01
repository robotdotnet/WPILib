using System;
using System.IO;
using System.Threading;
using HAL.Base;
using static WPILib.Timer;
using static HAL.Base.HAL;
using static HAL.Base.HAL.DriverStationConstants;
using static WPILib.Utility;
using static HAL.Base.HALDriverStation;
using HALPower = HAL.Base.HALPower;
using System.Runtime.CompilerServices;
using System.Text;

namespace WPILib
{
    /// <summary>
    /// Provides access to the network communication data to/from the Driver Station.
    /// </summary>
    public class DriverStation
    {
        //Enums
        /// <summary>
        /// Alliance value enum.
        /// </summary>
        public enum Alliance
        {
            /// <summary>
            /// The red alliance
            /// </summary>
            Red,
            /// <summary>
            /// The blue alliance
            /// </summary>
            Blue,
            /// <summary>
            /// The alliance is unknown.
            /// </summary>
            Invalid
        };

        //Private Fields
        private HALJoystickAxes[] m_joystickAxes = new HALJoystickAxes[JoystickPorts];
        // ReSharper disable InconsistentNaming
        private HALJoystickPOVs[] m_joystickPOVs = new HALJoystickPOVs[JoystickPorts];
        private HALJoystickButtons[] m_joystickButtons = new HALJoystickButtons[JoystickPorts];
        private HALJoystickDescriptor[] m_joystickDescriptors = new HALJoystickDescriptor[JoystickPorts];

        private HALJoystickAxes[] m_joystickAxesCache = new HALJoystickAxes[JoystickPorts];
        private HALJoystickPOVs[] m_joystickPOVsCache = new HALJoystickPOVs[JoystickPorts];
        private HALJoystickButtons[] m_joystickButtonsCache = new HALJoystickButtons[JoystickPorts];
        private HALJoystickDescriptor[] m_joystickDescriptorsCache = new HALJoystickDescriptor[JoystickPorts];
        // ReSharper restore InconsistentNaming

        // Internal Driver Station thread
        private Thread m_dsThread;
        private volatile bool m_isRunning = false;

        // WPILib WaitForData control variables
        private bool m_waitForDataPredicate = false;
        //std::condition_variable_any m_waitForDataCond;
        private readonly object m_waitForDataMutex = new object();

        private int m_newControlData = 0;

        private readonly object m_joystickDataMutex = new object();

        // Robot state status variables
        private bool m_userInDisabled = false;
        private bool m_userInAutonomous = false;
        private bool m_userInTeleop = false;
        private bool m_userInTest = false;

        HALControlWord m_controlWordCache;
        DateTime m_lastControlWordUpdate;
        private readonly object m_controlWordMutex = new object();

        private const double JoystickUnpluggedMessageInterval = 1.0;

        private double m_nextMessageTime = 0;

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
                m_joystickDescriptors[i] = new HALJoystickDescriptor();
                m_joystickDescriptors[i].isXbox = 0;
                m_joystickDescriptors[i].type = 0xFF;
                m_joystickDescriptors[i].name.byte0 = 0;

                m_joystickButtonsCache[i].count = 0;
                m_joystickAxesCache[i].count = 0;
                m_joystickPOVsCache[i].count = 0;
                m_joystickDescriptorsCache[i] = new HALJoystickDescriptor();
                m_joystickDescriptorsCache[i].isXbox = 0;
                m_joystickDescriptorsCache[i].type = 0xFF;
                m_joystickDescriptorsCache[i].name.byte0 = 0;
            }

            m_controlWordCache = new HALControlWord();

            m_lastControlWordUpdate = DateTime.MinValue;


            //Starts the driver station thread in the background.
            m_dsThread = new Thread(Task)
            {
                IsBackground = true,
                Name = "FRCDriverStation"
            };
            m_dsThread.Start();
        }

        /// <summary>
        /// Stops the driver station thread
        /// </summary>
        public void Release() => m_isRunning = false;

        //The DS loop thread
        private void Task()
        {
            m_isRunning = true;
            //The safety counter is used in order to implement motor safety
            int safetyCounter = 0;
            while (m_isRunning)
            {
                //Wait for new DS data, grab the newest data, and return the semaphore.
                HAL_WaitForDSData();
                GetData();

                // notify IsNewControlData variables
                Interlocked.Exchange(ref m_newControlData, 1);

                // notify WaitForData block
                lock (m_waitForDataMutex)
                {
                    m_waitForDataPredicate = true;
                    Monitor.PulseAll(m_waitForDataMutex);
                }


                //Every 4 loops (80ms) check all of the motors to make sure they have been updated
                if (++safetyCounter >= 4)
                {
                    MotorSafetyHelper.CheckMotors();
                    safetyCounter = 0;
                }

                //Report our program state.
                if (m_userInDisabled)
                    HAL_ObserveUserProgramDisabled();
                if (m_userInAutonomous)
                    HAL_ObserveUserProgramAutonomous();
                if (m_userInTeleop)
                    HAL_ObserveUserProgramAutonomous();
                if (m_userInTest)
                    HAL_ObserveUserProgramTest();
            }
        }

        /// <summary>
        /// Wait for new data from the driver station.
        /// </summary>
        /// <param name="timeout">The timeout in seconds</param>
        public bool WaitForData(double timeout = 0)
        {
            ulong startTime = Utility.GetFPGATime();
            ulong timeoutMicros = (ulong)(timeout * 1000000);

            lock (m_waitForDataMutex)
                try
                {
                    while (!m_waitForDataPredicate)
                    {
                        if (timeout > 0)
                        {
                            ulong now = Utility.GetFPGATime();
                            if (now < startTime + timeoutMicros)
                            {
                                // We still have time to wait
                                bool signaled = Monitor.Wait(m_waitForDataMutex, (int)((startTime + timeoutMicros - now) / 1000));
                                if (!signaled)
                                {
                                    // Return false if a timeout happened
                                    return false;
                                }
                            }
                            else
                            {
                                // Time has elapsed.
                                return false;
                            }
                        }
                        else
                        {
                            Monitor.Wait(m_waitForDataMutex);
                        }
                    }
                    m_waitForDataPredicate = false;
                    // Return true if we have received a proper signal
                    return true;
                }
                catch (ThreadInterruptedException)
                {
                    // return false on a thread interrupt
                    return false;
                }
        }

        /// <summary>
        /// Grabs the newest Joystick data and stores it
        /// </summary>
        protected void GetData()
        {
            for (byte stick = 0; stick < JoystickPorts; stick++)
            {
                HAL_GetJoystickAxes(stick, ref m_joystickAxesCache[stick]);
                HAL_GetJoystickPOVs(stick, ref m_joystickPOVsCache[stick]);
                HAL_GetJoystickButtons(stick, ref m_joystickButtonsCache[stick]);
                HAL_GetJoystickDescriptor(stick, ref m_joystickDescriptorsCache[stick]);
            }

            HALControlWord controlWord;
            UpdateControlWord(true, out controlWord);

            lock (m_joystickDataMutex)
            { 

                HALJoystickAxes[] currentAxes = m_joystickAxes;
                m_joystickAxes = m_joystickAxesCache;
                m_joystickAxesCache = currentAxes;

                HALJoystickButtons[] currentButtons = m_joystickButtons;
                m_joystickButtons = m_joystickButtonsCache;
                m_joystickButtonsCache = currentButtons;

                // ReSharper disable once InconsistentNaming
                HALJoystickPOVs[] currentPOVs = m_joystickPOVs;
                m_joystickPOVs = m_joystickPOVsCache;
                m_joystickPOVsCache = currentPOVs;

                HALJoystickDescriptor[] currentDescriptor = m_joystickDescriptors;
                m_joystickDescriptors = m_joystickDescriptorsCache;
                m_joystickDescriptorsCache = currentDescriptor;
            }
        }

        /// <summary>
        /// Reads the battery voltage
        /// </summary>
        /// <returns>The battery voltage in Volts.</returns>
        public double GetBatteryVoltage()
        {
            int status = 0;
            double voltage = HALPower.HAL_GetVinVoltage(ref status);
            CheckStatus(status);
            return voltage;
        }

        /// <summary>
        /// Reports errors related to unplugged joysticks.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="memberName">The Member Name</param>
        /// <param name="filePath">The File Path</param>
        /// <param name="lineNumber">The Line Number</param>
        private void ReportJoystickUnpluggedError(string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            double currentTime = GetFPGATimestamp();
            if (currentTime > m_nextMessageTime)
            {
                ReportError(message, false, 1, memberName, filePath, lineNumber);
                m_nextMessageTime = currentTime + JoystickUnpluggedMessageInterval;
            }
        }

        /// <summary>
        /// Reports errors related to unplugged joysticks.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="memberName">The Member Name</param>
        /// <param name="filePath">The File Path</param>
        /// <param name="lineNumber">The Line Number</param>
        private void ReportJoystickUnpluggedWarning(string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            double currentTime = GetFPGATimestamp();
            if (currentTime > m_nextMessageTime)
            {
                ReportWarning(message, false, 1, memberName, filePath, lineNumber);
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
            if (stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }

            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);

                if (axis > m_joystickAxes[stick].count)
                {
                    Monitor.Exit(m_joystickDataMutex);
                    lockEntered = false;

                    if (axis >= MaxJoystickAxes)
                        throw new ArgumentOutOfRangeException(nameof(axis),
                            $"Joystick axis is out of range, should be between 0 and {m_joystickAxes[stick].count}");
                    else
                    {
                        ReportJoystickUnpluggedWarning("Joystick axis " + axis + " on port " + stick +
                                                     " not available, check if controller is plugged in\n");
                    }
                    return 0.0;
                }

                return m_joystickAxes[stick].axes[axis];
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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
            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick index is out of range, should be 0-{JoystickPorts}");
            }

            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);
                return m_joystickAxes[stick].count;
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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

            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);

                if (pov >= m_joystickPOVs[stick].count)
                {
                    Monitor.Exit(m_joystickDataMutex);
                    lockEntered = false;
                    ReportJoystickUnpluggedWarning("Joystick POV " + pov + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return -1;
                }

                return m_joystickPOVs[stick].povs[pov];
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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
            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }
            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);

                return m_joystickPOVs[stick].count;
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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

            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }
            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);
                return (int)m_joystickButtons[stick].buttons;
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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
        public bool GetStickButton(int stick, int button)
        {
            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }
            if (button <= 0)
            {
                ReportJoystickUnpluggedError("Button indexes begin at 1 in WPILib for C#\n");
                return false;
            }

            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);

                if (button > m_joystickButtons[stick].count)
                {
                    Monitor.Exit(m_joystickDataMutex);
                    lockEntered = false;
                    ReportJoystickUnpluggedWarning("Joystick Button " + button + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return false;
                }


                return ((0x1 << (button - 1)) & m_joystickButtons[stick].buttons) != 0;
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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

            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }
            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);
                return m_joystickButtons[stick].count;
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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
            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }
            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);
                return m_joystickDescriptors[stick].isXbox != 0;
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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
            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }
            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);
                return m_joystickDescriptors[stick].type;
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
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

            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);
                return m_joystickDescriptors[stick].name.ToString();
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
            }
        }

        /// <summary>
        /// Gets the type of the axis on a given joystick port and axis
        /// </summary>
        /// <param name="stick">The joystick port number</param>
        /// <param name="axis">The joystick axis number</param>
        /// <returns>The joystick axis type</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the stick is out of range.</exception>
        public int GetJoystickAxisType(int stick, int axis)
        {
            if (stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }

            bool lockEntered = false;
            try
            {
                Monitor.Enter(m_joystickDataMutex, ref lockEntered);

                if (axis > m_joystickAxes[stick].count)
                {
                    Monitor.Exit(m_joystickDataMutex);
                    lockEntered = false;

                    if (axis >= MaxJoystickAxes)
                        throw new ArgumentOutOfRangeException(nameof(axis),
                            $"Joystick axis is out of range, should be between 0 and {m_joystickAxes[stick].count}");
                    else
                    {
                        ReportJoystickUnpluggedWarning("Joystick axis " + axis + " on port " + stick +
                                                     " not available, check if controller is plugged in\n");
                    }
                    return -1;
                }

                return m_joystickDescriptors[stick].axisTypes[axis];
            }
            finally
            {
                if (lockEntered) Monitor.Exit(m_joystickDataMutex);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Driver Station requires the robot to be enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                HALControlWord word;
                UpdateControlWord(false, out word);
                return word.GetEnabled() && word.GetDSAttached();
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
                HALControlWord word;
                UpdateControlWord(false, out word);
                return word.GetAutonomous();
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
                HALControlWord word;
                UpdateControlWord(false, out word);
                return word.GetTest();
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
                HALControlWord word;
                UpdateControlWord(false, out word);
                return !word.GetAutonomous() && !word.GetTest();
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
                bool retVal = HAL_GetSystemActive(ref status);
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
                bool retval = HAL_GetBrownedOut(ref status);
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
                return Interlocked.Exchange(ref m_newControlData, 0) != 0;
            }
        }

        /// <summary>
        /// Gets the current alliance and station from the FMS.
        /// </summary>
        /// <returns>The current alliance</returns>
        public Alliance GetAlliance()
        {
            int status = 0;
            HALAllianceStationID allianceStationID = HAL_GetAllianceStation(ref status);
            if (status != 0) return Alliance.Invalid;

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
            int status = 0;
            HALAllianceStationID allianceStationID = HAL_GetAllianceStation(ref status);
            if (status != 0) return 0;

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
        public bool FMSAttached
        {
            get
            {
                HALControlWord word;
                UpdateControlWord(false, out word);
                return word.GetFMSAttached();
            }
        }

        /// <summary>
        /// Gets if the DS is attached.
        /// </summary>
        public bool DSAtached
        {
            get
            {
                HALControlWord word;
                UpdateControlWord(false, out word);
                return word.GetDSAttached();
            }
        }

        private void UpdateControlWord(bool force, out HALControlWord controlWord)
        {
            DateTime now = DateTime.UtcNow;
            lock (m_controlWordMutex)
            {
                if (now - m_lastControlWordUpdate > TimeSpan.FromMilliseconds(50) || force)
                {
                    HAL_GetControlWord(ref m_controlWordCache);
                    m_lastControlWordUpdate = now;
                }
                controlWord = m_controlWordCache;
            }
        }

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
            int status = 0;
            double time = HAL_GetMatchTime(ref status);
            if (status != 0) return 0.0;
            return time;
        }

        /// <summary>
        /// Reports a warning to the Driver Station
        /// </summary>
        /// <param name="error"></param>
        /// <param name="printTrace"></param>
        /// <param name="errorCode"></param>
        /// <param name="memberName"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        public static void ReportWarning(string error, bool printTrace, int errorCode = 1, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            ReportErrorImpl(false, errorCode, error, printTrace, memberName, filePath, lineNumber);
        }

        /// <summary>
        /// Reports an error to the Driver Station
        /// </summary>
        /// <param name="error"></param>
        /// <param name="printTrace"></param>
        /// <param name="errorCode"></param>
        /// <param name="memberName"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        public static void ReportError(string error, bool printTrace, int errorCode = 1, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            ReportErrorImpl(true, errorCode, error, printTrace, memberName, filePath, lineNumber);
        }

        /// <summary>
        /// Report an error to the driver station.
        /// </summary>
        /// <param name="isError">True is error, fasle if warning</param>
        /// <param name="code">The code linked to the error.</param>
        /// <param name="error">The error to send</param>
        /// <param name="printTrace">If true, append stack trace to error string</param>
        /// <param name="filePath">The file path</param>
        /// <param name="lineNumber">The line number</param>
        /// <param name="memberName">The member name</param>
        private static void ReportErrorImpl(bool isError, int code, string error, bool printTrace, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            string locString = $"WPILib: {memberName}, File: {filePath}, Line: {lineNumber}\n";
            var stackTrace = Environment.StackTrace;
            string[] stackSplit = stackTrace.Split('\n');
            bool haveLoc = false;

            for (int i = 3; i < stackSplit.Length; i++)
            {
                string loc = stackSplit[i];
                if (!haveLoc && !loc.Contains("at WPILib"))
                {
                    loc = loc.Substring(loc.IndexOf("at", StringComparison.Ordinal) + 3);
                    locString = locString + "Caller: " + loc + "\n";
                    haveLoc = true;
                }
            }

            string trace = string.Empty;
            if (printTrace)
            {
                trace = stackTrace + "\n";
            }

            HAL_SendError(isError, code, false, error, locString, trace, true);
        }

        /// <summary>
        /// Only to be used to tell the Driver Station what code you claim to be executing
        /// for diagnostic purposes only.
        /// </summary>
        /// <param name="entering">If true, starting disabled code; if false, leaving disabled code.</param>
        public void InDisabled(bool entering)
        {
            m_userInDisabled = entering;
        }

        /// <summary>
        /// Only to be used to tell the Driver Station what code you claim to be executing
        /// for diagnostic purposes only.
        /// </summary>
        /// <param name="entering">If true, starting autonomous code; if false, leaving autonomous code.</param>
        public void InAutonomous(bool entering)
        {
            m_userInAutonomous = entering;
        }
        /// <summary>
        /// Only to be used to tell the Driver Station what code you claim to be executing
        /// for diagnostic purposes only.
        /// </summary>
        /// <param name="entering">If true, starting teleop code; if false, leaving teleop code.</param>
        public void InOperatorControl(bool entering)
        {
            m_userInTeleop = entering;
        }
        /// <summary>
        /// Only to be used to tell the Driver Station what code you claim to be executing
        /// for diagnostic purposes only.
        /// </summary>
        /// <param name="entering">If true, starting test code; if false, leaving test code.</param>
        public void InTest(bool entering)
        {
            m_userInTest = entering;
        }

    }
}
