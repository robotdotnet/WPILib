using System;
using System.IO;
using System.Threading;
using HAL.Base;
using static WPILib.Timer;
using static HAL.Base.HAL;
using static HAL.Base.HALSemaphore;
using static HAL.Base.HAL.DriverStationConstants;
using static WPILib.Utility;
using HALPower = HAL.Base.HALPower;
using System.Runtime.CompilerServices;
using System.Text;

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
        private HALJoystickPOVs[] m_joystickPOVs = new HALJoystickPOVs[JoystickPorts];
        private HALJoystickButtons[] m_joystickButtons = new HALJoystickButtons[JoystickPorts];
        private HALJoystickDescriptor[] m_joystickDescriptors = new HALJoystickDescriptor[JoystickPorts];

        private HALJoystickAxes[] m_joystickAxesCache = new HALJoystickAxes[JoystickPorts];
        private HALJoystickPOVs[] m_joystickPOVsCache = new HALJoystickPOVs[JoystickPorts];
        private HALJoystickButtons[] m_joystickButtonsCache = new HALJoystickButtons[JoystickPorts];
        private HALJoystickDescriptor[] m_joystickDescriptorsCache = new HALJoystickDescriptor[JoystickPorts];

        //Pointers to the semaphores to the HAL and FPGA
        private readonly object m_dataSem;

        private readonly IntPtr m_packetDataAvailableMutex;
        private readonly IntPtr m_packetDataAvailableSem;

        //New Control Data Fast Semaphore Lock
        private readonly object m_newControlDataLock = new object();
        private bool m_newControlData = false;

        //Driver station thread keep alive
        private bool m_threadKeepAlive = true;

        //User mode states
        private bool m_userInDisabled;
        private bool m_userInAutonomous;
        private bool m_userInTeleop;
        private bool m_userInTest;

        //Thread lock objects
        private readonly ReaderWriterLockSlim m_readWriteLock;

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

            m_readWriteLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);


            //Initializes the HAL semaphores
            m_dataSem = new object();


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
                    Monitor.Enter(m_dataSem);
                    Monitor.PulseAll(m_dataSem);
                }
                finally
                {
                    Monitor.Exit(m_dataSem);
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
                Monitor.Enter(m_dataSem);
                Monitor.Wait(m_dataSem, timeout);
            }
            finally
            {
                Monitor.Exit(m_dataSem);
            }
        }

        /// <summary>
        /// Grabs the newest Joystick data and stores it
        /// </summary>
        protected void GetData()
        {
            for (byte stick = 0; stick < JoystickPorts; stick++)
            {
                HALGetJoystickAxes(stick, ref m_joystickAxesCache[stick]);
                HALGetJoystickPOVs(stick, ref m_joystickPOVsCache[stick]);
                HALGetJoystickButtons(stick, ref m_joystickButtonsCache[stick]);
                HALGetJoystickDescriptor(stick, ref m_joystickDescriptorsCache[stick]);
            }
            bool lockEntered = false;
            try
            {
                m_readWriteLock.EnterWriteLock();
                lockEntered = true;

                HALJoystickAxes[] currentAxes = m_joystickAxes;
                m_joystickAxes = m_joystickAxesCache;
                m_joystickAxesCache = currentAxes;

                HALJoystickButtons[] currentButtons = m_joystickButtons;
                m_joystickButtons = m_joystickButtonsCache;
                m_joystickButtonsCache = currentButtons;

                HALJoystickPOVs[] currentPOVs = m_joystickPOVs;
                m_joystickPOVs = m_joystickPOVsCache;
                m_joystickPOVsCache = currentPOVs;

                HALJoystickDescriptor[] currentDescriptor = m_joystickDescriptors;
                m_joystickDescriptors = m_joystickDescriptorsCache;
                m_joystickDescriptorsCache = currentDescriptor;
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitWriteLock();
            }
            lock (m_newControlDataLock)
            {
                m_newControlData = true;
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
        /// <param name="memberName">The Member Name</param>
        /// <param name="filePath">The File Path</param>
        /// <param name="lineNumber">The Line Number</param>
        private void ReportJoystickUnpluggedError(string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            double currentTime = GetFPGATimestamp();
            if (currentTime > m_nextMessageTime)
            {
                ReportError(message, false, memberName, filePath, lineNumber);
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;

                if (axis > m_joystickAxes[stick].count)
                {
                    m_readWriteLock.ExitReadLock();
                    lockEntered = false;

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
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;
                return m_joystickAxes[stick].count;
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;

                if (pov >= m_joystickPOVs[stick].count)
                {
                    m_readWriteLock.ExitReadLock();
                    lockEntered = false;
                    ReportJoystickUnpluggedError("WARNING: Joystick POV " + pov + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return -1;
                }

                return m_joystickPOVs[stick].povs[pov];
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;

                return m_joystickPOVs[stick].count;
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;
                return (int)m_joystickButtons[stick].buttons;
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
            if (stick < 0 || stick >= JoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick),
                    $"Joystick Index is out of range, should be 0-{JoystickPorts}");
            }
            if (button <= 0)
            {
                ReportJoystickUnpluggedError("ERROR: Button indexes begin at 1 in WPILib for C#\n");
                return false;
            }

            bool lockEntered = false;
            try
            {
                m_readWriteLock.EnterReadLock();
                lockEntered = true;

                if (button > m_joystickButtons[stick].count)
                {
                    m_readWriteLock.ExitReadLock();
                    lockEntered = false;
                    ReportJoystickUnpluggedError("WARNING: Joystick Button " + button + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return false;
                }


                return ((0x1 << (button - 1)) & m_joystickButtons[stick].buttons) != 0;
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;
                return m_joystickButtons[stick].count;
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;
                return m_joystickDescriptors[stick].isXbox != 0;
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;
                return m_joystickDescriptors[stick].type;
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
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
                m_readWriteLock.EnterReadLock();
                lockEntered = true;
                return m_joystickDescriptors[stick].name.ToString();
            }
            finally
            {
                if (lockEntered) m_readWriteLock.ExitReadLock();
            }
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
                lock (m_newControlDataLock)
                {
                    bool result = m_newControlData;
                    m_newControlData = false;
                    return result;
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
        /// <param name="filePath">The file path</param>
        /// <param name="lineNumber">The line number</param>
        /// <param name="memberName">The member name</param>
        public static void ReportError(string error, bool printTrace, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(error);
            builder.AppendLine();
            builder.Append($" Caller: {memberName}, File: {filePath}, Line: {lineNumber}\n");
            if (printTrace)
            {

                var stacktrace = Environment.StackTrace;
                builder.Append(stacktrace);
            }
            TextWriter errorWriter = Console.Error;
            errorWriter.WriteLine(builder.ToString());


            HALControlWord controlWord = GetControlWord();
            if (controlWord.GetDSAttached())
            {
                HALSetErrorData(builder.ToString(), 0);
            }
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
