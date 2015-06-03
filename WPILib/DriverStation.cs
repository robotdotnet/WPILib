

using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using HAL_Base;

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
        private static DriverStation s_instance = new DriverStation();
        private HALJoystickAxes[] m_joystickAxes = new HALJoystickAxes[JoystickPorts];
        private HALJoystickPOVs[] m_joystickPOVs = new HALJoystickPOVs[JoystickPorts];
        private HALJoystickButtons[] m_joystickButtons = new HALJoystickButtons[JoystickPorts];
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private Thread m_thread;
        private IntPtr m_newControlData;
        private IntPtr m_packetDataAvailableMultiWait;
        private IntPtr m_packetDataAvailableMutex;
        private IntPtr m_waitForDataSem;
        private IntPtr m_waitForDataMutex;
        private bool m_threadKeepAlive = true;
        private bool m_userInDisabled = false;
        private bool m_userInAutonomous = false;
        private bool m_userInTeleop = false;
        private bool m_userInTest = false;
        private double m_nextMessageTime = 0.0;

        private object m_lockObject = new object();

        private const double JoystickUnpluggedMessageInterval = 1.0;

        //Public Fields






        public static DriverStation GetInstance()
        {
            return DriverStation.s_instance;
        }


        protected DriverStation()
        {

            for (int i = 0; i < JoystickPorts; i++)
            {
                m_joystickButtons[i].count = 0;
                m_joystickAxes[i].count = 0;
                m_joystickPOVs[i].count = 0;
            }


            m_packetDataAvailableMultiWait = HALSemaphore.InitializeMultiWait();
            m_newControlData = HALSemaphore.InitializeSemaphore(0);

            m_waitForDataSem = HALSemaphore.InitializeMultiWait();
            m_waitForDataMutex = HALSemaphore.InitializeMutexNormal();


            m_packetDataAvailableMutex = HALSemaphore.InitializeMutexNormal();
            HAL.HALSetNewDataSem(m_packetDataAvailableMultiWait);



            m_thread = new Thread(Task) {Priority = ThreadPriority.Highest, IsBackground = true};
            m_thread.Start();
        }

        public void Release()
        {
            m_threadKeepAlive = false;
        }

        private void Task()
        {
            int safetyCounter = 0;
            while (m_threadKeepAlive)
            {
                HALSemaphore.TakeMultiWait(m_packetDataAvailableMultiWait, m_packetDataAvailableMutex, 0);
                GetData();
                HALSemaphore.GiveMultiWait(m_waitForDataSem);
                if (++safetyCounter >= 4)
                {
                    MotorSafetyHelper.CheckMotors();
                    safetyCounter = 0;
                }
                if (m_userInDisabled)
                    HAL.HALNetworkCommunicationObserveUserProgramDisabled();
                if (m_userInAutonomous)
                    HAL.HALNetworkCommunicationObserveUserProgramAutonomous();
                if (m_userInTeleop)
                    HAL.HALNetworkCommunicationObserveUserProgramTeleop();
                if (m_userInTest)
                    HAL.HALNetworkCommunicationObserveUserProgramTest();
            }
        }

        public void WaitForData(long timeout = 0)
        {
            HALSemaphore.TakeMultiWait(m_waitForDataSem, m_waitForDataMutex, -1);
        }

        protected void GetData()
        {
            for (byte stick = 0; stick < JoystickPorts; stick++)
            {
                HAL.HALGetJoystickAxes(stick, ref m_joystickAxes[stick]);
                HAL.HALGetJoystickPOVs(stick, ref m_joystickPOVs[stick]);
                HAL.HALGetJoystickButtons(stick, ref m_joystickButtons[stick]);
            }
            HALSemaphore.GiveSemaphore(m_newControlData);
        }

        public double GetBatteryVoltage()
        {
            int status = 0;
            return HALPower.GetVinVoltage(ref status);
        }

        private void ReportJoystickUnpluggedError(String message)
        {
            double currentTime = Timer.GetFPGATimestamp();
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

        /**
     * Gets a value indicating whether the Driver Station requires the
     * robot to be enabled.
     *
     * @return True if the robot is enabled, false otherwise.
     */

        public bool IsEnabled()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetEnabled() && controlWord.GetDSAttached();
        }

        /**
         * Gets a value indicating whether the Driver Station requires the
         * robot to be disabled.
         *
         * @return True if the robot should be disabled, false otherwise.
         */

        public bool IsDisabled()
        {
            return !IsEnabled();
        }

        /**
         * Gets a value indicating whether the Driver Station requires the
         * robot to be running in autonomous mode.
         *
         * @return True if autonomous mode should be enabled, false otherwise.
         */

        public bool IsAutonomous()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetAutonomous();
        }

        /**
         * Gets a value indicating whether the Driver Station requires the
         * robot to be running in test mode.
         * @return True if test mode should be enabled, false otherwise.
         */

        public bool IsTest()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetTest();
        }

        /**
         * Gets a value indicating whether the Driver Station requires the
         * robot to be running in operator-controlled mode.
         *
         * @return True if operator-controlled mode should be enabled, false otherwise.
         */

        public bool IsOperatorControl()
        {
            return !(IsAutonomous() || IsTest());
        }

        public bool IsSysActive()
        {
            int status = 0;
            bool retVal = HAL.HALGetSystemActive(ref status);
            return retVal;
        }

        public bool IsBrownedOut()
        {
            int status = 0;
            bool retval = HAL.HALGetBrownedOut(ref status);
            return retval;
        }

        public bool IsNewControlData()
        {
            return HALSemaphore.TryTakeSemaphore(m_newControlData) == 0;
        }

        public Alliance GetAlliance()
        {
            HALAllianceStationID allianceStationID = new HALAllianceStationID();

            HAL.HALGetAllianceStation(ref allianceStationID);

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
            HAL.HALGetAllianceStation(ref allianceStationID);

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

        public bool IsFMSAttached()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetFMSAttached();
        }

        public bool IsDSAttached()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetDSAttached();
        }

        public double GetMatchTime()
        {
            float temp = 0;
            HAL.HALGetMatchTime(ref temp);
            return temp;
        }



        public static void ReportError(String error, bool printTrace)
        {
            String errorString = error;
            if (printTrace)
            {
                errorString += " at ";
                var stacktrace = new StackTrace();
                var traces = stacktrace.GetFrames();
                errorString = traces.Aggregate(errorString, (current, s) => current + (s + "\n"));
            }
            TextWriter errorWriter = Console.Error;
            errorWriter.WriteLine(errorString);
            errorWriter.Close();


            HALControlWord controlWord = HAL.GetControlWord();
            if (controlWord.GetDSAttached())
            {
                HAL.SetErrorData(errorString, 0);
            }
        }

        /** Only to be used to tell the Driver Station what code you claim to be executing
     *   for diagnostic purposes only
     * @param entering If true, starting disabled code; if false, leaving disabled code */

        public void InDisabled(bool entering)
        {
            m_userInDisabled = entering;
        }

        /** Only to be used to tell the Driver Station what code you claim to be executing
        *   for diagnostic purposes only
         * @param entering If true, starting autonomous code; if false, leaving autonomous code */

        public void InAutonomous(bool entering)
        {
            m_userInAutonomous = entering;
        }

        /** Only to be used to tell the Driver Station what code you claim to be executing
        *   for diagnostic purposes only
         * @param entering If true, starting teleop code; if false, leaving teleop code */

        public void InOperatorControl(bool entering)
        {
            m_userInTeleop = entering;
        }

        /** Only to be used to tell the Driver Station what code you claim to be executing
         *   for diagnostic purposes only
         * @param entering If true, starting test code; if false, leaving test code */

        public void InTest(bool entering)
        {
            m_userInTest = entering;
        }

    }
}
