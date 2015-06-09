using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using HAL_Base;
using static WPILib.Timer;
using static HAL_Base.HAL;
using static HAL_Base.HALSemaphore;

namespace WPILib
{
    public class DriverStation : RobotState.Interface
    {
        //Constants
        public const int JoystickPorts = 6;
        public const int MaxJoystickAxes = 12;
        public const int MaxJoystickPOVs = 12;

        //Enums
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
        private IntPtr m_newControlData;
        private IntPtr m_packetDataAvailableMultiWait;
        private IntPtr m_packetDataAvailableMutex;
        private IntPtr m_waitForDataSem;
        private IntPtr m_waitForDataMutex;
        private bool m_threadKeepAlive = true;
        private bool m_userInDisabled;
        private bool m_userInAutonomous;
        private bool m_userInTeleop;
        private bool m_userInTest;
        private double m_nextMessageTime;

        private object m_lockObject = new object();

        private const double JoystickUnpluggedMessageInterval = 1.0;

        //Public Fields


        public static DriverStation Instance { get; } = new DriverStation();


        protected DriverStation()
        {
            for (int i = 0; i < JoystickPorts; i++)
            {
                m_joystickButtons[i].count = 0;
                m_joystickAxes[i].count = 0;
                m_joystickPOVs[i].count = 0;
            }


            m_packetDataAvailableMultiWait = InitializeMultiWait();
            m_newControlData = InitializeSemaphore(0);

            m_waitForDataSem = InitializeMultiWait();
            m_waitForDataMutex = InitializeMutexNormal();


            m_packetDataAvailableMutex = InitializeMutexNormal();
            HALSetNewDataSem(m_packetDataAvailableMultiWait);



            var thread = new Thread(Task) { Priority = ThreadPriority.Highest, IsBackground = true };
            thread.Start();
        }

        public void Release() => m_threadKeepAlive = false;

        private void Task()
        {
            int safetyCounter = 0;
            while (m_threadKeepAlive)
            {
                TakeMultiWait(m_packetDataAvailableMultiWait, m_packetDataAvailableMutex, 0);
                GetData();
                GiveMultiWait(m_waitForDataSem);
                if (++safetyCounter >= 4)
                {
                    MotorSafetyHelper.CheckMotors();
                    safetyCounter = 0;
                }
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

        public void WaitForData(long timeout = 0) => TakeMultiWait(m_waitForDataSem, m_waitForDataMutex, -1);

        protected void GetData()
        {
            for (byte stick = 0; stick < JoystickPorts; stick++)
            {
                HALGetJoystickAxes(stick, ref m_joystickAxes[stick]);
                HALGetJoystickPOVs(stick, ref m_joystickPOVs[stick]);
                HALGetJoystickButtons(stick, ref m_joystickButtons[stick]);
            }
            GiveSemaphore(m_newControlData);
        }

        public double GetBatteryVoltage()
        {
            int status = 0;
            return HALPower.GetVinVoltage(ref status);
        }

        private void ReportJoystickUnpluggedError(string message)
        {
            double currentTime = FPGATimestamp;
            if (currentTime > m_nextMessageTime)
            {
                ReportError(message, false);
                m_nextMessageTime = currentTime + JoystickUnpluggedMessageInterval;
            }
        }

        public double GetStickAxis(int stick, int axis)
        {
            lock (m_joystickAxes)
            {
                if (stick >= JoystickPorts)
                {
                    throw new SystemException("Joystick Index is out of range");
                }

                if (axis > m_joystickAxes[stick].count)
                {
                    if (axis >= MaxJoystickAxes)
                        throw new SystemException("Joystick index is out of range, should be 0-5");
                    else
                    {
                        ReportJoystickUnpluggedError("WARNING: Joystick axis " + axis + " on port " + stick +
                                                     " not available, check if controller is plugged in\n");
                    }
                    return 0.0;
                }

                int value = m_joystickAxes[stick].axes[axis];//GetAxesData(axis, ref m_joystickAxes[stick]);

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

        public int GetStickAxisCount(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new SystemException("Joystick index is out of range, should be 0-5");
                }

                return m_joystickAxes[stick].count;
            }
        }

        public int GetStickPOV(int stick, int pov)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new SystemException("Joystick index is out of range, should be 0-5");
                }

                if (pov < 0 || pov >= MaxJoystickPOVs)
                {
                    throw new SystemException("Joystick POV is out of range");
                }


                if (pov >= m_joystickPOVs[stick].count)
                {
                    ReportJoystickUnpluggedError("WARNING: Joystick POV " + pov + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return -1;
                }

                return m_joystickPOVs[stick].povs[pov];//GetPOVData(pov, ref m_joystickPOVs[stick]);
            }
        }

        public int GetStickPOVCount(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new SystemException("Joystick index is out of range, should be 0-5");
                }

                return m_joystickPOVs[stick].count;
            }
        }

        public int GetStickButtons(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new SystemException("Joystick index is out of range, should be 0-5");
                }
                return (int)m_joystickButtons[stick].buttons;
            }
        }

        public bool GetStickButton(int stick, byte button)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new SystemException("Joystick index is out of range, should be 0-5");
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

        public int GetStickButtonCount(int stick)
        {
            lock (m_lockObject)
            {
                if (stick < 0 || stick >= JoystickPorts)
                {
                    throw new SystemException("Joystick index is out of range, should be 0-5");
                }

                return m_joystickButtons[stick].count;
            }
        }
        public bool Enabled
        {
            get
            {
                HALControlWord controlWord = GetControlWord();
                return controlWord.GetEnabled() && controlWord.GetDSAttached();
            }
        }

        public bool Disabled => !Enabled;

        public bool Autonomous
        {
            get
            {
                HALControlWord controlWord = GetControlWord();
                return controlWord.GetAutonomous();
            }
        }

        public bool Test
        {
            get
            {
                HALControlWord controlWord = GetControlWord();
                return controlWord.GetTest();
            }
        }

        public bool OperatorControl => !(Autonomous || Test);

        public bool IsSysActive()
        {
            int status = 0;
            bool retVal = HALGetSystemActive(ref status);
            return retVal;
        }

        public bool BrownedOut
        {
            get
            {
                int status = 0;
                bool retval = HALGetBrownedOut(ref status);
                return retval;
            }
        }

        public bool NewControlData => TryTakeSemaphore(m_newControlData) == 0;

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

        public bool FMSAttached => GetControlWord().GetFMSAttached();

        public bool DSAtached => GetControlWord().GetDSAttached();

        public double MatchTime
        {
            get
            {
                float temp = 0;
                HALGetMatchTime(ref temp);
                return temp;
            }
        }



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
            errorWriter.Close();


            HALControlWord controlWord = GetControlWord();
            if (controlWord.GetDSAttached())
            {
                SetErrorData(errorString, 0);
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
